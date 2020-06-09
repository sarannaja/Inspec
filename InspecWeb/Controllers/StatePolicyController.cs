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
    public class StatePolicyController : Controller
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

        public StatePolicyController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<StatePolicy> Get()
        {
            var statepolicydata = from P in _context.StatePolicys
                                  select P;
            return statepolicydata;

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
        public async Task<IActionResult> Post([FromForm] StatePolicyViewModel model)
        {
            var date = DateTime.Now;
            var filesname = "null";
            var random = RandomString(15);
            //ตรวจสอบว่ามี Folder Upload ใน wwwroot มั้ย
            if (!Directory.Exists(_environment.WebRootPath + "/assets" + "//StatePolicyFile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "/assets" + "//StatePolicyFile//"); //สร้าง Folder Upload ใน wwwroot
            }

            ////var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            //// path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "/assets" + "//StatePolicyFile//";


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
            var statePolicydata = new StatePolicy
            {
                GangId = model.GangId,
                Name = model.Name,
                File = filesname,
                No = model.No,
                RoundNo = model.RoundNo,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Date = model.Date,
                CreatedAt = date
            };
            _context.StatePolicys.Add(statePolicydata);
            _context.SaveChanges();
            return Ok(statePolicydata);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string year, string title, string file)
        {
            var governmentinspectionplan = _context.Governmentinspectionplans.Find(id);
            governmentinspectionplan.Year = year;
            governmentinspectionplan.Title = title;
            _context.Entry(governmentinspectionplan).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

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