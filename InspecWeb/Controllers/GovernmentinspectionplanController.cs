using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class GovernmentinspectionplanController : Controller
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

        public GovernmentinspectionplanController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Governmentinspectionplan> Get()
        {
            var governmentinspectionplandata = from P in _context.Governmentinspectionplans
                                 select P;
            return governmentinspectionplandata;

            //return 
            //_context.Provinces
            //   .Include(p => p.Districts)
            //   .Where(p => p.Id == 1)
            //   .ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] GovernmentinspectionplanViewModel model)
        {
            var date = DateTime.Now;
            var filesname = "null";
            var random = RandomString(15);
            //ตรวจสอบว่ามี Folder Upload ใน wwwroot มั้ย
            if (!Directory.Exists(_environment.WebRootPath + "/assets" + "//GovernmentinspectionplanFile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "/assets" + "//GovernmentinspectionplanFile//"); //สร้าง Folder Upload ใน wwwroot
            }

            ////var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            //// path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "/assets" + "//GovernmentinspectionplanFile//";

            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                ////foreach (var formFile in data.files)
                {
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + ext))
                        {
                            await formFile.Value.CopyToAsync(stream);

                            filesname = random + ext;
                        }
                    }
                }
            }
            var governmentinspectionplandata = new Governmentinspectionplan
            {
                Year = model.Year,
                //Position = model.Position,
                File = filesname,
                //Prefix = model.Prefix,
                Title = model.Title,
                CreatedAt = date
            };
            _context.Governmentinspectionplans.Add(governmentinspectionplandata);
            _context.SaveChanges();
            return Ok(governmentinspectionplandata);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] GovernmentinspectionplanViewModel model,long id)
        {
            var date = DateTime.Now;
            var filesname = model.filesname;
            var random = RandomString(15);
            //ตรวจสอบว่ามี Folder Upload ใน wwwroot มั้ย
            if (!Directory.Exists(_environment.WebRootPath + "/assets" + "//GovernmentinspectionplanFile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "/assets" + "//GovernmentinspectionplanFile//"); //สร้าง Folder Upload ใน wwwroot
            }

            ////var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            //// path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "/assets" + "//GovernmentinspectionplanFile//";

            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                ////foreach (var formFile in data.files)
                {
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + ext))
                        {
                            await formFile.Value.CopyToAsync(stream);

                            filesname = random + ext;
                        }
                    }
                }
            }

            var governmentinspectionplan = _context.Governmentinspectionplans.Find(id);
                governmentinspectionplan.Year = model.Year;
                governmentinspectionplan.Title = model.Title;
                governmentinspectionplan.File = filesname;
                _context.Entry(governmentinspectionplan).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

                return Ok(governmentinspectionplan);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var governmentinspectionplan = _context.Governmentinspectionplans.Find(id);

            _context.Governmentinspectionplans.Remove(governmentinspectionplan);
            _context.SaveChanges();
        }
    }
}