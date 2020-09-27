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
    [Route("api/Servicecoef")]
    public class ServicecoefController : Controller
    {

        private Repository repository;


        public ServicecoefController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Servicecoef> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Servicecoefs;
                }
            }
            List<Servicecoef> empty = new List<Servicecoef>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateServicecoef([FromBody]Servicecoef newServicecoef)
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
            if (newServicecoef.Id == 0)
            {
                repository.AddServicecoef(newServicecoef);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Льготный коэффициент успешно добавлен");
            }
            else if (newServicecoef.Id != 0)
            {
                repository.UpdateServicecoef(newServicecoef);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Льготный коэффициент успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}