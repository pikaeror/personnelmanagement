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
    [Route("api/Personreward")]
    public class PersonrewardController : Controller
    {

        private Repository repository;

        public PersonrewardController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostReward([FromBody]Personreward personreward)
        {

            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanEditPerson(user, repository);
                if (!hasAccess)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");

                }
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }
            if (personreward.Id == 0)
            {
                repository.AddPersonReward(user, personreward);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Награда добавлена");
            }
            else if (personreward.Id > 0)
            {
                repository.ChangePersonReward(user, personreward);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Награда изменена");
            }
            else if (personreward.Id < 0)
            {
                repository.DeletePersonReward(user, personreward);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Награда удалена");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}