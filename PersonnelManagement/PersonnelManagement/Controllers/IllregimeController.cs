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
    [Route("api/Illregime")]
    public class IllregimeController : Controller
    {

        private Repository repository;


        public IllregimeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Illregime> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Illregimes;
                }
            }
            List<Illregime> empty = new List<Illregime>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateIllregime([FromBody]Illregime newIllregime)
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
             * Means, we add new structure type.
             */
            if (newIllregime.Id == 0)
            {
                repository.AddIllregime(newIllregime);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Режим лечения успешно добавлен");
            }
            else if (newIllregime.Id != 0)
            {
                repository.UpdateIllregime(newIllregime);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Режим лечения успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }


    }
}