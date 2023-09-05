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
    public class GameFunction
    {
        [FunctionName("GameFunction")]
        public IActionResult RunGame(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "game/{number}/{login}")] HttpRequest req, int number, string login)
        {
            UserInfo user = new UserInfo();
            UserInfoRepository userInfoCRUD = new UserInfoRepository();
            user.Login = login;
            user.HiddenNumber = userInfoCRUD.GetUserByLogin(user.Login).HiddenNumber;            
                if (number == user.HiddenNumber)
                    return new OkObjectResult("Число отгадано");
                else if (number < user.HiddenNumber)
                {
                    return new OkObjectResult("Число отгадано");
                }
                else if (number > user.HiddenNumber)
                {
                    return new OkObjectResult("Ваше число больше загаданного");
                }
            return null;
        }
    }
}
