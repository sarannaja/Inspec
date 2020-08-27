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
    public class NationalstrategyController : Controller
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

        public NationalstrategyController (ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Nationalstrategy> Get()
        {
            var nationalstrategydata = from P in _context.Nationalstrategies
                                 select P;
            return nationalstrategydata;

          
        }


        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] NationalstrategyViewModel model)
        {
            var date = DateTime.Now;
            var filesname = "null";
            var random = RandomString(15);
            //ตรวจสอบว่ามี Folder Upload ใน wwwroot มั้ย
            if (!Directory.Exists(_environment.WebRootPath + "/assets" + "//NationalstrategyFile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "/assets" + "//NationalstrategyFile//"); //สร้าง Folder Upload ใน wwwroot
            }

            ////var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            //// path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "/assets" + "//NationalstrategyFile//";


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
            var nationalstrategydata = new Nationalstrategy
            {
                //Position = model.Position,
                //Prefix = model.Prefix,
                File = filesname,
                Title = model.Title,
                CreatedAt = date
            };
            _context.Nationalstrategies.Add(nationalstrategydata);
            _context.SaveChanges();
            return Ok(nationalstrategydata);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] NationalstrategyViewModel model,long id)
        {
            var nationalstrategy = _context.Nationalstrategies.Find(id);
            nationalstrategy.Title = model.Title;

            var filesname = model.namefile;
            var random = RandomString(15);

            System.Console.WriteLine("1 : ");
            //ตรวจสอบว่ามี Folder Upload ใน wwwroot มั้ย
            if (!Directory.Exists(_environment.WebRootPath + "/assets" + "//NationalstrategyFile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "/assets" + "//NationalstrategyFile//"); //สร้าง Folder Upload ใน wwwroot
            }

            ////var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            //// path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "/assets" + "//NationalstrategyFile//";

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
                        System.Console.WriteLine("2 : ");
                    }
                }
            }

            _context.Entry(nationalstrategy).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            System.Console.WriteLine("3 : ");
            return Ok(nationalstrategy);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var nationalstrategy = _context.Nationalstrategies.Find(id);

            _context.Nationalstrategies.Remove(nationalstrategy);
            _context.SaveChanges();
        }
    }
}
