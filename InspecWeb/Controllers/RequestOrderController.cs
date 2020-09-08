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

        //<!-- Get ข้อสั่ง Role แอดมิน-->
        [HttpGet]
        public IEnumerable<RequestOrder> Get()
        {
            var requestorderdata = _context.RequestOrders
                  .Include(m => m.RequestOrderFiles)
             .Include(m => m.RequestOrderAnswers)
             .ThenInclude(m => m.User)
             .Where(m => m.publics == 1)
             .ToList();
            return requestorderdata;
        }
        //<!-- END Get ข้อสั่ง Role แอดมิน-->

        //<!-- Get ข้อสั่ง Role คนที่สั่ง-->
        [HttpGet("commanded/{id}")]
        public IActionResult Commanded(string id)
        {
            var requestorderdata = _context.RequestOrders
                .Include(m => m.RequestOrderFiles)
                .Include(m => m.RequestOrderAnswers)
                .ThenInclude(m => m.User)
                .Where(m => m.UserID == id && m.publics == 1)
                .ToList();

            return Ok(requestorderdata);
        }
        //<!-- END get ข้อสั่ง Role คนที่สั่ง-->

        //<!-- Get ข้อสั่งการของผู้รับ-->
        [HttpGet("answered/{id}")]
        public IActionResult answered(string id)
        {
            var excutiveorderdata = _context.RequestOrders
                .Include(m => m.RequestOrderFiles)
                .Include(m => m.RequestOrderAnswers)
                .ThenInclude(m => m.User)
               .Where(x => x.RequestOrderAnswers.Any(x => x.UserID == id) && x.publics == 1 && x.Draft != 1)
               .ToList();

            return Ok(excutiveorderdata);
        }
        //<!-- END Get ข้อสั่งการของผู้รับ-->

        //<!-- Get รายละเอียดข้อสั่งการ -->
        [HttpGet("requestorderdetail/{id}")]
        public IActionResult requestorderdetail(long id)
        {
            var excutiveorderdetaildata = _context.RequestOrders
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
            return Ok(new { Id = requestordersdata.Id });
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
            return Ok(new { Id = model.id });
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
    }
}


