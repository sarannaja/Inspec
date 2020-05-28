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
        public object Get(long id)
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

            if (!Directory.Exists(_environment.WebRootPath + "//upload//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";


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
                .Where(m => m.CentralPolicyId == id);

            return Ok(executiveOrderdata);
            //return "value";
        }

        [HttpGet("detailrole3/{id}/{userid}")]
        public IActionResult Getexecutiverole3(long id,string userid)
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
            System.Console.WriteLine("detailexecutiveorder: " + model.id);
            System.Console.WriteLine("AnswerDetail: " + model.AnswerDetail);
            System.Console.WriteLine("AnswerProblem: " + model.AnswerProblem);
            System.Console.WriteLine("AnswerCounsel: " + model.AnswerCounsel);
            System.Console.WriteLine("AnswerExecutiveorder: " + model.files);
            var cabinedata = _context.ExecutiveOrders.Find(model.id);
            {
                cabinedata.AnswerDetail = model.AnswerDetail;
                cabinedata.AnswerProblem = model.AnswerProblem;
                cabinedata.AnswerCounsel = model.AnswerCounsel;

            };

            _context.Entry(cabinedata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            if (!Directory.Exists(_environment.WebRootPath + "//upload//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";
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
                    System.Console.WriteLine("Sucess");
                }
            }
            return Ok(new { Id = model.id }); 
        }

        [HttpGet("ex/{id}")]
        public IActionResult GetData(string id)
        {
            System.Console.WriteLine("DDDDD");
            System.Console.WriteLine("USERID : " + id);
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


