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
    [Route("api/Subjectcategory")]
    public class SubjectcategoryController : Controller
    {

        private Repository repository;


        public SubjectcategoryController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Subjectcategory> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Subjectcategory> subjectcategories = repository.Subjectcategories;
                    return subjectcategories;
                }
            }
            List<Subjectcategory> empty = new List<Subjectcategory>();
            return empty;
        }


    }
}