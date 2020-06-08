using System;
using System.Collections.Generic;
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
            var Requestdata = _context.RequestOrders.ToList();
            return Requestdata;

        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {

            var centralpolicydata = _context.CentralPolicies
                .Include(m => m.CentralPolicyProvinces)
                .ThenInclude(x => x.Province)
                .Include(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicyFiles)
                .Include(m => m.RequestOrders)
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

                DetailRequestOrder = model.Name,
                CentralPolicyId = model.CentralpolicyId,
                ProvinceId = model.ProvinceId,
                UserId = model.UserId,
                CreatedAt = date
                
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
            var requestOrderdata = _context.RequestOrders
                /*.Include(m => m.DetailExecutiveOrder)*/
                .Include(m => m.CentralPolicy)
                .Include(m => m.Province)
                .Include(m => m.RequestOrderFiles)
                .Include(m => m.AnswerRequestOrderFile)
                .Where(m => m.CentralPolicyId == id);

            return Ok(requestOrderdata);
            //return "value";
        }
        [HttpGet("view/{id}")]//new///
        public IActionResult Getviewrequest(long id)
        {
            var viewrequestOrderdata = _context.RequestOrders
                .Include(m => m.Province)
                .Include(m => m.UserId)
                .Include(m => m.CreatedAt)
                .Include(m => m.RequestOrderFiles)
                .Include(m => m.AnswerRequestOrderFile)
                .Where(m => m.CentralPolicyId == id);

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

            var requestOrderdata = _context.RequestOrders
                /*.Include(m => m.DetailExecutiveOrder)*/
                .Include(m => m.Province)
                .Include(m => m.RequestOrderFiles)
                .Where(m => m.CentralPolicyId == id && m.ProvinceId == provinceId);

            return Ok(requestOrderdata);
            //return "value";
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Put([FromForm] RequestViewModel model)
        {
               /* System.Console.WriteLine("detailrequestorder: " + model.id);
                System.Console.WriteLine("AnswerDetail: " + model.AnswerDetail);
                System.Console.WriteLine("AnswerProblem: " + model.AnswerProblem);
                System.Console.WriteLine("AnswerCounsel: " + model.AnswerCounsel);
                System.Console.WriteLine("AnswerRequestorder: " + model.files);*/
            var cabinedata = _context.RequestOrders.Find(model.id);
            {
                cabinedata.AnswerDetail = model.AnswerDetail;
                cabinedata.AnswerProblem = model.AnswerProblem;
                cabinedata.AnswerCounsel = model.AnswerCounsel;
                
            };

             _context.Entry(cabinedata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                }
            }

            return Ok(termsList);

        }
      
    }
 }


