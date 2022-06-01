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
using PersonnelManagement.Controllers;
using PersonnelManagement.Models;
using PersonnelManagement.Utils;
using PersonnelManagement.Services;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;

namespace PersonnelManagement.Models
{
    public class StructureBlock
    {

        public const int BLOCK_WIDTH = 8;

        public const int POSITION_HEIGHT = 3;

        public const int STR_POS_HEIGHT = 1;
        public const int STR_STR_HEIGHT = 3;

        public int Type { get; set; } = 0; // 0 for structures, 1 for positions (headers)
        public int Xstart { get; set; } = 0;
        public int Xend { get; set; } = 0;
        public int Ystart { get; set; } = 0;
        public int Yend { get; set; }

        /**
         * Structures count
         */
        public double Count { get; set; }
        public Dictionary<int, PrintCategoryInfo> Categories { get; set; } = new Dictionary<int, PrintCategoryInfo>();

        public Dictionary<string, Cell> Cells { get; set; } = new Dictionary<string, Cell>();

        private WorkbookPart workbookPart;

        /**
         * POSITION BLOCK
         */
        public StructureBlock(int xstart, int ystart, Position position, SpreadsheetDocument document, Dictionary<UInt32, Row> rows, Repository repository, Dictionary<int, Positiontype> positiontypesLocal, Dictionary<int, Positioncategory> positioncategoriesLocal, MergeCells mergeCells, WorkbookPart workbookPart, DateTime date, bool heading = false, List<Position> headPositions = null)
        {
            Xstart = xstart;
            Ystart = ystart;
            Xend = xstart + BLOCK_WIDTH - 1;
            Yend = ystart + POSITION_HEIGHT - 1;
            Type = 1;
            this.workbookPart = workbookPart;
            string printName = positiontypesLocal[position.Positiontype].Name;
            if (repository.IsSignedAndWillCreated(position, date))
            {
                printName += " (вводится c " + repository.TimeWillCreated(position, date).ToShortDateString() + ")"; 
            }
            Cell nameCell = CreateTextCell(ColumnLetter(Xstart),
                    (uint)Ystart, printName);
            rows[(uint)Ystart].AppendChild(nameCell);
            Format(document, nameCell, true, true, true, 3, 3, 3, 3);
            Cells.Add(GetName(Xstart, Ystart), nameCell);
            

            for (int i = Xstart; i <= Xend; i++)
            {
                for (int j = Ystart; j <= Yend; j++)
                {
                    if (!(i == Xstart && j == Ystart) && !(i == Xend && j == Ystart))
                    //if (!Cells.ContainsKey(GetName(i, j)))
                    {
                        Cell cell = CreateTextCell(ColumnLetter(i),
                            (uint)j, "");
                        rows[(uint)j].AppendChild(cell);
                        if (!(i == Xend))
                        {
                            Format(document, cell, true, true, true, 3, 0, 3, 3);
                        } else
                        {
                            if (!(j == Yend))
                            {
                                Format(document, cell, true, true, true, 0, 3, 0, 0);
                            } else
                            {
                                Format(document, cell, true, true, true, 0, 3, 3, 0);
                            }
                            
                        }
                        
                    }
                }
            }

            Cell countCell = CreateTextCell(ColumnLetter(Xend),
                    (uint)Ystart, "1");
            rows[(uint)Ystart].AppendChild(countCell);
            Cells.Add(GetName(Xend, Ystart), countCell);
            Format(document, countCell, true, true, true, 3, 3, 2, 2);

            mergeCells.Append(new MergeCell() { Reference = new StringValue(ColumnLetter(Xstart) + Ystart.ToString() + ":" + ColumnLetter(Xend - 1) + Yend.ToString()) });
        }

