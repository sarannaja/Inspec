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

namespace InspecWeb.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ExecutiveOrderController : Controller {
        public static IWebHostEnvironment _environment;

        private static Random random = new Random ();
        public static string RandomString (int length) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string (Enumerable.Repeat (chars, length)
                .Select (s => s[random.Next (s.Length)]).ToArray ());
        }

        private readonly ApplicationDbContext _context;

        public ExecutiveOrderController (ApplicationDbContext context, IWebHostEnvironment environment) {
            _context = context;
            _environment = environment;
        }

        //<!-- Get ข้อสั่ง Role แอดมิน-->
        [HttpGet]
        public IEnumerable<ExecutiveOrder> Get () {
            var excutiveorderdata = _context.ExecutiveOrders
                .Include(m => m.ExecutiveOrderFiles)
                .Include(m => m.ExecutiveOrderAnswers)
                .ThenInclude(m => m.User)
                .Where(m => m.publics == 1)
                .ToList();
            return excutiveorderdata;
        }
        //<!-- END Get ข้อสั่ง Role แอดมิน-->

        //<!-- Get ข้อสั่ง Role คนที่สั่ง-->
        [HttpGet ("commanded/{id}")]
        public IActionResult Commanded (string id) {
            var excutiveorderdata = _context.ExecutiveOrders     
                .Include (m => m.ExecutiveOrderFiles)
                .Include(m => m.ExecutiveOrderAnswers)
                .ThenInclude(m => m.User)              
                .Where (m => m.UserID == id && m.publics == 1)
                .ToList ();

            return Ok (excutiveorderdata);
        }
        //<!-- END get ข้อสั่ง Role คนที่สั่ง-->

        //<!-- Get ข้อสั่งการของผู้รับ-->
        [HttpGet ("answered/{id}")]
        public IActionResult answered (string id) {
            var excutiveorderdata = _context.ExecutiveOrders           
                .Include (m => m.ExecutiveOrderFiles)
                .Include(m => m.ExecutiveOrderAnswers)
                .ThenInclude(m => m.User)
               .Where(x => x.ExecutiveOrderAnswers.Any(x => x.UserID == id) && x.publics == 1 && x.Draft != 1)
               .ToList ();

            return Ok (excutiveorderdata);
        }
        //<!-- END Get ข้อสั่งการของผู้รับ-->


        //<!-- Get-->
        [HttpGet("ExecutiveOrdersfirst/{id}")]
        public IActionResult ExecutiveOrdersfirst(string id)
        {
            var excutiveorderdata = _context.ExecutiveOrderAnswers
                .Where(user => user.UserID == id)
                .Include(m => m.ExecutiveOrder)
                //.Select(s=> s.Subject )
                .ToArray();
            
            return Ok(excutiveorderdata);
        }
        //<!-- END -->

        //<!-- Get รายละเอียดข้อสั่งการ -->
        [HttpGet("excutiveorderdetail/{id}")]
        public IActionResult excutiveorderdetail(long id)
        {
            var excutiveorderdetaildata = _context.ExecutiveOrders
              .Include(m => m.ExecutiveOrderFiles)
              .Include(m => m.ExecutiveOrderAnswers)
              .ThenInclude(m => m.User)             
              .Include(m => m.ExecutiveOrderAnswers)
              .ThenInclude(m => m.ExecutiveOrderAnswerDetails)
              .ThenInclude(m => m.AnswerExecutiveOrderFiles)
              .Where(m => m.Id == id && m.publics == 1)
              .ToList();

            return Ok(excutiveorderdetaildata);
        }
        //<!-- END Get รายละเอียดข้อสั่งการ -->

        //<!-- เพิ่มข้อสั่งการ -->
        [HttpPost]
        public async Task<IActionResult> Post ([FromForm] ExecutiveViewModel model) {

            var date = DateTime.Now;

            var executiveordersdata = new ExecutiveOrder {
                UserID = model.Commanded_by,
                Subject = model.Subject,
                Subjectdetail = model.Subjectdetail,
                CreatedAt = date,
                Commanded_date = model.Commanded_date,
                publics = 1,
                Draft = model.Draft,
                Accept = 0,
                Cancel = 0,
            };
            System.Console.WriteLine ("2 : ");
            _context.ExecutiveOrders.Add(executiveordersdata);
            _context.SaveChanges();

            // <!-- เพิ่มผู่รับข้อสั่งการ  -->
            System.Console.WriteLine("3 : ");
            foreach (var item in model.Answer_by)
            {
                var data = new ExecutiveOrderAnswer
                {
                    ExecutiveOrderId = executiveordersdata.Id,
                    Status = "แจ้งแล้ว",
                    UserID = item,
                    publics = 1,
                };
                _context.ExecutiveOrderAnswers.Add(data);
                _context.SaveChanges();
                System.Console.WriteLine("3.2 : ");
            }
            // <!-- END เพิ่มผู่รับข้อสั่งการ  -->

            System.Console.WriteLine("4 : ");
            // <!-- อัพไฟล์  -->
            if (!Directory.Exists (_environment.WebRootPath + "//executivefile//")) {
                Directory.CreateDirectory (_environment.WebRootPath + "//executivefile//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "//executivefile//";
            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                {
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }
                        var ExecutiveOrderFile = new ExecutiveOrderFile
                        {
                            ExecutiveOrderId = executiveordersdata.Id,
                            Name = random + filename,
                        };
                        _context.ExecutiveFiles.Add(ExecutiveOrderFile);
                        _context.SaveChanges();
                    }
                }
            }
            // <!--END อัพไฟล์  -->
            System.Console.WriteLine("5 : ");
            //return Ok (new { Id = executiveordersdata.Id, Answer_by = executiveordersdata.Answer_by }); //เดียวมาใช้
            return Ok(new { Id = executiveordersdata.Id});
        }
        //<!-- END เพิ่มข้อสั่งการ -->

        //<!-- แก้ไขข้อสั่งการ -->
        [HttpPut("updateexecutiveorder")]
        public async Task<IActionResult> Put ([FromForm] ExecutiveViewModel model) {
            var date = DateTime.Now;
            var executiveordersdata = _context.ExecutiveOrders.Find (model.id); {
              
                executiveordersdata.Subject = model.Subject;
                executiveordersdata.Subjectdetail = model.Subjectdetail;
                executiveordersdata.Commanded_date = model.Commanded_date;
                executiveordersdata.Draft = model.Draft;
                executiveordersdata.CreatedAt = date;

            };

            _context.Entry (executiveordersdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges ();
            System.Console.WriteLine("1 : ");
            // <! -- ลบข้อมูลผู้รับข้อสั่งการเดิม -->
            var OLDExecutiveOrderAnswer = _context.ExecutiveOrderAnswers
                .Where(m => m.ExecutiveOrderId == executiveordersdata.Id);

            _context.ExecutiveOrderAnswers.RemoveRange(OLDExecutiveOrderAnswer);
            _context.SaveChanges();
            // <! -- END ลบข้อมูลผู้รับข้อสั่งการเดิม -->

            // <!-- แก้ไขผู่รับข้อสั่งการ  -->
            System.Console.WriteLine("3 : ");
            foreach (var item in model.Answer_by)
            {
                var data = new ExecutiveOrderAnswer
                {
                    ExecutiveOrderId = executiveordersdata.Id,
                    Status = "แจ้งแล้ว",
                    UserID = item,
                    publics = 1,
                };
                System.Console.WriteLine("3.2 : ");
                _context.ExecutiveOrderAnswers.Add(data);
                System.Console.WriteLine("3.3 : ");
                _context.SaveChanges();
            }
            // <!-- END แก้ไขผู้รับข้อสั่งการ  -->
            System.Console.WriteLine("4 : ");
            if (!Directory.Exists (_environment.WebRootPath + "//executivefile//")) {
                Directory.CreateDirectory (_environment.WebRootPath + "//executivefile//"); //สร้าง Folder Upload ใน wwwroot
            }
            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//executivefile//";
            if (model.files != null)
            {
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
                            ExecutiveOrderId = model.id,
                            Name = random + filename
                        };
                        _context.ExecutiveFiles.Add(ExecutiveOrderFile);
                        _context.SaveChanges();
                        System.Console.WriteLine("4.2 : ");
                    }
                }
            }
            System.Console.WriteLine("5 : ");
            return Ok (new { Id = model.id });
        }
        //<!-- END แก้ไขข้อสั่งการ -->

        //<!-- ยกเลิกข้อสั่งการ -->
        [HttpPut("cancelexecutiveorder")]
        public IActionResult PutCancelExecute([FromForm] ExecutiveViewModel model)
        {
            var date = DateTime.Now;
            var executiveordersdata = _context.ExecutiveOrders.Find(model.id);
            {
                executiveordersdata.Cancel = 1;
                executiveordersdata.Canceldetail = model.Canceldetail;
            };

            _context.Entry(executiveordersdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { Id = model.id });
        }
        //<!-- END ยกเลิกข้อสั่งการ -->

        //<!-- รับทราบข้อสั่งการ -->
        [HttpPut("gotitexecutiveorder")]
        public IActionResult PutgotitExecute([FromForm] ExecutiveViewModel model)
        {
            var date = DateTime.Now;
            var executiveordersdata = _context.ExecutiveOrders.Find(model.id);
            {
                executiveordersdata.Accept = 1;             
            };
            _context.Entry(executiveordersdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            System.Console.WriteLine("1 : ");
            var executiveorderanswerdata = _context.ExecutiveOrderAnswers.Find(model.ExecutiveOrderAnswerId);
            {
                executiveorderanswerdata.Status = "รับทราบข้อสั่งการ";
               executiveorderanswerdata.beaware_date = date;
            };
            _context.Entry(executiveorderanswerdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            System.Console.WriteLine("2 : ");
            return Ok(new { Id = model.id });
        }
        //<!-- END รับทราบข้อสั่งการ -->

        //<!-- รายงานข้อสั่งการ -->
        [HttpPut("answerexecutiveorder")]
        public async Task<IActionResult> PutanswerExecute([FromForm] ExecutiveViewModel model)
        {
            var date = DateTime.Now;

            var executiveorderanswerdata = _context.ExecutiveOrderAnswers.Find(model.ExecutiveOrderAnswerId);
            {
                executiveorderanswerdata.Status = "รายงานผลเรียบร้อย";           
            };
            _context.Entry(executiveorderanswerdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            var executiveorderanswerdetaildata = new ExecutiveOrderAnswerDetail
            {
                ExecutiveOrderAnswerId = model.ExecutiveOrderAnswerId,
                Answerdetail = model.Answerdetail,
                AnswerProblem = model.AnswerProblem,
                AnswerCounsel = model.AnswerCounsel,
                create_at = date,
                publics = 1,
              
            };
            System.Console.WriteLine("1 PutanswerExecute : ");
          
            _context.ExecutiveOrderAnswerDetails.Add(executiveorderanswerdetaildata);
            _context.SaveChanges();

            // <!-- อัพไฟล์  -->
            if (!Directory.Exists(_environment.WebRootPath + "//executivefile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//executivefile//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "//executivefile//";
            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                {
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }
                        var AnswerExecutiveOrderFile = new AnswerExecutiveOrderFile
                        {
                            ExecutiveOrderAnswerDetailId = executiveorderanswerdetaildata.Id,
                            Name = random + filename,
                        };
                        _context.AnswerExecutiveOrderFiles.Add(AnswerExecutiveOrderFile);
                        _context.SaveChanges();
                    }
                }
            }
            // <!--END อัพไฟล์  -->

            return Ok(new { Id = model.id });
        }
        //<!-- END รายงานข้อสั่งการ -->

        [HttpGet ("ex/{id}")]
        public IActionResult GetData (string id) {
          
            var userprovince = _context.UserProvinces
                .Where (m => m.UserID == id)
                .ToList ();

            var inspectionplans = _context.CentralPolicyProvinces
                .Include (m => m.CentralPolicy)
                .ThenInclude (m => m.CentralPolicyDates)
                .Include (m => m.CentralPolicy)
                .ThenInclude (x => x.FiscalYearNew)
                .ToList ();

            List<object> termsList = new List<object> ();
            foreach (var inspectionplan in inspectionplans) {
                for (int i = 0; i < userprovince.Count (); i++) {
                    if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                        termsList.Add (inspectionplan);
                }
            }

            return Ok (termsList);

        }

        [HttpPost ("export1")] // 9.5.9 (1)รายข้อสั่งการผู้บริหาร
        public IActionResult Getexport1 ([FromBody] UserViewModel body) {
            var userId = body.Id;
            //var Eexcutive12 = _context.ExecutiveOrders
            //    .Where (m => m.UserID == userId)
            //    .ToList ();

            var users = _context.Users
                .Where (m => m.Id == userId)
                .FirstOrDefault ();


            var Eexcutive1 = _context.ExecutiveOrderAnswers
                .Include(m => m.ExecutiveOrder)
                .Include(m => m.ExecutiveOrderAnswerDetails)
                .Where(m => m.ExecutiveOrder.Draft == 0)
                .Where(m => m.ExecutiveOrder.UserID == userId).ToList();
              

            System.Console.WriteLine ("export1 : " + userId);

            if (!Directory.Exists (_environment.WebRootPath + "//reportexecutive//")) //ถ้ามีไฟล์อยู่แล้ว
            {
                Directory.CreateDirectory (_environment.WebRootPath + "//reportexecutive//"); //สร้าง Folder reportexecutive ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/reportexecutive/"; // เก็บไฟล์ logo 
            var filename = "รายงานข้อสั่งการผู้บริหาร" + DateTime.Now.ToString ("dd MM yyyy") + ".docx"; // ชื่อไฟล์
            var createfile = filePath + filename; //
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine ("3");
            System.Console.WriteLine ("in create");
            using (DocX document = DocX.Create (createfile)) //สร้าง
            {
              

                System.Console.WriteLine ("5");

                // Add a title
                document.InsertParagraph ("ทะเบียนคำร้องขอจากหน่วยงานของรัฐ/หน่วยรับตรวจ").FontSize (16d)
                    .SpacingBefore (15d)
                    .SpacingAfter (15d)
                    .Bold () //ตัวหนา
                    .Alignment = Alignment.center;

                var name = document.InsertParagraph (users.Name);
                name.Alignment = Alignment.center;
                name.SpacingAfter (10d);
                name.FontSize (12d); //ขนาดตัวอักษร      
                System.Console.WriteLine ("7");

                int dataCount = 0;
                dataCount = Eexcutive1.Count; //เอาที่ select มาใช้
                dataCount += 1;
                System.Console.WriteLine ("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 300f, 300f, 300f, 300f, 300f, 300f, 300f };
                var t = document.InsertTable (dataCount, columnWidths.Length);

                System.Console.WriteLine ("8");

                // Set the table's column width and background 
                t.SetWidths (columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First ();

                // Fill in the columns of the first row in the table.
                //for (int i = 0; i < row.Cells.Count; ++i)
                //{
                row.Cells[0].Paragraphs.First ().Append ("ลำดับที่");
                row.Cells[1].Paragraphs.First ().Append ("วัน/เดือน/ปีที่มีข้อสั่งการ");
                row.Cells[2].Paragraphs.First ().Append ("ประเด็น/เรื่อง");
                row.Cells[3].Paragraphs.First ().Append ("สถานะเรื่อง");
                row.Cells[4].Paragraphs.First ().Append ("วัน/เดือน/ปีที่แจ้งข้อสั่งการ");
                row.Cells[5].Paragraphs.First ().Append ("ผู้รับข้อสั่งการ");
                row.Cells[6].Paragraphs.First ().Append ("การดำเนินการ");

                /*
                                System.Console.WriteLine("9999: " + model.reportData.Count());
                                System.Console.WriteLine("9: " + model.reportData.Length);*/

                //}
                // Add rows in the table.
                int j = 0;
                for (int i = 0; i < Eexcutive1.Count; i++) {
                    j += 1;
                    //System.Console.WriteLine(i+=1);

                    var users2 = _context.Users
                       .Where(m => m.Id == Eexcutive1[i].UserID)
                       .FirstOrDefault();
                    System.Console.WriteLine ("JJJJJ: " + j);
                    //System.Console.WriteLine("9.1: ");
                    t.Rows[j].Cells[0].Paragraphs[0].Append (j.ToString ()); //ลำดับ                
                    t.Rows[j].Cells[1].Paragraphs[0].Append (Eexcutive1[i].ExecutiveOrder.Commanded_date.ToString ());// วัน/เดือน/ปีที่มีข้อสั่งการ                  
                    t.Rows[j].Cells[2].Paragraphs[0].Append (Eexcutive1[i].ExecutiveOrder.Subject); // ประเด็น/เรื่อง                 
                    t.Rows[j].Cells[3].Paragraphs[0].Append (Eexcutive1[i].Status.ToString()); // สถานะเรื่อง                 
                    t.Rows[j].Cells[4].Paragraphs[0].Append (Eexcutive1[i].ExecutiveOrder.CreatedAt.ToString ()); // วัน/เดือน/ปีที่แจ้งข้อสั่งการ           
                    t.Rows[j].Cells[5].Paragraphs[0].Append (users2.Name);   //ผู้รับข้อสั่งการ             
                    t.Rows[j].Cells[6].Paragraphs[0].Append ("-"); //การดำเนินการ


                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border (BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                System.Console.WriteLine ("11");
                document.Save (); //save เอกสาร
                Console.WriteLine ("\tCreated: InsertHorizontalLine.docx\n");

                return Ok (new { data = filename });
            }
        }
        [HttpPost("export3")] // 9.5.9 (1)รายข้อสั่งการผู้บริหาร(ผู้รับข้อสั่งการ)
        public IActionResult Getexport2([FromBody] UserViewModel body)
        {
            System.Console.WriteLine("id " + body.Id);
            var userId = body.Id;

            var Eexcutive3 = _context.ExecutiveOrderAnswers
                .Include(m => m.ExecutiveOrder)
                .Include(m => m.ExecutiveOrderAnswerDetails)
                .Where(m => m.ExecutiveOrder.Draft == 0)
                .Where(m => m.ExecutiveOrder.UserID == userId).ToList();

            var users = _context.Users
                .Where(m => m.Id == userId)
                .FirstOrDefault();

            System.Console.WriteLine("export_ : " + userId);

            if (!Directory.Exists(_environment.WebRootPath + "//reportexecutive//")) //ถ้ามีไฟล์อยู่แล้ว
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//reportexecutive//"); //สร้าง Folder reportexecutive ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/reportexecutive/"; // เก็บไฟล์ logo 
            var filename = "รายงานข้อสั่งการผู้บริหาร" + DateTime.Now.ToString("dd MM yyyy") + ".docx"; // ชื่อไฟล์
            var createfile = filePath + filename; //
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            using (DocX document = DocX.Create(createfile)) //สร้าง
            {
             
                System.Console.WriteLine("5");

                // Add a title
                document.InsertParagraph("ทะเบียนข้อสั่งการผู้บริหาร").FontSize(16d)
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
                dataCount = Eexcutive3.Count; //เอาที่ select มาใช้
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
                row.Cells[2].Paragraphs.First().Append("ผู้ข้อสั่งการ");
                row.Cells[3].Paragraphs.First().Append("ประเด็น/เรื่อง");
                row.Cells[4].Paragraphs.First().Append("สถานะเรื่อง");
                row.Cells[5].Paragraphs.First().Append("วัน/เดือน/ปีที่รับทราบข้อสั่งการ");
                row.Cells[6].Paragraphs.First().Append("การดำเนินการ");

                // Add rows in the table.
                int j = 0;
                for (int i = 0; i < Eexcutive3.Count; i++)
                {
                    j += 1;
                    //System.Console.WriteLine(i+=1);
                    var username = _context.ApplicationUsers
                        .Where(m => m.Id == Eexcutive3[i].ExecutiveOrder.UserID)
                        .Select(m => m.Name)
                        .FirstOrDefault();
                    System.Console.WriteLine("JJJJJ: " + j);
  
                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString());
                    t.Rows[j].Cells[1].Paragraphs[0].Append(Eexcutive3[i].ExecutiveOrder.Commanded_date.ToString());
                    t.Rows[j].Cells[2].Paragraphs[0].Append(username);
                    t.Rows[j].Cells[3].Paragraphs[0].Append(Eexcutive3[i].ExecutiveOrder.Subject);
                    t.Rows[j].Cells[4].Paragraphs[0].Append(Eexcutive3[i].Status);
                    t.Rows[j].Cells[5].Paragraphs[0].Append(Eexcutive3[i].ExecutiveOrder.CreatedAt.ToString());
                    t.Rows[j].Cells[6].Paragraphs[0].Append("");


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

        [HttpGet("export2/{id}/{userId}")]
        public IActionResult export2(long id ,string userId)
        {
           
            //var exportexcutiveorderdata = _context.ExecutiveOrders
            //    .Where(m => m.Id == id)
            //   .FirstOrDefault();

            var exportexcutiveorderdata = _context.ExecutiveOrderAnswers
                .Include(m => m.ExecutiveOrder)
                .Include(m => m.ExecutiveOrderAnswerDetails)
                .Where(m => m.ExecutiveOrder.Draft == 0)
                .Where(m => m.ExecutiveOrder.UserID == userId)
                .FirstOrDefault();

            //ผู้สั่งการ
            var users = _context.Users
              .Where(m => m.Id == exportexcutiveorderdata.ExecutiveOrder.UserID)
               .FirstOrDefault();

            //ผู้รับข้อสั่งการ
            var username = _context.ApplicationUsers
                        .Where(m => m.Id == userId)
                        .Select(m => m.Name)
                        .FirstOrDefault();

            System.Console.WriteLine("export2 : " + id);

            if (!Directory.Exists(_environment.WebRootPath + "//reportexecutive//")) //ถ้ามีไฟล์อยู่แล้ว
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//reportexecutive//"); //สร้าง Folder reportexecutive ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/reportexecutive/"; // เก็บไฟล์ logo 
            var filename = "รายงานข้อสั่งการผู้บริหาร" + DateTime.Now.ToString("dd MM yyyy") + ".docx"; // ชื่อไฟล์
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
                document.InsertParagraph("รายงานข้อสั่งการของผู้บริหารและการดำเนินการตามข้อสั่งการ (รายเรื่อง)").FontSize(16d)
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


                document.InsertParagraph(" วันที่มีข้อสั่งการ   "+exportexcutiveorderdata.ExecutiveOrder.Commanded_date  + "   วันที่แจ้งข้อสั่งการ   " + exportexcutiveorderdata.ExecutiveOrder.CreatedAt).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.center;

                document.InsertParagraph("เรื่อง  "+ exportexcutiveorderdata.ExecutiveOrder.Subject).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;

                
                document.InsertParagraph("ผู้รับเรื่อง   " + username).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;

                document.InsertParagraph("รายละเอียด  " ).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;

                document.InsertParagraph("\n\n");

                document.InsertParagraph("การดำเนินการตามข้อสั่งการ").FontSize(16d)
                   .SpacingBefore(15d)
                   .SpacingAfter(15d)
                   .Bold() //ตัวหนา
                   .Alignment = Alignment.center;

                document.InsertParagraph("วันที่มีข้อสั่งการ   " + exportexcutiveorderdata.ExecutiveOrder.Commanded_date + "  วันที่แจ้งข้อสั่งการ   " ).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.center;

                document.InsertParagraph("รายละเอียด ").FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;

                document.InsertParagraph("ปัญหา/อุปสรรค " ).FontSize(16d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;

                document.InsertParagraph("ข้อเสนอแนะ " ).FontSize(16d)
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