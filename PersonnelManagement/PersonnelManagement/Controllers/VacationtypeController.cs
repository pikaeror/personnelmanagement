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
    [Route("api/Vacationtype")]
    public class VacationtypeController : Controller
    {

        private Repository repository;


        public VacationtypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Vacationtype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Vacationtypes;
                }
            }
            List<Vacationtype> empty = new List<Vacationtype>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateVacationtype([FromBody]Vacationtype newVacationtype)
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
            if (newVacationtype.Id == 0)
            {
                repository.AddVacationtype(newVacationtype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид отпуска успешно добавлен");
            }
            else if (newVacationtype.Id != 0)
            {
                repository.UpdateVacationtype(newVacationtype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид отпуска успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}