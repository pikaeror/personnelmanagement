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
    [Route("api/Personill")]
    public class PersonillController : Controller
    {

        private Repository repository;

        public PersonillController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostIll([FromBody]Personill personill)
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
            if (personill.Id == 0)
            {
                repository.AddPersonIll(user, personill);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Листок нетрудоспособности добавлен");
            }
            else if (personill.Id > 0)
            {
                repository.ChangePersonIll(user, personill);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Листок нетрудоспособности изменен");
            }
            else if (personill.Id < 0)
            {
                repository.DeletePersonIll(user, personill);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Листок нетрудоспособности удален");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        // GET: api/Personill/Toggleprivelege5
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

            Personill contextPersonill = repository.Personills.FirstOrDefault(p => p.Id == id);
            if (contextPersonill != null)
            {
                if (contextPersonill.Privelege > 0)
                {
                    contextPersonill.Privelege = 0;
                } else
                {
                    contextPersonill.Privelege = 1;
                }
                repository.SaveChanges();
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Изменено");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}