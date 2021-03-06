﻿using System;
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
        [HttpGet("structureslist")]
        public List<FeaturedStructure> getStructuresForUser()
        {
            List<FeaturedStructure> output = new List<FeaturedStructure>();
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                return repository.getStructuresForUserExcert(user).ToList();
            }
            return output;
        }


        [HttpGet("excertdecree")]
        public List<PersondecreeManagement> getDecreeList()
        {
            List<PersondecreeManagement> output = new List<PersondecreeManagement>();
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

        [HttpGet("listexcertsstructuretype/{id}")]
        public List<ExcertStructure> getExcertExcertStructures([FromRoute] int id)
        {
            List<ExcertStructure> output = new List<ExcertStructure>();
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                return repository.getExcertExcertStructures(id, user).ToList();
            }
            return output;
        }

        [HttpGet("excert/{ides}")]
        public ExcertComposition getExcertFullDecree([FromRoute] string ides)
        {
            ExcertComposition output = new ExcertComposition();
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                return repository.getEcxertFullDecree(ides, user);
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

        [HttpPost("updateexcert")]
        public IActionResult UpdateExcertOpens([FromBody] ExcertStructure structure)
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
            repository.UpdateExcert(structure, user);
            return new ObjectResult(Keys.SUCCESS_SHORT + ":Обновлено!");
        }
    }
}
