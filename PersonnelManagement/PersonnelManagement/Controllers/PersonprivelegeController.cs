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
    [Route("api/Personprivelege")]
    public class PersonprivelegeController : Controller
    {

        private Repository repository;

        public PersonprivelegeController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostPersonprivelege([FromBody]Personprivelege personprivelege)
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
            //repository.AddPersonContract(user, personcontract);
            //return new ObjectResult(Keys.SUCCESS_SHORT + ":Контракт добавлен");

            if (personprivelege.Id == 0)
            {
                repository.AddPersonPrivelege(user, personprivelege);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Льгота добавлена");
            }
            else if (personprivelege.Id > 0)
            {
                repository.ChangePersonPrivelege(user, personprivelege);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Льгота изменена");
            }
            else if (personprivelege.Id < 0)
            {
                repository.DeletePersonPrivelege(user, personprivelege);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Льгота удалена");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}