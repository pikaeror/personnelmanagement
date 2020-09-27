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
    [Route("api/Structureregion")]
    public class StructureregionController : Controller
    {

        private Repository repository;


        public StructureregionController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Structureregion> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Structureregions;
                }
            }
            List<Structureregion> empty = new List<Structureregion>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateStructureregion([FromBody]Structureregion newStructureregion)
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
             * Means, we add new structure region.
             */
            if (newStructureregion.Id == 0)
            {
                repository.AddStructureregion(newStructureregion);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Область успешно добавлена");
            }
            else if (newStructureregion.Id != 0)
            {
                repository.UpdateStructureregion(newStructureregion);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Область успешно обновлена");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }
    }
}