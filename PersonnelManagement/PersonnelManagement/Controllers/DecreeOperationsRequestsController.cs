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
    [Route("api/DecreeOperationsRequests")]
    public class DecreeOperationsRequestsController : Controller
    {

        private Repository repository;

        public DecreeOperationsRequestsController(Repository repository)
        {
            this.repository = repository;
        }

        // Is allowed to edit structures.
        // GET: api/DecreeOperationsRequests
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

        // GET: api/DecreeOperationsRequests/5
        [HttpGet("{id}")]
        public IEnumerable<DecreeoperationManagement> GetDecreeOperations([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                return repository.GetDecreeoperationManagement(id);
            }
            List<DecreeoperationManagement> empty = new List<DecreeoperationManagement>();
            return empty;
        }

        // POST: api/DecreeOperationsRequests
        [HttpPost]
        public IActionResult PostDecree([FromBody] List<DecreeOperationsRequest> decreeOperationsRequests)
        {
            if (decreeOperationsRequests == null)
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Оказия! Нулевые запросы");
            }
            int length = decreeOperationsRequests.Count; 
            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (!hasAccess)
                {
                    return new ObjectResult(new List<DecreeoperationManagement>()); // DENY
                }
            }
            else
            {
                return new ObjectResult(new List<DecreeoperationManagement>()); // DeENY
            }

            List<DecreeoperationManagement> decreeoperations = new List<DecreeoperationManagement>();
            foreach (DecreeOperationsRequest decreeOperationsRequest in decreeOperationsRequests)
            {
                decreeoperations.AddRange(repository.RequestDecreeOperations(decreeOperationsRequest));
            }
            return new ObjectResult(decreeoperations);

        }

    }
}