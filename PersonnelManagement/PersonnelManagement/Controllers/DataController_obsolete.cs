//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
//using PersonnelManagement.Models;
//using PersonnelManagement.Services;

//namespace PersonnelManagement.Controllers
//{
//    //[Produces("application/json")]
//    [Route("api/Data")]
//    public class DataController : Controller
//    {
//        private Repository repository;
        

//        public DataController(Repository repository)
//        {
//            this.repository = repository;
            
//        }

//        [HttpGet()]
//        public IEnumerable<Person> GetData()
//        {
//            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
//            if (IdentityService.IsLogined(sessionid, repository)){
//                User user = IdentityService.GetUserBySessionID(sessionid, repository);
//                bool hasAccess = IdentityService.CanReadCommonData(user);
//                if (hasAccess)
//                {
//                    return repository.Persons;
//                }
//            }
//            List<Person> empty = new List<Person>();
//            return empty;
//        }

//        [HttpPost()]
//        public void AddPerson([FromBody]Person person)
//        {
//            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
//            if (IdentityService.IsLogined(sessionid, repository))
//            {
//                User user = IdentityService.GetUserBySessionID(sessionid, repository);
//                bool hasAccess = IdentityService.CanWriteCommonData(user);
//                if (hasAccess)
//                {
//                    repository.SavePerson(person);
//                }
//            }
            
//        }
//    }

//}