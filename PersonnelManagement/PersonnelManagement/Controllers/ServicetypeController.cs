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
    [Route("api/Servicetype")]
    public class ServicetypeController : Controller
    {

        private Repository repository;


        public ServicetypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Servicetype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Servicetypes;
                }
            }
            List<Servicetype> empty = new List<Servicetype>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateServicetype([FromBody]Servicetype newServicetype)
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
             * Добавляем новый вид службы
             */
            if (newServicetype.Id == 0)
            {
                repository.AddServicetype(newServicetype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид службы успешно добавлен");
            }
            else if (newServicetype.Id != 0)
            {
                repository.UpdateServicetype(newServicetype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид службы успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}