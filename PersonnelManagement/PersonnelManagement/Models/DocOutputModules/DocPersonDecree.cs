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

        private void addCenterParagraffs(Body body, string write = "test line output")
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Indentation() { Left = "0" },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new Justification { Val = JustificationValues.Center },
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

        private void generateencourage(Body body,
            Persondecreeblock decree_block)
        {
            if (decree_block.Persondecreeblocktype != 1)
                return;
            addFirstParagraffs(body, decree_block.Index.ToString() +
                ". " +
                m_repository.Persondecreeblocktypes.First(r => r.Id == decree_block.Persondecreeblocktype).Name.ToString().ToUpper() +
                ":");

            List<Persondecreeblockintro> block_intros = m_repository.Persondecreeblockintros.Where(r => r.Persondecreeblock == decree_block.Id).ToList();
            block_intros.Sort((a, b) => a.Index.CompareTo(b.Index));

            foreach(Persondecreeblockintro block_intro in block_intros)
            {
                generateencourage(body, decree_block, block_intro);
            }
        }

        private void generateencourage(Body body,
            Persondecreeblock decree_block,
            Persondecreeblockintro decree_intro)
        {
            if (decree_intro == null)
                return;
            addFirstParagraffs(body, decree_block.Index.ToString() +
                "." +
                decree_intro.Index.ToString() +
                ". " +
                decree_intro.Name.ToString());

            List<Persondecreeblocksub> block_subs = m_repository.Persondecreeblocksubs.Where(r => r.Persondecreeblockintro == decree_intro.Id).ToList();
            block_subs.Sort((a, b) => a.Index.CompareTo(b.Index));

            foreach (Persondecreeblocksub decree_sub in block_subs)
            {
                generateencourage(body,
                    decree_block,
                    decree_intro,
                    decree_sub);
            }
        }

        private void generateencourage(Body body,
            Persondecreeblock decree_block,
            Persondecreeblockintro decree_intro,
            Persondecreeblocksub decree_sub)
        {
            if (decree_sub == null)
                return;
            Persondecreeblocksubtype type = m_repository.Persondecreeblocksubtypes.First(r => r.Id == decree_sub.Persondecreeblocksubtype);
            string printing_string = "";
            if(type != null)
                printing_string += " " + type.Name;
            if((decree_sub.Subvaluenumber1 != 1 && decree_sub.Subvaluenumber1 != 2) || decree_sub.Subvaluestring1 != "1")
                printing_string += " " + decree_sub.Subvaluestring1.Replace("\n", "");
            if(decree_sub.Persondecreeblocksubtype == 7)
                printing_string += " " + (m_repository.Rewardmoneys.First(r => r.Id == decree_sub.Subvaluenumber1) != null ?
                    (Int32.Parse(decree_sub.Subvaluestring1) > 1 ?
                    m_repository.Rewardmoneys.First(r => r.Id == decree_sub.Subvaluenumber1).Rewardmoneytypeplural :
                    m_repository.Rewardmoneys.First(r => r.Id == decree_sub.Subvaluenumber1).Rewardmoneytype) : "") +
                    (decree_sub.Subvaluenumber2 > 0 ? " каждого" : "");
            if(decree_sub.Persondecreeblocksubtype == 4 || decree_sub.Persondecreeblocksubtype == 3)
                printing_string += " " + (m_repository.Rewardtypes.First(r => r.Id == decree_sub.Subvaluenumber1).Name.ToString().Replace("«", "").Replace("»", ""));
            if (decree_sub.Persondecreeblocksubtype == 5 || decree_sub.Persondecreeblocksubtype == 6)
            {
                Structure actual_structure = decree_sub.Subvaluenumber1 == 0 ? null : m_repository.StructuresLocal().Values.First(r => r.Id == decree_sub.Subvaluenumber1);
                printing_string += actual_structure != null ? (" " + actual_structure.Name2) : "";
            }
            if (decree_sub.Subvaluenumber1 != null && decree_sub.Subvaluenumber1 > 0 && (decree_sub.Persondecreeblocksubtype == 1 || decree_sub.Persondecreeblocksubtype == 2))
                printing_string += " «" + m_repository.Ranks.First(r => r.Id == decree_sub.Subvaluenumber1) + "»";

            addCenterParagraffs(body, printing_string);
            List<Persondecreeoperation> decree_operations = m_repository.Persondecreeoperations.Where(r => r.Persondecreeblock == decree_block.Id).ToList();
            decree_operations.Sort((a, b) => a.Index.CompareTo(b.Index));

            foreach(Persondecreeoperation decree_operation in decree_operations)
            {
                generateencourage(body,
                    decree_block,
                    decree_intro,
                    decree_sub,
                    decree_operation);
            }
        }

        private void generateencourage(Body body,
            Persondecreeblock decree_block,
            Persondecreeblockintro decree_intro,
            Persondecreeblocksub decree_sub,
            Persondecreeoperation decree_operation)
        {
            if (decree_operation == null)
                return;
            string printing_string = "";
            Personpenalty penalty = m_repository.Personpenalties.First(r => r.Person == decree_operation.Person);
            if (decree_operation.Persondecreeblocksubtype == 8 && penalty != null)
                printing_string += "«" + m_repository.Penalties.First(r => r.Id == penalty.Penalty).Name +
                    "», объявленное приказом " + penalty.Orderwho + " от " +
                    penalty.Orderdate.ToString("dd.MM.yyyy") + " " +
                    penalty.Ordernumber + "c";
            Person person = m_repository.Persons.First(r => r.Id == decree_operation.Person);
            Personrank rank = m_repository.Personranks.First(r => r.Person == person.Id);
            if (((decree_operation.Persondecreeblocksubtype > 2 && decree_operation.Persondecreeblocksubtype <= 8) ||
                decree_operation.Persondecreeblocksubtype == 10) && person != null)
                printing_string += " " + (rank != null ? (m_repository.Ranks.First(r => r.Id == rank.Rank).Name4 + " " +
                    person.Surname4 + " " +
                    person.Name4 + " " +
                    person.Fathername4 + " " +
                    m_repository.Positions.First(r => r.Id == person.Position).Name4) : (" " + decree_operation.Nonperson);

            addFirstParagraffs(body, printing_string);
            //List<Persondecreeoperation> block_oper = m_repository.Persondecreeoperations.Where(r => r.Persondecreeblock == decree_block.Id).ToList();
            //block_oper.Sort((a, b) => a.Index.CompareTo(b.Index));

        }
    }
}
