using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Image = Xceed.Document.NET.Image;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class ElectronicBookReportController : Controller
    {
        public static IWebHostEnvironment _environment;


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private readonly ApplicationDbContext _context;

        public ElectronicBookReportController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost("printReport")]
        public IActionResult PrintReport([FromBody] ExportReportViewModel model)
        {
            var electronicBook = _context.ElectronicBooks
            .Include(x => x.User)
            .Include(x => x.ElectronicBookFiles)
            .Where(x => x.Id == model.electronicBookId)
            .FirstOrDefault();

            System.Console.WriteLine("in printtttt");

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "สมุดตรวจราชการอิเล็กทรอนิกส์" + DateTime.Now.ToString("dd MM yyyy") + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            using (DocX document = DocX.Create(createfile))
            {
                Image image = document.AddImage(myImageFullPath);
                Picture picture = image.CreatePicture(85, 85);
                var logo = document.InsertParagraph();
                logo.AppendPicture(picture).Alignment = Alignment.center;

                System.Console.WriteLine("5");

                // Add a title

                var reportType = document.InsertParagraph("สมุดตรวจราชการอิเล็กทรอนิกส์");
                reportType.FontSize(16d);
                reportType.SpacingBefore(15d);
                reportType.SpacingAfter(5d);
                reportType.Bold();
                reportType.Alignment = Alignment.center;

                System.Console.WriteLine("6");

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                var testDate = electronicBook.StartDate.Value.ToString("dddd dd MMMM yyyy");
                // Insert a title paragraph.
                var title = document.InsertParagraph("วันที่ตรวจราชการ: " + testDate);
                title.Alignment = Alignment.center;
                title.SpacingAfter(25d);
                title.FontSize(16d);
                title.Bold();

                System.Console.WriteLine("7");

                //var year = document.InsertParagraph("ตรวจ ณ สถานที่: ");
                //year.Alignment = Alignment.center;
                //year.SpacingAfter(10d);
                //year.FontSize(16d);
                //System.Console.WriteLine("8");


                var subjectTitle = document.InsertParagraph("ประเด็นการตรวจติดตาม");
                subjectTitle.Alignment = Alignment.left;
                //subjectTitle.SpacingAfter(10d);
                subjectTitle.FontSize(16d);
                subjectTitle.Bold();
                System.Console.WriteLine("8");

                var subject = document.InsertParagraph("");

                int s = 0;
                for (var i = 0; i < model.subjectData.Length; i++)
                {
                    s += 1;
                    subject.FontSize(14d).Append(s.ToString()).Append(") ").Append(model.subjectData[i] + "\n");
                }

                var detailTitle = document.InsertParagraph("ผลการตรวจ");
                detailTitle.SpacingBefore(10d);
                detailTitle.FontSize(16d);
                detailTitle.Bold();
                var detail = document.InsertParagraph(electronicBook.Detail);
                detail.SpacingBefore(5d);
                detail.FontSize(14d);
                // detail.UnderlineColor(Color.Black);
                // detail.UnderlineStyle(UnderlineStyle.dotted);

                var suggestionTitle = document.InsertParagraph("ปัญหาและอุปสรรค");
                suggestionTitle.SpacingBefore(15d);
                suggestionTitle.FontSize(16d);
                suggestionTitle.Bold();
                var suggestion = document.InsertParagraph(electronicBook.Problem);
                suggestion.SpacingBefore(5d);
                suggestion.FontSize(14d);
                // suggestion.UnderlineColor(Color.Black);
                // suggestion.UnderlineStyle(UnderlineStyle.dotted);

                var commandTitle = document.InsertParagraph("ข้อเสนอแนะ");
                commandTitle.SpacingBefore(15d);
                commandTitle.FontSize(16d);
                commandTitle.Bold();
                var command = document.InsertParagraph(electronicBook.Suggestion);
                command.SpacingBefore(5d);
                command.FontSize(14d);
                // command.UnderlineColor(Color.Black);
                // command.UnderlineStyle(UnderlineStyle.dotted);
                //command.InsertPageBreakAfterSelf();
                System.Console.WriteLine("11");


                //System.Console.WriteLine("9");

                //var region = document.InsertParagraph("เขตตรวจราชการที่: " + model.reportData2[i].region + "(จังหวัด: " + model.reportData2[i].province + ")");
                //region.Alignment = Alignment.center;
                //region.SpacingAfter(30d);
                //region.FontSize(16d);

                //var statusReport = document.InsertParagraph("สถานะของรายงาน: " + exportData.Status);
                //statusReport.FontSize(16d);
                //statusReport.Alignment = Alignment.right;

                //var monitorTopic = document.InsertParagraph("หัวข้อการตรวจติดตาม: " + exportData.MonitoringTopics);
                //monitorTopic.SpacingBefore(15d);
                //monitorTopic.FontSize(16d);
                //monitorTopic.Bold();

                //System.Console.WriteLine("99");
                var inspectorTitle = document.InsertParagraph("คำแนะนำผู้ตรวจราชการ");
                inspectorTitle.SpacingBefore(30d);
                inspectorTitle.SpacingAfter(5d);
                inspectorTitle.FontSize(16d);
                inspectorTitle.Bold();

                int dataCount = 0;
                dataCount = model.printReport.Count();
                dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 30f, 250f, 70f, 120f, };
                var t = document.InsertTable(dataCount, columnWidths.Length);

                //System.Console.WriteLine("8");

                //// Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;
                t.Alignment = Alignment.center;

                var row = t.Rows.First();
                //System.Console.WriteLine("9");

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row.Cells[0].Paragraphs.First().Append("ลำดับ").Alignment = Alignment.center;
                row.Cells[1].Paragraphs.First().Append("คำแนะนำหรือสั่งการของผู้ตรวจ").Alignment = Alignment.center;
                row.Cells[2].Paragraphs.First().Append("ความเห็นผู้ตรวจ").Alignment = Alignment.center;
                row.Cells[3].Paragraphs.First().Append("ลายมือชื่อผู้ตรวจ").Alignment = Alignment.center;

                System.Console.WriteLine("10");

                //}
                // Add rows in the table.
                int j = 0;
                for (int k = 0; k < model.printReport.Length; k++)
                {
                    j += 1;
                    System.Console.WriteLine("10.1");

                    System.Console.WriteLine("9.1: " + model.printReport[k].inspectorDescription);
                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString()).Alignment = Alignment.center;
                    t.Rows[j].Cells[1].Paragraphs[0].Append(model.printReport[k].inspectorDescription);
                    t.Rows[j].Cells[2].Paragraphs[0].Append(model.printReport[k].approve);
                    if (model.printReport[k].inspectorSign != null)
                    {
                        var myImageFullPath2 = filePath + model.printReport[k].inspectorSign;
                        Image image2 = document.AddImage(myImageFullPath2);
                        System.Console.WriteLine("JJJJJ: ");
                        Picture picture2 = image2.CreatePicture(30, 30);
                        t.Rows[j].Cells[3].Paragraphs[0].AppendPicture(picture2).SpacingBefore(3d).Append("\n" + model.printReport[k].inspectorName).Alignment = Alignment.center;
                    }
                    else
                    {
                        t.Rows[j].Cells[3].Paragraphs[0].Append(model.printReport[k].inspectorName).Alignment = Alignment.center;
                    }
                    System.Console.WriteLine("10");
                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                // document.InsertSectionPageBreak();

                System.Console.WriteLine("IN DEPARTMENT");

                var departmentTitle = document.InsertParagraph("การดำเนินการของหน่วยรับตรวจ");
                departmentTitle.SpacingBefore(30d);
                departmentTitle.SpacingAfter(5d);
                departmentTitle.FontSize(16d);
                departmentTitle.Bold();

                System.Console.WriteLine("IN DEPARTMENT2");

                int dataCount2 = 0;
                dataCount2 = model.printReport2.Count();
                dataCount2 += 1;
                System.Console.WriteLine("Data Count department: " + dataCount2);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths2 = new float[] { 30f, 250f, 80f, 120f, };
                var t2 = document.InsertTable(dataCount2, columnWidths2.Length);

                //System.Console.WriteLine("8");

                //// Set the table's column width and background 
                t2.SetWidths(columnWidths2);
                t2.AutoFit = AutoFit.Contents;
                t2.Alignment = Alignment.center;

                var row2 = t2.Rows.First();
                //System.Console.WriteLine("9");

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row2.Cells[0].Paragraphs.First().Append("ลำดับ").Alignment = Alignment.center;
                row2.Cells[1].Paragraphs.First().Append("การดำเนินการของหน่วยรับตรวจ").Alignment = Alignment.center;
                row2.Cells[2].Paragraphs.First().Append("หน่วยรับตรวจ").Alignment = Alignment.center;
                row2.Cells[3].Paragraphs.First().Append("ลายมือชื่อผู้รับตรวจ").Alignment = Alignment.center;

                System.Console.WriteLine("10");
                //}
                // Add rows in the table.
                int j2 = 0;
                for (int k = 0; k < model.printReport2.Length; k++)
                {
                    j2 += 1;
                    t2.Rows[j2].Cells[0].Paragraphs[0].Append(j2.ToString()).Alignment = Alignment.center;
                    t2.Rows[j2].Cells[1].Paragraphs[0].Append(model.printReport2[k].departmentDescription);
                    t2.Rows[j2].Cells[2].Paragraphs[0].Append(model.printReport2[k].department);
                    if (model.printReport2[k].departmentSign != null)
                    {
                        var myImageFullPath2 = filePath + model.printReport2[k].departmentSign;
                        Image image2 = document.AddImage(myImageFullPath2);
                        System.Console.WriteLine("JJJJJ: ");
                        Picture picture2 = image2.CreatePicture(30, 30);
                        t2.Rows[j2].Cells[3].Paragraphs[0].AppendPicture(picture2).SpacingBefore(3d).Append("\n" + model.printReport2[k].departmentName).Alignment = Alignment.center;
                    }
                    else
                    {
                        t2.Rows[j2].Cells[3].Paragraphs[0].Append(model.printReport2[k].departmentName);
                    }

                    System.Console.WriteLine("10");
                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder2 = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);


                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }

            return Ok(new { data = filename });
        }
    }
}
