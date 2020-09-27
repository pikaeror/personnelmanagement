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
    [Route("api/Persondecreetype")]
    public class PersondecreetypeController : Controller
    {

        private Repository repository;

        public PersondecreetypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Persondecreetype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Persondecreetype> persondecreetypes = repository.Persondecreetypes;
                    return persondecreetypes;
                }
            }
            List<Persondecreetype> empty = new List<Persondecreetype>();
            return empty;
        }

    }
}