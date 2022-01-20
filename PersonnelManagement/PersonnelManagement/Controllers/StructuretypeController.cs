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
    [Route("api/Structuretype")]
    public class StructuretypeController : Controller
    {
        private Repository repository;


        public StructuretypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Structuretype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Structuretypes;
                }
            }
            List<Structuretype> empty = new List<Structuretype>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateStructuretype([FromBody]Structuretype newStructuretype)
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
            if (newStructuretype.Id == 0)
            {
                repository.AddStructuretype(newStructuretype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Тип подразделения успешно добавлен");
            }
            else if (newStructuretype.Id != 0)
            {
                repository.UpdateStructuretype(newStructuretype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Тип подразделения успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }


    }
}