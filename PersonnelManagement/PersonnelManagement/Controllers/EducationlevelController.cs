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
    [Route("api/Educationlevel")]
    public class EducationlevelController : Controller
    {

        private Repository repository;


        public EducationlevelController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Educationlevel> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Educationlevels;
                }
            }
            List<Educationlevel> empty = new List<Educationlevel>();
            return empty;
        }
        [HttpPost()]
        public IActionResult UpdateEducationlevel([FromBody]Educationlevel newEducationlevel)
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
            if (newEducationlevel.Id == 0)
            {
                repository.AddEducationlevel(newEducationlevel);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Уровень образования успешно добавлен");
            }
            else if (newEducationlevel.Id != 0)
            {
                repository.UpdateEducationlevel(newEducationlevel);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Уровень образования успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }


    }
}