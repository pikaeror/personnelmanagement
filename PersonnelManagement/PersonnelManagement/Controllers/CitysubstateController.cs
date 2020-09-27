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
    [Route("api/Citysubstate")]
    public class CitysubstateController : Controller
    {

        private Repository repository;

        public CitysubstateController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Citysubstate> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Citysubstate> citysubstates = repository.Citysubstates;
                    return citysubstates;
                }
            }
            List<Citysubstate> empty = new List<Citysubstate>();
            return empty;
        }

    }
}