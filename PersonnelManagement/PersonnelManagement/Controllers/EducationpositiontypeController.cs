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
    [Route("api/Educationpositiontype")]
    public class EducationpositiontypeController : Controller
    {

        private Repository repository;

        public EducationpositiontypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Educationpositiontype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Educationpositiontype> educationpositiontypes = repository.Educationpositiontypes;
                    return educationpositiontypes;
                }
            }
            List<Educationpositiontype> empty = new List<Educationpositiontype>();
            return empty;
        }

    }
}