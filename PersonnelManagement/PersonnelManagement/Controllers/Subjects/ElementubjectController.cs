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
    [Produces("Elementsubject/json")]
    [Route("api/Elementsubject")]
    public class ElementsubjectController : Controller
    {
        private Repository repository;

        public ElementsubjectController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public Elementsubject GetData([FromRoute] int elementSubject)
        {
            SubjectWorker subjectWorker = new SubjectWorker(repository);
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return subjectWorker.GetSubjectElement(elementSubject);
                }
            }
            return null;
        }
    }
}