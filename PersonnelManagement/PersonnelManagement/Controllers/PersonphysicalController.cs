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
    [Route("api/Personphysical")]
    public class PersonphysicalController : Controller
    {

        private Repository repository;

        public PersonphysicalController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostPhysical([FromBody]PersonphysicalManager personphysical)
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
            if (personphysical.Id == 0)
            {
                repository.AddPersonPhysical(user, personphysical);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Результат сдачи норматива добавлен");
            }
            else if (personphysical.Id > 0)
            {
                repository.ChangePersonPhysical(user, personphysical);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Результат сдачи норматива изменен");
            }
            else if (personphysical.Id < 0)
            {
                repository.DeletePersonPhysical(user, personphysical);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Результат сдачи норматива удален");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}