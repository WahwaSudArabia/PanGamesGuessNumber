using PanGamesFunction.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PanGamesGuessNumber
{
    public partial class MainForm : Form
    {
        public MainForm(UserInfo user)
        {
            login = user.Login;
            password = user.Password;
            gamesCount = user.GamesCount;
            InitializeComponent();

        }

        private string login;
        private string password;
        private int? gamesCount;
        private void startLabel_Click(object sender, EventArgs e)
        {
            guessNumberTextBox.Visible = true;
            GuessLabel.Visible = true;
            HttpClient client = new HttpClient();
            var response = client.GetAsync("http://localhost:7140/api/startgame/" + login + "/" + password).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            MessageBox.Show(responseBody);
        }

        private void GuessLabel_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("http://localhost:7140/api/game/" + guessNumberTextBox.Text + "/" + login).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            if (responseBody == "Число отгадано")
            {
                guessNumberTextBox.Visible = false;
                GuessLabel.Visible = false;
                MessageBox.Show(responseBody);
            }
            else
            MessageBox.Show(responseBody);
        }
    }
}
