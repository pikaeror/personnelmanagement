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
    [Route("api/Personvvk")]
    public class PersonvvkController : Controller
    {

        private Repository repository;

        public PersonvvkController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostVvk([FromBody]Personvvk personvvk)
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

            if (personvvk.Id == 0)
            {
                repository.AddPersonVvk(user, personvvk);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Результат ВВК добавлен");
            }
            else if (personvvk.Id > 0)
            {
                repository.ChangePersonVvk(user, personvvk);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Результат ВВК изменен");
            }
            else if (personvvk.Id < 0)
            {
                repository.DeletePersonVvk(user, personvvk);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Результат ВВК удален");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}