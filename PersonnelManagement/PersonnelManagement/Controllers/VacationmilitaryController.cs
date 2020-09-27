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
    [Route("api/Vacationmilitary")]
    public class VacationmilitaryController : Controller
    {

        private Repository repository;


        public VacationmilitaryController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Vacationmilitary> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Vacationmilitaries;
                }
            }
            List<Vacationmilitary> empty = new List<Vacationmilitary>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateVacationmilitary([FromBody]Vacationmilitary newVacationmilitary)
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
            if (newVacationmilitary.Id == 0)
            {
                repository.AddVacationmilitary(newVacationmilitary);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Тип отпуска успешно добавлен");
            }
            else if (newVacationmilitary.Id != 0)
            {
                repository.UpdateVacationmilitary(newVacationmilitary);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Тип отпуска успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}