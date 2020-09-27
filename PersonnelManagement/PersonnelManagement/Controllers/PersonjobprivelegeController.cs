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
    [Route("api/Personjobprivelege")]
    public class PersonjobprivelegeController : Controller
    {

        private Repository repository;

        public PersonjobprivelegeController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostJobprivelege([FromBody]Personjobprivelege personjobprivelege)
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

            if (personjobprivelege.Id == 0)
            {
                repository.AddPersonJobprivelege(user, personjobprivelege);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Льготный период службы добавлен");
            }
            else if (personjobprivelege.Id > 0)
            {
                repository.ChangePersonJobprivelege(user, personjobprivelege);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Льготный период службы изменен");
            }
            else if (personjobprivelege.Id < 0)
            {
                repository.DeletePersonJobprivelege(user, personjobprivelege);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Льготный период службы удален");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}