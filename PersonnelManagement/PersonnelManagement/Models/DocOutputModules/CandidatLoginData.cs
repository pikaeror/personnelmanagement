using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;

namespace PersonnelManagement.Models
{
    public class CandidatLoginData : DocTemplate
    {
        public static void CreateDocument(MemoryStream mem, Repository repository, CabinetdataManager data)
        {
            using (var document = WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document))
            {
                document.AddMainDocumentPart();
                document.MainDocumentPart.Document = new Document();

                var body = new Body();
                SetMargins(body);
                addFirstHeader(body, data);
                document.MainDocumentPart.Document.AppendChild(body);
            }
        }

        private static void addFirstHeader(Body body, CabinetdataManager data)
        {
            addSecondParagraffs(body);

            write_Text(body: body, line: "Фамилия:\t" + data.Usersurname);
            write_Text(body: body, line: "Имя:\t" + data.Username);
            write_Text(body: body, line: "Отчество:\t" + data.Userpatronymic);
            write_Text(body: body, line: "Идентификационный номер:\t" + data.Userind);
            write_Text(body: body, line: "Пароль входа:\t" + data.Accesscode);

            //createSignature(body);
            //appendDecreeHistory(body: body, name: "");
        }

        private static void addSecondParagraffs(Body body)
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center },
                                                                              /*new Indentation() { Left = "300" },*/
                                                                              new ContextualSpacing() { Val = false },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });

            Run run = new Run();
            Text text = new Text();
            text.Text = "URL: http://172.26.200.47";

            run.Append(text);
            AppendFontDefault(run);
            AppendFontSize(run, FontSize);
            Paragraph paragraph = new Paragraph();
            paragraph.AppendChild(paragraphProperties);
            paragraph.Append(run);
            body.AppendChild(paragraph);
        }


    }
}
