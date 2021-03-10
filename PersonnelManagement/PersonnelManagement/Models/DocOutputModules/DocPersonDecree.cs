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
        Persondecree m_decree;

        MemoryStream m_stream;

        protected const string FontSize = "30";


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
            m_decree = decree;
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
                    generateimposeapenalty(body, k);
                    generatetoappoint(body, k);

                    generaterid(body, k);
                    generateshifting(body, k);
                    generateterminateservice(body, k);
                    generateremove(body, k);
                }
                addFirstParagraffs(body);
                document.MainDocumentPart.Document.AppendChild(body);
            }
        }

        private void addFirstParagraffs(Body body, string write = "test line output", bool second_paragraf = false)
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Indentation() { Left = "0", FirstLine = second_paragraf ? "710" : "0" },
                                                                              new ContextualSpacing() { Val = false },
                                                                              /*new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });*/
                                                                              new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto });
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
                                                                              /*new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });*/
                                                                              new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto });
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
            List<Persondecreeblocksub> block_subs = m_repository.Persondecreeblocksubs.Where(r => r.Persondecreeblockintro == 0 && r.Persondecreeblock == decree_block.Id).ToList();
            block_intros.Sort((a, b) => a.Index.CompareTo(b.Index));

            int count = 1;
            bool block_working = true;
            foreach(Persondecreeblockintro block_intro in block_intros)
            {
                if(count != block_intro.Index)
                {
                    foreach(Persondecreeblocksub decree_sub in block_subs)
                        generateencourage(body, decree_block, new Persondecreeblockintro() { Id = 0 }, decree_sub);
                    count++;
                    block_working = false;
                }
                generateencourage(body, decree_block, block_intro);
                count++;
            }
            if(block_working)
            {
                foreach (Persondecreeblocksub decree_sub in block_subs)
                    generateencourage(body, decree_block, new Persondecreeblockintro() { Id = 0 }, decree_sub);
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
                decree_intro.Name.ToString(),
                true);

            List<Persondecreeblocksub> block_subs = m_repository.Persondecreeblocksubs.Where(r => r.Persondecreeblockintro == decree_intro.Id && r.Persondecreeblock == decree_block.Id).ToList();
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
            string printing_string = decree_intro.Id != 0 ? "" :
                (decree_block.Index.ToString() +
                "." +
                decree_sub.Index.ToString() +
                ". "/* +
                m_repository.Persondecreeblocksubtypes.First(r => r.Id == decree_sub.Persondecreeblocksubtype).Name*/);
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

            if (decree_intro.Id != 0)
                addCenterParagraffs(body, printing_string);
            else
                addFirstParagraffs(body, printing_string, true);
            List<Persondecreeoperation> decree_operations = m_repository.Persondecreeoperations.Where(r => r.Persondecreeblocksub == decree_sub.Id).ToList();
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
            Personpenalty penalty = m_repository.Personpenalties.FirstOrDefault(r => r.Person == decree_operation.Person);
            if (decree_operation.Persondecreeblocksubtype == 8 && penalty != null)
                printing_string += "«" + m_repository.Penalties.First(r => r.Id == penalty.Penalty).Name +
                    "», объявленное приказом " + penalty.Orderwho + " от " +
                    penalty.Orderdate.ToString("dd.MM.yyyy") + " " +
                    penalty.Ordernumber + "c ";
            Person person = m_repository.Persons.FirstOrDefault(r => r.Id == decree_operation.Person);
            if (((decree_operation.Persondecreeblocksubtype > 2 && decree_operation.Persondecreeblocksubtype <= 8) ||
                decree_operation.Persondecreeblocksubtype == 10))
                printing_string += (person != null ?
                    (getFullName(person.Id, 3, 2) + " ") :
                (decree_operation.Nonperson.Replace("\n", "").Count() != 0 ?
                    (" " + decree_operation.Nonperson.Replace("\n", "") + ";") :
                ""));
            if ((decree_operation.Persondecreeblocksubtype <= 2 ||
                decree_operation.Persondecreeblocksubtype == 9))
                printing_string += (person != null ?
                    (getFullName(person.Id, 3, 2, true) + " ") :
                (decree_operation.Nonperson.Replace("\n", "").Count() != 0 ?
                    (" " + decree_operation.Nonperson.Replace("\n", "") + ";") :
                ""));
            addFirstParagraffs(body, printing_string, true);
            printing_string = "Основание: " + decree_operation.Subvaluestring2;
            if (decree_operation.Persondecreeblocksubtype == 8 && decree_operation.Subvaluestring2.Length > 0)
                addFirstParagraffs(body, printing_string, true);
        }

        private void generateimposeapenalty(Body body,
            Persondecreeblock decree_block)
        {
            if (decree_block.Persondecreeblocktype != 2)
                return;
            addFirstParagraffs(body, decree_block.Index.ToString() +
                ". " +
                m_repository.Persondecreeblocktypes.First(r => r.Id == decree_block.Persondecreeblocktype).Name.ToString().ToUpper() +
                ":");
            
            List<Persondecreeoperation> decree_operations = m_repository.Persondecreeoperations.Where(r => r.Persondecreeblock == decree_block.Id).ToList();
            decree_operations.Sort((a, b) => a.Index.CompareTo(b.Index));

            foreach (Persondecreeoperation decree_operation in decree_operations)
            {
                generateimposeapenalty(body, decree_block, decree_operation);
            }
        }

        private void generateimposeapenalty(Body body,
            Persondecreeblock decree_block,
            Persondecreeoperation decree_operation)
        {
            if (decree_operation == null)
                return;
            string printing_string = "";
            Penalty penalty = m_repository.Penalties.FirstOrDefault(r => r.Id == decree_operation.Optionnumber1);
            printing_string += decree_operation.Intro + " объявить " +
                "«" + penalty.Name + "» ";
            Person person = m_repository.Persons.FirstOrDefault(r => r.Id == decree_operation.Person);
            printing_string += (person != null ?
                (getFullName(person.Id, 3, 2)) :
            (decree_operation.Nonperson.Replace("\n", "").Count() != 0 ?
                    (decree_operation.Nonperson.Replace("\n", "") + ";") :
                ""));
            addFirstParagraffs(body, printing_string, true);
            printing_string = "Основание: " + decree_operation.Subvaluestring2;
            if (decree_operation.Subvaluestring2.Length > 0)
                addFirstParagraffs(body, printing_string, true);
        }

        private void generatetoappoint(Body body,
            Persondecreeblock decree_block)
        {
            if (decree_block.Persondecreeblocktype != 3)
                return;
            addFirstParagraffs(body, decree_block.Index.ToString() +
                ". " +
                m_repository.Persondecreeblocktypes.First(r => r.Id == decree_block.Persondecreeblocktype).Name.ToString().ToUpper() +
                ":");

            List<Persondecreeoperation> decree_operations = m_repository.Persondecreeoperations.Where(r => r.Persondecreeblock == decree_block.Id).ToList();
            decree_operations.Sort((a, b) => a.Index.CompareTo(b.Index));

            foreach (Persondecreeoperation decree_operation in decree_operations)
            {
                generatetoappoint(body, decree_block, decree_operation);
            }
        }

        private void generatetoappoint(Body body,
            Persondecreeblock decree_block,
            Persondecreeoperation decree_operation)
        {
            if (decree_operation == null)
                return;
            string printing_string = "";
            Person person = m_repository.Persons.FirstOrDefault(r => r.Id == decree_operation.Person);
            Appointtype appointtype = m_repository.Appointtypes.FirstOrDefault(r => r.Id == decree_operation.Optionnumber4);
            Position position = m_repository.PositionsLocal().GetValue(decree_operation.Optionnumber1);
            Structure actualStructure = m_repository.GetActualStructureInfo(position.Structure, m_user.Date.GetValueOrDefault());
            string old_position = "";
            if (actualStructure != null)
            {
                old_position = m_repository.FormTreeDocument(actualStructure, m_user.Date.GetValueOrDefault(), position, 2, null);
            }

            printing_string += ((person != null && m_repository.Personranks.FirstOrDefault(r => r.Person == person.Id) != null) ?
                (getFullName(person.Id, 2, 2, with_position: false) + " " +
                (appointtype != null ? (appointtype.Name + " ") : "") + "на должность " +
                old_position +
                (decree_operation.Optiondate1 != null ? (" с " + decree_operation.Optiondate1.GetValueOrDefault().ToString("dd.MM.yyyy")) : "") +
                ", освободив его от должности " +
                getFullPositionName(person, 2, 2) + "." +
                ((decree_operation.Optionnumber2 != null && decree_operation.Optionnumber2 > 0) ?
                (" Присвоить специальное звание «" + m_repository.Ranks.FirstOrDefault(r => r.Id == decree_operation.Optionnumber2).getSubjectCase() + "».") :
                "")) : 
            (getFullName(person.Id, 3, 2, with_position: false) + " " +
                (appointtype != null ? (appointtype.Name + " ") : "") + "на должность " +
                decree_operation.Optionstring4 +
                (decree_operation.Optiondate1 != null ? (" с " + decree_operation.Optiondate1.GetValueOrDefault().ToString("dd.MM.yyyy")) : "") +
                ((decree_operation.Optionnumber2 != null && decree_operation.Optionnumber2 > 0) ?
                (" Присвоить специальное звание «" + m_repository.Ranks.FirstOrDefault(r => r.Id == decree_operation.Optionnumber2).getSubjectCase() + "»") :
                "") +
                ((decree_operation.Optionstring3 != null && decree_operation.Optionstring3.Length > 0) ?
                (" и личный номер " + decree_operation.Optionstring3 + ".") :
                ".")));
            if (decree_operation.Optiondate2 != null)
                printing_string += "Установить стаж для выплаты процентной надбавки за выслугу лет по состоянию на " +
                    decree_operation.Optiondate2.GetValueOrDefault().ToString("dd.MM.yyyy") + " " +
                    decree_operation.Optionnumber5.ToString() + " " + getYearString(decree_operation.Optionnumber5) + " " +
                    decree_operation.Optionnumber6.ToString() + " " + getMonthString(decree_operation.Optionnumber6) + " " +
                    decree_operation.Optionnumber7.ToString() + " " + getDayString(decree_operation.Optionnumber7) + ".";
            if (decree_operation.Optiondate3 != null)
                printing_string += "Заключить контракт сроком на " +
                    decree_operation.Optionnumber8.ToString() + " " + getYearString(decree_operation.Optionnumber8) + " " +
                    decree_operation.Optionnumber9.ToString() + " " + getMonthString(decree_operation.Optionnumber9) + " " +
                    decree_operation.Optionnumber10.ToString() + " " + getDayString(decree_operation.Optionnumber10) + " " +
                    "с " + decree_operation.Optiondate3.GetValueOrDefault().ToString("dd.MM.yyyy") +
                    " по " + decree_operation.Optiondate4.GetValueOrDefault().ToString("dd.MM.yyyy") + ".";
            addFirstParagraffs(body, printing_string, true);
        }

        private void generatedelete(Body body,
            Persondecreeblock decree_block)
        {
            if (decree_block.Persondecreeblocktype != 4)
                return;
            addFirstParagraffs(body, decree_block.Index.ToString() +
                ". " +
                m_repository.Persondecreeblocktypes.First(r => r.Id == decree_block.Persondecreeblocktype).Name.ToString().ToUpper() +
                ":");

            List<Persondecreeblocksub> decree_subs = m_repository.Persondecreeblocksubs.Where(r => r.Persondecreeblock == decree_block.Id).ToList();
            decree_subs.Sort((a, b) => a.Index.CompareTo(b.Index));

            foreach (Persondecreeblocksub decree_sub in decree_subs)
            {
                generatedelete(body, decree_block, decree_sub);
            }
        }

        private void generatedelete(Body body,
            Persondecreeblock decree_block,
            Persondecreeblocksub decree_sub)
        {
            if (decree_sub == null)
                return;

            List<Persondecreeoperation> decree_operations = m_repository.Persondecreeoperations.Where(r => r.Persondecreeblocksub == decree_sub.Id).ToList();
            decree_operations.Sort((a, b) => a.Index.CompareTo(b.Index));

            foreach (Persondecreeoperation decree_operation in decree_operations)
            {
                generatedelete(body, decree_block, decree_sub, decree_operation);
            }
        }

        private void generatedelete(Body body,
            Persondecreeblock decree_block,
            Persondecreeblocksub decree_sub,
            Persondecreeoperation decree_operation)
        {
            if (decree_operation == null)
                return;
            string printing_string = "";
            

            
        }

        private void generaterid(Body body,
            Persondecreeblock decree_block)
        {
            if (decree_block.Persondecreeblocktype != 5)
                return;
            addFirstParagraffs(body, decree_block.Index.ToString() +
                ". " +
                m_repository.Persondecreeblocktypes.First(r => r.Id == decree_block.Persondecreeblocktype).Name.ToString().ToUpper() +
                ":");

            List<Persondecreeblocksub> decree_subs = m_repository.Persondecreeblocksubs.Where(r => r.Persondecreeblock == decree_block.Id).ToList();
            decree_subs.Sort((a, b) => a.Index.CompareTo(b.Index));

            foreach (Persondecreeblocksub decree_sub in decree_subs)
            {
                generaterid(body, decree_block, decree_sub);
            }
        }

        private void generaterid(Body body,
            Persondecreeblock decree_block,
            Persondecreeblocksub decree_sub)
        {
            if (decree_sub == null)
                return;

            List<Persondecreeoperation> decree_operations = m_repository.Persondecreeoperations.Where(r => r.Persondecreeblocksub == decree_sub.Id).ToList();
            decree_operations.Sort((a, b) => a.Index.CompareTo(b.Index));

            foreach (Persondecreeoperation decree_operation in decree_operations)
            {
                generaterid(body, decree_block, decree_sub, decree_operation);
            }
        }

        private void generaterid(Body body,
            Persondecreeblock decree_block,
            Persondecreeblocksub decree_sub,
            Persondecreeoperation decree_operation)
        {
            if (decree_operation == null)
                return;
            Transfertype transfertype = m_repository.Transfertypes.FirstOrDefault(r => r.Id == decree_operation.Persondecreeblocksubtype);
            Person person = m_repository.Persons.FirstOrDefault(r => r.Id == decree_operation.Person);
            string printing_string = "";
            printing_string += decree_block.Index + "." +
                decree_sub.Index + ". в соответствии с п. 52 и п.п." +
                (transfertype != null ? (" " + transfertype.Pointsubpoint + ". ") : "") +
                " Положения о прохождении службы в органах и подразделениях по чрезвычайным ситуациям Республики Беларусь " +
                getFullName(decree_operation.Person, 2, 2, with_position: false) + " " +
                decree_operation.Optionstring1 + " от должности " + 
                getFullPositionName(person, 2, 2) + " и зачислить его в распоряжение начальника " +
                decree_operation.Optionstring2 + ".";
            addFirstParagraffs(body,
                printing_string,
                true);
            printing_string = "Основание: " + decree_operation.Optionstring3 + ".";
            if (decree_operation.Optionstring3.Length > 0)
                addFirstParagraffs(body, printing_string, true);
        }

        private void generateshifting(Body body,
            Persondecreeblock decree_block)
        {
            if (decree_block.Persondecreeblocktype != 6)
                return;
            addFirstParagraffs(body, decree_block.Index.ToString() +
                ". " +
                m_repository.Persondecreeblocktypes.First(r => r.Id == decree_block.Persondecreeblocktype).Name.ToString().ToUpper() +
                ":");

            List<Persondecreeoperation> decree_operations = m_repository.Persondecreeoperations.Where(r => r.Persondecreeblock == decree_block.Id).ToList();
            decree_operations.Sort((a, b) => a.Index.CompareTo(b.Index));

            foreach (Persondecreeoperation decree_operation in decree_operations)
            {
                generateshifting(body, decree_block, decree_operation);
            }
        }

        private void generateshifting(Body body,
            Persondecreeblock decree_block,
            Persondecreeoperation decree_operation)
        {
            if (decree_operation == null)
                return;
            Transfertype transfertype = m_repository.Transfertypes.FirstOrDefault(r => r.Id == decree_operation.Persondecreeblocksubtype);
            Person person = m_repository.Persons.FirstOrDefault(r => r.Id == decree_operation.Person);
            string printing_string = "";
            printing_string += decree_block.Index + "." +
                decree_operation.Index + ". в распоряжение " +
                decree_operation.Optionstring1 +
               (decree_operation.Optiondate1 != null ? (" c " + decree_operation.Optiondate1.GetValueOrDefault().ToString("dd.MM.yyyy") + " ") : "") +
                getFullName(decree_operation.Person, 2, 2, with_position: false) + ", освободив его от должности " +
                getFullPositionName(person, 2, 2) + " и зачислить его в распоряжение начальника " +
                decree_operation.Optionstring2 + ".";
            addFirstParagraffs(body,
                printing_string,
                true);
            printing_string = "Основание: " + decree_operation.Optionstring2 + ".";
            if (decree_operation.Optionstring2.Length > 0)
                addFirstParagraffs(body, printing_string, true);
        }

        private void generateterminateservice(Body body,
            Persondecreeblock decree_block)
        {
            if (decree_block.Persondecreeblocktype != 7)
                return;
            addFirstParagraffs(body, decree_block.Index.ToString() +
                ". " +
                m_repository.Persondecreeblocktypes.First(r => r.Id == decree_block.Persondecreeblocktype).Name.ToString().ToUpper() +
                ":");

            List<Persondecreeoperation> decree_operations = m_repository.Persondecreeoperations.Where(r => r.Persondecreeblock == decree_block.Id).ToList();
            decree_operations.Sort((a, b) => a.Index.CompareTo(b.Index));

            foreach (Persondecreeoperation decree_operation in decree_operations)
            {
                generateterminateservice(body, decree_block, decree_operation);
            }
        }

        private void generateterminateservice(Body body,
            Persondecreeblock decree_block,
            Persondecreeoperation decree_operation)
        {
            if (decree_operation == null)
                return;
            Transfertype transfertype = m_repository.Transfertypes.FirstOrDefault(r => r.Id == decree_operation.Persondecreeblocksubtype);
            Interrupttype interrupttype = m_repository.Interrupttypes.FirstOrDefault(r => r.Id == decree_operation.Optionnumber1);
            Person person = m_repository.Persons.FirstOrDefault(r => r.Id == decree_operation.Person);
            string printing_string = "";
            printing_string += decree_block.Index + "." +
                decree_operation.Index + ". в органах и подразделениях по чрезвычайным ситуациям по подпункту" +
                (interrupttype != null ? (" " + interrupttype.Pointsubpoint + " ") : "") +
                "Положения о прохождении службы в органах и подразделениях по чрезвычайным ситуациям Республики Беларусь" +
                (interrupttype != null ? (" (" + interrupttype.Description + ") ") : "") +
                getFullName(decree_operation.Person, 2, 2, with_position: false) + ", " +
                getFullPositionName(person, 2, 2) + ".";
            addFirstParagraffs(body,
                printing_string,
                true);
            printing_string = "Основание: " + decree_operation.Optionstring1 + ".";
            if (decree_operation.Optionstring1.Length > 0)
                addFirstParagraffs(body, printing_string, true);
        }

        private void generateremove(Body body,
            Persondecreeblock decree_block)
        {
            if (decree_block.Persondecreeblocktype != 8)
                return;
            addFirstParagraffs(body, decree_block.Index.ToString() +
                ". " +
                m_repository.Persondecreeblocktypes.First(r => r.Id == decree_block.Persondecreeblocktype).Name.ToString().ToUpper() +
                ":");

            List<Persondecreeoperation> decree_operations = m_repository.Persondecreeoperations.Where(r => r.Persondecreeblock == decree_block.Id).ToList();
            decree_operations.Sort((a, b) => a.Index.CompareTo(b.Index));

            foreach (Persondecreeoperation decree_operation in decree_operations)
            {
                generateremove(body, decree_block, decree_operation);
            }
        }

        private void generateremove(Body body,
            Persondecreeblock decree_block,
            Persondecreeoperation decree_operation)
        {
            if (decree_operation == null)
                return;
            Transfertype transfertype = m_repository.Transfertypes.FirstOrDefault(r => r.Id == decree_operation.Persondecreeblocksubtype);
            Interrupttype interrupttype = m_repository.Interrupttypes.FirstOrDefault(r => r.Id == decree_operation.Optionnumber1);
            Person person = m_repository.Persons.FirstOrDefault(r => r.Id == decree_operation.Person);
            string printing_string = "";
            printing_string += decree_block.Index + "." +
                decree_operation.Index + " в соответствии с п. 49 Положения о прохождении службы в органах и подразделениях по чрезвычайным ситуациям Республики Беларусь от исполнения служебных обязанностей" +
                getFullName(decree_operation.Person, 2, 2, with_position: false) + ", " +
                getFullPositionName(person, 2, 2) +
                (decree_operation.Optiondate1.GetValueOrDefault() != null ? (" , c " + decree_operation.Optiondate1.GetValueOrDefault().ToString("dd.MM.yyyy" + " ")) : "" ) +
                decree_operation.Optionstring1 + ".";
            addFirstParagraffs(body,
                printing_string,
                true);
            printing_string = "Основание: " + decree_operation.Optionstring2 + ".";
            if (decree_operation.Optionstring2.Length > 0)
                addFirstParagraffs(body, printing_string, true);
        }

        private void generatechangecredentials(Body body,
            Persondecreeblock decree_block)
        {
            if (decree_block.Persondecreeblocktype != 9)
                return;
            addFirstParagraffs(body, decree_block.Index.ToString() +
                ". " +
                m_repository.Persondecreeblocktypes.First(r => r.Id == decree_block.Persondecreeblocktype).Name.ToString().ToUpper() +
                ":");

            List<Persondecreeoperation> decree_operations = m_repository.Persondecreeoperations.Where(r => r.Persondecreeblock == decree_block.Id).ToList();
            decree_operations.Sort((a, b) => a.Index.CompareTo(b.Index));

            foreach (Persondecreeoperation decree_operation in decree_operations)
            {
                generatechangecredentials(body, decree_block, decree_operation);
            }
        }

        private void generatechangecredentials(Body body,
            Persondecreeblock decree_block,
            Persondecreeoperation decree_operation)
        {
            if (decree_operation == null)
                return;
            Person person = m_repository.Persons.FirstOrDefault(r => r.Id == decree_operation.Person);
            Changedocumentstype changedocumentstype = m_repository.Changedocumentstypes.FirstOrDefault(r => r.Id == decree_operation.Optionnumber1);
            string printing_string = "";
            printing_string += decree_block.Index + "." +
                decree_operation.Index;
            if (decree_operation.Optionnumber1 != null)
                printing_string += " " +
                    (decree_operation.Optionnumber1 != 3 ? (decree_operation.Optionstring1 + " ") : (changedocumentstype != null ? (changedocumentstype.Name + " ") : ""));
            printing_string += getFullName(decree_operation.Person, 2, 2, with_position: true) + ", " + 
                (decree_operation.Optiondate1.GetValueOrDefault() != null ? (" , c " + decree_operation.Optiondate1.GetValueOrDefault().ToString("dd.MM.yyyy" + " ")) : "") +
                decree_operation.Optionstring1 + ".";
            addFirstParagraffs(body,
                printing_string,
                true);
            printing_string = "Основание: " + decree_operation.Optionstring2 + ".";
            if (decree_operation.Optionstring2.Length > 0)
                addFirstParagraffs(body, printing_string, true);
        }



        private string getFullName(int person_id, int person_case = 3, int position_case = 2, bool with_number=false, bool with_position=true)
        {
            string printing_string = "";
            Person person = m_repository.Persons.FirstOrDefault(r => r.Id == person_id);
            if (person == null)
                return printing_string;
            List<Personrank> ranks = null;
            if (person != null)
            {
                ranks = person == null ? null : m_repository.Personranks.Where(r => r.Person == person.Id && r.Datestart.GetValueOrDefault() < m_decree.Datesigned).ToList();
                ranks.Sort((a, b) => b.Datestart.GetValueOrDefault().Ticks.CompareTo(a.Datestart.GetValueOrDefault().Ticks));
            }
            Personrank rank = (person == null || ranks == null || ranks.Count == 0) ? null : ranks.First();
            printing_string += (rank != null ? (m_repository.Ranks.First(r => r.Id == rank.Rank).getSubjectCase(person_case) + " ") : "") +
                person.getSubjectCase(person_case) +
                (with_number ? (" (" + person.Numpersonal + ") ") : "") +
                (with_position ? getFullPositionName(person, person_case, position_case) : "");
            return printing_string;
        }

        private Position getOldPosition(Person person)
        {
            return null;
        }

        private string getFullPositionName(Person person, int person_case = 3, int position_case = 2, string output_string = "")
        {
            if (person == null)
                return output_string;
            Position position = m_repository.Positions.FirstOrDefault(r => r.Id == person.Position);
            if (position == null)
                return output_string;
            foreach (int i in position.getSubjectsList())
                output_string += i != 0 ? (m_repository.Subjects.FirstOrDefault(r => r.Id == i).getSubjectCase(person_case) + " ") : "";
            output_string = getFullPositionName(m_repository.Structures.FirstOrDefault(r => r.Id == position.Structure), position_case, output_string);
            return output_string;
        }

        private string getFullPositionName(Structure structure, int case_value, string output_string = "")
        {
            if (structure == null)
                return (output_string.Last() == ' ' ? output_string.Substring(0, output_string.Length - 1) : output_string);
            foreach (int i in structure.getSubjectsList())
                output_string += i != 0 ? (m_repository.Subjects.FirstOrDefault(r => r.Id == i).getSubjectCase(case_value) + " ") : "";
            Structure parent_structure = m_repository.Structures.FirstOrDefault(r => r.Id == structure.Parentstructure);
            return getFullPositionName(parent_structure, case_value, output_string);
        }

        private string getYearString(int number)
        {
            int lastLetter = number % 10;
            if (number > 10 && number < 21) {
                return "лет";
            } else if (lastLetter == 1) {
                return "год";
            } else if (lastLetter > 1 && lastLetter < 5) {
                return "года";
            } else {
                return "лет";
            }
        }
        private string getMonthString(int number)
        {
            int lastLetter = number % 10;
            if (number > 10 && number < 21) {
                return "месяцев";
            } else if (lastLetter == 1) {
                return "месяц";
            } else if (lastLetter > 1 && lastLetter < 5) {
                return "месяца";
            } else {
                return "месяцев";
            }
        }
        private string getDayString(int number)
        {
            int lastLetter = number % 10;
            if (number > 10 && number < 21) {
                return "дней";
            } else if (lastLetter == 1) {
                return "дня";
            } else if (lastLetter > 1 && lastLetter < 5) {
                return "дней";
            } else {
                return "дней";
            }
        }
    }
}
