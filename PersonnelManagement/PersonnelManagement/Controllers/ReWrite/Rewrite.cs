using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using PersonnelManagement.USERS;
using System.Threading;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/ReWrite")]
    public class Rewrite : Controller
    {
        private Repository repository;
        private DecreeWorker decreeWorker;
        private Models.ReWrite reWrite;

        public Rewrite(Repository repository)
        {
            this.repository = repository;
            this.decreeWorker = new DecreeWorker(ref repository);
            this.reWrite = new Models.ReWrite(ref repository);
        }

        [HttpPost()]
        public void worker([FromBody] USERS.User user)
        {
            reWrite.user = user;
            Thread tr_p = new Thread(reWrite.Worker);
            tr_p.Priority = ThreadPriority.Highest;
            tr_p.Start();
            tr_p.Join();

            DecreeWorker decreeWorker = new DecreeWorker(ref repository);
            decreeWorker.reWriteDecreesNull(user, true);
        }
    }
}
