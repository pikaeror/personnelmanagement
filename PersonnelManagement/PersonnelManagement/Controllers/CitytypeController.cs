using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using PersonnelManagement.USERS;


namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Citytype")]
    public class CitytypeController : Controller
    {

        private Repository repository;


        public CitytypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Citytype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Citytype> citytypes = repository.Citytypes;
                    return citytypes;
                }
            }
            List<Citytype> empty = new List<Citytype>();
            return empty;
        }

    }
}