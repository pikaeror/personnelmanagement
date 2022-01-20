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
    [Produces("application/json")]
    [Route("api/Console")]
    public class ConsoleController : Controller
    {

        private Repository repository;

        public ConsoleController(Repository repository)
        {
            this.repository = repository;
        }


        /**
         * For debugging. Makes decree datetime store in decreeoperation if date is not custom.
         */
        // GET: api/Console/Updatedateactives
        [HttpGet("Updatedateactives")]
        public string Updatedateactives()
        {
            repository.UpdateDatectives();
            return "success";
        }

        /**
         * Дебаг. У всех подразделений без типа, дается тип родителя.
         */
        // GET: api/Console/Updatestructuretypes
        [HttpGet("Updatestructuretypes")]
        public string Updatestructuretypes()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                repository.UpdateStructuretypes(user.Date.GetValueOrDefault());
            }
           
            return "success";
        }

    }
}