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
    [Route("api/Personrank")]
    public class PersonrankController : Controller
    {

        private Repository repository;

        public PersonrankController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostRank([FromBody]Personrank personrank)
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
            if (personrank.Id == 0)
            {
                repository.AddPersonRank(user, personrank);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Звание добавлено");
            } else if (personrank.Id > 0)
            {
                repository.ChangePersonRank(user, personrank);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Звание изменено");
            } else if (personrank.Id < 0)
            {
                repository.DeletePersonRank(user, personrank);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Звание удалено");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}