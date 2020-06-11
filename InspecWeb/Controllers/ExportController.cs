using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
                .Include(x => x.ElectronicBookSuggestGroups)

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

        [HttpPost("addImportReport")]
        public async Task<IActionResult> PostImportReport([FromForm] ImportReportViewModel model)
        {
            var SubjectName = _context.SubjectCentralPolicyProvinces
                .Where(x => x.Id == model.SubjectId)
                .Select(x => x.Name)
                .FirstOrDefault();

            long importId = 0;

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";

            if (model.fileWord != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.fileWord.Select((value, index) => new { Value = value, Index = index }))
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
                        using (var stream = System.IO.File.Create(filePath + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        System.Console.WriteLine("Start Upload 4.1");
                        var ImportReport = new ReportCommander
                        {
                            Subject = SubjectName,
                            TypeExport = model.TypeExport,
                            TypeReport = model.TypeReport,
                            FileWord = filename,
                            CreateBy = model.CreateBy
                        };

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.ReportCommanders.Add(ImportReport);
                        _context.SaveChanges();

                        importId = ImportReport.Id;

                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            if (model.fileExcel != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.fileExcel.Select((value, index) => new { Value = value, Index = index }))
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
                        using (var stream = System.IO.File.Create(filePath + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }
                        System.Console.WriteLine("Start Upload 4.1");

                        //var ImportReport = new ReportCommander
                        //{
                        //    Subject = SubjectName,
                        //    TypeExport = model.TypeExport,
                        //    TypeReport = model.TypeReport,
                        //    FileWord = random + filename,
                        //};
                        System.Console.WriteLine("Import ID: " + importId);

                        var importData = _context.ReportCommanders.Find(importId);
                        {
                            importData.FileExcel = filename;
                        }

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.Entry(importData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();

                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }
            return Ok(new { importId });
        }

        [HttpGet("getImportedReport/{userId}")]
        public IActionResult GetImportedReport(string userId)
        {
            var data = _context.ReportCommanders
                .Where(x => x.CreateBy == userId)
                .ToList();
            return Ok(new { data });
        }

        [HttpGet("getCommanderReport")]
        public IActionResult GetCommanderReport(string userId)
        {
            var data = _context.ReportCommanders
                .ToList();
            return Ok(new { data });
        }

        [HttpGet("getCommanderReport/{reportId}")]
        public IActionResult GetCommanderReport(long reportId)
        {
            var data = _context.ReportCommanders
                .Where(x => x.Id == reportId)
                .FirstOrDefault();

            var commanderName = _context.Users
                .Where(x => x.Id == data.Commander)
                .FirstOrDefault();

            return Ok(new { data, commanderName });
        }

        [HttpDelete("deleteImportedReport/{deleteId}")]
        public void Delete(long deleteId)
        {
            System.Console.WriteLine("ID: " + deleteId);
            var importedReportData = _context.ReportCommanders.Find(deleteId);

            _context.ReportCommanders.Remove(importedReportData);
            _context.SaveChanges();
        }

        [HttpPut("sendCommand")]
        public IActionResult PutCommand([FromBody]ImportReportViewModel model)
        {
            System.Console.WriteLine("Report ID: " + model.ReportId);
            System.Console.WriteLine("Command: " + model.Command);
            System.Console.WriteLine("Commander: " + model.Commander);
            var commandData = _context.ReportCommanders
                .Where(x => x.Id == model.ReportId)
                .FirstOrDefault();
            {
                commandData.Command = model.Command;
                commandData.Commander = model.Commander;
            }

            _context.Entry(commandData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            System.Console.WriteLine("Command Finished.");
            return Ok(commandData);
        }
    }
}
