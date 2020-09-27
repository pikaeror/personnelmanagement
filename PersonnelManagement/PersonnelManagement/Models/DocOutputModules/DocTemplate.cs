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
    public class DocTemplate
    {
        protected const string FontFamily = "Times New Roman";
        protected const string FontSize = "28";
        public static void CreateDocument(MemoryStream mem, Repository repository, StaffForDoc results)
        {
            using (var document = WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document))
            {
                document.AddMainDocumentPart();
                document.MainDocumentPart.Document = new Document();
                var body = new Body();
                SetMargins(body);
                addFirstHeader(body, repository, results);
                document.MainDocumentPart.Document.AppendChild(body);
            }
        }

        protected static void addFirstHeader(Body body, Repository repository, StaffForDoc results)
        {
            addFirstParagraffs(body);
            addSecondParagraffs(body, repository, results);
            addThirdParagraffs(body, repository, results);

            addGeneralHeader(body, repository, results);

            TableCreator(body, repository, results);

            createSignature(body);

            appendDecreeHistory(body, results);
        }

        protected static void addFirstParagraffs(Body body, int new_page = 0)
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Indentation() { Left = "5670", FirstLine = "700" },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });
            RunProperties rPr = new RunProperties(
                new RunFonts()
                {
                    Ascii = FontFamily
                });
            Run run = new Run();
            if (new_page > 0)
                rPr.AppendChild(new Break() { Type = BreakValues.Page });
            run.PrependChild<RunProperties>(rPr);
            Text text = new Text();
            text.Text = "УТВЕРЖДЕНО";

            run.Append(text);
            AppendFontDefault(run);
            AppendFontSize(run, FontSize);
            Paragraph paragraph = new Paragraph(paragraphProperties, run);
            body.AppendChild(paragraph);
        }

        private static void addSecondParagraffs(Body body, Repository repository, StaffForDoc results)
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Indentation() { Left = "6370" },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });

            Run run = new Run();
            Text text = new Text();
            text.Text = results.getOrderName();

            run.Append(text);
            AppendFontDefault(run);
            AppendFontSize(run, FontSize);
            Paragraph paragraph = new Paragraph();
            paragraph.AppendChild(paragraphProperties);
            paragraph.Append(run);
            body.AppendChild(paragraph);
        }
        protected static void addThirdParagraffs(Body body, Repository repository, StaffForDoc results)
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Indentation() { Left = "6370" },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });

            string k = Char.ConvertFromUtf32(8199);
            string context1 = string.Concat(Enumerable.Repeat(k, 14));// "______________";
            string context2 = string.Concat(Enumerable.Repeat(k, 5));// "_____";
            if (results.m_first_order.decree.Datesigned != null)
            {
                context1 = string.Concat(Enumerable.Repeat(k, 2));// "__";
                context1 += results.m_first_order.decree.Datesigned.Value.ToString("dd.MM.yyyy");
                context1 += string.Concat(Enumerable.Repeat(k, 2));// "__";
                if (results.m_first_order.decree.Number != null)
                    context2 = k + results.m_first_order.decree.Number;
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

        protected static void SetMargins(Body body)
        {
            var sectionProps = new SectionProperties();
            var pageMargin = new PageMargin
            {
                Top = FromMilimeters(150),
                Left = FromMilimeters(300),
                Right = FromMilimeters(80),
                Bottom = FromMilimeters(150)
            };
            sectionProps.AppendChild(pageMargin);
            body.AppendChild(sectionProps);
        }

        private static StringValue FromMilimetersString(int i)
        {
            return new StringValue(FromMilimeters(i).ToString());
        }

        protected static ushort FromMilimeters(float value)
        {
            try
            {
                checked
                {
                    return (ushort)(value * 5.67);
                }
            }
            catch
            {
                throw new ArgumentOutOfRangeException("value");
            }
        }

        protected static void addGeneralHeader(Body body, Repository repository, StaffForDoc results)
        {
            addGeneralName(body);
            addStructureName(body, repository, results);

            addRangeAndCity(body, repository, results);
            addDateActive(body, repository, results);
        }

        private static void addGeneralName(Body body)
        {
            write_Text(body);

            ParagraphProperties paragraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });
            Run run = new Run();
            Text text = new Text();
            text.Text = "Ш Т А Т";

            run.Append(text);
            AppendFontDefault(run);
            AppendFontSize(run, "30");
            AppendBold(run);
            Paragraph paragraph = new Paragraph(paragraphProperties, run);
            body.AppendChild(paragraph);
        }

        private static void addStructureName(Body body, Repository repository, StaffForDoc results)
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

            addParentsStructureName(body, paragraphProperties, repository, results.m_full_staff.m_structure, results.m_user);
        }
        protected static void addParentsStructureName(Body body, ParagraphProperties paragraphProperties, Repository repository, Structure structure, User user, int number_of_padege = 2, string previus_string = "", string start = "")
        {
            if (previus_string != repository.GetStructureNameDocument(structure, user.Date.GetValueOrDefault(), number_of_padege, user))
            {
                List<string> writer_list = splitNameBylens(start + repository.GetStructureNameDocument(structure, user.Date.GetValueOrDefault(), number_of_padege, user), new List<string>());
                foreach (string i in writer_list)
                {
                    Run run = new Run();
                    Text text = new Text();
                    text.Text = i;

                    run.Append(text);
                    AppendFontDefault(run);
                    AppendFontSize(run, FontSize);
                    AppendBold(run);
                    Paragraph paragraph = new Paragraph((ParagraphProperties)paragraphProperties.CloneNode(true), run);
                    paragraph.ParagraphProperties.SpacingBetweenLines = new SpacingBetweenLines();
                    paragraph.ParagraphProperties.SpacingBetweenLines.After = "0";
                    body.AppendChild(paragraph);
                }
            }
            previus_string = repository.GetStructureNameDocument(structure, user.Date.GetValueOrDefault(), number_of_padege, user);

            if (structure.Parentstructure == 0)
                return;

            addParentsStructureName(body, paragraphProperties, repository, repository.StructuresLocal()[structure.Parentstructure], user, previus_string: previus_string);
            return;
        }

        private static List<string> splitNameBylens(string input, List<string> old_string, int max_len = 61)
        {
            if (input == "" || input == null)
                return old_string;
            string time_string = "";
            foreach (string i in input.Split(' '))
            {
                if (time_string.Length + i.Length + 1 <= max_len)
                    time_string += i + " ";
                else
                {
                    old_string.Add(time_string);
                    return splitNameBylens(input.Substring(time_string.Length), old_string);
                }
            }
            old_string.Add(time_string);
            return old_string;
        }

        private static void addRangeAndCity(Body body, Repository repository, StaffForDoc results)
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });
            Run run = new Run();
            Text text = new Text();
            string write_text = "";
            if (results.m_actual_structure.Rank != 0 && results.m_actual_structure.Rank != null)
                write_text += "(" + results.m_actual_structure.Rank.ToString() + "-й разряд";
            if (results.m_actual_structure.City != null && results.m_actual_structure.City != "")
            {
                if (write_text != "")
                    write_text += ", ";
                else
                    write_text += "(";
                write_text += results.m_actual_structure.City + ")";
            }
            else
            {
                if (write_text == "")
                    return;
                write_text += ")";
            }
            text.Text = write_text;

            run.Append(text);
            AppendFontDefault(run);
            AppendFontSize(run, FontSize);
            Paragraph paragraph = new Paragraph(paragraphProperties, run);
            body.AppendChild(paragraph);
        }

        private static void addDateActive(Body body, Repository repository, StaffForDoc results)
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });
            Run run = new Run();
            Text text = new Text();
            text.Text = "(вступает в силу " + results.m_first_order.decree.Dateactive.GetValueOrDefault().ToString("dd.MM.yyyy") + ")";

            run.Append(text);
            AppendFontDefault(run);
            AppendFontSize(run, FontSize);
            Paragraph paragraph = new Paragraph(paragraphProperties, run);
            body.AppendChild(paragraph);
            write_Text(body);
        }

        protected static void write_Text(Body body, string line = "")
        {
            ParagraphProperties paragraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center },
                                                                              new ContextualSpacing() { Val = false },
                                                                              new SpacingBetweenLines() { AfterLines = 0, BeforeLines = 0, After = "0", Before = "0", LineRule = LineSpacingRuleValues.AtLeast });
            Run run = new Run();
            Text text = new Text();
            text.Text = line;

            run.Append(text);
            AppendFontDefault(run);
            AppendFontSize(run, FontSize);
            Paragraph paragraph = new Paragraph(paragraphProperties, run);
            body.AppendChild(paragraph);
        }

        private static void TableCreator(Body body, Repository repository, StaffForDoc results)
        {
            Table table = new Table();

            createTableHeader(table);

            TableProperties header_property = new TableProperties(new TableBorders(new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 }));
            table.AppendChild<TableProperties>(header_property);

            double full_positions = 0;
            writeResualt(table, repository, results.m_full_staff, out full_positions, flag_first_structure: true);
            writeSpecialTypeTotal(table, results.m_full_staff, repository);

            body.Append(table);
        }

        private static void writeSpecialTypeTotal(Table table, staffTree results, Repository repository)
        {
            if (results.m_total_special_type != null)
            {
                writeEmptyRow(table);
                TableRow row = new TableRow(new TableHeader());
                row.AppendChild(new TableHeader() { Val = OnOffOnlyValues.Off });
                writeHeaderCell(row, "Из них", FontSize, "75", BorderValues.None, BorderValues.Single, bold: true);
                writeHeaderCell(row, "", FontSize, "35", BorderValues.None, BorderValues.None);
                writeHeaderCell(row, "", FontSize, "24", BorderValues.None, BorderValues.None);
                writeHeaderCell(row, "", FontSize, "16", BorderValues.None, BorderValues.None);
                writeHeaderCell(row, "", FontSize, "25", BorderValues.None, BorderValues.None);
                table.Append(row);

                List<Positioncategoryrank> for_forech_item = results.m_total_special_type.Keys.ToList();
                for_forech_item.Sort((a, b) => a.Id.CompareTo(b.Id));
                foreach (Positioncategoryrank i in for_forech_item)
                {
                    List<int> for_forech_source = results.m_special_type_source[i].Keys.ToList();
                    for_forech_source.Sort((a, b) => a.CompareTo(b));
                    string time_source = "";
                    string devider_source = ";";
                    if (for_forech_source.Count > 1)
                        devider_source = ";";
                    foreach (int j in for_forech_source)
                    {
                        if (time_source != "")
                            time_source += devider_source + '\r';
                        time_source += results.m_special_type_source[i][j].ToString() + "-" + stringToLetter(repository.SourceoffinancingsLocal()[j].Name);
                    }

                    writeEmptyRow(table);
                    row = new TableRow(new TableHeader());
                    row.AppendChild(new TableHeader() { Val = OnOffOnlyValues.Off });
                    writeHeaderCell(row, i.Name, FontSize, "75", BorderValues.None, BorderValues.None, left_aling: true, upper_aling: true);
                    writeHeaderCell(row, "", FontSize, "35", BorderValues.None, BorderValues.None, left_aling: true, upper_aling: true);
                    writeHeaderCell(row, results.m_total_special_type[i].ToString(), FontSize, "24", BorderValues.None, BorderValues.None, upper_aling: true);
                    writeHeaderCell(row, time_source, FontSize, "16", BorderValues.None, BorderValues.None, upper_aling: true);
                    writeHeaderCell(row, "", "16", "25", BorderValues.None, BorderValues.None, upper_aling: true);

                    table.Append(row);
                }
            }
        }

        protected static void createTableHeader(Table table)
        {
            TableProperties header_property = new TableProperties(new TableBorders(new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 }));

            TableRow rowHeader = new TableRow();
            rowHeader.AppendChild(new TableHeader() { Val = OnOffOnlyValues.On });

            writeHeaderCell(rowHeader, "Наименование структурных подразделений и должностей".ToUpper(), "18", "75", BorderValues.Double, BorderValues.Double);
            writeHeaderCell(rowHeader, "Специальное звание \r(категория персонала)", "16", "35", BorderValues.Double, BorderValues.Double);
            writeHeaderCell(rowHeader, "Количество \rдолжностей \r(единиц техники \rи транспорта)", "16", "24", BorderValues.Double, BorderValues.Double);
            writeHeaderCell(rowHeader, "Источник финанси-\rрования", "16", "16", BorderValues.Double, BorderValues.Double);
            writeHeaderCell(rowHeader, "Примечание", "16", "25", BorderValues.Double, BorderValues.Double);

            if (rowHeader.TableRowProperties == null)
            {
                rowHeader.TableRowProperties = new TableRowProperties();
            }
            rowHeader.TableRowProperties.AppendChild(new TableRowHeight() { Val = 1150, HeightType = HeightRuleValues.Exact });
            table.AppendChild<TableProperties>(header_property);
            table.Append(rowHeader);
        }

        protected static void writeHeaderCell(TableRow row,
            string text,
            string font_size,
            string width_cell,
            BorderValues top_line = BorderValues.Single,
            BorderValues under_line = BorderValues.Single,
            bool bold = false,
            bool left_aling = false,
            bool upper_aling = false)
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
            tableCellNameProperties.Append(new VerticalMerge()
            {
                Val = MergedCellValues.Restart
            });
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

        private static string stringToLetter(string input)
        {
            string output = "";
            foreach (string i in input.Split(" "))
                output += char.ToUpper(i[0]);
            return output;
        }

        private static (double, double) preWritePosition(Table table, Repository repository, staffTree results, double del_positions, double full_counter, bool flag_first_structure = false, bool without_header = false)
        {
            if (results.m_positions.Count != 0)
            {
                writeEmptyRow(table);
                TableRow row = new TableRow(new TableHeader());
                row.AppendChild(new TableHeader() { Val = OnOffOnlyValues.Off });
                if(!without_header)
                    writeHeaderCell(row, results.m_structure.Name, FontSize, "75", BorderValues.None, BorderValues.Single, bold: true);
                else
                    writeHeaderCell(row, "", FontSize, "75", BorderValues.None, BorderValues.Single, bold: true);
                writeHeaderCell(row, "", FontSize, "35", BorderValues.None, BorderValues.None);
                writeHeaderCell(row, "", FontSize, "24", BorderValues.None, BorderValues.None);
                writeHeaderCell(row, "", FontSize, "16", BorderValues.None, BorderValues.None);
                writeHeaderCell(row, "", FontSize, "25", BorderValues.None, BorderValues.None);
                table.Append(row);

                foreach (List<Position> i in results.m_unique_positions.Values)
                {
                    foreach (Position j in i)
                    {
                        del_positions -= results.m_del_positions_by_structure[j.Positiontype];
                        string time_source = "";
                        string note = "";
                        string devider_source = ";";
                        if (results.m_source_note[j.Positiontype].Count > 1)
                            devider_source = ";";
                        foreach (SourceNote k in results.m_source_note[j.Positiontype])
                        {
                            if (time_source != "")
                                time_source += devider_source + '\r';
                            if (k.number == 0)
                                time_source += "-";
                            else
                                time_source += k.number.ToString() + "-" + stringToLetter(k.m_source.Name);

                            foreach (DateTime? p in k.m_date_activation.Keys)
                            {
                                if (note != "")
                                    note += devider_source + '\r';
                                note += k.m_date_activation[p].ToString() + " " + stringToLetter(k.m_source.Name) + "- c " + p.GetValueOrDefault().ToString("dd.MM.yyyy");
                            }
                        }
                        string position_category = "";
                        if (j.Cap != 0)
                            position_category = repository.RanksLocal()[j.Cap.GetValueOrDefault()].Name;
                        else
                            position_category = repository.PositioncategoriesLocal()[j.Positioncategory].Name;

                        string positions_counter = "-";
                        if (i.Count - results.m_del_positions_by_structure[j.Positiontype] != 0)
                            positions_counter = (i.Count - results.m_del_positions_by_structure[j.Positiontype]).ToString();
                        writePosition(table, repository, j, positions_counter, time_source, position_category, note);
                        break;
                    }
                }
                writeCountByStructure(table, results.m_positions.Count + del_positions);
            }
            else
            {
                if (!flag_first_structure)
                {
                    writeEmptyRow(table);
                    TableRow row = new TableRow(new TableHeader());
                    row.AppendChild(new TableHeader() { Val = OnOffOnlyValues.Off });
                    writeHeaderCell(row, results.m_structure.Name, FontSize, "75", BorderValues.None, BorderValues.Single, bold: true);
                    writeHeaderCell(row, "", FontSize, "35", BorderValues.None, BorderValues.None);
                    writeHeaderCell(row, "", FontSize, "24", BorderValues.None, BorderValues.None);
                    writeHeaderCell(row, "", FontSize, "16", BorderValues.None, BorderValues.None);
                    writeHeaderCell(row, "", FontSize, "25", BorderValues.None, BorderValues.None);
                    table.Append(row);
                }
            }
            full_counter += results.m_positions.Count + del_positions;
            return (full_counter, del_positions);
        }

        private static void writeResualt(Table table, Repository repository, staffTree results, out double counter, bool flag_first_structure = false)
        {
            double full_counter = 0;
            double del_positions = 0;
            if (!flag_first_structure)
                (full_counter, del_positions) = preWritePosition(table, repository, results, del_positions, full_counter, flag_first_structure: flag_first_structure);

            if (results.m_children_structure == null)
            {
                if (flag_first_structure)
                    (full_counter, del_positions) = preWritePosition(table, repository, results, del_positions, full_counter, flag_first_structure: flag_first_structure, without_header: true);
                counter = full_counter;
                return;
            }
            int bool_counter = 0;
            foreach (staffTree j in results.m_children_structure)
            {
                double time_count = 0;
                bool flag = false;
                if (flag_first_structure && (j.m_positions.Count() == 0 || j.m_positions == null) && bool_counter < 1)
                {
                    flag = true;
                    bool_counter++;
                }
                writeResualt(table, repository, j, out time_count, flag);
                full_counter += time_count;
            }
            if (flag_first_structure)
                (full_counter, del_positions) = preWritePosition(table, repository, results, del_positions, full_counter, flag_first_structure: flag_first_structure, without_header: true);
            counter = full_counter;
            writeCountByStructure(table, counter);
        }

        private static void writeEmptyRow(Table table)
        {
            TableRow row = new TableRow(new TableHeader());
            row.AppendChild(new TableHeader() { Val = OnOffOnlyValues.Off });
            writeHeaderCell(row, "", FontSize, "75", BorderValues.None, BorderValues.None);
            writeHeaderCell(row, "", FontSize, "35", BorderValues.None, BorderValues.None);
            writeHeaderCell(row, "", FontSize, "24", BorderValues.None, BorderValues.None);
            writeHeaderCell(row, "", FontSize, "16", BorderValues.None, BorderValues.None);
            writeHeaderCell(row, "", FontSize, "25", BorderValues.None, BorderValues.None);

            table.Append(row);
        }

        private static void writePosition(Table table, Repository repository, Position position, string count_by_position, string scourse, string position_category, string note)
        {
            writeEmptyRow(table);
            TableRow row = new TableRow(new TableHeader());
            row.AppendChild(new TableHeader() { Val = OnOffOnlyValues.Off });
            writeHeaderCell(row, repository.PositiontypesLocal()[position.Positiontype].Name, FontSize, "75", BorderValues.None, BorderValues.None, left_aling: true, upper_aling: true);
            writeHeaderCell(row, position_category.ToLower(), FontSize, "35", BorderValues.None, BorderValues.None, left_aling: true, upper_aling: true);
            writeHeaderCell(row, count_by_position, FontSize, "24", BorderValues.None, BorderValues.None, upper_aling: true);
            writeHeaderCell(row, scourse, FontSize, "16", BorderValues.None, BorderValues.None, upper_aling: true);
            writeHeaderCell(row, note, "16", "25", BorderValues.None, BorderValues.None, upper_aling: true);

            table.Append(row);
        }

        private static void writeCountByStructure(Table table, double count)
        {
            writeEmptyRow(table);
            TableRow row = new TableRow(new TableHeader());
            row.AppendChild(new TableHeader() { Val = OnOffOnlyValues.Off });
            writeHeaderCell(row, "", FontSize, "75", BorderValues.None, BorderValues.None);
            writeHeaderCell(row, "", FontSize, "35", BorderValues.None, BorderValues.None);
            writeHeaderCell(row, count.ToString(), FontSize, "24", BorderValues.Single, BorderValues.None, true);
            writeHeaderCell(row, "", FontSize, "16", BorderValues.None, BorderValues.None);
            writeHeaderCell(row, "", FontSize, "25", BorderValues.None, BorderValues.None);

            table.Append(row);
        }

        protected static void createSignature(Body body)
        {
            Paragraph par = new Paragraph(new Run(new Text("")));
            body.Append(par);

            Table table = new Table(new TableProperties(new TableWidth() { Width = "2594", Type = TableWidthUnitValues.Pct },
                                                        new TableIndentation() { Width = 4688, Type = TableWidthUnitValues.Dxa }));

            List<string> signature_names = new List<string> { "В.Н.Малахов", "А.М.Юржиц", "Д.Н.Турчин", "С.Л.Новик", "Е.Э.Дешковец", "С.Д.Ефремов", "О.В.Шабайлова" };
            foreach (string i in signature_names)
            {
                TableRow row = new TableRow(new TableRowProperties());
                writeHeaderCell(row, "", FontSize, "25", BorderValues.None, BorderValues.Single, left_aling: true);
                writeHeaderCell(row, "", FontSize, "2", BorderValues.None, BorderValues.None, left_aling: true);
                writeHeaderCell(row, i, FontSize, "25", BorderValues.None, BorderValues.None, left_aling: true);
                row.TableRowProperties.AppendChild(new TableRowHeight() { Val = 500, HeightType = HeightRuleValues.Exact });
                table.Append(row);
            }
            body.AppendChild(table);
        }

        protected static void appendDecreeHistory(Body body, string name = "Изменения внесены приказами МЧС:")
        {
            Run run = new Run(new Break() { Type = BreakValues.Page });
            Text text = new Text();
            text.Text = name;

            run.Append(text);
            AppendFontDefault(run);
            AppendFontSize(run, FontSize);
            Paragraph paragraph = new Paragraph(new ParagraphProperties(), run);
            paragraph.ParagraphProperties.SpacingBetweenLines = new SpacingBetweenLines();
            paragraph.ParagraphProperties.SpacingBetweenLines.After = "0";
            body.AppendChild(paragraph);
        }
        private static void appendDecreeHistory(Body body, StaffForDoc results)
        {
            appendDecreeHistory(body);
            foreach (Order i in results.full_order_history)
            {
                string time_str = "от " + i.decree.Datesigned.GetValueOrDefault().ToString("dd.MM.yyyy");// + " № " + i.decree.Number;
                Regex regular_value_for_number = new Regex(@"(№[\s]{0,}[\d]+)");
                if (regular_value_for_number.Matches(i.name).Count > 0)
                    time_str += " № " + regular_value_for_number.Matches(i.name)[0].Value.Substring(1);
                Run run = new Run();
                Text text = new Text();
                text.Text = time_str;
                run.Append(text);
                AppendFontDefault(run);
                AppendFontSize(run, "18");
                
                Paragraph paragraph = new Paragraph(new ParagraphProperties(), run);
                paragraph.ParagraphProperties.SpacingBetweenLines = new SpacingBetweenLines();
                paragraph.ParagraphProperties.SpacingBetweenLines.After = "0";
                body.AppendChild(paragraph);
            }
        }
        public static void AppendFontDefault(RunProperties runProperties)
        {
            // Main font
            RunFonts font = new RunFonts() { HighAnsi = FontFamily, Ascii = FontFamily, ComplexScript = FontFamily };
            runProperties.Append(font);
        }

        public static void AppendFontDefault(Run run)
        {
            // Main font
            if (run.RunProperties == null)
            {
                RunProperties runProperties = new RunProperties();
                run.PrependChild<RunProperties>(runProperties);
            }
            AppendFontDefault(run.RunProperties);
        }

        public static void AppendFontSize(RunProperties runProperties, string size)
        {
            runProperties.FontSize = new FontSize();
            runProperties.FontSize.Val = new StringValue(size);
        }

        public static void AppendFontSize(Run run, string size)
        {
            // Main font
            if (run.RunProperties == null)
            {
                RunProperties runProperties = new RunProperties();
                run.PrependChild<RunProperties>(runProperties);
            }
            AppendFontSize(run.RunProperties, size);
        }

        public static void Appendunderline(RunProperties runProperties)
        {
            runProperties.Underline = new Underline();
            runProperties.Underline.Val = new EnumValue<UnderlineValues>(0);
        }

        public static void Appendunderline(Run run)
        {
            // Main font
            if (run.RunProperties == null)
            {
                RunProperties runProperties = new RunProperties();
                run.PrependChild<RunProperties>(runProperties);
            }
            Appendunderline(run.RunProperties);
        }

        public static void AppendBold(RunProperties runProperties)
        {
            runProperties.Bold = new Bold();
            runProperties.Bold.Val = new OnOffValue(true);
        }

        public static void AppendBold(Run run)
        {
            // Main font
            if (run.RunProperties == null)
            {
                RunProperties runProperties = new RunProperties();
                run.PrependChild<RunProperties>(runProperties);
            }
            AppendBold(run.RunProperties);
        }

        public static void AppendParagraphALgne(ParagraphProperties paragraphProperties, JustificationValues type)
        {
            // Main font
            paragraphProperties.Append(new Justification() { Val = type });
        }

        public static void AppendParagraphALgne(Paragraph paragraph, JustificationValues type)
        {
            // Main font
            if (paragraph.ParagraphProperties == null)
            {
                ParagraphProperties paragraphProperties = new ParagraphProperties();
                paragraph.PrependChild<ParagraphProperties>(paragraphProperties);
            }
            AppendParagraphALgne(paragraph.ParagraphProperties, type);
        }

    }
}
