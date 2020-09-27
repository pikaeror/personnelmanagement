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
    [Route("api/Persontransfer")]
    public class PersontransferController : Controller
    {

        private Repository repository;

        public PersontransferController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostTransfer([FromBody]Persontransfer persontransfer)
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

            if (persontransfer.Structuretoselect > 0)
            {
                Persontransfer output = repository.AppointTransfer(user, persontransfer);
                return new ObjectResult(output);
                //return new ObjectResult(Keys.SUCCESS_SHORT + ":Прикреплено к орг-штатной должности");
            }
            else if (persontransfer.Id == 0)
            {
                repository.AddPersonTransfer(user, persontransfer);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Зачисление в распоряжение добавлено");
            }
            else if (persontransfer.Id > 0)
            {
                repository.ChangePersonTransfer(user, persontransfer);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Зачисление в распоряжение изменено");
            }
            else if (persontransfer.Id < 0)
            {
                repository.DeletePersonTransfer(user, persontransfer);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Зачисление в распоряжение удалено");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}