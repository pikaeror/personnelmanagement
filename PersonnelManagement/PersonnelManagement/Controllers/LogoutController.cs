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
    //[Produces("application/json")]
    [Route("api/Logout")]
    public class LogoutController : Controller
    {
        private Repository repository;
        private IdentityService identityService;

        public LogoutController(Repository repository, IdentityService identityService)
        {
            this.repository = repository;
            this.identityService = identityService;
        }

        [HttpGet()]
        public void Logout()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            Response.Cookies.Delete(Keys.COOKIES_SESSION);
            if (sessionid != null)
            {
                repository.GetContextUser().RemoveSession(sessionid);
            }
        }

    }
}