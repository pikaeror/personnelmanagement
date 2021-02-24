using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace PersonnelManagement.Models
{
    public class DocPersonDecree : DocTemplate
    {
        Repository m_repository;
        User m_user;

        MemoryStream m_stream;

        public DocPersonDecree(Repository repository, User user)
        {
            m_repository = repository;
            m_user = user;

            m_stream = new MemoryStream();
            //CreateDocument(m_stream);
        }

        public MemoryStream GetMemoryStream()
        {
            m_stream.Position = 0;
            return m_stream;
        }

        protected static void SetMargins(Body body)
        {
            var sectionProps = new SectionProperties();
            var pageMargin = new PageMargin
            {
                Top = FromMilimeters(150),
                Left = FromMilimeters(225),
                Right = FromMilimeters(80),
                Bottom = FromMilimeters(150)
            };
            sectionProps.AppendChild(pageMargin);
            body.AppendChild(sectionProps);
        }

        public void CreateDocument(MemoryStream mem)
        {
            using (var document = WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document))
            {
                document.AddMainDocumentPart();
                document.MainDocumentPart.Document = new Document();
                var body = new Body();
                SetMargins(body);
                addFirstParagraffs(body);
                document.MainDocumentPart.Document.AppendChild(body);
            }
        }

        public void Worker(Persondecree decree)
        {
            List<Persondecreeblock> blocks = loadDecreeBlocks(decree.Id);
            blocks.Sort((a, b) => a.Index.CompareTo(b.Index));

            using (var document = WordprocessingDocument.Create(m_stream, WordprocessingDocumentType.Document))
            {
                document.AddMainDocumentPart();
                document.MainDocumentPart.Document = new Document();
                var body = new Body();
                SetMargins(body);
                foreach(Persondecreeblock k in blocks)
                {
                    generateencourage(body, k);
                }
                addFirstParagraffs(body);
                document.MainDocumentPart.Document.AppendChild(body);
            }
        }

        private void addFirstParagraffs(Body body, string write = "test line output")
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Indentation() { Left = "0" },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });
            RunProperties rPr = new RunProperties(
                new RunFonts()
                {
                    Ascii = FontFamily
                });
            Run run = new Run();
            /*if (new_page > 0)
                rPr.AppendChild(new Break() { Type = BreakValues.Page });*/
            run.PrependChild<RunProperties>(rPr);
            Text text = new Text();
            text.Text = write;

            run.Append(text);
            AppendFontDefault(run);
            AppendFontSize(run, FontSize);
            Paragraph paragraph = new Paragraph(paragraphProperties, run);
            body.AppendChild(paragraph);
        }

        private Persondecree loadDecree(int id_decree)
        {
            return m_repository.PersondecreesLocal().Values.FirstOrDefault(r => r.Id == id_decree);
        }

        private List<Persondecreeblock> loadDecreeBlocks(int id_decree)
        {
            return m_repository.Persondecreeblocks.Where(r => r.Persondecree == id_decree).ToList();
        }

        private List<Persondecreeoperation> loadDecreeOperations(Persondecree decree)
        {
            List<Persondecreeoperation> output = m_repository.PersondecreeoperationsLocal().Values.Where(r => r.Persondecree == decree.Id).ToList();
            output.Sort((x, y) => x.Index.CompareTo(y.Index));
            return output;
        }

        private void generateencourage(Body body, Persondecreeblock input_list)
        {
            if (input_list.Persondecreeblocktype != 1)
                return;
            addFirstParagraffs(body, input_list.Index.ToString() +
                ". " +
                m_repository.Persondecreeblocktypes.First(r => r.Id == input_list.Persondecreeblocktype).Name.ToString().ToUpper() +
                ":");


        }
    }
}
