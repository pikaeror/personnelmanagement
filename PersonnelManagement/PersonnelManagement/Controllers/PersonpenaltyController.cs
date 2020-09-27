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
    [Route("api/Personpenalty")]
    public class PersonpenaltyController : Controller
    {

        private Repository repository;

        public PersonpenaltyController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostPenalty([FromBody]Personpenalty personpenalty)
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
            if (personpenalty.Id == 0)
            {
                repository.AddPersonPenalty(user, personpenalty);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Взыскание добавлено");
            }
            else if (personpenalty.Id > 0)
            {
                repository.ChangePersonPenalty(user, personpenalty);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Взыскание изменено");
            }
            else if (personpenalty.Id < 0)
            {
                repository.DeletePersonPenalty(user, personpenalty);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Взыскание удалено");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}