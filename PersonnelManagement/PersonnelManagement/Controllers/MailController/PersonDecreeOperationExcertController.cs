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
    [Route("api/Persondecreeoperationexcert")]
    public class PersonDecreeOperationExcertController : Controller
    {
        private Repository repository;

        public PersonDecreeOperationExcertController(Repository repository)
        {
            this.repository = repository;
        }

        /*[HttpGet]
        public string utu()
        {
            return "haha";
        }*/

        [HttpGet("excertdecree")]
        public List<Persondecree> getDecreeList()
        {
            List<Persondecree> output = new List<Persondecree>();
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                return repository.getExcertDecree(user).ToList();
            }
            return output;
        }

        [HttpGet("listexcertsstructure/{id}")]
        public List<Structure> getExcertStructures([FromRoute] int id)
        {
            List<Structure> output = new List<Structure>();
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                return repository.getExcertStructures(id, user).ToList();
            }
            return output;
        }

        // POST: api/Persondecreeoperation
        [HttpPost]
        public IActionResult UpdateOperations([FromBody] IEnumerable<PersondecreeoperationManagement> persondecreeoperations)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanEditPerson(user, repository);
                if (!hasAccess)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
                }
            }
            repository.UpdatePersonDecreeoperation(persondecreeoperations, user);
            return new ObjectResult(Keys.SUCCESS_SHORT + ":Обновлено!");
        }
    }
}
