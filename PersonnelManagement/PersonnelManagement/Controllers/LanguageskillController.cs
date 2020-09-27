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
    [Route("api/Languageskill")]
    public class LanguageskillController : Controller
    {

        private Repository repository;


        public LanguageskillController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Languageskill> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Languageskills;
                }
            }
            List<Languageskill> empty = new List<Languageskill>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateLanguageskill([FromBody]Languageskill newLanguageskill)
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
            if (newLanguageskill.Id == 0)
            {
                repository.AddLanguageskill(newLanguageskill);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Степень владения языком успешно добавлена");
            }
            else if (newLanguageskill.Id != 0)
            {
                repository.UpdateLanguageskill(newLanguageskill);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Степень владения  успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }
    }
}