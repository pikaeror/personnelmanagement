using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using System.IO;
using PersonnelManagement.Utils;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using Wps = DocumentFormat.OpenXml.Office2010.Word.DrawingShape;
using A14 = DocumentFormat.OpenXml.Office2010.Drawing;
using Wp14 = DocumentFormat.OpenXml.Office2010.Word.Drawing;
using V = DocumentFormat.OpenXml.Vml;
using Thm15 = DocumentFormat.OpenXml.Office2013.Theme;
using Ovml = DocumentFormat.OpenXml.Vml.Office;
using M = DocumentFormat.OpenXml.Math;
using W15 = DocumentFormat.OpenXml.Office2013.Word;
using System.Text.RegularExpressions;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Persondecree")]
    public class PersondecreeController : Controller
    {
        private const string firstLineIndent = "709";
        private const string leftIndent = "1514";
        private const string hangingIndent = "1514";
        private const string fontSize = "30";
        private const string beforeSpace = "120";
        private Repository repository;

        public PersondecreeController(Repository repository)
        {
            this.repository = repository;
        }

        // Is allowed to edit structures.
        // GET: api/Persondecree
        [HttpGet]
        public bool IsAllowedToEditDecrees()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                return true;
            }
            return false;
        }

        // GET: api/Persondecree/Active
        [HttpGet("FullByUser")]
        public IEnumerable<PersondecreeManagement> FullByUser()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                // return repository.GetDecree 
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                IEnumerable<PersondecreeManagement> rep = repository.GetPersondecreesFull(user);
                return repository.GetPersondecreesFull(user);
            }
            return null;
        }

        // GET: api/Persondecree/Active
        [HttpGet("Active")]
        public IEnumerable<PersondecreeManagement> GetDecreeActive()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                // return repository.GetDecree 
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                return repository.GetPersondecreesActive(user);
            }
            return null;
        }

        // GET: api/Persondecree/5
        [HttpGet("{id}")]
        public PersondecreeManagement GetDecree([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                PersondecreeManagement persondecree = repository.GetPersondecreeManagement(user, id);
                //return repository.Persondecrees.FirstOrDefault(d => d.Id == id);
                return persondecree;
            }
            return null;
        }

        // GET: api/Persondecree/Action
        [HttpPost("Action{str}")]
        public IActionResult PerformAction([FromRoute] string str, [FromBody] Persondecree folder)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                string[] parts = str.Split('_'); // По запросу к нам приходят цифры в формате "5_45_123_28", где первая цифра это тип действия. 
                                                 // Остальные - id проектов приказов, над которыми будут совершаться действия.
                                                 // Первая цифра:
                                                 // 1 - объединить проекты приказов в 1. Берет несколько проектов приказа и превращает в один большой. Оригиналы остаются.
                if (parts.Length < 2)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Произошла ошибка");
                }
                int operation = -1;
                List<int> ids = new List<int>();
                foreach(string part in parts)
                {
                    // В первый раз узнаем тип проводимой операции над проектами приказа
                    if (operation == -1)
                    {
                        operation = Int32.Parse(part);
                    // В последующие разы записываем id проектов приказа
                    } else
                    {
                        ids.Add(Int32.Parse(part));
                    }
                }
                switch (operation)
                {
                    case 1:
                        repository.PersondecreesUnite(user, ids, folder);
                        break;
                    default:
                        break;
                }
                return new ObjectResult(Keys.SUCCESS_SHORT + "Приказ " +
                    ((folder != null) ? ((folder.Name != "" || folder.Nickname != "") ? "" :
                    ((folder.Name) +
                    ((folder.Name != "" && folder.Nickname != "") ? " / " : "") +
                    folder.Nickname + " ")) : "") + "создан. Помещён в папку 'В работе'");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
        }

        [HttpGet("GetLustDecreeByUser")]
        public PersondecreeManagement GetLustDecreeByUser()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                List<Persondecree> persondecree = repository.GetContext().Persondecree.ToList().Where(r => r.Creator == user.Id && r.Owner == user.Id).ToList();
                int max_id = -1;
                PersondecreeManagement output = null;
                persondecree.ForEach(k => {
                    if(k.Id > max_id)
                    {
                        max_id = k.Id;
                        output = new PersondecreeManagement(k);
                    }
                });
                //return repository.Persondecrees.FirstOrDefault(d => d.Id == id);
                return output;
            }
            return null;
        }

        // POST: api/Persondecree
        [HttpPost]
        public IActionResult PostDecree([FromBody] PersondecreeManagement persondecreeManagement)
        {
            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanEditPerson(user, repository);
                //bool hasAccess = IdentityService.canEditStructures(sessionid, repository);
                if (!hasAccess)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");

                }
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }



            /**
             * Means, we add new decree.
             */
            if (persondecreeManagement.PersondecreeManagementStatus == Keys.PERSONDECREE_MANAGEMENT_NEWDECREE)
            {
                repository.AddNewPersondecree(user, persondecreeManagement);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Приказ создан");
            }
            /**
             * Decline decree.
             */
            else if (persondecreeManagement.PersondecreeManagementStatus == Keys.PERSONDECREE_MANAGEMENT_DECLINEDECREE)
            {
                repository.RemovePersondecree(user, persondecreeManagement);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Приказ отменен");
            }
            /**
             * Подписываем проект приказа
             */
            else if (persondecreeManagement.PersondecreeManagementStatus == Keys.PERSONDECREE_MANAGEMENT_ACCEPTDECREE)
            {
                repository.AcceptPersondecree(persondecreeManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Приказ подписан");
            }
            /**
             * Update decree info.
             */
            else if (persondecreeManagement.PersondecreeManagementStatus == Keys.PERSONDECREE_MANAGEMENT_UPDATEDECREEINFO)
            {
                repository.UpdatePersondecree(persondecreeManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Проект приказа обновлен");
            }
            /**
             * Печатаем весь проект приказа в Word
             */
            else if (persondecreeManagement.PersondecreeManagementStatus == Keys.PERSONDECREE_MANAGEMENT_PRINTDECREE)
            {
                //return File(GenerateDocument(persondecreeManagement, user), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "test.docx");
                //MemoryStream stream = GenerateDocument(persondecreeManagement, user);
                DocPersonDecree time = new DocPersonDecree(repository, user);
                time.Worker(persondecreeManagement);
                return File(time.GetMemoryStream(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "test.docx");
            }
            /**
             * Фильтрация если приказ уже подписан.
             */
            else if (persondecreeManagement.PersondecreeManagementStatus == Keys.PERSONDECREE_MANAGEMENT_LOCKDECREE)
            {
                //return new ObjectResult(repository.GetFilteredDecrees(user, decreeManagement));
                //return File(GenerateDocument(decreeManagement), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "test.docx");
                
            }
            /**
             * Направляет проект приказа другому кадровику
             */
            else if (persondecreeManagement.PersondecreeManagementStatus == Keys.PERSONDECREE_MANAGEMENT_CHANGEOWNER)
            {
                repository.ChangeownerPersondecree(persondecreeManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Проект приказа перенаправлен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        /// <summary>
        /// Генерация всего проекта приказа в формате Word
        /// </summary>
        /// <param name="persondecreeManagement"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public MemoryStream GenerateDocument(PersondecreeManagement persondecreeManagement, User user)
        {
            PersondecreeManagement persondecree = repository.GetPersondecreeManagement(user, persondecreeManagement.Id);
            List<Persondecreeblock> persondecreeblocks = repository.Persondecreeblocks.Where(p => p.Persondecree == persondecree.Id).ToList();
            
            var mem = new MemoryStream();
            // Создаем документ
            WordprocessingDocument wordDocument =
                WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document, true);
            
            // Основная часть документа
            MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document();
            Body docBody = new Body();
            mainPart.Document.Append(docBody);

            ThemePart themePart1 = mainPart.AddNewPart<ThemePart>("rId8");
            GenerateThemePart1Content(themePart1);

            FontTablePart fontTablePart1 = mainPart.AddNewPart<FontTablePart>("rId7");
            GenerateFontTablePart1Content(fontTablePart1);

            StyleDefinitionsPart styleDefinitionsPart1 = mainPart.AddNewPart<StyleDefinitionsPart>("rId1");
            GenerateStyleDefinitionsPart1Content(styleDefinitionsPart1);

            // Тестовое отображение Lorem Ipsum. Потом должно быть скрыто или удалено.
            bool test = false;
            if (test)
            {
                // Тестовая часть 
                Run titleRun = new Run();
                AppendBold(titleRun);
                AppendFontSize(titleRun, "30");

                Text titleText = new Text("ПРОЕКТ ПРИКАЗА");
                titleRun.Append(titleText);
                titleRun.Append(new CarriageReturn());


                Paragraph titleParagraph = new Paragraph();
                AppendParagraphCenter(titleParagraph);
                titleParagraph.Append(titleRun);
                docBody.Append(titleParagraph);

                Run p1Run = new Run();
                AppendFontSize(p1Run, fontSize);
                //AppendFontDefault(p1Run);

                Text p1Text = new Text("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
                p1Run.Append(p1Text);
                p1Run.Append(new CarriageReturn());
                Text p1Text2 = new Text("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
                p1Run.Append(p1Text2);

                Paragraph p1Paragraph = new Paragraph();
                AppendParagraphJustify(p1Paragraph);
                p1Paragraph.Append(p1Run);
                docBody.Append(p1Paragraph);


                Run p2Run = new Run();
                AppendFontSize(p2Run, fontSize);
                //AppendFontDefault(p2Run);

                Text p2Text = new Text("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
                p2Run.Append(p2Text);

                Paragraph p2Paragraph = new Paragraph();
                AppendParagraphJustify(p2Paragraph);
                AppendParagraphIndentation(p2Paragraph, firstLineIndent);
                p2Paragraph.Append(p2Run);
                docBody.Append(p2Paragraph);
            }

            List<PersondecreeoperationManagement> persondecreeoperationsAll = repository.GetPersondecreeoperation(user, persondecree.Id, true).ToList();
            

            // Проходимся по блокам внутри проекта приказа
            foreach (Persondecreeblock persondecreeblock in persondecreeblocks)
            {
                //List<PersondecreeoperationManagement> persondecreeoperations = repository.PersondecreeoperationsLocal().Values.Where(p => p.Persondecreeblock == persondecreeblock.Id).ToList();
                List<PersondecreeoperationManagement> persondecreeoperations = persondecreeoperationsAll.Where(p => p.Persondecreeblock == persondecreeblock.Id).ToList();
                // Расписываем управляющее слово и номер.
                string persondecreeblockText = "";
                persondecreeblockText += persondecreeblock.Index + ". ";
                persondecreeblockText += repository.Persondecreeblocktypes.FirstOrDefault(p => p.Id == persondecreeblock.Persondecreeblocktype).Name.ToUpper() + ":";
                Run persondecreeblock1Run = new Run();
                AppendFontSize(persondecreeblock1Run, fontSize);
                Text persondecreeblock1Text = new Text(persondecreeblockText);
                persondecreeblock1Run.Append(persondecreeblock1Text);
                Paragraph persondecreeblock1Paragraph = new Paragraph();
                persondecreeblock1Paragraph.Append(persondecreeblock1Run);
                docBody.Append(persondecreeblock1Paragraph);


                //Run persondecreeblock2Run = new Run();
                //AppendFontSize(persondecreeblock2Run, fontSize);
                //Text persondecreeblock2Text = new Text("");
                //persondecreeblock2Run.Append(persondecreeblock2Text);
                //Paragraph persondecreeblock2Paragraph = new Paragraph();
                //persondecreeblock2Paragraph.Append(persondecreeblock2Run);
                //docBody.Append(persondecreeblock2Paragraph);

                AddSpaceDocument(docBody);

                // Поощрить
                if (persondecreeblock.Persondecreeblocktype == 1)
                {
                    foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperations)
                    {
                        PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                        string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                        int padezh = 4; // Основной падеж сотрудника


                        if (persondecreeoperation.Persondecreeblocksubtype == 8 || persondecreeoperation.Persondecreeblocksubtype == 3 || persondecreeoperation.Persondecreeblocksubtype == 4
                        || persondecreeoperation.Persondecreeblocksubtype == 5 || persondecreeoperation.Persondecreeblocksubtype == 6 || persondecreeoperation.Persondecreeblocksubtype == 7)
                        {
                            padezh = 4;
                        }
                        else
                        {
                            padezh = 3;
                        }

                        string rank = "";
                    }
                }

                // Наложить дисциплинарное взыскание
                if (persondecreeblock.Persondecreeblocktype == 2)
                {
                    foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperations)
                    {
                        PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                        string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                        int padezh = 4; // Основной падеж сотрудника


                    }
                }

                // Назначить
                if (persondecreeblock.Persondecreeblocktype == 3)
                {
                    foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperations)
                    {
                        PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                        string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                        int padezh = 4; // Основной падеж сотрудника

                        string rank = "";
                        if (person != null && person.ActualRank != null)
                        {
                            rank = person.ActualRank.Name4;
                        }

                        string fio = "";
                        if (person != null)
                        {
                            fio += person.Surname4.ToUpper() + " ";
                            fio += person.Name4 + " ";
                            fio += person.Fathername4;
                        }
                        else
                        {
                            fio += nonperson;
                        }

                        string appointtype = repository.Appointtypes.FirstOrDefault(r => r.Id == persondecreeoperation.Optionnumber4).Name;
                        string newPosition = persondecreeoperation.Optionstring4;

                        string date = "";
                        if (persondecreeoperation.Optiondate1 != null)
                        {
                            date += "с " + persondecreeoperation.Optiondate1.GetValueOrDefault().ToString("d MMMM yyyy года");
                        }

                        string oldPosition = "";
                        if (person != null)
                        {

                            oldPosition = person.Positiontree2;
                        }

                        string gender = "его ";
                        if (person != null && person.Gender.StartsWith('Ж'))
                        {
                            gender = "ее ";
                        }

                        string text = "";
                        // Назначаем на должность того, кто уже есть на службе (то есть военного, а не гражданского)
                        if (person != null && person.ActualRank != null)
                        {
                            text = rank + AddSpaceIfNotEmpty(rank) + fio + AddSpaceIfNotEmpty(fio) + appointtype + AddSpaceIfNotEmpty(appointtype)
                                + "на должность " + newPosition + AddSpaceIfNotEmpty(newPosition, date) + date + ", освободив " + gender + "от должности"
                                + AddSpaceIfNotEmpty(oldPosition) + RemoveSpaces(oldPosition) + ".";

                            Text mainText = new Text(text);
                            Run mainRun = new Run();
                            Paragraph mainParagraph = new Paragraph();
                            mainRun.Append(mainText);
                            mainParagraph.Append(mainRun);
                            docBody.Append(mainParagraph);
                            AppendFontSize(mainRun, fontSize);
                            AppendParagraphJustify(mainParagraph);
                            AppendParagraphIndentation(mainParagraph, firstLineIndent);
                        }
                        else if (person != null)
                        {
                            string newRank = "";
                            if (persondecreeoperation.Optionnumber2 > 0)
                            {
                                newRank += ", присвоив специальное звание «" + repository.RanksLocal().GetValue(persondecreeoperation.Optionnumber2).Name + "»";
                            }
                            string newNumber = "";
                            if (persondecreeoperation.Optionstring3.Length > 0)
                            {
                                if (persondecreeoperation.Optionnumber2 > 0)
                                {
                                    newNumber += " и ";
                                }
                                else
                                {
                                    newNumber += ", присвоив ";
                                }
                                newNumber += "личный номер " + persondecreeoperation.Optionstring3;
                            }

                            text = fio + AddSpaceIfNotEmpty(fio) + appointtype + AddSpaceIfNotEmpty(appointtype)
                                + "на должность " + newPosition + AddSpaceIfNotEmpty(newPosition, date) + date + newRank + newNumber + ".";

                            Text mainText = new Text(text);
                            Run mainRun = new Run();
                            Paragraph mainParagraph = new Paragraph();
                            mainRun.Append(mainText);
                            mainParagraph.Append(mainRun);
                            docBody.Append(mainParagraph);
                            AppendFontSize(mainRun, fontSize);
                            AppendParagraphJustify(mainParagraph);
                            AppendParagraphIndentation(mainParagraph, firstLineIndent);
                        }
                        if (persondecreeoperation.Optiondate2 != null)
                        {
                            string experienceString = "Установить стаж для выплаты процентной надбавки за выслугу лет по состоянию на ";

                            string years = "";
                            if (persondecreeoperation.Optionnumber5 > 0)
                            {
                                years = persondecreeoperation.Optionnumber5 + " лет";
                            }
                            string months = "";
                            if (persondecreeoperation.Optionnumber6 > 0)
                            {
                                months = persondecreeoperation.Optionnumber6 + " месяцев";
                            }
                            string days = "";
                            if (persondecreeoperation.Optionnumber7 > 0)
                            {
                                days = persondecreeoperation.Optionnumber7 + " дней";
                            }

                            experienceString += persondecreeoperation.Optiondate2.GetValueOrDefault().ToString("d MMMM yyyy года")
                                + " — " + years + AddSpaceIfNotEmpty(years) + months + AddSpaceIfNotEmpty(months) + days + ".";

                            Text experienceText = new Text(experienceString);
                            Run experienceRun = new Run();
                            Paragraph experienceParagraph = new Paragraph();
                            experienceRun.Append(experienceText);
                            experienceParagraph.Append(experienceRun);
                            docBody.Append(experienceParagraph);
                            AppendFontSize(experienceRun, fontSize);
                            AppendParagraphJustify(experienceParagraph);
                            AppendParagraphIndentation(experienceParagraph, firstLineIndent);
                            //{{decreeoperation.optionnumber5}} лет {{decreeoperation.optionnumber6}} месяцев {{decreeoperation.optionnumber7}} дней.
                        }
                        if (persondecreeoperation.Optiondate3 != null)
                        {
                            string contractString = "Заключить контракт сроком на ";

                            string years = "";
                            if (persondecreeoperation.Optionnumber8 > 0)
                            {
                                years = persondecreeoperation.Optionnumber8 + " лет";
                            }
                            string months = "";
                            if (persondecreeoperation.Optionnumber9 > 0)
                            {
                                months = persondecreeoperation.Optionnumber9 + " месяцев";
                            }
                            string days = "";
                            if (persondecreeoperation.Optionnumber10 > 0)
                            {
                                days = persondecreeoperation.Optionnumber10 + " дней";
                            }

                            string dateStart = "";
                            if (persondecreeoperation.Optiondate3 != null)
                            {
                                dateStart += "с " + persondecreeoperation.Optiondate3.GetValueOrDefault().ToString("d MMMM yyyy года");
                            }

                            string dateEnd = "";
                            if (persondecreeoperation.Optiondate4 != null)
                            {
                                dateEnd += "по " + persondecreeoperation.Optiondate4.GetValueOrDefault().ToString("d MMMM yyyy года");
                            }

                            contractString += years + AddSpaceIfNotEmpty(years) + months + AddSpaceIfNotEmpty(months) + days
                                + AddSpaceIfNotEmpty(days) + dateStart + AddSpaceIfNotEmpty(dateStart) + dateEnd + ".";

                            Text contractText = new Text(contractString);
                            Run contractRun = new Run();
                            Paragraph contractParagraph = new Paragraph();
                            contractRun.Append(contractText);
                            contractParagraph.Append(contractRun);
                            docBody.Append(contractParagraph);
                            AppendFontSize(contractRun, fontSize);
                            AppendParagraphJustify(contractParagraph);
                            AppendParagraphIndentation(contractParagraph, firstLineIndent);
                        }

                        AddSpaceDocument(docBody);
                    }
                }

                // Уволить
                if (persondecreeblock.Persondecreeblocktype == 4)
                {
                    List<Persondecreeblocksub> persondecreeblocksubs = repository.Persondecreeblocksubs.Where(p => p.Persondecreeblock == persondecreeblock.Id).ToList();

                    foreach (Persondecreeblocksub persondecreeblocksub in persondecreeblocksubs)
                    {
                        List<PersondecreeoperationManagement> persondecreeoperationSubs = persondecreeoperations.Where(p => p.Persondecreeblocksub == persondecreeblocksub.Id).ToList();

                        foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperationSubs)
                        {
                            PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                            string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                            int padezh = 4; // Основной падеж сотрудника

                            string indexation = persondecreeblock.Index + "." + persondecreeblocksub.Index + ".";

                            string rank = "";
                            if (person != null && person.ActualRank != null)
                            {
                                rank = person.ActualRank.Name4;
                            }

                            string fio = "";
                            if (person != null)
                            {
                                fio += person.Surname4.ToUpper() + " ";
                                fio += person.Name4 + " ";
                                fio += person.Fathername4;
                            }
                            else
                            {
                                fio += nonperson;
                            }

                            string newPosition = persondecreeoperation.Optionstring4;

                            string date = "";
                            if (persondecreeoperation.Optiondate1 != null)
                            {
                                date += persondecreeoperation.Optiondate1.GetValueOrDefault().ToString("d MMMM yyyy года");
                            }

                            string position = "";
                            if (person != null)
                            {

                                position = person.Positiontree2;
                            }

                            string text = "";
                            string subtext = "";
                            // Увольняем человека со званием
                            if (person != null && person.ActualRank != null)
                            {

                                subtext = indexation + AddSpaceIfNotEmpty(indexation) + persondecreeoperation.Fireobject.Type + " по п.п. " + persondecreeoperation.Fireobject.Pointsubpoint
                                    + " Положения о прохождении службы в органах и подразделениях по чрезвычайным ситуациям Республики Беларусь ("
                                    + persondecreeoperation.Fireobject.Description + ")";
                                Text submainText = new Text(subtext);
                                Run submainRun = new Run();
                                Paragraph submainParagraph = new Paragraph();
                                submainRun.Append(submainText);
                                submainParagraph.Append(submainRun);
                                docBody.Append(submainParagraph);
                                AppendFontSize(submainRun, fontSize);
                                AppendParagraphJustify(submainParagraph);
                                AppendParagraphIndentation(submainParagraph, firstLineIndent);

                                AddSpaceDocument(docBody);

                                text = rank + AddSpaceIfNotEmpty(rank) + fio + AddCommaIfNotEmpty(position) + RemoveSpaces(position)
                                    + AddCommaIfNotEmpty(date) + date + ".";

                                Text mainText = new Text(text);
                                Run mainRun = new Run();
                                Paragraph mainParagraph = new Paragraph();
                                mainRun.Append(mainText);
                                mainParagraph.Append(mainRun);
                                docBody.Append(mainParagraph);
                                AppendFontSize(mainRun, fontSize);
                                AppendParagraphJustify(mainParagraph);
                                AppendParagraphIndentation(mainParagraph, firstLineIndent);
                            // Увольняем человека без звания
                            }
                            else if (person != null)
                            {
                                subtext = indexation + AddSpaceIfNotEmpty(indexation) + "по пункту " + persondecreeoperation.Fireobject.Subpoint 
                                    + AddSpaceIfNotEmpty(persondecreeoperation.Fireobject.Subpoint) + "статьи" + AddSpaceIfNotEmpty(persondecreeoperation.Fireobject.Point)
                                    + persondecreeoperation.Fireobject.Point + AddSpaceIfNotEmpty(persondecreeoperation.Fireobject.Point)
                                    + "Трудового кодекса Республики Беларусь ("
                                    + persondecreeoperation.Fireobject.Description + ")";
                                Text submainText = new Text(subtext);
                                Run submainRun = new Run();
                                Paragraph submainParagraph = new Paragraph();
                                submainRun.Append(submainText);
                                submainParagraph.Append(submainRun);
                                docBody.Append(submainParagraph);
                                AppendFontSize(submainRun, fontSize);
                                AppendParagraphJustify(submainParagraph);
                                AppendParagraphIndentation(submainParagraph, firstLineIndent);

                                AddSpaceDocument(docBody);

                                text = fio + AddCommaIfNotEmpty(position) + RemoveSpaces(position)
                                    + AddCommaIfNotEmpty(date) + date + ".";

                                Text mainText = new Text(text);
                                Run mainRun = new Run();
                                Paragraph mainParagraph = new Paragraph();
                                mainRun.Append(mainText);
                                mainParagraph.Append(mainRun);
                                docBody.Append(mainParagraph);
                                AppendFontSize(mainRun, fontSize);
                                AppendParagraphJustify(mainParagraph);
                                AppendParagraphIndentation(mainParagraph, firstLineIndent);
                            }
                            // Выплатить компенсацию
                            if (persondecreeoperation.Optionnumber2 > 0)
                            {
                                string payString = "Выплатить компенсацию за неиспользованные " + persondecreeoperation.Optionnumber2
                                    + " дней основного отпуска за " + persondecreeoperation.Optiondate1.GetValueOrDefault().Year.ToString() + " год.";
                                Text payText = new Text(payString);
                                Run payRun = new Run();
                                Paragraph payParagraph = new Paragraph();
                                payRun.Append(payText);
                                payParagraph.Append(payRun);
                                docBody.Append(payParagraph);
                                AppendFontSize(payRun, fontSize);
                                AppendParagraphJustify(payParagraph);
                                AppendParagraphIndentation(payParagraph, firstLineIndent);
                            }
                            // Удержать денежное довольствие
                            if (persondecreeoperation.Optionnumber3 > 0)
                            {
                                string payString = "Удержать денежное довольствие на " + persondecreeoperation.Optionnumber3
                                    + " дней использованного основного отпуска за " + persondecreeoperation.Optiondate1.GetValueOrDefault().Year.ToString() + " год.";
                                Text payText = new Text(payString);
                                Run payRun = new Run();
                                Paragraph payParagraph = new Paragraph();
                                payRun.Append(payText);
                                payParagraph.Append(payRun);
                                docBody.Append(payParagraph);
                                AppendFontSize(payRun, fontSize);
                                AppendParagraphJustify(payParagraph);
                                AppendParagraphIndentation(payParagraph, firstLineIndent);
                            }

                            // Основание
                            if (persondecreeoperation.Optionstring1.Length > 0)
                            {
                                string reportString = "Основание: ";
                                reportString += persondecreeoperation.Optionstring1;

                                Text reportText = new Text(reportString); ;
                                Run reportRun = new Run();
                                Paragraph reportParagraph = new Paragraph();
                                reportRun.Append(reportText);
                                reportParagraph.Append(reportRun);
                                docBody.Append(reportParagraph);
                                AppendFontSize(reportRun, fontSize);
                                AppendParagraphJustify(reportParagraph);
                                AppendParagraphIndentation(reportParagraph, "0", leftIndent, hangingIndent);
                                AppendParagraphSpacing(reportParagraph, beforeSpace, "0");
                            }
                            AddSpaceDocument(docBody);
                        }
                    }

                    
                }

                // Освободить
                if (persondecreeblock.Persondecreeblocktype == 5)
                {
                    foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperations)
                    {
                        PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                        string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                        int padezh = 4; // Основной падеж сотрудника


                    }
                }

                // Перевести
                if (persondecreeblock.Persondecreeblocktype == 6)
                {
                    foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperations)
                    {
                        PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                        string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                        int padezh = 4; // Основной падеж сотрудника


                    }
                }

                // Прекратить службу
                if (persondecreeblock.Persondecreeblocktype == 7)
                {
                    foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperations)
                    {
                        PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                        string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                        int padezh = 4; // Основной падеж сотрудника


                    }
                }

                // Отстранить
                if (persondecreeblock.Persondecreeblocktype == 8)
                {
                    foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperations)
                    {
                        PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                        string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                        int padezh = 4; // Основной падеж сотрудника


                    }
                }

                // Внести изменения в учетные документы
                if (persondecreeblock.Persondecreeblocktype == 9)
                {
                    foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperations)
                    {
                        PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                        string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                        int padezh = 4; // Основной падеж сотрудника


                    }
                }

                // Установить
                if (persondecreeblock.Persondecreeblocktype == 10)
                {
                    foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperations)
                    {
                        PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                        string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                        int padezh = 4; // Основной падеж сотрудника


                    }
                }

                // Заключить контракты с
                if (persondecreeblock.Persondecreeblocktype == 11)
                {
                    foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperations)
                    {
                        PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                        string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                        int padezh = 4; // Основной падеж сотрудника


                    }
                }

                // Продлить контракты с
                if (persondecreeblock.Persondecreeblocktype == 12)
                {
                    foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperations)
                    {
                        PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                        string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                        int padezh = 4; // Основной падеж сотрудника


                    }
                }

                // Выплатить денежную компенсацию
                if (persondecreeblock.Persondecreeblocktype == 13)
                {
                    foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperations)
                    {
                        PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                        string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                        int padezh = 4; // Основной падеж сотрудника


                    }
                }

                // Присвоить
                if (persondecreeblock.Persondecreeblocktype == 14)
                {
                    foreach (PersondecreeoperationManagement persondecreeoperation in persondecreeoperations)
                    {
                        PersonManager person = repository.GetPersonManager(user, persondecreeoperation.Person);
                        string nonperson = persondecreeoperation.Nonperson; // Человек "со стороны" (не имеет ЭЛД)
                        int padezh = 4; // Основной падеж сотрудника


                    }
                }

                
            }

            mainPart.Document.Save();
            wordDocument.Close();

            mem.Position = 0;
            return mem;
        }

        /// <summary>
        /// Добавляет пустой абзац в документ
        /// </summary>
        /// <param name="docBody"></param>
        public void AddSpaceDocument(Body docBody)
        {
            Text spaceText = new Text("");
            Run spaceRun = new Run();
            Paragraph spaceParagraph = new Paragraph();
            spaceRun.Append(spaceText);
            spaceParagraph.Append(spaceRun);
            docBody.Append(spaceParagraph);
            AppendFontSize(spaceRun, fontSize);
            AppendParagraphFontsize(spaceParagraph, fontSize);
        }

        /// <summary>
        /// Возвращает пробел, если строка на вводе не пустая. Возвращает "", если строка пуста или равна нулю
        /// Если объект str2 присутствует, то возвращает пробел, если ОБЕ строки не пусты
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string AddSpaceIfNotEmpty(string str, string str2 = null)
        {
            if (str2 == null)
            {
                if (str == null || str.Length == 0)
                {
                    return "";
                }
                return " ";
            }
            if (str == null || str.Length == 0 || str2.Length == 0)
            {
                return "";
            }
            return " ";
        }

        /// <summary>
        /// Возвращает пробел, если число на вводе равно 0. Возвращает "", если число равно нулю
        /// Если число str2 присутствует, то возвращает пробел, если ОБА числа 0
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public string AddSpaceIfNotEmpty(int str, int str2 = 0)
        {
            if (str2 == 0)
            {
                if (str == 0)
                {
                    return "";
                }
                return " ";
            }
            if (str == 0 || str2 == 0)
            {
                return "";
            }
            return " ";
        }

        /// <summary>
        /// Добавляет запятую с пробелом, если строка на вводе не пустая. Возвращает "", если строка пустя или равна нулю
        /// Если объект str2 присутствует, то возвращает пробел, если ОБЕ строки не пусты
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public string AddCommaIfNotEmpty(string str, string str2 = null)
        {
            if (str2 == null)
            {
                if (str == null || str.Length == 0)
                {
                    return "";
                }
                return ", ";
            }
            if (str == null || str.Length == 0 || str2.Length == 0)
            {
                return "";
            }
            return ", ";
        }

        /// <summary>
        /// Удаляет ненужные пробелы: двойные, перед строкой и после строки.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string RemoveSpaces(string str)
        {
            if (str == null)
            {
                return "";
            }
            str = str.Trim();
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            str = regex.Replace(str, " ");
            return str;
        }

        public void AppendFontDefault(RunProperties runProperties)
        {
            // Main font
            RunFonts font = new RunFonts() { HighAnsi = "Times New Roman" };
            runProperties.Append(font);
        }

        public void AppendFontDefault(Run run)
        {
            // Main font
            if (run.RunProperties == null)
            {
                RunProperties runProperties = new RunProperties();
                run.PrependChild<RunProperties>(runProperties);
            }
            AppendFontDefault(run.RunProperties);
        }

        public void AppendFontSize(RunProperties runProperties, string size)
        {
            runProperties.FontSize = new FontSize();
            runProperties.FontSize.Val = new StringValue(size);

            runProperties.FontSizeComplexScript = new FontSizeComplexScript();
            runProperties.FontSizeComplexScript.Val = new StringValue(size);
        }

        public void AppendFontSize(Run run, string size)
        {
            // Main font
            if (run.RunProperties == null)
            {
                RunProperties runProperties = new RunProperties();
                run.PrependChild<RunProperties>(runProperties);
            }
            AppendFontSize(run.RunProperties, size);
        }

        public void AppendBold(RunProperties runProperties)
        {
            runProperties.Bold = new Bold();

            //RunFonts font = new RunFonts() { HighAnsi = "Times New Roman" };
            //runProperties.Append(font);
        }

        public void AppendBold(Run run)
        {
            // Main font
            if (run.RunProperties == null)
            {
                RunProperties runProperties = new RunProperties();
                run.PrependChild<RunProperties>(runProperties);
            }
            AppendBold(run.RunProperties);
        }

        public void AppendParagraphCenter(ParagraphProperties paragraphProperties)
        {
            // Main font
            paragraphProperties.Append(new Justification() { Val = JustificationValues.Center });
        }

        public void AppendParagraphCenter(Paragraph paragraph)
        {
            // Main font
            if (paragraph.ParagraphProperties == null)
            {
                ParagraphProperties paragraphProperties = new ParagraphProperties();
                paragraph.PrependChild<ParagraphProperties>(paragraphProperties);
            }
            AppendParagraphCenter(paragraph.ParagraphProperties);
        }

        public void AppendParagraphJustify(ParagraphProperties paragraphProperties)
        {
            // Main font
            paragraphProperties.Append(new Justification() { Val = JustificationValues.Both });
        }

        public void AppendParagraphJustify(Paragraph paragraph)
        {
            // Main font
            if (paragraph.ParagraphProperties == null)
            {
                ParagraphProperties paragraphProperties = new ParagraphProperties();
                paragraph.PrependChild<ParagraphProperties>(paragraphProperties);
            }
            AppendParagraphJustify(paragraph.ParagraphProperties);
        }

        public void AppendParagraphIndentation(ParagraphProperties paragraphProperties, string firstLine, string left = null, string hanging = null)
        {
            Indentation indentation = null;
            if (left == null && hanging == null)
            {
                indentation = new Indentation { FirstLine = firstLine};
            } else
            {
                if (left == null)
                {
                    left = "0";
                }
                if (hanging == null)
                {
                    hanging = "0";
                }
                indentation = new Indentation { FirstLine = firstLine, Left = left, Hanging = hanging };
            }
            

            
            // Main font
            paragraphProperties.Append(indentation);
        }

        public void AppendParagraphIndentation(Paragraph paragraph, string firstLine, string left = null, string hanging = null)
        {
            // Main font
            if (paragraph.ParagraphProperties == null)
            {
                ParagraphProperties paragraphProperties = new ParagraphProperties();
                paragraph.PrependChild<ParagraphProperties>(paragraphProperties);
            }
            AppendParagraphIndentation(paragraph.ParagraphProperties, firstLine, left , hanging);
        }

        public void AppendParagraphFontsize(ParagraphProperties paragraphProperties, string size)
        {

            ParagraphMarkRunProperties paragraphMarkRunProperties10 = new ParagraphMarkRunProperties();
            Caps caps1 = new Caps();
            FontSize fontSize16 = new FontSize() { Val = "30" };
            FontSizeComplexScript fontSizeComplexScript16 = new FontSizeComplexScript() { Val = "30" };

            paragraphMarkRunProperties10.Append(caps1);
            paragraphMarkRunProperties10.Append(fontSize16);
            paragraphMarkRunProperties10.Append(fontSizeComplexScript16);

            paragraphProperties.Append(paragraphMarkRunProperties10);
        }

        public void AppendParagraphFontsize(Paragraph paragraph, string size)
        {
            // Main font
            if (paragraph.ParagraphProperties == null)
            {
                ParagraphProperties paragraphProperties = new ParagraphProperties();
                paragraph.PrependChild<ParagraphProperties>(paragraphProperties);
            }
            AppendParagraphFontsize(paragraph.ParagraphProperties, size);
        }

        public void AppendParagraphSpacing(ParagraphProperties paragraphProperties, string before, string after)
        {
            if (before == null)
            {
                before = "0";
            }
            if (after == null)
            {
                after = "0";
            }
            SpacingBetweenLines spacingBetweenLines = new SpacingBetweenLines() { Before = before, After = after };
            // Main font
            paragraphProperties.Append(spacingBetweenLines);
        }

        public void AppendParagraphSpacing(Paragraph paragraph, string before, string after)
        {
            // Main font
            if (paragraph.ParagraphProperties == null)
            {
                ParagraphProperties paragraphProperties = new ParagraphProperties();
                paragraph.PrependChild<ParagraphProperties>(paragraphProperties);
            }
            AppendParagraphSpacing(paragraph.ParagraphProperties, before, after);
        }

        //public void AppendParagraphIndentation(ParagraphProperties paragraphProperties, string left, string right, string hanging)
        //{
        //    Indentation indentation = new Indentation { Left = left, Right = right, Hanging = hanging };
        //    // Main font
        //    paragraphProperties.Append(indentation);
        //}

        //public void AppendParagraphIndentation(Paragraph paragraph, string left, string right, string hanging)
        //{
        //    // Main font
        //    if (paragraph.ParagraphProperties == null)
        //    {
        //        ParagraphProperties paragraphProperties = new ParagraphProperties();
        //        paragraph.PrependChild<ParagraphProperties>(paragraphProperties);
        //    }
        //    AppendParagraphIndentation(paragraph.ParagraphProperties, left, right, hanging);
        //}



        //////////////////////////////////////////////// МУСОР
        ///

        // Generates content of themePart1.
        private void GenerateThemePart1Content(ThemePart themePart1)
        {
            A.Theme theme1 = new A.Theme() { Name = "Тема Office" };
            theme1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            A.ThemeElements themeElements1 = new A.ThemeElements();

            A.ColorScheme colorScheme1 = new A.ColorScheme() { Name = "Стандартная" };

            A.Dark1Color dark1Color1 = new A.Dark1Color();
            A.SystemColor systemColor1 = new A.SystemColor() { Val = A.SystemColorValues.WindowText, LastColor = "000000" };

            dark1Color1.Append(systemColor1);

            A.Light1Color light1Color1 = new A.Light1Color();
            A.SystemColor systemColor2 = new A.SystemColor() { Val = A.SystemColorValues.Window, LastColor = "FFFFFF" };

            light1Color1.Append(systemColor2);

            A.Dark2Color dark2Color1 = new A.Dark2Color();
            A.RgbColorModelHex rgbColorModelHex4 = new A.RgbColorModelHex() { Val = "44546A" };

            dark2Color1.Append(rgbColorModelHex4);

            A.Light2Color light2Color1 = new A.Light2Color();
            A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex() { Val = "E7E6E6" };

            light2Color1.Append(rgbColorModelHex5);

            A.Accent1Color accent1Color1 = new A.Accent1Color();
            A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex() { Val = "5B9BD5" };

            accent1Color1.Append(rgbColorModelHex6);

            A.Accent2Color accent2Color1 = new A.Accent2Color();
            A.RgbColorModelHex rgbColorModelHex7 = new A.RgbColorModelHex() { Val = "ED7D31" };

            accent2Color1.Append(rgbColorModelHex7);

            A.Accent3Color accent3Color1 = new A.Accent3Color();
            A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex() { Val = "A5A5A5" };

            accent3Color1.Append(rgbColorModelHex8);

            A.Accent4Color accent4Color1 = new A.Accent4Color();
            A.RgbColorModelHex rgbColorModelHex9 = new A.RgbColorModelHex() { Val = "FFC000" };

            accent4Color1.Append(rgbColorModelHex9);

            A.Accent5Color accent5Color1 = new A.Accent5Color();
            A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex() { Val = "4472C4" };

            accent5Color1.Append(rgbColorModelHex10);

            A.Accent6Color accent6Color1 = new A.Accent6Color();
            A.RgbColorModelHex rgbColorModelHex11 = new A.RgbColorModelHex() { Val = "70AD47" };

            accent6Color1.Append(rgbColorModelHex11);

            A.Hyperlink hyperlink1 = new A.Hyperlink();
            A.RgbColorModelHex rgbColorModelHex12 = new A.RgbColorModelHex() { Val = "0563C1" };

            hyperlink1.Append(rgbColorModelHex12);

            A.FollowedHyperlinkColor followedHyperlinkColor1 = new A.FollowedHyperlinkColor();
            A.RgbColorModelHex rgbColorModelHex13 = new A.RgbColorModelHex() { Val = "954F72" };

            followedHyperlinkColor1.Append(rgbColorModelHex13);

            colorScheme1.Append(dark1Color1);
            colorScheme1.Append(light1Color1);
            colorScheme1.Append(dark2Color1);
            colorScheme1.Append(light2Color1);
            colorScheme1.Append(accent1Color1);
            colorScheme1.Append(accent2Color1);
            colorScheme1.Append(accent3Color1);
            colorScheme1.Append(accent4Color1);
            colorScheme1.Append(accent5Color1);
            colorScheme1.Append(accent6Color1);
            colorScheme1.Append(hyperlink1);
            colorScheme1.Append(followedHyperlinkColor1);

            A.FontScheme fontScheme1 = new A.FontScheme() { Name = "Стандартная" };

            A.MajorFont majorFont1 = new A.MajorFont();
            A.LatinFont latinFont1 = new A.LatinFont() { Typeface = "Calibri Light", Panose = "020F0302020204030204" };
            A.EastAsianFont eastAsianFont1 = new A.EastAsianFont() { Typeface = "" };
            A.ComplexScriptFont complexScriptFont1 = new A.ComplexScriptFont() { Typeface = "" };
            A.SupplementalFont supplementalFont1 = new A.SupplementalFont() { Script = "Jpan", Typeface = "ＭＳ ゴシック" };
            A.SupplementalFont supplementalFont2 = new A.SupplementalFont() { Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont3 = new A.SupplementalFont() { Script = "Hans", Typeface = "宋体" };
            A.SupplementalFont supplementalFont4 = new A.SupplementalFont() { Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont5 = new A.SupplementalFont() { Script = "Arab", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont6 = new A.SupplementalFont() { Script = "Hebr", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont7 = new A.SupplementalFont() { Script = "Thai", Typeface = "Angsana New" };
            A.SupplementalFont supplementalFont8 = new A.SupplementalFont() { Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont9 = new A.SupplementalFont() { Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont10 = new A.SupplementalFont() { Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont11 = new A.SupplementalFont() { Script = "Khmr", Typeface = "MoolBoran" };
            A.SupplementalFont supplementalFont12 = new A.SupplementalFont() { Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont13 = new A.SupplementalFont() { Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont14 = new A.SupplementalFont() { Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont15 = new A.SupplementalFont() { Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont16 = new A.SupplementalFont() { Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont17 = new A.SupplementalFont() { Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont18 = new A.SupplementalFont() { Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont19 = new A.SupplementalFont() { Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont20 = new A.SupplementalFont() { Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont21 = new A.SupplementalFont() { Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont22 = new A.SupplementalFont() { Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont23 = new A.SupplementalFont() { Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont24 = new A.SupplementalFont() { Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont25 = new A.SupplementalFont() { Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont26 = new A.SupplementalFont() { Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont27 = new A.SupplementalFont() { Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont28 = new A.SupplementalFont() { Script = "Viet", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont29 = new A.SupplementalFont() { Script = "Uigh", Typeface = "Microsoft Uighur" };
            A.SupplementalFont supplementalFont30 = new A.SupplementalFont() { Script = "Geor", Typeface = "Sylfaen" };

            majorFont1.Append(latinFont1);
            majorFont1.Append(eastAsianFont1);
            majorFont1.Append(complexScriptFont1);
            majorFont1.Append(supplementalFont1);
            majorFont1.Append(supplementalFont2);
            majorFont1.Append(supplementalFont3);
            majorFont1.Append(supplementalFont4);
            majorFont1.Append(supplementalFont5);
            majorFont1.Append(supplementalFont6);
            majorFont1.Append(supplementalFont7);
            majorFont1.Append(supplementalFont8);
            majorFont1.Append(supplementalFont9);
            majorFont1.Append(supplementalFont10);
            majorFont1.Append(supplementalFont11);
            majorFont1.Append(supplementalFont12);
            majorFont1.Append(supplementalFont13);
            majorFont1.Append(supplementalFont14);
            majorFont1.Append(supplementalFont15);
            majorFont1.Append(supplementalFont16);
            majorFont1.Append(supplementalFont17);
            majorFont1.Append(supplementalFont18);
            majorFont1.Append(supplementalFont19);
            majorFont1.Append(supplementalFont20);
            majorFont1.Append(supplementalFont21);
            majorFont1.Append(supplementalFont22);
            majorFont1.Append(supplementalFont23);
            majorFont1.Append(supplementalFont24);
            majorFont1.Append(supplementalFont25);
            majorFont1.Append(supplementalFont26);
            majorFont1.Append(supplementalFont27);
            majorFont1.Append(supplementalFont28);
            majorFont1.Append(supplementalFont29);
            majorFont1.Append(supplementalFont30);

            A.MinorFont minorFont1 = new A.MinorFont();
            A.LatinFont latinFont2 = new A.LatinFont() { Typeface = "Calibri", Panose = "020F0502020204030204" };
            A.EastAsianFont eastAsianFont2 = new A.EastAsianFont() { Typeface = "" };
            A.ComplexScriptFont complexScriptFont2 = new A.ComplexScriptFont() { Typeface = "" };
            A.SupplementalFont supplementalFont31 = new A.SupplementalFont() { Script = "Jpan", Typeface = "ＭＳ 明朝" };
            A.SupplementalFont supplementalFont32 = new A.SupplementalFont() { Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont33 = new A.SupplementalFont() { Script = "Hans", Typeface = "宋体" };
            A.SupplementalFont supplementalFont34 = new A.SupplementalFont() { Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont35 = new A.SupplementalFont() { Script = "Arab", Typeface = "Arial" };
            A.SupplementalFont supplementalFont36 = new A.SupplementalFont() { Script = "Hebr", Typeface = "Arial" };
            A.SupplementalFont supplementalFont37 = new A.SupplementalFont() { Script = "Thai", Typeface = "Cordia New" };
            A.SupplementalFont supplementalFont38 = new A.SupplementalFont() { Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont39 = new A.SupplementalFont() { Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont40 = new A.SupplementalFont() { Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont41 = new A.SupplementalFont() { Script = "Khmr", Typeface = "DaunPenh" };
            A.SupplementalFont supplementalFont42 = new A.SupplementalFont() { Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont43 = new A.SupplementalFont() { Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont44 = new A.SupplementalFont() { Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont45 = new A.SupplementalFont() { Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont46 = new A.SupplementalFont() { Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont47 = new A.SupplementalFont() { Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont48 = new A.SupplementalFont() { Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont49 = new A.SupplementalFont() { Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont50 = new A.SupplementalFont() { Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont51 = new A.SupplementalFont() { Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont52 = new A.SupplementalFont() { Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont53 = new A.SupplementalFont() { Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont54 = new A.SupplementalFont() { Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont55 = new A.SupplementalFont() { Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont56 = new A.SupplementalFont() { Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont57 = new A.SupplementalFont() { Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont58 = new A.SupplementalFont() { Script = "Viet", Typeface = "Arial" };
            A.SupplementalFont supplementalFont59 = new A.SupplementalFont() { Script = "Uigh", Typeface = "Microsoft Uighur" };
            A.SupplementalFont supplementalFont60 = new A.SupplementalFont() { Script = "Geor", Typeface = "Sylfaen" };

            minorFont1.Append(latinFont2);
            minorFont1.Append(eastAsianFont2);
            minorFont1.Append(complexScriptFont2);
            minorFont1.Append(supplementalFont31);
            minorFont1.Append(supplementalFont32);
            minorFont1.Append(supplementalFont33);
            minorFont1.Append(supplementalFont34);
            minorFont1.Append(supplementalFont35);
            minorFont1.Append(supplementalFont36);
            minorFont1.Append(supplementalFont37);
            minorFont1.Append(supplementalFont38);
            minorFont1.Append(supplementalFont39);
            minorFont1.Append(supplementalFont40);
            minorFont1.Append(supplementalFont41);
            minorFont1.Append(supplementalFont42);
            minorFont1.Append(supplementalFont43);
            minorFont1.Append(supplementalFont44);
            minorFont1.Append(supplementalFont45);
            minorFont1.Append(supplementalFont46);
            minorFont1.Append(supplementalFont47);
            minorFont1.Append(supplementalFont48);
            minorFont1.Append(supplementalFont49);
            minorFont1.Append(supplementalFont50);
            minorFont1.Append(supplementalFont51);
            minorFont1.Append(supplementalFont52);
            minorFont1.Append(supplementalFont53);
            minorFont1.Append(supplementalFont54);
            minorFont1.Append(supplementalFont55);
            minorFont1.Append(supplementalFont56);
            minorFont1.Append(supplementalFont57);
            minorFont1.Append(supplementalFont58);
            minorFont1.Append(supplementalFont59);
            minorFont1.Append(supplementalFont60);

            fontScheme1.Append(majorFont1);
            fontScheme1.Append(minorFont1);

            A.FormatScheme formatScheme1 = new A.FormatScheme() { Name = "Стандартная" };

            A.FillStyleList fillStyleList1 = new A.FillStyleList();

            A.SolidFill solidFill3 = new A.SolidFill();
            A.SchemeColor schemeColor1 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill3.Append(schemeColor1);

            A.GradientFill gradientFill1 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList1 = new A.GradientStopList();

            A.GradientStop gradientStop1 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor2 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.LuminanceModulation luminanceModulation1 = new A.LuminanceModulation() { Val = 110000 };
            A.SaturationModulation saturationModulation1 = new A.SaturationModulation() { Val = 105000 };
            A.Tint tint1 = new A.Tint() { Val = 67000 };

            schemeColor2.Append(luminanceModulation1);
            schemeColor2.Append(saturationModulation1);
            schemeColor2.Append(tint1);

            gradientStop1.Append(schemeColor2);

            A.GradientStop gradientStop2 = new A.GradientStop() { Position = 50000 };

            A.SchemeColor schemeColor3 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.LuminanceModulation luminanceModulation2 = new A.LuminanceModulation() { Val = 105000 };
            A.SaturationModulation saturationModulation2 = new A.SaturationModulation() { Val = 103000 };
            A.Tint tint2 = new A.Tint() { Val = 73000 };

            schemeColor3.Append(luminanceModulation2);
            schemeColor3.Append(saturationModulation2);
            schemeColor3.Append(tint2);

            gradientStop2.Append(schemeColor3);

            A.GradientStop gradientStop3 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor4 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.LuminanceModulation luminanceModulation3 = new A.LuminanceModulation() { Val = 105000 };
            A.SaturationModulation saturationModulation3 = new A.SaturationModulation() { Val = 109000 };
            A.Tint tint3 = new A.Tint() { Val = 81000 };

            schemeColor4.Append(luminanceModulation3);
            schemeColor4.Append(saturationModulation3);
            schemeColor4.Append(tint3);

            gradientStop3.Append(schemeColor4);

            gradientStopList1.Append(gradientStop1);
            gradientStopList1.Append(gradientStop2);
            gradientStopList1.Append(gradientStop3);
            A.LinearGradientFill linearGradientFill1 = new A.LinearGradientFill() { Angle = 5400000, Scaled = false };

            gradientFill1.Append(gradientStopList1);
            gradientFill1.Append(linearGradientFill1);

            A.GradientFill gradientFill2 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList2 = new A.GradientStopList();

            A.GradientStop gradientStop4 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor5 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.SaturationModulation saturationModulation4 = new A.SaturationModulation() { Val = 103000 };
            A.LuminanceModulation luminanceModulation4 = new A.LuminanceModulation() { Val = 102000 };
            A.Tint tint4 = new A.Tint() { Val = 94000 };

            schemeColor5.Append(saturationModulation4);
            schemeColor5.Append(luminanceModulation4);
            schemeColor5.Append(tint4);

            gradientStop4.Append(schemeColor5);

            A.GradientStop gradientStop5 = new A.GradientStop() { Position = 50000 };

            A.SchemeColor schemeColor6 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.SaturationModulation saturationModulation5 = new A.SaturationModulation() { Val = 110000 };
            A.LuminanceModulation luminanceModulation5 = new A.LuminanceModulation() { Val = 100000 };
            A.Shade shade1 = new A.Shade() { Val = 100000 };

            schemeColor6.Append(saturationModulation5);
            schemeColor6.Append(luminanceModulation5);
            schemeColor6.Append(shade1);

            gradientStop5.Append(schemeColor6);

            A.GradientStop gradientStop6 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor7 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.LuminanceModulation luminanceModulation6 = new A.LuminanceModulation() { Val = 99000 };
            A.SaturationModulation saturationModulation6 = new A.SaturationModulation() { Val = 120000 };
            A.Shade shade2 = new A.Shade() { Val = 78000 };

            schemeColor7.Append(luminanceModulation6);
            schemeColor7.Append(saturationModulation6);
            schemeColor7.Append(shade2);

            gradientStop6.Append(schemeColor7);

            gradientStopList2.Append(gradientStop4);
            gradientStopList2.Append(gradientStop5);
            gradientStopList2.Append(gradientStop6);
            A.LinearGradientFill linearGradientFill2 = new A.LinearGradientFill() { Angle = 5400000, Scaled = false };

            gradientFill2.Append(gradientStopList2);
            gradientFill2.Append(linearGradientFill2);

            fillStyleList1.Append(solidFill3);
            fillStyleList1.Append(gradientFill1);
            fillStyleList1.Append(gradientFill2);

            A.LineStyleList lineStyleList1 = new A.LineStyleList();

            A.Outline outline2 = new A.Outline() { Width = 6350, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill4 = new A.SolidFill();
            A.SchemeColor schemeColor8 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill4.Append(schemeColor8);
            A.PresetDash presetDash1 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };
            A.Miter miter2 = new A.Miter() { Limit = 800000 };

            outline2.Append(solidFill4);
            outline2.Append(presetDash1);
            outline2.Append(miter2);

            A.Outline outline3 = new A.Outline() { Width = 12700, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill5 = new A.SolidFill();
            A.SchemeColor schemeColor9 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill5.Append(schemeColor9);
            A.PresetDash presetDash2 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };
            A.Miter miter3 = new A.Miter() { Limit = 800000 };

            outline3.Append(solidFill5);
            outline3.Append(presetDash2);
            outline3.Append(miter3);

            A.Outline outline4 = new A.Outline() { Width = 19050, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill6 = new A.SolidFill();
            A.SchemeColor schemeColor10 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill6.Append(schemeColor10);
            A.PresetDash presetDash3 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };
            A.Miter miter4 = new A.Miter() { Limit = 800000 };

            outline4.Append(solidFill6);
            outline4.Append(presetDash3);
            outline4.Append(miter4);

            lineStyleList1.Append(outline2);
            lineStyleList1.Append(outline3);
            lineStyleList1.Append(outline4);

            A.EffectStyleList effectStyleList1 = new A.EffectStyleList();

            A.EffectStyle effectStyle1 = new A.EffectStyle();
            A.EffectList effectList3 = new A.EffectList();

            effectStyle1.Append(effectList3);

            A.EffectStyle effectStyle2 = new A.EffectStyle();
            A.EffectList effectList4 = new A.EffectList();

            effectStyle2.Append(effectList4);

            A.EffectStyle effectStyle3 = new A.EffectStyle();

            A.EffectList effectList5 = new A.EffectList();

            A.OuterShadow outerShadow2 = new A.OuterShadow() { BlurRadius = 57150L, Distance = 19050L, Direction = 5400000, Alignment = A.RectangleAlignmentValues.Center, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex14 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha1 = new A.Alpha() { Val = 63000 };

            rgbColorModelHex14.Append(alpha1);

            outerShadow2.Append(rgbColorModelHex14);

            effectList5.Append(outerShadow2);

            effectStyle3.Append(effectList5);

            effectStyleList1.Append(effectStyle1);
            effectStyleList1.Append(effectStyle2);
            effectStyleList1.Append(effectStyle3);

            A.BackgroundFillStyleList backgroundFillStyleList1 = new A.BackgroundFillStyleList();

            A.SolidFill solidFill7 = new A.SolidFill();
            A.SchemeColor schemeColor11 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill7.Append(schemeColor11);

            A.SolidFill solidFill8 = new A.SolidFill();

            A.SchemeColor schemeColor12 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint5 = new A.Tint() { Val = 95000 };
            A.SaturationModulation saturationModulation7 = new A.SaturationModulation() { Val = 170000 };

            schemeColor12.Append(tint5);
            schemeColor12.Append(saturationModulation7);

            solidFill8.Append(schemeColor12);

            A.GradientFill gradientFill3 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList3 = new A.GradientStopList();

            A.GradientStop gradientStop7 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor13 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint6 = new A.Tint() { Val = 93000 };
            A.SaturationModulation saturationModulation8 = new A.SaturationModulation() { Val = 150000 };
            A.Shade shade3 = new A.Shade() { Val = 98000 };
            A.LuminanceModulation luminanceModulation7 = new A.LuminanceModulation() { Val = 102000 };

            schemeColor13.Append(tint6);
            schemeColor13.Append(saturationModulation8);
            schemeColor13.Append(shade3);
            schemeColor13.Append(luminanceModulation7);

            gradientStop7.Append(schemeColor13);

            A.GradientStop gradientStop8 = new A.GradientStop() { Position = 50000 };

            A.SchemeColor schemeColor14 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint7 = new A.Tint() { Val = 98000 };
            A.SaturationModulation saturationModulation9 = new A.SaturationModulation() { Val = 130000 };
            A.Shade shade4 = new A.Shade() { Val = 90000 };
            A.LuminanceModulation luminanceModulation8 = new A.LuminanceModulation() { Val = 103000 };

            schemeColor14.Append(tint7);
            schemeColor14.Append(saturationModulation9);
            schemeColor14.Append(shade4);
            schemeColor14.Append(luminanceModulation8);

            gradientStop8.Append(schemeColor14);

            A.GradientStop gradientStop9 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor15 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade5 = new A.Shade() { Val = 63000 };
            A.SaturationModulation saturationModulation10 = new A.SaturationModulation() { Val = 120000 };

            schemeColor15.Append(shade5);
            schemeColor15.Append(saturationModulation10);

            gradientStop9.Append(schemeColor15);

            gradientStopList3.Append(gradientStop7);
            gradientStopList3.Append(gradientStop8);
            gradientStopList3.Append(gradientStop9);
            A.LinearGradientFill linearGradientFill3 = new A.LinearGradientFill() { Angle = 5400000, Scaled = false };

            gradientFill3.Append(gradientStopList3);
            gradientFill3.Append(linearGradientFill3);

            backgroundFillStyleList1.Append(solidFill7);
            backgroundFillStyleList1.Append(solidFill8);
            backgroundFillStyleList1.Append(gradientFill3);

            formatScheme1.Append(fillStyleList1);
            formatScheme1.Append(lineStyleList1);
            formatScheme1.Append(effectStyleList1);
            formatScheme1.Append(backgroundFillStyleList1);

            themeElements1.Append(colorScheme1);
            themeElements1.Append(fontScheme1);
            themeElements1.Append(formatScheme1);
            A.ObjectDefaults objectDefaults1 = new A.ObjectDefaults();
            A.ExtraColorSchemeList extraColorSchemeList1 = new A.ExtraColorSchemeList();

            A.OfficeStyleSheetExtensionList officeStyleSheetExtensionList1 = new A.OfficeStyleSheetExtensionList();

            A.OfficeStyleSheetExtension officeStyleSheetExtension1 = new A.OfficeStyleSheetExtension() { Uri = "{05A4C25C-085E-4340-85A3-A5531E510DB2}" };

            Thm15.ThemeFamily themeFamily1 = new Thm15.ThemeFamily() { Name = "Office Theme", Id = "{62F939B6-93AF-4DB8-9C6B-D6C7DFDC589F}", Vid = "{4A3C46E8-61CC-4603-A589-7422A47A8E4A}" };
            themeFamily1.AddNamespaceDeclaration("thm15", "http://schemas.microsoft.com/office/thememl/2012/main");

            officeStyleSheetExtension1.Append(themeFamily1);

            officeStyleSheetExtensionList1.Append(officeStyleSheetExtension1);

            theme1.Append(themeElements1);
            theme1.Append(objectDefaults1);
            theme1.Append(extraColorSchemeList1);
            theme1.Append(officeStyleSheetExtensionList1);

            themePart1.Theme = theme1;
        }

        // Generates content of fontTablePart1.
        private void GenerateFontTablePart1Content(FontTablePart fontTablePart1)
        {
            Fonts fonts1 = new Fonts() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14 w15" } };
            fonts1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            fonts1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            fonts1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            fonts1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
            fonts1.AddNamespaceDeclaration("w15", "http://schemas.microsoft.com/office/word/2012/wordml");

            Font font1 = new Font() { Name = "Times New Roman" };
            Panose1Number panose1Number1 = new Panose1Number() { Val = "02020603050405020304" };
            FontCharSet fontCharSet1 = new FontCharSet() { Val = "CC" };
            FontFamily fontFamily1 = new FontFamily() { Val = FontFamilyValues.Roman };
            Pitch pitch1 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature1 = new FontSignature() { UnicodeSignature0 = "E0002AFF", UnicodeSignature1 = "C0007841", UnicodeSignature2 = "00000009", UnicodeSignature3 = "00000000", CodePageSignature0 = "000001FF", CodePageSignature1 = "00000000" };

            font1.Append(panose1Number1);
            font1.Append(fontCharSet1);
            font1.Append(fontFamily1);
            font1.Append(pitch1);
            font1.Append(fontSignature1);

            Font font2 = new Font() { Name = "Arial" };
            Panose1Number panose1Number2 = new Panose1Number() { Val = "020B0604020202020204" };
            FontCharSet fontCharSet2 = new FontCharSet() { Val = "CC" };
            FontFamily fontFamily2 = new FontFamily() { Val = FontFamilyValues.Swiss };
            Pitch pitch2 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature2 = new FontSignature() { UnicodeSignature0 = "E0002AFF", UnicodeSignature1 = "C0007843", UnicodeSignature2 = "00000009", UnicodeSignature3 = "00000000", CodePageSignature0 = "000001FF", CodePageSignature1 = "00000000" };

            font2.Append(panose1Number2);
            font2.Append(fontCharSet2);
            font2.Append(fontFamily2);
            font2.Append(pitch2);
            font2.Append(fontSignature2);

            Font font3 = new Font() { Name = "Tahoma" };
            Panose1Number panose1Number3 = new Panose1Number() { Val = "020B0604030504040204" };
            FontCharSet fontCharSet3 = new FontCharSet() { Val = "CC" };
            FontFamily fontFamily3 = new FontFamily() { Val = FontFamilyValues.Swiss };
            Pitch pitch3 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature3 = new FontSignature() { UnicodeSignature0 = "E1002EFF", UnicodeSignature1 = "C000605B", UnicodeSignature2 = "00000029", UnicodeSignature3 = "00000000", CodePageSignature0 = "000101FF", CodePageSignature1 = "00000000" };

            font3.Append(panose1Number3);
            font3.Append(fontCharSet3);
            font3.Append(fontFamily3);
            font3.Append(pitch3);
            font3.Append(fontSignature3);

            Font font4 = new Font() { Name = "Calibri Light" };
            Panose1Number panose1Number4 = new Panose1Number() { Val = "020F0302020204030204" };
            FontCharSet fontCharSet4 = new FontCharSet() { Val = "CC" };
            FontFamily fontFamily4 = new FontFamily() { Val = FontFamilyValues.Swiss };
            Pitch pitch4 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature4 = new FontSignature() { UnicodeSignature0 = "A00002EF", UnicodeSignature1 = "4000207B", UnicodeSignature2 = "00000000", UnicodeSignature3 = "00000000", CodePageSignature0 = "0000019F", CodePageSignature1 = "00000000" };

            font4.Append(panose1Number4);
            font4.Append(fontCharSet4);
            font4.Append(fontFamily4);
            font4.Append(pitch4);
            font4.Append(fontSignature4);

            Font font5 = new Font() { Name = "Calibri" };
            Panose1Number panose1Number5 = new Panose1Number() { Val = "020F0502020204030204" };
            FontCharSet fontCharSet5 = new FontCharSet() { Val = "CC" };
            FontFamily fontFamily5 = new FontFamily() { Val = FontFamilyValues.Swiss };
            Pitch pitch5 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature5 = new FontSignature() { UnicodeSignature0 = "E10002FF", UnicodeSignature1 = "4000ACFF", UnicodeSignature2 = "00000009", UnicodeSignature3 = "00000000", CodePageSignature0 = "0000019F", CodePageSignature1 = "00000000" };

            font5.Append(panose1Number5);
            font5.Append(fontCharSet5);
            font5.Append(fontFamily5);
            font5.Append(pitch5);
            font5.Append(fontSignature5);

            fonts1.Append(font1);
            fonts1.Append(font2);
            fonts1.Append(font3);
            fonts1.Append(font4);
            fonts1.Append(font5);

            fontTablePart1.Fonts = fonts1;
        }

        // Generates content of styleDefinitionsPart1.
        private void GenerateStyleDefinitionsPart1Content(StyleDefinitionsPart styleDefinitionsPart1)
        {
            Styles styles1 = new Styles() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14 w15" } };
            styles1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            styles1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            styles1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            styles1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
            styles1.AddNamespaceDeclaration("w15", "http://schemas.microsoft.com/office/word/2012/wordml");

            DocDefaults docDefaults1 = new DocDefaults();

            RunPropertiesDefault runPropertiesDefault1 = new RunPropertiesDefault();

            RunPropertiesBaseStyle runPropertiesBaseStyle1 = new RunPropertiesBaseStyle();
            RunFonts runFonts1 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            Languages languages4 = new Languages() { Val = "ru-RU", EastAsia = "ru-RU", Bidi = "ar-SA" };

            runPropertiesBaseStyle1.Append(runFonts1);
            runPropertiesBaseStyle1.Append(languages4);

            runPropertiesDefault1.Append(runPropertiesBaseStyle1);
            ParagraphPropertiesDefault paragraphPropertiesDefault1 = new ParagraphPropertiesDefault();

            docDefaults1.Append(runPropertiesDefault1);
            docDefaults1.Append(paragraphPropertiesDefault1);

            LatentStyles latentStyles1 = new LatentStyles() { DefaultLockedState = false, DefaultUiPriority = 0, DefaultSemiHidden = false, DefaultUnhideWhenUsed = false, DefaultPrimaryStyle = false, Count = 371 };
            LatentStyleExceptionInfo latentStyleExceptionInfo1 = new LatentStyleExceptionInfo() { Name = "Normal", PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo2 = new LatentStyleExceptionInfo() { Name = "heading 1", PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo3 = new LatentStyleExceptionInfo() { Name = "heading 2", PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo4 = new LatentStyleExceptionInfo() { Name = "heading 3", PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo5 = new LatentStyleExceptionInfo() { Name = "heading 4", SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo6 = new LatentStyleExceptionInfo() { Name = "heading 5", SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo7 = new LatentStyleExceptionInfo() { Name = "heading 6", SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo8 = new LatentStyleExceptionInfo() { Name = "heading 7", SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo9 = new LatentStyleExceptionInfo() { Name = "heading 8", SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo10 = new LatentStyleExceptionInfo() { Name = "heading 9", SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo11 = new LatentStyleExceptionInfo() { Name = "caption", SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo12 = new LatentStyleExceptionInfo() { Name = "Title", PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo13 = new LatentStyleExceptionInfo() { Name = "Subtitle", PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo14 = new LatentStyleExceptionInfo() { Name = "Strong", PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo15 = new LatentStyleExceptionInfo() { Name = "Emphasis", PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo16 = new LatentStyleExceptionInfo() { Name = "Placeholder Text", UiPriority = 99, SemiHidden = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo17 = new LatentStyleExceptionInfo() { Name = "No Spacing", UiPriority = 1, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo18 = new LatentStyleExceptionInfo() { Name = "Light Shading", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo19 = new LatentStyleExceptionInfo() { Name = "Light List", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo20 = new LatentStyleExceptionInfo() { Name = "Light Grid", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo21 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo22 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo23 = new LatentStyleExceptionInfo() { Name = "Medium List 1", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo24 = new LatentStyleExceptionInfo() { Name = "Medium List 2", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo25 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo26 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo27 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo28 = new LatentStyleExceptionInfo() { Name = "Dark List", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo29 = new LatentStyleExceptionInfo() { Name = "Colorful Shading", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo30 = new LatentStyleExceptionInfo() { Name = "Colorful List", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo31 = new LatentStyleExceptionInfo() { Name = "Colorful Grid", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo32 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 1", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo33 = new LatentStyleExceptionInfo() { Name = "Light List Accent 1", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo34 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 1", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo35 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 1", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo36 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 1", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo37 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 1", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo38 = new LatentStyleExceptionInfo() { Name = "Revision", UiPriority = 99, SemiHidden = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo39 = new LatentStyleExceptionInfo() { Name = "List Paragraph", UiPriority = 34, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo40 = new LatentStyleExceptionInfo() { Name = "Quote", UiPriority = 29, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo41 = new LatentStyleExceptionInfo() { Name = "Intense Quote", UiPriority = 30, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo42 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 1", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo43 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 1", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo44 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 1", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo45 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 1", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo46 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 1", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo47 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 1", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo48 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 1", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo49 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 1", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo50 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 2", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo51 = new LatentStyleExceptionInfo() { Name = "Light List Accent 2", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo52 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 2", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo53 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 2", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo54 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 2", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo55 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 2", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo56 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 2", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo57 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 2", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo58 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 2", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo59 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 2", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo60 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 2", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo61 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 2", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo62 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 2", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo63 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 2", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo64 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 3", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo65 = new LatentStyleExceptionInfo() { Name = "Light List Accent 3", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo66 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 3", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo67 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 3", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo68 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 3", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo69 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 3", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo70 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 3", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo71 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 3", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo72 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 3", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo73 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 3", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo74 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 3", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo75 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 3", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo76 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 3", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo77 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 3", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo78 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 4", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo79 = new LatentStyleExceptionInfo() { Name = "Light List Accent 4", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo80 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 4", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo81 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 4", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo82 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 4", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo83 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 4", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo84 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 4", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo85 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 4", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo86 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 4", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo87 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 4", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo88 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 4", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo89 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 4", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo90 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 4", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo91 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 4", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo92 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 5", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo93 = new LatentStyleExceptionInfo() { Name = "Light List Accent 5", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo94 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 5", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo95 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 5", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo96 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 5", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo97 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 5", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo98 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 5", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo99 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 5", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo100 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 5", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo101 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 5", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo102 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 5", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo103 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 5", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo104 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 5", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo105 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 5", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo106 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 6", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo107 = new LatentStyleExceptionInfo() { Name = "Light List Accent 6", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo108 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 6", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo109 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 6", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo110 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 6", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo111 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 6", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo112 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 6", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo113 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 6", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo114 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 6", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo115 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 6", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo116 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 6", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo117 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 6", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo118 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 6", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo119 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 6", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo120 = new LatentStyleExceptionInfo() { Name = "Subtle Emphasis", UiPriority = 19, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo121 = new LatentStyleExceptionInfo() { Name = "Intense Emphasis", UiPriority = 21, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo122 = new LatentStyleExceptionInfo() { Name = "Subtle Reference", UiPriority = 31, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo123 = new LatentStyleExceptionInfo() { Name = "Intense Reference", UiPriority = 32, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo124 = new LatentStyleExceptionInfo() { Name = "Book Title", UiPriority = 33, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo125 = new LatentStyleExceptionInfo() { Name = "Bibliography", UiPriority = 37, SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo126 = new LatentStyleExceptionInfo() { Name = "TOC Heading", UiPriority = 39, SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo127 = new LatentStyleExceptionInfo() { Name = "Plain Table 1", UiPriority = 41 };
            LatentStyleExceptionInfo latentStyleExceptionInfo128 = new LatentStyleExceptionInfo() { Name = "Plain Table 2", UiPriority = 42 };
            LatentStyleExceptionInfo latentStyleExceptionInfo129 = new LatentStyleExceptionInfo() { Name = "Plain Table 3", UiPriority = 43 };
            LatentStyleExceptionInfo latentStyleExceptionInfo130 = new LatentStyleExceptionInfo() { Name = "Plain Table 4", UiPriority = 44 };
            LatentStyleExceptionInfo latentStyleExceptionInfo131 = new LatentStyleExceptionInfo() { Name = "Plain Table 5", UiPriority = 45 };
            LatentStyleExceptionInfo latentStyleExceptionInfo132 = new LatentStyleExceptionInfo() { Name = "Grid Table Light", UiPriority = 40 };
            LatentStyleExceptionInfo latentStyleExceptionInfo133 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo134 = new LatentStyleExceptionInfo() { Name = "Grid Table 2", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo135 = new LatentStyleExceptionInfo() { Name = "Grid Table 3", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo136 = new LatentStyleExceptionInfo() { Name = "Grid Table 4", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo137 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo138 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo139 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo140 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light Accent 1", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo141 = new LatentStyleExceptionInfo() { Name = "Grid Table 2 Accent 1", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo142 = new LatentStyleExceptionInfo() { Name = "Grid Table 3 Accent 1", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo143 = new LatentStyleExceptionInfo() { Name = "Grid Table 4 Accent 1", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo144 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark Accent 1", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo145 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful Accent 1", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo146 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful Accent 1", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo147 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light Accent 2", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo148 = new LatentStyleExceptionInfo() { Name = "Grid Table 2 Accent 2", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo149 = new LatentStyleExceptionInfo() { Name = "Grid Table 3 Accent 2", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo150 = new LatentStyleExceptionInfo() { Name = "Grid Table 4 Accent 2", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo151 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark Accent 2", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo152 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful Accent 2", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo153 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful Accent 2", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo154 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light Accent 3", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo155 = new LatentStyleExceptionInfo() { Name = "Grid Table 2 Accent 3", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo156 = new LatentStyleExceptionInfo() { Name = "Grid Table 3 Accent 3", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo157 = new LatentStyleExceptionInfo() { Name = "Grid Table 4 Accent 3", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo158 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark Accent 3", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo159 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful Accent 3", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo160 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful Accent 3", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo161 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light Accent 4", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo162 = new LatentStyleExceptionInfo() { Name = "Grid Table 2 Accent 4", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo163 = new LatentStyleExceptionInfo() { Name = "Grid Table 3 Accent 4", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo164 = new LatentStyleExceptionInfo() { Name = "Grid Table 4 Accent 4", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo165 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark Accent 4", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo166 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful Accent 4", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo167 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful Accent 4", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo168 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light Accent 5", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo169 = new LatentStyleExceptionInfo() { Name = "Grid Table 2 Accent 5", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo170 = new LatentStyleExceptionInfo() { Name = "Grid Table 3 Accent 5", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo171 = new LatentStyleExceptionInfo() { Name = "Grid Table 4 Accent 5", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo172 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark Accent 5", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo173 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful Accent 5", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo174 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful Accent 5", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo175 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light Accent 6", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo176 = new LatentStyleExceptionInfo() { Name = "Grid Table 2 Accent 6", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo177 = new LatentStyleExceptionInfo() { Name = "Grid Table 3 Accent 6", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo178 = new LatentStyleExceptionInfo() { Name = "Grid Table 4 Accent 6", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo179 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark Accent 6", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo180 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful Accent 6", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo181 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful Accent 6", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo182 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo183 = new LatentStyleExceptionInfo() { Name = "List Table 2", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo184 = new LatentStyleExceptionInfo() { Name = "List Table 3", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo185 = new LatentStyleExceptionInfo() { Name = "List Table 4", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo186 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo187 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo188 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo189 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light Accent 1", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo190 = new LatentStyleExceptionInfo() { Name = "List Table 2 Accent 1", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo191 = new LatentStyleExceptionInfo() { Name = "List Table 3 Accent 1", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo192 = new LatentStyleExceptionInfo() { Name = "List Table 4 Accent 1", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo193 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark Accent 1", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo194 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful Accent 1", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo195 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful Accent 1", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo196 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light Accent 2", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo197 = new LatentStyleExceptionInfo() { Name = "List Table 2 Accent 2", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo198 = new LatentStyleExceptionInfo() { Name = "List Table 3 Accent 2", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo199 = new LatentStyleExceptionInfo() { Name = "List Table 4 Accent 2", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo200 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark Accent 2", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo201 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful Accent 2", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo202 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful Accent 2", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo203 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light Accent 3", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo204 = new LatentStyleExceptionInfo() { Name = "List Table 2 Accent 3", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo205 = new LatentStyleExceptionInfo() { Name = "List Table 3 Accent 3", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo206 = new LatentStyleExceptionInfo() { Name = "List Table 4 Accent 3", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo207 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark Accent 3", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo208 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful Accent 3", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo209 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful Accent 3", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo210 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light Accent 4", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo211 = new LatentStyleExceptionInfo() { Name = "List Table 2 Accent 4", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo212 = new LatentStyleExceptionInfo() { Name = "List Table 3 Accent 4", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo213 = new LatentStyleExceptionInfo() { Name = "List Table 4 Accent 4", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo214 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark Accent 4", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo215 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful Accent 4", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo216 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful Accent 4", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo217 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light Accent 5", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo218 = new LatentStyleExceptionInfo() { Name = "List Table 2 Accent 5", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo219 = new LatentStyleExceptionInfo() { Name = "List Table 3 Accent 5", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo220 = new LatentStyleExceptionInfo() { Name = "List Table 4 Accent 5", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo221 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark Accent 5", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo222 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful Accent 5", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo223 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful Accent 5", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo224 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light Accent 6", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo225 = new LatentStyleExceptionInfo() { Name = "List Table 2 Accent 6", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo226 = new LatentStyleExceptionInfo() { Name = "List Table 3 Accent 6", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo227 = new LatentStyleExceptionInfo() { Name = "List Table 4 Accent 6", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo228 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark Accent 6", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo229 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful Accent 6", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo230 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful Accent 6", UiPriority = 52 };

            latentStyles1.Append(latentStyleExceptionInfo1);
            latentStyles1.Append(latentStyleExceptionInfo2);
            latentStyles1.Append(latentStyleExceptionInfo3);
            latentStyles1.Append(latentStyleExceptionInfo4);
            latentStyles1.Append(latentStyleExceptionInfo5);
            latentStyles1.Append(latentStyleExceptionInfo6);
            latentStyles1.Append(latentStyleExceptionInfo7);
            latentStyles1.Append(latentStyleExceptionInfo8);
            latentStyles1.Append(latentStyleExceptionInfo9);
            latentStyles1.Append(latentStyleExceptionInfo10);
            latentStyles1.Append(latentStyleExceptionInfo11);
            latentStyles1.Append(latentStyleExceptionInfo12);
            latentStyles1.Append(latentStyleExceptionInfo13);
            latentStyles1.Append(latentStyleExceptionInfo14);
            latentStyles1.Append(latentStyleExceptionInfo15);
            latentStyles1.Append(latentStyleExceptionInfo16);
            latentStyles1.Append(latentStyleExceptionInfo17);
            latentStyles1.Append(latentStyleExceptionInfo18);
            latentStyles1.Append(latentStyleExceptionInfo19);
            latentStyles1.Append(latentStyleExceptionInfo20);
            latentStyles1.Append(latentStyleExceptionInfo21);
            latentStyles1.Append(latentStyleExceptionInfo22);
            latentStyles1.Append(latentStyleExceptionInfo23);
            latentStyles1.Append(latentStyleExceptionInfo24);
            latentStyles1.Append(latentStyleExceptionInfo25);
            latentStyles1.Append(latentStyleExceptionInfo26);
            latentStyles1.Append(latentStyleExceptionInfo27);
            latentStyles1.Append(latentStyleExceptionInfo28);
            latentStyles1.Append(latentStyleExceptionInfo29);
            latentStyles1.Append(latentStyleExceptionInfo30);
            latentStyles1.Append(latentStyleExceptionInfo31);
            latentStyles1.Append(latentStyleExceptionInfo32);
            latentStyles1.Append(latentStyleExceptionInfo33);
            latentStyles1.Append(latentStyleExceptionInfo34);
            latentStyles1.Append(latentStyleExceptionInfo35);
            latentStyles1.Append(latentStyleExceptionInfo36);
            latentStyles1.Append(latentStyleExceptionInfo37);
            latentStyles1.Append(latentStyleExceptionInfo38);
            latentStyles1.Append(latentStyleExceptionInfo39);
            latentStyles1.Append(latentStyleExceptionInfo40);
            latentStyles1.Append(latentStyleExceptionInfo41);
            latentStyles1.Append(latentStyleExceptionInfo42);
            latentStyles1.Append(latentStyleExceptionInfo43);
            latentStyles1.Append(latentStyleExceptionInfo44);
            latentStyles1.Append(latentStyleExceptionInfo45);
            latentStyles1.Append(latentStyleExceptionInfo46);
            latentStyles1.Append(latentStyleExceptionInfo47);
            latentStyles1.Append(latentStyleExceptionInfo48);
            latentStyles1.Append(latentStyleExceptionInfo49);
            latentStyles1.Append(latentStyleExceptionInfo50);
            latentStyles1.Append(latentStyleExceptionInfo51);
            latentStyles1.Append(latentStyleExceptionInfo52);
            latentStyles1.Append(latentStyleExceptionInfo53);
            latentStyles1.Append(latentStyleExceptionInfo54);
            latentStyles1.Append(latentStyleExceptionInfo55);
            latentStyles1.Append(latentStyleExceptionInfo56);
            latentStyles1.Append(latentStyleExceptionInfo57);
            latentStyles1.Append(latentStyleExceptionInfo58);
            latentStyles1.Append(latentStyleExceptionInfo59);
            latentStyles1.Append(latentStyleExceptionInfo60);
            latentStyles1.Append(latentStyleExceptionInfo61);
            latentStyles1.Append(latentStyleExceptionInfo62);
            latentStyles1.Append(latentStyleExceptionInfo63);
            latentStyles1.Append(latentStyleExceptionInfo64);
            latentStyles1.Append(latentStyleExceptionInfo65);
            latentStyles1.Append(latentStyleExceptionInfo66);
            latentStyles1.Append(latentStyleExceptionInfo67);
            latentStyles1.Append(latentStyleExceptionInfo68);
            latentStyles1.Append(latentStyleExceptionInfo69);
            latentStyles1.Append(latentStyleExceptionInfo70);
            latentStyles1.Append(latentStyleExceptionInfo71);
            latentStyles1.Append(latentStyleExceptionInfo72);
            latentStyles1.Append(latentStyleExceptionInfo73);
            latentStyles1.Append(latentStyleExceptionInfo74);
            latentStyles1.Append(latentStyleExceptionInfo75);
            latentStyles1.Append(latentStyleExceptionInfo76);
            latentStyles1.Append(latentStyleExceptionInfo77);
            latentStyles1.Append(latentStyleExceptionInfo78);
            latentStyles1.Append(latentStyleExceptionInfo79);
            latentStyles1.Append(latentStyleExceptionInfo80);
            latentStyles1.Append(latentStyleExceptionInfo81);
            latentStyles1.Append(latentStyleExceptionInfo82);
            latentStyles1.Append(latentStyleExceptionInfo83);
            latentStyles1.Append(latentStyleExceptionInfo84);
            latentStyles1.Append(latentStyleExceptionInfo85);
            latentStyles1.Append(latentStyleExceptionInfo86);
            latentStyles1.Append(latentStyleExceptionInfo87);
            latentStyles1.Append(latentStyleExceptionInfo88);
            latentStyles1.Append(latentStyleExceptionInfo89);
            latentStyles1.Append(latentStyleExceptionInfo90);
            latentStyles1.Append(latentStyleExceptionInfo91);
            latentStyles1.Append(latentStyleExceptionInfo92);
            latentStyles1.Append(latentStyleExceptionInfo93);
            latentStyles1.Append(latentStyleExceptionInfo94);
            latentStyles1.Append(latentStyleExceptionInfo95);
            latentStyles1.Append(latentStyleExceptionInfo96);
            latentStyles1.Append(latentStyleExceptionInfo97);
            latentStyles1.Append(latentStyleExceptionInfo98);
            latentStyles1.Append(latentStyleExceptionInfo99);
            latentStyles1.Append(latentStyleExceptionInfo100);
            latentStyles1.Append(latentStyleExceptionInfo101);
            latentStyles1.Append(latentStyleExceptionInfo102);
            latentStyles1.Append(latentStyleExceptionInfo103);
            latentStyles1.Append(latentStyleExceptionInfo104);
            latentStyles1.Append(latentStyleExceptionInfo105);
            latentStyles1.Append(latentStyleExceptionInfo106);
            latentStyles1.Append(latentStyleExceptionInfo107);
            latentStyles1.Append(latentStyleExceptionInfo108);
            latentStyles1.Append(latentStyleExceptionInfo109);
            latentStyles1.Append(latentStyleExceptionInfo110);
            latentStyles1.Append(latentStyleExceptionInfo111);
            latentStyles1.Append(latentStyleExceptionInfo112);
            latentStyles1.Append(latentStyleExceptionInfo113);
            latentStyles1.Append(latentStyleExceptionInfo114);
            latentStyles1.Append(latentStyleExceptionInfo115);
            latentStyles1.Append(latentStyleExceptionInfo116);
            latentStyles1.Append(latentStyleExceptionInfo117);
            latentStyles1.Append(latentStyleExceptionInfo118);
            latentStyles1.Append(latentStyleExceptionInfo119);
            latentStyles1.Append(latentStyleExceptionInfo120);
            latentStyles1.Append(latentStyleExceptionInfo121);
            latentStyles1.Append(latentStyleExceptionInfo122);
            latentStyles1.Append(latentStyleExceptionInfo123);
            latentStyles1.Append(latentStyleExceptionInfo124);
            latentStyles1.Append(latentStyleExceptionInfo125);
            latentStyles1.Append(latentStyleExceptionInfo126);
            latentStyles1.Append(latentStyleExceptionInfo127);
            latentStyles1.Append(latentStyleExceptionInfo128);
            latentStyles1.Append(latentStyleExceptionInfo129);
            latentStyles1.Append(latentStyleExceptionInfo130);
            latentStyles1.Append(latentStyleExceptionInfo131);
            latentStyles1.Append(latentStyleExceptionInfo132);
            latentStyles1.Append(latentStyleExceptionInfo133);
            latentStyles1.Append(latentStyleExceptionInfo134);
            latentStyles1.Append(latentStyleExceptionInfo135);
            latentStyles1.Append(latentStyleExceptionInfo136);
            latentStyles1.Append(latentStyleExceptionInfo137);
            latentStyles1.Append(latentStyleExceptionInfo138);
            latentStyles1.Append(latentStyleExceptionInfo139);
            latentStyles1.Append(latentStyleExceptionInfo140);
            latentStyles1.Append(latentStyleExceptionInfo141);
            latentStyles1.Append(latentStyleExceptionInfo142);
            latentStyles1.Append(latentStyleExceptionInfo143);
            latentStyles1.Append(latentStyleExceptionInfo144);
            latentStyles1.Append(latentStyleExceptionInfo145);
            latentStyles1.Append(latentStyleExceptionInfo146);
            latentStyles1.Append(latentStyleExceptionInfo147);
            latentStyles1.Append(latentStyleExceptionInfo148);
            latentStyles1.Append(latentStyleExceptionInfo149);
            latentStyles1.Append(latentStyleExceptionInfo150);
            latentStyles1.Append(latentStyleExceptionInfo151);
            latentStyles1.Append(latentStyleExceptionInfo152);
            latentStyles1.Append(latentStyleExceptionInfo153);
            latentStyles1.Append(latentStyleExceptionInfo154);
            latentStyles1.Append(latentStyleExceptionInfo155);
            latentStyles1.Append(latentStyleExceptionInfo156);
            latentStyles1.Append(latentStyleExceptionInfo157);
            latentStyles1.Append(latentStyleExceptionInfo158);
            latentStyles1.Append(latentStyleExceptionInfo159);
            latentStyles1.Append(latentStyleExceptionInfo160);
            latentStyles1.Append(latentStyleExceptionInfo161);
            latentStyles1.Append(latentStyleExceptionInfo162);
            latentStyles1.Append(latentStyleExceptionInfo163);
            latentStyles1.Append(latentStyleExceptionInfo164);
            latentStyles1.Append(latentStyleExceptionInfo165);
            latentStyles1.Append(latentStyleExceptionInfo166);
            latentStyles1.Append(latentStyleExceptionInfo167);
            latentStyles1.Append(latentStyleExceptionInfo168);
            latentStyles1.Append(latentStyleExceptionInfo169);
            latentStyles1.Append(latentStyleExceptionInfo170);
            latentStyles1.Append(latentStyleExceptionInfo171);
            latentStyles1.Append(latentStyleExceptionInfo172);
            latentStyles1.Append(latentStyleExceptionInfo173);
            latentStyles1.Append(latentStyleExceptionInfo174);
            latentStyles1.Append(latentStyleExceptionInfo175);
            latentStyles1.Append(latentStyleExceptionInfo176);
            latentStyles1.Append(latentStyleExceptionInfo177);
            latentStyles1.Append(latentStyleExceptionInfo178);
            latentStyles1.Append(latentStyleExceptionInfo179);
            latentStyles1.Append(latentStyleExceptionInfo180);
            latentStyles1.Append(latentStyleExceptionInfo181);
            latentStyles1.Append(latentStyleExceptionInfo182);
            latentStyles1.Append(latentStyleExceptionInfo183);
            latentStyles1.Append(latentStyleExceptionInfo184);
            latentStyles1.Append(latentStyleExceptionInfo185);
            latentStyles1.Append(latentStyleExceptionInfo186);
            latentStyles1.Append(latentStyleExceptionInfo187);
            latentStyles1.Append(latentStyleExceptionInfo188);
            latentStyles1.Append(latentStyleExceptionInfo189);
            latentStyles1.Append(latentStyleExceptionInfo190);
            latentStyles1.Append(latentStyleExceptionInfo191);
            latentStyles1.Append(latentStyleExceptionInfo192);
            latentStyles1.Append(latentStyleExceptionInfo193);
            latentStyles1.Append(latentStyleExceptionInfo194);
            latentStyles1.Append(latentStyleExceptionInfo195);
            latentStyles1.Append(latentStyleExceptionInfo196);
            latentStyles1.Append(latentStyleExceptionInfo197);
            latentStyles1.Append(latentStyleExceptionInfo198);
            latentStyles1.Append(latentStyleExceptionInfo199);
            latentStyles1.Append(latentStyleExceptionInfo200);
            latentStyles1.Append(latentStyleExceptionInfo201);
            latentStyles1.Append(latentStyleExceptionInfo202);
            latentStyles1.Append(latentStyleExceptionInfo203);
            latentStyles1.Append(latentStyleExceptionInfo204);
            latentStyles1.Append(latentStyleExceptionInfo205);
            latentStyles1.Append(latentStyleExceptionInfo206);
            latentStyles1.Append(latentStyleExceptionInfo207);
            latentStyles1.Append(latentStyleExceptionInfo208);
            latentStyles1.Append(latentStyleExceptionInfo209);
            latentStyles1.Append(latentStyleExceptionInfo210);
            latentStyles1.Append(latentStyleExceptionInfo211);
            latentStyles1.Append(latentStyleExceptionInfo212);
            latentStyles1.Append(latentStyleExceptionInfo213);
            latentStyles1.Append(latentStyleExceptionInfo214);
            latentStyles1.Append(latentStyleExceptionInfo215);
            latentStyles1.Append(latentStyleExceptionInfo216);
            latentStyles1.Append(latentStyleExceptionInfo217);
            latentStyles1.Append(latentStyleExceptionInfo218);
            latentStyles1.Append(latentStyleExceptionInfo219);
            latentStyles1.Append(latentStyleExceptionInfo220);
            latentStyles1.Append(latentStyleExceptionInfo221);
            latentStyles1.Append(latentStyleExceptionInfo222);
            latentStyles1.Append(latentStyleExceptionInfo223);
            latentStyles1.Append(latentStyleExceptionInfo224);
            latentStyles1.Append(latentStyleExceptionInfo225);
            latentStyles1.Append(latentStyleExceptionInfo226);
            latentStyles1.Append(latentStyleExceptionInfo227);
            latentStyles1.Append(latentStyleExceptionInfo228);
            latentStyles1.Append(latentStyleExceptionInfo229);
            latentStyles1.Append(latentStyleExceptionInfo230);

            Style style1 = new Style() { Type = StyleValues.Paragraph, StyleId = "a", Default = true };
            StyleName styleName1 = new StyleName() { Val = "Normal" };
            PrimaryStyle primaryStyle1 = new PrimaryStyle();
            Rsid rsid2843 = new Rsid() { Val = "0023000B" };

            StyleRunProperties styleRunProperties1 = new StyleRunProperties();
            FontSize fontSize679 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript679 = new FontSizeComplexScript() { Val = "24" };

            styleRunProperties1.Append(fontSize679);
            styleRunProperties1.Append(fontSizeComplexScript679);

            style1.Append(styleName1);
            style1.Append(primaryStyle1);
            style1.Append(rsid2843);
            style1.Append(styleRunProperties1);

            Style style2 = new Style() { Type = StyleValues.Paragraph, StyleId = "2" };
            StyleName styleName2 = new StyleName() { Val = "heading 2" };
            BasedOn basedOn1 = new BasedOn() { Val = "a" };
            NextParagraphStyle nextParagraphStyle1 = new NextParagraphStyle() { Val = "a" };
            PrimaryStyle primaryStyle2 = new PrimaryStyle();
            Rsid rsid2844 = new Rsid() { Val = "00055234" };

            StyleParagraphProperties styleParagraphProperties1 = new StyleParagraphProperties();
            KeepNext keepNext1 = new KeepNext();
            SpacingBetweenLines spacingBetweenLines91 = new SpacingBetweenLines() { Before = "240", After = "60" };
            OutlineLevel outlineLevel15 = new OutlineLevel() { Val = 1 };

            styleParagraphProperties1.Append(keepNext1);
            styleParagraphProperties1.Append(spacingBetweenLines91);
            styleParagraphProperties1.Append(outlineLevel15);

            StyleRunProperties styleRunProperties2 = new StyleRunProperties();
            RunFonts runFonts2 = new RunFonts() { Ascii = "Arial", HighAnsi = "Arial", ComplexScript = "Arial" };
            Bold bold1 = new Bold();
            BoldComplexScript boldComplexScript1 = new BoldComplexScript();
            Italic italic1 = new Italic();
            ItalicComplexScript italicComplexScript1 = new ItalicComplexScript();
            FontSize fontSize680 = new FontSize() { Val = "28" };
            FontSizeComplexScript fontSizeComplexScript680 = new FontSizeComplexScript() { Val = "28" };

            styleRunProperties2.Append(runFonts2);
            styleRunProperties2.Append(bold1);
            styleRunProperties2.Append(boldComplexScript1);
            styleRunProperties2.Append(italic1);
            styleRunProperties2.Append(italicComplexScript1);
            styleRunProperties2.Append(fontSize680);
            styleRunProperties2.Append(fontSizeComplexScript680);

            style2.Append(styleName2);
            style2.Append(basedOn1);
            style2.Append(nextParagraphStyle1);
            style2.Append(primaryStyle2);
            style2.Append(rsid2844);
            style2.Append(styleParagraphProperties1);
            style2.Append(styleRunProperties2);

            Style style3 = new Style() { Type = StyleValues.Paragraph, StyleId = "3" };
            StyleName styleName3 = new StyleName() { Val = "heading 3" };
            BasedOn basedOn2 = new BasedOn() { Val = "a" };
            NextParagraphStyle nextParagraphStyle2 = new NextParagraphStyle() { Val = "a" };
            PrimaryStyle primaryStyle3 = new PrimaryStyle();
            Rsid rsid2845 = new Rsid() { Val = "003A0E75" };

            StyleParagraphProperties styleParagraphProperties2 = new StyleParagraphProperties();
            KeepNext keepNext2 = new KeepNext();
            Justification justification133 = new Justification() { Val = JustificationValues.Both };
            OutlineLevel outlineLevel16 = new OutlineLevel() { Val = 2 };

            styleParagraphProperties2.Append(keepNext2);
            styleParagraphProperties2.Append(justification133);
            styleParagraphProperties2.Append(outlineLevel16);

            StyleRunProperties styleRunProperties3 = new StyleRunProperties();
            FontSize fontSize681 = new FontSize() { Val = "30" };
            FontSizeComplexScript fontSizeComplexScript681 = new FontSizeComplexScript() { Val = "20" };

            styleRunProperties3.Append(fontSize681);
            styleRunProperties3.Append(fontSizeComplexScript681);

            style3.Append(styleName3);
            style3.Append(basedOn2);
            style3.Append(nextParagraphStyle2);
            style3.Append(primaryStyle3);
            style3.Append(rsid2845);
            style3.Append(styleParagraphProperties2);
            style3.Append(styleRunProperties3);

            Style style4 = new Style() { Type = StyleValues.Character, StyleId = "a0", Default = true };
            StyleName styleName4 = new StyleName() { Val = "Default Paragraph Font" };
            SemiHidden semiHidden1 = new SemiHidden();

            style4.Append(styleName4);
            style4.Append(semiHidden1);

            Style style5 = new Style() { Type = StyleValues.Table, StyleId = "a1", Default = true };
            StyleName styleName5 = new StyleName() { Val = "Normal Table" };
            SemiHidden semiHidden2 = new SemiHidden();

            StyleTableProperties styleTableProperties1 = new StyleTableProperties();
            TableIndentation tableIndentation1 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

            TableCellMarginDefault tableCellMarginDefault1 = new TableCellMarginDefault();
            TopMargin topMargin1 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellLeftMargin tableCellLeftMargin1 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
            BottomMargin bottomMargin1 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellRightMargin tableCellRightMargin1 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

            tableCellMarginDefault1.Append(topMargin1);
            tableCellMarginDefault1.Append(tableCellLeftMargin1);
            tableCellMarginDefault1.Append(bottomMargin1);
            tableCellMarginDefault1.Append(tableCellRightMargin1);

            styleTableProperties1.Append(tableIndentation1);
            styleTableProperties1.Append(tableCellMarginDefault1);

            style5.Append(styleName5);
            style5.Append(semiHidden2);
            style5.Append(styleTableProperties1);

            Style style6 = new Style() { Type = StyleValues.Numbering, StyleId = "a2", Default = true };
            StyleName styleName6 = new StyleName() { Val = "No List" };
            SemiHidden semiHidden3 = new SemiHidden();

            style6.Append(styleName6);
            style6.Append(semiHidden3);

            Style style7 = new Style() { Type = StyleValues.Paragraph, StyleId = "a3" };
            StyleName styleName7 = new StyleName() { Val = "Body Text" };
            BasedOn basedOn3 = new BasedOn() { Val = "a" };
            Rsid rsid2846 = new Rsid() { Val = "0023000B" };

            StyleParagraphProperties styleParagraphProperties3 = new StyleParagraphProperties();
            Justification justification134 = new Justification() { Val = JustificationValues.Center };

            styleParagraphProperties3.Append(justification134);

            StyleRunProperties styleRunProperties4 = new StyleRunProperties();
            FontSize fontSize682 = new FontSize() { Val = "28" };
            FontSizeComplexScript fontSizeComplexScript682 = new FontSizeComplexScript() { Val = "20" };

            styleRunProperties4.Append(fontSize682);
            styleRunProperties4.Append(fontSizeComplexScript682);

            style7.Append(styleName7);
            style7.Append(basedOn3);
            style7.Append(rsid2846);
            style7.Append(styleParagraphProperties3);
            style7.Append(styleRunProperties4);

            Style style8 = new Style() { Type = StyleValues.Paragraph, StyleId = "20" };
            StyleName styleName8 = new StyleName() { Val = "Body Text Indent 2" };
            BasedOn basedOn4 = new BasedOn() { Val = "a" };
            LinkedStyle linkedStyle1 = new LinkedStyle() { Val = "21" };
            Rsid rsid2847 = new Rsid() { Val = "0023000B" };

            StyleParagraphProperties styleParagraphProperties4 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines92 = new SpacingBetweenLines() { After = "120", Line = "480", LineRule = LineSpacingRuleValues.Auto };
            Indentation indentation117 = new Indentation() { Start = "283" };

            styleParagraphProperties4.Append(spacingBetweenLines92);
            styleParagraphProperties4.Append(indentation117);

            style8.Append(styleName8);
            style8.Append(basedOn4);
            style8.Append(linkedStyle1);
            style8.Append(rsid2847);
            style8.Append(styleParagraphProperties4);

            Style style9 = new Style() { Type = StyleValues.Paragraph, StyleId = "a4" };
            StyleName styleName9 = new StyleName() { Val = "Body Text Indent" };
            BasedOn basedOn5 = new BasedOn() { Val = "a" };
            Rsid rsid2848 = new Rsid() { Val = "0023000B" };

            StyleParagraphProperties styleParagraphProperties5 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines93 = new SpacingBetweenLines() { After = "120" };
            Indentation indentation118 = new Indentation() { Start = "283" };

            styleParagraphProperties5.Append(spacingBetweenLines93);
            styleParagraphProperties5.Append(indentation118);

            style9.Append(styleName9);
            style9.Append(basedOn5);
            style9.Append(rsid2848);
            style9.Append(styleParagraphProperties5);

            Style style10 = new Style() { Type = StyleValues.Paragraph, StyleId = "Normal", CustomStyle = true };
            StyleName styleName10 = new StyleName() { Val = "Normal" };
            Rsid rsid2849 = new Rsid() { Val = "0023000B" };

            StyleRunProperties styleRunProperties5 = new StyleRunProperties();
            SnapToGrid snapToGrid11 = new SnapToGrid() { Val = false };
            FontSize fontSize683 = new FontSize() { Val = "28" };

            styleRunProperties5.Append(snapToGrid11);
            styleRunProperties5.Append(fontSize683);

            style10.Append(styleName10);
            style10.Append(rsid2849);
            style10.Append(styleRunProperties5);

            Style style11 = new Style() { Type = StyleValues.Paragraph, StyleId = "a5" };
            StyleName styleName11 = new StyleName() { Val = "header" };
            BasedOn basedOn6 = new BasedOn() { Val = "a" };
            LinkedStyle linkedStyle2 = new LinkedStyle() { Val = "1" };
            Rsid rsid2850 = new Rsid() { Val = "008B1C0E" };

            StyleParagraphProperties styleParagraphProperties6 = new StyleParagraphProperties();

            Tabs tabs2 = new Tabs();
            TabStop tabStop3 = new TabStop() { Val = TabStopValues.Center, Position = 4153 };
            TabStop tabStop4 = new TabStop() { Val = TabStopValues.Right, Position = 8306 };

            tabs2.Append(tabStop3);
            tabs2.Append(tabStop4);

            styleParagraphProperties6.Append(tabs2);

            StyleRunProperties styleRunProperties6 = new StyleRunProperties();
            FontSize fontSize684 = new FontSize() { Val = "28" };
            FontSizeComplexScript fontSizeComplexScript683 = new FontSizeComplexScript() { Val = "20" };

            styleRunProperties6.Append(fontSize684);
            styleRunProperties6.Append(fontSizeComplexScript683);

            style11.Append(styleName11);
            style11.Append(basedOn6);
            style11.Append(linkedStyle2);
            style11.Append(rsid2850);
            style11.Append(styleParagraphProperties6);
            style11.Append(styleRunProperties6);

            Style style12 = new Style() { Type = StyleValues.Paragraph, StyleId = "a6", CustomStyle = true };
            StyleName styleName12 = new StyleName() { Val = "Бланки" };
            BasedOn basedOn7 = new BasedOn() { Val = "a" };
            Rsid rsid2851 = new Rsid() { Val = "008B1C0E" };

            StyleRunProperties styleRunProperties7 = new StyleRunProperties();
            FontSize fontSize685 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript684 = new FontSizeComplexScript() { Val = "20" };

            styleRunProperties7.Append(fontSize685);
            styleRunProperties7.Append(fontSizeComplexScript684);

            style12.Append(styleName12);
            style12.Append(basedOn7);
            style12.Append(rsid2851);
            style12.Append(styleRunProperties7);

            Style style13 = new Style() { Type = StyleValues.Character, StyleId = "a7" };
            StyleName styleName13 = new StyleName() { Val = "page number" };
            BasedOn basedOn8 = new BasedOn() { Val = "a0" };
            Rsid rsid2852 = new Rsid() { Val = "008B1C0E" };

            style13.Append(styleName13);
            style13.Append(basedOn8);
            style13.Append(rsid2852);

            Style style14 = new Style() { Type = StyleValues.Paragraph, StyleId = "30" };
            StyleName styleName14 = new StyleName() { Val = "Body Text Indent 3" };
            BasedOn basedOn9 = new BasedOn() { Val = "a" };
            Rsid rsid2853 = new Rsid() { Val = "00E453E2" };

            StyleParagraphProperties styleParagraphProperties7 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines94 = new SpacingBetweenLines() { After = "120" };
            Indentation indentation119 = new Indentation() { Start = "283" };

            styleParagraphProperties7.Append(spacingBetweenLines94);
            styleParagraphProperties7.Append(indentation119);

            StyleRunProperties styleRunProperties8 = new StyleRunProperties();
            FontSize fontSize686 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript685 = new FontSizeComplexScript() { Val = "16" };

            styleRunProperties8.Append(fontSize686);
            styleRunProperties8.Append(fontSizeComplexScript685);

            style14.Append(styleName14);
            style14.Append(basedOn9);
            style14.Append(rsid2853);
            style14.Append(styleParagraphProperties7);
            style14.Append(styleRunProperties8);

            Style style15 = new Style() { Type = StyleValues.Paragraph, StyleId = "a8" };
            StyleName styleName15 = new StyleName() { Val = "Balloon Text" };
            BasedOn basedOn10 = new BasedOn() { Val = "a" };
            SemiHidden semiHidden4 = new SemiHidden();
            Rsid rsid2854 = new Rsid() { Val = "00E61113" };

            StyleRunProperties styleRunProperties9 = new StyleRunProperties();
            RunFonts runFonts3 = new RunFonts() { Ascii = "Tahoma", HighAnsi = "Tahoma", ComplexScript = "Tahoma" };
            FontSize fontSize687 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript686 = new FontSizeComplexScript() { Val = "16" };

            styleRunProperties9.Append(runFonts3);
            styleRunProperties9.Append(fontSize687);
            styleRunProperties9.Append(fontSizeComplexScript686);

            style15.Append(styleName15);
            style15.Append(basedOn10);
            style15.Append(semiHidden4);
            style15.Append(rsid2854);
            style15.Append(styleRunProperties9);

            Style style16 = new Style() { Type = StyleValues.Paragraph, StyleId = "a9" };
            StyleName styleName16 = new StyleName() { Val = "footer" };
            BasedOn basedOn11 = new BasedOn() { Val = "a" };
            Rsid rsid2855 = new Rsid() { Val = "00C539BB" };

            StyleParagraphProperties styleParagraphProperties8 = new StyleParagraphProperties();

            Tabs tabs3 = new Tabs();
            TabStop tabStop5 = new TabStop() { Val = TabStopValues.Center, Position = 4677 };
            TabStop tabStop6 = new TabStop() { Val = TabStopValues.Right, Position = 9355 };

            tabs3.Append(tabStop5);
            tabs3.Append(tabStop6);

            styleParagraphProperties8.Append(tabs3);

            style16.Append(styleName16);
            style16.Append(basedOn11);
            style16.Append(rsid2855);
            style16.Append(styleParagraphProperties8);

            Style style17 = new Style() { Type = StyleValues.Paragraph, StyleId = "ConsPlusNormal", CustomStyle = true };
            StyleName styleName17 = new StyleName() { Val = "ConsPlusNormal" };
            Rsid rsid2856 = new Rsid() { Val = "00961A2B" };

            StyleParagraphProperties styleParagraphProperties9 = new StyleParagraphProperties();
            AutoSpaceDE autoSpaceDE1 = new AutoSpaceDE() { Val = false };
            AutoSpaceDN autoSpaceDN1 = new AutoSpaceDN() { Val = false };
            AdjustRightIndent adjustRightIndent1 = new AdjustRightIndent() { Val = false };
            Indentation indentation120 = new Indentation() { FirstLine = "720" };

            styleParagraphProperties9.Append(autoSpaceDE1);
            styleParagraphProperties9.Append(autoSpaceDN1);
            styleParagraphProperties9.Append(adjustRightIndent1);
            styleParagraphProperties9.Append(indentation120);

            StyleRunProperties styleRunProperties10 = new StyleRunProperties();
            RunFonts runFonts4 = new RunFonts() { Ascii = "Arial", HighAnsi = "Arial", ComplexScript = "Arial" };

            styleRunProperties10.Append(runFonts4);

            style17.Append(styleName17);
            style17.Append(rsid2856);
            style17.Append(styleParagraphProperties9);
            style17.Append(styleRunProperties10);

            Style style18 = new Style() { Type = StyleValues.Paragraph, StyleId = "31" };
            StyleName styleName18 = new StyleName() { Val = "Body Text 3" };
            BasedOn basedOn12 = new BasedOn() { Val = "a" };
            Rsid rsid2857 = new Rsid() { Val = "00253FF7" };

            StyleParagraphProperties styleParagraphProperties10 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines95 = new SpacingBetweenLines() { After = "120" };

            styleParagraphProperties10.Append(spacingBetweenLines95);

            StyleRunProperties styleRunProperties11 = new StyleRunProperties();
            FontSize fontSize688 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript687 = new FontSizeComplexScript() { Val = "16" };

            styleRunProperties11.Append(fontSize688);
            styleRunProperties11.Append(fontSizeComplexScript687);

            style18.Append(styleName18);
            style18.Append(basedOn12);
            style18.Append(rsid2857);
            style18.Append(styleParagraphProperties10);
            style18.Append(styleRunProperties11);

            Style style19 = new Style() { Type = StyleValues.Paragraph, StyleId = "15", CustomStyle = true };
            StyleName styleName19 = new StyleName() { Val = "Обычный + 15 пт" };
            Aliases aliases1 = new Aliases() { Val = "По ширине" };
            BasedOn basedOn13 = new BasedOn() { Val = "a" };
            Rsid rsid2858 = new Rsid() { Val = "004F31BC" };

            StyleParagraphProperties styleParagraphProperties11 = new StyleParagraphProperties();
            Justification justification135 = new Justification() { Val = JustificationValues.Both };

            styleParagraphProperties11.Append(justification135);

            StyleRunProperties styleRunProperties12 = new StyleRunProperties();
            FontSize fontSize689 = new FontSize() { Val = "30" };
            FontSizeComplexScript fontSizeComplexScript688 = new FontSizeComplexScript() { Val = "30" };

            styleRunProperties12.Append(fontSize689);
            styleRunProperties12.Append(fontSizeComplexScript688);

            style19.Append(styleName19);
            style19.Append(aliases1);
            style19.Append(basedOn13);
            style19.Append(rsid2858);
            style19.Append(styleParagraphProperties11);
            style19.Append(styleRunProperties12);

            Style style20 = new Style() { Type = StyleValues.Paragraph, StyleId = "10", CustomStyle = true };
            StyleName styleName20 = new StyleName() { Val = " Знак Знак Знак Знак Знак Знак Знак Знак Знак1 Знак" };
            BasedOn basedOn14 = new BasedOn() { Val = "a" };
            Rsid rsid2859 = new Rsid() { Val = "00412985" };

            StyleParagraphProperties styleParagraphProperties12 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines96 = new SpacingBetweenLines() { After = "160", Line = "240", LineRule = LineSpacingRuleValues.Exact };

            styleParagraphProperties12.Append(spacingBetweenLines96);

            StyleRunProperties styleRunProperties13 = new StyleRunProperties();
            RunFonts runFonts5 = new RunFonts() { Ascii = "Arial", HighAnsi = "Arial", ComplexScript = "Arial" };
            FontSize fontSize690 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript689 = new FontSizeComplexScript() { Val = "20" };
            Languages languages5 = new Languages() { Val = "de-CH", EastAsia = "de-CH" };

            styleRunProperties13.Append(runFonts5);
            styleRunProperties13.Append(fontSize690);
            styleRunProperties13.Append(fontSizeComplexScript689);
            styleRunProperties13.Append(languages5);

            style20.Append(styleName20);
            style20.Append(basedOn14);
            style20.Append(rsid2859);
            style20.Append(styleParagraphProperties12);
            style20.Append(styleRunProperties13);

            Style style21 = new Style() { Type = StyleValues.Character, StyleId = "1", CustomStyle = true };
            StyleName styleName21 = new StyleName() { Val = "Верхний колонтитул Знак1" };
            LinkedStyle linkedStyle3 = new LinkedStyle() { Val = "a5" };
            Locked locked1 = new Locked();
            Rsid rsid2860 = new Rsid() { Val = "00C86C77" };

            StyleRunProperties styleRunProperties14 = new StyleRunProperties();
            FontSize fontSize691 = new FontSize() { Val = "28" };

            styleRunProperties14.Append(fontSize691);

            style21.Append(styleName21);
            style21.Append(linkedStyle3);
            style21.Append(locked1);
            style21.Append(rsid2860);
            style21.Append(styleRunProperties14);

            Style style22 = new Style() { Type = StyleValues.Character, StyleId = "21", CustomStyle = true };
            StyleName styleName22 = new StyleName() { Val = "Основной текст с отступом 2 Знак" };
            LinkedStyle linkedStyle4 = new LinkedStyle() { Val = "20" };
            Rsid rsid2861 = new Rsid() { Val = "00C86C77" };

            StyleRunProperties styleRunProperties15 = new StyleRunProperties();
            FontSize fontSize692 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript690 = new FontSizeComplexScript() { Val = "24" };

            styleRunProperties15.Append(fontSize692);
            styleRunProperties15.Append(fontSizeComplexScript690);

            style22.Append(styleName22);
            style22.Append(linkedStyle4);
            style22.Append(rsid2861);
            style22.Append(styleRunProperties15);

            styles1.Append(docDefaults1);
            styles1.Append(latentStyles1);
            styles1.Append(style1);
            styles1.Append(style2);
            styles1.Append(style3);
            styles1.Append(style4);
            styles1.Append(style5);
            styles1.Append(style6);
            styles1.Append(style7);
            styles1.Append(style8);
            styles1.Append(style9);
            styles1.Append(style10);
            styles1.Append(style11);
            styles1.Append(style12);
            styles1.Append(style13);
            styles1.Append(style14);
            styles1.Append(style15);
            styles1.Append(style16);
            styles1.Append(style17);
            styles1.Append(style18);
            styles1.Append(style19);
            styles1.Append(style20);
            styles1.Append(style21);
            styles1.Append(style22);

            styleDefinitionsPart1.Styles = styles1;
        }
    }

    
}