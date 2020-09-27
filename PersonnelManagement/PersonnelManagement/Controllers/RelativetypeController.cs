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
    [Route("api/Relativetype")]
    public class RelativetypeController : Controller
    {

        private Repository repository;


        public RelativetypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Relativetype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Relativetypes;
                }
            }
            List<Relativetype> empty = new List<Relativetype>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateRelativetype([FromBody]Relativetype newRelativetype)
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
            if (newRelativetype.Id == 0)
            {
                repository.AddRelativetype(newRelativetype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Тип родственника успешно добавлен");
            }
            else if (newRelativetype.Id != 0)
            {
                repository.UpdateRelativetype(newRelativetype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Тип родственника успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }


    }
}