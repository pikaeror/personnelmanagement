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
    [Route("api/Personworktrip")]
    public class PersonworktripController : Controller
    {

        private Repository repository;

        public PersonworktripController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostWorktrip([FromBody]Personworktrip personworktrip)
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
            if (personworktrip.Id == 0)
            {
                repository.AddPersonWorktrip(user, personworktrip);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Служебная загранпоездка добавлена");
            }
            else if (personworktrip.Id > 0)
            {
                repository.ChangePersonWorktrip(user, personworktrip);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Служебная загранпоездка изменена");
            }
            else if (personworktrip.Id < 0)
            {
                repository.DeletePersonWorktrip(user, personworktrip);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Служебная загранпоездка удалена");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        // GET: api/Personworktrip/Toggleprivelege5
        [HttpGet("Toggleprivelege{id}")]
        public IActionResult Toggleprivelege([FromRoute] int id)
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

            Personworktrip contextPersonworktrip = repository.Personworktrips.FirstOrDefault(p => p.Id == id);
            if (contextPersonworktrip != null)
            {
                if (contextPersonworktrip.Privelege > 0)
                {
                    contextPersonworktrip.Privelege = 0;
                }
                else
                {
                    contextPersonworktrip.Privelege = 1;
                }
                repository.SaveChanges();
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Изменено");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}