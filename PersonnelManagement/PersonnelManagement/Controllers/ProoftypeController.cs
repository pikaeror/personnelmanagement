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
    [Route("api/Prooftype")]
    public class ProoftypeController : Controller
    {

        private Repository repository;


        public ProoftypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Prooftype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Prooftypes;
                }
            }
            List<Prooftype> empty = new List<Prooftype>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateProoftype([FromBody]Prooftype newProoftype)
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
            if (newProoftype.Id == 0)
            {
                repository.AddProoftype(newProoftype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид документа-основания успешно добавлен");
            }
            else if (newProoftype.Id != 0)
            {
                repository.UpdateProoftype(newProoftype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид документа-основания успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}