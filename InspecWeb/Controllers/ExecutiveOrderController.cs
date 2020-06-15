using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
//using InspecWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Image = Xceed.Document.NET.Image;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutiveOrderController : Controller
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

        public ExecutiveOrderController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        [HttpGet]
        public IEnumerable<ExecutiveOrder> Get()
        {

            var executivedata = _context.ExecutiveOrders.ToList();
            return executivedata;
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var centralpolicydata = _context.CentralPolicies
                .Include(m => m.CentralPolicyProvinces)
                .ThenInclude(x => x.Province)
                .Include(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicyFiles)
                .Include(m => m.ExecutiveOrders)
                .Include(m => m.Subjects)
                .ThenInclude(m => m.Subquestions)
                .Where(m => m.Id == id).FirstOrDefault();

            return Ok(centralpolicydata);
            //return "value";
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ExecutiveViewModel model)
        {
            //System.Console.WriteLine("centralpolicy: " + model.CentralpolicyId);
            //System.Console.WriteLine("provinceid: " + model.ProvinceId);
            //System.Console.WriteLine("Name: " + model.Name);
            var date = DateTime.Now;
            var cabinedata = new ExecutiveOrder
            {

                DetailExecutiveOrder = model.Name,
                CentralPolicyId = model.CentralpolicyId,
                ProvinceId = model.ProvinceId,
                UserId = model.UserId,
                CreatedAt = date

            };

            _context.ExecutiveOrders.Add(cabinedata);
            _context.SaveChanges();

            if (!Directory.Exists(_environment.WebRootPath + "//executivefile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//executivefile//"); //สร้าง Folder Upload ใน wwwroot
            }
            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//executivefile//";


            foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
            //foreach (var formFile in data.files)
            {
                var random = RandomString(10);
                string filePath2 = formFile.Value.FileName;
                string filename = Path.GetFileName(filePath2);
                string ext = Path.GetExtension(filename);

                if (formFile.Value.Length > 0)
                {
                    // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                    using (var stream = System.IO.File.Create(filePath + random + filename))
                    {
                        await formFile.Value.CopyToAsync(stream);
                    }

                    var ExecutiveOrderFile = new ExecutiveOrderFile
                    {
                        ExecutiveOrderId = cabinedata.Id,
                        Name = random + filename,
                    };
                    _context.ExecutiveFiles.Add(ExecutiveOrderFile);
                    _context.SaveChanges();
                }
            }
            //System.Console.WriteLine("Return ID: " + cabinedata.Id);
            return Ok(new { Id = cabinedata.Id });
        }

        [HttpGet("province/{id}")]
        public object Getprovince(long id)
        {
            var result = new List<object>();


            var centralpolicyprovincedata = _context.CentralPolicyProvinces
                .Include(m => m.Province)
                .Where(m => m.CentralPolicyId == id)
                .ToList();

            //foreach (var provinceid in centralpolicyprovincedata)
            //{
            //    var provincename = _context.Provinces
            //        .Where(x => x.Id == provinceid)
            //        .ToList();

            //    result.Add(

            //        provincename
            //    );
            //}

            return Ok(centralpolicyprovincedata);
            //return "value";
        }
        [HttpGet("detail/{id}")]//new///
        public IActionResult Getexecutive(long id)
        {
            var executiveOrderdata = _context.ExecutiveOrders
                /*.Include(m => m.DetailExecutiveOrder)*/
                .Include(m => m.Province)
                .Include(m => m.ExecutiveOrderFiles)
                .Include(m => m.AnswerExecutiveOrderFiles)
                .Include(m => m.CentralPolicy)
                .Where(m => m.CentralPolicyId == id);

            return Ok(executiveOrderdata);
            //return "value";
        }
        [HttpGet("view/{id}")]//new///
        public IActionResult Getviewexecutive(long id)
        {
            var executiveOrderdata = _context.ExecutiveOrders
                /*.Include(m => m.DetailExecutiveOrder)*/
                .Include(m => m.Province)
                .Include(m => m.UserId)
                .Include(m => m.CreatedAt)
                .Include(m => m.ExecutiveOrderFiles)
                .Include(m => m.AnswerUserId)
                .Include(m => m.AnswerExecutiveOrderFiles)
                .Include(m => m.CentralPolicy)
                .Where(m => m.CentralPolicyId == id);

            return Ok(executiveOrderdata);
            //return "value";
        }


        [HttpGet("detailrole3/{id}/{userid}")]
        public IActionResult Getexecutiverole3(long id, string userid)
        {
            var provinceId = _context.UserProvinces
                .Where(x => x.UserID == userid)
                .Select(x => x.ProvinceId)
                .FirstOrDefault();

            var executiveOrderdata = _context.ExecutiveOrders
                /*.Include(m => m.DetailExecutiveOrder)*/
                .Include(m => m.Province)
                .Include(m => m.ExecutiveOrderFiles)
                .Where(m => m.CentralPolicyId == id && m.ProvinceId == provinceId);

            return Ok(executiveOrderdata);
            //return "value";
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] ExecutiveViewModel model)
        {
            /*System.Console.WriteLine("detailexecutiveorder: " + model.id);
            System.Console.WriteLine("AnswerDetail: " + model.AnswerDetail);
            System.Console.WriteLine("AnswerProblem: " + model.AnswerProblem);
            System.Console.WriteLine("AnswerCounsel: " + model.AnswerCounsel);
            System.Console.WriteLine("AnswerExecutiveorder: " + model.files);*/
            var cabinedata = _context.ExecutiveOrders.Find(model.id);
            {
                cabinedata.AnswerDetail = model.AnswerDetail;
                cabinedata.AnswerProblem = model.AnswerProblem;
                cabinedata.AnswerCounsel = model.AnswerCounsel;
                cabinedata.AnswerUserId = model.AnswerUserId;
                //System.Console.WriteLine(model.AnswerUserId);
            };

            _context.Entry(cabinedata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            if (!Directory.Exists(_environment.WebRootPath + "//executivefile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//executivefile//"); //สร้าง Folder Upload ใน wwwroot
            }
            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//executivefile//";
            foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
            //foreach (var formFile in data.files)
            {

                var random = RandomString(10);
                string filePath2 = formFile.Value.FileName;
                string filename = Path.GetFileName(filePath2);
                string ext = Path.GetExtension(filename);

                if (formFile.Value.Length > 0)
                {

                    // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                    using (var stream = System.IO.File.Create(filePath + random + filename))
                    {
                        await formFile.Value.CopyToAsync(stream);
                    }
                    var AnswerExecutiveOrderFile = new AnswerExecutiveOrderFile
                    {
                        ExecutiveOrderId = model.id,
                        Name = random + filename
                    };
                    _context.AnswerExecutiveOrderFiles.Add(AnswerExecutiveOrderFile);
                    _context.SaveChanges();
                    /*  System.Console.WriteLine("Sucess");*/
                }
            }
            return Ok(new { Id = model.id });
        }

        [HttpGet("ex/{id}")]
        public IActionResult GetData(string id)
        {
            /* System.Console.WriteLine("DDDDD");
             System.Console.WriteLine("USERID : " + id);*/
            //var inspectionPlanEventdata = from P in _context.InspectionPlanEvents
            //                              select P;
            //return inspectionPlanEventdata;
            var userprovince = _context.UserProvinces
                               .Where(m => m.UserID == id)
                               .ToList(); 

            //var inspectionplans = _context.InspectionPlanEvents
            //                    .Include(m => m.Province)
            //                    .Include(m => m.CentralPolicyEvents)
            //                    .ThenInclude(m => m.CentralPolicy)
            //                    .ThenInclude(m => m.CentralPolicyProvinces)
            //                    .ToList();

            //var inspectionplans = _context.CentralPolicies
            //                    .Include(m => m.CentralPolicyProvinces)
            //                    .ThenInclude(x => x.Province).ToList();

            var inspectionplans = _context.CentralPolicyProvinces
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicy)
                .ThenInclude(x => x.FiscalYear)
                .ToList();

            List<object> termsList = new List<object>();
            foreach (var inspectionplan in inspectionplans)
            {
                for (int i = 0; i < userprovince.Count(); i++)
                {
                    if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                        termsList.Add(inspectionplan);
                }
            }

            return Ok(termsList);

        }

        [HttpPost("export1")]// 9.5.9 (1)รายข้อสั่งการผู้บริหาร
        public IActionResult Getexport1([FromBody] UserViewModel body  )
        {
            var userId = body.Id;
            var Eexcutive1 = _context.ExecutiveOrders
                //.Include(m => m.CreatedAt)
                .Include(m => m.CentralPolicy)
                //.ThenInclude(m => m.Title)
                //.Include(m => m.CentralPolicy)
                //.ThenInclude(m => m.Status)
                //.Include(m => m.CreatedAt)
                //.Include(m => m.AnswerUserId)
                //.Include(m => m.AnswerDetail)
                //.Include(m => m.ExecutiveOrderFiles)
                .Where(m => m.UserId == userId)
                .ToList();

                var users = _context.Users      
                    .Where(m => m.Id == userId)
                    .FirstOrDefault();

            System.Console.WriteLine("export1 : " + userId);

            if (!Directory.Exists(_environment.WebRootPath + "//reportexecutive//"))//ถ้ามีไฟล์อยู่แล้ว
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//reportexecutive//"); //สร้าง Folder reportexecutive ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/reportexecutive/"; // เก็บไฟล์ logo 
            var filename = "รายงานข้อสั่งการผู้บริหาร"+ DateTime.Now.ToString("dd MM yyyy") + ".docx"; // ชื่อไฟล์
            var createfile = filePath + filename; //
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            using (DocX document = DocX.Create(createfile))//สร้าง
            {
                //System.Console.WriteLine("4");
                //Image image = document.AddImage(myImageFullPath);
                //Picture picture = image.CreatePicture(85, 85);
                //var logo = document.InsertParagraph();
                //logo.AppendPicture(picture).Alignment = Alignment.center;


                System.Console.WriteLine("5");

                // Add a title
                document.InsertParagraph("ทะเบียนข้อสั่งการผู้บริหาร").FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold()//ตัวหนา
                    .Alignment = Alignment.center;

            

                var name = document.InsertParagraph(users.Name);
                name.Alignment = Alignment.center;
                name.SpacingAfter(10d);
                name.FontSize(12d);//ขนาดตัวอักษร      
                System.Console.WriteLine("7");

                int dataCount = 0;
                dataCount = Eexcutive1.Count; //เอาที่ select มาใช้
                dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f, 300f, 300f };
                var t = document.InsertTable(dataCount, columnWidths.Length);

                System.Console.WriteLine("8");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row.Cells[0].Paragraphs.First().Append("ลำดับที่");
                row.Cells[1].Paragraphs.First().Append("วัน/เดือน/ปีที่มีข้อสั่งการ");
                row.Cells[2].Paragraphs.First().Append("ประเด็น/เรื่อง");
                row.Cells[3].Paragraphs.First().Append("สถานะเรื่อง");
                row.Cells[4].Paragraphs.First().Append("วัน/เดือน/ปีที่แจ้งข้อสั่งการ");
                row.Cells[5].Paragraphs.First().Append("ผู้รับข้อสั่งการ");
                row.Cells[6].Paragraphs.First().Append("การดำเนินการ");
               
/*
                System.Console.WriteLine("9999: " + model.reportData.Count());
                System.Console.WriteLine("9: " + model.reportData.Length);*/

                //}
                // Add rows in the table.
                int j = 0;
                for (int i = 0; i < Eexcutive1.Count; i++)
                {
                    j += 1;
                    //System.Console.WriteLine(i+=1);

                    System.Console.WriteLine("JJJJJ: " + j);
                    //System.Console.WriteLine("9.1: ");
                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString());
                    //System.Console.WriteLine("9.2: " + Eexcutive1[i].CreatedAt);
                    t.Rows[j].Cells[1].Paragraphs[0].Append(Eexcutive1[i].CreatedAt.ToString());
                   // System.Console.WriteLine("9.3: " + model.reportData[i].suggestion);
                    t.Rows[j].Cells[2].Paragraphs[0].Append(Eexcutive1[i].CentralPolicy.Title);
                    // System.Console.WriteLine("9.4: " +Eexcutive1[i].CentralPolicy.Title);
                    t.Rows[j].Cells[3].Paragraphs[0].Append(Eexcutive1[i].CentralPolicy.Status);
                    // System.Console.WriteLine("9.5: " + Eexcutive1[i].CentralPolicy.Status);
                    t.Rows[j].Cells[4].Paragraphs[0].Append(Eexcutive1[i].CreatedAt.ToString());
                    // System.Console.WriteLine("9.6: " + Eexcutive1[i].CreatedAt);
                    t.Rows[j].Cells[5].Paragraphs[0].Append(Eexcutive1[i].AnswerUserId);
                    // System.Console.WriteLine("10:  " + Eexcutive1[i].CreatedAt);
                    t.Rows[j].Cells[6].Paragraphs[0].Append(Eexcutive1[i].AnswerDetail);
                    // System.Console.WriteLine("10: +Eexcutive1[i].AnswerDetail");

                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                System.Console.WriteLine("11");
                document.Save();//save เอกสาร
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            
                return Ok(new { data = filename });
            }
            }
        }

    //[HttpPost("export333")]
    //public IActionResult Getexport332([FromBody] UserViewModel body)
    //{
    //    var userId = body.Id;
    //    var Eexcutive1 = _context.ExecutiveOrders
    //        .Include(m => m.CentralPolicy)
    //        .Where(m => m.UserId == userId)
    //        .ToList();

    //    var users = _context.Users
    //        .Where(m => m.Id == userId)
    //        .FirstOrDefault();

    //    System.Console.WriteLine("export1 : " + userId);
    //        return Ok(new { data = Eexcutive1 });
    //}

}




