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
    [Route("api/Personlanguage")]
    public class PersonlanguageController : Controller
    {

        private Repository repository;

        public PersonlanguageController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostLanguage([FromBody]Personlanguage personlanguage)
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

            if (personlanguage.Id == 0)
            {
                repository.AddPersonLanguage(user, personlanguage);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Владение иностранным языком добавлено");
            }
            else if (personlanguage.Id > 0)
            {
                repository.ChangePersonLanguage(user, personlanguage);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Владение иностранным языком изменено");
            }
            else if (personlanguage.Id < 0)
            {
                repository.DeletePersonLanguage(user, personlanguage);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Владение иностранным языком удалено");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}