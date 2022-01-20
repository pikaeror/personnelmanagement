using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using PersonnelManagement.USERS;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Ranks")]
    public class RanksController : Controller
    {
        private Repository repository;


        public RanksController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Rank> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Rank> ranks = repository.RanksLocal().Values.OrderBy(o => o.Order);
                    List<Rank> ranksTemp = ranks.ToList();
                    return ranks; // ??? Может rankstemp возвращать надо?
                }
            }
            List<Rank> empty = new List<Rank>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateRank([FromBody]Rank newRank)
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
             * Means, we add new rank.
             */
            if (newRank.Id == 0)
            {
                repository.AddRank(newRank);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Звание успешно добавлено");
            } else if (newRank.Order.GetValueOrDefault() == 0 ) 
            {
                repository.UpdateRank(newRank);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Звание успешно обновлено");
            } else if (newRank.Order.GetValueOrDefault() != 0)
            {
                repository.UpdateRankOrder(newRank);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Порядок звания обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }
    }
}