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
    [Route("api/DecreeOperations")]
    public class DecreeOperationsController : Controller
    {

        private Repository repository;

        public DecreeOperationsController(Repository repository)
        {
            this.repository = repository;
        }

        // Is allowed to edit structures.
        // GET: api/DecreeOperations
        [HttpGet]
        public bool IsAllowedToEditDecreeOperations()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                return true;
            }
            return false;
        }

        // GET: api/DecreeOperations/5
        [HttpGet("{id}")]
        public IEnumerable<DecreeoperationManagement> GetDecreeOperations([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                return repository.GetDecreeoperationManagement(id, true);
            }
            List<DecreeoperationManagement> empty = new List<DecreeoperationManagement>();
            return empty;
        }

        // POST: api/DecreeOperations
        [HttpPost]
        public IActionResult PostDecree([FromBody] DecreeoperationManagement decreeoperationManagement)
        {
            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.canEditStructures(sessionid, repository);
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
             * Means, we remove decree operation with its subject
             */
            if (decreeoperationManagement.MetaStatus == Keys.DECREE_OPERATION_META_REMOVE)
            {
                repository.RemoveDecreeOperationWithItsSubject(decreeoperationManagement);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Удалено из приказа");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}