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
    [Route("api/Positiondecertificate")]
    public class PositiondecertificateController : Controller
    {

        private Repository repository;


        public PositiondecertificateController(Repository repository)
        {
            this.repository = repository;

        }


        [HttpPost()]
        public IActionResult UpdateDecertificate([FromBody]PositionDecertificate newPositionDecertificate)
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

            repository.AddPositionDecertificate(newPositionDecertificate);
            return new ObjectResult(Keys.SUCCESS_SHORT + ":Дата разаттестации изменена");
        }
    }
}