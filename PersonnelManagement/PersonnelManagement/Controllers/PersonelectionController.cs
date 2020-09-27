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
    [Route("api/Personelection")]
    public class PersonelectionController : Controller
    {

        private Repository repository;

        public PersonelectionController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostElection([FromBody]Personelection personelection)
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
            if (personelection.Id == 0)
            {
                repository.AddPersonElection(user, personelection);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Участие в выборных органах добавлено");
            }
            else if (personelection.Id > 0)
            {
                repository.ChangePersonElection(user, personelection);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Участие в выборных органах изменено");
            }
            else if (personelection.Id < 0)
            {
                repository.DeletePersonElection(user, personelection);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Участие в выборных органах удалено");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}