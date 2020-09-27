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
    [Route("api/Rewardtype")]
    public class RewardtypeController : Controller
    {

        private Repository repository;


        public RewardtypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Rewardtype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Rewardtypes;
                }
            }
            List<Rewardtype> empty = new List<Rewardtype>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateRewardtype([FromBody]Rewardtype newRewardtype)
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
            if (newRewardtype.Id == 0)
            {
                repository.AddRewardtype(newRewardtype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид награды успешно добавлен");
            }
            else if (newRewardtype.Id != 0)
            {
                repository.UpdateRewardtype(newRewardtype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид награды успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}