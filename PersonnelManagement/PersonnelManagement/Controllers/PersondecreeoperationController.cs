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
    [Route("api/Persondecreeoperation")]
    public class PersondecreeoperationController : Controller
    {

        private Repository repository;

        public PersondecreeoperationController(Repository repository)
        {
            this.repository = repository;
        }

        // Is allowed to edit structures.
        // GET: api/Persondecreeoperation
        [HttpGet]
        public bool IsAllowedToEditDecreeOperations()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                return true;
            }
            return false;
        }

        // GET: api/Persondecreeoperation/5
        [HttpGet("{id}")]
        public IEnumerable<PersondecreeoperationManagement> GetDecreeOperations([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                //bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                return repository.GetPersondecreeoperation(user, id, true);
            }
            List<PersondecreeoperationManagement> empty = new List<PersondecreeoperationManagement>();
            return empty;
        }

        // GET: api/Persondecreeoperation/Upriority5
        [HttpGet("Upriority{id}")]
        public IActionResult Uppriorityintro([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    repository.UpStructure(id, user);
                    return new ObjectResult(Keys.SUCCESS_SHORT + ":Выполнено");
                }

                return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }


        // POST: api/Persondecreeoperation
        [HttpPost]
        public IActionResult PostDecree([FromBody] PersondecreeoperationManagement persondecreeoperation)
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

            if (persondecreeoperation.Status == 1)
            {
                persondecreeoperation.personFromStructure.ForEach(element =>
                {
                    persondecreeoperation.Person = element.Id;
                    repository.AddPersonDecreeoperation(user, persondecreeoperation);
                });
                persondecreeoperation.candidateSearch.ForEach(element =>
                {
                    persondecreeoperation.Person = -element.Id;
                    repository.AddPersonDecreeoperation(user, persondecreeoperation);
                });
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Добавлено!");
            }
            else if (persondecreeoperation.Status == 2)
            {
                repository.RemovePersonDecreeoperation(user, persondecreeoperation);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Удалено!");
            }
            else if (persondecreeoperation.Status == 3)
            {
                repository.UpdatePersonDecreeoperation(user, persondecreeoperation);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Обновлено!");
            }
            ///**
            // * Means, we remove decree operation with its subject
            // 
            //if (decreeoperationManagement.MetaStatus == Keys.DECREE_OPERATION_META_REMOVE)
            //{
            //    repository.RemoveDecreeOperationWithItsSubject(decreeoperationManagement);
            //    return new ObjectResult(Keys.SUCCESS_SHORT + ":Удалено из приказа");
            //}
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        /*// POST: api/Persondecreeoperation
        [HttpPost("/updatelist")]
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
            *//*repository.UpdatePersonDecreeoperation(user, persondecreeoperation);*//*
            return new ObjectResult(Keys.SUCCESS_SHORT + ":Обновлено!");
        }*/
    }
}