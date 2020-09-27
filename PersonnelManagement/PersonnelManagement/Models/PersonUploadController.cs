using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;

namespace PersonnelManagement.Models
{
    [Produces("application/json")]
    [Route("api/PersonUpload")]
    public class PersonUploadController : Controller
    {

        private Repository repository;

        public PersonUploadController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public async Task<IActionResult> UploadFile([FromForm]IFormFile file)
        {
            //return RedirectToAction
            return Ok(new { });
            //return new ObjectResult(Keys.SUCCESS_SHORT + ":Успешно загружено!");
        }
    }
}