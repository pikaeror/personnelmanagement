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
    [Route("api/Holiday")]
    public class HolidayController : Controller
    {
        private Repository repository;

        public HolidayController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet()]
        public IEnumerable<Holiday> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Holidays;
                }
            }
            List<Holiday> empty = new List<Holiday>();
            return empty;
        }

        [HttpPost()]
        public IActionResult PostHoliday([FromBody]Holiday holiday)
        {

            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanEditPerson(user, repository);
                if (!hasAccess)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");

                }
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }

            if (holiday.Id == 0)
            {
                repository.AddHoliday(user, holiday);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Праздник добавлен");
            }
            else if (holiday.Id > 0)
            {
                repository.ChangeHoliday(user, holiday);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Праздник изменен");
            }
            else if (holiday.Id < 0)
            {
                repository.DeleteHoliday(user, holiday);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Праздник удален");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}