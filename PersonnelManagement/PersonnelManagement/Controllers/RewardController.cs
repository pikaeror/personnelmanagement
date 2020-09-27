using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Reward")]
    public class RewardController : Controller
    {

        private Repository repository;


        public RewardController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Reward> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Rewards;
                }
            }
            List<Reward> empty = new List<Reward>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateReward([FromBody]Reward newReward)
        {
            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanWriteAdminData(user);
                if (!hasAccess)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");

                }
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }

            /**
             * Means, we add new structure type.
             */
            if (newReward.Id == 0)
            {
                repository.AddReward(newReward);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Награда успешно добавлена");
            }
            else if (newReward.Id != 0)
            {
                repository.UpdateReward(newReward);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Награда успешно обновлена");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}