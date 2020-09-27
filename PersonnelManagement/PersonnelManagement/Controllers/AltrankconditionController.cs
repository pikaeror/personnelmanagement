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
    [Route("api/Altrankcondition")]
    public class AltrankconditionController : Controller
    {

        private Repository repository;


        public AltrankconditionController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Altrankcondition> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    if (repository.AltrankconditionsLocal() == null)
                    {
                        repository.UpdateAltrankconditionsLocal();
                    }
                    return repository.AltrankconditionsLocal().Values;

                    //return repository.Altrankconditions;
                }
            }
            List<Altrankcondition> empty = new List<Altrankcondition>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateAltrankcondition([FromBody]Altrankcondition newAltrankcondition)
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
            if (newAltrankcondition.Id == 0)
            {
                repository.AddAltrankcondition(newAltrankcondition);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Условие для вилки званий успешно добавлено");
            }
            else if (newAltrankcondition.Id != 0)
            {
                repository.UpdateAltrankcondition(newAltrankcondition);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Условие для вилки званий успешно обновлено");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}