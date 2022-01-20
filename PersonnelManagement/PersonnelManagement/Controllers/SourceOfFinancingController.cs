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
    [Route("api/SourceOfFinancing")]
    public class SourceOfFinancingController : Controller
    {
        private Repository repository;


        public SourceOfFinancingController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Sourceoffinancing> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    if (repository.SourceoffinancingsLocal() == null)
                    {
                        repository.UpdateSourceoffinancingsLocal();
                    }
                    return repository.SourceoffinancingsLocal().Values;

                    //return repository.Sourcesoffinancings;
                }
            }
            List<Sourceoffinancing> empty = new List<Sourceoffinancing>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateSourceOfFinancing([FromBody]Sourceoffinancing newSourceoffinancing)
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
             * Means, we add new rank.
             */
            if (newSourceoffinancing.Id == 0)
            {
                repository.AddSourceOfFinancing(newSourceoffinancing);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Тип источника финансирования успешно добавлен");
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
            }
        }
    }
}