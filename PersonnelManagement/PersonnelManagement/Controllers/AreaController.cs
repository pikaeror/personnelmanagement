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
    [Route("api/Area")]
    public class AreaController : Controller
    {

        private Repository repository;


        public AreaController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Area> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Area> areas = repository.Areas;
                    return areas;
                }
            }
            List<Area> empty = new List<Area>();
            return empty;
        }

    }
}