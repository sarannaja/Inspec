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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class ExportController : Controller
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

        public ExportController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost("report")]
        public object Post([FromBody] ExportReportViewModel model)
        {
            List<object> termsList = new List<object>();
            ArrayList myAL = new ArrayList();

            //var ebook = _context.ElectronicBookGroups
            //    .Include(x => x.CentralPolicyProvince)
            //    .ThenInclude(x => x.Province)
            //    .Include(x => x.CentralPolicyProvince)
            //    .ThenInclude(x => x.CentralPolicy)
            //    .ThenInclude(x => x.CentralPolicyUser)
            //    .Include(x => x.ElectronicBook)
            //    .Where(x => x.ElectronicBook.CreatedBy == model.userId && x.ElectronicBook.ElectronicBookFiles.Any(x => x.Type == "SignatureProvince File"))
            //    .ToList();

            //foreach (var ebookData in ebook)
            //{
            //    var centralpolicydata = _context.CentralPolicies
            //  .Include(m => m.CentralPolicyProvinces)
            //  .ThenInclude(x => x.Province)
            //  .Include(m => m.CentralPolicyDates)
            //  .Include(m => m.CentralPolicyFiles)
            //  .Include(m => m.CentralPolicyProvinces)
            //  .ThenInclude(x => x.SubjectCentralPolicyProvinces)
            //  .Where(m => m.Id == ebookData.CentralPolicyProvince.CentralPolicyId)

            //  .Select(x => x.CentralPolicyProvinces)
            //  .FirstOrDefault();

            //    termsList.Add(centralpolicydata);
            //}
            //return Ok(termsList);

            var centralPolicyProvinceData = _context.CentralPolicyProvinces
                .Where(x => x.Id == model.centralPolicyProvinceId)
                .FirstOrDefault();

            var centralPolicyData = _context.CentralPolicies
                .Where(x => x.Id == centralPolicyProvinceData.CentralPolicyId)
                .FirstOrDefault();

            var subjectcentralpolicyprovincedata = _context.SubjectCentralPolicyProvinces
                .Include(x => x.CentralPolicyProvince.CentralPolicy)
                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubquestionChoiceCentralPolicyProvinces)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubjectCentralPolicyProvinceUserGroups)
                .ThenInclude(m => m.User)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
                .ThenInclude(m => m.ProvincialDepartment)
                // .Include(x => x.ElectronicBookSuggestGroups)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(x => x.AnswerSubquestions)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(x => x.AnswerSubquestionOutsiders)

                .Include(x => x.SuggestionSubjects)

                .Where(m => m.Type == "NoMaster")
                .Where(m => m.CentralPolicyProvinceId == model.centralPolicyProvinceId)
                .ToList();


            var dataTest = _context.SubjectCentralPolicyProvinces
                .Where(m => m.CentralPolicyProvinceId == model.centralPolicyProvinceId)
                .GroupBy(g => new { centralPolicyName = g.CentralPolicyProvince.CentralPolicy.Title, subject = g.Name, subjectId = g.Id })
                .Select(g => new
                {
                    g.Key.centralPolicyName,
                    g.Key.subject,
                    g.Key.subjectId
                })
                 .ToList();

            //if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            //{
            //    Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            //}
            //var filePath = _environment.WebRootPath + "/Uploads/";
            //var filename = "DOC" + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + ".docx";
            //var createfile = filePath + filename;
            //var myImageFullPath = filePath + "logo01.png";


            //System.Console.WriteLine("in create");
            //using (DocX document = DocX.Create(createfile))
            //{
            //    Image image = document.AddImage(myImageFullPath);
            //    Picture picture = image.CreatePicture(90, 90);
            //    var logo = document.InsertParagraph();
            //    logo.AppendPicture(picture).Alignment = Alignment.center;

            //    // Add a title
            //    document.InsertParagraph("รายงานผลการตรวจราชการ (แบบบูรณาการ/ม. 34/อื่น ๆ) : รายเขต").FontSize(15d).SpacingAfter(50d).Alignment = Alignment.center;

            //    // Insert a title paragraph.
            //    var title = document.InsertParagraph("ประเด็น/เรื่อง " + centralPolicyData.Title);
            //    title.Alignment = Alignment.center;
            //    title.SpacingAfter(40d);

            //    // Add a table in a document of 1 row and 3 columns.
            //    var columnWidths = new float[] { 200f, 200f, 200f, 200f, 200f, 200f, 200f };
            //    var t = document.InsertTable(1, columnWidths.Length);

            //    // Set the table's column width and background 
            //    t.SetWidths(columnWidths);
            //    t.AutoFit = AutoFit.Contents;

            //    var row = t.Rows.First();

            //    // Fill in the columns of the first row in the table.
            //    //for (int i = 0; i < row.Cells.Count; ++i)
            //    //{
            //    row.Cells[0].Paragraphs.First().Append("ประเด็นการตรวจติดตาม");
            //    row.Cells[1].Paragraphs.First().Append("ข้อค้นพบ/ประเด็นปัญหา/ผลการตรวจ");
            //    row.Cells[2].Paragraphs.First().Append("หน่วยรับตรวจ/หน่วยงานที่รับผิดชอบ");
            //    row.Cells[3].Paragraphs.First().Append("ข้อเสนอแนะของผู้ตรวจราชการ");
            //    row.Cells[4].Paragraphs.First().Append("รายงานผลการดำเนินการของหน่วยรับตรวจ");
            //    row.Cells[5].Paragraphs.First().Append("เอกสารแนบ (ไฟล์)");
            //    row.Cells[6].Paragraphs.First().Append("ความเห็นของ ที่ปรึกษา ผต.ภาคประชาชน");

            //    //}
            //    // Add rows in the table.
            //    for (int i = 0; i < dataTest.Count; i++)
            //    {

            //        var newRow = t.InsertRow();
            //        //t.Rows[i].Paragraphs.First().Append(subjectcentralpolicyprovincedata[i].Name);


            //        t.Rows[i + 1].Cells[0].Paragraphs.First().Append(dataTest[i].centralPolicyName);
            //        t.Rows[i + 1].Cells[1].Paragraphs.First().Append(dataTest[i].subject);
            //        t.Rows[i + 1].Cells[2].Paragraphs.First().Append("ja ja ja");
            //        t.Rows[i + 1].Cells[3].Paragraphs.First().Append("Eiei");
            //        t.Rows[i + 1].Cells[4].Paragraphs.First().Append("Test Ja");
            //        t.Rows[i + 1].Cells[5].Paragraphs.First().Append("Naja");
            //        t.Rows[i + 1].Cells[6].Paragraphs.First().Append("asdfasdfsadf");
            //        // Fill in the columns of the new rows.
            //        //for (int j = 0; j < subjectcentralpolicyprovincedata.Count; ++j)
            //        //{
            //        //    var newCell = newRow.Cells[j];
            //        //    newCell.Paragraphs.First().Append("test" + i);
            //        //    // Remove the left margin of the new cells.
            //        //    newCell.MarginLeft = 0;
            //        //}
            //    }

            //    // Set a blank border for the table's top/bottom borders.
            //    var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
            //    //t.SetBorder(TableBorderType.Bottom, blankBorder);
            //    //t.SetBorder(TableBorderType.Top, blankBorder);

            //    document.Save();
            //    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            //}

            return Ok(new { subjectcentralpolicyprovincedata });
        }

        [HttpPost("createReport")]
        public IActionResult CreateReport([FromBody] ExportReportViewModel model)
        {
            System.Console.WriteLine("1");
            var centralPolicyProvinceData = _context.CentralPolicyProvinces
               .Where(x => x.Id == model.centralPolicyProvinceId)
               .FirstOrDefault();

            var centralPolicyData = _context.CentralPolicies
                .Where(x => x.Id == centralPolicyProvinceData.CentralPolicyId)
                .Include(x => x.FiscalYear)
                .FirstOrDefault();
            System.Console.WriteLine("2");

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "DOC" + centralPolicyData.Title + DateTime.Now.ToString("dd MM yyyy") + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            using (DocX document = DocX.Create(createfile))
            {
                System.Console.WriteLine("4");
                Image image = document.AddImage(myImageFullPath);
                Picture picture = image.CreatePicture(85, 85);
                var logo = document.InsertParagraph();
                logo.AppendPicture(picture).Alignment = Alignment.center;


                System.Console.WriteLine("5");

                // Add a title
                document.InsertParagraph("รายงานผลการตรวจราชการ (แบบบูรณาการ/ม. 34/อื่น ๆ)").FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold()
                    .Alignment = Alignment.center;

                System.Console.WriteLine("6");
                // Insert a title paragraph.
                var title = document.InsertParagraph("ประเด็น/เรื่อง:  " + centralPolicyData.Title);
                title.Alignment = Alignment.center;
                title.SpacingAfter(15d);
                title.FontSize(16d);
                title.Bold();

                var year = document.InsertParagraph("รอบการตรวจราชการที่..............." + "ปีงบประมาณ พ.ศ. " + centralPolicyData.FiscalYear.Year);
                year.Alignment = Alignment.center;
                year.SpacingAfter(10d);
                year.FontSize(16d);

                var inspector = document.InsertParagraph("ของผู้ตรวจราชการสำนักนายกรัฐมนตรี/ผู้ตรวจราชการกระทรวง....................");
                inspector.Alignment = Alignment.center;
                inspector.SpacingAfter(10d);
                inspector.FontSize(16d);

                var region = document.InsertParagraph("เขตตรวจราชการที่..........(จังหวัด...............)");
                region.Alignment = Alignment.center;
                region.SpacingAfter(30d);
                region.FontSize(16d);


                System.Console.WriteLine("7");

                int dataCount = 0;
                dataCount = model.reportData.Length;
                dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f, 300f };
                var t = document.InsertTable(dataCount, columnWidths.Length);

                System.Console.WriteLine("8");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row.Cells[0].Paragraphs.First().Append("ประเด็นการตรวจติดตาม");
                row.Cells[1].Paragraphs.First().Append("ข้อค้นพบ/ผลการตรวจ");
                row.Cells[2].Paragraphs.First().Append("ประเด็นปัญหา");
                row.Cells[3].Paragraphs.First().Append("ข้อเสนอแนะของผู้ตรวจราชการ");
                row.Cells[4].Paragraphs.First().Append("หน่วยรับตรวจ/หน่วยงานที่รับผิดชอบ");
                row.Cells[5].Paragraphs.First().Append("ความเห็นของ ที่ปรึกษา ผต.ภาคประชาชน");

                System.Console.WriteLine("9999: " + model.reportData.Count());
                System.Console.WriteLine("9: " + model.reportData.Length);

                //}
                // Add rows in the table.
                int j = 0;
                for (int i = 0; i < model.reportData.Length; i++)
                {
                    j += 1;
                    //System.Console.WriteLine(i+=1);

                    System.Console.WriteLine("JJJJJ: " + j);
                    System.Console.WriteLine("9.1: " + model.reportData[i].subject);
                    t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData[i].subject);
                    System.Console.WriteLine("9.2: " + model.reportData[i].detail);
                    t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData[i].detail);
                    System.Console.WriteLine("9.3: " + model.reportData[i].suggestion);
                    t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData[i].suggestion);
                    System.Console.WriteLine("9.4: " + model.reportData[i].problem);
                    t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData[i].problem);
                    System.Console.WriteLine("9.5: " + model.reportData[i].department);
                    t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData[i].department);
                    System.Console.WriteLine("9.6: " + model.reportData[i].opinionPeople);
                    t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData[i].opinionPeople);
                    System.Console.WriteLine("10");
                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                System.Console.WriteLine("11");
                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }
            return Ok(new { data = filename });
        }

        [HttpPost("createReport2")]
        public IActionResult CreateReport2([FromBody] ExportReportViewModel model)
        {
            var exportData = _context.ImportReports
                .Where(x => x.Id == model.reportId)
                .FirstOrDefault();

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "DOC" + exportData.MonitoringTopics + DateTime.Now.ToString("dd MM yyyy") + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");

            System.Console.WriteLine("test: " + model.reportData2[0].centralPolicy);

            //centralPolicyData.CentralPolicyEvent.CentralPolicy.Title
            if (exportData.ReportType == "รายเขต")
            {
                System.Console.WriteLine("in รายเขต");
                using (DocX document = DocX.Create(createfile))
                {
                    // document.DifferentOddAndEvenPages = true;
                    // document.Sections[i].DifferentFirstPage = true;

                    int l = 0;
                    for (int i = 0; i < model.reportData2.Length; i++)
                    {
                        l += 1;
                        System.Console.WriteLine("in loop: " + l);
                        System.Console.WriteLine("4");
                        Image image = document.AddImage(myImageFullPath);
                        Picture picture = image.CreatePicture(85, 85);
                        var logo = document.InsertParagraph();
                        logo.AppendPicture(picture).Alignment = Alignment.center;

                        System.Console.WriteLine("5");

                        // Add a title

                        var reportType = document.InsertParagraph("รายงานผลการตรวจราชการ (" + exportData.CentralPolicyType + ")" + " : " + exportData.ReportType);
                        reportType.FontSize(16d);
                        reportType.SpacingBefore(15d);
                        reportType.SpacingAfter(15d);
                        reportType.Bold();
                        reportType.Alignment = Alignment.center;

                        System.Console.WriteLine("6");
                        // Insert a title paragraph.
                        var title = document.InsertParagraph("ประเด็น/เรื่อง:  " + model.reportData2[i].centralPolicy);
                        title.Alignment = Alignment.center;
                        title.SpacingAfter(15d);
                        title.FontSize(16d);
                        title.Bold();

                        System.Console.WriteLine("7");

                        var year = document.InsertParagraph("รอบการตรวจราชการที่: " + exportData.InspectionRound + " ปีงบประมาณ: พ.ศ. " + model.reportData2[i].fiscalYear);
                        year.Alignment = Alignment.center;
                        year.SpacingAfter(10d);
                        year.FontSize(16d);
                        System.Console.WriteLine("8");

                        var inspector = document.InsertParagraph("ของผู้ตรวจราชการสำนักนายกรัฐมนตรี/ผู้ตรวจราชการกระทรวง: " + model.reportData2[i].department);
                        inspector.Alignment = Alignment.center;
                        inspector.SpacingAfter(10d);
                        inspector.FontSize(16d);

                        System.Console.WriteLine("9");

                        var region = document.InsertParagraph("เขตตรวจราชการที่: " + model.reportData2[i].region + "(จังหวัด: " + model.reportData2[i].province + ")");
                        region.Alignment = Alignment.center;
                        region.SpacingAfter(30d);
                        region.FontSize(16d);

                        var statusReport = document.InsertParagraph("สถานะของรายงาน: " + exportData.Status);
                        statusReport.FontSize(16d);
                        statusReport.Alignment = Alignment.right;

                        var monitorTopic = document.InsertParagraph("หัวข้อการตรวจติดตาม: " + exportData.MonitoringTopics);
                        monitorTopic.SpacingBefore(15d);
                        monitorTopic.FontSize(16d);
                        monitorTopic.Bold();

                        System.Console.WriteLine("99");

                        int dataCount = 0;
                        dataCount = model.reportData2[i].tableData.Count();
                        dataCount += 1;
                        System.Console.WriteLine("Data Count: " + dataCount);
                        // Add a table in a document of 1 row and 3 columns.
                        var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f, 300f };
                        var t = document.InsertTable(dataCount, columnWidths.Length);

                        System.Console.WriteLine("8");

                        // Set the table's column width and background 
                        t.SetWidths(columnWidths);
                        t.AutoFit = AutoFit.Contents;

                        var row = t.Rows.First();
                        System.Console.WriteLine("9");

                        // Fill in the columns of the first row in the table.
                        //for (int i = 0; i < row.Cells.Count; ++i)
                        //{
                        row.Cells[0].Paragraphs.First().Append("ประเด็นการตรวจติดตาม");
                        row.Cells[1].Paragraphs.First().Append("ข้อค้นพบ/ผลการตรวจ");
                        row.Cells[2].Paragraphs.First().Append("ประเด็นปัญหา");
                        row.Cells[3].Paragraphs.First().Append("ข้อเสนอแนะของผู้ตรวจราชการ");
                        row.Cells[4].Paragraphs.First().Append("หน่วยรับตรวจ/หน่วยงานที่รับผิดชอบ");
                        row.Cells[5].Paragraphs.First().Append("ความเห็นของ ที่ปรึกษา ผต.ภาคประชาชน");

                        System.Console.WriteLine("10");
                        //}
                        // Add rows in the table.
                        int j = 0;
                        for (int k = 0; k < model.reportData2[i].tableData.Length; k++)
                        {
                            j += 1;
                            //System.Console.WriteLine(i+=1);
                            System.Console.WriteLine("10.1");
                            System.Console.WriteLine("JJJJJ: " + j);
                            System.Console.WriteLine("9.1: " + model.reportData2[i].tableData[k].subject);
                            t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.2: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.3: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.4: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.5: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.6: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            System.Console.WriteLine("10");
                        }

                        // Set a blank border for the table's top/bottom borders.
                        var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                        //t.SetBorder(TableBorderType.Bottom, blankBorder);
                        //t.SetBorder(TableBorderType.Top, blankBorder);

                        // document.InsertSectionPageBreak();

                        var detailTitle = document.InsertParagraph("สรุปผลการตรวจราชการ");
                        detailTitle.SpacingBefore(15d);
                        detailTitle.FontSize(16d);
                        detailTitle.Bold();
                        var detail = document.InsertParagraph(exportData.DetailReport);
                        detail.SpacingBefore(5d);
                        detail.FontSize(15d);
                        // detail.UnderlineColor(Color.Black);
                        // detail.UnderlineStyle(UnderlineStyle.dotted);

                        var suggestionTitle = document.InsertParagraph("ข้อเสนอแนะเชิงนโยบายของผู้ตรวจราชการ");
                        suggestionTitle.SpacingBefore(15d);
                        suggestionTitle.FontSize(16d);
                        suggestionTitle.Bold();
                        var suggestion = document.InsertParagraph(exportData.Suggestion);
                        suggestion.SpacingBefore(5d);
                        suggestion.FontSize(15d);
                        // suggestion.UnderlineColor(Color.Black);
                        // suggestion.UnderlineStyle(UnderlineStyle.dotted);

                        var commandTitle = document.InsertParagraph("ข้อสั่งการของผู้บังคับบัญชา");
                        commandTitle.SpacingBefore(15d);
                        commandTitle.FontSize(16d);
                        commandTitle.Bold();
                        var command = document.InsertParagraph(exportData.Command);
                        command.SpacingBefore(5d);
                        command.FontSize(15d);
                        // command.UnderlineColor(Color.Black);
                        // command.UnderlineStyle(UnderlineStyle.dotted);
                        command.InsertPageBreakAfterSelf();
                        System.Console.WriteLine("11");
                    }

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }

            if (exportData.ReportType == "รายจังหวัด")
            {
                System.Console.WriteLine("in รายจังหวัด");
                using (DocX document = DocX.Create(createfile))
                {
                    // document.DifferentOddAndEvenPages = true;
                    // document.Sections[i].DifferentFirstPage = true;

                    int l = 0;
                    for (int i = 0; i < model.reportData2.Length; i++)
                    {
                        l += 1;
                        System.Console.WriteLine("in loop: " + l);
                        System.Console.WriteLine("4");
                        Image image = document.AddImage(myImageFullPath);
                        Picture picture = image.CreatePicture(85, 85);
                        var logo = document.InsertParagraph();
                        logo.AppendPicture(picture).Alignment = Alignment.center;

                        System.Console.WriteLine("5");

                        // Add a title

                        var reportType = document.InsertParagraph("รายงานผลการตรวจราชการ (" + exportData.CentralPolicyType + ")" + " : " + exportData.ReportType);
                        reportType.FontSize(16d);
                        reportType.SpacingBefore(15d);
                        reportType.SpacingAfter(15d);
                        reportType.Bold();
                        reportType.Alignment = Alignment.center;

                        System.Console.WriteLine("6");
                        // Insert a title paragraph.
                        // var title = document.InsertParagraph("ประเด็น/เรื่อง:  " + model.reportData2[i].centralPolicy);
                        // title.Alignment = Alignment.center;
                        // title.SpacingAfter(15d);
                        // title.FontSize(16d);
                        // title.Bold();

                        System.Console.WriteLine("7");

                        var year = document.InsertParagraph("รอบการตรวจราชการที่: " + exportData.InspectionRound + " ปีงบประมาณ: พ.ศ. " + model.reportData2[i].fiscalYear);
                        year.Alignment = Alignment.center;
                        year.SpacingAfter(10d);
                        year.FontSize(16d);
                        System.Console.WriteLine("8");

                        var inspector = document.InsertParagraph("ของผู้ตรวจราชการสำนักนายกรัฐมนตรี/ผู้ตรวจราชการกระทรวง: " + model.reportData2[i].department);
                        inspector.Alignment = Alignment.center;
                        inspector.SpacingAfter(10d);
                        inspector.FontSize(16d);

                        System.Console.WriteLine("9");

                        var region = document.InsertParagraph("เขตตรวจราชการที่: " + model.reportData2[i].region + "(จังหวัด: " + model.reportData2[i].province + ")");
                        region.Alignment = Alignment.center;
                        region.SpacingAfter(30d);
                        region.FontSize(16d);

                        var statusReport = document.InsertParagraph("สถานะของรายงาน: " + exportData.Status);
                        statusReport.FontSize(16d);
                        statusReport.Alignment = Alignment.right;

                        var monitorTopic = document.InsertParagraph("หัวข้อการตรวจติดตาม: " + exportData.MonitoringTopics);
                        monitorTopic.SpacingBefore(15d);
                        monitorTopic.FontSize(16d);
                        monitorTopic.Bold();

                        System.Console.WriteLine("99");

                        int dataCount = 0;
                        dataCount = model.reportData2[i].tableData.Count();
                        dataCount += 1;
                        System.Console.WriteLine("Data Count: " + dataCount);
                        // Add a table in a document of 1 row and 3 columns.
                        var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f, 300f };
                        var t = document.InsertTable(dataCount, columnWidths.Length);

                        System.Console.WriteLine("8");

                        // Set the table's column width and background 
                        t.SetWidths(columnWidths);
                        t.AutoFit = AutoFit.Contents;

                        var row = t.Rows.First();
                        System.Console.WriteLine("9");

                        // Fill in the columns of the first row in the table.
                        //for (int i = 0; i < row.Cells.Count; ++i)
                        //{
                        row.Cells[0].Paragraphs.First().Append("ประเด็นการตรวจติดตาม");
                        row.Cells[1].Paragraphs.First().Append("ข้อค้นพบ/ผลการตรวจ");
                        row.Cells[2].Paragraphs.First().Append("ประเด็นปัญหา");
                        row.Cells[3].Paragraphs.First().Append("ข้อเสนอแนะของผู้ตรวจราชการ");
                        row.Cells[4].Paragraphs.First().Append("หน่วยรับตรวจ/หน่วยงานที่รับผิดชอบ");
                        row.Cells[5].Paragraphs.First().Append("ความเห็นของ ที่ปรึกษา ผต.ภาคประชาชน");

                        System.Console.WriteLine("10");
                        //}
                        // Add rows in the table.
                        int j = 0;
                        for (int k = 0; k < model.reportData2[i].tableData.Length; k++)
                        {
                            j += 1;
                            //System.Console.WriteLine(i+=1);
                            System.Console.WriteLine("10.1");
                            System.Console.WriteLine("JJJJJ: " + j);
                            System.Console.WriteLine("9.1: " + model.reportData2[i].tableData[k].subject);
                            t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.2: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.3: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.4: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.5: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.6: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            System.Console.WriteLine("10");
                        }

                        // Set a blank border for the table's top/bottom borders.
                        var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                        //t.SetBorder(TableBorderType.Bottom, blankBorder);
                        //t.SetBorder(TableBorderType.Top, blankBorder);

                        // document.InsertSectionPageBreak();

                        var detailTitle = document.InsertParagraph("สรุปผลการตรวจราชการ");
                        detailTitle.SpacingBefore(15d);
                        detailTitle.FontSize(16d);
                        detailTitle.Bold();
                        var detail = document.InsertParagraph(exportData.DetailReport);
                        detail.SpacingBefore(5d);
                        detail.FontSize(15d);
                        // detail.UnderlineColor(Color.Black);
                        // detail.UnderlineStyle(UnderlineStyle.dotted);

                        var suggestionTitle = document.InsertParagraph("ข้อเสนอแนะเชิงนโยบายของผู้ตรวจราชการ");
                        suggestionTitle.SpacingBefore(15d);
                        suggestionTitle.FontSize(16d);
                        suggestionTitle.Bold();
                        var suggestion = document.InsertParagraph(exportData.Suggestion);
                        suggestion.SpacingBefore(5d);
                        suggestion.FontSize(15d);
                        // suggestion.UnderlineColor(Color.Black);
                        // suggestion.UnderlineStyle(UnderlineStyle.dotted);

                        var commandTitle = document.InsertParagraph("ข้อสั่งการของผู้บังคับบัญชา");
                        commandTitle.SpacingBefore(15d);
                        commandTitle.FontSize(16d);
                        commandTitle.Bold();
                        var command = document.InsertParagraph(exportData.Command);
                        command.SpacingBefore(5d);
                        command.FontSize(15d);
                        // command.UnderlineColor(Color.Black);
                        // command.UnderlineStyle(UnderlineStyle.dotted);
                        command.InsertPageBreakAfterSelf();
                        System.Console.WriteLine("11");
                    }

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }

            if (exportData.ReportType == "รายวัน")
            {
                System.Console.WriteLine("in รายวัน");
                using (DocX document = DocX.Create(createfile))
                {
                    // document.DifferentOddAndEvenPages = true;
                    // document.Sections[i].DifferentFirstPage = true;

                    int l = 0;
                    for (int i = 0; i < model.reportData2.Length; i++)
                    {
                        l += 1;
                        System.Console.WriteLine("in loop: " + l);
                        System.Console.WriteLine("4");
                        Image image = document.AddImage(myImageFullPath);
                        Picture picture = image.CreatePicture(85, 85);
                        var logo = document.InsertParagraph();
                        logo.AppendPicture(picture).Alignment = Alignment.center;

                        System.Console.WriteLine("5");

                        // Add a title

                        var reportType = document.InsertParagraph("รายงานผลการตรวจราชการ (" + exportData.CentralPolicyType + ")" + " : " + exportData.ReportType);
                        reportType.FontSize(16d);
                        reportType.SpacingBefore(15d);
                        reportType.SpacingAfter(15d);
                        reportType.Bold();
                        reportType.Alignment = Alignment.center;

                        System.Console.WriteLine("6");
                        // Insert a title paragraph.
                        // var title = document.InsertParagraph("ประเด็น/เรื่อง:  " + model.reportData2[i].centralPolicy);
                        // title.Alignment = Alignment.center;
                        // title.SpacingAfter(15d);
                        // title.FontSize(16d);
                        // title.Bold();

                        System.Console.WriteLine("7");

                        var year = document.InsertParagraph("วันที่ตรวจราชการ: ...............................");
                        year.Alignment = Alignment.center;
                        year.SpacingAfter(10d);
                        year.FontSize(16d);
                        System.Console.WriteLine("8");

                        var inspector = document.InsertParagraph("ผู้ตรวจราชการสำนักนายกรัฐมนตรี/ผู้ตรวจราชการกระทรวง: " + model.reportData2[i].department);
                        inspector.Alignment = Alignment.center;
                        inspector.SpacingAfter(10d);
                        inspector.FontSize(16d);

                        System.Console.WriteLine("9");

                        var region = document.InsertParagraph("เขตตรวจราชการที่: " + model.reportData2[i].region + "(จังหวัด: " + model.reportData2[i].province + ")");
                        region.Alignment = Alignment.center;
                        region.SpacingAfter(30d);
                        region.FontSize(16d);

                        var statusReport = document.InsertParagraph("สถานะของรายงาน: " + exportData.Status);
                        statusReport.FontSize(16d);
                        statusReport.Alignment = Alignment.right;

                        var monitorTopic = document.InsertParagraph("หัวข้อการตรวจติดตาม: " + exportData.MonitoringTopics);
                        monitorTopic.SpacingBefore(15d);
                        monitorTopic.FontSize(16d);
                        monitorTopic.Bold();

                        System.Console.WriteLine("99");

                        int dataCount = 0;
                        dataCount = model.reportData2[i].tableData.Count();
                        dataCount += 1;
                        System.Console.WriteLine("Data Count: " + dataCount);
                        // Add a table in a document of 1 row and 3 columns.
                        var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f, 300f };
                        var t = document.InsertTable(dataCount, columnWidths.Length);

                        System.Console.WriteLine("8");

                        // Set the table's column width and background 
                        t.SetWidths(columnWidths);
                        t.AutoFit = AutoFit.Contents;

                        var row = t.Rows.First();
                        System.Console.WriteLine("9");

                        // Fill in the columns of the first row in the table.
                        //for (int i = 0; i < row.Cells.Count; ++i)
                        //{
                        row.Cells[0].Paragraphs.First().Append("ประเด็นการตรวจติดตาม");
                        row.Cells[1].Paragraphs.First().Append("ข้อค้นพบ/ผลการตรวจ");
                        row.Cells[2].Paragraphs.First().Append("ประเด็นปัญหา");
                        row.Cells[3].Paragraphs.First().Append("ข้อเสนอแนะของผู้ตรวจราชการ");
                        row.Cells[4].Paragraphs.First().Append("หน่วยรับตรวจ/หน่วยงานที่รับผิดชอบ");
                        row.Cells[5].Paragraphs.First().Append("ความเห็นของ ที่ปรึกษา ผต.ภาคประชาชน");

                        System.Console.WriteLine("10");
                        //}
                        // Add rows in the table.
                        int j = 0;
                        for (int k = 0; k < model.reportData2[i].tableData.Length; k++)
                        {
                            j += 1;
                            //System.Console.WriteLine(i+=1);
                            System.Console.WriteLine("10.1");
                            System.Console.WriteLine("JJJJJ: " + j);
                            System.Console.WriteLine("9.1: " + model.reportData2[i].tableData[k].subject);
                            t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.2: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.3: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.4: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.5: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.6: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            System.Console.WriteLine("10");
                        }

                        // Set a blank border for the table's top/bottom borders.
                        var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                        //t.SetBorder(TableBorderType.Bottom, blankBorder);
                        //t.SetBorder(TableBorderType.Top, blankBorder);

                        // document.InsertSectionPageBreak();

                        var detailTitle = document.InsertParagraph("สรุปผลการตรวจราชการ");
                        detailTitle.SpacingBefore(15d);
                        detailTitle.FontSize(16d);
                        detailTitle.Bold();
                        var detail = document.InsertParagraph(exportData.DetailReport);
                        detail.SpacingBefore(5d);
                        detail.FontSize(15d);
                        // detail.UnderlineColor(Color.Black);
                        // detail.UnderlineStyle(UnderlineStyle.dotted);

                        var suggestionTitle = document.InsertParagraph("ข้อเสนอแนะเชิงนโยบายของผู้ตรวจราชการ");
                        suggestionTitle.SpacingBefore(15d);
                        suggestionTitle.FontSize(16d);
                        suggestionTitle.Bold();
                        var suggestion = document.InsertParagraph(exportData.Suggestion);
                        suggestion.SpacingBefore(5d);
                        suggestion.FontSize(15d);
                        // suggestion.UnderlineColor(Color.Black);
                        // suggestion.UnderlineStyle(UnderlineStyle.dotted);

                        var commandTitle = document.InsertParagraph("ข้อสั่งการของผู้บังคับบัญชา");
                        commandTitle.SpacingBefore(15d);
                        commandTitle.FontSize(16d);
                        commandTitle.Bold();
                        var command = document.InsertParagraph(exportData.Command);
                        command.SpacingBefore(5d);
                        command.FontSize(15d);
                        // command.UnderlineColor(Color.Black);
                        // command.UnderlineStyle(UnderlineStyle.dotted);
                        command.InsertPageBreakAfterSelf();
                        System.Console.WriteLine("11");
                    }

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }

            if (exportData.ReportType == "รายหน่วยงาน")
            {
                System.Console.WriteLine("in รายหน่วยงาน");
                using (DocX document = DocX.Create(createfile))
                {
                    // document.DifferentOddAndEvenPages = true;
                    // document.Sections[i].DifferentFirstPage = true;

                    int l = 0;
                    for (int i = 0; i < model.reportData2.Length; i++)
                    {
                        l += 1;
                        System.Console.WriteLine("in loop: " + l);
                        System.Console.WriteLine("4");
                        Image image = document.AddImage(myImageFullPath);
                        Picture picture = image.CreatePicture(85, 85);
                        var logo = document.InsertParagraph();
                        logo.AppendPicture(picture).Alignment = Alignment.center;

                        System.Console.WriteLine("5");

                        // Add a title

                        var reportType = document.InsertParagraph("รายงานผลการตรวจราชการ (" + exportData.CentralPolicyType + ")" + " : " + exportData.ReportType);
                        reportType.FontSize(16d);
                        reportType.SpacingBefore(15d);
                        reportType.SpacingAfter(15d);
                        reportType.Bold();
                        reportType.Alignment = Alignment.center;

                        System.Console.WriteLine("6");
                        // Insert a title paragraph.
                        // var title = document.InsertParagraph("ประเด็น/เรื่อง:  " + model.reportData2[i].centralPolicy);
                        // title.Alignment = Alignment.center;
                        // title.SpacingAfter(15d);
                        // title.FontSize(16d);
                        // title.Bold();

                        System.Console.WriteLine("7");

                        // var year = document.InsertParagraph("วันที่ตรวจราชการ: ...............................");
                        // year.Alignment = Alignment.center;
                        // year.SpacingAfter(10d);
                        // year.FontSize(16d);
                        // System.Console.WriteLine("8");

                        var inspector = document.InsertParagraph("ผู้ตรวจราชการสำนักนายกรัฐมนตรี/ผู้ตรวจราชการกระทรวง: " + model.reportData2[i].department);
                        inspector.Alignment = Alignment.center;
                        inspector.SpacingAfter(10d);
                        inspector.FontSize(16d);

                        System.Console.WriteLine("9");

                        var region = document.InsertParagraph("เขตตรวจราชการที่: " + model.reportData2[i].region + "(จังหวัด: " + model.reportData2[i].province + ")");
                        region.Alignment = Alignment.center;
                        region.SpacingAfter(10d);
                        region.FontSize(16d);

                        var year = document.InsertParagraph("รอบการตรวจราชการที่: " + exportData.InspectionRound + " ปีงบประมาณ: พ.ศ. " + model.reportData2[i].fiscalYear);
                        year.Alignment = Alignment.center;
                        year.SpacingAfter(30d);
                        year.FontSize(16d);

                        var statusReport = document.InsertParagraph("สถานะของรายงาน: " + exportData.Status);
                        statusReport.FontSize(16d);
                        statusReport.Alignment = Alignment.right;

                        var monitorTopic = document.InsertParagraph("หัวข้อการตรวจติดตาม: " + exportData.MonitoringTopics);
                        monitorTopic.SpacingBefore(15d);
                        monitorTopic.FontSize(16d);
                        monitorTopic.Bold();

                        System.Console.WriteLine("99");

                        int dataCount = 0;
                        dataCount = model.reportData2[i].tableData.Count();
                        dataCount += 1;
                        System.Console.WriteLine("Data Count: " + dataCount);
                        // Add a table in a document of 1 row and 3 columns.
                        var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f, 300f };
                        var t = document.InsertTable(dataCount, columnWidths.Length);

                        System.Console.WriteLine("8");

                        // Set the table's column width and background 
                        t.SetWidths(columnWidths);
                        t.AutoFit = AutoFit.Contents;

                        var row = t.Rows.First();
                        System.Console.WriteLine("9");

                        // Fill in the columns of the first row in the table.
                        //for (int i = 0; i < row.Cells.Count; ++i)
                        //{
                        row.Cells[0].Paragraphs.First().Append("ประเด็นการตรวจติดตาม");
                        row.Cells[1].Paragraphs.First().Append("ข้อค้นพบ/ผลการตรวจ");
                        row.Cells[2].Paragraphs.First().Append("ประเด็นปัญหา");
                        row.Cells[3].Paragraphs.First().Append("ข้อเสนอแนะของผู้ตรวจราชการ");
                        row.Cells[4].Paragraphs.First().Append("หน่วยรับตรวจ/หน่วยงานที่รับผิดชอบ");
                        row.Cells[5].Paragraphs.First().Append("ความเห็นของ ที่ปรึกษา ผต.ภาคประชาชน");

                        System.Console.WriteLine("10");
                        //}
                        // Add rows in the table.
                        int j = 0;
                        for (int k = 0; k < model.reportData2[i].tableData.Length; k++)
                        {
                            j += 1;
                            //System.Console.WriteLine(i+=1);
                            System.Console.WriteLine("10.1");
                            System.Console.WriteLine("JJJJJ: " + j);
                            System.Console.WriteLine("9.1: " + model.reportData2[i].tableData[k].subject);
                            t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.2: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.3: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.4: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.5: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.6: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            System.Console.WriteLine("10");
                        }

                        // Set a blank border for the table's top/bottom borders.
                        var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                        //t.SetBorder(TableBorderType.Bottom, blankBorder);
                        //t.SetBorder(TableBorderType.Top, blankBorder);

                        // document.InsertSectionPageBreak();

                        var detailTitle = document.InsertParagraph("สรุปผลการตรวจราชการ");
                        detailTitle.SpacingBefore(15d);
                        detailTitle.FontSize(16d);
                        detailTitle.Bold();
                        var detail = document.InsertParagraph(exportData.DetailReport);
                        detail.SpacingBefore(5d);
                        detail.FontSize(15d);
                        // detail.UnderlineColor(Color.Black);
                        // detail.UnderlineStyle(UnderlineStyle.dotted);

                        var suggestionTitle = document.InsertParagraph("ข้อเสนอแนะเชิงนโยบายของผู้ตรวจราชการ");
                        suggestionTitle.SpacingBefore(15d);
                        suggestionTitle.FontSize(16d);
                        suggestionTitle.Bold();
                        var suggestion = document.InsertParagraph(exportData.Suggestion);
                        suggestion.SpacingBefore(5d);
                        suggestion.FontSize(15d);
                        // suggestion.UnderlineColor(Color.Black);
                        // suggestion.UnderlineStyle(UnderlineStyle.dotted);

                        var commandTitle = document.InsertParagraph("ข้อสั่งการของผู้บังคับบัญชา");
                        commandTitle.SpacingBefore(15d);
                        commandTitle.FontSize(16d);
                        commandTitle.Bold();
                        var command = document.InsertParagraph(exportData.Command);
                        command.SpacingBefore(5d);
                        command.FontSize(15d);
                        // command.UnderlineColor(Color.Black);
                        // command.UnderlineStyle(UnderlineStyle.dotted);
                        command.InsertPageBreakAfterSelf();
                        System.Console.WriteLine("11");
                    }

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }

            if (exportData.ReportType == "รายภาค")
            {
                System.Console.WriteLine("in รายภาค");
                using (DocX document = DocX.Create(createfile))
                {
                    // document.DifferentOddAndEvenPages = true;
                    // document.Sections[i].DifferentFirstPage = true;

                    int l = 0;
                    for (int i = 0; i < model.reportData2.Length; i++)
                    {
                        l += 1;
                        System.Console.WriteLine("in loop: " + l);
                        System.Console.WriteLine("4");
                        Image image = document.AddImage(myImageFullPath);
                        Picture picture = image.CreatePicture(85, 85);
                        var logo = document.InsertParagraph();
                        logo.AppendPicture(picture).Alignment = Alignment.center;

                        System.Console.WriteLine("5");

                        // Add a title

                        var reportType = document.InsertParagraph("รายงานผลการตรวจราชการ (" + exportData.CentralPolicyType + ")" + " : " + exportData.ReportType);
                        reportType.FontSize(16d);
                        reportType.SpacingBefore(15d);
                        reportType.SpacingAfter(15d);
                        reportType.Bold();
                        reportType.Alignment = Alignment.center;

                        System.Console.WriteLine("6");
                        // Insert a title paragraph.
                        // var title = document.InsertParagraph("ประเด็น/เรื่อง:  " + model.reportData2[i].centralPolicy);
                        // title.Alignment = Alignment.center;
                        // title.SpacingAfter(15d);
                        // title.FontSize(16d);
                        // title.Bold();

                        System.Console.WriteLine("7");

                        // var year = document.InsertParagraph("วันที่ตรวจราชการ: ...............................");
                        // year.Alignment = Alignment.center;
                        // year.SpacingAfter(10d);
                        // year.FontSize(16d);
                        // System.Console.WriteLine("8");

                        // var inspector = document.InsertParagraph("ผู้ตรวจราชการสำนักนายกรัฐมนตรี/ผู้ตรวจราชการกระทรวง: " + model.reportData2[i].department);
                        // inspector.Alignment = Alignment.center;
                        // inspector.SpacingAfter(10d);
                        // inspector.FontSize(16d);

                        // System.Console.WriteLine("9");

                        var zone = document.InsertParagraph("ภาค: ......................");
                        zone.Alignment = Alignment.center;
                        zone.SpacingAfter(10d);
                        zone.FontSize(16d);

                        var year = document.InsertParagraph("รอบการตรวจราชการที่: " + exportData.InspectionRound + " ปีงบประมาณ: พ.ศ. " + model.reportData2[i].fiscalYear);
                        year.Alignment = Alignment.center;
                        year.SpacingAfter(15d);
                        year.FontSize(16d);

                        var statusReport = document.InsertParagraph("สถานะของรายงาน: " + exportData.Status);
                        statusReport.FontSize(16d);
                        statusReport.Alignment = Alignment.right;

                        var zone2 = document.InsertParagraph("ภาค: ............................... ประกอบด้วย");
                        zone2.Alignment = Alignment.left;
                        zone2.SpacingAfter(10d);
                        zone2.FontSize(16d);

                        var region = document.InsertParagraph("เขตตรวจราชการที่: " + model.reportData2[i].region + "(จังหวัด: " + model.reportData2[i].province + ")");
                        region.Alignment = Alignment.left;
                        region.SpacingAfter(15d);
                        region.FontSize(16d);

                        var monitorTopic = document.InsertParagraph("หัวข้อการตรวจติดตาม: " + exportData.MonitoringTopics);
                        monitorTopic.SpacingBefore(15d);
                        monitorTopic.FontSize(16d);
                        monitorTopic.Bold();

                        System.Console.WriteLine("99");

                        int dataCount = 0;
                        dataCount = model.reportData2[i].tableData.Count();
                        dataCount += 1;
                        System.Console.WriteLine("Data Count: " + dataCount);
                        // Add a table in a document of 1 row and 3 columns.
                        var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f, 300f };
                        var t = document.InsertTable(dataCount, columnWidths.Length);

                        System.Console.WriteLine("8");

                        // Set the table's column width and background 
                        t.SetWidths(columnWidths);
                        t.AutoFit = AutoFit.Contents;

                        var row = t.Rows.First();
                        System.Console.WriteLine("9");

                        // Fill in the columns of the first row in the table.
                        //for (int i = 0; i < row.Cells.Count; ++i)
                        //{
                        row.Cells[0].Paragraphs.First().Append("ประเด็นการตรวจติดตาม");
                        row.Cells[1].Paragraphs.First().Append("ข้อค้นพบ/ผลการตรวจ");
                        row.Cells[2].Paragraphs.First().Append("ประเด็นปัญหา");
                        row.Cells[3].Paragraphs.First().Append("ข้อเสนอแนะของผู้ตรวจราชการ");
                        row.Cells[4].Paragraphs.First().Append("หน่วยรับตรวจ/หน่วยงานที่รับผิดชอบ");
                        row.Cells[5].Paragraphs.First().Append("ความเห็นของ ที่ปรึกษา ผต.ภาคประชาชน");

                        System.Console.WriteLine("10");
                        //}
                        // Add rows in the table.
                        int j = 0;
                        for (int k = 0; k < model.reportData2[i].tableData.Length; k++)
                        {
                            j += 1;
                            //System.Console.WriteLine(i+=1);
                            System.Console.WriteLine("10.1");
                            System.Console.WriteLine("JJJJJ: " + j);
                            System.Console.WriteLine("9.1: " + model.reportData2[i].tableData[k].subject);
                            t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.2: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.3: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.4: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.5: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            // System.Console.WriteLine("9.6: " + model.reportData2[i].tableData[k].subject);
                            // t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData2[i].tableData[k].subject);
                            System.Console.WriteLine("10");
                        }

                        // Set a blank border for the table's top/bottom borders.
                        var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                        //t.SetBorder(TableBorderType.Bottom, blankBorder);
                        //t.SetBorder(TableBorderType.Top, blankBorder);

                        // document.InsertSectionPageBreak();

                        var detailTitle = document.InsertParagraph("สรุปผลการตรวจราชการ");
                        detailTitle.SpacingBefore(15d);
                        detailTitle.FontSize(16d);
                        detailTitle.Bold();
                        var detail = document.InsertParagraph(exportData.DetailReport);
                        detail.SpacingBefore(5d);
                        detail.FontSize(15d);
                        // detail.UnderlineColor(Color.Black);
                        // detail.UnderlineStyle(UnderlineStyle.dotted);

                        var suggestionTitle = document.InsertParagraph("ข้อเสนอแนะเชิงนโยบายของผู้ตรวจราชการ");
                        suggestionTitle.SpacingBefore(15d);
                        suggestionTitle.FontSize(16d);
                        suggestionTitle.Bold();
                        var suggestion = document.InsertParagraph(exportData.Suggestion);
                        suggestion.SpacingBefore(5d);
                        suggestion.FontSize(15d);
                        // suggestion.UnderlineColor(Color.Black);
                        // suggestion.UnderlineStyle(UnderlineStyle.dotted);

                        var commandTitle = document.InsertParagraph("ข้อสั่งการของผู้บังคับบัญชา");
                        commandTitle.SpacingBefore(15d);
                        commandTitle.FontSize(16d);
                        commandTitle.Bold();
                        var command = document.InsertParagraph(exportData.Command);
                        command.SpacingBefore(5d);
                        command.FontSize(15d);
                        // command.UnderlineColor(Color.Black);
                        // command.UnderlineStyle(UnderlineStyle.dotted);
                        command.InsertPageBreakAfterSelf();
                        System.Console.WriteLine("11");
                    }

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }

            return Ok(new { data = filename });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("subjectImport")]
        public IActionResult GetSubjectImport()
        {
            var data = _context.SubjectCentralPolicyProvinces
                .Where(x => x.Status == "ใช้งานจริง" && x.Type == "Master")
                .ToList();
            return Ok(new { data });
        }

        [HttpGet("getImportedReport/{userId}")]
        public IActionResult GetImportedReport(string userId)
        {
            var importData = _context.ImportReports
                .Include(x => x.ImportReportGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.InspectionPlanEvent)

                .Include(x => x.ImportReportGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.CentralPolicy)
                .Where(x => x.CreatedBy == userId)
                .ToList();

            //var importData = _context.ImportReportGroups
            //  .Include(x => x.ImportReport)
            //  .Where(x => x.ImportReport.CreatedBy == userId)
            //  .ToList();

            return Ok(new { importData });
        }

        [HttpGet("getImportedReportById/{reportId}")]
        public IActionResult GetImportedReportById(long reportId)
        {
            var importData = _context.ImportReports
                .Include(x => x.User)
                .ThenInclude(x => x.Departments)
                .Include(x => x.FiscalYear)
                .Include(x => x.Region)
                .Include(x => x.Province)
                .Include(x => x.ImportReportGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.InspectionPlanEvent)
                .ThenInclude(x => x.CentralPolicies)
                .ThenInclude(x => x.CentralPolicyProvinces)
                .ThenInclude(x => x.SubjectCentralPolicyProvinces)

                .Include(x => x.ImportReportGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.CentralPolicy)
                .ThenInclude(x => x.CentralPolicyProvinces)
                .ThenInclude(x => x.SubjectCentralPolicyProvinces)
                .ThenInclude(x => x.SubquestionCentralPolicyProvinces)
                .ThenInclude(x => x.SubjectCentralPolicyProvinceGroups)
                .ThenInclude(x => x.ProvincialDepartment)
                .ThenInclude(x => x.Department)

                .Include(x => x.ReportCommanders)
                .ThenInclude(x => x.User)

                .Include(x => x.ImportReportFiles)

                .Where(x => x.Id == reportId)
                .FirstOrDefault();

            //var importData = _context.ImportReportGroups
            //  .Include(x => x.ImportReport)
            //  .Where(x => x.ImportReport.CreatedBy == userId)
            //  .ToList();

            return Ok(new { importData });
        }

        [HttpGet("getCommanderReport")]
        public IActionResult GetCommanderReport(string userId)
        {
            var data = _context.ReportCommanders
                .ToList();
            return Ok(new { data });
        }

        [HttpGet("getCommanderReport/{provinceId}/{userID}")]
        public IActionResult GetCommanderReport(long provinceId, string userID)
        {
            System.Console.WriteLine("ProvinceId: " + provinceId);
            var commanderReport = _context.ImportReports
                .Include(x => x.ImportReportGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.CentralPolicy)
                .Include(x => x.User)
                //.Where(x => x.ProvinceId == provinceId && x.Status == "ส่งแล้ว")
                .Where(x => x.SendCommander == userID && x.Status == "ส่งแล้ว")
                .ToList();

            return Ok(new { commanderReport });
        }

        [HttpDelete("deleteImportedReport/{deleteId}")]
        public void Delete(long deleteId)
        {
            System.Console.WriteLine("ID: " + deleteId);
            var importedReportData = _context.ImportReports.Find(deleteId);

            _context.ImportReports.Remove(importedReportData);
            _context.SaveChanges();
        }

        [HttpGet("getImportReportFiscalYears")]
        public IActionResult GetImportReportFiscalYears()
        {
            var importFiscalYear = _context.FiscalYears
            .ToList();

            return Ok(new { importFiscalYear });
        }

        [HttpGet("getImportReportFiscalYearRelations/{fiscalYearId}")]
        public IActionResult GetImportReportFiscalYearRelations(long fiscalYearId)
        {
            var importFiscalYearRelations = _context.FiscalYearRelations
            .Include(x => x.Region)
            .Include(x => x.Province)
            .Where(x => x.FiscalYearId == fiscalYearId)
            .ToList();

            return Ok(new { importFiscalYearRelations });
        }

        [HttpPost("addImportReport")]
        public async Task<IActionResult> PostElectronicBookToProvince([FromForm] ImportReportViewModel model)
        {
            var zoneData = _context.Provinces
                .Where(x => x.Id == model.provinceId)
                .FirstOrDefault();

            var importReportData = new ImportReport
            {
                FiscalYearId = model.fiscalYearId,
                RegionId = model.regionId,
                ProvinceId = model.provinceId,
                CentralPolicyType = model.centralPolicyType,
                ReportType = model.reportType,
                InspectionRound = model.inspectionRound,
                MonitoringTopics = model.monitoringTopics,
                DetailReport = model.detailReport,
                Suggestion = model.suggestion,
                Command = model.command,
                CreatedBy = model.UserId,
                CreateAt = DateTime.Now,
                Status = "ร่าง",
                DepartmentId = model.DepartmentId,
                ZoneId = zoneData.SectorId,
            };
            _context.ImportReports.Add(importReportData);
            _context.SaveChanges();
            System.Console.WriteLine("finished.");

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";

            if (model.files != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {
                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("Start Upload 4");
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }
                        System.Console.WriteLine("Start Upload 4.1");
                        var ImportReportFileData = new ImportReportFile
                        {
                            ImportReportId = importReportData.Id,
                            Name = random + filename,
                            // Description = model.Description,
                            // Type = model.Type
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.ImportReportFiles.Add(ImportReportFileData);
                        _context.SaveChanges();
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }


            foreach (var centralPolicyEventId in model.centralPolicyEventId)
            {
                var importReportGroupData = new ImportReportGroup
                {
                    ImportReportId = importReportData.Id,
                    CentralPolicyEventId = centralPolicyEventId
                };

                _context.ImportReportGroups.Add(importReportGroupData);
                _context.SaveChanges();
            }

            return Ok(new { status = true });
        }

        [HttpPut("sendReportToCommander")]
        public IActionResult SendReportToCommander(ImportReportViewModel model)
        {
            System.Console.WriteLine("ReportId: " + model.reportId);

            // (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
            //   ForEach(x => x.Status = status);

            var importReport = _context.ImportReports
               .Where(x => x.Id == model.reportId)
               .FirstOrDefault();
            {
                importReport.Status = "ส่งแล้ว";
                importReport.SendCommander = model.Commander;
            }
            _context.Entry(importReport).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok(new { status = true });
        }

        [HttpPost("sendCommand")]
        public IActionResult SendCommand(ImportReportViewModel model)
        {

            System.Console.WriteLine("reportID: " + model.reportId);
            System.Console.WriteLine("Command: " + model.command);
            System.Console.WriteLine("userId: " + model.UserId);
            var commandData = new ReportCommander
            {
                ImportReportId = model.reportId,
                Command = model.command,
                UserCommanderId = model.UserId,
                Status = "บันทึกข้อสั่งการแล้ว",
                CreateAt = DateTime.Now,
            };
            _context.ReportCommanders.Add(commandData);
            _context.SaveChanges();
            System.Console.WriteLine("finished.");

            return Ok(new { status = true });
        }

        [HttpPost("editImportReport")]
        public IActionResult EditImportReport(ImportReportViewModel model)
        {
            System.Console.WriteLine("ReportId: " + model.reportId);

            // (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
            //   ForEach(x => x.Status = status);

            var importReport = _context.ImportReports
               .Where(x => x.Id == model.reportId)
               .FirstOrDefault();
            {
                importReport.FiscalYearId = model.fiscalYearId;
                importReport.RegionId = model.regionId;
                importReport.ProvinceId = model.provinceId;
                importReport.CentralPolicyType = model.centralPolicyType;
                importReport.ReportType = model.reportType;
                importReport.InspectionRound = model.inspectionRound;
                importReport.MonitoringTopics = model.monitoringTopics;
                importReport.DetailReport = model.detailReport;
                importReport.Suggestion = model.suggestion;
                importReport.Command = model.command;
            }
            _context.Entry(importReport).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            System.Console.WriteLine("Edit 1");

            var removeReportGroup = _context.ImportReportGroups
            .Where(x => x.ImportReportId == model.reportId)
            .ToList();

            foreach (var item in removeReportGroup)
            {
                // var remove = _context.ImportReportGroups
                // .Where(x => x.ImportReportId == item.Id)
                // .FirstOrDefault();

                var removeData = _context.ImportReportGroups.Find(item.Id);
                _context.ImportReportGroups.Remove(removeData);
                _context.SaveChanges();
            }

            System.Console.WriteLine("Edit 2");

            foreach (var centralPolicyEventId in model.centralPolicyEventId)
            {
                var importReportGroupData = new ImportReportGroup
                {
                    ImportReportId = model.reportId,
                    CentralPolicyEventId = centralPolicyEventId
                };

                _context.ImportReportGroups.Add(importReportGroupData);
                _context.SaveChanges();
            }

            System.Console.WriteLine("Edit 3");

            return Ok(new { status = true });
        }

        [HttpGet("getCommander")]
        public IActionResult GetCommander()
        {
            var data = _context.Users
                .Where(x => x.Role_id == 8)
                .ToList();
            return Ok(new { data });
        }

        [HttpGet("getAllImportedReport")]
        public IActionResult GetAllImportedReport()
        {
            var importData = _context.ImportReports
                .Include(x => x.ImportReportGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.InspectionPlanEvent)

                .Include(x => x.ImportReportGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.CentralPolicy)
                .Where(x => x.Status == "ส่งแล้ว")
                .ToList();

            //var importData = _context.ImportReportGroups
            //  .Include(x => x.ImportReport)
            //  .Where(x => x.ImportReport.CreatedBy == userId)
            //  .ToList();

            return Ok(new { importData });
        }

        [HttpGet("getDepartments")]
        public IActionResult GetDepartments()
        {
            var Departments = _context.Departments
            .ToList();

            return Ok(new { Departments });
        }

        [HttpGet("getZones")]
        public IActionResult GetZones()
        {
            var Sectors = _context.Sectors
            .ToList();

            return Ok(new { Sectors });
        }

        [HttpGet("getRegions")]
        public IActionResult GetRegions()
        {
            var Regions = _context.Regions
            .ToList();

            return Ok(new { Regions });
        }

        [HttpGet("getProvinces")]
        public IActionResult GetProvinces()
        {
            var Provinces = _context.Provinces
            .ToList();

            return Ok(new { Provinces });
        }

        [HttpGet("getAllReportByDepartment/{departmentId}")]
        public IActionResult GetAllReportByDepartment(long departmentId)
        {
            var Reports = _context.ImportReports
                .Include(x => x.ImportReportGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.CentralPolicy)
                .ThenInclude(x => x.CentralPolicyProvinces)
                .ThenInclude(x => x.SubjectCentralPolicyProvinces)
                .Include(x => x.FiscalYear)
                .Include(x => x.User)
                .ThenInclude(x => x.Departments)
                .Include(x => x.Commander)
                .Include(x => x.Region)
                .Include(x => x.Province)
                .Include(x => x.ReportCommanders)
                .Where(x => x.DepartmentId == departmentId && x.Status == "ส่งแล้ว")
            .ToList();

            return Ok(new { Reports });
        }

        [HttpGet("getAllReportByRegion/{regionId}")]
        public IActionResult GetAllReportByRegion(long regionId)
        {
            var Reports = _context.ImportReports
                .Include(x => x.ImportReportGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.CentralPolicy)
                .ThenInclude(x => x.CentralPolicyProvinces)
                .ThenInclude(x => x.SubjectCentralPolicyProvinces)
                .Include(x => x.FiscalYear)
                .Include(x => x.User)
                .ThenInclude(x => x.Departments)
                .Include(x => x.Commander)
                .Include(x => x.Region)
                .Include(x => x.Province)
                .Include(x => x.ReportCommanders)
                .Where(x => x.RegionId == regionId && x.Status == "ส่งแล้ว")
            .ToList();

            return Ok(new { Reports });
        }

        [HttpGet("getAllReportByZone/{ZoneId}")]
        public IActionResult GetAllReportByZone(long zoneId)
        {
            var Reports = _context.ImportReports
                .Include(x => x.ImportReportGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.CentralPolicy)
                .ThenInclude(x => x.CentralPolicyProvinces)
                .ThenInclude(x => x.SubjectCentralPolicyProvinces)
                .Include(x => x.FiscalYear)
                .Include(x => x.User)
                .ThenInclude(x => x.Departments)
                .Include(x => x.Commander)
                .Include(x => x.Region)
                .Include(x => x.Province)
                .Include(x => x.ReportCommanders)
                .Include(x => x.Zone)
                .Where(x => x.ZoneId == zoneId && x.Status == "ส่งแล้ว")
            .ToList();

            return Ok(new { Reports });
        }

        [HttpGet("getAllReportByProvince/{provinceId}")]
        public IActionResult GetAllReportByProvince(long provinceId)
        {
            var Reports = _context.ImportReports
                .Include(x => x.ImportReportGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.CentralPolicy)
                .ThenInclude(x => x.CentralPolicyProvinces)
                .ThenInclude(x => x.SubjectCentralPolicyProvinces)
                .Include(x => x.FiscalYear)
                .Include(x => x.User)
                .ThenInclude(x => x.Departments)
                .Include(x => x.Commander)
                .Include(x => x.Region)
                .Include(x => x.Province)
                .Include(x => x.ReportCommanders)
                .Where(x => x.ProvinceId == provinceId && x.Status == "ส่งแล้ว")
            .ToList();

            return Ok(new { Reports });
        }

        [HttpPost("getAllReportByDay")]
        public IActionResult GetAllReportByDay(ExportReportViewModel model)
        {
            System.Console.WriteLine("StartDate: " + model.startDate.Date);
            var Reports = _context.ImportReports
                .Include(x => x.ImportReportGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.CentralPolicy)
                .ThenInclude(x => x.CentralPolicyProvinces)
                .ThenInclude(x => x.SubjectCentralPolicyProvinces)
                .Include(x => x.FiscalYear)
                .Include(x => x.User)
                .ThenInclude(x => x.Departments)
                .Include(x => x.Commander)
                .Include(x => x.Region)
                .Include(x => x.Province)
                .Include(x => x.ReportCommanders)
                .Where(x => x.CreateAt.Value.Date == model.startDate.Date && x.Status == "ส่งแล้ว")
            .ToList();

            return Ok(new { Reports });
        }

        [HttpPost("exportAllDepartmentReport")]
        public IActionResult ExportAllDepartmentReport([FromBody] ExportReportViewModel model)
        {

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "ทะเบียนรายงานผลการตรวจราชการ" + DateTime.Now.ToString("dd MM yyyy") + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");

            //if (exportData.ReportType == "รายเขต")
            //{
            System.Console.WriteLine("in รายเขต");
            using (DocX document = DocX.Create(createfile))
            {
                document.PageLayout.Orientation = Orientation.Landscape;
                System.Console.WriteLine("4");

                var reportType = document.InsertParagraph("ทะเบียนรายงานผลการตรวจราชการ : " + model.reportType);
                reportType.FontSize(20d);
                reportType.SpacingAfter(15d);
                reportType.Bold();
                reportType.Alignment = Alignment.center;

                System.Console.WriteLine("6");
                // Insert a title paragraph.
                var title = document.InsertParagraph("หน่วยงาน:  " + model.reportDepartment);
                title.Alignment = Alignment.center;
                title.SpacingAfter(15d);
                title.FontSize(18d);
                title.Bold();

                System.Console.WriteLine("7");

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                var thDate = DateTime.Now.ToString("dd MMMM yyyy");

                var year = document.InsertParagraph("วันที่เรียกรายงาน: " + thDate);
                year.Alignment = Alignment.right;
                year.SpacingAfter(10d);
                year.FontSize(16d);
                System.Console.WriteLine("8");

                int dataCount = 0;
                dataCount = model.allReport.Count();
                dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 35f, 85f, 190f, 130f, 65f, 200f };
                var t = document.InsertTable(dataCount, columnWidths.Length);
                t.Alignment = Alignment.center;

                System.Console.WriteLine("8");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();
                System.Console.WriteLine("9");

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row.Cells[0].Paragraphs.First().Append("ลำดับที่").Alignment = Alignment.center;
                row.Cells[1].Paragraphs.First().Append("วัน/เดือน/ปี ที่มีรายงาน").Alignment = Alignment.center;
                row.Cells[2].Paragraphs.First().Append("ประเด็น/เรื่อง").Alignment = Alignment.center;
                row.Cells[3].Paragraphs.First().Append("ผู้สร้างรายงาน").Alignment = Alignment.center;
                row.Cells[4].Paragraphs.First().Append("สถานะรายงาน").Alignment = Alignment.center;
                row.Cells[5].Paragraphs.First().Append("ข้อสั่งการของผู้บังคับบัญชา").Alignment = Alignment.center;

                System.Console.WriteLine("10");
                //}
                // Add rows in the table.
                int j = 0;
                for (int k = 0; k < model.allReport.Length; k++)
                {
                    j += 1;
                    //System.Console.WriteLine(i+=1);
                    System.Console.WriteLine("10.1");
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                    var thDate2 = model.allReport[k].dateReport.ToString("dd MMMM yyyy");

                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString()).Alignment = Alignment.center;
                    t.Rows[j].Cells[1].Paragraphs[0].Append(thDate2);
                    t.Rows[j].Cells[2].Paragraphs[0].Append(model.allReport[k].subject);
                    t.Rows[j].Cells[3].Paragraphs[0].Append(model.allReport[k].createBy);
                    t.Rows[j].Cells[4].Paragraphs[0].Append(model.allReport[k].status).Alignment = Alignment.center;
                    t.Rows[j].Cells[5].Paragraphs[0].Append(model.allReport[k].command);
                    System.Console.WriteLine("10");
                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                // document.InsertSectionPageBreak();
                //}

                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }

            return Ok(new { data = filename });
        }

        [HttpPost("exportAllRegionReport")]
        public IActionResult ExportAllRegionReport([FromBody] ExportReportViewModel model)
        {

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "ทะเบียนรายงานผลการตรวจราชการ" + DateTime.Now.ToString("dd MM yyyy") + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");

            //if (exportData.ReportType == "รายเขต")
            //{
            System.Console.WriteLine("in รายเขต");
            using (DocX document = DocX.Create(createfile))
            {

                //var province = _context.FiscalYearRelations
                //    .Where(x => x.RegionId == model.reportRegionId)
                //    .Select(x => x.Province.Name)
                //    .FirstOrDefault();



                document.PageLayout.Orientation = Orientation.Landscape;
                System.Console.WriteLine("4");

                var reportType = document.InsertParagraph("ทะเบียนรายงานผลการตรวจราชการ : " + model.reportType);
                reportType.FontSize(20d);
                reportType.SpacingAfter(15d);
                reportType.Bold();
                reportType.Alignment = Alignment.center;

                System.Console.WriteLine("6");
                // Insert a title paragraph.
                var title = document.InsertParagraph("เขตตรวจราชการที่:  " + model.reportRegion);
                title.Alignment = Alignment.center;
                title.SpacingAfter(15d);
                title.FontSize(18d);
                title.Bold();

                System.Console.WriteLine("7");

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                var thDate = DateTime.Now.ToString("dd MMMM yyyy");

                var year = document.InsertParagraph("วันที่เรียกรายงาน: " + thDate);
                year.Alignment = Alignment.right;
                year.SpacingAfter(10d);
                year.FontSize(16d);
                System.Console.WriteLine("8");

                int dataCount = 0;
                dataCount = model.allReport.Count();
                dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 35f, 85f, 190f, 80f, 130f, 65f, 200f };
                var t = document.InsertTable(dataCount, columnWidths.Length);
                t.Alignment = Alignment.center;

                System.Console.WriteLine("8");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();
                System.Console.WriteLine("9");

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row.Cells[0].Paragraphs.First().Append("ลำดับที่").Alignment = Alignment.center;
                row.Cells[1].Paragraphs.First().Append("วัน/เดือน/ปี ที่มีรายงาน").Alignment = Alignment.center;
                row.Cells[2].Paragraphs.First().Append("ประเด็น/เรื่อง").Alignment = Alignment.center;
                row.Cells[3].Paragraphs.First().Append("จังหวัด").Alignment = Alignment.center;
                row.Cells[4].Paragraphs.First().Append("ผู้สร้างรายงาน").Alignment = Alignment.center;
                row.Cells[5].Paragraphs.First().Append("สถานะรายงาน").Alignment = Alignment.center;
                row.Cells[6].Paragraphs.First().Append("ข้อสั่งการของผู้บังคับบัญชา").Alignment = Alignment.center;

                System.Console.WriteLine("10");
                //}
                // Add rows in the table.
                int j = 0;
                for (int k = 0; k < model.allReport.Length; k++)
                {
                    j += 1;
                    //System.Console.WriteLine(i+=1);
                    System.Console.WriteLine("10.1");
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                    var thDate2 = model.allReport[k].dateReport.ToString("dd MMMM yyyy");

                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString()).Alignment = Alignment.center;
                    t.Rows[j].Cells[1].Paragraphs[0].Append(thDate2);
                    t.Rows[j].Cells[2].Paragraphs[0].Append(model.allReport[k].subject);
                    t.Rows[j].Cells[3].Paragraphs[0].Append(model.allReport[k].provinceReport);
                    t.Rows[j].Cells[4].Paragraphs[0].Append(model.allReport[k].createBy);
                    t.Rows[j].Cells[5].Paragraphs[0].Append(model.allReport[k].status).Alignment = Alignment.center;
                    t.Rows[j].Cells[6].Paragraphs[0].Append(model.allReport[k].command);
                    System.Console.WriteLine("10");
                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                // document.InsertSectionPageBreak();
                //}

                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }

            return Ok(new { data = filename });
        }

        [HttpPost("exportAllProvinceReport")]
        public IActionResult ExportAllProvinceReport([FromBody] ExportReportViewModel model)
        {

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "ทะเบียนรายงานผลการตรวจราชการ" + DateTime.Now.ToString("dd MM yyyy") + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");

            //if (exportData.ReportType == "รายเขต")
            //{
            System.Console.WriteLine("in รายเขต");
            using (DocX document = DocX.Create(createfile))
            {

                //var province = _context.FiscalYearRelations
                //    .Where(x => x.RegionId == model.reportRegionId)
                //    .Select(x => x.Province.Name)
                //    .FirstOrDefault();



                document.PageLayout.Orientation = Orientation.Landscape;
                System.Console.WriteLine("4");

                var reportType = document.InsertParagraph("ทะเบียนรายงานผลการตรวจราชการ : " + model.reportType);
                reportType.FontSize(20d);
                reportType.SpacingAfter(15d);
                reportType.Bold();
                reportType.Alignment = Alignment.center;

                System.Console.WriteLine("6");
                // Insert a title paragraph.
                var title = document.InsertParagraph("จังหวัด:  " + model.reportProvince);
                title.Alignment = Alignment.center;
                title.SpacingAfter(15d);
                title.FontSize(18d);
                title.Bold();

                System.Console.WriteLine("7");

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                var thDate = DateTime.Now.ToString("dd MMMM yyyy");

                var year = document.InsertParagraph("วันที่เรียกรายงาน: " + thDate);
                year.Alignment = Alignment.right;
                year.SpacingAfter(10d);
                year.FontSize(16d);
                System.Console.WriteLine("8");

                int dataCount = 0;
                dataCount = model.allReport.Count();
                dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 35f, 85f, 190f, 130f, 65f, 200f };
                var t = document.InsertTable(dataCount, columnWidths.Length);
                t.Alignment = Alignment.center;

                System.Console.WriteLine("8");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();
                System.Console.WriteLine("9");

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row.Cells[0].Paragraphs.First().Append("ลำดับที่").Alignment = Alignment.center;
                row.Cells[1].Paragraphs.First().Append("วัน/เดือน/ปี ที่มีรายงาน").Alignment = Alignment.center;
                row.Cells[2].Paragraphs.First().Append("ประเด็น/เรื่อง").Alignment = Alignment.center;
                row.Cells[3].Paragraphs.First().Append("ผู้สร้างรายงาน").Alignment = Alignment.center;
                row.Cells[4].Paragraphs.First().Append("สถานะรายงาน").Alignment = Alignment.center;
                row.Cells[5].Paragraphs.First().Append("ข้อสั่งการของผู้บังคับบัญชา").Alignment = Alignment.center;

                System.Console.WriteLine("10");
                //}
                // Add rows in the table.
                int j = 0;
                for (int k = 0; k < model.allReport.Length; k++)
                {
                    j += 1;
                    //System.Console.WriteLine(i+=1);
                    System.Console.WriteLine("10.1");
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                    var thDate2 = model.allReport[k].dateReport.ToString("dd MMMM yyyy");

                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString()).Alignment = Alignment.center;
                    t.Rows[j].Cells[1].Paragraphs[0].Append(thDate2);
                    t.Rows[j].Cells[2].Paragraphs[0].Append(model.allReport[k].subject);
                    t.Rows[j].Cells[3].Paragraphs[0].Append(model.allReport[k].createBy);
                    t.Rows[j].Cells[4].Paragraphs[0].Append(model.allReport[k].status).Alignment = Alignment.center;
                    t.Rows[j].Cells[5].Paragraphs[0].Append(model.allReport[k].command);
                    System.Console.WriteLine("10");
                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                // document.InsertSectionPageBreak();
                //}

                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }

            return Ok(new { data = filename });
        }

        [HttpPost("exportAllDayReport")]
        public IActionResult ExportAllDayReport([FromBody] ExportReportViewModel model)
        {

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "ทะเบียนรายงานผลการตรวจราชการ" + DateTime.Now.ToString("dd MM yyyy") + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");

            //if (exportData.ReportType == "รายเขต")
            //{
            System.Console.WriteLine("in รายเขต");
            using (DocX document = DocX.Create(createfile))
            {

                //var province = _context.FiscalYearRelations
                //    .Where(x => x.RegionId == model.reportRegionId)
                //    .Select(x => x.Province.Name)
                //    .FirstOrDefault();



                document.PageLayout.Orientation = Orientation.Landscape;
                System.Console.WriteLine("4");

                var reportType = document.InsertParagraph("ทะเบียนรายงานผลการตรวจราชการ : " + model.reportType);
                reportType.FontSize(20d);
                reportType.SpacingAfter(15d);
                reportType.Bold();
                reportType.Alignment = Alignment.center;

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                var reportDate = model.reportDate.ToString("dd MMMM yyyy");

                System.Console.WriteLine("6");
                // Insert a title paragraph.
                var title = document.InsertParagraph("รายงานประจำวันที่:  " + reportDate);
                title.Alignment = Alignment.center;
                title.SpacingAfter(15d);
                title.FontSize(18d);
                title.Bold();

                System.Console.WriteLine("7");

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                var thDate = DateTime.Now.ToString("dd MMMM yyyy");

                var year = document.InsertParagraph("วันที่เรียกรายงาน: " + thDate);
                year.Alignment = Alignment.right;
                year.SpacingAfter(10d);
                year.FontSize(16d);
                System.Console.WriteLine("8");

                int dataCount = 0;
                dataCount = model.allReport.Count();
                dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 35f, 130f, 190f, 65f, 200f };
                var t = document.InsertTable(dataCount, columnWidths.Length);
                t.Alignment = Alignment.center;

                System.Console.WriteLine("8");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();
                System.Console.WriteLine("9");

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row.Cells[0].Paragraphs.First().Append("ลำดับที่").Alignment = Alignment.center;
                //row.Cells[1].Paragraphs.First().Append("วัน/เดือน/ปี ที่มีรายงาน").Alignment = Alignment.center;

                row.Cells[1].Paragraphs.First().Append("ผู้สร้างรายงาน").Alignment = Alignment.center;
                row.Cells[2].Paragraphs.First().Append("ประเด็น/เรื่อง").Alignment = Alignment.center;
                row.Cells[3].Paragraphs.First().Append("สถานะรายงาน").Alignment = Alignment.center;
                row.Cells[4].Paragraphs.First().Append("ข้อสั่งการของผู้บังคับบัญชา").Alignment = Alignment.center;

                System.Console.WriteLine("10");
                //}
                // Add rows in the table.
                int j = 0;
                for (int k = 0; k < model.allReport.Length; k++)
                {
                    j += 1;
                    //System.Console.WriteLine(i+=1);
                    System.Console.WriteLine("10.1");
                    //Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                    //var thDate2 = model.allReport[k].dateReport.ToString("dd MMMM yyyy");

                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString()).Alignment = Alignment.center;
                    t.Rows[j].Cells[1].Paragraphs[0].Append(model.allReport[k].createBy);
                    t.Rows[j].Cells[2].Paragraphs[0].Append(model.allReport[k].subject);
                    t.Rows[j].Cells[3].Paragraphs[0].Append(model.allReport[k].status).Alignment = Alignment.center;
                    t.Rows[j].Cells[4].Paragraphs[0].Append(model.allReport[k].command);
                    System.Console.WriteLine("10");
                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                // document.InsertSectionPageBreak();
                //}

                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }

            return Ok(new { data = filename });
        }

        [HttpPost("exportAllZoneReport")]
        public IActionResult ExportAllZoneReport([FromBody] ExportReportViewModel model)
        {

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "ทะเบียนรายงานผลการตรวจราชการ" + DateTime.Now.ToString("dd MM yyyy") + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");

            //if (exportData.ReportType == "รายเขต")
            //{
            System.Console.WriteLine("in รายเขต");
            using (DocX document = DocX.Create(createfile))
            {

                //var province = _context.FiscalYearRelations
                //    .Where(x => x.RegionId == model.reportRegionId)
                //    .Select(x => x.Province.Name)
                //    .FirstOrDefault();



                document.PageLayout.Orientation = Orientation.Landscape;
                System.Console.WriteLine("4");

                var reportType = document.InsertParagraph("ทะเบียนรายงานผลการตรวจราชการ : " + model.reportType);
                reportType.FontSize(20d);
                reportType.SpacingAfter(15d);
                reportType.Bold();
                reportType.Alignment = Alignment.center;

                System.Console.WriteLine("6");
                // Insert a title paragraph.
                var title = document.InsertParagraph("ภาค:  " + model.reportZone);
                title.Alignment = Alignment.center;
                title.SpacingAfter(15d);
                title.FontSize(18d);
                title.Bold();

                System.Console.WriteLine("7");

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                var thDate = DateTime.Now.ToString("dd MMMM yyyy");

                var year = document.InsertParagraph("วันที่เรียกรายงาน: " + thDate);
                year.Alignment = Alignment.right;
                year.SpacingAfter(10d);
                year.FontSize(16d);
                System.Console.WriteLine("8");

                int dataCount = 0;
                dataCount = model.allReport.Count();
                dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 35f, 85f, 190f, 80f, 130f, 65f, 200f };
                var t = document.InsertTable(dataCount, columnWidths.Length);
                t.Alignment = Alignment.center;

                System.Console.WriteLine("8");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();
                System.Console.WriteLine("9");

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row.Cells[0].Paragraphs.First().Append("ลำดับที่").Alignment = Alignment.center;
                row.Cells[1].Paragraphs.First().Append("วัน/เดือน/ปี ที่มีรายงาน").Alignment = Alignment.center;
                row.Cells[2].Paragraphs.First().Append("ประเด็น/เรื่อง").Alignment = Alignment.center;
                row.Cells[3].Paragraphs.First().Append("จังหวัด").Alignment = Alignment.center;
                row.Cells[4].Paragraphs.First().Append("ผู้สร้างรายงาน").Alignment = Alignment.center;
                row.Cells[5].Paragraphs.First().Append("สถานะรายงาน").Alignment = Alignment.center;
                row.Cells[6].Paragraphs.First().Append("ข้อสั่งการของผู้บังคับบัญชา").Alignment = Alignment.center;

                System.Console.WriteLine("10");
                //}
                // Add rows in the table.
                int j = 0;
                for (int k = 0; k < model.allReport.Length; k++)
                {
                    j += 1;
                    //System.Console.WriteLine(i+=1);
                    System.Console.WriteLine("10.1");
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                    var thDate2 = model.allReport[k].dateReport.ToString("dd MMMM yyyy");

                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString()).Alignment = Alignment.center;
                    t.Rows[j].Cells[1].Paragraphs[0].Append(thDate2);
                    t.Rows[j].Cells[2].Paragraphs[0].Append(model.allReport[k].subject);
                    t.Rows[j].Cells[3].Paragraphs[0].Append(model.allReport[k].provinceReport);
                    t.Rows[j].Cells[4].Paragraphs[0].Append(model.allReport[k].createBy);
                    t.Rows[j].Cells[5].Paragraphs[0].Append(model.allReport[k].status).Alignment = Alignment.center;
                    t.Rows[j].Cells[6].Paragraphs[0].Append(model.allReport[k].command);
                    System.Console.WriteLine("10");
                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                // document.InsertSectionPageBreak();
                //}

                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }

            return Ok(new { data = filename });
        }
    }
}
