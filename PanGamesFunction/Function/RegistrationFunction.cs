using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using PanGamesFunction.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanGamesFunction.Function
{
    public class RegistrationFunction
    {
        [FunctionName("RegistrationFunction")]
        public IActionResult RunAuth(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "registration/{login}/{password}")] HttpRequest req,
        string login, string password)
        {
            UserInfoRepository auth = new UserInfoRepository();
            UserInfo user = new UserInfo();
            user.Login = login;
            user.Password = password;
            if (auth.Add(user) == "success")
            {
                return new OkObjectResult("Успешная регистрация");
            } else
            {
                return new OkObjectResult("Такой логин уже используется");
            }

        }
    }
}
