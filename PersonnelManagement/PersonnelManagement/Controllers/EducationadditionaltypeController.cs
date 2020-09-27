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
    [Route("api/Educationadditionaltype")]
    public class EducationadditionaltypeController : Controller
    {

        private Repository repository;
        public EducationadditionaltypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Educationadditionaltype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Educationadditionaltype> educationadditionaltypes = repository.Educationadditionaltypes;
                    return educationadditionaltypes;
                }
            }
            List<Educationadditionaltype> empty = new List<Educationadditionaltype>();
            return empty;
        }

    }
}