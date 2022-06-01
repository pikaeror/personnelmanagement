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
    [Route("api/Setpersondatatype")]
    public class SetpersondatatypeController : Controller
    {

        private Repository repository;


        public SetpersondatatypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Setpersondatatype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    IEnumerable<Setpersondatatype> setpersondatatypes = repository.Setpersondatatypes;
                    return setpersondatatypes;
                }
            }
            List<Setpersondatatype> empty = new List<Setpersondatatype>();
            return empty;
        }

    }
}