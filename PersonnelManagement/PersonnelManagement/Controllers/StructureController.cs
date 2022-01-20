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
    //[Produces("application/json")]
    [Route("api/Structure")]
    public class StructureController : Controller
    {

        private IdentityService identityService;
        private Repository repository;

        public StructureController(IdentityService identityService, Repository repository)
        {
            this.identityService = identityService;
            this.repository = repository;
        }

        [HttpGet()]
        public Dictionary<string, string> GetStructureData()
        {
            Dictionary<string, string> structureData = new Dictionary<string, string>();
            if (IdentityService.IsLogined(Request.Cookies[Keys.COOKIES_SESSION], repository))
            {
                //structureData[Keys.IDENTITY_LOGINED_KEY] = Keys.IDENTITY_LOGINED_TRUE;
                IEnumerable<Structure> structures = repository.StructuresLocal().Values;
                foreach (Structure structure in structures)
                {
                    structureData.Add(structure.Id.ToString(), structure.Name);
                }
            }
            

            return structureData;
        }

        // GET: api/Structure/All
        [HttpGet("All")]
        public IEnumerable<Structure> GetAllStructure()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Structures;
                }
            }
            List<Structure> empty = new List<Structure>();
            return empty;
        }

        // GET: api/Structure/Featured
        [HttpGet("Featured")]
        public List<FeaturedStructure> GetFeaturedStructures()
        {
            List<KeyValuePair<int, FeaturedStructure>> output = new List<KeyValuePair<int, FeaturedStructure>>();
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;

            Dictionary<string, string> structureData = new Dictionary<string, string>();
            if (IdentityService.IsLogined(Request.Cookies[Keys.COOKIES_SESSION], repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                //structureData[Keys.IDENTITY_LOGINED_KEY] = Keys.IDENTITY_LOGINED_TRUE;
                if (repository.StructuresLocal() == null)
                {
                    repository.UpdateStructuresLocal();
                }
                IEnumerable<Structure> structures = repository.FilterDeletedStructures(repository.StructuresLocal().Values, user.Date.GetValueOrDefault());
                foreach (Structure structure in structures)
                {
                    if (structure.Featured.GetValueOrDefault(0) > 0)
                    {
                        FeaturedStructure outputpair = new FeaturedStructure();
                        if (structure.Changeorigin == 0)
                        {
                            outputpair.Id = structure.Id.ToString();
                        } else
                        {
                            outputpair.Id = structure.Changeorigin.ToString();
                        }
                        
                        outputpair.Name = structure.Nameshortened;
                        KeyValuePair<int, FeaturedStructure> pair = new KeyValuePair<int, FeaturedStructure>(structure.Priority, outputpair);
                        output.Add(pair);
                        structureData.Add(structure.Id.ToString(), structure.Nameshortened);
                    }
                }
            }
            output = output.OrderBy(s => s.Key).ToList();
            List<FeaturedStructure> featuredStructures = new List<FeaturedStructure>();
            foreach(KeyValuePair<int, FeaturedStructure> outputElement in output)
            {
                featuredStructures.Add(outputElement.Value);
            }

            return featuredStructures;
            //return structureData;
        }

    }
}