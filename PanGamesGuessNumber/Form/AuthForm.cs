using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.Logging;
using System.Text.Json;
using PanGamesFunction;
using PanGamesFunction.DB;
using System.IdentityModel.Tokens.Jwt;
using System.Xml;
using System;

namespace PanGamesGuessNumber
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }


        private void enterLabel_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("http://localhost:7140/api/authorization/" + loginTxtBox.Text + "/" + passwordTxtBox.Text).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            if ((responseBody == "Не правильный логин или пользователя не существует") || (responseBody == "Не верный пароль"))
            {
                MessageBox.Show(responseBody);
            }
            else
            {
                this.Hide();
                UserInfo user = new UserInfo();
                user = JsonSerializer.Deserialize<UserInfo>(responseBody);
                MainForm mainForm = new MainForm(user);
                mainForm.Show();
            }
        }

        private void authLabel_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("http://localhost:7140/api/registration/" + loginTxtBox.Text + "/" + passwordTxtBox.Text).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            MessageBox.Show(responseBody);

        }
    }
}