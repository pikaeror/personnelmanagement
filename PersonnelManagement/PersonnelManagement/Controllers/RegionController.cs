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
    [Route("api/Region")]
    public class RegionController : Controller
    {

        private Repository repository;


        public RegionController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Region> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Region> regions = repository.Regions;
                    return regions;
                }
            }
            List<Region> empty = new List<Region>();
            return empty;
        }

    }
}