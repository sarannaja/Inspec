using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Drawing;
using System.IO;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Image = Xceed.Document.NET.Image;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
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
        private readonly IEmailSender _emailSender;
        private static UserManager<ApplicationUser> _userManager;
        public ReportController(ApplicationDbContext context, IEmailSender emailSender, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet("province")]
        public IActionResult Getprovince()
        {

            var provincedata = from P in _context.Provinces
                               select P;
            return Ok(provincedata);
        }

        // GET: api/values
        [HttpGet("centralpolicyprovinces/{id}")]
        public IActionResult Getcentralpolicyprovinces(long id)
        {
            var centralpolicyprovincesdata = _context.CentralPolicyProvinces
                .Include(m => m.CentralPolicy)
                .Where(m => m.ProvinceId == id);
            return Ok(centralpolicyprovincesdata);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            //var centralpolicydata = _context.CentralPolicies
            //    .Include(m => m.CentralPolicyProvinces)
            //    .ThenInclude(m => m.Province)
            //    .Include(m => m.CentralPolicyProvinces)
            //    .ThenInclude(m => m.SubjectCentralPolicyProvinces)
            //    .ThenInclude(m => m.SubquestionCentralPolicyProvinces)
            //    .ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
            //    .ThenInclude(m => m.ProvincialDepartment)
            //    .Where(m => m.Id == id).FirstOrDefault();
            //var user = _userManager.Users
            //    .Where(m => m.Id == centralpolicydata.CreatedBy)
            //    .Include(m => m.Departments)
            //    //.ThenInclude(m=>m.)
            //    .FirstOrDefault();

            var subjectdata = _context.SubjectCentralPolicyProvinces
                .Include(m => m.CentralPolicyProvince)
                .ThenInclude(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyProvinces)
                .ThenInclude(x => x.Province)
                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
                .ThenInclude(m => m.ProvincialDepartment)
                //.Where(m => m.CentralPolicyId == id);
                .Where(m => m.CentralPolicyProvince.CentralPolicyId == id && m.Type == "Master");

            //return Ok(new { centralpolicydata, user });
            return Ok(new { subjectdata });
        }

        [HttpGet("subjectdetail/{id}")]
        public IActionResult Get2(long id)
        {
            var subjectdata = _context.SubjectCentralPolicyProvinces
                .Include(m => m.CentralPolicyProvince)
                .Include(m => m.SubjectDateCentralPolicyProvinces)
                .ThenInclude(m => m.CentralPolicyDateProvince)

                .Include(m => m.SubjectGroup)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubquestionChoiceCentralPolicyProvinces)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
                .ThenInclude(m => m.ProvincialDepartment)
                .Include(m => m.SubjectCentralPolicyProvinceFiles)
                .Where(m => m.Id == id)
                .First();

            return Ok(subjectdata);
        }
        [HttpPost("reportsubject")]
        public IActionResult CreateReport2([FromBody] reportsubject model)
        {

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "DOC" + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            //if (model.reporttype == "รายหน่วยงาน")
            //{
            using (DocX document = DocX.Create(createfile))
            {
                System.Console.WriteLine("4");
                //Image image = document.AddImage(myImageFullPath);
                //Picture picture = image.CreatePicture(85, 85);
                //var logo = document.InsertParagraph();
                //logo.AppendPicture(picture).Alignment = Alignment.center;


                System.Console.WriteLine("5");



                foreach (var subjectdata in model.Subject)
                {
                    // Add a title
                    document.InsertParagraph("แบบประเด็น" + model.type)
                        .FontSize(16d)
                        .SpacingBefore(15d)
                        .SpacingAfter(15d)
                        .Bold()
                        .Alignment = Alignment.center;

                    System.Console.WriteLine("6");
                    // Insert a title paragraph.
                    var year = document.InsertParagraph("ประจำปีงบประมาณ พ.ศ." + model.fiscalyear);
                    year.Alignment = Alignment.center;
                    year.SpacingAfter(5d);
                    year.FontSize(16d);


                    var region = document.InsertParagraph("ประเด็นนโยบายสำคัญ : " + model.title);
                    region.Alignment = Alignment.center;
                    region.SpacingAfter(5d);
                    region.FontSize(16d);
                    //region.Bold();
                    System.Console.WriteLine("7");
                    var region2 = document.InsertParagraph("เรื่อง : " + subjectdata.name);
                    region2.Alignment = Alignment.center;
                    region2.SpacingAfter(30d);
                    region2.FontSize(16d);

                    var region3 = document.InsertParagraph("ผู้ให้ข้อมูล");
                    //region2.Alignment = Alignment.center;
                    region3.SpacingAfter(10d);
                    region3.FontSize(16d);

                    for (int i = 0; i < subjectdata.provincialDepartment.Length; i++)
                    {
                        var region33 = document.InsertParagraph(i + 1 + "." + subjectdata.provincialDepartment[i]);
                        //region2.Alignment = Alignment.center;
                        region33.SpacingAfter(10d);
                        region33.FontSize(16d);
                    }


                    var region4 = document.InsertParagraph("คำชี้แจง : " + subjectdata.explanation);
                    //region2.Alignment = Alignment.center;
                    region4.SpacingAfter(10d);
                    region4.FontSize(16d);

                    var region5 = document.InsertParagraph("ประเด็นการตรวจติดตาม");
                    //region2.Alignment = Alignment.center;
                    region5.SpacingAfter(10d);
                    region5.FontSize(16d);


                    for (int i = 0; i < subjectdata.namesubquestion.Length; i++)
                    {
                        var region6 = document.InsertParagraph(i + 1 + "." + subjectdata.namesubquestion[i]);
                        //region2.Alignment = Alignment.center;
                        region6.SpacingAfter(10d);
                        region6.FontSize(16d);

                    }
                    var region7 = document.InsertParagraph("");
                    //region2.Alignment = Alignment.center;
                    region7.SpacingAfter(10d);
                    region7.FontSize(16d);
                    region7.InsertPageBreakAfterSelf();
                }

                //int dataCount = 0;
                ////dataCount = model.reportData.Length;
                ////dataCount += 1;
                //System.Console.WriteLine("Data Count: " + dataCount);
                //// Add a table in a document of 1 row and 3 columns.
                //var columnWidths = new float[] { 300f, 300f, 300f, 300f };
                //var t = document.InsertTable(5, columnWidths.Length);

                //System.Console.WriteLine("8");

                //// Set the table's column width and background 
                //t.SetWidths(columnWidths);
                //t.AutoFit = AutoFit.Contents;

                //var row = t.Rows.First();

                //// Fill in the columns of the first row in the table.
                ////for (int i = 0; i < row.Cells.Count; ++i)
                ////{
                //row.Cells[0].Paragraphs.First().Append("จังหวัด");
                //row.Cells[1].Paragraphs.First().Append("ผต.นร./ผต.กท. (เจ้าของเรื่อง)");
                //row.Cells[2].Paragraphs.First().Append("ข้อเสนอแนะของ ผต.");
                //row.Cells[3].Paragraphs.First().Append("หน่วยงานที่เกี่ยวข้อง (หน่วยรับดำเนินการ)");


                //System.Console.WriteLine("9999: ");
                //System.Console.WriteLine("9: ");

                //}
                // Add rows in the table.
                //int j = 0;
                //for (int i = 0; i < model.reportData.Length; i++)
                //{
                //    j += 1;
                //    //System.Console.WriteLine(i+=1);

                //    System.Console.WriteLine("JJJJJ: " + j);
                //    System.Console.WriteLine("9.1: " + model.reportData[i].subject);
                //    t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData[i].subject);
                //    System.Console.WriteLine("9.2: " + model.reportData[i].detail);
                //    t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData[i].detail);
                //    System.Console.WriteLine("9.3: " + model.reportData[i].suggestion);
                //    t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData[i].suggestion);
                //    System.Console.WriteLine("9.4: " + model.reportData[i].problem);
                //    t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData[i].problem);
                //    System.Console.WriteLine("9.5: " + model.reportData[i].department);
                //    t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData[i].department);
                //    System.Console.WriteLine("9.6: " + model.reportData[i].opinionPeople);
                //    t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData[i].opinionPeople);
                //    System.Console.WriteLine("10");
                //}

                // Set a blank border for the table's top/bottom borders.
                //var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                System.Console.WriteLine("11");
                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }
            //}
            return Ok(new { data = filename });
        }
        [HttpPost("reportperformance")]
        public IActionResult CreateReport([FromForm] reportperformance model)
        {
            var provincedata = _context.Provinces
                .Where(m => m.Id == model.provinceid)
                .FirstOrDefault();

            System.Console.WriteLine("1" + provincedata.Name);
            var centralpolicydata = _context.CentralPolicies
                .Where(m => m.Id == model.centralpolicyid)
                .FirstOrDefault();

            System.Console.WriteLine("2" + centralpolicydata.Title);
            var subjectdata = _context.SubjectCentralPolicyProvinces
                .Where(m => m.Id == model.subjectid)
                .FirstOrDefault();
            System.Console.WriteLine("3" + subjectdata.Name);

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "DOC" + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            if (model.reporttype == "รายหน่วยงาน")
            {
                using (DocX document = DocX.Create(createfile))
                {
                    System.Console.WriteLine("4");
                    //Image image = document.AddImage(myImageFullPath);
                    //Picture picture = image.CreatePicture(85, 85);
                    //var logo = document.InsertParagraph();
                    //logo.AppendPicture(picture).Alignment = Alignment.center;


                    System.Console.WriteLine("5");

                    // Add a title
                    document.InsertParagraph("รายงานผลการดำเนินการของหน่วยรับตรวจ " + model.reporttype)
                        .FontSize(16d)
                        .SpacingBefore(15d)
                        .SpacingAfter(15d)
                        .Bold()
                        .Alignment = Alignment.center;

                    System.Console.WriteLine("6");
                    // Insert a title paragraph.
                    var year = document.InsertParagraph("รอบการตรวจราชการที่....................");
                    year.Alignment = Alignment.center;
                    year.SpacingAfter(10d);
                    year.FontSize(16d);

                    var inspector = document.InsertParagraph("หน่วยงาน.................... เขต.................... จังหวัด : " + provincedata.Name);
                    inspector.Alignment = Alignment.center;
                    inspector.SpacingAfter(30d);
                    inspector.FontSize(16d);


                    var region = document.InsertParagraph("หัวข้อการตรวจติดตาม : " + centralpolicydata.Title);
                    //region.Alignment = Alignment.center;
                    region.SpacingAfter(30d);
                    region.FontSize(16d);
                    region.Bold();

                    var region2 = document.InsertParagraph("ประเด็นการตรวจติดตาม : " + subjectdata.Name);
                    //region2.Alignment = Alignment.center;
                    region2.SpacingAfter(30d);
                    region2.FontSize(16d);
                    region2.Bold();

                    var region3 = document.InsertParagraph("ผลการดำเนินการของหน่วยงาน....................");
                    //region3.Alignment = Alignment.center;
                    region3.SpacingAfter(30d);
                    region3.FontSize(16d);
                    region3.Bold();

                    var region4 = document.InsertParagraph("เอกสารแนบ/ภาพ....................");
                    //region4.Alignment = Alignment.center;
                    region4.SpacingAfter(30d);
                    region4.FontSize(16d);
                    region4.Bold();



                    //System.Console.WriteLine("7");

                    //int dataCount = 0;
                    ////dataCount = model.reportData.Length;
                    ////dataCount += 1;
                    //System.Console.WriteLine("Data Count: " + dataCount);
                    //// Add a table in a document of 1 row and 3 columns.
                    //var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f, 300f };
                    //var t = document.InsertTable(5, columnWidths.Length);

                    //System.Console.WriteLine("8");

                    //// Set the table's column width and background 
                    //t.SetWidths(columnWidths);
                    //t.AutoFit = AutoFit.Contents;

                    //var row = t.Rows.First();

                    //// Fill in the columns of the first row in the table.
                    ////for (int i = 0; i < row.Cells.Count; ++i)
                    ////{
                    //row.Cells[0].Paragraphs.First().Append("ประเด็นการตรวจติดตาม");
                    //row.Cells[1].Paragraphs.First().Append("ข้อค้นพบ/ผลการตรวจ");
                    //row.Cells[2].Paragraphs.First().Append("ประเด็นปัญหา");
                    //row.Cells[3].Paragraphs.First().Append("ข้อเสนอแนะของผู้ตรวจราชการ");
                    //row.Cells[4].Paragraphs.First().Append("หน่วยรับตรวจ/หน่วยงานที่รับผิดชอบ");
                    //row.Cells[5].Paragraphs.First().Append("ความเห็นของ ที่ปรึกษา ผต.ภาคประชาชน");

                    //System.Console.WriteLine("9999: ");
                    //System.Console.WriteLine("9: ");

                    //}
                    // Add rows in the table.
                    //int j = 0;
                    //for (int i = 0; i < model.reportData.Length; i++)
                    //{
                    //    j += 1;
                    //    //System.Console.WriteLine(i+=1);

                    //    System.Console.WriteLine("JJJJJ: " + j);
                    //    System.Console.WriteLine("9.1: " + model.reportData[i].subject);
                    //    t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData[i].subject);
                    //    System.Console.WriteLine("9.2: " + model.reportData[i].detail);
                    //    t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData[i].detail);
                    //    System.Console.WriteLine("9.3: " + model.reportData[i].suggestion);
                    //    t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData[i].suggestion);
                    //    System.Console.WriteLine("9.4: " + model.reportData[i].problem);
                    //    t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData[i].problem);
                    //    System.Console.WriteLine("9.5: " + model.reportData[i].department);
                    //    t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData[i].department);
                    //    System.Console.WriteLine("9.6: " + model.reportData[i].opinionPeople);
                    //    t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData[i].opinionPeople);
                    //    System.Console.WriteLine("10");
                    //}

                    // Set a blank border for the table's top/bottom borders.
                    var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                    //t.SetBorder(TableBorderType.Bottom, blankBorder);
                    //t.SetBorder(TableBorderType.Top, blankBorder);

                    System.Console.WriteLine("11");
                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }
            if (model.reporttype == "รายจังหวัด")
            {
                using (DocX document = DocX.Create(createfile))
                {
                    System.Console.WriteLine("4");
                    //Image image = document.AddImage(myImageFullPath);
                    //Picture picture = image.CreatePicture(85, 85);
                    //var logo = document.InsertParagraph();
                    //logo.AppendPicture(picture).Alignment = Alignment.center;


                    System.Console.WriteLine("5");

                    // Add a title
                    document.InsertParagraph("รายงานผลการดำเนินการของหน่วยรับตรวจ " + model.reporttype)
                        .FontSize(16d)
                        .SpacingBefore(15d)
                        .SpacingAfter(15d)
                        .Bold()
                        .Alignment = Alignment.center;

                    System.Console.WriteLine("6");
                    // Insert a title paragraph.
                    var year = document.InsertParagraph("รอบการตรวจราชการที่....................");
                    year.Alignment = Alignment.center;
                    year.SpacingAfter(10d);
                    year.FontSize(16d);

                    var inspector = document.InsertParagraph("เขต.................... จังหวัด : " + provincedata.Name);
                    inspector.Alignment = Alignment.center;
                    inspector.SpacingAfter(30d);
                    inspector.FontSize(16d);


                    var region = document.InsertParagraph("หัวข้อการตรวจติดตาม : " + centralpolicydata.Title);
                    //region.Alignment = Alignment.center;
                    region.SpacingAfter(30d);
                    region.FontSize(16d);
                    region.Bold();

                    var region2 = document.InsertParagraph("ประเด็นการตรวจติดตาม : " + subjectdata.Name);
                    //region2.Alignment = Alignment.center;
                    region2.SpacingAfter(30d);
                    region2.FontSize(16d);
                    region2.Bold();

                    System.Console.WriteLine("7");

                    int dataCount = 0;
                    //dataCount = model.reportData.Length;
                    //dataCount += 1;
                    System.Console.WriteLine("Data Count: " + dataCount);
                    // Add a table in a document of 1 row and 3 columns.
                    var columnWidths = new float[] { 300f, 300f, 300f };
                    var t = document.InsertTable(5, columnWidths.Length);

                    System.Console.WriteLine("8");

                    // Set the table's column width and background 
                    t.SetWidths(columnWidths);
                    t.AutoFit = AutoFit.Contents;

                    var row = t.Rows.First();

                    // Fill in the columns of the first row in the table.
                    //for (int i = 0; i < row.Cells.Count; ++i)
                    //{
                    row.Cells[0].Paragraphs.First().Append("หน่วยรับตรวจ");
                    row.Cells[1].Paragraphs.First().Append("ผลการดำเนินงาน");
                    row.Cells[2].Paragraphs.First().Append("เอกสารแนบ/ภาพ*");
                    //row.Cells[3].Paragraphs.First().Append("ข้อเสนอแนะของผู้ตรวจราชการ");
                    //row.Cells[4].Paragraphs.First().Append("หน่วยรับตรวจ/หน่วยงานที่รับผิดชอบ");
                    //row.Cells[5].Paragraphs.First().Append("ความเห็นของ ที่ปรึกษา ผต.ภาคประชาชน");

                    //System.Console.WriteLine("9999: ");
                    //System.Console.WriteLine("9: ");

                    //}
                    // Add rows in the table.
                    //int j = 0;
                    //for (int i = 0; i < model.reportData.Length; i++)
                    //{
                    //    j += 1;
                    //    //System.Console.WriteLine(i+=1);

                    //    System.Console.WriteLine("JJJJJ: " + j);
                    //    System.Console.WriteLine("9.1: " + model.reportData[i].subject);
                    //    t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData[i].subject);
                    //    System.Console.WriteLine("9.2: " + model.reportData[i].detail);
                    //    t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData[i].detail);
                    //    System.Console.WriteLine("9.3: " + model.reportData[i].suggestion);
                    //    t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData[i].suggestion);
                    //    System.Console.WriteLine("9.4: " + model.reportData[i].problem);
                    //    t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData[i].problem);
                    //    System.Console.WriteLine("9.5: " + model.reportData[i].department);
                    //    t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData[i].department);
                    //    System.Console.WriteLine("9.6: " + model.reportData[i].opinionPeople);
                    //    t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData[i].opinionPeople);
                    //    System.Console.WriteLine("10");
                    //}

                    // Set a blank border for the table's top/bottom borders.
                    //var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                    //t.SetBorder(TableBorderType.Bottom, blankBorder);
                    //t.SetBorder(TableBorderType.Top, blankBorder);

                    System.Console.WriteLine("11");
                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }
            if (model.reporttype == "รายเขต")
            {
                using (DocX document = DocX.Create(createfile))
                {
                    System.Console.WriteLine("4");
                    //Image image = document.AddImage(myImageFullPath);
                    //Picture picture = image.CreatePicture(85, 85);
                    //var logo = document.InsertParagraph();
                    //logo.AppendPicture(picture).Alignment = Alignment.center;


                    System.Console.WriteLine("5");

                    // Add a title
                    document.InsertParagraph("รายงานผลการดำเนินการของหน่วยรับตรวจ " + model.reporttype)
                        .FontSize(16d)
                        .SpacingBefore(15d)
                        .SpacingAfter(15d)
                        .Bold()
                        .Alignment = Alignment.center;

                    System.Console.WriteLine("6");
                    // Insert a title paragraph.
                    var year = document.InsertParagraph("รอบการตรวจราชการที่.................... เขต....................");
                    year.Alignment = Alignment.center;
                    year.SpacingAfter(10d);
                    year.FontSize(16d);

                    var region = document.InsertParagraph("หัวข้อการตรวจติดตาม : " + centralpolicydata.Title);
                    //region.Alignment = Alignment.center;
                    region.SpacingAfter(30d);
                    region.FontSize(16d);
                    region.Bold();

                    var region2 = document.InsertParagraph("ประเด็นการตรวจติดตาม : " + subjectdata.Name);
                    //region2.Alignment = Alignment.center;
                    region2.SpacingAfter(30d);
                    region2.FontSize(16d);
                    region2.Bold();

                    System.Console.WriteLine("7");

                    int dataCount = 0;
                    //dataCount = model.reportData.Length;
                    //dataCount += 1;
                    System.Console.WriteLine("Data Count: " + dataCount);
                    // Add a table in a document of 1 row and 3 columns.
                    var columnWidths = new float[] { 300f, 300f, 300f };
                    var t = document.InsertTable(5, columnWidths.Length);

                    System.Console.WriteLine("8");

                    // Set the table's column width and background 
                    t.SetWidths(columnWidths);
                    t.AutoFit = AutoFit.Contents;

                    var row = t.Rows.First();

                    // Fill in the columns of the first row in the table.
                    //for (int i = 0; i < row.Cells.Count; ++i)
                    //{
                    row.Cells[0].Paragraphs.First().Append("จังหวัด");
                    row.Cells[1].Paragraphs.First().Append("หน่วยรับตรวจ");
                    row.Cells[2].Paragraphs.First().Append("ผลการดำเนินงาน");
                    row.Cells[3].Paragraphs.First().Append("เอกสารแนบ/ภาพ*");
                    //row.Cells[3].Paragraphs.First().Append("ข้อเสนอแนะของผู้ตรวจราชการ");
                    //row.Cells[4].Paragraphs.First().Append("หน่วยรับตรวจ/หน่วยงานที่รับผิดชอบ");
                    //row.Cells[5].Paragraphs.First().Append("ความเห็นของ ที่ปรึกษา ผต.ภาคประชาชน");

                    //System.Console.WriteLine("9999: ");
                    //System.Console.WriteLine("9: ");

                    //}
                    // Add rows in the table.
                    //int j = 0;
                    //for (int i = 0; i < model.reportData.Length; i++)
                    //{
                    //    j += 1;
                    //    //System.Console.WriteLine(i+=1);

                    //    System.Console.WriteLine("JJJJJ: " + j);
                    //    System.Console.WriteLine("9.1: " + model.reportData[i].subject);
                    //    t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData[i].subject);
                    //    System.Console.WriteLine("9.2: " + model.reportData[i].detail);
                    //    t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData[i].detail);
                    //    System.Console.WriteLine("9.3: " + model.reportData[i].suggestion);
                    //    t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData[i].suggestion);
                    //    System.Console.WriteLine("9.4: " + model.reportData[i].problem);
                    //    t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData[i].problem);
                    //    System.Console.WriteLine("9.5: " + model.reportData[i].department);
                    //    t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData[i].department);
                    //    System.Console.WriteLine("9.6: " + model.reportData[i].opinionPeople);
                    //    t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData[i].opinionPeople);
                    //    System.Console.WriteLine("10");
                    //}

                    // Set a blank border for the table's top/bottom borders.
                    //var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                    //t.SetBorder(TableBorderType.Bottom, blankBorder);
                    //t.SetBorder(TableBorderType.Top, blankBorder);

                    System.Console.WriteLine("11");
                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }
            return Ok(new { data = filename });
        }
        [HttpGet("reportsuggestions")]
        public IActionResult CreateReport2()
        {
            //var provincedata = _context.Provinces
            //    .Where(m => m.Id == model.provinceid)
            //    .FirstOrDefault();

            //System.Console.WriteLine("1" + provincedata.Name);
            //var centralpolicydata = _context.CentralPolicies
            //    .Where(m => m.Id == model.centralpolicyid)
            //    .FirstOrDefault();

            //System.Console.WriteLine("2" + centralpolicydata.Title);
            //var subjectdata = _context.SubjectCentralPolicyProvinces
            //    .Where(m => m.Id == model.subjectid)
            //    .FirstOrDefault();
            //System.Console.WriteLine("3" + subjectdata.Name);

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "DOC" + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            //if (model.reporttype == "รายหน่วยงาน")
            //{
            using (DocX document = DocX.Create(createfile))
            {
                System.Console.WriteLine("4");
                //Image image = document.AddImage(myImageFullPath);
                //Picture picture = image.CreatePicture(85, 85);
                //var logo = document.InsertParagraph();
                //logo.AppendPicture(picture).Alignment = Alignment.center;


                System.Console.WriteLine("5");

                // Add a title
                document.InsertParagraph("รายงานแบบข้อเสนอแนะของผู้ตรวจราชการ (รายประเด็น)")
                    .FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold()
                    .Alignment = Alignment.center;

                System.Console.WriteLine("6");
                // Insert a title paragraph.
                var year = document.InsertParagraph("รอบการตรวจราชการที่....................เดือน....................ปี....................");
                year.Alignment = Alignment.center;
                year.SpacingAfter(10d);
                year.FontSize(16d);

                //var inspector = document.InsertParagraph("หน่วยงาน.................... เขต.................... จังหวัด : ");
                //inspector.Alignment = Alignment.center;
                //inspector.SpacingAfter(30d);
                //inspector.FontSize(16d);


                var region = document.InsertParagraph("หัวข้อการตรวจติดตาม : ");
                //region.Alignment = Alignment.center;
                region.SpacingAfter(30d);
                region.FontSize(16d);
                region.Bold();

                var region2 = document.InsertParagraph("ประเด็นการตรวจติดตาม : ");
                //region2.Alignment = Alignment.center;
                region2.SpacingAfter(30d);
                region2.FontSize(16d);
                region2.Bold();


                var region4 = document.InsertParagraph("ข้อเสนอแนะระดับนโยบาย");
                //region4.Alignment = Alignment.center;
                region4.SpacingAfter(30d);
                region4.FontSize(16d);
                region4.Bold();
                region4.UnderlineStyle(UnderlineStyle.dash);



                int dataCount = 0;
                //dataCount = model.reportData.Length;
                //dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 300f, 300f, 300f, 300f };
                var t = document.InsertTable(5, columnWidths.Length);

                System.Console.WriteLine("8");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row.Cells[0].Paragraphs.First().Append("จังหวัด");
                row.Cells[1].Paragraphs.First().Append("ผต.นร./ผต.กท. (เจ้าของเรื่อง)");
                row.Cells[2].Paragraphs.First().Append("ข้อเสนอแนะของ ผต.");
                row.Cells[3].Paragraphs.First().Append("หน่วยงานที่เกี่ยวข้อง (หน่วยรับดำเนินการ)");


                //System.Console.WriteLine("9999: ");
                //System.Console.WriteLine("9: ");

                //}
                // Add rows in the table.
                //int j = 0;
                //for (int i = 0; i < model.reportData.Length; i++)
                //{
                //    j += 1;
                //    //System.Console.WriteLine(i+=1);

                //    System.Console.WriteLine("JJJJJ: " + j);
                //    System.Console.WriteLine("9.1: " + model.reportData[i].subject);
                //    t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData[i].subject);
                //    System.Console.WriteLine("9.2: " + model.reportData[i].detail);
                //    t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData[i].detail);
                //    System.Console.WriteLine("9.3: " + model.reportData[i].suggestion);
                //    t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData[i].suggestion);
                //    System.Console.WriteLine("9.4: " + model.reportData[i].problem);
                //    t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData[i].problem);
                //    System.Console.WriteLine("9.5: " + model.reportData[i].department);
                //    t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData[i].department);
                //    System.Console.WriteLine("9.6: " + model.reportData[i].opinionPeople);
                //    t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData[i].opinionPeople);
                //    System.Console.WriteLine("10");
                //}

                // Set a blank border for the table's top/bottom borders.
                //var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                System.Console.WriteLine("11");
                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }
            //}
            return Ok(new { data = filename });
        }
        [HttpGet("reportsuggestionsresult")]
        public IActionResult CreateReport3()
        {
            //var provincedata = _context.Provinces
            //    .Where(m => m.Id == model.provinceid)
            //    .FirstOrDefault();

            //System.Console.WriteLine("1" + provincedata.Name);
            //var centralpolicydata = _context.CentralPolicies
            //    .Where(m => m.Id == model.centralpolicyid)
            //    .FirstOrDefault();

            //System.Console.WriteLine("2" + centralpolicydata.Title);
            //var subjectdata = _context.SubjectCentralPolicyProvinces
            //    .Where(m => m.Id == model.subjectid)
            //    .FirstOrDefault();
            //System.Console.WriteLine("3" + subjectdata.Name);

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "DOC" + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            //if (model.reporttype == "รายหน่วยงาน")
            //{
            using (DocX document = DocX.Create(createfile))
            {
                System.Console.WriteLine("4");
                //Image image = document.AddImage(myImageFullPath);
                //Picture picture = image.CreatePicture(85, 85);
                //var logo = document.InsertParagraph();
                //logo.AppendPicture(picture).Alignment = Alignment.center;


                System.Console.WriteLine("5");

                // Add a title
                document.InsertParagraph("รายงานผลการดำเนินการตามข้อเสนอแนะของผู้ตรวจราชการ")
                    .FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold()
                    .Alignment = Alignment.center;

                System.Console.WriteLine("6");
                // Insert a title paragraph.
                var year = document.InsertParagraph("รอบการตรวจราชการที่....................เดือน....................ปี....................");
                year.Alignment = Alignment.center;
                year.SpacingAfter(10d);
                year.FontSize(16d);

                var inspector = document.InsertParagraph("หน่วยงาน....................จังหวัด : ");
                inspector.Alignment = Alignment.center;
                inspector.SpacingAfter(30d);
                inspector.FontSize(16d);


                var region = document.InsertParagraph("หัวข้อการตรวจติดตาม : ");
                //region.Alignment = Alignment.center;
                region.SpacingAfter(30d);
                region.FontSize(16d);
                region.Bold();

                var region2 = document.InsertParagraph("ประเด็นการตรวจติดตาม : ");
                //region2.Alignment = Alignment.center;
                region2.SpacingAfter(30d);
                region2.FontSize(16d);
                region2.Bold();


                var region4 = document.InsertParagraph("ข้อเสนอแนะระดับพื้นที่");
                //region4.Alignment = Alignment.center;
                region4.SpacingAfter(30d);
                region4.FontSize(16d);
                region4.Bold();
                region4.UnderlineStyle(UnderlineStyle.singleLine);



                int dataCount = 0;
                //dataCount = model.reportData.Length;
                //dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f };
                var t = document.InsertTable(5, columnWidths.Length);

                System.Console.WriteLine("8");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row.Cells[0].Paragraphs.First().Append("ลำดับที่");
                row.Cells[1].Paragraphs.First().Append("ผต.นร./ผต.กท. (เจ้าของเรื่อง)");
                row.Cells[2].Paragraphs.First().Append("ข้อเสนอแนะของ ผต.");
                row.Cells[3].Paragraphs.First().Append("การดำเนินงานของหน่วยงาน");
                row.Cells[4].Paragraphs.First().Append("เอกสารแนบ (เป็น Link ให้อ่านเพิ่มเติม)");


                //System.Console.WriteLine("9999: ");
                //System.Console.WriteLine("9: ");

                //}
                // Add rows in the table.
                //int j = 0;
                //for (int i = 0; i < model.reportData.Length; i++)
                //{
                //    j += 1;
                //    //System.Console.WriteLine(i+=1);

                //    System.Console.WriteLine("JJJJJ: " + j);
                //    System.Console.WriteLine("9.1: " + model.reportData[i].subject);
                //    t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData[i].subject);
                //    System.Console.WriteLine("9.2: " + model.reportData[i].detail);
                //    t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData[i].detail);
                //    System.Console.WriteLine("9.3: " + model.reportData[i].suggestion);
                //    t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData[i].suggestion);
                //    System.Console.WriteLine("9.4: " + model.reportData[i].problem);
                //    t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData[i].problem);
                //    System.Console.WriteLine("9.5: " + model.reportData[i].department);
                //    t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData[i].department);
                //    System.Console.WriteLine("9.6: " + model.reportData[i].opinionPeople);
                //    t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData[i].opinionPeople);
                //    System.Console.WriteLine("10");
                //}

                // Set a blank border for the table's top/bottom borders.
                //var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                var region11 = document.InsertParagraph("หัวข้อการตรวจติดตาม : ");
                //region.Alignment = Alignment.center;
                region11.SpacingAfter(30d);
                region11.FontSize(16d);
                region11.Bold();

                var region22 = document.InsertParagraph("ประเด็นการตรวจติดตาม : ");
                //region2.Alignment = Alignment.center;
                region22.SpacingAfter(30d);
                region22.FontSize(16d);
                region22.Bold();


                var region33 = document.InsertParagraph("ข้อเสนอแนะระดับนโยบาย");
                //region4.Alignment = Alignment.center;
                region33.SpacingAfter(30d);
                region33.FontSize(16d);
                region33.Bold();
                region33.UnderlineStyle(UnderlineStyle.singleLine);

                int dataCount2 = 0;
                //dataCount = model.reportData.Length;
                //dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths2 = new float[] { 300f, 300f, 300f, 300f, 300f };
                var t2 = document.InsertTable(5, columnWidths.Length);

                System.Console.WriteLine("8");

                // Set the table's column width and background 
                t2.SetWidths(columnWidths);
                t2.AutoFit = AutoFit.Contents;

                var row2 = t2.Rows.First();

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row2.Cells[0].Paragraphs.First().Append("ลำดับที่");
                row2.Cells[1].Paragraphs.First().Append("ผต.นร./ผต.กท. (เจ้าของเรื่อง)");
                row2.Cells[2].Paragraphs.First().Append("ข้อเสนอแนะของ ผต.");
                row2.Cells[3].Paragraphs.First().Append("การดำเนินงานของหน่วยงาน");
                row2.Cells[4].Paragraphs.First().Append("เอกสารแนบ (เป็น Link ให้อ่านเพิ่มเติม)");


                System.Console.WriteLine("11");
                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }
            //}
            return Ok(new { data = filename });
        }
        [HttpGet("reportquestionnaire")]
        public IActionResult CreateReport4()
        {
            //var provincedata = _context.Provinces
            //    .Where(m => m.Id == model.provinceid)
            //    .FirstOrDefault();

            //System.Console.WriteLine("1" + provincedata.Name);
            //var centralpolicydata = _context.CentralPolicies
            //    .Where(m => m.Id == model.centralpolicyid)
            //    .FirstOrDefault();

            //System.Console.WriteLine("2" + centralpolicydata.Title);
            //var subjectdata = _context.SubjectCentralPolicyProvinces
            //    .Where(m => m.Id == model.subjectid)
            //    .FirstOrDefault();
            //System.Console.WriteLine("3" + subjectdata.Name);

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "DOC" + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            //if (model.reporttype == "รายหน่วยงาน")
            //{
            using (DocX document = DocX.Create(createfile))
            {
                System.Console.WriteLine("4");
                //Image image = document.AddImage(myImageFullPath);
                //Picture picture = image.CreatePicture(85, 85);
                //var logo = document.InsertParagraph();
                //logo.AppendPicture(picture).Alignment = Alignment.center;


                System.Console.WriteLine("5");

                // Add a title
                document.InsertParagraph("รายงานแบบสอบถามความคิดเห็นของที่ปรึกษาผู้ตรวจราชการภาคประชาชน")
                    .FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold()
                    .Alignment = Alignment.center;

                System.Console.WriteLine("6");
                // Insert a title paragraph.
                var year = document.InsertParagraph("รอบการตรวจราชการที่....................เดือน....................ปี....................");
                year.Alignment = Alignment.center;
                year.SpacingAfter(10d);
                year.FontSize(16d);

                int dataCount = 0;
                //dataCount = model.reportData.Length;
                //dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f, 300f };
                var t = document.InsertTable(5, columnWidths.Length);

                System.Console.WriteLine("8");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row.Cells[0].Paragraphs.First().Append("ลำดับที่");
                row.Cells[1].Paragraphs.First().Append("หัวข้อการตรวจติดตาม");
                row.Cells[2].Paragraphs.First().Append("ประเด็นการตรวจติดตาม");
                row.Cells[3].Paragraphs.First().Append("หน่วยงานเจ้าของเรื่อง/ผต.นร./ผต.กท.");
                row.Cells[4].Paragraphs.First().Append("จังหวัด");
                row.Cells[5].Paragraphs.First().Append("ที่ปรึกษาที่ได้รับแบบสอบถามฯ");


                //System.Console.WriteLine("9999: ");
                //System.Console.WriteLine("9: ");

                //}
                // Add rows in the table.
                //int j = 0;
                //for (int i = 0; i < model.reportData.Length; i++)
                //{
                //    j += 1;
                //    //System.Console.WriteLine(i+=1);

                //    System.Console.WriteLine("JJJJJ: " + j);
                //    System.Console.WriteLine("9.1: " + model.reportData[i].subject);
                //    t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData[i].subject);
                //    System.Console.WriteLine("9.2: " + model.reportData[i].detail);
                //    t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData[i].detail);
                //    System.Console.WriteLine("9.3: " + model.reportData[i].suggestion);
                //    t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData[i].suggestion);
                //    System.Console.WriteLine("9.4: " + model.reportData[i].problem);
                //    t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData[i].problem);
                //    System.Console.WriteLine("9.5: " + model.reportData[i].department);
                //    t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData[i].department);
                //    System.Console.WriteLine("9.6: " + model.reportData[i].opinionPeople);
                //    t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData[i].opinionPeople);
                //    System.Console.WriteLine("10");
                //}

                // Set a blank border for the table's top/bottom borders.
                //var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);


                System.Console.WriteLine("11");
                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }
            //}
            return Ok(new { data = filename });
        }
        [HttpGet("reportcomment")]
        public IActionResult CreateReport5()
        {
            //var provincedata = _context.Provinces
            //    .Where(m => m.Id == model.provinceid)
            //    .FirstOrDefault();

            //System.Console.WriteLine("1" + provincedata.Name);
            //var centralpolicydata = _context.CentralPolicies
            //    .Where(m => m.Id == model.centralpolicyid)
            //    .FirstOrDefault();

            //System.Console.WriteLine("2" + centralpolicydata.Title);
            //var subjectdata = _context.SubjectCentralPolicyProvinces
            //    .Where(m => m.Id == model.subjectid)
            //    .FirstOrDefault();
            //System.Console.WriteLine("3" + subjectdata.Name);

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "DOC" + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            //if (model.reporttype == "รายหน่วยงาน")
            //{
            using (DocX document = DocX.Create(createfile))
            {
                System.Console.WriteLine("4");
                //Image image = document.AddImage(myImageFullPath);
                //Picture picture = image.CreatePicture(85, 85);
                //var logo = document.InsertParagraph();
                //logo.AppendPicture(picture).Alignment = Alignment.center;


                System.Console.WriteLine("5");

                // Add a title
                document.InsertParagraph("รายงานความคิดเห็นของที่ปรึกษาผู้ตรวจราชการภาคประชาชน")
                    .FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold()
                    .Alignment = Alignment.center;

                System.Console.WriteLine("6");
                // Insert a title paragraph.
                var year = document.InsertParagraph("รอบการตรวจราชการที่....................เดือน....................ปี....................");
                year.Alignment = Alignment.center;
                year.SpacingAfter(10d);
                year.FontSize(16d);

                var inspector = document.InsertParagraph("ชื่อที่ปรึกษาฯ....................เขต....................จังหวัด....................");
                inspector.Alignment = Alignment.center;
                inspector.SpacingAfter(30d);
                inspector.FontSize(16d);

                int dataCount = 0;
                //dataCount = model.reportData.Length;
                //dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f };
                var t = document.InsertTable(5, columnWidths.Length);

                System.Console.WriteLine("8");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row.Cells[0].Paragraphs.First().Append("ลำดับที่");
                row.Cells[1].Paragraphs.First().Append("หน่วยงานเจ้าของเรื่อง/ผต.นร./ผต.กท.");
                row.Cells[2].Paragraphs.First().Append("หัวข้อการตรวจติดตาม");
                row.Cells[3].Paragraphs.First().Append("ประเด็นการตรวจติดตาม");
                row.Cells[4].Paragraphs.First().Append("ความเห็นที่ปรึกษาฯ");



                //System.Console.WriteLine("9999: ");
                //System.Console.WriteLine("9: ");

                //}
                // Add rows in the table.
                //int j = 0;
                //for (int i = 0; i < model.reportData.Length; i++)
                //{
                //    j += 1;
                //    //System.Console.WriteLine(i+=1);

                //    System.Console.WriteLine("JJJJJ: " + j);
                //    System.Console.WriteLine("9.1: " + model.reportData[i].subject);
                //    t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData[i].subject);
                //    System.Console.WriteLine("9.2: " + model.reportData[i].detail);
                //    t.Rows[j].Cells[1].Paragraphs[0].Append(model.reportData[i].detail);
                //    System.Console.WriteLine("9.3: " + model.reportData[i].suggestion);
                //    t.Rows[j].Cells[2].Paragraphs[0].Append(model.reportData[i].suggestion);
                //    System.Console.WriteLine("9.4: " + model.reportData[i].problem);
                //    t.Rows[j].Cells[3].Paragraphs[0].Append(model.reportData[i].problem);
                //    System.Console.WriteLine("9.5: " + model.reportData[i].department);
                //    t.Rows[j].Cells[4].Paragraphs[0].Append(model.reportData[i].department);
                //    System.Console.WriteLine("9.6: " + model.reportData[i].opinionPeople);
                //    t.Rows[j].Cells[5].Paragraphs[0].Append(model.reportData[i].opinionPeople);
                //    System.Console.WriteLine("10");
                //}

                // Set a blank border for the table's top/bottom borders.
                //var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);


                System.Console.WriteLine("11");
                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }
            //}
            return Ok(new { data = filename });
        }
    }
}
