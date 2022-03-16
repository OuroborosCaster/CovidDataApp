using System;
using System.IO;
using System.Reflection;
using MSWord = Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using MSExcel=Microsoft.Office.Interop.Excel;

namespace CovidDataApp
{
    class WriteInFile
    {
        public static void WriteContentInTxt(String content)
        {
            try
            {
                string fileName = DateTime.Now.ToString("yyyy年M月d日hh时m分s秒");
                using (StreamWriter sw = new StreamWriter($"{fileName}.txt"))
                {
                    sw.WriteLine(content);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void WriteContentInDoc(string header, string[,] tb)
        {
            object path;
            //string docContent=content;
            MSWord.Application wordApp; //Word应用程序变量 
            MSWord.Document wordDoc;    //Word文档变量

            string fileName = DateTime.Now.ToString("yyyy年M月d日hh时m分s秒");
            path = Environment.CurrentDirectory + @"\" + fileName + ".docx";

            wordApp = new MSWord.ApplicationClass();
            wordApp.Visible = false;//设置是否前台运行

            if (File.Exists((string)path))
            {
                File.Delete((string)path);
            }

            //由于使用的是COM库，因此有许多变量需要用Missing.Value代替
            Object Nothing = Missing.Value;

            wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);

            #region 页面设置

            wordDoc.PageSetup.PaperSize = MSWord.WdPaperSize.wdPaperA4;//设置纸张样式为A4纸
            wordDoc.PageSetup.Orientation = MSWord.WdOrientation.wdOrientLandscape;//排列方式为垂直方向
            wordDoc.PageSetup.TopMargin = 57.0f;
            wordDoc.PageSetup.BottomMargin = 57.0f;
            wordDoc.PageSetup.LeftMargin = 57.0f;
            wordDoc.PageSetup.RightMargin = 57.0f;
            wordDoc.PageSetup.HeaderDistance = 30.0f;//页眉位置            

            #endregion

            #region 页码设置并添加页码

            //为当前页添加页码
            MSWord.PageNumbers pns = wordApp.Selection.Sections[1].Headers[MSWord.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers;//获取当前页的号码
            pns.NumberStyle = MSWord.WdPageNumberStyle.wdPageNumberStyleNumberInDash;//设置页码的风格，是Dash形还是圆形的
            pns.HeadingLevelForChapter = 0;
            //pns.IncludeChapterNumber = false;
            pns.RestartNumberingAtSection = false;
            pns.StartingNumber = 1; //开始页页码
            object pagenmbetal = MSWord.WdPageNumberAlignment.wdAlignPageNumberRight;//将号码设置在右边
            object first = true;
            wordApp.Selection.Sections[1].Footers[MSWord.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers.Add(ref pagenmbetal, ref first);

            #endregion

            #region 行间距与缩进

            wordApp.Selection.ParagraphFormat.LineSpacing = 16f;//设置文档的行间距
            wordApp.Selection.ParagraphFormat.FirstLineIndent = 30;//首行缩进的长度            

            #endregion

            #region 写入内容
            object unit = MSWord.WdUnits.wdStory;

            //写入抬头            
            wordApp.Selection.EndKey(ref unit, ref Nothing);//将光标移到文本末尾
            wordApp.Selection.ParagraphFormat.FirstLineIndent = 0;//取消首行缩进的长度            
            wordDoc.Paragraphs.Last.Range.Font.Name = "黑体";
            wordDoc.Paragraphs.Last.Range.Text = header;

            //添加表格、填充数据、设置表格行列宽高

            wordDoc.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
            wordApp.Selection.EndKey(ref unit, ref Nothing); //将光标移动到文档末尾
            wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//居中对齐
            object WdLine2 = MSWord.WdUnits.wdLine;//换一行;  
            wordApp.Selection.MoveDown(ref WdLine2, 6, ref Nothing);//向下跨15行输入表格，这样表格就在文字下方了，不过这是非主流的方法

            //设置表格的行数和列数
            int table1Row = 63;
            int table1Column = 13;

            //定义一个Word中的表格对象
            MSWord.Table table1 = wordDoc.Tables.Add(wordApp.Selection.Range, table1Row, table1Column, ref Nothing, ref Nothing);

            //默认创建的表格没有边框，这里修改其属性，使得创建的表格带有边框 
            table1.Borders.Enable = 1;//这个值可以设置得很大，例如5、13等等

            //表格的索引是从1开始的。
            wordDoc.Tables[1].Cell(1, 1).Range.Text = "列\n行";
            for (int i = 1; i <= table1Row; i++)
            {
                for (int j = 1; j <= table1Column; j++)
                {
                    table1.Cell(i, j).Range.Text = tb[i - 1, j - 1];

                }
            }


            //设置table样式
            table1.Rows.HeightRule = MSWord.WdRowHeightRule.wdRowHeightAtLeast;//高度规则是：行高有最低值下限？
            table1.Rows.Height = wordApp.CentimetersToPoints(float.Parse("0.8"));// 

            table1.Range.Font.Size = 10.5F;
            table1.Range.Font.Bold = 0;

            table1.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//表格文本居中
            table1.Range.Cells.VerticalAlignment = MSWord.WdCellVerticalAlignment.wdCellAlignVerticalBottom;//文本垂直贴到底部
                                                                                                            //设置table边框样式
            table1.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleDouble;//表格外框是双线
            table1.Borders.InsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;//表格内框是单线

            table1.Rows[1].Range.Font.Bold = 1;//加粗
            table1.Rows[1].Range.Font.Size = 12F;
            table1.Cell(1, 1).Range.Font.Size = 10.5F;
            wordApp.Selection.Cells.Height = 30;//所有单元格的高度


            #endregion

            wordApp.Selection.EndKey(ref unit, ref Nothing); //将光标移动到文档末尾


            object format = MSWord.WdSaveFormat.wdFormatDocumentDefault;

            wordDoc.SaveAs(ref path, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            //关闭wordDoc文档对象

            //看是不是要打印
            //wordDoc.PrintOut();



            wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
            //关闭wordApp组件对象
            wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            Console.WriteLine(path + " 创建完毕！");


        }

        public static void WriteContentInExcel(string header, string[,] tb)
        {
            Object Nothing = Missing.Value;

            MSExcel.Application excelApp = new MSExcel.Application();//初始化应用
            MSExcel.Workbooks books=excelApp.Workbooks;
            MSExcel.Workbook book = books.Add(Nothing);//初始化workbook

            object path;
            string fileName = DateTime.Now.ToString("yyyy年M月d日hh时m分s秒");
            path = Environment.CurrentDirectory + @"/" + fileName + ".xlsx";

            
            excelApp.Visible = false;

            if (File.Exists((string)path))
            {
                File.Delete((string)path);
            }

            MSExcel.Sheets sheets = book.Sheets;
            MSExcel.Worksheet sheet=(MSExcel.Worksheet)sheets.get_Item(1);//初始化worksheet

            sheet.Cells.NumberFormatLocal = "@";//设置单元格格式为文本
            sheet.Cells.EntireColumn.AutoFit();

            for (int i = 1; i <= tb.GetLength(0); i++)
            {
                for(int j = 1; j <= tb.GetLength(1); j++)
                {
                    sheet.Cells[i, j] = tb[i-1, j-1];
                }
            }

            book.SaveAs(path, Nothing, Nothing, Nothing, Nothing, Nothing, MSExcel.XlSaveAsAccessMode.xlNoChange, Nothing, Nothing, Nothing, Nothing, Nothing);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            Console.WriteLine(path + " 创建完毕！");
        }
    }
}