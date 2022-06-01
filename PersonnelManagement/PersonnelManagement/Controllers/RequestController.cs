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
    [Route("api/request")]
    public class RequestController : Controller
    {
        private Repository repository;

        public RequestController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet("structureTree")]
        public StructureTree GetStructureTree()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = IdentityService.GetUserBySessionID(sessionid, repository);

            if (IdentityService.IsLogined(sessionid, repository))
            {
                StructureTree structureTree = repository.GetStructureTree(user.Structure.Value, user.Date.GetValueOrDefault());
                return structureTree;
            }

            return null;
        }
    }
}
