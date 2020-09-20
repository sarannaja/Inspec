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
            System.Console.WriteLine("in create");
            if (model.reporttype == "1")
            {
                System.Console.WriteLine("type1");
                var type = "(รายหน่วยงาน)";
                var answerdata = _context.AnswerSubquestions
                    .Include(m => m.SubquestionCentralPolicyProvince)
                    .ThenInclude(m => m.SubjectCentralPolicyProvince)
                    .Include(m => m.User)
                    .ThenInclude(m => m.ProvincialDepartments)
                    .Include(m => m.User)
                    .ThenInclude(m => m.Province)
                    .Where(m => m.User.ProvincialDepartmentId == model.provincialDepartmentId)
                    .ToList();

                var regiondata = _context.FiscalYearRelations
                    .Include(m => m.Region)
                    .Where(m => m.ProvinceId == answerdata[0].User.ProvinceId)
                    .FirstOrDefault();

                using (DocX document = DocX.Create(createfile))
                {
                    System.Console.WriteLine("3");
                    //Image image = document.AddImage(myImageFullPath);
                    //Picture picture = image.CreatePicture(85, 85);
                    //var logo = document.InsertParagraph();
                    //logo.AppendPicture(picture).Alignment = Alignment.center;


                    System.Console.WriteLine("4");
                    for (int i = 0; i < answerdata.Count; i++)
                    {


                        // Add a title
                        document.InsertParagraph("รายงานผลการดำเนินการของหน่วยรับตรวจ " + type)
                            .FontSize(16d)
                            .SpacingBefore(15d)
                            .SpacingAfter(15d)
                            .Bold()
                            .Alignment = Alignment.center;

                        System.Console.WriteLine("5");
                        // Insert a title paragraph.
                        var year = document.InsertParagraph("รอบการตรวจราชการที่....................");
                        year.Alignment = Alignment.center;
                        year.SpacingAfter(10d);
                        year.FontSize(16d);

                        var inspector = document.InsertParagraph("หน่วยงาน : " + answerdata[i].User.ProvincialDepartments.Name + "เขต : " + regiondata.Region.Name + " จังหวัด : " + answerdata[i].User.Province.Name);
                        inspector.Alignment = Alignment.center;
                        inspector.SpacingAfter(30d);
                        inspector.FontSize(16d);

                        var region = document.InsertParagraph("หัวข้อการตรวจติดตาม : " + answerdata[i].SubquestionCentralPolicyProvince.SubjectCentralPolicyProvince.Name);
                        //region.Alignment = Alignment.center;
                        region.SpacingAfter(30d);
                        region.FontSize(16d);
                        region.Bold();

                        var region2 = document.InsertParagraph("ประเด็นการตรวจติดตาม : " + answerdata[i].SubquestionCentralPolicyProvince.Name);
                        //region2.Alignment = Alignment.center;
                        region2.SpacingAfter(30d);
                        region2.FontSize(16d);
                        region2.Bold();

                        var region3 = document.InsertParagraph("ผลการดำเนินการของหน่วยงาน:" + answerdata[i].Answer);
                        //region3.Alignment = Alignment.center;
                        region3.SpacingAfter(30d);
                        region3.FontSize(16d);
                        region3.Bold();

                        var region4 = document.InsertParagraph("เอกสารแนบ/ภาพ....................");
                        //region4.Alignment = Alignment.center;
                        region4.SpacingAfter(30d);
                        region4.FontSize(16d);
                        region4.Bold();
                    }

                    // Set a blank border for the table's top/bottom borders.
                    var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                    //t.SetBorder(TableBorderType.Bottom, blankBorder);
                    //t.SetBorder(TableBorderType.Top, blankBorder);

                    System.Console.WriteLine("6");
                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }
            if (model.reporttype == "2")
            {
                System.Console.WriteLine("type1");
                var type = "(รายจังหวัด)";

                var answerdata = _context.SubjectCentralPolicyProvinces
                 .Include(m => m.SubjectGroup)
                 .ThenInclude(m => m.Province)

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
                 .ThenInclude(x => x.AnswerSubquestionStatus)
                 .ThenInclude(x => x.User)

                 .Include(m => m.SubquestionCentralPolicyProvinces)
                 .ThenInclude(x => x.AnswerSubquestionOutsiders)
                 .Where(m => m.Type == "NoMaster")
                 .Where(m => m.SubjectGroupId == model.SubjectGroupId)
                 .Where(m => m.CentralPolicyProvinceId == model.CentralPolicyProvinceId)
                 .Where(m => m.SubjectGroup.ProvinceId == model.provinceid)
                 .FirstOrDefault();


                var testData = answerdata.SubquestionCentralPolicyProvinces.ToArray();

                var regiondata = _context.FiscalYearRelations
                   .Include(m => m.Region)
                   .Where(m => m.ProvinceId == answerdata.SubjectGroup.Province.Id)
                   .FirstOrDefault();

                using (DocX document = DocX.Create(createfile))
                {
                    System.Console.WriteLine("type1");
                    //Image image = document.AddImage(myImageFullPath);
                    //Picture picture = image.CreatePicture(85, 85);
                    //var logo = document.InsertParagraph();
                    //logo.AppendPicture(picture).Alignment = Alignment.center;


                    System.Console.WriteLine("1");

                    // Add a title
                    document.InsertParagraph("รายงานผลการดำเนินการของหน่วยรับตรวจ " + type)
                        .FontSize(16d)
                        .SpacingBefore(15d)
                        .SpacingAfter(15d)
                        .Bold()
                        .Alignment = Alignment.center;

                    System.Console.WriteLine("2");
                    // Insert a title paragraph.
                    var year = document.InsertParagraph("รอบการตรวจราชการที่....................");
                    year.Alignment = Alignment.center;
                    year.SpacingAfter(10d);
                    year.FontSize(16d);
                    System.Console.WriteLine("3");

                    var inspector = document.InsertParagraph("เขต : " + regiondata.Region.Name + " จังหวัด : " + answerdata.SubjectGroup.Province.Name);
                    inspector.Alignment = Alignment.center;
                    inspector.SpacingAfter(30d);
                    inspector.FontSize(16d);

                    var region = document.InsertParagraph("หัวข้อการตรวจติดตาม : " + answerdata.Name);
                    //region.Alignment = Alignment.center;
                    region.SpacingAfter(30d);
                    region.FontSize(16d);
                    region.Bold();


                    for (int i = 0; i < testData.Length; i++)
                    {
                        var region2 = document.InsertParagraph("ประเด็นการตรวจติดตาม : " + testData[i].Name);
                        //region2.Alignment = Alignment.center;
                        region2.SpacingBefore(30d);
                        region2.SpacingAfter(10d);
                        region2.FontSize(16d);
                        region2.Bold();

                        //var region3 = document.InsertParagraph("ผลการดำเนินการของหน่วยงาน:" + answerdata[i].Answer);
                        ////region3.Alignment = Alignment.center;
                        //region3.SpacingAfter(30d);
                        //region3.FontSize(16d);
                        //region3.Bold();

                        //var region4 = document.InsertParagraph("เอกสารแนบ/ภาพ....................");
                        ////region4.Alignment = Alignment.center;
                        //region4.SpacingAfter(30d);
                        //region4.FontSize(16d);
                        //region4.Bold();

                        var testData2 = testData[i].AnswerSubquestions.ToArray();
                        int dataCount = 0;
                        dataCount = testData2.Length;
                        dataCount += 1;
                        System.Console.WriteLine("Data Count: " + dataCount);
                        // Add a table in a document of 1 row and 3 columns.
                        var columnWidths = new float[] { 300f, 300f, 300f };
                        var t = document.InsertTable(dataCount, columnWidths.Length);

                        System.Console.WriteLine("4");

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

                        //System.Console.WriteLine("9999: ");
                        System.Console.WriteLine("5");

                        //}
                        // Add rows in the table.

                        int j = 0;
                        System.Console.WriteLine("Count: " + testData2.Length);
                        for (int ii = 0; ii < testData2.Length; ii++)
                        {
                            j += 1;
                            //    //System.Console.WriteLine(i+=1);
                            System.Console.WriteLine("6.1: " + testData2[ii].Answer);
                            t.Rows[j].Cells[0].Paragraphs[0].Append(testData2[ii].User.ProvincialDepartments.Name);
                            System.Console.WriteLine("6.2: " + testData2[ii].Answer);
                            t.Rows[j].Cells[1].Paragraphs[0].Append(testData2[ii].Answer);
                            //System.Console.WriteLine("6.3: " + answerdata[i].Answer);
                            //t.Rows[j].Cells[2].Paragraphs[0].Append(answerdata[i].Answer);
                            System.Console.WriteLine("7");

                        }

                        // Set a blank border for the table's top/bottom borders.
                        var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                        //t.SetBorder(TableBorderType.Bottom, blankBorder);
                        //t.SetBorder(TableBorderType.Top, blankBorder);
                    }
                    System.Console.WriteLine("8");
                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }
            if (model.reporttype == "3")
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
        [HttpPost("reportsuggestions")]
        public IActionResult CreateReport2([FromForm] reportsuggestions model)
        {
            var reportsuggestionsdata = _context.SubjectGroups
                 .Include(m => m.CentralPolicy)
                 .Include(m => m.Province)
                 .Include(m => m.SubjectCentralPolicyProvinces)
                 .ThenInclude(m => m.SubquestionCentralPolicyProvinces)
                 .ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
                 .ThenInclude(m => m.ProvincialDepartment)
                 .Include(m => m.AnswerRecommenDationInspectors)
                 .Where(m => m.Id == model.subjectgroupsid)
                 .FirstOrDefault();

            var testData = reportsuggestionsdata.SubjectCentralPolicyProvinces.ToArray();
            System.Console.WriteLine("3" + reportsuggestionsdata.Suggestion);

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

                for (int i = 0; i < testData.Length; i++)
                {
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

                    var region = document.InsertParagraph("หัวข้อการตรวจติดตาม : " + reportsuggestionsdata.CentralPolicy.Title);
                    //region.Alignment = Alignment.center;
                    region.SpacingAfter(10d);
                    region.FontSize(16d);
                    region.Bold();

                    var region2 = document.InsertParagraph("ประเด็นการตรวจติดตาม : " + testData[i].Name);
                    //region2.Alignment = Alignment.center;
                    region2.SpacingAfter(30d);
                    region2.FontSize(16d);
                    region2.Bold();




                    //var region4 = document.InsertParagraph("ข้อเสนอแนะระดับนโยบาย");
                    ////region4.Alignment = Alignment.center;
                    //region4.SpacingAfter(30d);
                    //region4.FontSize(16d);
                    //region4.Bold();
                    //region4.UnderlineStyle(UnderlineStyle.dash);


                    var testData2 = testData[i].SubquestionCentralPolicyProvinces;
                    var testData3 = testData2.ToArray();
                    var testData4 = testData3[0].SubjectCentralPolicyProvinceGroups.ToArray();

                    int dataCount = 0;
                    dataCount = testData4.Count();
                    dataCount += 1;
                    System.Console.WriteLine("Data Count: " + dataCount);
                    // Add a table in a document of 1 row and 3 columns.
                    var columnWidths = new float[] { 300f, 300f, 300f, 300f };
                    var t = document.InsertTable(dataCount, columnWidths.Length);

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
                    int j = 0;
                    for (int ii = 0; ii < testData4.Length; ii++)
                    {
                        j += 1;
                        //System.Console.WriteLine(i+=1);

                        System.Console.WriteLine("JJJJJ: " + j);
                        System.Console.WriteLine("9.1: " + reportsuggestionsdata.Province.Name);
                        t.Rows[j].Cells[0].Paragraphs[0].Append(reportsuggestionsdata.Province.Name);
                        System.Console.WriteLine("9.2: " + ".....");
                        t.Rows[j].Cells[1].Paragraphs[0].Append(".....");
                        System.Console.WriteLine("9.3: " + reportsuggestionsdata.Suggestion);
                        t.Rows[j].Cells[2].Paragraphs[0].Append(reportsuggestionsdata.Suggestion);
                        System.Console.WriteLine("9.4: " + testData4[ii].ProvincialDepartment.Name);
                        t.Rows[j].Cells[3].Paragraphs[0].Append(testData4[ii].ProvincialDepartment.Name);
                        System.Console.WriteLine("10");
                    }

                    // Set a blank border for the table's top/bottom borders.
                    var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                    //t.SetBorder(TableBorderType.Bottom, blankBorder);
                    //t.SetBorder(TableBorderType.Top, blankBorder);
                }
                System.Console.WriteLine("11");
                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }
            //}
            return Ok(new { data = filename });
        }
        [HttpPost("reportsuggestionsresult")]
        public IActionResult CreateReport3([FromForm] reportsuggestionsresult model)
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
            if (model.reporttype == "1")
            {


                var data = _context.AnswerRecommenDationInspectors
                    .Include(m => m.SubjectGroup)
                    .ThenInclude(m => m.CentralPolicy)
                    .Include(m => m.SubjectGroup)
                    .ThenInclude(m => m.SubjectCentralPolicyProvinces)
                    .Include(m => m.User)
                    .ThenInclude(m => m.Province)
                    .Include(m => m.User)
                    .ThenInclude(m => m.ProvincialDepartments)
                    .Where(m => m.SubjectGroupId == model.SubjectGroupId)
                    .Where(m => m.User.ProvincialDepartmentId == model.provincialDepartmentId);

                var ProvincialDepartmentdata = _context.ProvincialDepartment
                    .Where(m => m.Id == model.provincialDepartmentId)
                    .FirstOrDefault();

                var Provincedata = _context.Provinces
                    .Where(m => m.Id == model.provinceId)
                    .FirstOrDefault();

                var testData = data.ToArray();


                using (DocX document = DocX.Create(createfile))
                {
                    System.Console.WriteLine("4");
                    var type = "รายหน่วยงาน";
                    //Image image = document.AddImage(myImageFullPath);
                    //Picture picture = image.CreatePicture(85, 85);
                    //var logo = document.InsertParagraph();
                    //logo.AppendPicture(picture).Alignment = Alignment.center;


                    System.Console.WriteLine("5");

                    // Add a title
                    int j = 0;
                    for (int i = 0; i < testData.Length; i++)
                    {
                        j += 1;
                        var testData2 = testData[i].SubjectGroup.SubjectCentralPolicyProvinces.ToArray();
                        for (int ii = 0; ii < testData2.Length; ii++)
                        {

                            document.InsertParagraph("รายงานผลการดำเนินการตามข้อเสนอแนะของผู้ตรวจราชการ : " + type)
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

                            System.Console.WriteLine("6.1");
                            var inspector = document.InsertParagraph("หน่วยงาน : " + ProvincialDepartmentdata.Name + "จังหวัด : " + Provincedata.Name);
                            inspector.Alignment = Alignment.center;
                            inspector.SpacingAfter(30d);
                            inspector.FontSize(16d);

                            System.Console.WriteLine("6.2");
                            var region = document.InsertParagraph("หัวข้อการตรวจติดตาม : " + testData[i].SubjectGroup.CentralPolicy.Title);
                            //region.Alignment = Alignment.center;
                            region.SpacingAfter(10d);
                            region.FontSize(16d);
                            region.Bold();

                            System.Console.WriteLine("6.3");
                            var region2 = document.InsertParagraph("ประเด็นการตรวจติดตาม : " + testData2[ii].Name);
                            //region2.Alignment = Alignment.center;
                            region2.SpacingAfter(10d);
                            region2.FontSize(16d);
                            region2.Bold();


                            //var region4 = document.InsertParagraph("ข้อเสนอแนะระดับพื้นที่");
                            ////region4.Alignment = Alignment.center;
                            //region4.SpacingAfter(30d);
                            //region4.FontSize(16d);
                            //region4.Bold();
                            //region4.UnderlineStyle(UnderlineStyle.singleLine);



                            int dataCount = 0;
                            dataCount = testData.Length;
                            dataCount += 1;
                            System.Console.WriteLine("Data Count: " + dataCount);
                            // Add a table in a document of 1 row and 3 columns.
                            var columnWidths = new float[] { 300f, 300f, 300f, 300f };
                            var t = document.InsertTable(dataCount, columnWidths.Length);

                            System.Console.WriteLine("8");

                            // Set the table's column width and background 
                            t.SetWidths(columnWidths);
                            t.AutoFit = AutoFit.Contents;

                            var row = t.Rows.First();

                            // Fill in the columns of the first row in the table.
                            //for (int i = 0; i < row.Cells.Count; ++i)
                            //{
                            //row.Cells[0].Paragraphs.First().Append("ลำดับที่");
                            row.Cells[0].Paragraphs.First().Append("ผต.นร./ผต.กท. (เจ้าของเรื่อง)");
                            row.Cells[1].Paragraphs.First().Append("ข้อเสนอแนะของ ผต.");
                            row.Cells[2].Paragraphs.First().Append("การดำเนินงานของหน่วยงาน");
                            row.Cells[3].Paragraphs.First().Append("เอกสารแนบ (เป็น Link ให้อ่านเพิ่มเติม)");


                            //System.Console.WriteLine("9999: ");
                            //System.Console.WriteLine("9: ");

                            //}
                            // Add rows in the table.
                            //int j = 0;
                            //for (int i = 0; i < model.reportData.Length; i++)
                            //{

                            //    //System.Console.WriteLine(i+=1);

                            System.Console.WriteLine("JJJJJ: " + j);
                            System.Console.WriteLine("9.1: " + ".........");
                            t.Rows[j].Cells[0].Paragraphs[0].Append(".........");
                            System.Console.WriteLine("9.2: " + testData[i].SubjectGroup.Suggestion);
                            t.Rows[j].Cells[1].Paragraphs[0].Append(testData[i].SubjectGroup.Suggestion);
                            System.Console.WriteLine("9.3: " + testData[i].Answersuggestion);
                            t.Rows[j].Cells[2].Paragraphs[0].Append(testData[i].Answersuggestion);
                            System.Console.WriteLine("9.4: " + ".......");
                            t.Rows[j].Cells[3].Paragraphs[0].Append(".........");
                            System.Console.WriteLine("10");
                            //}

                            // Set a blank border for the table's top/bottom borders.
                            var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                            //t.SetBorder(TableBorderType.Bottom, blankBorder);
                            //t.SetBorder(TableBorderType.Top, blankBorder);
                        }
                    }

                    System.Console.WriteLine("11");
                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }
            if (model.reporttype == "2")
            {

                var data = _context.AnswerRecommenDationInspectors
                     .Include(m => m.SubjectGroup)
                     .ThenInclude(m => m.CentralPolicy)
                     .Include(m => m.SubjectGroup)
                     .ThenInclude(m => m.SubjectCentralPolicyProvinces)
                     .Include(m => m.User)
                     .ThenInclude(m => m.Province)
                     .Include(m => m.User)
                     .ThenInclude(m => m.ProvincialDepartments)
                     .Where(m => m.SubjectGroupId == model.SubjectGroupId)
                     .Where(m => m.User.ProvinceId == model.provinceId);

                var ProvincialDepartmentdata = _context.ProvincialDepartment
                    .Where(m => m.Id == model.provincialDepartmentId)
                    .FirstOrDefault();

                var Provincedata = _context.Provinces
                    .Where(m => m.Id == model.provinceId)
                    .FirstOrDefault();

                var regiondata = _context.FiscalYearRelations
                    .Include(m => m.Region)
                    .Where(m => m.ProvinceId == Provincedata.Id)
                    .FirstOrDefault();

                var testData = data.ToArray();


                using (DocX document = DocX.Create(createfile))
                {
                    System.Console.WriteLine("4");
                    var type = "รายจังหวัด";
                    //Image image = document.AddImage(myImageFullPath);
                    //Picture picture = image.CreatePicture(85, 85);
                    //var logo = document.InsertParagraph();
                    //logo.AppendPicture(picture).Alignment = Alignment.center;


                    System.Console.WriteLine("5");

                    // Add a title
                    int j = 0;
                    for (int i = 0; i < testData.Length; i++)
                    {
                        j += 1;
                        var testData2 = testData[i].SubjectGroup.SubjectCentralPolicyProvinces.ToArray();
                        for (int ii = 0; ii < testData2.Length; ii++)
                        {

                            document.InsertParagraph("รายงานผลการดำเนินการตามข้อเสนอแนะของผู้ตรวจราชการ : " + type)
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

                            System.Console.WriteLine("6.1");
                            var inspector = document.InsertParagraph("เขต : " + regiondata.Region.Name + "จังหวัด : " + Provincedata.Name);
                            inspector.Alignment = Alignment.center;
                            inspector.SpacingAfter(30d);
                            inspector.FontSize(16d);

                            System.Console.WriteLine("6.2");
                            var region = document.InsertParagraph("หัวข้อการตรวจติดตาม : " + testData[i].SubjectGroup.CentralPolicy.Title);
                            //region.Alignment = Alignment.center;
                            region.SpacingAfter(10d);
                            region.FontSize(16d);
                            region.Bold();

                            System.Console.WriteLine("6.3");
                            var region2 = document.InsertParagraph("ประเด็นการตรวจติดตาม : " + testData2[ii].Name);
                            //region2.Alignment = Alignment.center;
                            region2.SpacingAfter(10d);
                            region2.FontSize(16d);
                            region2.Bold();


                            //var region4 = document.InsertParagraph("ข้อเสนอแนะระดับพื้นที่");
                            ////region4.Alignment = Alignment.center;
                            //region4.SpacingAfter(30d);
                            //region4.FontSize(16d);
                            //region4.Bold();
                            //region4.UnderlineStyle(UnderlineStyle.singleLine);



                            int dataCount = 0;
                            dataCount = testData.Length;
                            dataCount += 1;
                            System.Console.WriteLine("Data Count: " + dataCount);
                            // Add a table in a document of 1 row and 3 columns.
                            var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f };
                            var t = document.InsertTable(dataCount, columnWidths.Length);

                            System.Console.WriteLine("8");

                            // Set the table's column width and background 
                            t.SetWidths(columnWidths);
                            t.AutoFit = AutoFit.Contents;

                            var row = t.Rows.First();

                            // Fill in the columns of the first row in the table.
                            //for (int i = 0; i < row.Cells.Count; ++i)
                            //{
                            //row.Cells[0].Paragraphs.First().Append("ลำดับที่");
                            row.Cells[0].Paragraphs.First().Append("ผต.นร./ผต.กท. (เจ้าของเรื่อง)");
                            row.Cells[1].Paragraphs.First().Append("ข้อเสนอแนะของ ผต.");
                            row.Cells[2].Paragraphs.First().Append("หน่วยรับดำเนินการ");
                            row.Cells[3].Paragraphs.First().Append("การดำเนินงานของหน่วยงาน");
                            row.Cells[4].Paragraphs.First().Append("เอกสารแนบ (เป็น Link ให้อ่านเพิ่มเติม)");


                            //System.Console.WriteLine("9999: ");
                            //System.Console.WriteLine("9: ");

                            //}
                            // Add rows in the table.
                            //int j = 0;
                            //for (int i = 0; i < model.reportData.Length; i++)
                            //{

                            //    //System.Console.WriteLine(i+=1);

                            System.Console.WriteLine("JJJJJ: " + j);
                            System.Console.WriteLine("9.1: " + ".........");
                            t.Rows[j].Cells[0].Paragraphs[0].Append(".........");
                            System.Console.WriteLine("9.2: " + testData[i].SubjectGroup.Suggestion);
                            t.Rows[j].Cells[1].Paragraphs[0].Append(testData[i].SubjectGroup.Suggestion);
                            System.Console.WriteLine("9.3: " + testData[i].SubjectGroup.Suggestion);
                            t.Rows[j].Cells[2].Paragraphs[0].Append(testData[i].User.ProvincialDepartments.Name);
                            System.Console.WriteLine("9.4: " + testData[i].Answersuggestion);
                            t.Rows[j].Cells[3].Paragraphs[0].Append(testData[i].Answersuggestion);
                            System.Console.WriteLine("9.5: " + ".......");
                            t.Rows[j].Cells[4].Paragraphs[0].Append(".........");
                            System.Console.WriteLine("10");
                            //}

                            // Set a blank border for the table's top/bottom borders.
                            var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                            //t.SetBorder(TableBorderType.Bottom, blankBorder);
                            //t.SetBorder(TableBorderType.Top, blankBorder);
                        }
                    }

                    System.Console.WriteLine("11");
                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }
            return Ok(new { data = filename });
        }
        [HttpPost("reportquestionnaire")]
        public IActionResult CreateReport4([FromForm] reportquestionnaire model)
        {
            var CentralPolicyEvents = _context.CentralPolicyEvents
               .Include(m => m.CentralPolicyEventQuestions)
               .Include(m => m.CentralPolicy)
               .Where(m => m.Id == model.planid).First();

            var InspectionPlanEvents = _context.InspectionPlanEvents
                .Where(m => m.Id == CentralPolicyEvents.InspectionPlanEventId).First();

            var user = _context.CentralPolicyUsers
                .Include(m => m.User)
                .ThenInclude(m => m.Province)
                .Include(m => m.User)
                .ThenInclude(m => m.ProvincialDepartments)
                .Where(m => m.User.Role_id == 7)
                .Where(m => m.CentralPolicyId == CentralPolicyEvents.CentralPolicyId && m.ProvinceId == InspectionPlanEvents.ProvinceId)
                .Where(m => m.InspectionPlanEventId == InspectionPlanEvents.Id)
                .ToList();

            var cenprolicyevent = _context.CentralPolicyEvents
               .Where(m => m.InspectionPlanEventId == model.planid)
               //.Where(m => m.CentralPolicyId == cenid.CentralPolicyId)
               .FirstOrDefault();

            var question = _context.CentralPolicyEventQuestions
                .Include(m => m.CentralPolicyEvent)
                .Include(m => m.AnswerCentralPolicyProvinces)
                .Where(m => m.CentralPolicyEventId == cenprolicyevent.Id)
                .ToList();

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
                dataCount = question.Count;
                dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f };
                var t = document.InsertTable(dataCount, columnWidths.Length);

                System.Console.WriteLine("8");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                //row.Cells[0].Paragraphs.First().Append("ลำดับที่");
                row.Cells[0].Paragraphs.First().Append("หัวข้อการตรวจติดตาม");
                row.Cells[1].Paragraphs.First().Append("คำถามภาคประชาชน");
                row.Cells[2].Paragraphs.First().Append("หน่วยงานเจ้าของเรื่อง/ผต.นร./ผต.กท.");
                row.Cells[3].Paragraphs.First().Append("จังหวัด");
                row.Cells[4].Paragraphs.First().Append("ที่ปรึกษาที่ได้รับแบบสอบถามฯ");



                //System.Console.WriteLine("9999: ");
                //System.Console.WriteLine("9: ");

                //}
                // Add rows in the table.
                int j = 0;
                for (int i = 0; i < question.Count; i++)
                {
                    j += 1;
                    //System.Console.WriteLine(i+=1);

                    System.Console.WriteLine("JJJJJ: " + j);
                    //System.Console.WriteLine("9.1: " + model.reportData[i].subject);
                    //t.Rows[j].Cells[0].Paragraphs[0].Append(model.reportData[i].subject);
                    System.Console.WriteLine("9.2: " + CentralPolicyEvents.CentralPolicy.Title);
                    t.Rows[j].Cells[0].Paragraphs[0].Append(CentralPolicyEvents.CentralPolicy.Title);
                    System.Console.WriteLine("9.3: " + question[i].QuestionPeople);
                    t.Rows[j].Cells[1].Paragraphs[0].Append(question[i].QuestionPeople);
                    for (int ii = 0; ii < user.Count; ii++)
                    {
                        System.Console.WriteLine("9.4: " + user[ii].User.ProvincialDepartments.Name);
                        t.Rows[j].Cells[2].Paragraphs[0].Append(user[ii].User.ProvincialDepartments.Name);
                        System.Console.WriteLine("9.5: " + user[ii].User.Province.Name);
                        t.Rows[j].Cells[3].Paragraphs[0].Append(user[ii].User.Province.Name);
                        System.Console.WriteLine("9.6: " + user[ii].User.Name);
                        t.Rows[j].Cells[4].Paragraphs[0].Append(user[ii].User.Name);
                        System.Console.WriteLine("10");
                    }

                }

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
        [HttpPost("reportcomment")]
        public IActionResult CreateReport5([FromForm] reportcomment model)
        {

            //var answerdata = _context.AnswerCentralPolicyProvinces
            //    .Include(m => m.CentralPolicyProvince)
            //    .ThenInclude(m => m.CentralPolicy)
            //    .Include(m => m.CentralPolicyEventQuestion)
            //    .Include(m => m.User)
            //    .Where(m => m.UserId == model.userid)
            //    .ToList();

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
            if (model.reporttype == "1")
            {
                var answerdata = _context.AnswerCentralPolicyProvinces
                .Include(m => m.CentralPolicyProvince)
                .ThenInclude(m => m.CentralPolicy)
                .Include(m => m.CentralPolicyEventQuestion)
                .Include(m => m.User)
                .ThenInclude(m => m.UserProvince)         
                .ThenInclude(m => m.Province)
                .Where(m => m.UserId == model.userid)
                .ToList();

                var regiondata = _context.FiscalYearRelations
                  .Include(m => m.Region)
                  .Where(m => m.ProvinceId == answerdata[0].User.ProvinceId)
                  .FirstOrDefault();

                System.Console.WriteLine("type1");
                var type = "รายบุคคล";
                using (DocX document = DocX.Create(createfile))
                {
                    System.Console.WriteLine("4");
                    //Image image = document.AddImage(myImageFullPath);
                    //Picture picture = image.CreatePicture(85, 85);
                    //var logo = document.InsertParagraph();
                    //logo.AppendPicture(picture).Alignment = Alignment.center;


                    System.Console.WriteLine("5");

                    // Add a title
                    document.InsertParagraph("รายงานความคิดเห็นของที่ปรึกษาผู้ตรวจราชการภาคประชาชน : " + type)
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

                    System.Console.WriteLine("ชื่อที่ปรึกษาฯ" + answerdata[0].User.Name);
                    System.Console.WriteLine("เขต" + regiondata.Region.Name);
                    System.Console.WriteLine("จังหวัด" + answerdata[0].User.Province.Name);

                    var inspector = document.InsertParagraph("ชื่อที่ปรึกษาฯ : " + answerdata[0].User.Name + "เขต : " + regiondata.Region.Name + "จังหวัด : " + answerdata[0].User.Province.Name);
                    inspector.Alignment = Alignment.center;
                    inspector.SpacingAfter(30d);
                    inspector.FontSize(16d);

                    System.Console.WriteLine("7");
                    int dataCount = 0;
                    dataCount = answerdata.Count;
                    dataCount += 1;
                    System.Console.WriteLine("Data Count: " + dataCount);
                    // Add a table in a document of 1 row and 3 columns.
                    var columnWidths = new float[] { 300f, 300f, 300f, 300f };
                    var t = document.InsertTable(dataCount, columnWidths.Length);

                    System.Console.WriteLine("8");

                    // Set the table's column width and background 
                    t.SetWidths(columnWidths);
                    t.AutoFit = AutoFit.Contents;

                    var row = t.Rows.First();

                    // Fill in the columns of the first row in the table.
                    //for (int i = 0; i < row.Cells.Count; ++i)
                    //{
                    //row.Cells[0].Paragraphs.First().Append("ลำดับที่");
                    row.Cells[0].Paragraphs.First().Append("หน่วยงานเจ้าของเรื่อง/ผต.นร./ผต.กท.");
                    row.Cells[1].Paragraphs.First().Append("หัวข้อการตรวจติดตาม");
                    row.Cells[2].Paragraphs.First().Append("คำถามภาคประชาชน");
                    row.Cells[3].Paragraphs.First().Append("ความเห็นที่ปรึกษาฯ");



                    //System.Console.WriteLine("9999: ");
                    //System.Console.WriteLine("9: ");

                    //}
                    // Add rows in the table.
                    int j = 0;
                    for (int i = 0; i < answerdata.Count; i++)
                    {
                        j += 1;
                        //System.Console.WriteLine(i+=1);

                        System.Console.WriteLine("JJJJJ: " + j);
                        System.Console.WriteLine("9.1: ");
                        t.Rows[j].Cells[0].Paragraphs[0].Append("..............");
                        System.Console.WriteLine("9.2: " + answerdata[i].CentralPolicyProvince.CentralPolicy.Title);
                        t.Rows[j].Cells[1].Paragraphs[0].Append(answerdata[i].CentralPolicyProvince.CentralPolicy.Title);
                        System.Console.WriteLine("9.3: " + answerdata[i].CentralPolicyEventQuestion.QuestionPeople);
                        t.Rows[j].Cells[2].Paragraphs[0].Append(answerdata[i].CentralPolicyEventQuestion.QuestionPeople);
                        System.Console.WriteLine("9.4: " + answerdata[i].Answer);
                        t.Rows[j].Cells[3].Paragraphs[0].Append(answerdata[i].Answer);
                        System.Console.WriteLine("10");
                    }

                    // Set a blank border for the table's top/bottom borders.
                    //var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                    //t.SetBorder(TableBorderType.Bottom, blankBorder);
                    //t.SetBorder(TableBorderType.Top, blankBorder);


                    System.Console.WriteLine("11");
                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }
            if (model.reporttype == "2")
            {
                var answerdata = _context.AnswerCentralPolicyProvinces
                .Include(m => m.CentralPolicyProvince)
                .ThenInclude(m => m.CentralPolicy)
                .Include(m => m.CentralPolicyEventQuestion)
                .Include(m => m.User)
                .ThenInclude(m => m.Province)
                .Where(m => m.User.ProvinceId == 1)
                .ToList();

                var regiondata = _context.FiscalYearRelations
                  .Include(m => m.Region)
                  .Where(m => m.ProvinceId == answerdata[0].User.ProvinceId)
                  .FirstOrDefault();

                System.Console.WriteLine("type2");
                var type = "รายจังหวัด";
                using (DocX document = DocX.Create(createfile))
                {
                    System.Console.WriteLine("4");
                    //Image image = document.AddImage(myImageFullPath);
                    //Picture picture = image.CreatePicture(85, 85);
                    //var logo = document.InsertParagraph();
                    //logo.AppendPicture(picture).Alignment = Alignment.center;


                    System.Console.WriteLine("5");

                    // Add a title
                    document.InsertParagraph("รายงานความคิดเห็นของที่ปรึกษาผู้ตรวจราชการภาคประชาชน : " + type)
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

                    System.Console.WriteLine("ชื่อที่ปรึกษาฯ" + answerdata[0].User.Name);
                    System.Console.WriteLine("เขต" + regiondata.Region.Name);
                    System.Console.WriteLine("จังหวัด" + answerdata[0].User.Province.Name);

                    var inspector = document.InsertParagraph("เขต : " + regiondata.Region.Name + "จังหวัด : " + answerdata[0].User.Province.Name);
                    inspector.Alignment = Alignment.center;
                    inspector.SpacingAfter(30d);
                    inspector.FontSize(16d);

                    System.Console.WriteLine("7");
                    int dataCount = 0;
                    dataCount = answerdata.Count;
                    dataCount += 1;
                    System.Console.WriteLine("Data Count: " + dataCount);
                    // Add a table in a document of 1 row and 3 columns.
                    var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f };
                    var t = document.InsertTable(dataCount, columnWidths.Length);

                    System.Console.WriteLine("8");

                    // Set the table's column width and background 
                    t.SetWidths(columnWidths);
                    t.AutoFit = AutoFit.Contents;

                    var row = t.Rows.First();

                    // Fill in the columns of the first row in the table.
                    //for (int i = 0; i < row.Cells.Count; ++i)
                    //{
                    //row.Cells[0].Paragraphs.First().Append("ลำดับที่");
                    row.Cells[0].Paragraphs.First().Append("หน่วยงานเจ้าของเรื่อง/ผต.นร./ผต.กท.");
                    row.Cells[1].Paragraphs.First().Append("หัวข้อการตรวจติดตาม");
                    row.Cells[2].Paragraphs.First().Append("คำถามภาคประชาชน");
                    row.Cells[3].Paragraphs.First().Append("ชื่อที่ปรึกษาฯ");
                    row.Cells[4].Paragraphs.First().Append("ความเห็นที่ปรึกษาฯ");



                    //System.Console.WriteLine("9999: ");
                    //System.Console.WriteLine("9: ");

                    //}
                    // Add rows in the table.
                    int j = 0;
                    for (int i = 0; i < answerdata.Count; i++)
                    {
                        j += 1;
                        //System.Console.WriteLine(i+=1);

                        System.Console.WriteLine("JJJJJ: " + j);
                        System.Console.WriteLine("9.1: ");
                        t.Rows[j].Cells[0].Paragraphs[0].Append("..............");
                        System.Console.WriteLine("9.2: " + answerdata[i].CentralPolicyProvince.CentralPolicy.Title);
                        t.Rows[j].Cells[1].Paragraphs[0].Append(answerdata[i].CentralPolicyProvince.CentralPolicy.Title);
                        System.Console.WriteLine("9.3: " + answerdata[i].CentralPolicyEventQuestion.QuestionPeople);
                        t.Rows[j].Cells[2].Paragraphs[0].Append(answerdata[i].CentralPolicyEventQuestion.QuestionPeople);
                        System.Console.WriteLine("9.4: " + answerdata[i].Answer);
                        t.Rows[j].Cells[3].Paragraphs[0].Append(answerdata[i].User.Name);
                        System.Console.WriteLine("9.5: " + answerdata[i].Answer);
                        t.Rows[j].Cells[4].Paragraphs[0].Append(answerdata[i].Answer);
                        System.Console.WriteLine("10");
                    }

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
        // GET: api/values
        [HttpGet("test")]
        public IActionResult test()
        {
            var data = _context.AnswerRecommenDationInspectors
                     .Include(m => m.SubjectGroup)
                     .ThenInclude(m => m.CentralPolicy)
                     .Include(m => m.SubjectGroup)
                     .ThenInclude(m => m.SubjectCentralPolicyProvinces)
                     .Include(m => m.User)
                     .ThenInclude(m => m.Province)
                     .Include(m => m.User)
                     .ThenInclude(m => m.ProvincialDepartments)
                     .Where(m => m.SubjectGroupId == 5)
                     .Where(m => m.User.ProvinceId == 1);

            return Ok(data);
            //return Ok(new { data });
        }
    }
}
