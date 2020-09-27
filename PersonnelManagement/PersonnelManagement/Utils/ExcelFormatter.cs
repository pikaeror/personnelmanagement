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
using PersonnelManagement.Services;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;

namespace PersonnelManagement.Utils
{
    public static class ExcelFormatter
    {

        public static void CreateStylesheet(SpreadsheetDocument document)
        {
            WorkbookStylesPart wbsp = document.WorkbookPart.AddNewPart<WorkbookStylesPart>();
            // add styles to sheet
            wbsp.Stylesheet = CreateStylesheet();
            wbsp.Stylesheet.Save();
        }

        /**
         * Borderfat 0 - no, 1 - slim, 2 - medium, 3 - fat
         */
        public static void Format(SpreadsheetDocument document, Cell c, bool center = false, bool bold = false, bool border = false, int bordertop = 0, int borderright = 0, int borderbottom = 0, int borderleft = 0)
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
                borderId = InsertBorder(document.WorkbookPart, GenerateBorder(bordertop, borderright, borderbottom, borderleft));
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

        public static void AddCellFormatCenter(CellFormats cf, Fonts fs, bool center = false, bool bold = false)
        {

        }



        private static Cell CreateTextCell(string header, UInt32 index,
            string text)
        {
            var cell = new Cell
            {
                DataType = CellValues.InlineString,
                CellReference = header + index
            };

            var istring = new InlineString();
            var t = new Text { Text = text };
            istring.AppendChild(t);

            cell.AppendChild(istring);
            return cell;
        }

        private static string ColumnLetter(int intCol)
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

        public static void AddBold(SpreadsheetDocument document, Cell c)
        {
            Fonts fs = AddFont(document.WorkbookPart.WorkbookStylesPart.Stylesheet.Fonts);
            AddCellFormat(document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats, document.WorkbookPart.WorkbookStylesPart.Stylesheet.Fonts);
            c.StyleIndex = (UInt32)(document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats.Elements<CellFormat>().Count() - 1);
        }

        public static void AddCellFormat(CellFormats cf, Fonts fs)
        {
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = 0, FontId = (UInt32)(fs.Elements<Font>().Count() - 1), FillId = 0, BorderId = 0, FormatId = 0, ApplyFill = true };
            cf.Append(cellFormat2);
        }



        public static Fonts AddFont(Fonts fs)
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



        public static uint InsertBorder(WorkbookPart workbookPart, Border border)
        {
            Borders borders = workbookPart.WorkbookStylesPart.Stylesheet.Elements<Borders>().First();
            borders.Append(border);
            return (uint)borders.Count++;
        }

        public static string GetName(int x, int y)
        {
            return ColumnLetter(x) + y.ToString();
        }

        public static Border GenerateBorder(int bordertop, int borderright, int borderbottom, int borderleft)
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
    }
}
