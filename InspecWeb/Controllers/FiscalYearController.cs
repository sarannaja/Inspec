using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class FiscalYearController : Controller
    {
        public static IWebHostEnvironment _environment;

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private readonly ApplicationDbContext _context;

        public FiscalYearController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {

            var fiscalyearData = _context.FiscalYears
                .Include(m => m.SetinspectionareaFiles)
                .OrderBy(x => x.CreatedAt)
                .ToList();
            return Ok(fiscalyearData);


        }

        [HttpGet("getCurrentYear")]
        public IActionResult GetCurrentYear()
        {
            var fiscalyearData = _context.FiscalYearNew
                .OrderByDescending(x => x.Year)
                .ToList();

            return Ok(fiscalyearData);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IEnumerable<object> Get(long id)
        {
            var fiscalyeardata = _context.FiscalYearRelations
                                        .Include(m => m.FiscalYear)
                                        .Include(m => m.Region)
                                        .Include(m => m.Province)
                                        .Where(m => m.FiscalYearId == id);
            return fiscalyeardata;


            //var fiscalyearprovince = new List<Array>();

            //foreach(var fiscalyeardata in fiscalyeardatas)
            //{ 
            //    fiscalyearprovince.Add(new
            //    {
            //        Province = fiscalyeardata.Pro
            //    });
            //}

            //return fiscalyeardata;
        }

        [HttpGet("getProvinceRecycled/{id}")]
        public IEnumerable<object> GetProvince(long id)
        {
            var fiscalyeardata = _context.FiscalYearRelations
                .Include(m => m.Province)
                .Where(m => m.FiscalYearId == id);

            //var province = _context.Provinces;
           
            //foreach(var eee in province)
            //{
            //    var pId = eee.Id;
            //    System.Console.WriteLine("PID: " + pId);
            //}
           
            //foreach (var ddd in fiscalyeardata)
            //{
            //    var fId = ddd.ProvinceId;
            //    System.Console.WriteLine("FID: " + fId);
            //}

            return fiscalyeardata;

        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Setinspectionarea model)
        {
            var date = DateTime.Now;

            var fiscalyeardata = new FiscalYear
            {
                Year = model.Year,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Orderdate = model.Orderdate,
                CreatedAt = date
            };
            _context.FiscalYears.Add(fiscalyeardata);
            _context.SaveChanges();

            if (!Directory.Exists(_environment.WebRootPath + "//Setinspectionareafile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Setinspectionareafile//"); //สร้าง Folder Upload ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "//Setinspectionareafile//";
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

                        var SetinspectionareaFile = new SetinspectionareaFile
                        {
                            FiscalYearId = fiscalyeardata.Id,
                            Name = random + filename,
                            CreatedAt = date
                        };
                        _context.SetinspectionareaFiles.Add(SetinspectionareaFile);
                        _context.SaveChanges();
                    }
                }
                System.Console.WriteLine("testuser : 2");
            }

            return Ok(new { status = "ture" });
        }

        //POST api/values
        [HttpPost("AddRelation")]
        public void Post([FromBody] FiscalYearRelationViewModel model)
        {

            foreach (var id in model.ProvinceId)
            {
                System.Console.WriteLine("LOOP: " + id);
                var fiscalyearrelationdata = new FiscalYearRelation
                {
                    FiscalYearId = model.FiscalYearId,
                    RegionId = model.RegionId,
                    ProvinceId = id
                };
                _context.FiscalYearRelations.Add(fiscalyearrelationdata);
            }


            _context.SaveChanges();


        }

        
        [HttpPost("EditRelation/{id}")]
        public void Put([FromBody] FiscalYearRelationViewModel model,long id)
        {
            var data = _context.FiscalYearRelations.Find(id);

            _context.FiscalYearRelations.RemoveRange(data);
            _context.SaveChanges();

            foreach (var item in model.ProvinceId)
            {
                System.Console.WriteLine("LOOP: " + item);
                var fiscalyearrelationdata = new FiscalYearRelation
                {
                    FiscalYearId = model.FiscalYearId,
                    RegionId = model.RegionId,
                    ProvinceId = item
                };
                _context.FiscalYearRelations.Add(fiscalyearrelationdata);
            }
            _context.SaveChanges();

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] Setinspectionarea model, long id)
        {
            var date = DateTime.Now;
            var fiscalyear = _context.FiscalYears.Find(id);
            fiscalyear.Year = model.Year;
            fiscalyear.Orderdate = model.Orderdate;
            fiscalyear.StartDate = model.StartDate;
            fiscalyear.EndDate = model.EndDate;
            fiscalyear.UpdateAt = date;
            _context.Entry(fiscalyear).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            if (!Directory.Exists(_environment.WebRootPath + "//Setinspectionareafile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Setinspectionareafile//"); //สร้าง Folder Upload ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "//Setinspectionareafile//";
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

                        var SetinspectionareaFile = new SetinspectionareaFile
                        {
                            FiscalYearId = id,
                            Name = random + filename,
                            CreatedAt = date
                        };
                        _context.SetinspectionareaFiles.Add(SetinspectionareaFile);
                        _context.SaveChanges();
                    }
                }
                System.Console.WriteLine("testuser : 2");
            }

            return Ok(new { status = "ture" });

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var fiscalyeardata = _context.FiscalYears.Find(id);

            _context.FiscalYears.Remove(fiscalyeardata);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("DeleteRelation/{id}/{fiscalyearid}")]
        public void Delete(long id, long fiscalyearid)

        {
            System.Console.WriteLine("LOOP: " + id + fiscalyearid);
            var fiscalyearrelationdata = _context.FiscalYearRelations
                .Where(x => x.RegionId == id && x.FiscalYearId == fiscalyearid);
            foreach (var test in fiscalyearrelationdata)
            {
                _context.FiscalYearRelations.Remove(test);
            }
            
            _context.SaveChanges();
        }

        [HttpGet("getCurrentFiscalYear/{year}")]
        public IActionResult GetCurrentFiscalYear(long year)
        {
            var fiscalyearData = _context.FiscalYearNew
                //.OrderByDescending(x => x.Year)
                .Where(m => m.Year == year+543)
                .FirstOrDefault();

            return Ok(fiscalyearData);
        }

        [HttpGet("getReportFiscalYearRelations/{fiscalYearId}/{userId}")]
        public IActionResult GetImportReportFiscalYearRelations(long fiscalYearId, string userId)
        {
            List<object> termsList = new List<object>();
            var userdata = _context.Users
                .Include(m => m.UserProvince)
                .Where(m => m.Id == userId)
                .FirstOrDefault();

            foreach (var provinceUser in userdata.UserProvince)
            {

                var importFiscalYearRelations = _context.FiscalYearRelations
                    .Include(x => x.Region)
                    .Include(x => x.Province)
                    .Where(x => x.FiscalYearId == fiscalYearId)
                    .Where(x => x.ProvinceId == provinceUser.ProvinceId)
                    .FirstOrDefault();

                termsList.Add(importFiscalYearRelations);
            }

            return Ok(new { termsList });
        }
    }
}

public class Setinspectionarea
{
    public long Id { get; set; }

    public string Year { get; set; }
    public DateTime? Orderdate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<IFormFile> files { get; set; }

}