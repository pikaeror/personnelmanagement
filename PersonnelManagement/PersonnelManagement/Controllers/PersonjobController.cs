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
    [Route("api/Personjob")]
    public class PersonjobController : Controller
    {

        private Repository repository;

        public PersonjobController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostJob([FromBody]Personjob personjob)
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

            // Режим выбрать должность или назначить на должность с выбором должности из 
            if (personjob.Positiontoselect > 0)
            {
                Personjob output = repository.AppointPersonJob(user, personjob);
                return new ObjectResult(output);
                //return new ObjectResult(Keys.SUCCESS_SHORT + ":Прикреплено к орг-штатной должности");
            }
            else if (personjob.Id == 0)
            {
                repository.AddPersonJob(user, personjob);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Работа добавлена");
            }
            else if (personjob.Id > 0)
            {
                repository.ChangePersonJob(user, personjob);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Работа изменена");
            }
            else if (personjob.Id < 0)
            {
                repository.DeletePersonJob(user, personjob);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Работа удалена");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}