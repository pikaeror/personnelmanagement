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
    [Route("api/Personscience")]
    public class PersonscienceController : Controller
    {

        private Repository repository;

        public PersonscienceController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostScience([FromBody]Personscience personscience)
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
            if (personscience.Id == 0)
            {
                repository.AddPersonScience(user, personscience);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Научный труд, звание, изобретение добавлено");
            }
            else if (personscience.Id > 0)
            {
                repository.ChangePersonScience(user, personscience);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Научный труд, звание, изобретение изменено");
            }
            else if (personscience.Id < 0)
            {
                repository.DeletePersonScience(user, personscience);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Научный труд, звание, изобретение удалено");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}