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
            var excutiveorderdata = _context.RequestOrders
                .Include(m => m.User_Answer_by)
                .Include(m => m.RequestOrderFiles)
                .Include(m => m.AnswerRequestOrderFile)
                .Where(m => m.Commanded_by == id && m.publics == 1)
                .ToList();

            return Ok(excutiveorderdata);
        }

        [HttpGet("answered/{id}")]
        public IActionResult answered(string id)
        {
            var excutiveorderdata = _context.RequestOrders
                .Include(m => m.User_Answer_by)
                 .Include(m => m.RequestOrderFiles)
                .Include(m => m.AnswerRequestOrderFile)
                .Where(m => m.Answer_by == id && m.publics == 1)
                .ToList();

            return Ok(excutiveorderdata);
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
            var cabinedata = new RequestOrder
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

            _context.RequestOrders.Add(cabinedata);
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
                        RequestOrderId = cabinedata.Id,
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

    }
}


