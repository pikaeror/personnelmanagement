using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using PersonnelManagement.USERS;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Decrees")]
    public class DecreesController : Controller
    {

        private Repository repository;
        private DecreeWorker decreeWorker;

        public DecreesController(Repository repository)
        {
            this.repository = repository;
            this.decreeWorker = new DecreeWorker(ref repository);
        }

        // Is allowed to edit structures.
        // GET: api/Decrees
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

        // GET: api/Decrees/Active
        [HttpGet("Active")]
        public IEnumerable<Decree> GetDecreeActive()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                // return repository.GetDecree 
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                return repository.GetDecreesActive(user);
            }
            return null;
        }

        // GET: api/Decrees/SelectActive5
        [HttpGet("SelectActive{id}")]
        public void GetDecreeSelectActive([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                // return repository.GetDecree 
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                repository.GetContextUser().DecreesSelectActive(user, id);
            }
        }

        // GET: api/Decrees/5
        [HttpGet("{id}")]
        public Decree GetDecree([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                // return repository.GetDecree 

                return repository.Decrees.FirstOrDefault(d => d.Id == id);
            }
            return null;
        }



        // POST: api/Decrees
        [HttpPost]
        public IActionResult PostDecree([FromBody] DecreeManagement decreeManagement)
        {
            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.canEditStructures(sessionid, repository);
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
            if (decreeManagement.DecreeManagementStatus == Keys.DECREE_MANAGEMENT_NEWDECREE)
            {
                repository.AddNewDecree(decreeManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Приказ создан");
            }
            /**
             * Decline decree.
             */
            else if (decreeManagement.DecreeManagementStatus == Keys.DECREE_MANAGEMENT_DECLINEDECREE)
            {
                repository.RemoveDecree(decreeManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Приказ отменен");
            }
            /**
             * Accept and apply decree.
             */
            else if (decreeManagement.DecreeManagementStatus == Keys.DECREE_MANAGEMENT_ACCEPTDECREE)
            {
                repository.AcceptDecree(decreeManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Приказ подписан");
            }
            /**
             * Update decree info.
             */
            else if (decreeManagement.DecreeManagementStatus == Keys.DECREE_MANAGEMENT_UPDATEDECREEINFO)
            {
                repository.UpdateDecree(decreeManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Проект приказа обновлен");
            } else if (decreeManagement.DecreeManagementStatus == Keys.DECREE_MANAGEMENT_PRINTDECREE)
            {
                return File(GenerateDocument(decreeManagement), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "test.docx");
            }
            /**
             * Фильтрация если приказ уже подписан.
             */
            else if (decreeManagement.DecreeManagementStatus == Keys.DECREE_MANAGEMENT_FILTERSIGNEDECREE)
            {
                return new ObjectResult(repository.GetFilteredDecrees(user, decreeManagement));
                //return File(GenerateDocument(decreeManagement), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "test.docx");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        [HttpPost("Finder")]
        public IEnumerable<Decree> decreeFinder([FromBody] DecreeFinder decreeFinder)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            List<Decree> output = new List<Decree>();
            //DateTime start = DateTime.Now;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.canEditStructures(sessionid, repository);
                if (hasAccess)
                {
                    //decreeWorker.reWriteDecreesNull(user, decreeFinder.rewrite);
                    output = decreeWorker.FinderByFinder(decreeFinder);
                }
            }
            //DateTime end = DateTime.Now;
            //DateTime delta = end - start;
            return output;
        }

        public MemoryStream GenerateDocument(DecreeManagement decreeManagement)
        {
            var mem = new MemoryStream();
            // Create Document
            using (WordprocessingDocument wordDocument =
                WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document, true))
            {
                Decree decree = repository.Decrees.First(d => decreeManagement.Id == d.Id);
                /**
                 *  Get decreeoperations with detailed info.
                 */
                List<DecreeoperationManagement> decreeoperations = repository.GetDecreeoperationManagement(decreeManagement.Id).ToList();

                

                /**
                 * Positions modifying only currently
                 */
                DecreeoperationManagement decreeoperationManagement = decreeoperations.First();
                

                // Add a main document part. 
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                // Create the document structure and add some text.
                mainPart.Document = new Document();
                Body docBody = new Body();
                mainPart.Document.Append(docBody);

                // Title 
                Run titleRun = new Run();
                Text titleText = new Text("П Е Р Е Ч Е Н Ь");
                titleRun.Append(titleText);
                titleRun.Append(new CarriageReturn());

                Paragraph titleParagraph = new Paragraph();
                ParagraphProperties titleParagraphProperties = new ParagraphProperties();
                Justification titleJustification = new Justification() {Val = JustificationValues.Center };
                titleParagraphProperties.Append(titleJustification);
                titleParagraph.Append(titleParagraphProperties);
                
                titleParagraph.Append(titleRun);
                docBody.Append(titleParagraph);

                RunProperties titleRunProperties = new RunProperties();
                titleRunProperties.Bold = new Bold();

                titleRunProperties.FontSize = new FontSize();
                titleRunProperties.FontSize.Val = new StringValue("30");
                titleRun.PrependChild<RunProperties>(titleRunProperties);
                AppendFontDefault(titleRun);

                // Title descr
                Table tableTitle = new Table();
                TableProperties tableTitleProperties = new TableProperties();
                TableIndentation tableIndentation = new TableIndentation() { Width = 959, Type = TableWidthUnitValues.Dxa };
                tableTitleProperties.AppendChild(tableIndentation);
                tableTitle.AppendChild(tableTitleProperties);

                TableRow rowTitle = new TableRow();
                TableCell tableCellTitle = new TableCell();
                Paragraph paragraphCellTitle = new Paragraph();
                Run runCellTitle = new Run();
                Text textCellTitle = new Text("изменений в штатах " + decreeoperationManagement.SortTree[0]);
                runCellTitle.Append(textCellTitle);
                paragraphCellTitle.Append(runCellTitle);
                TableCellProperties tableCellTitleProperties = new TableCellProperties();
                tableCellTitleProperties.Append(new VerticalMerge()
                {
                    Val = MergedCellValues.Restart
                });
                tableCellTitleProperties.Append(new TableCellVerticalAlignment()
                {
                    Val = TableVerticalAlignmentValues.Center
                });
                tableCellTitle.Append(tableCellTitleProperties);
                tableCellTitle.Append(paragraphCellTitle);
                AppendFontDefault(runCellTitle);
                AppendFontSize(runCellTitle, "28");
                AppendParagraphCenter(paragraphCellTitle);
                rowTitle.Append(tableCellTitle);
                tableTitle.Append(rowTitle);
                docBody.Append(tableTitle);

                /**
                 * Paragraph between main table and title table.
                 */
                Paragraph paragraphBetween = new Paragraph(new Run(new Text("")));
                docBody.Append(paragraphBetween);


                /**
                 * Main table.
                 */
                Table tableMain = new Table();
                TableProperties tableProperties = new TableProperties();
                TableBorders tableBorders = new TableBorders();

                InsideVerticalBorder insideVBorder = new InsideVerticalBorder();
                insideVBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                insideVBorder.Color = "000000";
                tableBorders.AppendChild(insideVBorder);
                /*InsideHorizontalBorder insideHBorder = new InsideHorizontalBorder();
                insideHBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                insideHBorder.Color = "000000";
                tableBorders.AppendChild(insideHBorder);*/
                tableProperties.AppendChild(tableBorders);
                tableMain.AppendChild(tableProperties);

                /**
                 * Top row
                 */
                TableRow rowTop = new TableRow();
                TableRow rowTopBottom = new TableRow();
                TableRowProperties rowTopProperties = new TableRowProperties();
                rowTopProperties.AppendChild(new TableHeader());
                rowTop.AppendChild(rowTopProperties);
                TableRowProperties rowTopBottomProperties = new TableRowProperties();
                rowTopBottomProperties.AppendChild(new TableHeader());
                rowTopBottom.AppendChild(rowTopBottomProperties);

                TableCell tableCellName = new TableCell();
                Paragraph paragraphCellName = new Paragraph();
                Run runCellName = new Run();
                Text textCellName = new Text("НАИМЕНОВАНИЕ  СТРУКТУРНЫХ ПОДРАЗДЕЛЕНИЙ И  ДОЛЖНОСТЕЙ");
                runCellName.Append(textCellName);
                paragraphCellName.Append(runCellName);
                TableCellProperties tableCellNameProperties = new TableCellProperties();
                tableCellNameProperties.Append(new VerticalMerge()
                {
                    Val = MergedCellValues.Restart
                });
                tableCellNameProperties.Append(new TableCellVerticalAlignment()
                {
                    Val = TableVerticalAlignmentValues.Center
                });
                TableCellBorders tableBordersName = new TableCellBorders();
                TopBorder topBorderName = new TopBorder();
                topBorderName.Val = new EnumValue<BorderValues>(BorderValues.Double);
                topBorderName.Color = "000000";
                tableBordersName.AppendChild(topBorderName);
                tableCellNameProperties.Append(tableBordersName);
                tableCellName.Append(tableCellNameProperties);
                tableCellName.Append(paragraphCellName);
                AppendFontDefault(runCellName);
                AppendFontSize(runCellName, "18");
                AppendParagraphCenter(paragraphCellName);
                rowTop.Append(tableCellName);

                

                TableCell tableCellNameBottom = new TableCell();
                Paragraph paragraphCellNameBottom = new Paragraph();
                Run runCellNameBottom = new Run();
                Text textCellNameBottom = new Text("");
                runCellNameBottom.Append(textCellNameBottom);
                paragraphCellNameBottom.Append(runCellNameBottom);
                TableCellProperties tableCellNameBottomProperties = new TableCellProperties();
                tableCellNameBottomProperties.Append(new VerticalMerge()
                {
                    Val = MergedCellValues.Continue
                });
                TableCellBorders tableBordersNameBottom = new TableCellBorders();
                BottomBorder botBorderNameBottom = new BottomBorder();
                botBorderNameBottom.Val = new EnumValue<BorderValues>(BorderValues.Double);
                botBorderNameBottom.Color = "000000";
                tableBordersNameBottom.AppendChild(botBorderNameBottom);
                tableCellNameBottomProperties.Append(tableBordersNameBottom);
                tableCellNameBottom.Append(tableCellNameBottomProperties);
                tableCellNameBottom.Append(paragraphCellNameBottom);
                AppendFontDefault(runCellNameBottom);
                AppendFontSize(runCellNameBottom, "16");
                AppendParagraphCenter(paragraphCellNameBottom);
                rowTopBottom.Append(tableCellNameBottom);
                

                TableCell tableCellRank = new TableCell();
                Paragraph paragraphCellRank = new Paragraph();
                Run runCellRank = new Run();
                Text textCellRank = new Text("Специальное звание (категория персонала)");
                runCellRank.Append(textCellRank);
                TableCellProperties tableCellRankProperties = new TableCellProperties();
                tableCellRankProperties.Append(new VerticalMerge()
                {
                    Val = MergedCellValues.Restart
                });
                tableCellRankProperties.Append(new TableCellVerticalAlignment()
                {
                    Val = TableVerticalAlignmentValues.Center
                });
                TableCellBorders tableBordersRank = new TableCellBorders();
                TopBorder topBorderRank = new TopBorder();
                topBorderRank.Val = new EnumValue<BorderValues>(BorderValues.Double);
                topBorderRank.Color = "000000";
                tableBordersRank.AppendChild(topBorderRank);
                tableCellRankProperties.Append(tableBordersRank);
                paragraphCellRank.Append(runCellRank);
                tableCellRank.Append(tableCellRankProperties);
                tableCellRank.Append(paragraphCellRank);
                AppendFontDefault(runCellRank);
                AppendFontSize(runCellRank, "16");
                AppendParagraphCenter(paragraphCellRank);
                rowTop.Append(tableCellRank);

                TableCell tableCellRankBottom = new TableCell();
                Paragraph paragraphCellRankBottom = new Paragraph();
                Run runCellRankBottom = new Run();
                Text textCellRankBottom = new Text("");
                runCellRankBottom.Append(textCellRankBottom);
                TableCellProperties tableCellRankBottomProperties = new TableCellProperties();
                tableCellRankBottomProperties.Append(new VerticalMerge()
                {
                    Val = MergedCellValues.Continue
                });
                TableCellBorders tableBordersRankBottom = new TableCellBorders();
                BottomBorder botBorderRankBottom = new BottomBorder();
                botBorderRankBottom.Val = new EnumValue<BorderValues>(BorderValues.Double);
                botBorderRankBottom.Color = "000000";
                tableBordersRankBottom.AppendChild(botBorderRankBottom);
                tableCellRankBottomProperties.Append(tableBordersRankBottom);
                paragraphCellRankBottom.Append(runCellRankBottom);
                tableCellRankBottom.Append(tableCellRankBottomProperties);
                tableCellRankBottom.Append(paragraphCellRankBottom);
                AppendFontDefault(runCellRankBottom);
                AppendFontSize(runCellRankBottom, "16");
                AppendParagraphCenter(paragraphCellRankBottom);
                rowTopBottom.Append(tableCellRankBottom);

                TableCell tableCellQuantity = new TableCell();
                Paragraph paragraphCellQuantity = new Paragraph();
                Run runCellQuantity = new Run();
                Text textCellQuantity = new Text("Количество должностей (единиц техники и транспорта)");
                runCellQuantity.Append(textCellQuantity);
                paragraphCellQuantity.Append(runCellQuantity);
                TableCellProperties tableCellQuantityProperties = new TableCellProperties();
                tableCellQuantityProperties.Append(new HorizontalMerge()
                {
                    Val = MergedCellValues.Restart
                });
                tableCellQuantityProperties.Append(new TableCellVerticalAlignment()
                {
                    Val = TableVerticalAlignmentValues.Center
                });
                TableCellBorders tableBordersQuantity = new TableCellBorders();
                TopBorder topBorderQuantity = new TopBorder();
                topBorderQuantity.Val = new EnumValue<BorderValues>(BorderValues.Double);
                topBorderQuantity.Color = "000000";
                tableBordersQuantity.AppendChild(topBorderQuantity);
                tableCellQuantityProperties.Append(tableBordersQuantity);
                tableCellQuantity.Append(tableCellQuantityProperties);
                tableCellQuantity.Append(paragraphCellQuantity);
                AppendFontDefault(runCellQuantity);
                AppendFontSize(runCellQuantity, "16");
                AppendParagraphCenter(paragraphCellQuantity);
                rowTop.Append(tableCellQuantity);

                TableCell tableCellQuantityRight = new TableCell();
                Paragraph paragraphCellQuantityRight = new Paragraph();
                Run runCellQuantityRight = new Run();
                Text textCellQuantityRight = new Text("Количество 2");
                runCellQuantityRight.Append(textCellQuantityRight);
                paragraphCellQuantityRight.Append(runCellQuantityRight);
                TableCellProperties tableCellQuantityRightProperties = new TableCellProperties();
                tableCellQuantityRightProperties.Append(new HorizontalMerge()
                {
                    Val = MergedCellValues.Continue
                });
                TableCellBorders tableBordersQuantityRight = new TableCellBorders();
                TopBorder topBorderQuantityRight = new TopBorder();
                topBorderQuantityRight.Val = new EnumValue<BorderValues>(BorderValues.Double);
                topBorderQuantityRight.Color = "000000";
                tableBordersQuantityRight.AppendChild(topBorderQuantityRight);
                tableCellQuantityRightProperties.Append(tableBordersQuantityRight);
                tableCellQuantityRight.Append(tableCellQuantityRightProperties);
                tableCellQuantityRight.Append(paragraphCellQuantityRight);
                AppendFontDefault(runCellQuantityRight);
                AppendFontSize(runCellQuantityRight, "16");
                AppendParagraphCenter(paragraphCellQuantityRight);
                rowTop.Append(tableCellQuantityRight);

                TableCell tableCellQuantityBottomLeft = new TableCell();
                Paragraph paragraphCellQuantityBottomLeft = new Paragraph();
                Run runCellQuantityBottomLeft = new Run();
                Text textCellQuantityBottomLeft = new Text("вводится");
                TableCellProperties tableCellQuantityBottomLeftProperties = new TableCellProperties();
                tableCellQuantityBottomLeftProperties.Append(new TableCellVerticalAlignment()
                {
                    Val = TableVerticalAlignmentValues.Center
                });
                TableCellBorders tableBordersQuantityBottomLeft = new TableCellBorders();
                TopBorder topBorderQuantityBottomLeft = new TopBorder();
                topBorderQuantityBottomLeft.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                topBorderQuantityBottomLeft.Color = "000000";
                tableBordersQuantityBottomLeft.AppendChild(topBorderQuantityBottomLeft);
                BottomBorder botBorderQuantityBottomLeft = new BottomBorder();
                botBorderQuantityBottomLeft.Val = new EnumValue<BorderValues>(BorderValues.Double);
                botBorderQuantityBottomLeft.Color = "000000";
                tableBordersQuantityBottomLeft.AppendChild(botBorderQuantityBottomLeft);
                tableCellQuantityBottomLeftProperties.Append(tableBordersQuantityBottomLeft);
                tableCellQuantityBottomLeft.Append(tableCellQuantityBottomLeftProperties);
                runCellQuantityBottomLeft.Append(textCellQuantityBottomLeft);
                paragraphCellQuantityBottomLeft.Append(runCellQuantityBottomLeft);
                tableCellQuantityBottomLeft.Append(paragraphCellQuantityBottomLeft);
                AppendFontDefault(runCellQuantityBottomLeft);
                AppendFontSize(runCellQuantityBottomLeft, "16");
                AppendParagraphCenter(paragraphCellQuantityBottomLeft);
                rowTopBottom.Append(tableCellQuantityBottomLeft);

                TableCell tableCellQuantityBottomRight = new TableCell();
                Paragraph paragraphCellQuantityBottomRight = new Paragraph();
                Run runCellQuantityBottomRight = new Run();
                Text textCellQuantityBottomRight = new Text("сокращ.");
                TableCellProperties tableCellQuantityBottomRightProperties = new TableCellProperties();
                tableCellQuantityBottomRightProperties.Append(new TableCellVerticalAlignment()
                {
                    Val = TableVerticalAlignmentValues.Center
                });
                TableCellBorders tableBordersQuantityBottomRight = new TableCellBorders();
                TopBorder topBorderQuantityBottomRight = new TopBorder();
                topBorderQuantityBottomRight.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                topBorderQuantityBottomRight.Color = "000000";
                tableBordersQuantityBottomRight.AppendChild(topBorderQuantityBottomRight);
                BottomBorder botBorderQuantityBottomRight = new BottomBorder();
                botBorderQuantityBottomRight.Val = new EnumValue<BorderValues>(BorderValues.Double);
                botBorderQuantityBottomRight.Color = "000000";
                tableBordersQuantityBottomRight.AppendChild(botBorderQuantityBottomRight);
                tableCellQuantityBottomRightProperties.Append(tableBordersQuantityBottomRight);
                tableCellQuantityBottomRight.Append(tableCellQuantityBottomRightProperties);
                runCellQuantityBottomRight.Append(textCellQuantityBottomRight);
                paragraphCellQuantityBottomRight.Append(runCellQuantityBottomRight);
                tableCellQuantityBottomRight.Append(paragraphCellQuantityBottomRight);
                AppendFontDefault(runCellQuantityBottomRight);
                AppendFontSize(runCellQuantityBottomRight, "16");
                AppendParagraphCenter(paragraphCellQuantityBottomRight);
                rowTopBottom.Append(tableCellQuantityBottomRight);



                TableCell tableCellSof = new TableCell();
                Paragraph paragraphCellSof = new Paragraph();
                Run runCellSof = new Run();
                Text textCellSof = new Text("Источник финансирования");
                runCellSof.Append(textCellSof);
                paragraphCellSof.Append(runCellSof);
                TableCellProperties tableCellSofProperties = new TableCellProperties();
                tableCellSofProperties.Append(new VerticalMerge()
                {
                    Val = MergedCellValues.Restart
                });
                tableCellSofProperties.Append(new TableCellVerticalAlignment()
                {
                    Val = TableVerticalAlignmentValues.Center
                });
                TableCellBorders tableBordersSof = new TableCellBorders();
                TopBorder topBorderSof = new TopBorder();
                topBorderSof.Val = new EnumValue<BorderValues>(BorderValues.Double);
                topBorderSof.Color = "000000";
                tableBordersSof.AppendChild(topBorderSof);
                tableCellSofProperties.Append(tableBordersSof);
                tableCellSof.Append(tableCellSofProperties);
                tableCellSof.Append(paragraphCellSof);
                AppendFontDefault(runCellSof);
                AppendFontSize(runCellSof, "16");
                AppendParagraphCenter(paragraphCellSof);
                rowTop.Append(tableCellSof);

                TableCell tableCellSofBottom = new TableCell();
                Paragraph paragraphCellSofBottom = new Paragraph();
                Run runCellSofBottom = new Run();
                Text textCellSofBottom = new Text("");
                runCellSofBottom.Append(textCellSofBottom);
                paragraphCellSofBottom.Append(runCellSofBottom);
                TableCellProperties tableCellSofBottomProperties = new TableCellProperties();
                tableCellSofBottomProperties.Append(new VerticalMerge()
                {
                    Val = MergedCellValues.Continue
                });
                TableCellBorders tableBordersSofBottom = new TableCellBorders();
                BottomBorder botBorderSofBottom = new BottomBorder();
                botBorderSofBottom.Val = new EnumValue<BorderValues>(BorderValues.Double);
                botBorderSofBottom.Color = "000000";
                tableBordersSofBottom.AppendChild(botBorderSofBottom);
                tableCellSofBottomProperties.Append(tableBordersSofBottom);
                tableCellSofBottom.Append(tableCellSofBottomProperties);
                tableCellSofBottom.Append(paragraphCellSofBottom);
                AppendFontDefault(runCellSofBottom);
                AppendFontSize(runCellSofBottom, "16");
                AppendParagraphCenter(paragraphCellSofBottom);
                rowTopBottom.Append(tableCellSofBottom);


                TableCell tableCellNotice = new TableCell();
                Paragraph paragraphCellNotice = new Paragraph();
                Run runCellNotice = new Run();
                Text textCellNotice = new Text("Примечание");
                runCellNotice.Append(textCellNotice);
                paragraphCellNotice.Append(runCellNotice);
                TableCellProperties tableCellNoticeProperties = new TableCellProperties();
                tableCellNoticeProperties.Append(new VerticalMerge()
                {
                    Val = MergedCellValues.Restart
                });
                tableCellNoticeProperties.Append(new TableCellVerticalAlignment()
                {
                    Val = TableVerticalAlignmentValues.Center
                });
                TableCellBorders tableBordersNotice = new TableCellBorders();
                TopBorder topBorderNotice = new TopBorder();
                topBorderNotice.Val = new EnumValue<BorderValues>(BorderValues.Double);
                topBorderNotice.Color = "000000";
                tableBordersNotice.AppendChild(topBorderNotice);
                tableCellNoticeProperties.Append(tableBordersNotice);
                tableCellNotice.Append(tableCellNoticeProperties);
                tableCellNotice.Append(paragraphCellNotice);
                AppendFontDefault(runCellNotice);
                AppendFontSize(runCellNotice, "16");
                AppendParagraphCenter(paragraphCellNotice);
                rowTop.Append(tableCellNotice);

                TableCell tableCellNoticeBottom = new TableCell();
                Paragraph paragraphCellNoticeBottom = new Paragraph();
                Run runCellNoticeBottom = new Run();
                Text textCellNoticeBottom = new Text("");
                runCellNoticeBottom.Append(textCellNoticeBottom);
                paragraphCellNoticeBottom.Append(runCellNoticeBottom);
                TableCellProperties tableCellNoticeBottomProperties = new TableCellProperties();
                tableCellNoticeBottomProperties.Append(new VerticalMerge()
                {
                    Val = MergedCellValues.Continue
                });
                TableCellBorders tableBordersNoticeBottom = new TableCellBorders();
                BottomBorder botBorderNoticeBottom = new BottomBorder();
                botBorderNoticeBottom.Val = new EnumValue<BorderValues>(BorderValues.Double);
                botBorderNoticeBottom.Color = "000000";
                tableBordersNoticeBottom.AppendChild(botBorderNoticeBottom);
                tableCellNoticeBottomProperties.Append(tableBordersNoticeBottom);
                tableCellNoticeBottom.Append(tableCellNoticeBottomProperties);
                tableCellNoticeBottom.Append(paragraphCellNoticeBottom);
                AppendFontDefault(runCellNoticeBottom);
                AppendFontSize(runCellNoticeBottom, "16");
                AppendParagraphCenter(paragraphCellNoticeBottom);
                rowTopBottom.Append(tableCellNoticeBottom);

                tableMain.Append(rowTop);
                tableMain.Append(rowTopBottom);
                // END TOP ROW

                /**
                 * /////////////////////////////
                 * F I L L I N G      T A B L E
                 * ////////////////////////////
                 */
                AppendElementsToTable(decree, decreeoperations, tableMain);
                //AppendPositionsToTable(decree, decreeoperations, tableMain);

                docBody.Append(tableMain);

                mainPart.Document.Save();
                wordDocument.Close();
            }

            mem.Position = 0;
            return mem;
        }

        public List<string> FindMissingParents(DecreeoperationManagement dom, List<DecreeoperationManagement> doms, List<PrintEnumElement> printEnumElements)
        {
            List<string> missingParents = new List<string>();
            for (int i = 1; i < dom.TreeRank; i++)
            {
                string parent = "";
                for (int j = 0; j <= i; j++)
                {
                    if (!dom.SortTree[j].Equals(""))
                    {
                        parent += dom.SortTree[j] + Keys.TREE_BEAUTY;
                    }
                    
                }
                if (parent.Length > 0)
                {
                    parent = parent.Remove(parent.Length - Keys.TREE_BEAUTY.Length);
                    missingParents.Add(parent);
                }
                
            }
            foreach (DecreeoperationManagement domChild in doms)
            {
                if (missingParents.Contains(domChild.FullTree))
                {
                    missingParents.Remove(domChild.FullTree);
                }
            }
            foreach (PrintEnumElement pee in printEnumElements)
            {
                if (missingParents.Contains(pee.FullTree))
                {
                    missingParents.Remove(pee.FullTree);
                }
            }
            return missingParents;
        }

        /**
         * New function.
         */
        public void AppendElementsToTable(Decree decree, List<DecreeoperationManagement> decreeoperations, Table table)
        {
            decreeoperations = repository.CompressPositions(decreeoperations);
            List<PrintEnumElement> elements = new List<PrintEnumElement>();
            Dictionary<int, PrintEnumElement> closestStructuresByLevels = new Dictionary<int, PrintEnumElement>(); // Нужно при добавлении элементов в список, чтобы сохранять информацию о численности в 
                                                                                                                   // подразделениях находящихся на одном уровне.

            PrintEnumElement firstStructure = null;
            PrintEnumElement lastStructure = null;
            PrintEnumElement currentStructure = null;
            PrintEnumElement previousElement = null;
            double count = 0;
            double countNegative = 0;

            foreach (DecreeoperationManagement dom in decreeoperations)
            {
                List<string> missingParents = FindMissingParents(dom, decreeoperations, elements);
                foreach(string missingParent in missingParents) // Находит родителей только для текущей записи в итерации...
                {
                    PrintEnumElement pee = new PrintEnumElement();
                    pee.InEnum = false;
                    pee.Position = null;
                    pee.Structure = null;
                    pee.FullTree = missingParent;
                    string[] hierarchy = missingParent.Split(Keys.TREE_BEAUTY);
                    pee.Level = hierarchy.Length - 1;
                    pee.Name = hierarchy[hierarchy.Length - 1];
                    elements.Add(pee);

                    /**
                    * Add closest structure by level
                    */
                    if (!closestStructuresByLevels.ContainsKey(pee.Level))
                    {
                        closestStructuresByLevels.Add(pee.Level, pee);
                    }

                    CalcStructures(lastStructure, currentStructure, closestStructuresByLevels, count, countNegative);

                    count = 0;
                    countNegative = 0;
                    
                    if (firstStructure == null)
                    {
                        firstStructure = pee;
                    }

                    lastStructure = currentStructure;
                    currentStructure = pee;

                    if (firstStructure == null)
                    {
                        firstStructure = lastStructure;
                    }
                }

                PrintEnumElement peeDom = new PrintEnumElement();
                peeDom.InEnum = true;
                peeDom.Position = null;
                peeDom.Structure = null;
                peeDom.FullTree = dom.FullTree;
                string[] hierarchyDom = peeDom.FullTree.Split(Keys.TREE_BEAUTY);
                peeDom.Level = hierarchyDom.Length - 1;
                peeDom.Name = hierarchyDom[hierarchyDom.Length - 1];
                peeDom.DecreeoperationManagement = dom;


                if (dom.Subject < 0)
                {
                    int minusid = -dom.Subject;
                    Structure structure = repository.StructuresLocal().GetValue(minusid);
                        //Structure structure = repository.Structures.First(s => s.Id == dom.Subject);
                    peeDom.Structure = structure;

                    //if (peeDom.CountByLevels.Count == 0)
                    //{
                    //    peeDom.CountByLevels.Add(0);
                    //}
                    //if (peeDom.CountByLevelsNegative.Count == 0)
                    //{
                    //    peeDom.CountByLevelsNegative.Add(0);
                    //}
                    //if (firstStructure == null)
                    //{
                    //    firstStructure = peeDom;
                    //}
                } else { 
                        Models.Position position = repository.Positions.First(p => p.Id == dom.Subject);
                        peeDom.Position = position;

                        count += dom.CompressionAdded;
                        countNegative += dom.CompressionDeleted;
                        //lastStructure.CountByLevels[0] += dom.CompressionAdded;
                        //lastStructure.CountByLevelsNegative[0] += dom.CompressionDeleted;
                }
                

                
                /**
                 * Last structure.
                 */
                if (dom.Subject < 0)
                {
                    CalcStructures(lastStructure, currentStructure, closestStructuresByLevels, count, countNegative);
                    
                    if (dom != decreeoperations.Last())
                    {
                        count = 0;
                        countNegative = 0;
                    }

                    lastStructure = currentStructure;
                    currentStructure = peeDom;
                    if (firstStructure == null)
                    {
                        firstStructure = peeDom;
                    }
                }

                previousElement = peeDom;
                elements.Add(peeDom);
            }
            CalcStructures(lastStructure, currentStructure, closestStructuresByLevels, count, countNegative);
            currentStructure.LevelDifference = currentStructure.Level - firstStructure.Level;

            previousElement = null;
            currentStructure = null;
            lastStructure = null;

            bool departmentWithPositions = false;

            
            /**
             * When all elements are ready, we are iterating through it
             */
            foreach (PrintEnumElement element in elements)
            {
                /**
                 * Putting custom date to an element.
                 * НА ЗАВТРА, ЧТОБЫ ДАТЫ БЫЛИ И У ОТДЕЛОВ И ПОДРАЗДЕЛЕНИЙ
                 */
                bool datecustom = false;
                DateTime dateactive = new DateTime();
                if (element.DecreeoperationManagement != null && element.DecreeoperationManagement.Datecustom > 0)
                {
                    datecustom = true;
                    dateactive = element.DecreeoperationManagement.Dateactive.GetValueOrDefault();
                }


                /**
                 * Iterating through structures.
                 */
                if (!element.InEnum || element.Structure != null)
                {
                    
                    /**
                    * If department/structure was with positions, print summary
                    */
                    if (departmentWithPositions && !(lastStructure != null && lastStructure.CountByLevels.Count == 0 && lastStructure.CountByLevelsNegative.Count == 0 && decreeoperations.First().SortTree[0].Equals(previousElement.Name)))
                    {
                        for (int level = 0; level < lastStructure.LevelDifference + 1; level++)
                        {
                            TableRow summaryCountRow = new TableRow();

                            for (int i = 0; i < 2; i++)
                            {
                                TableCell cellEmpty = new TableCell(new Paragraph(new Run(new Text(""))));
                                summaryCountRow.Append(cellEmpty);
                            }

                            TableCell tableCellPositionSummaryAdded = new TableCell();
                            Paragraph paragraphCellPositionSummaryAdded = new Paragraph();
                            Run runCellPositionSummaryAdded = new Run();
                            String countSummaryAdded = "-";
                            if (lastStructure.CountByLevels.Count > level)
                            {
                                countSummaryAdded = lastStructure.CountByLevels[level].ToString();
                            }
                            Text textCellPositionSummaryAdded = new Text(countSummaryAdded);
                            runCellPositionSummaryAdded.Append(textCellPositionSummaryAdded);
                            paragraphCellPositionSummaryAdded.Append(runCellPositionSummaryAdded);
                            tableCellPositionSummaryAdded.Append(paragraphCellPositionSummaryAdded);
                            TableCellProperties tableCellSummaryAddedProperties = new TableCellProperties();
                            TableCellBorders tableBordersSummaryAdded = new TableCellBorders();
                            TopBorder topBorderSummaryAdded = new TopBorder();
                            topBorderSummaryAdded.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                            topBorderSummaryAdded.Color = "000000";
                            tableBordersSummaryAdded.AppendChild(topBorderSummaryAdded);
                            tableCellSummaryAddedProperties.Append(tableBordersSummaryAdded);
                            tableCellPositionSummaryAdded.Append(tableCellSummaryAddedProperties);
                            AppendFontDefault(runCellPositionSummaryAdded);
                            AppendParagraphCenter(paragraphCellPositionSummaryAdded);
                            AppendFontSize(runCellPositionSummaryAdded, "28");
                            summaryCountRow.Append(tableCellPositionSummaryAdded);

                            TableCell tableCellPositionSummaryDeleted = new TableCell();
                            Paragraph paragraphCellPositionSummaryDeleted = new Paragraph();
                            Run runCellPositionSummaryDeleted = new Run();
                            String countSummaryDeleted = "-";
                            if (lastStructure.CountByLevelsNegative.Count > level)
                            {
                                countSummaryDeleted = lastStructure.CountByLevelsNegative[level].ToString();
                            }
                            Text textCellPositionSummaryDeleted = new Text(countSummaryDeleted);
                            runCellPositionSummaryDeleted.Append(textCellPositionSummaryDeleted);
                            paragraphCellPositionSummaryDeleted.Append(runCellPositionSummaryDeleted);
                            tableCellPositionSummaryDeleted.Append(paragraphCellPositionSummaryDeleted);
                            TableCellProperties tableCellSummaryDeletedProperties = new TableCellProperties();
                            TableCellBorders tableBordersSummaryDeleted = new TableCellBorders();
                            TopBorder topBorderSummaryDeleted = new TopBorder();
                            topBorderSummaryDeleted.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                            topBorderSummaryDeleted.Color = "000000";
                            tableBordersSummaryDeleted.AppendChild(topBorderSummaryDeleted);
                            tableCellSummaryDeletedProperties.Append(tableBordersSummaryDeleted);
                            tableCellPositionSummaryDeleted.Append(tableCellSummaryDeletedProperties);
                            AppendFontDefault(runCellPositionSummaryDeleted);
                            AppendParagraphCenter(paragraphCellPositionSummaryDeleted);
                            AppendFontSize(runCellPositionSummaryDeleted, "28");
                            summaryCountRow.Append(tableCellPositionSummaryDeleted);

                            for (int i = 0; i < 2; i++)
                            {
                                TableCell cellEmpty = new TableCell(new Paragraph(new Run(new Text(""))));
                                summaryCountRow.Append(cellEmpty);
                            }
                            table.Append(summaryCountRow);
                        }

                        
                    }


                    TableRow departmentRow = new TableRow();
                    TableCell tableCellDepartment = new TableCell();
                    Paragraph paragraphCellDepartment = new Paragraph();
                    Run runCellDepartment = new Run();
                    runCellDepartment.Append(new Break());
                    string elName = element.Name;
                    if (element.DecreeoperationManagement != null && element.DecreeoperationManagement.Changed > 0)
                    {
                        Structure previousStructure = repository.Structures.First(s => s.Id == element.DecreeoperationManagement.Changedtype);
                        elName = previousStructure.Name;
                    }
                    Text textCellDepartment = new Text(elName);
                    runCellDepartment.Append(textCellDepartment);
                    paragraphCellDepartment.Append(runCellDepartment);
                    TableCellProperties tableCellDepartmentProperties = new TableCellProperties();
                    TableCellBorders tableBordersDepartment = new TableCellBorders();
                    BottomBorder bottomBorderDepartment = new BottomBorder();
                    bottomBorderDepartment.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    bottomBorderDepartment.Color = "000000";
                    tableBordersDepartment.AppendChild(bottomBorderDepartment);
                    tableCellDepartmentProperties.Append(tableBordersDepartment);
                    tableCellDepartment.Append(tableCellDepartmentProperties);
                    tableCellDepartment.Append(paragraphCellDepartment);
                    AppendFontDefault(runCellDepartment);
                    AppendFontSize(runCellDepartment, "28");
                    AppendParagraphCenter(paragraphCellDepartment);
                    departmentRow.Append(tableCellDepartment);
                    for (int i = 0; i < 5; i++)
                    {
                        TableCell cellEmpty = new TableCell(new Paragraph(new Run(new Text(""))));
                        departmentRow.Append(cellEmpty);
                    }

                    if (!decreeoperations.First().SortTree[0].Equals(elName)) // Do not display first structure
                    {
                        table.Append(departmentRow);
                    }
                    

                    /**
                     * STRUCTURE Insert added/deleted row. Doesn't affect first structure
                     */
                    if (element.Structure != null && !decreeoperations.First().SortTree[0].Equals(elName))
                    {

                        if (element.DecreeoperationManagement.Changed == 0)
                        {
                            string additional = "(";
                            if (element.DecreeoperationManagement.Created > 0)
                            {
                                additional += "создается";
                            }
                            else
                            {
                                additional += "упраздняется";
                            }
                            additional += ")";

                            TableRow departmentRowAdditional = new TableRow();
                            TableCell tableCellDepartmentAdditional = new TableCell();
                            Paragraph paragraphCellDepartmentAdditional = new Paragraph();
                            Run runCellDepartmentAdditional = new Run();
                            runCellDepartmentAdditional.Append(new Break());
                            Text textCellDepartmentAdditional = new Text(additional);
                            runCellDepartmentAdditional.Append(textCellDepartmentAdditional);
                            paragraphCellDepartmentAdditional.Append(runCellDepartmentAdditional);
                            TableCellProperties tableCellDepartmentPropertiesAdditional = new TableCellProperties();
                            tableCellDepartmentAdditional.Append(tableCellDepartmentPropertiesAdditional);
                            tableCellDepartmentAdditional.Append(paragraphCellDepartmentAdditional);
                            AppendFontDefault(runCellDepartmentAdditional);
                            AppendFontSize(runCellDepartmentAdditional, "28");
                            AppendParagraphCenter(paragraphCellDepartmentAdditional);
                            departmentRowAdditional.Append(tableCellDepartmentAdditional);
                            for (int i = 0; i < 5; i++)
                            {
                                if (i != 4)
                                {
                                    TableCell cellEmpty = new TableCell(new Paragraph(new Run(new Text(""))));
                                    departmentRowAdditional.Append(cellEmpty);
                                }
                                else
                                {
                                    TableCell tableCellPositionNotice = new TableCell();
                                    Paragraph paragraphCellPositionNotice = new Paragraph();
                                    Run runCellPositionNotice = new Run();
                                    runCellPositionNotice.Append(new Break());
                                    string notice = "";
                                    if (datecustom == true)
                                    {
                                        notice = "c " + dateactive.ToString("dd/MM/yyyy");
                                    }
                                    Text textCellPositionNotice = new Text(notice);
                                    runCellPositionNotice.Append(textCellPositionNotice);
                                    paragraphCellPositionNotice.Append(runCellPositionNotice);
                                    tableCellPositionNotice.Append(paragraphCellPositionNotice);
                                    AppendFontDefault(runCellPositionNotice);
                                    AppendFontSize(runCellPositionNotice, "28");
                                    departmentRowAdditional.Append(tableCellPositionNotice);
                                }

                            }
                            table.Append(departmentRowAdditional);
                        /**
                         * Structure renaming
                         */
                        }
                        else if (element.Structure.Changestructurerename > 0)
                        {
                            string additional = "Переименовывается в";

                            TableRow departmentRowAdditional = new TableRow();
                            TableCell tableCellDepartmentAdditional = new TableCell();
                            Paragraph paragraphCellDepartmentAdditional = new Paragraph();
                            Run runCellDepartmentAdditional = new Run();
                            runCellDepartmentAdditional.Append(new Break());
                            Text textCellDepartmentAdditional = new Text(additional);
                            runCellDepartmentAdditional.Append(textCellDepartmentAdditional);
                            paragraphCellDepartmentAdditional.Append(runCellDepartmentAdditional);
                            TableCellProperties tableCellDepartmentPropertiesAdditional = new TableCellProperties();
                            tableCellDepartmentAdditional.Append(tableCellDepartmentPropertiesAdditional);
                            tableCellDepartmentAdditional.Append(paragraphCellDepartmentAdditional);
                            AppendFontDefault(runCellDepartmentAdditional);
                            AppendFontSize(runCellDepartmentAdditional, "28");
                            AppendParagraphCenter(paragraphCellDepartmentAdditional);
                            departmentRowAdditional.Append(tableCellDepartmentAdditional);
                            for (int i = 0; i < 5; i++)
                            {
                                if (i != 4)
                                {
                                    TableCell cellEmpty = new TableCell(new Paragraph(new Run(new Text(""))));
                                    departmentRowAdditional.Append(cellEmpty);

                                }
                                else
                                {
                                    TableCell tableCellPositionNotice = new TableCell();
                                    Paragraph paragraphCellPositionNotice = new Paragraph();
                                    Run runCellPositionNotice = new Run();
                                    runCellPositionNotice.Append(new Break());
                                    string notice = "";
                                    if (datecustom == true)
                                    {
                                        notice = "c " + dateactive.ToString("dd/MM/yyyy");
                                    }
                                    Text textCellPositionNotice = new Text(notice);
                                    runCellPositionNotice.Append(textCellPositionNotice);
                                    paragraphCellPositionNotice.Append(runCellPositionNotice);
                                    tableCellPositionNotice.Append(paragraphCellPositionNotice);
                                    AppendFontDefault(runCellPositionNotice);
                                    AppendFontSize(runCellPositionNotice, "28");
                                    departmentRowAdditional.Append(tableCellPositionNotice);
                                }

                            }
                            table.Append(departmentRowAdditional);

                            TableRow departmentRowNew = new TableRow();
                            TableCell tableCellDepartmentNew = new TableCell();
                            Paragraph paragraphCellDepartmentNew = new Paragraph();
                            Run runCellDepartmentNew = new Run();
                            runCellDepartmentNew.Append(new Break());
                            Text textCellDepartmentNew = new Text(element.Structure.Name);
                            runCellDepartmentNew.Append(textCellDepartmentNew);
                            paragraphCellDepartmentNew.Append(runCellDepartmentNew);
                            TableCellProperties tableCellDepartmentPropertiesNew = new TableCellProperties();
                            TableCellBorders tableBordersDepartmentNew = new TableCellBorders();
                            BottomBorder bottomBorderDepartmentNew = new BottomBorder();
                            bottomBorderDepartmentNew.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                            bottomBorderDepartmentNew.Color = "000000";
                            tableBordersDepartmentNew.AppendChild(bottomBorderDepartmentNew);
                            tableCellDepartmentPropertiesNew.Append(tableBordersDepartmentNew);
                            tableCellDepartmentNew.Append(tableCellDepartmentPropertiesNew);
                            tableCellDepartmentNew.Append(paragraphCellDepartmentNew);
                            AppendFontDefault(runCellDepartmentNew);
                            AppendFontSize(runCellDepartmentNew, "28");
                            AppendParagraphCenter(paragraphCellDepartmentNew);
                            departmentRowNew.Append(tableCellDepartmentNew);
                            for (int i = 0; i < 5; i++)
                            {
                                TableCell cellEmpty = new TableCell(new Paragraph(new Run(new Text(""))));
                                departmentRowNew.Append(cellEmpty);
                            }
                            table.Append(departmentRowNew);
                        }

                    }

                    /**
                     * Reset counter.
                     */
                    departmentWithPositions = false;

                    /**
                     *  Start department's position counter.
                     */
                    if ( element.Structure != null)
                    {
                        departmentWithPositions = true;
                    }

                    lastStructure = element;
                /**
                 * Position.
                 */
                } else
                {
                    TableRow positionRow = new TableRow();

                    TableCell tableCellPositionName = new TableCell();
                    Paragraph paragraphCellPositionName = new Paragraph();
                    Run runCellPositionName = new Run();
                    runCellPositionName.Append(new Break());
                    string positionName = PositionName(element.Position.Positiontype);
                    bool replacedByCivil = false;
                    string civilName = null;
                    int sof = element.Position.Sourceoffinancing;
                    int rankCap = element.Position.Cap.GetValueOrDefault(0);
                    
                    if (element.Position.Replacedbycivil > 0)
                    {
                        replacedByCivil = true;
                        if (element.Position.Replacedbycivilpositiontype > 0)
                        {
                            civilName = PositionName(element.Position.Replacedbycivilpositiontype);
                        }
                        else
                        {
                            civilName = null;
                        }
                    }

                    if (replacedByCivil && civilName != null && !civilName.Equals(positionName))
                    {
                        positionName += " (" + civilName + ")";
                    }
                    Text textCellPositionName = new Text(positionName);
                    runCellPositionName.Append(textCellPositionName);
                    paragraphCellPositionName.Append(runCellPositionName);
                    tableCellPositionName.Append(paragraphCellPositionName);
                    AppendFontDefault(runCellPositionName);
                    AppendFontSize(runCellPositionName, "28");
                    positionRow.Append(tableCellPositionName);

                    TableCell tableCellPositionRank = new TableCell();
                    Paragraph paragraphCellPositionRank = new Paragraph();
                    Run runCellPositionRank = new Run();
                    runCellPositionRank.Append(new Break());
                    String rankName = "";
                    if (rankCap > 0)
                    {
                        rankName = repository.Ranks.First(r => r.Id == rankCap).Name;
                        if (replacedByCivil)
                        {
                            rankName += "*";
                        }
                    }
                    else if (repository.PositioncategoriesLocal()[element.Position.Positioncategory].Civil > 0)
                    {
                        rankName = repository.PositioncategoriesLocal()[element.Position.Positioncategory].Name;
                    }
                    Text textCellPositionRank = new Text(rankName.ToLower());
                    runCellPositionRank.Append(textCellPositionRank);
                    paragraphCellPositionRank.Append(runCellPositionRank);
                    tableCellPositionRank.Append(paragraphCellPositionRank);
                    AppendFontDefault(runCellPositionRank);
                    AppendFontSize(runCellPositionRank, "28");
                    positionRow.Append(tableCellPositionRank);

                    TableCell tableCellPositionAdded = new TableCell();
                    Paragraph paragraphCellPositionAdded = new Paragraph();
                    Run runCellPositionAdded = new Run();
                    runCellPositionAdded.Append(new Break());
                    String countAdded = "-";
                    
                    if (element.DecreeoperationManagement.CompressionAdded > 0)
                    {
                        countAdded = element.DecreeoperationManagement.CompressionAdded.ToString();
                    }
                    Text textCellPositionAdded = new Text(countAdded);
                    runCellPositionAdded.Append(textCellPositionAdded);
                    paragraphCellPositionAdded.Append(runCellPositionAdded);
                    tableCellPositionAdded.Append(paragraphCellPositionAdded);
                    AppendFontDefault(runCellPositionAdded);
                    AppendParagraphCenter(paragraphCellPositionAdded);
                    AppendFontSize(runCellPositionAdded, "28");
                    positionRow.Append(tableCellPositionAdded);

                    TableCell tableCellPositionDeleted = new TableCell();
                    Paragraph paragraphCellPositionDeleted = new Paragraph();
                    Run runCellPositionDeleted = new Run();
                    runCellPositionDeleted.Append(new Break());
                    String countDeleted = "-";
                    if (element.DecreeoperationManagement.CompressionDeleted > 0)
                    {
                        countDeleted = element.DecreeoperationManagement.CompressionDeleted.ToString();
                    }
                    Text textCellPositionDeleted = new Text(countDeleted);
                    runCellPositionDeleted.Append(textCellPositionDeleted);
                    paragraphCellPositionDeleted.Append(runCellPositionDeleted);
                    tableCellPositionDeleted.Append(paragraphCellPositionDeleted);
                    AppendFontDefault(runCellPositionDeleted);
                    AppendParagraphCenter(paragraphCellPositionDeleted);
                    AppendFontSize(runCellPositionDeleted, "28");
                    positionRow.Append(tableCellPositionDeleted);

                    TableCell tableCellPositionSof = new TableCell();
                    Paragraph paragraphCellPositionSof = new Paragraph();
                    Run runCellPositionSof = new Run();
                    runCellPositionSof.Append(new Break());
                    String sofName = "";
                    if (sof > 0)
                    {
                        sofName = repository.Sourcesoffinancings.First(s => s.Id == sof).Name;
                    }
                    Text textCellPositionSof = new Text(ShortifySOF(sofName));
                    runCellPositionSof.Append(textCellPositionSof);
                    paragraphCellPositionSof.Append(runCellPositionSof);
                    AppendParagraphCenter(paragraphCellPositionSof);
                    tableCellPositionSof.Append(paragraphCellPositionSof);
                    AppendFontDefault(runCellPositionSof);
                    AppendFontSize(runCellPositionSof, "28");
                    positionRow.Append(tableCellPositionSof);

                    TableCell tableCellPositionNotice = new TableCell();
                    Paragraph paragraphCellPositionNotice = new Paragraph();
                    Run runCellPositionNotice = new Run();
                    runCellPositionNotice.Append(new Break());
                    string notice = "";
                    if (datecustom == true)
                    {
                        notice = "c " + dateactive.ToString("dd/MM/yyyy");
                    }
                    Text textCellPositionNotice = new Text(notice);
                    runCellPositionNotice.Append(textCellPositionNotice);
                    paragraphCellPositionNotice.Append(runCellPositionNotice);
                    tableCellPositionNotice.Append(paragraphCellPositionNotice);
                    AppendFontDefault(runCellPositionNotice);
                    AppendFontSize(runCellPositionNotice, "28");
                    positionRow.Append(tableCellPositionNotice);

                    table.Append(positionRow);
                }

                previousElement = element;
            }
            // Здесь заканчивается итерация через элементы

            /**
             * If department was with positions, print summary. For last structure
             * То есть, когда итерация окончилась, мы делаем подбивку для последнего подразделения
             */
            if (departmentWithPositions)
            {
                for (int level = 0; level < lastStructure.LevelDifference + 1; level++)
                {
                    TableRow summaryCountRow = new TableRow();

                    for (int i = 0; i < 2; i++)
                    {
                        TableCell cellEmpty = new TableCell(new Paragraph(new Run(new Text(""))));
                        summaryCountRow.Append(cellEmpty);
                    }

                    TableCell tableCellPositionSummaryAdded = new TableCell();
                    Paragraph paragraphCellPositionSummaryAdded = new Paragraph();
                    Run runCellPositionSummaryAdded = new Run();
                    String countSummaryAdded = "-";
                    if (lastStructure.CountByLevels.Count > 0 && lastStructure.CountByLevels[level] > 0)
                    {
                        countSummaryAdded = lastStructure.CountByLevels[level].ToString();
                    }
                    Text textCellPositionSummaryAdded = new Text(countSummaryAdded);
                    runCellPositionSummaryAdded.Append(textCellPositionSummaryAdded);
                    paragraphCellPositionSummaryAdded.Append(runCellPositionSummaryAdded);
                    tableCellPositionSummaryAdded.Append(paragraphCellPositionSummaryAdded);
                    TableCellProperties tableCellSummaryAddedProperties = new TableCellProperties();
                    TableCellBorders tableBordersSummaryAdded = new TableCellBorders();
                    TopBorder topBorderSummaryAdded = new TopBorder();
                    topBorderSummaryAdded.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    topBorderSummaryAdded.Color = "000000";
                    tableBordersSummaryAdded.AppendChild(topBorderSummaryAdded);
                    tableCellSummaryAddedProperties.Append(tableBordersSummaryAdded);
                    tableCellPositionSummaryAdded.Append(tableCellSummaryAddedProperties);
                    AppendFontDefault(runCellPositionSummaryAdded);
                    AppendParagraphCenter(paragraphCellPositionSummaryAdded);
                    AppendFontSize(runCellPositionSummaryAdded, "28");
                    summaryCountRow.Append(tableCellPositionSummaryAdded);

                    TableCell tableCellPositionSummaryDeleted = new TableCell();
                    Paragraph paragraphCellPositionSummaryDeleted = new Paragraph();
                    Run runCellPositionSummaryDeleted = new Run();
                    String countSummaryDeleted = "-";
                    if (lastStructure.CountByLevelsNegative.Count > level && lastStructure.CountByLevelsNegative[level] > 0)
                    {
                        countSummaryDeleted = lastStructure.CountByLevelsNegative[level].ToString();
                    }
                    Text textCellPositionSummaryDeleted = new Text(countSummaryDeleted);
                    runCellPositionSummaryDeleted.Append(textCellPositionSummaryDeleted);
                    paragraphCellPositionSummaryDeleted.Append(runCellPositionSummaryDeleted);
                    tableCellPositionSummaryDeleted.Append(paragraphCellPositionSummaryDeleted);
                    TableCellProperties tableCellSummaryDeletedProperties = new TableCellProperties();
                    TableCellBorders tableBordersSummaryDeleted = new TableCellBorders();
                    TopBorder topBorderSummaryDeleted = new TopBorder();
                    topBorderSummaryDeleted.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    topBorderSummaryDeleted.Color = "000000";
                    tableBordersSummaryDeleted.AppendChild(topBorderSummaryDeleted);
                    tableCellSummaryDeletedProperties.Append(tableBordersSummaryDeleted);
                    tableCellPositionSummaryDeleted.Append(tableCellSummaryDeletedProperties);
                    AppendFontDefault(runCellPositionSummaryDeleted);
                    AppendParagraphCenter(paragraphCellPositionSummaryDeleted);
                    AppendFontSize(runCellPositionSummaryDeleted, "28");
                    summaryCountRow.Append(tableCellPositionSummaryDeleted);

                    for (int i = 0; i < 2; i++)
                    {
                        TableCell cellEmpty = new TableCell(new Paragraph(new Run(new Text(""))));
                        summaryCountRow.Append(cellEmpty);
                    }
                    table.Append(summaryCountRow);
                }

                 
            }
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

        public string ShortifySOF(String sof)
        {
            String newString = "";
            sof.Split(' ').ToList().ForEach(i => newString += i[0]);
            newString = newString.ToUpper();
            return newString;
        }

        public string PositionName(int positiontype)
        {
            return repository.Positiontypes.First(p => p.Id == positiontype).Name;
        }


        public void CalcStructures(PrintEnumElement lastStructure, PrintEnumElement peeDom, Dictionary<int, PrintEnumElement> closestStructuresByLevels, double count, double countNegative)
        {
            if (peeDom != null)
            {
                    peeDom.PureCount = count;
                    peeDom.CountByLevels.Add(count);

                /**
                 * Add closest structure by level
                 */
                if (!closestStructuresByLevels.ContainsKey(peeDom.Level))
                {
                    closestStructuresByLevels.Add(peeDom.Level, peeDom);
                }

                if (lastStructure != null && peeDom.Level > lastStructure.Level)
                {
                    peeDom.Level = lastStructure.Level + 1; // Чтобы различие в уровне было только на 1, избежать случайных скачков в уровнях.
                }


                if (lastStructure != null && peeDom.Level > lastStructure.Level)
                {
                    foreach (double countByLevel in lastStructure.CountByLevels)
                    {
                        double sum = countByLevel + count;
                        peeDom.CountByLevels.Add(sum);
                    }

                    for (int i = 0; i < peeDom.Level; i++)
                    {
                        if (closestStructuresByLevels.ContainsKey(i))
                        {
                            closestStructuresByLevels[i].CountByLevelsWithChildren += peeDom.CountByLevels.First(); // В родителей докидываем численность детей.
                            // System.Collections.Generic.KeyNotFoundException: 'The given key was not present in the dictionary.'
                        }
                        
                    }
                }
                else if (lastStructure != null && peeDom.Level == lastStructure.Level)
                {
                    int indexer = 0;
                    foreach (double countByLevel in lastStructure.CountByLevels)
                    {
                        if (indexer > 0)
                        {
                            peeDom.CountByLevels.Add(countByLevel + count);
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
                else if (lastStructure != null && peeDom.Level < lastStructure.Level)
                {
                    int indexer = 0;
                    if (closestStructuresByLevels.ContainsKey(peeDom.Level))
                    {
                        foreach (double countByLevel in closestStructuresByLevels[peeDom.Level].CountByLevels.ToList())
                        {
                            if (indexer > 0)
                            {
                                //peeDom.CountByLevels.Add(countByLevel + positions.Count() + closestStructuresByLevels[peeDom.Level].CountByLevelsWithChildren);
                                double sum = countByLevel + count + closestStructuresByLevels[peeDom.Level].CountByLevelsWithChildren;
                                peeDom.CountByLevels.Add(sum);
                            }

                            indexer++;
                        }
                    }
                    for (int i = 0; i < peeDom.Level; i++)
                    {
                        if (closestStructuresByLevels.ContainsKey(i)) // Без этого ошибку дает
                        {
                            closestStructuresByLevels[i].CountByLevelsWithChildren += peeDom.CountByLevels.First(); // В родителей докидываем численность детей.
                        }
                            
                    }

                    closestStructuresByLevels[peeDom.Level] = peeDom;

                }

                if (lastStructure != null && peeDom.Level < lastStructure.Level)
                {
                    lastStructure.LevelDifference = lastStructure.Level - peeDom.Level;
                }
            }


            if (peeDom != null)
            {
                peeDom.PureCountNegative = countNegative;
                peeDom.CountByLevelsNegative.Add(countNegative);

                /**
                 * Add closest structure by level
                 */
                if (!closestStructuresByLevels.ContainsKey(peeDom.Level))
                {
                    closestStructuresByLevels.Add(peeDom.Level, peeDom);
                }

                if (lastStructure != null && peeDom.Level > lastStructure.Level)
                {
                    peeDom.Level = lastStructure.Level + 1; // Чтобы различие в уровне было только на 1, избежать случайных скачков в уровнях.
                }


                if (lastStructure != null && peeDom.Level > lastStructure.Level)
                {
                    foreach (double countByLevelNegative in lastStructure.CountByLevelsNegative)
                    {
                        double sum = countByLevelNegative + countNegative;
                        peeDom.CountByLevelsNegative.Add(sum);
                    }

                    for (int i = 0; i < peeDom.Level; i++)
                    {
                        if (closestStructuresByLevels.ContainsKey(i))
                        {
                            closestStructuresByLevels[i].CountByLevelsWithChildrenNegative += peeDom.CountByLevelsNegative.First(); // В родителей докидываем численность детей.
                            // System.Collections.Generic.KeyNotFoundException: 'The given key was not present in the dictionary.'
                        }

                    }
                }
                else if (lastStructure != null && peeDom.Level == lastStructure.Level)
                {
                    int indexer = 0;
                    foreach (double countByLevelNegative in lastStructure.CountByLevelsNegative)
                    {
                        if (indexer > 0)
                        {
                            peeDom.CountByLevelsNegative.Add(countByLevelNegative + countNegative);
                        }

                        indexer++;
                    }
                    //peeDom.CountByLevelsWithChildren += closestStructuresByLevels[peeDom.Level].CountByLevelsWithChildren;
                    for (int i = 0; i < peeDom.Level; i++)
                    {
                        if (closestStructuresByLevels.ContainsKey(i))
                        {
                            closestStructuresByLevels[i].CountByLevelsWithChildrenNegative += peeDom.CountByLevelsNegative.First(); // В родителей докидываем численность детей.
                        }
                            
                    }
                    closestStructuresByLevels[peeDom.Level] = peeDom;

                }
                else if (lastStructure != null && peeDom.Level < lastStructure.Level)
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
                        foreach (double countByLevelNegative in closestStructuresByLevels[peeDom.Level].CountByLevelsNegative.ToList())
                        {
                            if (indexer > 0)
                            {
                                //peeDom.CountByLevels.Add(countByLevel + positions.Count() + closestStructuresByLevels[peeDom.Level].CountByLevelsWithChildren);
                                double sum = countByLevelNegative + countNegative + closestStructuresByLevels[peeDom.Level].CountByLevelsWithChildrenNegative;
                                peeDom.CountByLevelsNegative.Add(sum);
                            }

                            indexer++;
                        }
                    }
                    for (int i = 0; i < peeDom.Level; i++)
                    {
                        if (closestStructuresByLevels.ContainsKey(i))
                        {
                            closestStructuresByLevels[i].CountByLevelsWithChildrenNegative += peeDom.CountByLevelsNegative.First(); // В родителей докидываем численность детей.
                        }
                            
                    }

                    closestStructuresByLevels[peeDom.Level] = peeDom;

                }

                if (lastStructure != null && peeDom.Level < lastStructure.Level)
                {
                    lastStructure.LevelDifference = lastStructure.Level - peeDom.Level;
                }
            }
        }


        /**
         * OLD
         */
        public void old(PrintEnumElement lastStructure, PrintEnumElement peeDom, Dictionary<int, PrintEnumElement> closestStructuresByLevels, double count, double countNegative)
        {
            if (peeDom != null)
            {
                peeDom.PureCount = count;
                peeDom.CountByLevels.Add(count);

                /**
                 * Add closest structure by level
                 */
                if (!closestStructuresByLevels.ContainsKey(peeDom.Level))
                {
                    closestStructuresByLevels.Add(peeDom.Level, peeDom);
                }

                if (lastStructure != null && peeDom.Level > lastStructure.Level)
                {
                    peeDom.Level = lastStructure.Level + 1; // Чтобы различие в уровне было только на 1, избежать случайных скачков в уровнях.
                }


                if (lastStructure != null && peeDom.Level > lastStructure.Level)
                {
                    foreach (double countByLevel in lastStructure.CountByLevels)
                    {
                        double sum = countByLevel + count;
                        peeDom.CountByLevels.Add(sum);
                    }

                    for (int i = 0; i < peeDom.Level; i++)
                    {
                        closestStructuresByLevels[i].CountByLevelsWithChildren += peeDom.CountByLevels.First(); // В родителей докидываем численность детей.
                    }
                }
                else if (lastStructure != null && peeDom.Level == lastStructure.Level)
                {
                    int indexer = 0;
                    foreach (double countByLevel in lastStructure.CountByLevels)
                    {
                        if (indexer > 0)
                        {
                            peeDom.CountByLevels.Add(countByLevel + count);
                        }

                        indexer++;
                    }
                    //peeDom.CountByLevelsWithChildren += closestStructuresByLevels[peeDom.Level].CountByLevelsWithChildren;
                    for (int i = 0; i < peeDom.Level; i++)
                    {
                        closestStructuresByLevels[i].CountByLevelsWithChildren += peeDom.CountByLevels.First(); // В родителей докидываем численность детей.
                    }
                    closestStructuresByLevels[peeDom.Level] = peeDom;

                }
                else if (lastStructure != null && peeDom.Level < lastStructure.Level)
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
                        foreach (double countByLevel in closestStructuresByLevels[peeDom.Level].CountByLevels.ToList())
                        {
                            if (indexer > 0)
                            {
                                //peeDom.CountByLevels.Add(countByLevel + positions.Count() + closestStructuresByLevels[peeDom.Level].CountByLevelsWithChildren);
                                double sum = countByLevel + count + closestStructuresByLevels[peeDom.Level].CountByLevelsWithChildren;
                                peeDom.CountByLevels.Add(sum);
                            }

                            indexer++;
                        }
                    }
                    for (int i = 0; i < peeDom.Level; i++)
                    {
                        closestStructuresByLevels[i].CountByLevelsWithChildren += peeDom.CountByLevels.First(); // В родителей докидываем численность детей.
                    }

                    closestStructuresByLevels[peeDom.Level] = peeDom;

                }

                if (lastStructure != null && peeDom.Level < lastStructure.Level)
                {
                    lastStructure.LevelDifference = lastStructure.Level - peeDom.Level;
                }
            }
        }


    }
}