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
    [Route("api/Permissiontype")]
    public class PermissiontypeController : Controller
    {
        private Repository repository;


        public PermissiontypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Permissiontype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Permissiontypes;
                }
            }
            List<Permissiontype> empty = new List<Permissiontype>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdatePermissiontype([FromBody]Permissiontype newPermissiontype)
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
            if (newPermissiontype.Id == 0)
            {
                repository.AddPermissiontype(newPermissiontype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид разрешения успешно добавлен");
            }
            else if (newPermissiontype.Id != 0)
            {
                repository.UpdatePermissiontype(newPermissiontype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Вид разрешения успешно обновлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}