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
using PersonnelManagement.Utils;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Pmrequest")]
    public class PmrequestController : Controller
    {
        private Repository repository;
        private pmContext context;

        public PmrequestController(Repository repository)
        {
            this.repository = repository;
            this.context = repository.GetContext();
        }

        // Is allowed to edit structures.
        // GET: api/Pmrequest
        [HttpGet]
        public bool IsAllowedToEditStructures()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                return repository.IsAllowedStructureToEdit(user);
            }
            return false;
        }

        private void setThread(string session_id, int PID = -1)
        {
            Session session = context.Session.First(r => r.Id == session_id);
            session.LastPidrequest = PID;
            context.Session.Update(session);
            context.SaveChanges();
        }

        private int getThread(string session_id)
        {
            var t = context.SaveChanges();
            Session session = context.Session.First(r => r.Id == session_id);
            return session.LastPidrequest;
        }

/*        [DllImport("Kernel32", EntryPoint = "GetCurrentThreadId", ExactSpelling = true)]
        public static extern Int32 GetCurrentWin32ThreadId();*/

        // POST: api/Pmrequest
        [HttpPost]
        public IActionResult PostPmrequest([FromBody] Pmrequest pmrequest)
        {
            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);

                //int[] structuresArray = pmrequest.Structures.Split(',').Select(int.Parse).ToArray();
                //structuresArray = FilterStructuresByReadability(user, structuresArray.ToList()).ToArray();

                bool hasAccess = IdentityService.CanReadStructure(sessionid, repository, user.Structure.GetValueOrDefault());
                if (!hasAccess)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
                }
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }
            IActionResult output = new ObjectResult("");
            
            Thread current = new Thread(() => { output = worker(new Repository(repository.GetContext()), pmrequest, user); });
            setThread(sessionid, current.ManagedThreadId);
            current.Start();
            //var h = GetNativeThreadId(current);
            current.Join();
            if(getThread(sessionid) == current.ManagedThreadId)
            {
                setThread(sessionid);
                return output;
            }
            return new ObjectResult("");

            foreach (ProcessThread t in Process.GetCurrentProcess().Threads)
            {
                System.Console.WriteLine(t.StartTime.ToString());
            }
            // current.Abort();
            // setThread(sessionid, current.)
            /**
             *  По должностям.
             */
            if (pmrequest.Type.Equals(Keys.PMREQUEST_TYPE_POSITION))
            {
                Pmrequest clone = Repository.Clone<Pmrequest>(pmrequest);
                clone.Ranks = "";
                pmrequest.AnyAltranks = false;
                List<Position> positionsAll = new List<Position>(repository.Positions);
                List<PmrequestPosition> positions = repository.GetPmrequestPositions(pmrequest, user);
                List<PmrequestPosition> positionsAllranks = null;
                
                if (pmrequest.AnyAltranks)
                {
                    positionsAllranks = repository.GetPmrequestPositions(clone, user);
                } else
                {
                    positionsAllranks = positions;
                }
                
                Pmresult pmresult = repository.GetPmresult(positionsAllranks, pmrequest);
                
                //return new ObjectResult(pmresult);
                MemoryStream mem = GenerateDocument(positions, pmrequest);
                if (Pmmemory.tempFiles.ContainsKey(user.Id))
                {
                    Pmmemory.tempFiles.TryRemove(user.Id, out _);
                }
                Pmmemory.tempFiles.TryAdd(user.Id, mem.ToArray());
                setThread(sessionid);
                return new ObjectResult(pmresult);
                // return File(mem, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");
            /**
             * По подразделениям.
             */
            } else if (pmrequest.Type.Equals(Keys.PMREQUEST_TYPE_STRUCTURE))
            {
                /**
                 * Простой вывод запроса по подразделениям
                 */
                if (pmrequest.Structurecountmode == 0)
                {
                    List<PmrequestStructure> structures = repository.GetPmrequestStructures(pmrequest, user);
                    Pmresult pmresult = new Pmresult();
                    pmresult.Count = structures.Count;
                    MemoryStream mem = GenerateDocument(structures, pmrequest);
                    if (Pmmemory.tempFiles.ContainsKey(user.Id))
                    {
                        Pmmemory.tempFiles.TryRemove(user.Id, out _);
                    }
                    Pmmemory.tempFiles.TryAdd(user.Id, mem.ToArray());
                    setThread(sessionid);
                    return new ObjectResult(pmresult);
                    //return File(GenerateDocument(structures, pmrequest), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");
                /**
                 * Сведения о штатной численности органов и подразделений по чрезвычайным ситуациям
                 */
                } else
                {
                    List<PmrequestStructure> structures = repository.GetPmrequestStructuresCount(pmrequest, user);
                    Pmresult pmresult = new Pmresult();
                    pmresult.Count = structures.Count;
                    MemoryStream mem = GenerateStructureCountDocument(structures, pmrequest, user);
                    if (Pmmemory.tempFiles.ContainsKey(user.Id))
                    {
                        Pmmemory.tempFiles.TryRemove(user.Id, out _);
                    }
                    Pmmemory.tempFiles.TryAdd(user.Id, mem.ToArray());
                    setThread(sessionid);
                    return new ObjectResult(pmresult);
                }

            } else if (pmrequest.Type.Equals(Keys.PMREQUEST_TYPE_LOAD))
            {
                setThread(sessionid);
                return File(Pmmemory.tempFiles[user.Id], "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");

            /**
             * Которые будут введены/удалены
             */
            } else if (pmrequest.Type.Equals(Keys.PMREQUEST_TYPE_ADDREMOVE))
            {
                if (pmrequest.Id < 0)
                {
                    pmrequest.Id = -pmrequest.Id;
                }

                
                Pmrequest clone = Repository.Clone<Pmrequest>(pmrequest);
                clone.Ranks = "";
                pmrequest.AnyAltranks = false;
                List<Position> positionsAll = new List<Position>(repository.Positions);
                List<PmrequestPosition> positions = repository.GetPmrequestPositionsAddRemove(pmrequest, user);
                List<PmrequestPosition> positionsAllranks = null;


                positionsAllranks = positions;

                //Pmresult pmresult = repository.GetPmresult(positionsAllranks, pmrequest);

                //return new ObjectResult(pmresult);
                pmrequest.Displaytreeseparately = 1;
                pmrequest.AddRemove = true;
                MemoryStream mem = GenerateDocument(positions, pmrequest);
                //if (Pmmemory.tempFiles.ContainsKey(user.Id))
                //{
                //    Pmmemory.tempFiles.TryRemove(user.Id, out _);
                //}
                //Pmmemory.tempFiles.TryAdd(user.Id, mem.ToArray());
                //return new ObjectResult(pmresult);
                //return File(Pmmemory.tempFiles[user.Id], "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");
                setThread(sessionid);
                return File(mem, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");
                /**
                 * По подразделениям.
                 */
            }
            setThread(sessionid);
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        [HttpPost("Stoped")]
        public IActionResult terminateThread()
        {
            setThread(Request.Cookies[Keys.COOKIES_SESSION]);
            //int thread_id = getThread(Request.Cookies[Keys.COOKIES_SESSION]);
            return new ObjectResult(Keys.ERROR_SHORT);
        }

        public static int GetNativeThreadId(Thread thread)
        {
            var f = typeof(Thread).GetField("DONT_USE_InternalThread",
                BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            var pInternalThread = (IntPtr)f.GetValue(thread);
            var nativeId = Marshal.ReadInt32(pInternalThread, (IntPtr.Size == 8) ? 0x022C : 0x0160); // found by analyzing the memory
            return nativeId;
        }

        public static void worker(ref Tuple<Repository, Pmrequest, User, IActionResult> input)
        {
            PmrequestController.worker(input.Item1, input.Item2, input.Item3);
        }

        public static IActionResult worker(Repository repository, Pmrequest pmrequest, User user)
        {
            IActionResult output = null;
            PmrequestController time = new PmrequestController(repository);
            if (pmrequest.Type.Equals(Keys.PMREQUEST_TYPE_POSITION))
            {
                Pmrequest clone = Repository.Clone<Pmrequest>(pmrequest);
                clone.Ranks = "";
                pmrequest.AnyAltranks = false;
                List<Position> positionsAll = new List<Position>(repository.Positions);
                List<PmrequestPosition> positions = repository.GetPmrequestPositions(pmrequest, user);
                List<PmrequestPosition> positionsAllranks = null;

                if (pmrequest.AnyAltranks)
                {
                    positionsAllranks = repository.GetPmrequestPositions(clone, user);
                }
                else
                {
                    positionsAllranks = positions;
                }

                Pmresult pmresult = repository.GetPmresult(positionsAllranks, pmrequest);

                MemoryStream mem = time.GenerateDocument(positions, pmrequest);
                if (Pmmemory.tempFiles.ContainsKey(user.Id))
                {
                    Pmmemory.tempFiles.TryRemove(user.Id, out _);
                }
                Pmmemory.tempFiles.TryAdd(user.Id, mem.ToArray());
                return new ObjectResult(pmresult);
            }
            else if (pmrequest.Type.Equals(Keys.PMREQUEST_TYPE_STRUCTURE))
            {
                /**
                 * Простой вывод запроса по подразделениям
                 */
                if (pmrequest.Structurecountmode == 0)
                {
                    List<PmrequestStructure> structures = repository.GetPmrequestStructures(pmrequest, user);
                    Pmresult pmresult = new Pmresult();
                    pmresult.Count = structures.Count;
                    MemoryStream mem = time.GenerateDocument(structures, pmrequest);
                    if (Pmmemory.tempFiles.ContainsKey(user.Id))
                    {
                        Pmmemory.tempFiles.TryRemove(user.Id, out _);
                    }
                    Pmmemory.tempFiles.TryAdd(user.Id, mem.ToArray());
                    return new ObjectResult(pmresult);
                }
                else
                {
                    List<PmrequestStructure> structures = repository.GetPmrequestStructuresCount(pmrequest, user);
                    Pmresult pmresult = new Pmresult();
                    pmresult.Count = structures.Count;
                    MemoryStream mem = time.GenerateStructureCountDocument(structures, pmrequest, user);
                    if (Pmmemory.tempFiles.ContainsKey(user.Id))
                    {
                        Pmmemory.tempFiles.TryRemove(user.Id, out _);
                    }
                    Pmmemory.tempFiles.TryAdd(user.Id, mem.ToArray());
                    return new ObjectResult(pmresult);
                }

            }
            else if (pmrequest.Type.Equals(Keys.PMREQUEST_TYPE_LOAD))
            {
                return time.File(Pmmemory.tempFiles[user.Id], "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");
            }
            else if (pmrequest.Type.Equals(Keys.PMREQUEST_TYPE_ADDREMOVE))
            {
                if (pmrequest.Id < 0)
                {
                    pmrequest.Id = -pmrequest.Id;
                }


                Pmrequest clone = Repository.Clone<Pmrequest>(pmrequest);
                clone.Ranks = "";
                pmrequest.AnyAltranks = false;
                List<Position> positionsAll = new List<Position>(repository.Positions);
                List<PmrequestPosition> positions = repository.GetPmrequestPositionsAddRemove(pmrequest, user);
                List<PmrequestPosition> positionsAllranks = null;


                positionsAllranks = positions;

                pmrequest.Displaytreeseparately = 1;
                pmrequest.AddRemove = true;
                MemoryStream mem = time.GenerateDocument(positions, pmrequest);
                return time.File(mem, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");
                /**
                 * По подразделениям.
                 */
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        /// <summary>
        /// PRINT INFO ABOUT POSITIONS
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="pmrequest"></param>
        /// <returns></returns>
        public MemoryStream GenerateDocument(List<PmrequestPosition> positions, Pmrequest pmrequest)
        {
            /**
             * Information selector
             */
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
                columns.Append(new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)100U, Width = 20, CustomWidth = true });
                worksheet.Append(columns);

                worksheet.Append(sheetData);
                worksheetPart.Worksheet = worksheet;

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Результат запроса" };

                sheets.Append(sheet);

                UInt32 rowIndex = 1;

                var row = new Row { RowIndex = rowIndex };
                sheetData.Append(row);
                row.AppendChild(CreateTextCell(ColumnLetter(1),
                    rowIndex, "Результат запроса"));

                rowIndex++;
                row = new Row { RowIndex = rowIndex };
                sheetData.Append(row);
                double count = 0;
                foreach (Models.PmrequestPosition position in positions)
                {
                    count += position.Partval;
                }
                Cell countCell = CreateTextCell(ColumnLetter(1),
                    rowIndex, count.ToString());
                row.AppendChild(countCell);
                AddBold(document, countCell);

                List<string[]> trees = null;
                int treemaxlength = 0;
                if (pmrequest.Displaytreeseparately == 1)
                {
                    trees = new List<string[]>();
                    foreach (PmrequestPosition position in positions)
                    {
                        if (position.Tree.Length > 0)
                        {
                            string[] arr = position.Tree.Split(Keys.TREE_BEAUTY);
                            trees.Add(arr);
                            if (arr.Length > treemaxlength)
                            {
                                treemaxlength = arr.Length;
                            }
                        } else
                        {
                            trees.Add(new string[1] {""});
                        }
                    }
                }

                List<AltrankPrintable> printables = pmrequest.AltrankPrintables;
                int index = 0;
                bool firstTime = true;
                foreach (PmrequestPosition position in positions)
                {
                    rowIndex++;
                    row = new Row { RowIndex = rowIndex };
                    sheetData.Append(row);
                    int column = 0;
                    Row rowPrev = null;

                    if (firstTime)
                    {
                        rowPrev = row;
                        rowIndex++;
                        row = new Row { RowIndex = rowIndex };
                        sheetData.Append(row);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.Positiontype));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Наименование должности");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.Partval.ToString()));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Количество");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    if (pmrequest.AddRemove)
                    {
                        column++;
                        row.AppendChild(CreateTextCell(ColumnLetter(column),
                            rowIndex, position.Signed));
                        if (firstTime)
                        {
                            Cell cell = CreateTextCell(ColumnLetter(column),
                                rowIndex - 1, "Вводится или сокращается");
                            rowPrev.AppendChild(cell);
                            AddBold(document, cell);
                        }
                    }

                    if (pmrequest.AddRemove)
                    {
                        column++;
                        row.AppendChild(CreateTextCell(ColumnLetter(column),
                            rowIndex, position.DateCreated));
                        if (firstTime)
                        {
                            Cell cell = CreateTextCell(ColumnLetter(column),
                                rowIndex - 1, "Дата");
                            rowPrev.AppendChild(cell);
                            AddBold(document, cell);
                        }
                    }

                    if (pmrequest.Displaytreeseparately == 1)
                    {
                        int indexLocal = 0;
                        foreach (string str in trees[index])
                        {
                            column++;

                            if (firstTime)
                            {
                                Cell cell = CreateTextCell(ColumnLetter(column),
                                    rowIndex - 1, "Иерархия подразделений");
                                rowPrev.AppendChild(cell);
                                AddBold(document, cell);
                            }

                            row.AppendChild(CreateTextCell(ColumnLetter(column),
                                rowIndex, str));
                            indexLocal++;
                        }
                        if (treemaxlength > indexLocal)
                        {
                            int empty = treemaxlength - indexLocal;
                            for (int i = 0; i < empty; i++)
                            {
                                column++;
                                row.AppendChild(CreateTextCell(ColumnLetter(column),
                                    rowIndex, ""));
                            }
                        }
                    } else
                    {
                        column++;
                        row.AppendChild(CreateTextCell(ColumnLetter(column),
                            rowIndex, position.Tree));
                        if (firstTime)
                        {
                            Cell cell = CreateTextCell(ColumnLetter(column),
                                    rowIndex - 1, "Иерархия подразделений");
                            rowPrev.AppendChild(cell);
                            AddBold(document, cell);
                        }
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.Structuremrd));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Тип подразделения");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }


                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.Rank));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Предельное звание");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    if (printables != null)
                    {
                        foreach(AltrankPrintable printable in printables)
                        {
                            column++;
                            row.AppendChild(CreateTextCell(ColumnLetter(column),
                                rowIndex, ""));
                            if (firstTime)
                            {
                                Cell cell = CreateTextCell(ColumnLetter(column),
                                    rowIndex - 1, printable.Altrankconditiongroup.Name);
                                rowPrev.AppendChild(cell);
                                AddBold(document, cell);
                            }
                            int subindex = 0;
                            foreach(string name in printable.Altrankconditionnames)
                            {
                                column++;
                                if (printable.Altranknames.ContainsKey(position.Id))
                                {
                                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                                        rowIndex, printable.Altranknames[position.Id][subindex])); 
                                } else
                                {
                                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                                        rowIndex, ""));
                                }
                                if (firstTime)
                                {
                                    Cell cell = CreateTextCell(ColumnLetter(column),
                                        rowIndex - 1, printable.Altrankconditionnames[subindex]);
                                    rowPrev.AppendChild(cell);
                                    AddBold(document, cell);
                                }

                                subindex++;
                            }

                        }
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.Positioncategory));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Категория");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.Sof));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Источник финансирования");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.Mrds));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Метки рода деятельности");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    


                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.ReplacedByCivil));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Может ли замещаться гражданским");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.ReplacedByCivilDecree));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Название и номер приказа о замещении");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.ReplacedByCivilDecreeDate));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Дата начала действия приказа");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.ReplacedByCivilDate));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Замещается до");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.ReplacedByCivilPositiontype));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Наименование должности при замещении");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.ReplacedByCivilPositioncategory));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Категория должности при замещении");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.CivilClassLow));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Минимальный класс гос. служащего");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.CivilClassHigh));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Максимальный класс гос. служащего");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    if (!pmrequest.AddRemove)
                    {
                        column++;
                        row.AppendChild(CreateTextCell(ColumnLetter(column),
                            rowIndex, position.Signed));
                        if (firstTime)
                        {
                            Cell cell = CreateTextCell(ColumnLetter(column),
                                rowIndex - 1, "Приказ подписан");
                            rowPrev.AppendChild(cell);
                            AddBold(document, cell);
                        }
                    }
                    

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.DecreeCreationName));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Название приказа");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.DecreeCreationNumber));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Номер приказа");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.DecreeCreationUnofficialName));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Неофициальное название приказа");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.DecertificateDate));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Дата разаттестации");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    if (!pmrequest.AddRemove)
                    {
                        column++;
                        row.AppendChild(CreateTextCell(ColumnLetter(column),
                            rowIndex, position.DateCreated));
                        if (firstTime)
                        {
                            Cell cell = CreateTextCell(ColumnLetter(column),
                                rowIndex - 1, "Дата введения");
                            rowPrev.AppendChild(cell);
                            AddBold(document, cell);
                        }
                    }
                       

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.Heading));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Возглавляет подразделение");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.Curation));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Курирует подразделения");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, position.Notice));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Примечание");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    index++;
                    firstTime = false;
                }

                workbookPart.Workbook.Save();
            }
            mem.Position = 0;
            return mem;
        }

        /// <summary>
        /// PRINT INFO ABOUT STRUCTURE COUNT
        /// Генерация предоставления сведений о штатной численности подразделений
        /// </summary>
        /// <param name="structures"></param>
        /// <param name="pmrequest"></param>
        /// <returns></returns>
        public MemoryStream GenerateStructureCountDocument(List<PmrequestStructure> structures, Pmrequest pmrequest, User user)
        {
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
                columns.Append(new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 28, CustomWidth = true });
                columns.Append(new Column() { Min = (UInt32Value)2U, Max = (UInt32Value)100U, Width = 9, CustomWidth = true });
                worksheet.Append(columns);

                worksheet.Append(sheetData);
                worksheetPart.Worksheet = worksheet;
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());


                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Сведения" };

                sheets.Append(sheet);
                MergeCells mergeCells = new MergeCells();

                UInt32 rowIndex = 1;

                int summaryColumn = 4;
                int summaryLength = 15;

                
                var row = new Row { RowIndex = rowIndex };
                sheetData.Append(row);
                Cell titleCell = CreateTextCell(ColumnLetter(summaryColumn),
                    rowIndex, "С В Е Д Е Н И Я");
                row.AppendChild(titleCell);
                ExcelFormatter.Format(document, titleCell, true, true, false, 0, 0, 0, 0);
                mergeCells.Append(new MergeCell() { Reference = new StringValue(ColumnLetter(summaryColumn) + rowIndex.ToString() + ":" + ColumnLetter(summaryColumn + summaryLength) + rowIndex.ToString()) });

                rowIndex++;
                row = new Row { RowIndex = rowIndex };
                sheetData.Append(row);
                Cell descriptionTitleCell = CreateTextCell(ColumnLetter(summaryColumn),
                    rowIndex, "о штатной численности органов и подразделений по чрезвычайным ситуациям");
                row.AppendChild(descriptionTitleCell);
                ExcelFormatter.Format(document, descriptionTitleCell, true, false, false, 0, 0, 0, 0);
                mergeCells.Append(new MergeCell() { Reference = new StringValue(ColumnLetter(summaryColumn) + rowIndex.ToString() + ":" + ColumnLetter(summaryColumn + summaryLength) + rowIndex.ToString()) });

                rowIndex++;
                row = new Row { RowIndex = rowIndex };
                sheetData.Append(row);
                Cell dateTitleCell = CreateTextCell(ColumnLetter(summaryColumn),
                    rowIndex, "по состоянию на " + user.Date.GetValueOrDefault().ToShortDateString());
                row.AppendChild(dateTitleCell);
                ExcelFormatter.Format(document, dateTitleCell, true, false, false, 0, 0, 0, 0);
                mergeCells.Append(new MergeCell() { Reference = new StringValue(ColumnLetter(summaryColumn) + rowIndex.ToString() + ":" + ColumnLetter(summaryColumn + summaryLength) + rowIndex.ToString()) });

                rowIndex++;
                row = new Row { RowIndex = rowIndex };
                sheetData.Append(row);
                Cell structureTitleCell = null;
                if (pmrequest.StructureMain != null)
                {
                    structureTitleCell = CreateTextCell(ColumnLetter(summaryColumn),
                    rowIndex, "подразделение " + pmrequest.StructureMain.Name);
                } else
                {
                    structureTitleCell = CreateTextCell(ColumnLetter(summaryColumn),
                    rowIndex, "Министерство по чрезвычайным ситуациям");
                }
                
                row.AppendChild(structureTitleCell);
                ExcelFormatter.Format(document, structureTitleCell, true, false, false, 0, 0, 0, 0);
                mergeCells.Append(new MergeCell() { Reference = new StringValue(ColumnLetter(summaryColumn) + rowIndex.ToString() + ":" + ColumnLetter(summaryColumn + summaryLength) + rowIndex.ToString()) });

                rowIndex++;
                //rowIndex++;
                //row = new Row { RowIndex = rowIndex };
                //sheetData.Append(row);
                //Cell countCell = CreateTextCell(ColumnLetter(1),
                //    rowIndex, structures.Count.ToString());
                //row.AppendChild(countCell);
                //AddBold(document, countCell);

                bool ranks = true;
                bool tree = true;


                int index = 0;
                Row rowPrev = null;
                PmrequestStructure structureFirst = structures.FirstOrDefault();
                List<int> skipCategory = new List<int>();
                if (structureFirst != null)
                {
                    rowIndex++;
                    int column = 1;
                    row = new Row { RowIndex = rowIndex };
                    rowPrev = row;
                    sheetData.Append(row);
                    uint rowIndexPrev = rowIndex;
                    Cell structureCell = CreateTextCell(ColumnLetter(column),
                    rowIndex, "Подразделение");
                    row.AppendChild(structureCell);
                    ExcelFormatter.Format(document, structureCell, true, true, true, 2, 2, 1, 2);

                    rowIndex++;
                    row = new Row { RowIndex = rowIndex };
                    
                    sheetData.Append(row);

                    
                    
                    mergeCells.Append(new MergeCell() { Reference = new StringValue(ColumnLetter(column) + rowIndexPrev.ToString() + ":" + ColumnLetter(column) + rowIndex.ToString()) });
                    column++;

                    if (tree)
                    {
                        Cell treeCell = CreateTextCell(ColumnLetter(column),
                    rowIndexPrev, "Структура");
                        rowPrev.AppendChild(treeCell);
                        ExcelFormatter.Format(document, treeCell, true, true, true, 2, 2, 1, 2);
                        
                        mergeCells.Append(new MergeCell() { Reference = new StringValue(ColumnLetter(column) + rowIndexPrev.ToString() + ":" + ColumnLetter(column) + rowIndex.ToString()) });
                        column++;
                    }

                    if (ranks)
                    {
                        Cell rankCell = CreateTextCell(ColumnLetter(column),
                    rowIndexPrev, "Разряд");
                        rowPrev.AppendChild(rankCell);
                        ExcelFormatter.Format(document, rankCell, true, true, true, 2, 2, 1, 2);

                        mergeCells.Append(new MergeCell() { Reference = new StringValue(ColumnLetter(column) + rowIndexPrev.ToString() + ":" + ColumnLetter(column) + rowIndex.ToString()) });
                        column++;
                    }

                    
                    
                    foreach (PmrequestStructureCount pmrequestStructureCount in structureFirst.PmrequestStructureCounts)
                    {
                        bool first = true;
                        string sofName = "В С Е Г О";
                        if (pmrequestStructureCount.Sourceoffinancing != null)
                        {
                            sofName = pmrequestStructureCount.Sourceoffinancing.Name;
                            column++;
                            first = false;
                        } 

                        int pcNameCellColumn = column;
                        Cell sofNameCell = CreateTextCell(ColumnLetter(column),
                        rowIndexPrev, sofName);
                        rowPrev.AppendChild(sofNameCell);
                        ExcelFormatter.Format(document, sofNameCell, true, false, true, 2, 2, 1, 2);

                        string pcName = "Всего";
                        Cell summaryCountCell = CreateTextCell(ColumnLetter(column),
                            rowIndex, pcName);
                        row.AppendChild(summaryCountCell);
                        ExcelFormatter.Format(document, summaryCountCell, true, false, true, 1, 1, 1, 1);

                        int countPositionIndex = 0;
                        foreach (KeyValuePair<int, double> pair in pmrequestStructureCount.CountPositionCategory)
                        {
                            pcName = "Всего";
                            if (pair.Key > 0)
                            {
                                if (pair.Value == 0 && first)
                                {
                                    skipCategory.Add(pair.Key);
                                }
                                if (skipCategory.Contains(pair.Key))
                                {
                                    continue;
                                }
                                pcName = repository.PositioncategoriesLocal()[pair.Key].Name;
                                column++;

                                
                            }
                            
                            //column++; Отображает правильно, но так не должно быть
                            Cell pcCell = CreateTextCell(ColumnLetter(column),
                            rowIndex, pcName);
                            row.AppendChild(pcCell);
                            countPositionIndex++;
                            ExcelFormatter.Format(document, pcCell, true, false, true, 1, 1, 1, 1);
                           
                            Cell emptyPCNameCell = CreateTextCell(ColumnLetter(column),
                            rowIndexPrev, "");
                            rowPrev.AppendChild(emptyPCNameCell);
                            ExcelFormatter.Format(document, emptyPCNameCell, true, false, true, 2, 2, 1, 2);
                        }
                        mergeCells.Append(new MergeCell() { Reference = new StringValue(ColumnLetter(pcNameCellColumn) + rowIndexPrev.ToString() + ":" + ColumnLetter(pcNameCellColumn + countPositionIndex) + rowIndexPrev.ToString()) });
                    }

                }


                foreach (PmrequestStructure structure in structures)
                {
                    rowIndex++;
                    row = new Row { RowIndex = rowIndex };
                    sheetData.Append(row);
                    int column = 0;
                    

                    column++;
                    Cell structureNameCell = CreateTextCell(ColumnLetter(column),
                    rowIndex, structure.Name);
                    row.AppendChild(structureNameCell);
                    ExcelFormatter.Format(document, structureNameCell, false, false, true, 1, 1, 1, 1);

                    if (tree)
                    {
                        string structureTree = structure.Tree;
                        if (structureTree != null && structureTree.Length > 0)
                        {
                            structureTree = structureTree.Split(" — ").Last();
                        } else
                        {
                            structureTree = "";
                        }

                        column++;
                        Cell structureTreeCell = CreateTextCell(ColumnLetter(column),
                        rowIndex, structureTree); //structure.Tree
                        row.AppendChild(structureTreeCell);
                        ExcelFormatter.Format(document, structureTreeCell, false, false, true, 1, 1, 1, 1);
                    }

                    if (ranks)
                    {
                        column++;
                        Cell structureTreeCell = CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.Rank); //structure.Tree
                        row.AppendChild(structureTreeCell);
                        ExcelFormatter.Format(document, structureTreeCell, false, false, true, 1, 1, 1, 1);
                    }

                    foreach (PmrequestStructureCount pmrequestStructureCount in structure.PmrequestStructureCounts)
                    {
                        column++;
                        Cell summaryCountCell = CreateTextCell(ColumnLetter(column),
                        rowIndex, pmrequestStructureCount.CountSummary.ToString());
                        row.AppendChild(summaryCountCell);
                        ExcelFormatter.Format(document, summaryCountCell, false, false, true, 1, 1, 1, 1);

                        foreach (KeyValuePair<int, double> pair in pmrequestStructureCount.CountPositionCategory)
                        {
                            if (skipCategory.Contains(pair.Key))
                            {
                                continue;
                            }
                            column++;
                            Cell countCell = CreateTextCell(ColumnLetter(column),
                            rowIndex, pair.Value.ToString());
                            row.AppendChild(countCell);
                            ExcelFormatter.Format(document, countCell, false, false, true, 1, 1, 1, 1);
                        }
                    }
                }

                worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<DocumentFormat.OpenXml.Spreadsheet.SheetData>().First());
                workbookPart.Workbook.Save();
            }
            mem.Position = 0;
            return mem;
        }



        /// <summary>
        /// PRINT INFO ABOUT STRUCTURES
        /// </summary>
        /// <param name="structures"></param>
        /// <param name="pmrequest"></param>
        /// <returns></returns>
        public MemoryStream GenerateDocument(List<PmrequestStructure> structures, Pmrequest pmrequest)
        {
            /**
             * Information selector
             */

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
                columns.Append(new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)100U, Width = 25, CustomWidth = true });
                worksheet.Append(columns);

                worksheet.Append(sheetData);
                worksheetPart.Worksheet = worksheet;
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());


                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Результат запроса" };

                sheets.Append(sheet);

                UInt32 rowIndex = 1;


                var row = new Row { RowIndex = rowIndex };
                sheetData.Append(row);
                row.AppendChild(CreateTextCell(ColumnLetter(1),
                    rowIndex, "Результат запроса"));

                rowIndex++;
                row = new Row { RowIndex = rowIndex };
                sheetData.Append(row);
                Cell countCell = CreateTextCell(ColumnLetter(1),
                    rowIndex, structures.Count.ToString());
                row.AppendChild(countCell);
                AddBold(document, countCell);

                List<string[]> trees = null;
                int treemaxlength = 0;
                if (pmrequest.Displaytreeseparately == 1)
                {
                    trees = new List<string[]>();
                    foreach (PmrequestStructure structure in structures)
                    {
                        if (structure.Tree.Length > 0)
                        {
                            string[] arr = structure.Tree.Split(Keys.TREE_BEAUTY);
                            trees.Add(arr);
                            if (arr.Length > treemaxlength)
                            {
                                treemaxlength = arr.Length;
                            }
                        }
                        else
                        {
                            trees.Add(new string[1] { "" });
                        }
                    }
                }

                int index = 0;
                bool firstTime = true;
                foreach (PmrequestStructure structure in structures)
                {
                    rowIndex++;
                    row = new Row { RowIndex = rowIndex };
                    sheetData.Append(row);
                    int column = 0;
                    Row rowPrev = null;

                    if (firstTime)
                    {
                        rowPrev = row;
                        rowIndex++;
                        row = new Row { RowIndex = rowIndex };
                        sheetData.Append(row);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.Name));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Наименование подразделения");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    if (pmrequest.Displaytreeseparately == 1)
                    {
                        int indexLocal = 0;
                        if (firstTime)
                        {
                            Cell cell = CreateTextCell(ColumnLetter(column + 1),
                                rowIndex - 1, "Иерархия подразделений");
                            rowPrev.AppendChild(cell);
                            AddBold(document, cell);
                        }
                        foreach (string str in trees[index])
                        {
                            column++;
                            row.AppendChild(CreateTextCell(ColumnLetter(column),
                                rowIndex, str));
                            indexLocal++;
                        }
                        if (treemaxlength > indexLocal)
                        {
                            int empty = treemaxlength - indexLocal;
                            for (int i = 0; i < empty; i++)
                            {
                                column++;
                                row.AppendChild(CreateTextCell(ColumnLetter(column),
                                    rowIndex, ""));
                            }
                        }
                    }
                    else
                    {
                        column++;
                        row.AppendChild(CreateTextCell(ColumnLetter(column),
                            rowIndex, structure.Tree));
                        if (firstTime)
                        {
                            Cell cell = CreateTextCell(ColumnLetter(column),
                                rowIndex - 1, "Иерархия подразделений");
                            rowPrev.AppendChild(cell);
                            AddBold(document, cell);
                        }
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.Type));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Метка типа подразделения");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.Rank));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Разряд");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.Region));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Область");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.City));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Город");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.Street));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Улица");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.Head));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Возглавляет");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.Curator));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Курирует");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.StructureInfoInner.PositionCountSigned.ToString()));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Число должностей (подписаны)");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.StructureInfoInner.PositionCountUnsigned.ToString()));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Число должностей (не подписаны)");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    foreach (KeyValuePair<Sourceoffinancing, double> sofs in structure.SofSigned)
                    {// Тут может быть ошибка с выводом
                        column++;
                        row.AppendChild(CreateTextCell(ColumnLetter(column),
                            rowIndex, sofs.Value.ToString()));
                        if (firstTime)
                        {
                            Cell cell = CreateTextCell(ColumnLetter(column),
                                rowIndex - 1, sofs.Key.Name);
                            rowPrev.AppendChild(cell);
                            AddBold(document, cell);
                        }
                    }

                    foreach (KeyValuePair<Sourceoffinancing, double> sofs in structure.SofUnsigned)
                    {// Тут может быть ошибка с выводом
                        column++;
                        row.AppendChild(CreateTextCell(ColumnLetter(column),
                            rowIndex, sofs.Value.ToString()));
                        if (firstTime)
                        {
                            Cell cell = CreateTextCell(ColumnLetter(column),
                                rowIndex - 1, sofs.Key.Name + "(не подписаны)");
                            rowPrev.AppendChild(cell);
                            AddBold(document, cell);
                        }
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.Signed));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Приказ подписан");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.DecreeCreationName));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Название приказа");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.DecreeCreationNumber));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Номер приказа");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    column++;
                    row.AppendChild(CreateTextCell(ColumnLetter(column),
                        rowIndex, structure.DecreeCreationUnofficialName));
                    if (firstTime)
                    {
                        Cell cell = CreateTextCell(ColumnLetter(column),
                            rowIndex - 1, "Неофициальное название приказа");
                        rowPrev.AppendChild(cell);
                        AddBold(document, cell);
                    }

                    index++;
                    firstTime = false;
                }
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
    }
}