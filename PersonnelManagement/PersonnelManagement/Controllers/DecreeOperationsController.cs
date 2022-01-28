using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using PersonnelManagement.USERS;
using System.Threading;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/DecreeOperations")]
    public class DecreeOperationsController : Controller
    {

        private Repository repository;
        private DecreeOperationWorker decreeWorker;

        public DecreeOperationsController(Repository repository)
        {
            this.repository = repository;
            this.decreeWorker = new DecreeOperationWorker(ref repository);
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

        // GET: api/DecreeOperations/5
        [HttpPost("Finder")]
        public IEnumerable<DecreeoperationManagement> GetDecreeOperation([FromBody] Decree decree)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                decreeWorker.user = user;
                DecreeOperationWorker decreeWorker_s = new DecreeOperationWorker(ref repository);
                decreeWorker_s.user = user;
                Thread tr_p = new Thread(decreeWorker.partial_decreeoperations_pos);
                Thread tr_s = new Thread(decreeWorker_s.partial_decreeoperations_str);
                tr_p.Priority = ThreadPriority.Highest;
                tr_s.Priority = ThreadPriority.Highest;
                tr_p.Start();
                System.Threading.Thread.Sleep(10000);
                tr_s.Start();
                tr_p.Join();
                tr_s.Join();
                /*Thread tr = new Thread(decreeWorker.partial_decreeoperations);
                tr.Priority = ThreadPriority.Highest;
                tr.Start();
                tr.Join();*/
                //decreeWorker.worker_partial(user);
                //bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                //return repository.GetDecreeoperationManagement(decree.Id, true);
            }
            List<DecreeoperationManagement> empty = new List<DecreeoperationManagement>();
            return empty;
        }
    }
}