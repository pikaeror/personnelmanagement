using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using PersonnelManagement.Udostoverenia;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Certificate")]
    public class CertificateController : Controller
    {

        private Repository repository;

        public CertificateController(Repository repository)
        {
            this.repository = repository;
        }
    }
}