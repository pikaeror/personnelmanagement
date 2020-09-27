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
    [Route("api/Languagetype")]
    public class LanguagetypeController : Controller
    {

        private Repository repository;


        public LanguagetypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Languagetype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Languagetypes;
                }
            }
            List<Languagetype> empty = new List<Languagetype>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateLanguagetype([FromBody]Languagetype newLanguagetype)
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
            if (newLanguagetype.Id == 0)
            {
                repository.AddLanguagetype(newLanguagetype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Язык успешно добавлен");
            }
            else if (newLanguagetype.Id != 0)
            {
                repository.UpdateLanguagetype(newLanguagetype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Язык успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}