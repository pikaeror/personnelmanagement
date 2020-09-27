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
    [Route("api/Personcontract")]
    public class PersoncontractController : Controller
    {

        private Repository repository;

        public PersoncontractController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostContract([FromBody]Personcontract personcontract)
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

            if (personcontract.Id == 0)
            {
                repository.AddPersonContract(user, personcontract);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Контракт добавлен");
            }
            else if (personcontract.Id > 0)
            {
                repository.ChangePersonContract(user, personcontract);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Контракт изменен");
            }
            else if (personcontract.Id < 0)
            {
                repository.DeletePersonContract(user, personcontract);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Контракт удален");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }


    }
}