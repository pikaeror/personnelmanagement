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
    [Route("api/Externalorderwhotype")]
    public class ExternalorderwhotypeController : Controller
    {

        private Repository repository;

        public ExternalorderwhotypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Externalorderwhotype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Externalorderwhotype> externalorderwhotypes = repository.Externalorderwhotypes;
                    return externalorderwhotypes;
                }
            }
            List<Externalorderwhotype> empty = new List<Externalorderwhotype>();
            return empty;
        }

    }
}