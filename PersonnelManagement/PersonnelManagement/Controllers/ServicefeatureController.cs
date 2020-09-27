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
    [Route("api/Servicefeature")]
    public class ServicefeatureController : Controller
    {

        private Repository repository;


        public ServicefeatureController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Servicefeature> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Servicefeatures;
                }
            }
            List<Servicefeature> empty = new List<Servicefeature>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateServicefeature([FromBody]Servicefeature newServicefeature)
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
             * Means, we add new service feature.
             */
            if (newServicefeature.Id == 0)
            {
                repository.AddServicefeature(newServicefeature);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Особенность службы успешно добавлена");
            }
            else if (newServicefeature.Id != 0)
            {
                repository.UpdateServicefeature(newServicefeature);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Особенность службы успешно обновлена");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}