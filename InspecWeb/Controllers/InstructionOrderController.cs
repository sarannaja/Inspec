using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class InstructionOrderController : Controller
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

        public InstructionOrderController (ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<InstructionOrder> Get()
        {
            var instructionorderdata = from P in _context.InstructionOrders
                                       .OrderByDescending(m => m.Id)
                                       select P;
            return instructionorderdata;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] InstructionorderViewModel model)
        {
            var date = DateTime.Now;
            var filesname = "null";
            var random = RandomString(15);
            //ตรวจสอบว่ามี Folder Upload ใน wwwroot มั้ย
            if (!Directory.Exists(_environment.WebRootPath + "/assets" + "//InstructionorderFile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "/assets" + "//InstructionorderFile//"); //สร้าง Folder Upload ใน wwwroot
            }

            ////var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            //// path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "/assets" + "//InstructionorderFile//";


            foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
            ////foreach (var formFile in data.files)
            {
                string filePath2 = formFile.Value.FileName;
                string filename = Path.GetFileName(filePath2);
                string ext = Path.GetExtension(filename);

                if (formFile.Value.Length > 0)
                {
                    // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                    using (var stream = System.IO.File.Create(filePath + random+"_"+formFile.Value.FileName))
                    {
                        await formFile.Value.CopyToAsync(stream);

                        filesname = random + formFile.Value.FileName;
                    }
                }
            }
            var instructionorderdata = new InstructionOrder
            {
                Name = model.Name,
                Year = model.Year,
                Order = model.Order,
                File = filesname,
                CreateBy = model.CreateBy,
                Detail = model.Detail,
                CreatedAt = date
            };
            _context.InstructionOrders.Add(instructionorderdata);
            _context.SaveChanges();
            return Ok(new { Id = instructionorderdata.Id, title = model.Name });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] InstructionorderRequest request, long id)
        {
            var data = _context.InstructionOrders.Find(id);
            data.Name = request.Name;
            data.Year = request.Year;
            data.Order = request.Order;
            data.Detail = request.Detail;
            data.CreateBy = request.CreateBy;


            var date = DateTime.Now;
            var filesname = request.Filename;
            var random = RandomString(15);

          
            if (!Directory.Exists(_environment.WebRootPath + "/assets" + "//InstructionorderFile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "/assets" + "//InstructionorderFile//"); //สร้าง Folder Upload ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/assets" + "//InstructionorderFile//";

            if (request.files != null)
            {
                foreach (var formFile in request.files.Select((value, index) => new { Value = value, Index = index }))
                ////foreach (var formFile in data.files)
                {
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        using (var stream = System.IO.File.Create(filePath + random+"_"+formFile.Value.FileName))
                        {
                            await formFile.Value.CopyToAsync(stream);

                           filesname = random + formFile.Value.FileName;
                        }
                       
                    }
                }
            }


            data.File = filesname;
       
            _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { Id = id, title = request.Name });
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var instructionorder = _context.InstructionOrders.Find(id);

            _context.InstructionOrders.Remove(instructionorder);
            _context.SaveChanges();
        }
    }
}

public class InstructionorderRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Year { get; set; }
    public string Order { get; set; }
    public string Detail { get; set; }
    public string CreateBy { get; set; }
    public string Filename { get; set; }

    public List<IFormFile> files { get; set; }

}
