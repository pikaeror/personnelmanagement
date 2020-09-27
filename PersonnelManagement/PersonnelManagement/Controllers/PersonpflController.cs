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
    [Route("api/Personpfl")]
    public class PersonpflController : Controller
    {

        private Repository repository;

        public PersonpflController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult UploadFile([FromBody]PersonphotoManager personphotoManager)
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
            Personphoto personphoto = personphotoManager.ToPersonphoto();
            repository.UploadPhoto(user, personphoto);
            //repository.UpdatePerson(user, person);
            return new ObjectResult(Keys.SUCCESS_SHORT + ":Фотография обновлена");
            //return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}