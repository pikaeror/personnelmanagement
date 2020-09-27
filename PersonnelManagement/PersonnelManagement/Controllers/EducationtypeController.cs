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
    [Route("api/Educationtype")]
    public class EducationtypeController : Controller
    {

        private Repository repository;


        public EducationtypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Educationtype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Educationtypes;
                }
            }
            List<Educationtype> empty = new List<Educationtype>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateEducationtype([FromBody]Educationtype newEducationtype)
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
            if (newEducationtype.Id == 0)
            {
                repository.AddEducationtype(newEducationtype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Форма обучения успешно добавлена");
            }
            else if (newEducationtype.Id != 0)
            {
                repository.UpdateEducationtype(newEducationtype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Форма обучения успешно обновлена");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}