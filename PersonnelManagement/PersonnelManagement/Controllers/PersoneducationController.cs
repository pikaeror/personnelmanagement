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
    [Route("api/Personeducation")]
    public class PersoneducationController : Controller
    {

        private Repository repository;

        public PersoneducationController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostEducation([FromBody]Personeducation personeducation)
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
            if (personeducation.Id == 0)
            {
                repository.AddPersonEducation(user, personeducation);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Образование добавлено");
            }
            else if (personeducation.Id > 0)
            {
                repository.ChangePersonEducation(user, personeducation);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Образование изменено");
            }
            else if (personeducation.Id < 0)
            {
                repository.DeletePersonEducation(user, personeducation);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Образование удалено");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}