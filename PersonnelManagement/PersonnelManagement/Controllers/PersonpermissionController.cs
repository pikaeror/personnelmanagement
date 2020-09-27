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
    [Route("api/Personpermission")]
    public class PersonpermissionController : Controller
    {

        private Repository repository;

        public PersonpermissionController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostPermission([FromBody]Personpermission personpermission)
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

            if (personpermission.Id == 0)
            {
                repository.AddPersonPermission(user, personpermission);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Разрешение добавлено");
            }
            else if (personpermission.Id > 0)
            {
                repository.ChangePersonPermission(user, personpermission);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Разрешение изменено");
            }
            else if (personpermission.Id < 0)
            {
                repository.DeletePersonPermission(user, personpermission);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Разрешение удалено");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}