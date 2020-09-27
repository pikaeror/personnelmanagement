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
    [Route("api/Personattestation")]
    public class PersonattestationController : Controller
    {

        private Repository repository;

        public PersonattestationController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostAttestation([FromBody]Personattestation personattestation)
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
            if (personattestation.Id == 0)
            {
                repository.AddPersonAttestation(user, personattestation);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Аттестация добавлена");
            }
            else if (personattestation.Id > 0)
            {
                repository.ChangePersonAttestation(user, personattestation);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Аттестация изменена");
            }
            else if (personattestation.Id < 0)
            {
                repository.DeletePersonAttestation(user, personattestation);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Аттестация удалена");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}