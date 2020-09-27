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
    [Route("api/Persondecreeblock")]
    public class PersondecreeblockController : Controller
    {

        private Repository repository;


        public PersondecreeblockController(Repository repository)
        {
            this.repository = repository;

        }

        // Можно ли редактировать блоки
        // GET: api/Persondecreeblock
        [HttpGet]
        public bool IsAllowedToEditDecreeBlocks()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                return true;
            }
            return false;
        }

        // GET: api/Persondecreeblock/5
        [HttpGet("{id}")]
        public IEnumerable<PersondecreeblockManagement> GetDecreeBlocks([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                //bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                return repository.GetPersondecreeblock(user, id, true);
            }
            List<PersondecreeblockManagement> empty = new List<PersondecreeblockManagement>();
            return empty;
        }

        // POST: api/Persondecreeblock
        [HttpPost]
        public IActionResult PostDecree([FromBody] PersondecreeblockManagement persondecreeblock)
        {
            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanEditPerson(user, repository);
                //bool hasAccess = IdentityService.canEditStructures(sessionid, repository);
                if (!hasAccess)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");

                }
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }

            if (persondecreeblock == null)
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Найдены ошибки в форме");
            }
            // Добавляем новый элемент в проект приказа
            if (persondecreeblock.Status == 1)
            {
                repository.AddPersonDecreeblock(user, persondecreeblock);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Добавлено!");

            } else if (persondecreeblock.Status == 2) // Удаляем блок
            {
                repository.RemovePersonDecreeblock(user, persondecreeblock);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Удалено!");

            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }
    }
}