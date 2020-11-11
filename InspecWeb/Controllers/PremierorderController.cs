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
    public class PremierorderController : Controller
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

        public PremierorderController (ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Premierorder> Get()
        {
            var premierorderdata = from P in _context.Premierorders.OrderByDescending(m => m.Id)
                                   select P;
            return premierorderdata;

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
        public async Task<IActionResult> Post([FromForm] PremierorderViewModel model)
        {
            var date = DateTime.Now;
            var filesname = "null";
            var random = RandomString(15);
            //ตรวจสอบว่ามี Folder Upload ใน wwwroot มั้ย
            if (!Directory.Exists(_environment.WebRootPath + "/assets" + "//PremierorderFile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "/assets" + "//PremierorderFile//"); //สร้าง Folder Upload ใน wwwroot
            }

            ////var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            //// path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "/assets" + "//PremierorderFile//";


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
            var premierorderdata = new Premierorder
            {
                Title = model.Title,
                Year = model.Year,
                File = filesname,
            };
            _context.Premierorders.Add(premierorderdata);
            _context.SaveChanges();
            return Ok(premierorderdata);

        }

        // PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(long id, string year, string title, string file)
        //{
        //    var premierorder = _context.Premierorders.Find(id);
        //    premierorder.Title = title;
        //    premierorder.Year = year;
            
        //    _context.Entry(premierorder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    _context.SaveChanges();

        //}

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] PremierorderViewModel request)
        {

            var date = DateTime.Now;

            var data = _context.Premierorders.Find(request.Id);
            data.Year = request.Year;
            data.Title = request.Title;
            _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            System.Console.WriteLine("1 : " + request.Year);
            // <!-- อัพไฟล์  -->
            if (!Directory.Exists(_environment.WebRootPath + "/assets" + "//PremierorderFile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "/assets" + "//PremierorderFile//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/assets" + "//PremierorderFile//";
            System.Console.WriteLine("2 : " + request.Title);
            if (request.files != null)
            {
                foreach (var formFile in request.files.Select((value, index) => new { Value = value, Index = index }))
                {
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);
                    System.Console.WriteLine("3 : ");
                    if (formFile.Value.Length > 0)
                    {
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }
                        var data2 = _context.Premierorders.Find(request.Id);
                        data2.File = random + filename;
                        System.Console.WriteLine("4 : ");
                        _context.Entry(data2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();

                    }
                }
            }
            // <!--END อัพไฟล์  -->
            System.Console.WriteLine("5 : ");
            return Ok(new { Id = data.Id, title = data.Title });

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var premierorder = _context.Premierorders.Find(id);

            _context.Premierorders.Remove(premierorder);
            _context.SaveChanges();
        }
    }
}
