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
    [Route("api/Persondecreelevel")]
    public class PersondecreelevelController : Controller
    {

        private Repository repository;


        public PersondecreelevelController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Persondecreelevel> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Persondecreelevel> persondecreelevels = repository.Persondecreelevels;
                    return persondecreelevels;
                }
            }
            List<Persondecreelevel> empty = new List<Persondecreelevel>();
            return empty;
        }

    }
}