using System;
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

        //<!-- Get ข้อสั่ง Role แอดมิน-->
        [HttpGet]
        public IEnumerable<RequestOrder> Get()
        {
            var requestorderdata = _context.RequestOrders
                 .Include(m => m.User)
                 .Include(m => m.RequestOrderFiles)
                 .Include(m => m.RequestOrderAnswers)
                 .ThenInclude(m => m.User)
                 .Where(m => m.publics == 1)
                 .OrderByDescending(m => m.Id)
                 .ToList();
            return requestorderdata;
        }
        //<!-- END Get ข้อสั่ง Role แอดมิน-->

        //<!-- Get ข้อสั่ง Role คนที่สั่ง-->
        [HttpGet("commanded/{id}")]
        public IActionResult Commanded(string id)
        {
            var requestorderdata = _context.RequestOrders
                .Include(m => m.User)
                .Include(m => m.RequestOrderFiles)
                .Include(m => m.RequestOrderAnswers)
                .ThenInclude(m => m.User)
                .Where(m => m.UserID == id && m.publics == 1)
                .OrderByDescending(m => m.Id)
                .ToList();

            return Ok(requestorderdata);
        }
        //<!-- END get ข้อสั่ง Role คนที่สั่ง-->

        //<!-- Get ข้อสั่งการของผู้รับ-->
        [HttpGet("answered/{id}")]
        public IActionResult answered(string id)
        {
            var excutiveorderdata = _context.RequestOrders
                .Include(m => m.User)
                .Include(m => m.RequestOrderFiles)
                .Include(m => m.RequestOrderAnswers)
                .ThenInclude(m => m.User)
               .Where(x => x.RequestOrderAnswers.Any(x => x.UserID == id) && x.publics == 1 && x.Draft != 1)
               .OrderByDescending(m => m.Id)
               .ToList();

            return Ok(excutiveorderdata);
        }
        //<!-- END Get ข้อสั่งการของผู้รับ-->

        //<!-- Get รายละเอียดข้อสั่งการ -->
        [HttpGet("requestorderdetail/{id}")]
        public IActionResult requestorderdetail(long id)
        {
            var excutiveorderdetaildata = _context.RequestOrders
              .Include(m => m.User)
              .Include(m => m.RequestOrderFiles)
              .Include(m => m.RequestOrderAnswers)
              .ThenInclude(m => m.User)
              .Include(m => m.RequestOrderAnswers)
              .ThenInclude(m => m.RequestOrderAnswerDetails)
              .ThenInclude(m => m.AnswerRequestOrderFiles)
              .Where(m => m.Id == id && m.publics == 1)
              .ToList();

            return Ok(excutiveorderdetaildata);
        }
        //<!-- END Get รายละเอียดข้อสั่งการ -->

        //<!-- เพิ่มแจ้งคำร้อง -->
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] RequestViewModel model)
        {

            var date = DateTime.Now;

            var requestordersdata = new RequestOrder
            {
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
            System.Console.WriteLine("2 : ");
            _context.RequestOrders.Add(requestordersdata);
            _context.SaveChanges();

            // <!-- เพิ่มผู่รับข้อสั่งการ  -->
            System.Console.WriteLine("3 : ");
            foreach (var item in model.Answer_by)
            {
                var data = new RequestOrderAnswer
                {
                    RequestOrderId = requestordersdata.Id,
                    Status = "แจ้งแล้ว",
                    UserID = item,
                    publics = 1,
                };
                _context.RequestOrderAnswers.Add(data);
                _context.SaveChanges();
            }
            // <!-- END เพิ่มผู่รับข้อสั่งการ  -->

            System.Console.WriteLine("4 : ");
            // <!-- อัพไฟล์  -->
            if (!Directory.Exists(_environment.WebRootPath + "//requestfile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//requestfile//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "//requestfile//";
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
                        var RequestOrderFile = new RequestOrderFile
                        {
                            RequestOrderId = requestordersdata.Id,
                            Name = random + filename,
                        };
                        _context.RequestOrderFiles.Add(RequestOrderFile);
                        _context.SaveChanges();
                    }
                }
            }
            // <!--END อัพไฟล์  -->
            System.Console.WriteLine("5 : ");
            //return Ok (new { Id = executiveordersdata.Id, Answer_by = executiveordersdata.Answer_by }); //เดียวมาใช้
            return Ok(new { Id = requestordersdata.Id, title = requestordersdata.Subject });
        }
        //<!-- END เพิ่มแจ้งคำร้อง -->

        //<!-- แก้ไขคำร้อง -->
        [HttpPut("updaterequestorder")]
        public async Task<IActionResult> Put([FromForm] RequestViewModel model)
        {
            var date = DateTime.Now;
            var requestorder = _context.RequestOrders.Find(model.id);
            {

                requestorder.Subject = model.Subject;
                requestorder.Subjectdetail = model.Subjectdetail;
                requestorder.Commanded_date = model.Commanded_date;
                requestorder.Draft = model.Draft;
                requestorder.CreatedAt = date;

            };

            _context.Entry(requestorder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            // <! -- ลบข้อมูลผู้รับข้อสั่งการเดิม -->
            var OLDrequestOrderAnswer = _context.RequestOrderAnswers
                .Where(m => m.RequestOrderId == requestorder.Id);

            _context.RequestOrderAnswers.RemoveRange(OLDrequestOrderAnswer);
            _context.SaveChanges();
            // <! -- END ลบข้อมูลผู้รับข้อสั่งการเดิม -->

            // <!-- แก้ไขผู่รับข้อสั่งการ  -->
            System.Console.WriteLine("3 : ");
            foreach (var item in model.Answer_by)
            {
                var data = new RequestOrderAnswer
                {
                    RequestOrderId = requestorder.Id,
                    Status = "แจ้งแล้ว",
                    UserID = item,
                    publics = 1,
                };
                _context.RequestOrderAnswers.Add(data);
                _context.SaveChanges();
            }
            // <!-- END แก้ไขผู้รับข้อสั่งการ  -->

            if (!Directory.Exists(_environment.WebRootPath + "//requestfile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//requestfile//"); //สร้าง Folder Upload ใน wwwroot
            }
            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//requestfile//";
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
                        var requestorderfile = new RequestOrderFile
                        {
                            RequestOrderId = model.id,
                            Name = random + filename
                        };
                        _context.RequestOrderFiles.Add(requestorderfile);
                        _context.SaveChanges();
                        /*  System.Console.WriteLine("Sucess");*/
                    }
                }
            }
            return Ok(new { Id = model.id, title = model.Subject });
        }
        //<!-- END แก้ไขคำร้อง -->

        //<!-- ยกเลิกข้อสั่งการ -->
        [HttpPut("cancelrequestorder")]
        public IActionResult PutCancel([FromForm] RequestViewModel model)
        {
            var date = DateTime.Now;
            var requestordersdata = _context.RequestOrders.Find(model.id);
            {
                requestordersdata.Cancel = 1;
                requestordersdata.Canceldetail = model.Canceldetail;
            };

            _context.Entry(requestordersdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { Id = model.id });
        }
        //<!-- END ยกเลิกข้อสั่งการ -->

        //<!-- รับทราบข้อสั่งการ -->
        [HttpPut("gotitrequestorder")]
        public IActionResult Putgotit([FromForm] RequestViewModel model)
        {
            var date = DateTime.Now;
            var requestordersdata = _context.RequestOrders.Find(model.id);
            {
                requestordersdata.Accept = 1;
            };
            _context.Entry(requestordersdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            System.Console.WriteLine("1 : ");
            var requestorderanswerdata = _context.RequestOrderAnswers.Find(model.RequestOrderAnswerId);
            {
                requestorderanswerdata.Status = "รับทราบเรียบร้อย";
                requestorderanswerdata.beaware_date = date;
            };
            _context.Entry(requestorderanswerdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            System.Console.WriteLine("2 : ");
            return Ok(new { Id = model.id });
        }
        //<!-- END รับทราบข้อสั่งการ -->

        //<!-- รายงาน -->
        [HttpPut("answerrequestorder")]
        public async Task<IActionResult> PutanswerExecute([FromForm] RequestViewModel model)
        {
            var date = DateTime.Now;

            var requestorderanswerdata = _context.RequestOrderAnswers.Find(model.RequestOrderAnswerId);
            {
                requestorderanswerdata.Status = "รายงานผลเรียบร้อย";
            };
            _context.Entry(requestorderanswerdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            var requestorderanswerdetaildata = new RequestOrderAnswerDetail
            {
                RequestOrderAnswerId = model.RequestOrderAnswerId,
                Answerdetail = model.Answerdetail,
                AnswerProblem = model.AnswerProblem,
                AnswerCounsel = model.AnswerCounsel,
                create_at = date,
                publics = 1,

            };
            System.Console.WriteLine("1 PutanswerExecute : ");

            _context.RequestOrderAnswerDetails.Add(requestorderanswerdetaildata);
            _context.SaveChanges();

            // <!-- อัพไฟล์  -->
            if (!Directory.Exists(_environment.WebRootPath + "//requestfile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//requestfile//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "//requestfile//";
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
                        var AnswerRequestOrderFile = new AnswerRequestOrderFile
                        {
                            RequestOrderAnswerDetailId = requestorderanswerdetaildata.Id,
                            Name = random + filename,
                        };
                        _context.AnswerRequestOrderFiles.Add(AnswerRequestOrderFile);
                        _context.SaveChanges();
                    }
                }
            }
            // <!--END อัพไฟล์  -->

            return Ok(new { Id = model.id });
        }
        //<!-- END รายงาน -->

        [HttpDelete("{id}")]
        public void Delete(long id)
        {

            var data1 = _context.RequestOrderAnswers.Where(m => m.RequestOrderId == id);
            _context.RequestOrderAnswers.RemoveRange(data1);
            _context.SaveChanges();

            var data2 = _context.RequestOrderFiles.Where(m => m.RequestOrderId == id);
            _context.RequestOrderFiles.RemoveRange(data2);
            _context.SaveChanges();

            var data3 = _context.RequestOrders.Find(id);
            _context.RequestOrders.Remove(data3);
            _context.SaveChanges();
        }

        [HttpPost("exportrequest1")] 
        public IActionResult Getexport1([FromBody] UserViewModel body)
        {
            var userId = body.Id;
            var random = RandomString(3);
            var users = _context.Users
                .Where(m => m.Id == userId)
                .FirstOrDefault();


            var Eexcutive1 = _context.RequestOrderAnswers
                .Include(m => m.RequestOrder)
                .Include(m => m.RequestOrderAnswerDetails)
                .Where(m => m.RequestOrder.Draft == 0)
                .Where(m => m.RequestOrder.UserID == userId).ToList();


            System.Console.WriteLine("export1 : " + userId);

            if (!Directory.Exists(_environment.WebRootPath + "//reportrequest//")) //ถ้ามีไฟล์อยู่แล้ว
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//reportrequest//"); //สร้าง Folder reportexecutive ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/reportrequest/"; // เก็บไฟล์ logo 
            var filename = "ทะเบียนคำร้องขอจากหน่วยงานของรัฐหน่วยรับตรวจ" + DateTime.Now.ToString("dd MM yyyy") + random +".docx"; // ชื่อไฟล์
            var createfile = filePath + filename; //
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            using (DocX document = DocX.Create(createfile)) //สร้าง
            {

                document.SetDefaultFont(new Xceed.Document.NET.Font("ThSarabunNew"));
                document.AddHeaders();
                document.AddFooters();

                // Force the first page to have a different Header and Footer.
                document.DifferentFirstPage = true;
                // Force odd & even pages to have different Headers and Footers.
                document.DifferentOddAndEvenPages = true;

                // Insert a Paragraph into the first Header.
                document.Footers.First.InsertParagraph("วันที่ออกรายงาน: ").Append(DateTime.Now.ToString("dd MMMM yyyy HH:mm", new CultureInfo("th-TH"))).Append(" น.").Alignment = Alignment.right;
                // Insert a Paragraph into the even Header.
                document.Footers.Even.InsertParagraph("วันที่ออกรายงาน: ").Append(DateTime.Now.ToString("dd MMMM yyyy HH:mm", new CultureInfo("th-TH"))).Append(" น.").Alignment = Alignment.right;
                // Insert a Paragraph into the odd Header.
                document.Footers.Odd.InsertParagraph("วันที่ออกรายงาน: ").Append(DateTime.Now.ToString("dd MMMM yyyy HH:mm", new CultureInfo("th-TH"))).Append(" น.").Alignment = Alignment.right;

                // Add the page number in the first Footer.
                document.Headers.First.InsertParagraph("").AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.center;
                // Add the page number in the even Footers.
                document.Headers.Even.InsertParagraph("").AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.center;
                // Add the page number in the odd Footers.
                document.Headers.Odd.InsertParagraph("").AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.center;
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

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                var Date = DateTime.Now.ToString("dd MMMM yyyy");
                var year = document.InsertParagraph("วันที่เรียกรายงาน" + Date);
                year.Alignment = Alignment.center;
                year.SpacingAfter(10d);

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
                row.Cells[1].Paragraphs.First().Append("วัน/เดือน/ปีที่มีคำร้องขอ");
                row.Cells[2].Paragraphs.First().Append("ประเด็น/เรื่อง");
                row.Cells[3].Paragraphs.First().Append("สถานะเรื่อง");
                row.Cells[4].Paragraphs.First().Append("ผู้รับคำร้องขอ");                            
                row.Cells[5].Paragraphs.First().Append("การดำเนินการ");
                row.Cells[6].Paragraphs.First().Append("วัน/เดือน/ที่รับทราบคำร้องขอ");


                // Add rows in the table.
                int j = 0;
                for (int i = 0; i < Eexcutive1.Count; i++)
                {
                    j += 1;
                    //System.Console.WriteLine(i+=1);

                    var users2 = _context.Users
                       .Where(m => m.Id == Eexcutive1[i].UserID)
                       .FirstOrDefault();

                    var Commanded_date = Eexcutive1[i].RequestOrder.Commanded_date.Value.ToString("dd MMMM yyyy");
                    var beaware_date = Eexcutive1[i].beaware_date.Value.ToString("dd MMMM yyyy");
                    System.Console.WriteLine("9.1: " + j);
                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString());               
                    t.Rows[j].Cells[1].Paragraphs[0].Append(Commanded_date);            
                    t.Rows[j].Cells[2].Paragraphs[0].Append(Eexcutive1[i].RequestOrder.Subject);              
                    t.Rows[j].Cells[3].Paragraphs[0].Append(Eexcutive1[i].Status.ToString());                           
                    t.Rows[j].Cells[4].Paragraphs[0].Append(users2.Name);            
                    t.Rows[j].Cells[5].Paragraphs[0].Append("-"); 
                    t.Rows[j].Cells[6].Paragraphs[0].Append(beaware_date);          

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
        [HttpPost("exportrequest3")] 
        public IActionResult Getexport3([FromBody] UserViewModel body)
        {
            System.Console.WriteLine("id " + body.Id);
            var userId = body.Id;
            var random = RandomString(3);
            var Eexcutive3 = _context.RequestOrderAnswers
                .Include(m => m.RequestOrder)
                .Include(m => m.RequestOrderAnswerDetails)
                .Where(m => m.RequestOrder.Draft == 0)
                .Where(m => m.UserID == userId).ToList();

            var users = _context.Users
                .Where(m => m.Id == userId)
                .FirstOrDefault();

            var appDataPath = _environment.WebRootPath + "//reportrequest//";
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }
            System.Console.WriteLine("export_ : " + userId);
            //if (!Directory.Exists(_environment.WebRootPath + "//reportexecutive//")) //ถ้ามีไฟล์อยู่แล้ว
            //{
            //    Directory.CreateDirectory(_environment.WebRootPath + "//reportexecutive//"); //สร้าง Folder reportexecutive ใน wwwroot
            //}

            var filePath = _environment.WebRootPath + "/reportrequest/"; // เก็บไฟล์ logo 
            var filename = "ทะเบียนคำร้องขอจากหน่วยงานของรัฐหน่วยรับตรวจ" + DateTime.Now.ToString("dd MM yyyy") +random+".docx"; // ชื่อไฟล์
            var createfile = filePath + filename; //
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            using (DocX document = DocX.Create(createfile)) //สร้าง
            {
                document.SetDefaultFont(new Xceed.Document.NET.Font("ThSarabunNew"));
                document.AddHeaders();
                document.AddFooters();

                // Force the first page to have a different Header and Footer.
                document.DifferentFirstPage = true;
                // Force odd & even pages to have different Headers and Footers.
                document.DifferentOddAndEvenPages = true;

                // Insert a Paragraph into the first Header.
                document.Footers.First.InsertParagraph("วันที่ออกรายงาน: ").Append(DateTime.Now.ToString("dd MMMM yyyy HH:mm", new CultureInfo("th-TH"))).Append(" น.").Alignment = Alignment.right;
                // Insert a Paragraph into the even Header.
                document.Footers.Even.InsertParagraph("วันที่ออกรายงาน: ").Append(DateTime.Now.ToString("dd MMMM yyyy HH:mm", new CultureInfo("th-TH"))).Append(" น.").Alignment = Alignment.right;
                // Insert a Paragraph into the odd Header.
                document.Footers.Odd.InsertParagraph("วันที่ออกรายงาน: ").Append(DateTime.Now.ToString("dd MMMM yyyy HH:mm", new CultureInfo("th-TH"))).Append(" น.").Alignment = Alignment.right;

                // Add the page number in the first Footer.
                document.Headers.First.InsertParagraph("").AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.center;
                // Add the page number in the even Footers.
                document.Headers.Even.InsertParagraph("").AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.center;
                // Add the page number in the odd Footers.
                document.Headers.Odd.InsertParagraph("").AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.center;
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

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                var Date = DateTime.Now.ToString("dd MMMM yyyy");
                var year = document.InsertParagraph("วันที่เรียกรายงาน" + Date);
                year.Alignment = Alignment.center;
                year.SpacingAfter(10d);

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
                row.Cells[1].Paragraphs.First().Append("วัน/เดือน/ปี ที่มีคำร้องขอ");
                row.Cells[2].Paragraphs.First().Append("ผู้แจ้ง คำร้องขอ/ หน่วยงาน");
                row.Cells[3].Paragraphs.First().Append("ประเด็น/เรื่อง");
                row.Cells[4].Paragraphs.First().Append("สถานะเรื่อง");
                row.Cells[5].Paragraphs.First().Append("วัน/เดือน/ปี ที่รับทราบคำร้องขอ");
                row.Cells[6].Paragraphs.First().Append("การดำเนินการ");

                // Add rows in the table.
                int j = 0;
                for (int i = 0; i < Eexcutive3.Count; i++)
                {
                    j += 1;
                    //System.Console.WriteLine(i+=1);
                    var username = _context.ApplicationUsers
                        .Where(m => m.Id == Eexcutive3[i].RequestOrder.UserID)
                        .Select(m => m.Name)
                        .FirstOrDefault();
                    System.Console.WriteLine("JJJJJ: " + j);

                    var Commanded_date = Eexcutive3[i].RequestOrder.Commanded_date.Value.ToString("dd MMMM yyyy");
                    var beaware_date = Eexcutive3[i].beaware_date.Value.ToString("dd MMMM yyyy");

                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString());
                    t.Rows[j].Cells[1].Paragraphs[0].Append(Commanded_date);
                    t.Rows[j].Cells[2].Paragraphs[0].Append(username);
                    t.Rows[j].Cells[3].Paragraphs[0].Append(Eexcutive3[i].RequestOrder.Subject);
                    t.Rows[j].Cells[4].Paragraphs[0].Append(Eexcutive3[i].Status);
                    t.Rows[j].Cells[5].Paragraphs[0].Append(beaware_date.ToString());
                    t.Rows[j].Cells[6].Paragraphs[0].Append("-");


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

        [HttpGet("exportrequest2/{id}/{userId}")]
        public IActionResult exportrequest2(long id, string userId)
        {

            //var exportexcutiveorderdata = _context.ExecutiveOrders
            //    .Where(m => m.Id == id)
            //   .FirstOrDefault();
            System.Console.WriteLine("1 : " + userId + " : " + id);

            var exportexcutiveorderdata = _context.RequestOrderAnswers
                .Include(m => m.RequestOrder)
                .Include(m => m.RequestOrderAnswerDetails)
                //.Where(m => m.RequestOrder.Draft == 0) 
                .Where(m => m.Id == id) 
                .Where(m => m.UserID == userId)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();

            var detail = _context.RequestOrderAnswerDetails
               .Where(m => m.RequestOrderAnswerId == exportexcutiveorderdata.Id)
               .FirstOrDefault();

            System.Console.WriteLine("1.2 : " + exportexcutiveorderdata.RequestOrder.UserID);
            //ผู้สั่งการ
            var users = _context.Users
              .Where(m => m.Id == exportexcutiveorderdata.RequestOrder.UserID)
               .FirstOrDefault();
            System.Console.WriteLine("1.3 : ");

            //ผู้รับข้อสั่งการ
            var username = _context.ApplicationUsers
                        .Where(m => m.Id == userId)
                        .Select(m => m.Name)
                        .FirstOrDefault();

            System.Console.WriteLine("export2 : " + id);

            if (!Directory.Exists(_environment.WebRootPath + "//reportrequestorder//")) //ถ้ามีไฟล์อยู่แล้ว
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//reportrequestorder//"); //สร้าง Folder reportexecutive ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/reportrequestorder/"; // เก็บไฟล์ logo 
            var filename = DateTime.Now.ToString("dd MM yyyy") + ".docx"; // ชื่อไฟล์
            var createfile = filePath + filename; //
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("3");
            System.Console.WriteLine("in create");
            using (DocX document = DocX.Create(createfile)) //สร้าง

            {
                document.SetDefaultFont(new Xceed.Document.NET.Font("ThSarabunNew"));
                document.AddHeaders();
                document.AddFooters();

                // Force the first page to have a different Header and Footer.
                document.DifferentFirstPage = true;
                // Force odd & even pages to have different Headers and Footers.
                document.DifferentOddAndEvenPages = true;

                // Insert a Paragraph into the first Header.
                document.Footers.First.InsertParagraph("วันที่ออกรายงาน: ").Append(DateTime.Now.ToString("dd MMMM yyyy HH:mm", new CultureInfo("th-TH"))).Append(" น.").Alignment = Alignment.right;
                // Insert a Paragraph into the even Header.
                document.Footers.Even.InsertParagraph("วันที่ออกรายงาน: ").Append(DateTime.Now.ToString("dd MMMM yyyy HH:mm", new CultureInfo("th-TH"))).Append(" น.").Alignment = Alignment.right;
                // Insert a Paragraph into the odd Header.
                document.Footers.Odd.InsertParagraph("วันที่ออกรายงาน: ").Append(DateTime.Now.ToString("dd MMMM yyyy HH:mm", new CultureInfo("th-TH"))).Append(" น.").Alignment = Alignment.right;

                // Add the page number in the first Footer.
                document.Headers.First.InsertParagraph("").AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.center;
                // Add the page number in the even Footers.
                document.Headers.Even.InsertParagraph("").AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.center;
                // Add the page number in the odd Footers.
                document.Headers.Odd.InsertParagraph("").AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.center;
                System.Console.WriteLine("5");


                document.InsertParagraph("รายงานคำร้องขอของหน่วยงานของรัฐ/หน่วยรับตรวจ (รายเรื่อง)").FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold() //ตัวหนา
                    .Alignment = Alignment.center;

                var name = document.InsertParagraph(users.Name);
                name.Alignment = Alignment.center;
                name.SpacingAfter(10d);
                name.FontSize(14d); //ขนาดตัวอักษร
                name.Bold();
                System.Console.WriteLine("7");

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                var Commanded_date = exportexcutiveorderdata.RequestOrder.Commanded_date.Value.ToString("dd MMMM yyyy");
                var CreatedAt = exportexcutiveorderdata.RequestOrder.CreatedAt.Value.ToString("dd MMMM yyyy");
                var beaware_date = exportexcutiveorderdata.beaware_date.Value.ToString("dd MMMM yyyy");


                document.InsertParagraph(" วันที่มีคำร้องขอ" + Commanded_date + "วันที่แจ้งคำร้องขอ" + CreatedAt).FontSize(14d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                .Alignment = Alignment.center;

                document.InsertParagraph("เรื่อง :" + exportexcutiveorderdata.RequestOrder.Subject).FontSize(14d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                .Alignment = Alignment.left;


                document.InsertParagraph("ผู้รับคำร้องขอ  :" + username).FontSize(14d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)

                .Alignment = Alignment.left;

                document.InsertParagraph("รายละเอียด  :").FontSize(14d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                .Alignment = Alignment.left;

                document.InsertParagraph("\n\n");

                document.InsertParagraph("การดำเนินการตามคำร้องขอ ").FontSize(16d)
                   .SpacingBefore(15d)
                   .SpacingAfter(15d)
                   .Bold() //ตัวหนา
                   .Alignment = Alignment.center;

                document.InsertParagraph("วันที่มีคำร้องขอ  " + Commanded_date + "  วันที่รับทราบคำร้องขอ  " + beaware_date).FontSize(14d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                .Alignment = Alignment.center;


                document.InsertParagraph("รายละเอียด :" + detail.Answerdetail).FontSize(14d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;

                document.InsertParagraph("ปัญหา/อุปสรรค :" + detail.AnswerProblem).FontSize(14d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;

                document.InsertParagraph("ข้อเสนอแนะ :" + detail.AnswerCounsel).FontSize(14d)
                .SpacingBefore(15d)
                .SpacingAfter(15d)
                //.Bold() //ตัวหนา
                .Alignment = Alignment.left;

                System.Console.WriteLine("11");
                document.Save(); //save เอกสาร
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                System.Console.WriteLine("12");
                return Ok(new { data = filename });
            }
        }
    }
}


