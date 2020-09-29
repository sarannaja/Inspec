using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO; //excel
using System.Linq;
using System.Threading.Tasks;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class InformationinspectionController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static IWebHostEnvironment _environment;

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public InformationinspectionController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Informationinspection> Get()
        {
            var data = from P in _context.Informationinspections
                       select P;
            return data;
        }



        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] InformationinspectionsRequest request)
        {
            System.Console.WriteLine("0 : " + request.Title);
            var date = DateTime.Now;

            var data = new Informationinspection
            {
                Title = request.Title,
                Filename = "n",
                CreatedAt = date
            };
            _context.Informationinspections.Add(data);
            _context.SaveChanges();
            System.Console.WriteLine("1 : ");
            // <!-- อัพไฟล์  -->
            if (!Directory.Exists(_environment.WebRootPath + "//informationinspections//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//informationinspections//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "//informationinspections//";
            System.Console.WriteLine("2 : ");
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
                        var data2 = _context.Informationinspections.Find(data.Id);
                        data2.Filename = random + filename;
                        System.Console.WriteLine("4 : ");
                        _context.Entry(data2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();

                    }
                }
            }
            // <!--END อัพไฟล์  -->
            System.Console.WriteLine("5 : ");
            return Ok(new { Id = data.Id });
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] InformationinspectionsRequest request)
        {

            var date = DateTime.Now;

            var data = _context.Informationinspections.Find(request.Id);
            data.Title = request.Title;
            _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            System.Console.WriteLine("1 : ");
            // <!-- อัพไฟล์  -->
            if (!Directory.Exists(_environment.WebRootPath + "//informationinspections//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//informationinspections//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "//informationinspections//";
            System.Console.WriteLine("2 : ");
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
                        var data2 = _context.Informationinspections.Find(request.Id);
                        data2.Filename = random + filename;
                        System.Console.WriteLine("4 : ");
                        _context.Entry(data2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();

                    }
                }
            }
            // <!--END อัพไฟล์  -->
            System.Console.WriteLine("5 : ");
            return Ok(new { Id = data.Id });

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var data = _context.Informationinspections.Find(id);

            _context.Informationinspections.Remove(data);
            _context.SaveChanges();
        }

    }
}

public class InformationinspectionsRequest
{
    public long Id { get; set; }
    public string Title { get; set; }
    public List<IFormFile> files { get; set; }
}