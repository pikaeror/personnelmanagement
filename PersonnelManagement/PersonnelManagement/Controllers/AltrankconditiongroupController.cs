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
    [Route("api/Altrankconditiongroup")]
    public class AltrankconditiongroupController : Controller
    {

        private Repository repository;


        public AltrankconditiongroupController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Altrankconditiongroup> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    if (repository.AltrankconditiongroupsLocal() == null)
                    {
                        repository.UpdateAltrankconditiongroupsLocal();
                    }
                    return repository.AltrankconditiongroupsLocal().Values;

                    //return repository.Altrankconditiongroups;
                }
            }
            List<Altrankconditiongroup> empty = new List<Altrankconditiongroup>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateAltrankconditiongroup([FromBody]Altrankconditiongroup newAltrankconditiongroup)
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
            if (newAltrankconditiongroup.Id == 0)
            {
                repository.AddAltrankconditiongroup(newAltrankconditiongroup);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вилка званий успешно добавлена");
            }
            else if (newAltrankconditiongroup.Id != 0)
            {
                repository.UpdateAltrankconditiongroup(newAltrankconditiongroup);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вилка званий успешно обновлена");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}