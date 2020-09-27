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
    [Route("api/Persondriver")]
    public class PersondriverController : Controller
    {

        private Repository repository;

        public PersondriverController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostDriver([FromBody]Persondriver persondriver)
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

            if (persondriver.Id == 0)
            {
                repository.AddPersonDriver(user, persondriver);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Водительское удостоверение добавлено");
            }
            else if (persondriver.Id > 0)
            {
                repository.ChangePersonDriver(user, persondriver);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Водительское удостоверение изменено");
            }
            else if (persondriver.Id < 0)
            {
                repository.DeletePersonDriver(user, persondriver);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Водительское удостоверение удалено");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}