        /**
         * STRUCTURES BLOCK
         */
        public StructureBlock(int xstart, int ystart, int structureID, User user,  SpreadsheetDocument document, Dictionary<UInt32, Row> rows, Repository repository, Dictionary<int, Positiontype> positiontypesLocal, Dictionary<int, Positioncategory> positioncategoriesLocal, MergeCells mergeCells, WorkbookPart workbookPart, bool heading = false, List<Position> headPositions = null)
        {
            DateTime date = user.Date.GetValueOrDefault();
            Xstart = xstart;
            Ystart = ystart;
            Xend = xstart + BLOCK_WIDTH - 1;
            Type = 0;

            this.workbookPart = workbookPart;

            int x = Xstart;
            int y = Ystart;

            Dictionary<int, Sourceoffinancing> sofsLocal = repository.SourceoffinancingsLocal();
            Dictionary<int, PrintEnumElement> closestStructuresByLevels = new Dictionary<int, PrintEnumElement>();

            Structure structure = repository.StructuresLocal().GetValue(structureID);
            //IEnumerable<>      DUBLICATING
            Structure actualStructure = repository.GetActualStructureInfo(structureID, user.Date.GetValueOrDefault());
            if (!repository.IsSignedAndCreated(actualStructure, user.Date.GetValueOrDefault()))
            {
                //return null;
            }
            List<int> structureids = repository.GetStructuresSiblings(structure.Id, null, date);
            //List<int> structureids = repository.GetStructuresSiblingsWithSameOrNullType(structure.Id);
            //IEnumerable<StructureExpanded> structures = repository.GetAllowedStructuresToReadDebug(user, structureids.ToArray(), structure.Id);
            IEnumerable<StructureExpanded> structures = null;
            if (structureids.Count > 0)
            {
                //structures = repository.GetAllowedStructuresToRead(user, structureids.ToArray(), structureids.First());
                structures = repository.GetAllowedStructuresToReadStructureBlock(user, structureids.ToArray(), structureids.First());
            }
            else
            {
                //structures = repository.GetAllowedStructuresToRead(user, structureids.ToArray(), structure.Id);
                structures = repository.GetAllowedStructuresToReadStructureBlock(user, structureids.ToArray(), structure.Id);
                
            }

            
            List<PrintEnumElement> elements = new List<PrintEnumElement>();
            //StructureExpanded previousElementStructure = null;
            PrintEnumElement previousElement = null;
            structures = structures.OrderBy(s => s.Order).ToList();
            //structures = structures.Where(s => repository.IsSignedAndCreated(s, user.Date.GetValueOrDefault())).OrderBy(s => s.Order).ToList(); --- ??

            List<int> headPositionsIDs = null;
            if (heading)
            {
                if (headPositions == null)
                {
                    headPositions = new List<Position>();
                }
                headPositionsIDs = headPositions.Select(s => s.Id).ToList();
            }

            foreach (StructureExpanded structureExpanded in structures)
            {
                int originalId = repository.GetStructureOriginalId(structureExpanded.Id); ;
                //int level = structureExpanded.Level; - from 0...
                PrintEnumElement peeDom = new PrintEnumElement();
                peeDom.InEnum = true;
                peeDom.Position = null;
                peeDom.Structure = repository.GetActualStructureInfo(structureExpanded.Id, user.Date.GetValueOrDefault());
                peeDom.Name = peeDom.Structure.Name;
                elements.Add(peeDom);

                int originID = peeDom.Structure.Id;
                if (peeDom.Structure.Changeorigin > 0)
                {
                    originID = peeDom.Structure.Changeorigin;
                }
               
                IEnumerable<Models.Position> positions = repository.GetPositions(originalId, user.Date.Value, heading);
                if (heading)
                {
                    //positions = positions.Where(p => (p.Head == 0 || p.Headid == structureExpanded.Id || p.Headid == originalId) && !headPositionsIDs.Contains(p.Id));
                    positions = positions.Where(p => (p.Head == 0 || repository.IsHeading(p.Id, structureExpanded.Id, date)) && !headPositionsIDs.Contains(p.Id));
                }
                //positions = positions.Where(p => repository.IsSignedAndCreated(p, user.Date.GetValueOrDefault()));
                positions = positions.Where(p => repository.IsSigned(p, user.Date.GetValueOrDefault()));
                Dictionary<long, PrintEnumElement> pees = new Dictionary<long, PrintEnumElement>();



                double count = 0;
                double countFuture = 0;
                foreach (Models.Position position in positions)
                {
                    if (repository.IsSignedAndDeleted(position, date))
                    {
                        continue; // 
                    }
                    bool signedAndCreated = repository.IsSignedAndCreated(position, date);
                    bool signedAndWillCreated = repository.IsSignedAndWillCreated(position, date);
                    

                    if (position.Positioncategory > 0)
                    {
                        if (!Categories.ContainsKey(position.Positioncategory))
                        {
                            Categories.Add(position.Positioncategory, new PrintCategoryInfo());
                        }
                        Categories[position.Positioncategory].Count += position.Partval;
                        if (!Categories[position.Positioncategory].Sofs.ContainsKey(position.Sourceoffinancing))
                        {
                            Categories[position.Positioncategory].Sofs.Add(position.Sourceoffinancing, 0);
                        }
                        Categories[position.Positioncategory].Sofs[position.Sourceoffinancing] += position.Partval;
                    }
                    //if (!pees.ContainsKey(position.Positiontype))
                    DateTime dateCreated = repository.TimeWillCreated(position, date);
                    if (!pees.ContainsKey(PositionUtils.CalculateHash(position, dateCreated)))
                    {
                        PrintEnumElement pee = new PrintEnumElement();
                        pee.InEnum = true;
                        pee.Position = position;
                        pee.Structure = null;
                        if (signedAndCreated)
                        {
                            pee.Count = position.Partval;
                        } else if (signedAndWillCreated)
                        {
                            pee.Count = 0;
                            pee.Prolonged = true;
                            pee.ProlongDate = dateCreated;
                        }
                        
                        pee.Name = positiontypesLocal[position.Positiontype].Name;
                        if (!pee.Sofs.ContainsKey(position.Sourceoffinancing))
                        {
                            pee.Sofs.Add(position.Sourceoffinancing, 0);
                        }
                        
                        if (signedAndCreated)
                        {
                            pee.Sofs[position.Sourceoffinancing] += position.Partval;
                        } else if (signedAndWillCreated)
                        {
                            if (!pee.SofsFuture.ContainsKey(position.Sourceoffinancing))
                            {
                                pee.SofsFuture.Add(position.Sourceoffinancing, 0);
                            }
                            pee.SofsFuture[position.Sourceoffinancing] += position.Partval;
                        }
                        

                        //pees.Add(position.Positiontype, pee);
                        pees.Add(PositionUtils.CalculateHash(position, dateCreated), pee);
                        

                        elements.Add(pee);
                    }
                    else
                    {

                        if (!pees[PositionUtils.CalculateHash(position, dateCreated)].Sofs.ContainsKey(position.Sourceoffinancing))
                        {
                            pees[PositionUtils.CalculateHash(position, dateCreated)].Sofs.Add(position.Sourceoffinancing, 0);
                        }
                        if (!pees[PositionUtils.CalculateHash(position, dateCreated)].SofsFuture.ContainsKey(position.Sourceoffinancing))
                        {
                            pees[PositionUtils.CalculateHash(position, dateCreated)].SofsFuture.Add(position.Sourceoffinancing, 0);
                        }
                        if (signedAndCreated)
                        {
                            pees[PositionUtils.CalculateHash(position, dateCreated)].Sofs[position.Sourceoffinancing] += position.Partval;
                            pees[PositionUtils.CalculateHash(position, dateCreated)].Count += position.Partval;
                        } else
                        {
                            pees[PositionUtils.CalculateHash(position, dateCreated)].SofsFuture[position.Sourceoffinancing] += position.Partval;
                        }
                        
                    }
                    if (signedAndCreated)
                    {
                        count += position.Partval;
                    }
                    
                }

                //int count = positions.Count();
                peeDom.PureCount = count;
                peeDom.CountByLevels.Add(count);
                Count += count;
                peeDom.Level = structureExpanded.Level;

                if (previousElement != null && peeDom.Level > previousElement.Level)
                {
                    peeDom.Level = previousElement.Level + 1; // Чтобы различие в уровне было только на 1, избежать случайных скачков в уровнях.
                }

                /**
                 * Add closest structure by level
                 */
                if (!closestStructuresByLevels.ContainsKey(peeDom.Level))
                {
                    closestStructuresByLevels.Add(peeDom.Level, peeDom);
                }


                


                if (previousElement != null && peeDom.Level > previousElement.Level)
                {
                    foreach (double countByLevel in previousElement.CountByLevels)
                    {
                        //peeDom.CountByLevels.Add(countByLevel + positions.Count()); // 
                        peeDom.CountByLevels.Add(countByLevel + PositionsCount(positions));
                    }

                    for (int i = 0; i < peeDom.Level; i++)
                    {
                        if (closestStructuresByLevels.ContainsKey(i))
                        {
                            closestStructuresByLevels[i].CountByLevelsWithChildren += peeDom.CountByLevels.First(); // В родителей докидываем численность детей.
                        }
                    }
                }
                else if (previousElement != null && peeDom.Level == previousElement.Level)
                {
                    int indexer = 0;
                    foreach (double countByLevel in previousElement.CountByLevels)
                    {
                        if (indexer > 0)
                        {
                            //peeDom.CountByLevels.Add(countByLevel + positions.Count());
                            peeDom.CountByLevels.Add(countByLevel + PositionsCount(positions));
                        }

                        indexer++;
                    }
                    //peeDom.CountByLevelsWithChildren += closestStructuresByLevels[peeDom.Level].CountByLevelsWithChildren;
                    for (int i = 0; i < peeDom.Level; i++)
                    {
                        if (closestStructuresByLevels.ContainsKey(i))
                        {
                            closestStructuresByLevels[i].CountByLevelsWithChildren += peeDom.CountByLevels.First(); // В родителей докидываем численность детей.
                        }
                        
                    }
                    closestStructuresByLevels[peeDom.Level] = peeDom;

                }
                else if (previousElement != null && peeDom.Level < previousElement.Level)
                {
                    // nullify closest structures that higher.
                    IEnumerable<int> higher = closestStructuresByLevels.Keys.Where(k => k > peeDom.Level).ToList();
                    foreach (int i in higher)
                    {
                        closestStructuresByLevels.Remove(i);
                    }
                    int indexer = 0;
                    if (closestStructuresByLevels.ContainsKey(peeDom.Level))
                    {
                        foreach (double countByLevel in closestStructuresByLevels[peeDom.Level].CountByLevels)
                        {
                            if (indexer > 0)
                            {
                                //peeDom.CountByLevels.Add(countByLevel + positions.Count() + closestStructuresByLevels[peeDom.Level].CountByLevelsWithChildren);

                                peeDom.CountByLevels.Add(countByLevel + PositionsCount(positions) + closestStructuresByLevels[peeDom.Level].CountByLevelsWithChildren);
                            }

                            indexer++;
                        }
                    }
                    for (int i = 0; i < peeDom.Level; i++)
                    {
                        if (closestStructuresByLevels.ContainsKey(i))
                        {
                            closestStructuresByLevels[i].CountByLevelsWithChildren += peeDom.CountByLevels.First(); // В родителей докидываем численность детей.
                        }
                    }

                    closestStructuresByLevels[peeDom.Level] = peeDom;

                }


                if (previousElement != null && peeDom.Level < previousElement.Level)
                {
                    previousElement.LevelDifference = previousElement.Level - peeDom.Level; // Хуйня тут. 
                }

                previousElement = peeDom;
            }


            if (previousElement != null)
            {
                previousElement.LevelDifference = previousElement.Level - structures.First().Level;
            }


            int index = 0;
            /**
             * Generating
             */
            foreach (PrintEnumElement element in elements)
            {
                int x_index = 1;
                x = Xstart;
                y += 1;

                string name = "1";
                if (element.Position != null)
                {
                    name = positiontypesLocal[element.Position.Positiontype].Name;
                    if (positioncategoriesLocal.Values.Where(pc => pc.Officer == 0).Select(s=>s.Id).Contains(element.Position.Positioncategory))
                    {
                        //name += " (" + positioncategoriesLocal[element.Position.Positioncategory].Name + ")"; - Не пишем в скобках старший начальствующий состав и т.п.
                    }
                    if (element.Position.Cap.GetValueOrDefault() > 0)
                    {
                        string rankName = repository.RanksLocal().GetValue(element.Position.Cap.GetValueOrDefault()).Name;
                        if (rankName != null)
                        {
                            name += " (" + rankName + ")";
                        }
                    }
                    if (element.Position.Replacedbycivil > 0)
                    {
                        name += " (Может замещаться)";
                    }
                    if (element.Prolonged)
                    {
                        string futureCount = "";
                        if (element.SofsFuture.Count > 0)
                        {
                            foreach (KeyValuePair<int, double> sof in element.SofsFuture)
                            {
                                futureCount += ShortifySOF(sofsLocal[sof.Key].Name) + " " + sof.Value + ",";
                            }
                            if (futureCount.Length > 0)
                            {
                                futureCount = futureCount.Remove(futureCount.Length - 1);
                                futureCount += " ";
                            }
                        }
                        name += " (" + futureCount + "с " + element.ProlongDate.ToShortDateString() + ")";
                    }
                } else if (element.Structure != null)
                {
                    name = element.Structure.Name;
                }

                /**
                 * POSITIONS
                 */
                if (element.Position != null)
                {
                    Cell nameCell = CreateTextCell(ColumnLetter(x),
                    (uint)y, name);
                    rows[(uint)y].AppendChild(nameCell);
                    Cells.Add(GetName(x, y), nameCell);
                    Format(document, nameCell, false, false, true, 2, 2, 2, 3);
                    x_index++;
                    x++;
                    while (x_index < BLOCK_WIDTH)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(x),
                        (uint)y, "");
                        rows[(uint)y].AppendChild(cell);
                        Cells.Add(GetName(x, y), cell);
                        Format(document, cell, true, true, true, 2, 2, 2, 2);
                        x_index++;
                        x++;
                    }
                    string count = "";
                    foreach (KeyValuePair<int, double> sof in element.Sofs)
                    {
                        count += ShortifySOF(sofsLocal[sof.Key].Name) + " " + sof.Value + ","; 
                    }
                    if (count.Length > 0)
                    {
                        count = count.Remove(count.Length - 1);
                    }
                    //double count = element.Count;
                    
                    Cell countCell = CreateTextCell(ColumnLetter(x),
                        (uint)y, count.ToString());
                    rows[(uint)y].AppendChild(countCell);
                    Cells.Add(GetName(x, y), countCell);
                    Format(document, countCell, true, true, true, 2, 3, 2, 2);
                    mergeCells.Append(new MergeCell() { Reference = new StringValue(ColumnLetter(Xstart) + y.ToString() + ":" + ColumnLetter(Xend - 1) + y.ToString()) });
                } else
                /**
                 * STRUCTURES
                 */
                {
                    if (index == 0)
                    {
                        Cell nameCell = CreateTextCell(ColumnLetter(x),
                    (uint)y, name);
                        rows[(uint)y].AppendChild(nameCell);
                        Cells.Add(GetName(x, y), nameCell);
                        Format(document, nameCell, true, true, true, 3,3, 3, 3);
                        x_index++;
                        x++;

                        while (x_index < BLOCK_WIDTH)
                        {
                            Cell cell = CreateTextCell(ColumnLetter(x),
                            (uint)y, "");
                            rows[(uint)y].AppendChild(cell);
                            Cells.Add(GetName(x, y), cell);
                            Format(document, cell, true, true, true, 3, 3, 3, 3);
                            x_index++;
                            x++;
                        }
                        
                        double overallCount = element.CountByLevels[0] + element.CountByLevelsWithChildren;
                        Cell overallCountCell = CreateTextCell(ColumnLetter(x),
                            (uint)y, overallCount.ToString());
                        rows[(uint)y].AppendChild(overallCountCell);
                        Cells.Add(GetName(x, y), overallCountCell);
                        Format(document, overallCountCell, true, true, true, 3, 3, 3, 3);
                    } else
                    {
                        while (x_index <= BLOCK_WIDTH)
                        {
                            Cell cell = CreateTextCell(ColumnLetter(x),
                            (uint)y, "");
                            rows[(uint)y].AppendChild(cell);
                            Cells.Add(GetName(x, y), cell);
                            Format(document, cell, true, true, true, 2, 3, 2, 3);
                            x_index++;
                            x++;
                        }
                        mergeCells.Append(new MergeCell() { Reference = new StringValue(ColumnLetter(Xstart) + y.ToString() + ":" + ColumnLetter(Xend) + y.ToString()) });

                    }
                    x = Xstart;
                    x_index = 1;
                    y++;

                    /***
                     *  Second
                     */
                    if (index != 0)
                    {
                        Cell nameCell = CreateTextCell(ColumnLetter(x),
                    (uint)y, name);
                        rows[(uint)y].AppendChild(nameCell);
                        Cells.Add(GetName(x, y), nameCell);
                        Format(document, nameCell, true, false, true, 2, 2, 2, 3);
                        x_index++;
                        x++;
                    }
                    
                    while (x_index <= BLOCK_WIDTH)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(x),
                        (uint)y, "");
                        rows[(uint)y].AppendChild(cell);
                        Cells.Add(GetName(x, y), cell);
                        if (x_index != BLOCK_WIDTH)
                        {
                            Format(document, cell, true, true, true, 2, 0, 0, 3);
                        } else
                        {
                            Format(document, cell, true, true, true, 2, 3, 0, 0);
                        }
                        
