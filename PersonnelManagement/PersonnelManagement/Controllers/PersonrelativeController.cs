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
    [Route("api/Personrelative")]
    public class PersonrelativeController : Controller
    {
        private Repository repository;

        public PersonrelativeController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostRelative([FromBody]Personrelative personrelative)
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
            if (personrelative.Id == 0)
            {
                repository.AddPersonRelative(user, personrelative);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Родственник добавлен");
            }
            else if (personrelative.Id > 0)
            {
                repository.ChangePersonRelative(user, personrelative);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Родственник изменен");
            }
            else if (personrelative.Id < 0)
            {
                repository.DeletePersonRelative(user, personrelative);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Родственник удален");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}