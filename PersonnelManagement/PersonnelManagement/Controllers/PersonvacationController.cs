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
    [Route("api/Personvacation")]
    public class PersonvacationController : Controller
    {

        private Repository repository;

        public PersonvacationController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostContract([FromBody]Personvacation personvacation)
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

            if (personvacation.Id == 0)
            {
                repository.AddPersonVacation(user, personvacation);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Отпуск добавлен");
            }
            else if (personvacation.Id > 0)
            {
                repository.ChangePersonVacation(user, personvacation);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Отпуск изменен");
            }
            else if (personvacation.Id < 0)
            {
                repository.DeletePersonVacation(user, personvacation);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Отпуск удален");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}