using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;
using PersonnelManagement.USERS;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/PrintStructurestaff")]
    public class PrintStructurestaffController : Controller
    {

        private Repository repository;
        private Dictionary<UInt32, Row> rows;
        private const int ROW_COUNT = 400;

        private const int POSITION_START_X = 2;
        private const int POSITION_START_Y = 4;

        public PrintStructurestaffController(Repository repository)
        {
            this.repository = repository;
        }

        // POST: api/PrintStructurestaff
        [HttpPost]
        public IActionResult PostPmrequest([FromBody] StaffManagement staffManagement)
        {
            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadStructure(sessionid, repository, staffManagement.Id);
                if (!hasAccess)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");

                }
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }

            if (staffManagement.Type.Equals(Keys.STAFF_MANAGEMENT_TYPE_ALL))
            {
                return File(GenerateDocument(staffManagement, user), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        public MemoryStream GenerateDocument(StaffManagement staffManagement, User user)
        {
            DateTime date = user.Date.GetValueOrDefault();
            Dictionary<int, Positiontype> positiontypesLocal = repository.PositiontypesLocal();
            Dictionary<int, Sourceoffinancing> sofsLocal = repository.SourceoffinancingsLocal();
            Dictionary<int, Positioncategory> positioncategoriesLocal = repository.PositioncategoriesLocal();

            Structure structure = repository.StructuresLocal()[staffManagement.Id]; // ПОЛОЦК И НОВОПОЛОЦК
            Structure originalStructure = repository.GetOriginalStructure(staffManagement.Id);
            Structure actualStructure = structure;
            if (staffManagement.Realid != 0)
            {
                actualStructure = repository.StructuresLocal()[staffManagement.Realid];
            }
            //bool mainStructureSigned = repository.IsSigned(structure, user.Date.GetValueOrDefault());

            bool heading = false;
            if (staffManagement.Head > 0)
            {
                heading = true;
            }

            List<Position> headPositions = repository.GetPositions(structure.Id, user.Date.GetValueOrDefault(), heading, true).ToList();

            //IEnumerable<Structure> structures = repository.GetChildren(structure.Id);
            IEnumerable<Structure> structures = repository.GetChildren(originalStructure.Id).ToList(); // Детей надо брать с оригинала.

            //structures = structures.Where(s => repository.IsSigned(s, user.Date.GetValueOrDefault()));
            //List<Structure> filteredStructures = new List<Structure>();
            //foreach (Structure subStructure in structures)
            //{

            //    Structure actualSubStructure = repository.GetActualStructureInfo(subStructure.Id, user.Date.GetValueOrDefault(), structures);
            //    if (actualSubStructure != null)
            //    {
            //        bool hasOrigin = true;
            //        if (actualSubStructure.Changeorigin > 0 && !repository.StructuresLocal().ContainsKey(actualSubStructure.Changeorigin))
            //        {
            //            hasOrigin = false; // bug
            //        }
            //        if (hasOrigin)
            //        {
            //            filteredStructures.Add(actualSubStructure);
            //        }
            //    }
            //    else
            //    {
            //        Structure debug = structure;
            //    }

            //}
            //structures = repository.FilterDeletedStructures(filteredStructures.Distinct().ToList(), date).OrderBy(o => o.Priority).ToList();

            structures = repository.FilterDeletedStructures(structures, date).ToList();
            List<Structure> filteredStructures = new List<Structure>();
            foreach (Structure structureEl in structures)
            {
                Structure actualStructureEl = repository.GetActualStructureInfo(structureEl.Id, date, structures);

                if (actualStructureEl != null)
                {
                    filteredStructures.Add(actualStructureEl);
                }

            }

            structures = filteredStructures.Distinct().ToList();
            structures = structures.Where(s => s.Parentstructure == originalStructure.Id).OrderBy(o => o.Priority).ToList(); // Отбраковывает те подразделения, которые когда-то были подчинены введенному, но потом перестали (были перемещены).


            double count = 0;
            Dictionary<int, PrintCategoryInfo> categories = new Dictionary<int, PrintCategoryInfo>();

            double countFuture = 0;
            Dictionary<int, PrintCategoryInfo> categoriesFuture = new Dictionary<int, PrintCategoryInfo>();

            /**
             * Document generator
             */
            var mem = new MemoryStream();
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(mem, SpreadsheetDocumentType.Workbook, true))
            {
                
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                WorkbookStylesPart wbsp = workbookPart.AddNewPart<WorkbookStylesPart>();
                // add styles to sheet
                wbsp.Stylesheet = CreateStylesheet();
                wbsp.Stylesheet.Save();
                Worksheet worksheet = new Worksheet();
                DocumentFormat.OpenXml.Spreadsheet.SheetData sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
                Columns columns = new Columns();
                columns.Append(new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)100U, Width = 10, CustomWidth = true });
                worksheet.Append(columns);
                worksheet.Append(sheetData);
                worksheetPart.Worksheet = worksheet;
                MergeCells mergeCells = new MergeCells();
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Структура" };



                sheets.Append(sheet);

                rows = new Dictionary<UInt32, Row>();
                for (UInt32 i = 1; i <= ROW_COUNT; i++)
                {
                    var registerRow = new Row { RowIndex = i };
                    sheetData.Append(registerRow);
                    rows.Add(i, registerRow);
                }
    

                    UInt32 rowIndex = 1;

                Cell structureCell = CreateTextCell(ColumnLetter(2),
                    rowIndex, "СТРУКТУРА");
                rows[rowIndex].AppendChild(structureCell);
                AddBold(document, structureCell);

                rowIndex++;

                string fullStructureName = repository.FormTreeDocument1(actualStructure, true, date);
                //Cell structureNameCell = CreateTextCell(ColumnLetter(2),
                //    rowIndex, actualStructure.Name);
                Cell structureNameCell = CreateTextCell(ColumnLetter(2),
                    rowIndex, fullStructureName);
                rows[rowIndex].AppendChild(structureNameCell);
                AddBold(document, structureNameCell);


                int currentPosX = POSITION_START_X;
                int currentPosY = POSITION_START_Y;

                bool curationMode = false;
                bool headingMode = false;
                

                Position hPosition = headPositions.FirstOrDefault();
                if (hPosition != null)
                {
                    StructureBlock headPosition = new StructureBlock(currentPosX, currentPosY, hPosition, document, rows, repository, positiontypesLocal, positioncategoriesLocal, mergeCells, workbookPart, date);
                    if (repository.IsSignedAndCreated(hPosition, date))
                    {
                        count += hPosition.Partval;
                        AddToCategories(categories, hPosition);
                    } else if (repository.IsSigned(hPosition, date))
                    {
                        countFuture += hPosition.Partval;
                        AddToCategories(categoriesFuture, hPosition);
                    }
                    

                    currentPosX = headPosition.Xstart;
                    currentPosY = headPosition.Yend + 2;
                }
                

                List<int> orderCuration = new List<int>();
                Dictionary<int, List<int>> curations = new Dictionary<int, List<int>>();
                Dictionary<int, List<Structure>> sortedStructures = new Dictionary<int, List<Structure>>();
                if (headPositions.Count > 1)
                {
                    foreach (Position position in headPositions.Skip(1))
                    {
                        StructureBlock sb = new StructureBlock(currentPosX, currentPosY, position, document, rows, repository, positiontypesLocal, positioncategoriesLocal, mergeCells, workbookPart, date);
                        //AddToCategories(categories, position);
                        //count += position.Partval;
                        if (repository.IsSignedAndCreated(position, date))
                        {
                            count += position.Partval;
                            AddToCategories(categories, position);
                        }
                        else if (repository.IsSigned(position, date))
                        {
                            countFuture += position.Partval;
                            AddToCategories(categoriesFuture, position);
                        }

                        if (!headingMode && position.Curator > 0)
                        {
                            curationMode = true;
                            orderCuration.Add(position.Id);
                            foreach (int i in position.Curatorlist.Split(',').Select(int.Parse).ToArray())
                            {
                                if (!curations.ContainsKey(position.Id))
                                {
                                    curations.Add(position.Id, new List<int>());
                                }
                                curations[position.Id].Add(i);
                            }
                        }
                        else if (!curationMode && position.Head > 0)
                        {
                            headingMode = true;
                            orderCuration.Add(position.Id);
                        }
                        else
                        {
                            orderCuration.Add(position.Id);
                        }


                        currentPosX = sb.Xend + 2;
                        currentPosY = sb.Ystart;
                    }
                }
                

                int STRUCTURE_Y_TOP = currentPosY + 4;
                int STRUCTURE_Y_LOWEST = STRUCTURE_Y_TOP + 50;

                currentPosX = POSITION_START_X;
                currentPosY = STRUCTURE_Y_TOP;

                List<int> usedStructures = new List<int>();

                if (curationMode)
                {
                    foreach (int curatorid in orderCuration)
                    {
                        foreach (Structure str in structures)
                        {
                            Position curator = repository.GetCuration(str.Id, date);
                            if (curations.ContainsKey(curatorid) && curator != null && curator.Id == curatorid)
                            //if (curations.ContainsKey(curatorid) && curations[curatorid].Contains(str.Id))
                            {
                                StructureBlock structureBlock = new StructureBlock(currentPosX, currentPosY, str.Id, user, document, rows, repository, positiontypesLocal, positioncategoriesLocal, mergeCells, workbookPart, heading, headPositions);
                                count += structureBlock.Count;
                                AddToCategories(categories, structureBlock);
                                currentPosY = structureBlock.Yend + 2;
                                usedStructures.Add(str.Id);
                            }
                        }
                        currentPosX += StructureBlock.BLOCK_WIDTH + 1;
                        currentPosY = STRUCTURE_Y_TOP;
                    }
                    
                } else if (headingMode)
                {
                    foreach (int curatorid in orderCuration)
                    {
                        foreach (Structure str in structures)
                        {
                            Position head = repository.GetHead(str.Id, date);
                            if (head != null && head.Id == curatorid)
                            //if (str.Head == curatorid)
                            {
                                StructureBlock structureBlock = new StructureBlock(currentPosX, currentPosY, str.Id, user, document, rows, repository, positiontypesLocal, positioncategoriesLocal, mergeCells, workbookPart, heading, headPositions);
                                count += structureBlock.Count;
                                AddToCategories(categories, structureBlock);
                                currentPosY = structureBlock.Yend + 2;
                                usedStructures.Add(str.Id);
                            }
                        }
                        currentPosX += StructureBlock.BLOCK_WIDTH + 1;
                        currentPosY = STRUCTURE_Y_TOP;
                    }
                    
                }

                structures = structures.Where(s => !usedStructures.Contains(s.Id));

                int y_alt = currentPosY;
                foreach (Structure str in structures)
                {
                    StructureBlock structureBlock = new StructureBlock(currentPosX, currentPosY, str.Id, user, document, rows, repository, positiontypesLocal, positioncategoriesLocal, mergeCells, workbookPart, heading, headPositions);
                    count += structureBlock.Count;
                    AddToCategories(categories, structureBlock);

                    currentPosY = structureBlock.Yend + 2;
                    y_alt = structureBlock.Yend + 2;
                    if (currentPosY > STRUCTURE_Y_LOWEST)
                    {
                        currentPosY = STRUCTURE_Y_TOP;
                        currentPosX += StructureBlock.BLOCK_WIDTH + 1;
                    }
                }
                currentPosY = y_alt;
                string counttext = "По штату: "; 

                Cell countCell = CreateTextCell(ColumnLetter(currentPosX),
                    (uint)currentPosY, counttext);
                rows[(uint)currentPosY].AppendChild(countCell);

                Cell countCell2 = CreateTextCell(ColumnLetter(currentPosX + 1),
                    (uint)currentPosY, count.ToString());
                rows[(uint)currentPosY].AppendChild(countCell2);

                Cell countCell3 = CreateTextCell(ColumnLetter(currentPosX + 2),
                    (uint)currentPosY, " ед.");
                rows[(uint)currentPosY].AppendChild(countCell3);

                currentPosY += 2;
                Cell fromCell = CreateTextCell(ColumnLetter(currentPosX),
                    (uint)currentPosY, "Из них:");
                rows[(uint)currentPosY].AppendChild(fromCell);
                List<KeyValuePair<int, PrintCategoryInfo>> sortedCategories = new List<KeyValuePair<int, PrintCategoryInfo>>();
                foreach (KeyValuePair<int, PrintCategoryInfo> entry in new SortedDictionary<int, PrintCategoryInfo>(categories))
                {
                    if (positioncategoriesLocal[entry.Key].Officer > 0)
                    {
                        sortedCategories.Insert(0, entry);
                    } else
                    {
                        sortedCategories.Add(entry);
                    }
                }
                foreach (KeyValuePair<int, PrintCategoryInfo> entry in sortedCategories)
                {
                    currentPosY += 1;
                    string categorytext = " - " + positioncategoriesLocal[entry.Key].Name;

                    Cell categoryCellNumber = CreateTextCell(ColumnLetter(currentPosX),
                        (uint)currentPosY, entry.Value.Count.ToString());
                    rows[(uint)currentPosY].AppendChild(categoryCellNumber);

                    Cell categoryCell = CreateTextCell(ColumnLetter(currentPosX + 1),
                        (uint)currentPosY, categorytext);
                    rows[(uint)currentPosY].AppendChild(categoryCell);
                }

                worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<DocumentFormat.OpenXml.Spreadsheet.SheetData>().First());


                workbookPart.Workbook.Save();
            }
            mem.Position = 0;
            return mem;
        }



        private Cell CreateTextCell(string header, UInt32 index,
    string text)
        {
            if (text == null)
            {
                text = "";
            }
            Cell cell = null;

            if (int.TryParse(text, out int n) || double.TryParse(text, out double x))
            {
                cell = new Cell
                {
                    DataType = CellValues.Number,
                    CellReference = header + index
                };
                cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                //cell.CellValue = new CellValue(text);
                cell.CellValue = new CellValue(text.Replace(',', '.'));
            }
            else
            {
                cell = new Cell
                {
                    DataType = CellValues.InlineString,
                    CellReference = header + index
                };
                var istring = new InlineString();
                var t = new Text { Text = text };
                istring.AppendChild(t);
                cell.AppendChild(istring);
            }
            return cell;
        }

        private string ColumnLetter(int intCol)
        {
            intCol = intCol - 1;
            var intFirstLetter = ((intCol) / 676) + 64;
            var intSecondLetter = ((intCol % 676) / 26) + 64;
            var intThirdLetter = (intCol % 26) + 65;

            var firstLetter = (intFirstLetter > 64)
                ? (char)intFirstLetter : ' ';
            var secondLetter = (intSecondLetter > 64)
                ? (char)intSecondLetter : ' ';
            var thirdLetter = (char)intThirdLetter;

            return string.Concat(firstLetter, secondLetter,
                thirdLetter).Trim();
        }

        private Cell GetCell(string columnName, uint rowIndex, DocumentFormat.OpenXml.Spreadsheet.SheetData sheetData)
        {
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                    {
                        refCell = cell;
                        break;
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);
                return newCell;
            }
        }

        public void AddBold(SpreadsheetDocument document, Cell c)
        {
            Fonts fs = AddFont(document.WorkbookPart.WorkbookStylesPart.Stylesheet.Fonts);
            AddCellFormat(document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats, document.WorkbookPart.WorkbookStylesPart.Stylesheet.Fonts);
            c.StyleIndex = (UInt32)(document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats.Elements<CellFormat>().Count() - 1);
        }

        public Fonts AddFont(Fonts fs)
        {
            Font font2 = new Font();
            Bold bold1 = new Bold();
            FontSize fontSize2 = new FontSize() { Val = 11D };
            Color color2 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName2 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering2 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme2 = new FontScheme() { Val = FontSchemeValues.Minor };

            font2.Append(bold1);
            font2.Append(fontSize2);
            font2.Append(color2);
            font2.Append(fontName2);
            font2.Append(fontFamilyNumbering2);
            font2.Append(fontScheme2);

            fs.Append(font2);
            return fs;
        }

        public void AddCellFormat(CellFormats cf, Fonts fs)
        {
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = 0, FontId = (UInt32)(fs.Elements<Font>().Count() - 1), FillId = 0, BorderId = 0, FormatId = 0, ApplyFill = true };
            cf.Append(cellFormat2);
        }

        private static Stylesheet CreateStylesheet()
        {
            Stylesheet stylesheet1 = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            Fonts fonts1 = new Fonts() { Count = (UInt32Value)1U, KnownFonts = true };

            Font font1 = new Font();
            FontSize fontSize1 = new FontSize() { Val = 11D };
            Color color1 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

            font1.Append(fontSize1);
            font1.Append(color1);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);
            font1.Append(fontScheme1);

            fonts1.Append(font1);

            Fills fills1 = new Fills() { Count = (UInt32Value)5U };

            // FillId = 0
            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };
            fill1.Append(patternFill1);

            // FillId = 1
            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };
            fill2.Append(patternFill2);

            // FillId = 2,RED
            Fill fill3 = new Fill();
            PatternFill patternFill3 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor1 = new ForegroundColor() { Rgb = "FFFF0000" };
            BackgroundColor backgroundColor1 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill3.Append(foregroundColor1);
            patternFill3.Append(backgroundColor1);
            fill3.Append(patternFill3);

            // FillId = 3,BLUE
            Fill fill4 = new Fill();
            PatternFill patternFill4 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor2 = new ForegroundColor() { Rgb = "FF0070C0" };
            BackgroundColor backgroundColor2 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill4.Append(foregroundColor2);
            patternFill4.Append(backgroundColor2);
            fill4.Append(patternFill4);

            // FillId = 4,YELLO
            Fill fill5 = new Fill();
            PatternFill patternFill5 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor3 = new ForegroundColor() { Rgb = "FFFFFF00" };
            BackgroundColor backgroundColor3 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill5.Append(foregroundColor3);
            patternFill5.Append(backgroundColor3);
            fill5.Append(patternFill5);

            fills1.Append(fill1);
            fills1.Append(fill2);
            fills1.Append(fill3);
            fills1.Append(fill4);
            fills1.Append(fill5);

            Borders borders1 = new Borders() { Count = (UInt32Value)1U };

            Border border1 = new Border();
            LeftBorder leftBorder1 = new LeftBorder();
            RightBorder rightBorder1 = new RightBorder();
            TopBorder topBorder1 = new TopBorder();
            BottomBorder bottomBorder1 = new BottomBorder();
            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder1);
            border1.Append(rightBorder1);
            border1.Append(topBorder1);
            border1.Append(bottomBorder1);
            border1.Append(diagonalBorder1);

            borders1.Append(border1);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)1U };
            CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };

            cellStyleFormats1.Append(cellFormat1);

            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)4U };
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };
            CellFormat cellFormat3 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat4 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)3U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat5 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)4U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };

            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat3);
            cellFormats1.Append(cellFormat4);
            cellFormats1.Append(cellFormat5);

            CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
            CellStyle cellStyle1 = new CellStyle() { Name = "Normal", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

            cellStyles1.Append(cellStyle1);
            DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)0U };
            TableStyles tableStyles1 = new TableStyles() { Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium2", DefaultPivotStyle = "PivotStyleMedium9" };

            StylesheetExtensionList stylesheetExtensionList1 = new StylesheetExtensionList();

            StylesheetExtension stylesheetExtension1 = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            X14.SlicerStyles slicerStyles1 = new X14.SlicerStyles() { DefaultSlicerStyle = "SlicerStyleLight1" };

            stylesheetExtension1.Append(slicerStyles1);

            stylesheetExtensionList1.Append(stylesheetExtension1);

            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);
            stylesheet1.Append(stylesheetExtensionList1);
            return stylesheet1;
        }

        private byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private byte[] ReadFullyStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private void AddToCategories(Dictionary<int, PrintCategoryInfo> categories, Position position)
        {
            if (!categories.ContainsKey(position.Positioncategory))
            {
                categories.Add(position.Positioncategory, new PrintCategoryInfo());
            }
            categories[position.Positioncategory].Count += position.Partval;
            if (!categories[position.Positioncategory].Sofs.ContainsKey(position.Sourceoffinancing))
            {
                categories[position.Positioncategory].Sofs.Add(position.Sourceoffinancing, 0);
            }
            categories[position.Positioncategory].Sofs[position.Sourceoffinancing] += position.Partval;
        }

        private void AddToCategories(Dictionary<int, PrintCategoryInfo> categories, StructureBlock structureBlock)
        {
            Dictionary<int, PrintCategoryInfo> blockCategories = structureBlock.Categories;
            foreach (KeyValuePair<int, PrintCategoryInfo> entry in blockCategories)
            {
                if (!categories.ContainsKey(entry.Key))
                {
                    categories.Add(entry.Key, new PrintCategoryInfo());
                }
                categories[entry.Key].Count += entry.Value.Count;
                foreach (KeyValuePair<int, double> sof in entry.Value.Sofs)
                {
                    if (!categories[entry.Key].Sofs.ContainsKey(sof.Key))
                    {
                        categories[entry.Key].Sofs.Add(sof.Key, 0);
                    }
                    categories[entry.Key].Sofs[sof.Key] += sof.Value;
                }
            }

            
            
        }

    }
}