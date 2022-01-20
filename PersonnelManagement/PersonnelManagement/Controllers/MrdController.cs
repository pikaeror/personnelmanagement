using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using PersonnelManagement.USERS;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Mrd")]
    public class MrdController : Controller
    {

        private Repository repository;


        public MrdController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Mrd> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    if (repository.MrdsLocal() == null)
                    {
                        repository.UpdateMrdsLocal();
                    }
                    return repository.MrdsLocal().Values;
                }
            }
            List<Mrd> empty = new List<Mrd>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateMrd([FromBody]Mrd newMrd)
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
             * Means, we add new positiontype.
             */
            if (newMrd.Id == 0)
            {
                repository.AddMrd(newMrd);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Метка рода деятельности успешно добавлена");
            }
            else if (newMrd.Id != 0)
            {
                repository.UpdateMrd(newMrd);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Метка рода деятельности успешно обновлена");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}