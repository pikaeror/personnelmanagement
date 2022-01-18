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
    [Route("api/request")]
    public class RequestController : Controller
    {
        private Repository repository;

        public RequestController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet("educationdata")]
        public Educationdata GetEducationdata()
        {
            Educationdata educationdata = new Educationdata();
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = IdentityService.GetUserBySessionID(sessionid, repository);

            if (IdentityService.IsLogined(sessionid, repository))
            {
                educationdata.all_levels = repository.Get_all_levels();
                educationdata.all_cvalifications = repository.Get_all_cvalifications(user);
                educationdata.all_specializations = repository.Get_all_specializations(user);
            }
            return educationdata;
        }

        [HttpGet("structureTree")]
        public StructureTree GetStructureTree()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = IdentityService.GetUserBySessionID(sessionid, repository);

            if (IdentityService.IsLogined(sessionid, repository))
            {
                StructureTree structureTree = repository.GetStructureTree(user.Structure.Value, user.Date.GetValueOrDefault());
                return structureTree;
            }

            return null;
        }

        [HttpPost("PersonEducationRequest")]
        public IEnumerable<Education_Respons> GetEducationRespons([FromBody]Education_Request education_Request)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = IdentityService.GetUserBySessionID(sessionid, repository);

            ELDRequestWorker worker = new ELDRequestWorker(repository, user);

            if (IdentityService.IsLogined(sessionid, repository))
            {
                return worker.GetEducation(education_Request);
            }
            return null;
        }

        [HttpPost("PersonRankRequest")]
        public IEnumerable<Rank_respons> GetRank_Respons([FromBody]Rank_request rank_Request)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = IdentityService.GetUserBySessionID(sessionid, repository);

            ELDRequestWorker worker = new ELDRequestWorker(repository, user);

            if (IdentityService.IsLogined(sessionid, repository))
            {
                return worker.GetRank(rank_Request);
            }
            return null;
        }
    }
}