                        x_index++;
                        x++;
                    }

                    x = Xstart;
                    x_index = 1;
                    y++;
                    while (x_index < BLOCK_WIDTH)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(x),
                        (uint)y, "");
                        rows[(uint)y].AppendChild(cell);
                        Cells.Add(GetName(x, y), cell);
                        Format(document, cell, true, true, true, 2, 2, 2, 3);
                        x_index++;
                        x++;
                    }

                    double count = element.Count;
                    if (element.Structure != null)
                    {
                        count = element.CountByLevels[0];
                    }
                    Cell countCell = CreateTextCell(ColumnLetter(x),
                        (uint)y, count.ToString());
                    rows[(uint)y].AppendChild(countCell);
                    Cells.Add(GetName(x, y), countCell);
                    Format(document, countCell, true, true, true, 2, 3, 2, 2);
                    if (index == 0)
                    {
                        mergeCells.Append(new MergeCell() { Reference = new StringValue(ColumnLetter(Xstart) + (y-2).ToString() + ":" + ColumnLetter(Xend - 1) + y.ToString()) });
                    } else
                    {
                        mergeCells.Append(new MergeCell() { Reference = new StringValue(ColumnLetter(Xstart) + (y-1).ToString() + ":" + ColumnLetter(Xend - 1) + y.ToString()) });
                    }
                    
                }



                index++;
            }

            Yend = y;
        }


            /**
             * Borderfat 0 - no, 1 - slim, 2 - medium, 3 - fat
             */
        public void Format(SpreadsheetDocument document, Cell c, bool center = false, bool bold = false, bool border = false, int bordertop = 0, int borderright = 0, int borderbottom = 0, int borderleft = 0)
        {
            Fonts fs = AddFont(document.WorkbookPart.WorkbookStylesPart.Stylesheet.Fonts);

            CellFormats cf = document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats;
            Alignment alignment = null;
            if (center)
            {
                alignment = new Alignment();
                alignment.Vertical = VerticalAlignmentValues.Center;
                alignment.Horizontal = HorizontalAlignmentValues.Center;
                alignment.WrapText = true;
            }
            

            UInt32 borderId = 0;
            if (border)
            {
                borderId = InsertBorder(workbookPart, GenerateBorder(bordertop, borderright, borderbottom, borderleft));
            }

            UInt32 fontId = 0;
            if (bold)
            {
                fontId = (UInt32)(fs.Elements<Font>().Count() - 1);
            }

            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = 0, FontId = fontId, FillId = 0, BorderId = borderId, FormatId = 0, ApplyFill = true, Alignment = alignment };
            cf.Append(cellFormat2);

            c.StyleIndex = (UInt32)(document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats.Elements<CellFormat>().Count() - 1);
        }

        public void AddCellFormatCenter(CellFormats cf, Fonts fs, bool center = false, bool bold = false)
        {
            
        }



        //private Cell CreateTextCell(string header, UInt32 index,
        //    string text)
        //{
        //    var cell = new Cell
        //    {
        //        DataType = CellValues.InlineString,
        //        CellReference = header + index
        //    };

        //    var istring = new InlineString();
        //    var t = new Text { Text = text };
        //    istring.AppendChild(t);

        //    cell.AppendChild(istring);
        //    return cell;
        //}

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

        public void AddBold(SpreadsheetDocument document, Cell c)
        {
            Fonts fs = AddFont(document.WorkbookPart.WorkbookStylesPart.Stylesheet.Fonts);
            AddCellFormat(document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats, document.WorkbookPart.WorkbookStylesPart.Stylesheet.Fonts);
            c.StyleIndex = (UInt32)(document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats.Elements<CellFormat>().Count() - 1);
        }

        public void AddCellFormat(CellFormats cf, Fonts fs)
        {
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = 0, FontId = (UInt32)(fs.Elements<Font>().Count() - 1), FillId = 0, BorderId = 0, FormatId = 0, ApplyFill = true };
            cf.Append(cellFormat2);
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

        

        public uint InsertBorder(WorkbookPart workbookPart, Border border)
        {
            Borders borders = workbookPart.WorkbookStylesPart.Stylesheet.Elements<Borders>().First();
            borders.Append(border);
            return (uint)borders.Count++;
        }

        public string GetName(int x, int y)
        {
            return ColumnLetter(x) + y.ToString();
        }

        public Border GenerateBorder(int bordertop, int borderright, int borderbottom, int borderleft)
        {
            Border border2 = new Border();

            if (borderleft > 0)
            {
                var leftBorderStyle = BorderStyleValues.Medium;
                if (borderleft == 1)
                {
                    leftBorderStyle = BorderStyleValues.Thin;
                }
                if (borderleft == 2)
                {
                    leftBorderStyle = BorderStyleValues.Medium;
                } 
                if (borderleft == 3)
                {
                    leftBorderStyle = BorderStyleValues.Thick;
                }
                LeftBorder leftBorder2 = new LeftBorder() { Style = leftBorderStyle };
                Color color1 = new Color() { Indexed = (UInt32Value)64U };
                leftBorder2.Append(color1);
                border2.Append(leftBorder2);
            }

            if (borderright > 0)
            {
                var rightBorderStyle = BorderStyleValues.Medium;
                if (borderright == 1)
                {
                    rightBorderStyle = BorderStyleValues.Thin;
                }
                if (borderright == 2)
                {
                    rightBorderStyle = BorderStyleValues.Medium;
                }
                if (borderright == 3)
                {
                    rightBorderStyle = BorderStyleValues.Thick;
                }
                RightBorder rightBorder2 = new RightBorder() { Style = rightBorderStyle };
                Color color2 = new Color() { Indexed = (UInt32Value)64U };
                rightBorder2.Append(color2);
                border2.Append(rightBorder2);
            }

            if (bordertop > 0)
            {
                var topBorderStyle = BorderStyleValues.Medium;
                if (bordertop == 1)
                {
                    topBorderStyle = BorderStyleValues.Thin;
                }
                if (bordertop == 2)
                {
                    topBorderStyle = BorderStyleValues.Medium;
                }
                if (bordertop == 3)
                {
                    topBorderStyle = BorderStyleValues.Thick;
                }
                TopBorder topBorder2 = new TopBorder() { Style = topBorderStyle };
                Color color3 = new Color() { Indexed = (UInt32Value)64U };
                topBorder2.Append(color3);
                border2.Append(topBorder2);
            }
            
            if (borderbottom > 0)
            {
                var bottomBorderStyle = BorderStyleValues.Medium;
                if (borderbottom == 1)
                {
                    bottomBorderStyle = BorderStyleValues.Thin;
                }
                if (borderbottom == 2)
                {
                    bottomBorderStyle = BorderStyleValues.Medium;
                }
                if (borderbottom == 3)
                {
                    bottomBorderStyle = BorderStyleValues.Thick;
                }
                BottomBorder bottomBorder2 = new BottomBorder() { Style = bottomBorderStyle };
                Color color4 = new Color() { Indexed = (UInt32Value)64U };
                border2.Append(bottomBorder2);
                bottomBorder2.Append(color4);
            }
            

            
            DiagonalBorder diagonalBorder2 = new DiagonalBorder();
            
            border2.Append(diagonalBorder2);

            return border2;
        }

        public double PositionsCount(IEnumerable<Models.Position> positions)
        {
            double count = 0;

            foreach (Models.Position pos in positions)
            {
                count += pos.Partval;
            }

            return count;
        }

        public string ShortifySOF(String sof)
        {
            String newString = "";
            sof.Split(' ').ToList().ForEach(i => newString += i[0]);
            newString = newString.ToUpper();
            return newString;
        }
    }
}
