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
    [Route("api/Penalty")]
    public class PenaltyController : Controller
    {

        private Repository repository;


        public PenaltyController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Penalty> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Penalties;
                }
            }
            List<Penalty> empty = new List<Penalty>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdatePenalties([FromBody]Penalty newPenalty)
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
            if (newPenalty.Id == 0)
            {
                repository.AddPenalty(newPenalty);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид взыскания успешно добавлен");
            }
            else if (newPenalty.Id != 0)
            {
                repository.UpdatePenalty(newPenalty);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид взыскания успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }


    }
}