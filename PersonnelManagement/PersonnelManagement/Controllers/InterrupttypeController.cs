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
    [Route("api/Interrupttype")]
    public class InterrupttypeController : Controller
    {

        private Repository repository;


        public InterrupttypeController(Repository repository)
        {
            this.repository = repository;

        }
    }
}