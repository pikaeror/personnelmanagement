using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using System.IO;
using PersonnelManagement.Utils;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/PrintStaff")]
    public class PrintStaffController : Controller
    {

        private Repository repository;

        DocGenerator m_staff_doc_generator;

        public PrintStaffController(Repository repository)
        {
            this.repository = repository;
            m_staff_doc_generator = new DocGenerator(this.repository);
        }

        // POST: api/PrintStaff
        [HttpPost]
        public IActionResult PostDecree([FromBody] StaffManagement staffManagement)
        {
            m_staff_doc_generator.InitializeStuffManagement(staffManagement, Request.Cookies[Keys.COOKIES_SESSION]);
            return File(m_staff_doc_generator.getStream(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "test.docx");
        }
    }
}