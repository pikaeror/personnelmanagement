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
    [Route("api/Illcode")]
    public class IllcodeController : Controller
    {
        private Repository repository;


        public IllcodeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Illcode> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Illcodes;
                }
            }
            List<Illcode> empty = new List<Illcode>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateIllcode([FromBody]Illcode newIllcode)
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
            if (newIllcode.Id == 0)
            {
                repository.AddIllcode(newIllcode);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Код заболевания успешно добавлен");
            }
            else if (newIllcode.Id != 0)
            {
                repository.UpdateIllcode(newIllcode);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Код заболевания успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }
    }
}