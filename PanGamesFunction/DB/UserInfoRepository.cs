using Microsoft.Extensions.Hosting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PanGamesFunction.DB
{
    public class UserInfoRepository
    {
        private string CONNECTION_STRING = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=PanGames";
        //private string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["PanGamesDB"].ConnectionString;
        public string Add(UserInfo user)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                if ((GetUserByLogin(user.Login) == null))
                {
                    string commandText = $"INSERT INTO {"userinfo"} (Login, Password, GamesCount, HiddenNumber) VALUES (@login, @password, @gamescount, @hiddennumber)";
                    using (var cmd = new NpgsqlCommand(commandText, connection))
                    {
                        cmd.Parameters.AddWithValue("Login", user.Login);
                        cmd.Parameters.AddWithValue("Password", user.Password);
                        cmd.Parameters.AddWithValue("GamesCount", user.GamesCount);
                        cmd.Parameters.AddWithValue("HiddenNumber", user.HiddenNumber);
                        cmd.ExecuteNonQuery();
                    }
                    return "success";
                } else
                {
                    return "already exists";
                }
            }
        }

        public UserInfo GetUserByLogin(string login)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                string commandText = $"SELECT * FROM {"userinfo"} WHERE Login = @Login";
                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("Login", login);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            UserInfo user = UserInfo.ReadUser(reader);
                            return user;
                        }
                }
                return null;
            }
        }
        public string Update(UserInfo user)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                var commandText = $@"UPDATE {"userinfo"}
                SET Password = @password, GamesCount = @gamescount, HiddenNumber = @hiddennumber
                WHERE login = @login";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("Login", user.Login);
                    cmd.Parameters.AddWithValue("Password", user.Password);
                    cmd.Parameters.AddWithValue("GamesCount", user.GamesCount);
                    cmd.Parameters.AddWithValue("HiddenNumber", user.HiddenNumber);
                    cmd.ExecuteNonQuery();
                }
            }
            return "success";
        }
    }

   
}
