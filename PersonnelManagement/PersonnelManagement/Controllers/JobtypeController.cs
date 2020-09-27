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
    [Route("api/Jobtype")]
    public class JobtypeController : Controller
    {

        private Repository repository;


        public JobtypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Jobtype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Jobtypes;
                }
            }
            List<Jobtype> empty = new List<Jobtype>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateJobtype([FromBody]Jobtype newJobtype)
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
             * Добавляем вид трудовой деятельности
             */
            if (newJobtype.Id == 0)
            {
                repository.AddJobtype(newJobtype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид трудовой деятельности успешно добавлен");
            }
            else if (newJobtype.Id != 0)
            {
                repository.UpdateJobtype(newJobtype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид трудовой деятельности успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}