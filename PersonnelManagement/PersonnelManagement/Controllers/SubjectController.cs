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
    [Route("api/Subject")]
    public class SubjectController : Controller
    {

        private Repository repository;


        public SubjectController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Subject> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    if (repository.SubjectsLocal() == null)
                    {
                        repository.UpdateSubjectsLocal();
                    }
                    return repository.SubjectsLocal().Values;
                }
            }
            List<Subject> empty = new List<Subject>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdatePositiontype([FromBody]Subject newSubject)
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
             * Отсутствие ID означает, что добавляем новый Subject
             */
            if (newSubject.Id == 0)
            {
                bool success = repository.AddSubject(newSubject);
                if (success)
                {
                    return new ObjectResult(Keys.SUCCESS_SHORT + ":Наименование успешно добавлено");
                }
                else
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Наименование уже занято");
                }

            }
            else
            {
                repository.UpdateSubject(newSubject);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Наименование успешно изменено");
            }
            //return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}