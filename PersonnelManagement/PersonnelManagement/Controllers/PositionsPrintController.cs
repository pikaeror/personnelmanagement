using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using PersonnelManagement.Models;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/PositionsPrint")]
    public class PositionsPrintController : Controller
    {

        [HttpPost]
        public IActionResult GetDocumentBase([FromBody]PositionManagement positionManagement)
        {
            var mem = new MemoryStream();
            // Create Document
            using (WordprocessingDocument wordDocument =
                WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document, true))
            {
                // Add a main document part. 
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                // Create the document structure and add some text.
                mainPart.Document = new Document();
                Body docBody = new Body();
                mainPart.Document.Append(docBody);

                if (positionManagement.Type.Equals(Keys.POSITION_MANAGEMENT_ADDNEWPOSITION))
                {
                    // Add your docx content here
                    Paragraph p = new Paragraph();
                    Run r = new Run();
                    Text t = new Text("Название новой должности - " + positionManagement.Name);
                    r.Append(t);
                    p.Append(r);
                    docBody.Append(p);
                }
                
            }

            //mem.Close();
            //return new ObjectResult(":Отказано в доступе");
            // Download File
            mem.Position = 0;
            return File(mem, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "test.docx");

        }

    }
}