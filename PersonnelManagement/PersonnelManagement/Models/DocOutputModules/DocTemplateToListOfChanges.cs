using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace PersonnelManagement.Models
{
    public class DocTemplateToListOfChanges : DocTemplate
    {
        public static void CreateDocument(MemoryStream mem, Repository repository, DecreeChanges results, Decree decree, User user)
        {
            using (var document = WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document))
            {
                document.AddMainDocumentPart();
                document.MainDocumentPart.Document = new Document();
                int count = 0;
                foreach(DecreeChanges i in results.m_children_changes)
                {
                    var body = new Body();
                    SetMargins(body);
                    addFirstHeader(body, repository, i, decree, user, count);
                    document.MainDocumentPart.Document.AppendChild(body);
                    count++;
                }
            }
        }

        private static void addFirstHeader(Body body, Repository repository, DecreeChanges results, Decree decree, User user, int count = 0)
        {
            addFirstParagraffs(body, count);
            addSecondParagraffs(body);
            addThirdParagraffs(body, decree);

            addGeneralHeader(body, repository, results, user);

            TableCreator(body, repository, results, user);

            createSignature(body, repository);
            //appendDecreeHistory(body: body, name: "");
        }

        private static void addSecondParagraffs(Body body)
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Indentation() { Left = "6370" },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });

            Run run = new Run();
            Text text = new Text();
            text.Text = "приказ Министерства по чрезвычайным ситуациям Республики Беларусь";

            run.Append(text);
            AppendFontDefault(run);
            AppendFontSize(run, FontSize);
            Paragraph paragraph = new Paragraph();
            paragraph.AppendChild(paragraphProperties);
            paragraph.Append(run);
            body.AppendChild(paragraph);
        }

        protected static void addThirdParagraffs(Body body, Decree decree)
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Indentation() { Left = "6370" },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });

            string k = Char.ConvertFromUtf32(8199);
            string context1 = string.Concat(Enumerable.Repeat(k, 14));// "______________";
            string context2 = string.Concat(Enumerable.Repeat(k, 5));// "_____";
            if (decree.Dateactive != null)
            {
                context1 = string.Concat(Enumerable.Repeat(k, 2));// "__";
                context1 += decree.Dateactive.GetValueOrDefault().ToString("dd.MM.yyyy");
                context1 += string.Concat(Enumerable.Repeat(k, 2));// "__";
                if (decree.Number != null)
                    context2 = k + decree.Number;
                else
                    context2 = string.Concat(Enumerable.Repeat(k, 4));// "____";
                context2 += k;// "_";
            }
            Run run1 = new Run();
            Text text1 = new Text();
            text1.Text = context1;
            run1.Append(text1);
            AppendFontDefault(run1);
            AppendFontSize(run1, FontSize);
            Appendunderline(run1);

            Run run3 = new Run();
            Text text3 = new Text();
            text3.Text = "№";
            run3.Append(text3);
            AppendFontDefault(run3);
            AppendFontSize(run3, FontSize);

            Run run2 = new Run();
            Text text2 = new Text();
            text2.Text = context2;
            run2.Append(text2);
            AppendFontDefault(run2);
            AppendFontSize(run2, FontSize);
            Appendunderline(run2);
            Paragraph paragraph = new Paragraph(paragraphProperties, run1);
            paragraph.AppendChild(run3);
            paragraph.AppendChild(run2);
            body.AppendChild(paragraph);
        }

        private static void addGeneralName(Body body)
        {
            write_Text(body);

            ParagraphProperties paragraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });
            Run run = new Run();
            Text text = new Text();
            text.Text = "П Е Р Е Ч Е Н Ь".ToUpper();

            run.Append(text);
            AppendFontDefault(run);
            AppendFontSize(run, "30");
            AppendBold(run);
            Paragraph paragraph = new Paragraph(paragraphProperties, run);
            body.AppendChild(paragraph);
        }

        protected static void addGeneralHeader(Body body, Repository repository, DecreeChanges results, User user)
        {
            addGeneralName(body);
            addStructureName(body, repository, results.m_structure, user);

            addRangeAndCity(body, results.m_structure);
        }

        private static void addStructureName(Body body, Repository repository, Structure results, User user)
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Indentation() { Left = "200", Right = "200" },
                                                                              new Justification() { Val = JustificationValues.Center },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new ParagraphBorders()
                                                                              {
                                                                                  BetweenBorder = new BetweenBorder() { Val = BorderValues.Single },
                                                                                  BottomBorder = new BottomBorder() { Val = BorderValues.Single }
                                                                              },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });

            addParentsStructureName(body, paragraphProperties, repository, results, user, start: "изменений в штатах ");
        }

        private static void addRangeAndCity(Body body, Structure results)
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });
            Run run = new Run();
            Text text = new Text();
            text.Text = DecreeChanges.getRankAndCity(results);

            run.Append(text);
            AppendFontDefault(run);
            AppendFontSize(run, FontSize);
            Paragraph paragraph = new Paragraph(paragraphProperties, run);
            body.AppendChild(paragraph);
        }

        /*private static string getRankAndCity(Structure structure)
        {
            string rank = (structure.Rank != 0) ? structure.Rank.ToString() + "-й разряд" : "";
            string city = (structure.City != null) ? structure.City.ToString() : "";
            if (rank != "" && city != "")
                return "(" + rank + ", " + city + ")";
            return rank != "" ? "(" + rank + ")" : (city != "" ? "(" + city + ")" : "");
        }*/

        private static void TableCreator(Body body, Repository repository, DecreeChanges results, User user)
        {
            Table table = new Table();

            createTableHeader(table);

            TableProperties header_property = new TableProperties(new TableBorders(new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 }));
            table.AppendChild<TableProperties>(header_property);

            writeResualt(table, repository, user, results, results, forgot_header: true, under_level: 3);

            body.Append(table);
        }

        protected static void createTableHeader(Table table)
        {
            TableProperties header_property = new TableProperties(new TableBorders(new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 }));

            TableRow rowHeader = new TableRow();
            TableRow rowHeaderBottom = new TableRow();
            rowHeader.AppendChild(new TableHeader() { Val = OnOffOnlyValues.On });
            rowHeaderBottom.AppendChild(new TableHeader() { Val = OnOffOnlyValues.On });

            writeHeaderCell(rowHeader, "Наименование структурных подразделений и должностей".ToUpper(), "18", "70", BorderValues.Double, BorderValues.Double, merge_v: true);
            writeHeaderCell(rowHeaderBottom, "", "18", "70", BorderValues.Double, BorderValues.Double, merge_v: true, merge_v_cont: true);

            writeHeaderCell(rowHeader, "Специальное звание \r(категория персонала)", "16", "35", BorderValues.Double, BorderValues.Double, merge_v: true);
            writeHeaderCell(rowHeaderBottom, "", "16", "35", BorderValues.Double, BorderValues.Double, merge_v: true, merge_v_cont: true);

            writeHeaderCell(rowHeader, "Количество \rдолжностей \r(единиц техники \rи транспорта)", "16", "15", BorderValues.Double, merge_h: true);
            writeHeaderCell(rowHeader, "", "16", "15", BorderValues.Double, merge_h: true, merge_h_cont: true);

            writeHeaderCell(rowHeaderBottom, "вводится", "16", "15", BorderValues.Single, BorderValues.Double);
            writeHeaderCell(rowHeaderBottom, "сокращ.", "16", "15", BorderValues.Single, BorderValues.Double);

            /*writeHeaderCell(rowHeader, "Количество \rдолжностей \r(единиц техники \rи транспорта)", "16", "24", BorderValues.Double, BorderValues.Double, merge_h: true);
            writeHeaderCell(rowHeader, "", "16", "24", BorderValues.Double, BorderValues.Double, merge_h: true, merge_h_cont: true);*/

            writeHeaderCell(rowHeader, "Источник финанси-\rрования", "16", "16", BorderValues.Double, BorderValues.Double, merge_v: true);
            writeHeaderCell(rowHeaderBottom, "", "16", "16", BorderValues.Double, BorderValues.Double, merge_v: true, merge_v_cont: true);

            writeHeaderCell(rowHeader, "Примечание", "16", "20", BorderValues.Double, BorderValues.Double, merge_v: true);
            writeHeaderCell(rowHeaderBottom, "", "16", "20", BorderValues.Double, BorderValues.Double, merge_v: true, merge_v_cont: true);

            if (rowHeader.TableRowProperties == null)
            {
                rowHeader.TableRowProperties = new TableRowProperties();
            }
            if (rowHeaderBottom.TableRowProperties == null)
            {
                rowHeaderBottom.TableRowProperties = new TableRowProperties();
            }
            rowHeader.TableRowProperties.AppendChild(new TableRowHeight() { Val = 800, HeightType = HeightRuleValues.Exact });
            rowHeaderBottom.TableRowProperties.AppendChild(new TableRowHeight() { Val = 350, HeightType = HeightRuleValues.Exact });
            table.AppendChild<TableProperties>(header_property);
            table.Append(rowHeader);
            table.Append(rowHeaderBottom);
        }

        protected static void writeHeaderCell(TableRow row,
            string text,
            string font_size,
            string width_cell,
            BorderValues top_line = BorderValues.Single,
            BorderValues under_line = BorderValues.Single,
            bool bold = false,
            bool left_aling = false,
            bool upper_aling = false,
            bool merge_v = false,
            bool merge_v_cont = false,
            bool merge_h = false,
            bool merge_h_cont = false)
        {
            TableCell tableCellName = new TableCell();
            Paragraph paragraphCellName = new Paragraph(new ParagraphProperties(new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast }));
            Run runCellName = new Run();
            Text textCellName = new Text(text);
            runCellName.Append(textCellName);
            AppendFontDefault(runCellName);
            AppendFontSize(runCellName, font_size);
            if (bold)
                AppendBold(runCellName);
            paragraphCellName.Append(runCellName);

            TableCellProperties tableCellNameProperties = new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Pct, Width = width_cell });
            if (merge_v)
            {
                if (!merge_v_cont)
                    tableCellNameProperties.Append(new VerticalMerge() { Val = MergedCellValues.Restart });
                if (merge_v_cont)
                    tableCellNameProperties.Append(new VerticalMerge() { Val = MergedCellValues.Continue });
            } else if(merge_h)
            {
                if (!merge_h_cont)
                    tableCellNameProperties.Append(new HorizontalMerge() { Val = MergedCellValues.Restart });
                if (merge_h_cont)
                    tableCellNameProperties.Append(new HorizontalMerge() { Val = MergedCellValues.Continue });
            }
            tableCellNameProperties.Append(new TableCellVerticalAlignment()
            {
                Val = TableVerticalAlignmentValues.Center
            });
            if (upper_aling)
                tableCellNameProperties.TableCellVerticalAlignment.Val = TableVerticalAlignmentValues.Top;
            TableCellBorders tableBordersName = new TableCellBorders();
            TopBorder topBorderName = new TopBorder();
            //topBorderName.Val = new EnumValue<BorderValues>(BorderValues.Double);
            topBorderName.Val = new EnumValue<BorderValues>(top_line);
            topBorderName.Color = "000000";
            tableBordersName.AppendChild(topBorderName);
            tableCellNameProperties.Append(tableBordersName);

            TableCellBorders tableBordersNameBottom = new TableCellBorders();
            BottomBorder botBorderNameBottom = new BottomBorder();
            botBorderNameBottom.Val = new EnumValue<BorderValues>(under_line);
            botBorderNameBottom.Color = "000000";
            tableBordersNameBottom.AppendChild(botBorderNameBottom);
            tableCellNameProperties.Append(tableBordersNameBottom);

            tableCellName.Append(tableCellNameProperties);
            tableCellName.Append(paragraphCellName);

            if (left_aling)
                AppendParagraphALgne(paragraphCellName, JustificationValues.Left);
            else
                AppendParagraphALgne(paragraphCellName, JustificationValues.Center);
            row.Append(tableCellName);
        }

        private static void writeResualt(Table table, Repository repository, User user, DecreeChanges results, DecreeChanges struct_under_level, bool forgot_header=false, int under_level = 3)
        {
            under_level--;
            bool flag_counter_writer = false;
            if (!forgot_header)
            {
                Structure time_structure = results.m_structure;
                bool skip = false;
                foreach(Structure i in repository.GetChildrenList(repository.GetOriginalStructure(results.m_structure).Id))
                {
                    Structure l = repository.GetActualStructureInfo(i.Id, user.Date.GetValueOrDefault());
                    if (l != null && l.Nameshortened == "Аппарат отдела")
                    {
                        skip = true;
                        break;
                    }
                }
                if (time_structure.Nameshortened == "Аппарат отдела")
                {
                    time_structure = struct_under_level.m_structure;
                    skip = false;
                }
                flag_counter_writer = false;
                if (!skip)
                {
                    bool bold = true;
                    if (results.m_structure.City == "" || results.m_structure.City == null)
                        bold = false;
                    if (under_level == 0 && time_structure != struct_under_level.m_structure)
                    {
                        if (results.m_structure_custom_changes)
                            WriteStructure(table,
                                new ChangeDecreeItem(name: results.m_privios_structure.Nameshortened + " " + repository.GetStructureNameDocument(struct_under_level.m_structure, user.Date.GetValueOrDefault(), 2, user)),
                                                     bold: bold);
                        else if(results.m_structure_custom_deleted && !struct_under_level.m_structure_custom_deleted)
                            WriteStructure(table,
                                new ChangeDecreeItem(name: results.m_structure.Name + " " + repository.GetStructureNameDocument(struct_under_level.m_structure, user.Date.GetValueOrDefault(), 2, user)),
                                                     bold: bold);
                        else
                            WriteStructure(table,
                                new ChangeDecreeItem(name: results.m_structure.Nameshortened + " " + repository.GetStructureNameDocument(struct_under_level.m_structure, user.Date.GetValueOrDefault(), 2, user)),
                                                     bold: bold);
                    }
                    else
                    {
                        if (results.m_structure_custom_changes)
                            WriteStructure(table,
                                           new ChangeDecreeItem(name: results.m_privios_structure.Name), bold: bold);
                        else if (results.m_structure_custom_deleted && !struct_under_level.m_structure_custom_deleted)
                            WriteStructure(table,
                                new ChangeDecreeItem(name: results.m_structure.Name + " " + repository.GetStructureNameDocument(struct_under_level.m_structure, user.Date.GetValueOrDefault(), 2, user)),
                                                     bold: bold);
                        else 
                            WriteStructure(table,
                                           new ChangeDecreeItem(name: time_structure.Name), bold: bold);
                    }
                    foreach (ChangeDecreeItem i in results.m_structure_changes)
                    {
                        if (results.m_structure_custom_changes && i == results.m_structure_changes.Last())
                            WriteStructure(table, i, bold: bold, line: true);
                        else if (!bold && struct_under_level.m_structure_custom_deleted)
                            continue;
                        else
                            WriteStructure(table, i, bold: false, line: false);
                    }
                    if (results.m_structure_changes.Count == 0)
                        WriteStructure(table, new ChangeDecreeItem(name: DecreeChanges.getRankAndCity(time_structure)), bold: false, line: false);
                    flag_counter_writer = true;
                }
            }
            foreach(ChangeDecreeItem i in results.m_changes)
                WritePosition(table, i);
            WritePosition(table,
                new ChangeDecreeItem());

            foreach (DecreeChanges i in results.m_children_changes)
                writeResualt(table, repository, user, i, results, under_level: under_level);

            if (flag_counter_writer)
                WriteCounter(table,
                    new ChangeDecreeItem(name: "ИТОГО в " + results.m_structure.Nameshortened.ToString(),
                                         add: results.addition_operations == 0 ? "-" : results.addition_operations.ToString(),
                                         remove: results.forgote_operations == 0 ? "-" : results.forgote_operations.ToString()));
            else if(forgot_header)
                WriteCounter(table,
                    new ChangeDecreeItem(name: "ВСЕГО в " + results.m_structure.Nameshortened.ToString(),
                                         add: results.addition_operations == 0 ? "-" : results.addition_operations.ToString(),
                                         remove: results.forgote_operations == 0 ? "-" : results.forgote_operations.ToString()));
            WritePosition(table,
                new ChangeDecreeItem());
        }

        private static void WritePosition(Table table, ChangeDecreeItem results)
        {
            TableRow row = new TableRow(new TableHeader());
            row.AppendChild(new TableHeader() { Val = OnOffOnlyValues.Off });
            writeHeaderCell(row, results.name, FontSize, "70", BorderValues.None, BorderValues.None, left_aling: true, upper_aling: true);
            writeHeaderCell(row, results.rankname, FontSize, "35", BorderValues.None, BorderValues.None, left_aling: true, upper_aling: true);
            writeHeaderCell(row, results.add_number, FontSize, "15", BorderValues.None, BorderValues.None, upper_aling: true);
            writeHeaderCell(row, results.remove_number, FontSize, "15", BorderValues.None, BorderValues.None, upper_aling: true);
            writeHeaderCell(row, results.source_flag, FontSize, "16", BorderValues.None, BorderValues.None, upper_aling: true);
            writeHeaderCell(row, results.note, "16", "20", BorderValues.None, BorderValues.None, upper_aling: true);
            table.Append(row);
        }
        private static void WriteStructure(Table table, ChangeDecreeItem results, bool bold = true, bool line = true)
        {
            TableRow row = new TableRow(new TableHeader());
            row.AppendChild(new TableHeader() { Val = OnOffOnlyValues.Off });
            if(!line)
                writeHeaderCell(row, results.name, FontSize, "70", BorderValues.None, BorderValues.None, bold: bold);
            else
                writeHeaderCell(row, results.name, FontSize, "70", BorderValues.None, BorderValues.Single, bold: bold);
            writeHeaderCell(row, results.rankname, FontSize, "35", BorderValues.None, BorderValues.None);
            writeHeaderCell(row, results.add_number, FontSize, "15", BorderValues.None, BorderValues.None);
            writeHeaderCell(row, results.remove_number, FontSize, "15", BorderValues.None, BorderValues.None);
            writeHeaderCell(row, results.source_flag, FontSize, "16", BorderValues.None, BorderValues.None);
            writeHeaderCell(row, results.note, "16", "20", BorderValues.None, BorderValues.None);
            table.Append(row);
        }

        private static void WriteCounter(Table table, ChangeDecreeItem results)
        {
            TableRow row = new TableRow(new TableHeader());
            row.AppendChild(new TableHeader() { Val = OnOffOnlyValues.Off });
            writeHeaderCell(row, results.name, FontSize, "70", BorderValues.None, BorderValues.None, bold: true, left_aling: true);
            writeHeaderCell(row, results.rankname, FontSize, "35", BorderValues.None, BorderValues.None);
            writeHeaderCell(row, results.add_number, FontSize, "15", BorderValues.Single, BorderValues.None, bold: true);
            writeHeaderCell(row, results.remove_number, FontSize, "15", BorderValues.Single, BorderValues.None, bold: true);
            writeHeaderCell(row, results.source_flag, FontSize, "16", BorderValues.None, BorderValues.None);
            writeHeaderCell(row, results.note, "16", "20", BorderValues.None, BorderValues.None);
            table.Append(row);
        }
    }
}
