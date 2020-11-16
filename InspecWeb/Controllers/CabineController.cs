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
    public class CabineController : Controller
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

        public CabineController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Cabine> Get()
        {
            var cabinedata = from P in _context.Cabines
                               select P;
            return cabinedata;

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
        public async Task<IActionResult> Post([FromForm] CabineViewModel model)
        {
            var date = DateTime.Now;
            var imagename = "null";
            var random = RandomString(15); 
            //ตรวจสอบว่ามี Folder Upload ใน wwwroot มั้ย
            if (!Directory.Exists(_environment.WebRootPath +"/assets"+ "//CabineFile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath  +"/assets" + "//CabineFile//"); //สร้าง Folder Upload ใน wwwroot
            }

            ////var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            //// path ที่เก็บไฟล์
              var filePath = _environment.WebRootPath + "/assets" + "//CabineFile//";


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

                        imagename = random + ext;
                    }
                }
            }
            var cabinedata = new Cabine
            {
                Name = model.Name,
                Position = model.Position,
                Image = imagename,
                Prefix = model.Prefix,
                MinistryId = 1,
                Detail = model.Detail,
                CreatedAt = date
            };
            _context.Cabines.Add(cabinedata);
            _context.SaveChanges();
            return Ok(cabinedata);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name, string position, /*string image,*/ string prefix, string detail)
        {
            var cabine = _context.Cabines.Find(id);
            cabine.Name = name;
            cabine.Position = position;
            //cabine.Image = image;
            cabine.MinistryId = 1;
            cabine.Prefix = prefix;
            cabine.Detail = detail;
            _context.Entry(cabine).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var cabinedata = _context.Cabines.Find(id);

            _context.Cabines.Remove(cabinedata);
            _context.SaveChanges();
        }
    }
}
