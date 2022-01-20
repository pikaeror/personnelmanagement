using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using PersonnelManagement.USERS;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Altrank")]
    public class AltrankController : Controller
    {

        private Repository repository;


        public AltrankController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Altrank> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadAdminData(user);
                if (hasAccess)
                {
                    if (repository.AltranksLocal() == null)
                    {
                        repository.UpdateAltranksLocal();
                    }
                    return repository.AltranksLocal().Values;

                    //return repository.Altranks;
                }
            }
            List<Altrank> empty = new List<Altrank>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateAltrankcondition([FromBody]Altrank newAltrank)
        {
            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanWriteAdminData(user);
                if (!hasAccess)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");

                }
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }

            /**
             * Means, we add new positiontype.
             */
            if (newAltrank.Id == 0)
            {
                repository.AddAltrank(newAltrank);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Звание для вилки званий успешно добавлено");
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
            }
        }

    }
}