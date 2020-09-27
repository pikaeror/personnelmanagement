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
    [Route("api/Personadditionalagreement")]
    public class PersonadditionalagreementController : Controller
    {

        private Repository repository;

        public PersonadditionalagreementController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostAdditionalagreement([FromBody] Personadditionalagreement personadditionalagreement)
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
            if (personadditionalagreement.Id == 0)
            {
                repository.AddPersonAdditionalagreement(user, personadditionalagreement);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Дополнительное соглашение добавлено");
            }
            else if (personadditionalagreement.Id > 0)
            {
                repository.ChangePersonAdditionalagreement(user, personadditionalagreement);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Дополнительное соглашение изменено");
            }
            else if (personadditionalagreement.Id < 0)
            {
                repository.DeletePersonAdditionalagreement(user, personadditionalagreement);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Дополнительное соглашение удалено");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}