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
    [Route("api/Educationdocument")]
    public class EducationdocumentController : Controller
    {

        private Repository repository;


        public EducationdocumentController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Educationdocument> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Educationdocuments;
                }
            }
            List<Educationdocument> empty = new List<Educationdocument>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateEducationlevel([FromBody]Educationdocument newEducationdocument)
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
            if (newEducationdocument.Id == 0)
            {
                repository.AddEducationdocument(newEducationdocument);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Документ образования успешно добавлен");
            }
            else if (newEducationdocument.Id != 0)
            {
                repository.UpdateEducationdocument(newEducationdocument);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Документ образования успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }


    }
}