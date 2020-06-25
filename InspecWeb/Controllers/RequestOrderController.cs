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
    public class RequestOrderController : Controller
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

        public RequestOrderController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        [HttpGet]
        public IEnumerable<RequestOrder> Get()
        {
            var requestorderdata = _context.RequestOrders
                .Where(m => m.publics == 1)
                .ToList();
            return requestorderdata;
        }


        [HttpGet("commanded/{id}")]
        public IActionResult Commanded(string id)
        {
            var requestorderdata = _context.RequestOrders
                .Include(m => m.User_Answer_by)
                .Include(m => m.RequestOrderFiles)
                .Include(m => m.AnswerRequestOrderFile)
                .Where(m => m.Commanded_by == id && m.publics == 1)
                .ToList();

            return Ok(requestorderdata);
        }

        [HttpGet("answered/{id}")]
        public IActionResult answered(string id)
        {
            var requestorderdata = _context.RequestOrders
                .Include(m => m.User_Answer_by)
                 .Include(m => m.RequestOrderFiles)
                .Include(m => m.AnswerRequestOrderFile)
                .Where(m => m.Answer_by == id && m.publics == 1)
                .ToList();

            return Ok(requestorderdata);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {

            var centralpolicydata = _context.CentralPolicies
                .Include(m => m.CentralPolicyProvinces)
                .ThenInclude(x => x.Province)
                .Include(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicyFiles)
                //.Include(m => m.RequestOrders)
                .Include(m => m.Subjects)
                .ThenInclude(m => m.Subquestions)
            /*.Where(m => m.Id == id).FirstOrDefault();*/
            .Where(m => m.Class == "แผนการตรวจประจำปี" && m.Id == id).ToList();

            // .First()        
            return Ok(centralpolicydata);
            //return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] RequestViewModel model)
        {
            /* System.Console.WriteLine("centralpolicy: " + model.CentralpolicyId);
             System.Console.WriteLine("provinceid: " + model.ProvinceId);
             System.Console.WriteLine("Name: " + model.Name);*/
            var date = DateTime.Now;
            var requestorderdata = new RequestOrder
            {

                Commanded_by = model.Commanded_by,
                Subject = model.Subject,
                Subjectdetail = model.Subjectdetail,
                Status = "แจ้งแล้ว",
                CreatedAt = date,
                Commanded_date = model.Commanded_date,
                publics = 1,
                Answer_by = model.Answer_by

            };

            _context.RequestOrders.Add(requestorderdata);
            _context.SaveChanges();

            if (!Directory.Exists(_environment.WebRootPath + "//requestfile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//requestfile//"); //สร้าง Folder Upload ใน wwwroot
            }
            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//requestfile//";


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

                    var RequestOrderFile = new RequestOrderFile
                    {
                        RequestOrderId = requestorderdata.Id,
                        Name = random + filename,
                    };
                    _context.RequestOrderFiles.Add(RequestOrderFile);
                    _context.SaveChanges();
                    /* System.Console.WriteLine("Sucess");*/
                }
            }
            return Ok(new { status = true });
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
        public IActionResult Getrequest(long id)
        {
            var requestOrderdata = _context.RequestOrders;
                /*.Include(m => m.DetailExecutiveOrder)*/
                //.Include(m => m.CentralPolicy)
                //.Include(m => m.Province)
                //.Include(m => m.RequestOrderFiles)
                //.Include(m => m.AnswerRequestOrderFile)
                //.Where(m => m.CentralPolicyId == id);

            return Ok(requestOrderdata);
            //return "value";
        }
        [HttpGet("view/{id}")]//new///
        public IActionResult Getviewrequest(long id)
        {
            var viewrequestOrderdata = _context.RequestOrders;
                //.Include(m => m.Province)
                //.Include(m => m.UserId)
                //.Include(m => m.CreatedAt)
                //.Include(m => m.RequestOrderFiles)
                //.Include(m => m.AnswerRequestOrderFile)
                //.Where(m => m.CentralPolicyId == id);

            return Ok(viewrequestOrderdata);
            //return "value";
        }

        [HttpGet("detailforinspector/{id}/{userid}")]
        public IActionResult Getrequestrole3(long id, string userid)
        {
            var provinceId = _context.UserProvinces
                .Where(x => x.UserID == userid)
                .Select(x => x.ProvinceId)
                .FirstOrDefault();

            var requestOrderdata = _context.RequestOrders;
                /*.Include(m => m.DetailExecutiveOrder)*/
                //.Include(m => m.Province)
                //.Include(m => m.RequestOrderFiles)
                //.Where(m => m.CentralPolicyId == id && m.ProvinceId == provinceId);

            return Ok(requestOrderdata);
            //return "value";
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] RequestViewModel model)
        {
            /* System.Console.WriteLine("detailrequestorder: " + model.id);
             System.Console.WriteLine("AnswerDetail: " + model.AnswerDetail);
             System.Console.WriteLine("AnswerProblem: " + model.AnswerProblem);
             System.Console.WriteLine("AnswerCounsel: " + model.AnswerCounsel);
             System.Console.WriteLine("AnswerRequestorder: " + model.files);*/
            System.Console.WriteLine("momotest: " + model.id);

           var date = DateTime.Now;
            var requestordersdata = _context.RequestOrders.Find(model.id);
            {
                requestordersdata.Answerdetail = model.Answerdetail;
                requestordersdata.AnswerProblem = model.AnswerProblem;
                requestordersdata.AnswerCounsel = model.AnswerCounsel;
                requestordersdata.Status = "ตอบกลับเรียบร้อย";
                requestordersdata.beaware_date = date;

            };

            _context.Entry(requestordersdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            if (!Directory.Exists(_environment.WebRootPath + "//requestfile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//requestfile//"); //สร้าง Folder Upload ใน wwwroot
            }
            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//requestfile//";

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
                    var AnswerRequestOrderFile = new AnswerRequestOrderFile
                    {
                        RequestOrderId = model.id,
                        Name = random + filename
                    };
                    _context.AnswerRequestOrderFiles.Add(AnswerRequestOrderFile);
                    _context.SaveChanges();
                    /*System.Console.WriteLine("Success");*/
                }
            }
            return Ok(new { status = true });
        }
        [HttpGet("ex/{id}")]
        public IActionResult GetData(string id)
        {
            //System.Console.WriteLine(id);
            var userprovince = _context.UserProvinces
                               .Where(m => m.UserID == id)
                               .ToList();



            var inspectionplans = _context.CentralPolicyProvinces
                .Include(m => m.CentralPolicy)
                .ThenInclude(x => x.FiscalYear)
                .Include(m => m.CentralPolicy)
                .ThenInclude(x => x.CentralPolicyDates)
                .ToList();

            List<object> termsList = new List<object>();
            foreach (var inspectionplan in inspectionplans)
            {
                for (int i = 0; i < userprovince.Count(); i++)
                {
                    if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                        termsList.Add(inspectionplan);
                    //System.Console.WriteLine(userprovince[i].ProvinceId);
                }
            }

            return Ok(termsList);

        }
        [HttpGet("centralpolicyprovinceid/{id}")]
        public IActionResult Getcentralpolicyprovinceid(long id)
        {
            var data = _context.CentralPolicyProvinces
                .Include(m => m.Province)
                .Where(m => m.CentralPolicyId == id).ToList();
            //System.Console.WriteLine("tttt",data);
            return Ok(data);
        }
        public void CreateReport(List<object> centralpolicydata, string typeId)
        {
            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "DOC" + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            if (typeId == "1")
            {
                System.Console.WriteLine("in create");
                using (DocX document = DocX.Create(createfile))
                {
                    Image image = document.AddImage(myImageFullPath);
                    Picture picture = image.CreatePicture(90, 90);
                    var logo = document.InsertParagraph();
                    logo.AppendPicture(picture).Alignment = Alignment.left;

                    // Add a title
                    document.InsertParagraph("Columns width").FontSize(15d).SpacingAfter(50d).Alignment = Alignment.center;

                    // Insert a title paragraph.
                    var p = document.InsertParagraph("In the following table, the cell's left margin has been removed for rows 2-6 as well as the top/bottom table's borders.").Bold();
                    p.Alignment = Alignment.center;
                    p.SpacingAfter(40d);

                    // Add a table in a document of 1 row and 3 columns.
                    var columnWidths = new float[] { 200f, 200f, 200f, 200f, 200f, 200f, 200f };
                    var t = document.InsertTable(1, columnWidths.Length);

                    // Set the table's column width and background 
                    t.SetWidths(columnWidths);
                    t.AutoFit = AutoFit.Contents;

                    var row = t.Rows.First();

                    // Fill in the columns of the first row in the table.
                    //for (int i = 0; i < row.Cells.Count; ++i)
                    //{
                    row.Cells[0].Paragraphs.First().Append("ประเด็นการตรวจติดตาม");
                    row.Cells[1].Paragraphs.First().Append("ข้อค้นพบ/ประเด็นปัญหา/ผลการตรวจ");
                    row.Cells[2].Paragraphs.First().Append("หน่วยรับตรวจ/หน่วยงานที่รับผิดชอบ");
                    row.Cells[3].Paragraphs.First().Append("ข้อเสนอแนะของผู้ตรวจราชการ");
                    row.Cells[4].Paragraphs.First().Append("รายงานผลการดำเนินการของหน่วยรับตรวจ");
                    row.Cells[5].Paragraphs.First().Append("เอกสารแนบ (ไฟล์)");
                    row.Cells[6].Paragraphs.First().Append("ความเห็นของ ที่ปรึกษา ผต.ภาคประชาชน");

                    //}

                    // Add rows in the table.
                    for (int i = 0; i < 5; i++)
                    {
                        var newRow = t.InsertRow();

                        // Fill in the columns of the new rows.
                        for (int j = 0; j < newRow.Cells.Count; ++j)
                        {
                            var newCell = newRow.Cells[j];
                            newCell.Paragraphs.First().Append("test" + i);
                            // Remove the left margin of the new cells.
                            newCell.MarginLeft = 0;
                        }
                    }

                    // Set a blank border for the table's top/bottom borders.
                    var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                    //t.SetBorder(TableBorderType.Bottom, blankBorder);
                    //t.SetBorder(TableBorderType.Top, blankBorder);

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }
        }

        [HttpPost("exportrequest1")] //รายงานแจ้งข้อมูลถึงผู้ตรวจราชการ ***role 5
        public IActionResult Getexport1([FromBody] UserViewModel body)
        {
            var userId = body.Id;
            var Exportrequest1 = _context.RequestOrders
                .Where(m => m.Commanded_by == userId)
                .ToList();

            var users = _context.Users
                .Where(m => m.Id == userId)
                .FirstOrDefault();

            System.Console.WriteLine("export1 : " + userId);

            if (!Directory.Exists(_environment.WebRootPath + "//reportrequest//")) //ถ้ามีไฟล์อยู่แล้ว
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//reportrequest//"); //สร้าง Folder reportrequest ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/reportrequest/"; // เก็บไฟล์ logo 
            var filename = "รายงานแจ้งคำร้องขอจากหน่วยงานของรัฐ" + DateTime.Now.ToString("dd MM yyyy") + ".docx"; // ชื่อไฟล์
            var createfile = filePath + filename; //
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            using (DocX document = DocX.Create(createfile)) //สร้าง
            {
                //System.Console.WriteLine("4");
                //Image image = document.AddImage(myImageFullPath);
                //Picture picture = image.CreatePicture(85, 85);
                //var logo = document.InsertParagraph();
                //logo.AppendPicture(picture).Alignment = Alignment.center;

                System.Console.WriteLine("5");

                // Add a title
                document.InsertParagraph("ทะเบียนคำร้องขอจากหน่วยงานของรัฐ/หน่วยรับตรวจ").FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold() //ตัวหนา
                    .Alignment = Alignment.center;

                var name = document.InsertParagraph(users.Name);
                name.Alignment = Alignment.center;
                name.SpacingAfter(10d);
                name.FontSize(12d); //ขนาดตัวอักษร      
                System.Console.WriteLine("7");

                int dataCount = 0;
                dataCount = Exportrequest1.Count; //เอาที่ select มาใช้
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
                row.Cells[1].Paragraphs.First().Append("วัน/เดือน/ปีที่มีคำร้องขอ");
                row.Cells[2].Paragraphs.First().Append("ประเด็น/เรื่อง");
                row.Cells[3].Paragraphs.First().Append("สถานะเรื่อง");
                row.Cells[4].Paragraphs.First().Append("ผู้รับคำร้องขอ/หน่อยงาน");
                row.Cells[5].Paragraphs.First().Append("การดำเนินการ");
                row.Cells[6].Paragraphs.First().Append("วัน/เดือน/ปีที่แจ้งข้อสั่งการ");
                /*
                                System.Console.WriteLine("9999: " + model.reportData.Count());
                                System.Console.WriteLine("9: " + model.reportData.Length);*/

                //}
                // Add rows in the table.
                int j = 0;
                for (int i = 0; i < Exportrequest1.Count; i++)
                {
                    j += 1;
                    //System.Console.WriteLine(i+=1);
                    var username = _context.ApplicationUsers
                        .Where(m => m.Id == Exportrequest1[i].Commanded_by)
                        .Select(m => m.Name)
                        .FirstOrDefault();
                    System.Console.WriteLine("JJJJJ: " + j);
                    //System.Console.WriteLine("9.1: ");
                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString());
                    //System.Console.WriteLine("9.2: " + Eexcutive1[i].CreatedAt);
                    t.Rows[j].Cells[1].Paragraphs[0].Append(Exportrequest1[i].Commanded_date.ToString());
                    // System.Console.WriteLine("9.3: " + model.reportData[i].suggestion);
                    t.Rows[j].Cells[2].Paragraphs[0].Append(Exportrequest1[i].Subject);
                    // System.Console.WriteLine("9.4: " +Eexcutive1[i].CentralPolicy.Title);
                    t.Rows[j].Cells[3].Paragraphs[0].Append(Exportrequest1[i].Status);
                    // System.Console.WriteLine("9.5: " + Eexcutive1[i].CentralPolicy.Status);
                    // System.Console.WriteLine("9.6: " + Eexcutive1[i].CreatedAt);
                    t.Rows[j].Cells[4].Paragraphs[0].Append(username);
                    // System.Console.WriteLine("10:  " + Eexcutive1[i].CreatedAt);
                    t.Rows[j].Cells[5].Paragraphs[0].Append(Exportrequest1[i].Answerdetail);
                    // System.Console.WriteLine("10: +Eexcutive1[i].AnswerDetail");
                    t.Rows[j].Cells[6].Paragraphs[0].Append(Exportrequest1[i].CreatedAt.ToString());
                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                System.Console.WriteLine("11");
                document.Save(); //save เอกสาร
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                return Ok(new { data = filename });
            }
        }

        [HttpPost("exportrequest3")] //รายงานแจ้งข้อมูลถึงผู้ตรวจราชการ ***role 3
        public IActionResult Getexport3([FromBody] UserViewModel body)
        {
            var userId = body.Id;
            var Exportrequest3 = _context.RequestOrders
                .Where(m => m.Answer_by == userId)
                .ToList();

            var users = _context.Users
                .Where(m => m.Id == userId)
                .FirstOrDefault();

            System.Console.WriteLine("export3 : " + userId);

            if (!Directory.Exists(_environment.WebRootPath + "//reportrequest//")) //ถ้ามีไฟล์อยู่แล้ว
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//reportrequest//"); //สร้าง Folder reportrequest ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/reportrequest/"; // เก็บไฟล์ logo 
            var filename = "รายงานแจ้งคำร้องขอจากหน่วยงานของรัฐ" + DateTime.Now.ToString("dd MM yyyy") + ".docx"; // ชื่อไฟล์
            var createfile = filePath + filename; //
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            using (DocX document = DocX.Create(createfile)) //สร้าง
            {
                //System.Console.WriteLine("4");
                //Image image = document.AddImage(myImageFullPath);
                //Picture picture = image.CreatePicture(85, 85);
                //var logo = document.InsertParagraph();
                //logo.AppendPicture(picture).Alignment = Alignment.center;

                System.Console.WriteLine("5");

                // Add a title
                document.InsertParagraph("ทะเบียนคำร้องขอจากหน่วยงานของรัฐ/หน่วยรับตรวจ").FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold() //ตัวหนา
                    .Alignment = Alignment.center;

                var name = document.InsertParagraph(users.Name);
                name.Alignment = Alignment.center;
                name.SpacingAfter(10d);
                name.FontSize(12d); //ขนาดตัวอักษร      
                System.Console.WriteLine("7");

                int dataCount = 0;
                dataCount = Exportrequest3.Count; //เอาที่ select มาใช้
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
                row.Cells[1].Paragraphs.First().Append("วัน/เดือน/ปีที่มีคำร้องขอ");
                row.Cells[2].Paragraphs.First().Append("ผู้แจ้งคำร้องขอ/หน่อยงาน");
                row.Cells[3].Paragraphs.First().Append("ประเด็น/เรื่อง");
                row.Cells[4].Paragraphs.First().Append("สถานะเรื่อง");
                row.Cells[5].Paragraphs.First().Append("วัน/เดือน/ปีที่แจ้งข้อสั่งการ");
                row.Cells[6].Paragraphs.First().Append("การดำเนินการ");
                /*
                                System.Console.WriteLine("9999: " + model.reportData.Count());
                                System.Console.WriteLine("9: " + model.reportData.Length);*/

                //}
                // Add rows in the table.
                int j = 0;
                for (int i = 0; i < Exportrequest3.Count; i++)
                {
                    j += 1;
                    //System.Console.WriteLine(i+=1);
                    var username = _context.ApplicationUsers
                        .Where(m => m.Id == Exportrequest3[i].Commanded_by)
                        .Select(m => m.Name)
                        .FirstOrDefault();
                    System.Console.WriteLine("JJJJJ: " + j);
                    //System.Console.WriteLine("9.1: ");
                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString());
                    //System.Console.WriteLine("9.2: " + Eexcutive1[i].CreatedAt);
                    t.Rows[j].Cells[1].Paragraphs[0].Append(Exportrequest3[i].Commanded_date.ToString());
                    // System.Console.WriteLine("9.3: " + model.reportData[i].suggestion);
                    t.Rows[j].Cells[2].Paragraphs[0].Append(username);
                    // System.Console.WriteLine("9.4: " +Eexcutive1[i].CentralPolicy.Title);
                    t.Rows[j].Cells[3].Paragraphs[0].Append(Exportrequest3[i].Subject);
                    // System.Console.WriteLine("9.5: " + Eexcutive1[i].CentralPolicy.Status);
                    t.Rows[j].Cells[4].Paragraphs[0].Append(Exportrequest3[i].Status);
                    // System.Console.WriteLine("9.6: " + Eexcutive1[i].CreatedAt);
                    t.Rows[j].Cells[5].Paragraphs[0].Append(Exportrequest3[i].CreatedAt.ToString());
                    // System.Console.WriteLine("10:  " + Eexcutive1[i].CreatedAt);
                    t.Rows[j].Cells[6].Paragraphs[0].Append(Exportrequest3[i].Answerdetail);
                    // System.Console.WriteLine("10: +Eexcutive1[i].AnswerDetail");
               
                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                System.Console.WriteLine("11");
                document.Save(); //save เอกสาร
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                return Ok(new { data = filename });
            }
        }

        [HttpGet("exportrequest2/{id}")]
        public IActionResult export2(long id)
        {
            var exportrequestorderdata = _context.RequestOrders
                .Where(m => m.Id == id)
               .FirstOrDefault();

            var users = _context.Users
              .Where(m => m.Id == exportrequestorderdata.Commanded_by)
               .FirstOrDefault();

            var username = _context.ApplicationUsers
                        .Where(m => m.Id == exportrequestorderdata.Answer_by)
                        .Select(m => m.Name)
                        .FirstOrDefault();

            System.Console.WriteLine("export2 : " + id);

            if (!Directory.Exists(_environment.WebRootPath + "//reportrequest//")) //ถ้ามีไฟล์อยู่แล้ว
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//reportrequest//"); //สร้าง Folder reportrequest ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/reportrequest/"; // เก็บไฟล์ logo 
            var filename = "แจ้งคำร้องขอจากหน่วยงานของรัฐ" + DateTime.Now.ToString("dd MM yyyy") + ".docx"; // ชื่อไฟล์
            var createfile = filePath + filename; //
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            using (DocX document = DocX.Create(createfile)) //สร้าง

            {
                //System.Console.WriteLine("4");
                //Image image = document.AddImage(myImageFullPath);
                //Picture picture = image.CreatePicture(85, 85);
                //var logo = document.InsertParagraph();
                //logo.AppendPicture(picture).Alignment = Alignment.center;

                System.Console.WriteLine("5");

                // Add a title
                document.InsertParagraph("รายงานคำร้องขอของหน่วยงานของรัฐ/หน่วยรับตรวจ (รายเรื่อง)").FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold() //ตัวหนา
                    .Alignment = Alignment.center;

                var name = document.InsertParagraph(users.Name);
                name.Alignment = Alignment.center;
                name.SpacingAfter(10d);
                name.FontSize(16d); //ขนาดตัวอักษร
                name.Bold();
                System.Console.WriteLine("7");

                //object v = Bold();
                //string a = "รายละเอียด" + v;
                //string b = exportexcutiveorderdata.Subjectdetail;


                document.InsertParagraph(" วันที่มีคำร้องขอ   " + exportrequestorderdata.Commanded_date + "   วันที่แจ้งคำร้องขอ   " + exportrequestorderdata.CreatedAt).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.center;

                document.InsertParagraph("เรื่อง  " + exportrequestorderdata.Subject).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;


                document.InsertParagraph("ผู้รับคำร้องขอ   " + username).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;


                document.InsertParagraph("รายละเอียด  " + exportrequestorderdata.Subjectdetail).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;

                document.InsertParagraph("\n\n");

                document.InsertParagraph("การดำเนินการตามคำร้องขอ").FontSize(16d)
                   .SpacingBefore(15d)
                   .SpacingAfter(15d)
                   .Bold() //ตัวหนา
                   .Alignment = Alignment.center;

                document.InsertParagraph("วันที่มีคำร้องขอ    " + exportrequestorderdata.Commanded_date + "  วันที่รับทราบคำร้องขอ   " + exportrequestorderdata.beaware_date).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.center;

                document.InsertParagraph("รายละเอียด " + exportrequestorderdata.Answerdetail).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;

                document.InsertParagraph("ปัญหา/อุปสรรค " + exportrequestorderdata.AnswerProblem).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;

                document.InsertParagraph("ข้อเสนอแนะ " + exportrequestorderdata.AnswerCounsel).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;

                System.Console.WriteLine("11");
                document.Save(); //save เอกสาร
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                return Ok(new { data = filename });
            }
        }

    }
}


