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
    [Route("api/Drivertype")]
    public class DrivertypeController : Controller
    {

        private Repository repository;


        public DrivertypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Drivertype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Drivertypes;
                }
            }
            List<Drivertype> empty = new List<Drivertype>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateDrivertype([FromBody]Drivertype newDrivertype)
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
            if (newDrivertype.Id == 0)
            {
                repository.AddDrivertype(newDrivertype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид транспортного средства успешно добавлен");
            }
            else if (newDrivertype.Id != 0)
            {
                repository.UpdateDrivertype(newDrivertype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид транспортного средства успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}