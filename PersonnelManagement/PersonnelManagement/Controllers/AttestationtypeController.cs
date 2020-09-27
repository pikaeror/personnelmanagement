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
    [Route("api/Attestationtype")]
    public class AttestationtypeController : Controller
    {

        private Repository repository;


        public AttestationtypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Attestationtype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Attestationtypes;
                }
            }
            List<Attestationtype> empty = new List<Attestationtype>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateAttestationtype([FromBody]Attestationtype newAttestationtype)
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
            if (newAttestationtype.Id == 0)
            {
                repository.AddAttestationtype(newAttestationtype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Тип аттестации успешно добавлен");
            }
            else if (newAttestationtype.Id != 0)
            {
                repository.UpdateAttestationtype(newAttestationtype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Тип аттестации успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}