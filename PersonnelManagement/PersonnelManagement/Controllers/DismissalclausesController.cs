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
    [Route("api/Dismissalclauses")]
    public class DismissalclausesController : ControllerBase
    {
        private Repository repository;

        public DismissalclausesController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet()]
        public IEnumerable<Dismissalclauses> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Dismissalclauses> areas = repository.Dismissalclauses;
                    return areas;
                }
            }
            List<Dismissalclauses> empty = new List<Dismissalclauses>();
            return empty;
        }
    }
}
