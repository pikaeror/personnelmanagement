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
    [Route("api/Educationstage")]
    public class EducationstageController : Controller
    {

        private Repository repository;

        public EducationstageController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Educationstage> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Educationstage> educationstages = repository.Educationstages;
                    return educationstages;
                }
            }
            List<Educationstage> empty = new List<Educationstage>();
            return empty;
        }

    }
}