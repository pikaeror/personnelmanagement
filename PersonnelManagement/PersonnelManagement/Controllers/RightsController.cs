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
    [Route("api/Rights")]
    public class RightsController : Controller
    {

        private Repository repository;

        public RightsController(Repository repository)
        {
            this.repository = repository;
        }

        // Получаем права доступа по умолчанию, которые привязаны к должности. Если нет привязанных, возвращаем новый пустой объект
        // GET: api/Rights/Position/5
        [HttpGet("Position/{id}")]
        public Rights GetPositiontypeRights([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToRead = IdentityService.CanReadCommonData(user);
                if (isAllowedToRead)
                {
                    Rights rights = repository.RightsLocal().Values.FirstOrDefault(r => r.Position == id);
                    if (rights == null)
                    {
                        return new Rights();
                    }
                    return rights;
                }

                return new Rights();
            }
            return new Rights();
        }

        [HttpPost()]
        public IActionResult PostRights([FromBody]Rights rights)
        {

            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanEditPerson(user, repository);
                if (!hasAccess)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");

                }
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }
            if (rights.Id == 0)
            {
                repository.AddRights(user, rights);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Права добавлены");
            }
            else if (rights.Id > 0)
            {
                repository.ChangeRights(user, rights);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Права изменены");
            }
            else if (rights.Id < 0)
            {
                repository.DeleteRights(user, rights);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Права удалены");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}