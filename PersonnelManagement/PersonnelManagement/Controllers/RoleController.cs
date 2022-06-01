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
    [Route("api/Role")]
    public class RoleController : Controller
    {

        private Repository repository;

        public RoleController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet()]
        public IEnumerable<Role> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    //IEnumerable<Role> roles = repository.RolesLocal().Values.OrderBy(o => o.Order);
                    IEnumerable<Role> roles = repository.RolesLocal().Values;
                    List<Role> rolesTemp = roles.ToList();
                    return rolesTemp;
                }
            }
            List<Role> empty = new List<Role>();
            return empty;
        }

        [HttpPost()]
        public IActionResult PostRole([FromBody]Role role)
        {

            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
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
            if (role.Id == 0)
            {
                //repository.AddPersonRelative(user, personrelative);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Роль добавлена");
            }
            else if (role.Id > 0)
            {
                //repository.ChangePersonRelative(user, personrelative);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Роль изменена");
            }
            else if (role.Id < 0)
            {
                //repository.DeletePersonRelative(user, personrelative);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Роль удалена");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}