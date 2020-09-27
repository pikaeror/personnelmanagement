using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ChangesByDecreeOutputWord : ControllerBase
    {
        private Repository m_repository;
        private OrderListOfChanges m_list_of_changes_Worker;

        public ChangesByDecreeOutputWord(Repository repository)
        {
            m_repository = repository;
            m_list_of_changes_Worker = new OrderListOfChanges(m_repository);
        }

        [HttpPost("Decree/ListOfChanges")]
        //public IActionResult EditDocumentByDecreeListOfChanges([FromBody] IEnumerable<Decreeoperation> input)
        public IActionResult EditDocumentByDecreeListOfChanges([FromBody] IEnumerable<Decreeoperation> input)
        {
            
            m_list_of_changes_Worker.InitializeChangesList(input, Request.Cookies[Keys.COOKIES_SESSION]);

            return File(m_list_of_changes_Worker.getStream(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "test.docx");
        }

        [HttpGet("Decree/ListOfChanges/{input_decree_id}")]
        //public IActionResult EditDocumentByDecreeListOfChanges([FromBody] IEnumerable<Decreeoperation> input)
        public IActionResult EditDocumentByDecreeListOfChanges([FromRoute] int input_decree_id)
        {

            m_list_of_changes_Worker.InitializeChangesList(m_repository.DecreesLocal().Values.Where(s => s.Id == input_decree_id).ToList()[0], Request.Cookies[Keys.COOKIES_SESSION]);

            return File(m_list_of_changes_Worker.getStream(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "test.docx");
        }
    }
}
