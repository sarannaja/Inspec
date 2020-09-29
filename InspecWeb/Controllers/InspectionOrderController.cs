using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class InspectionOrderController : Controller
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

        public InspectionOrderController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<InspectionOrder> Get()
        {
            var inspectionorderdata = from P in _context.InspectionOrders
                                      select P;
            return inspectionorderdata;

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
        public async Task<IActionResult> Post([FromForm] InspectionorderViewModel model)
        {
            var date = DateTime.Now;
            var filesname = "null";
            var random = RandomString(15);
            //ตรวจสอบว่ามี Folder Upload ใน wwwroot มั้ย
            if (!Directory.Exists(_environment.WebRootPath + "/assets" + "//InspectionorderFile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "/assets" + "//InspectionorderFile//"); //สร้าง Folder Upload ใน wwwroot
            }

            ////var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            //// path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "/assets" + "//InspectionorderFile//";


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
            var inspectionorderdata = new InspectionOrder
            {
                Name = model.Name,
                Year = model.Year,
                Order = model.Order,
                //Position = model.Position,
                //Prefix = model.Prefix,
                File = filesname,
                CreateBy = model.CreateBy,
                CreatedAt = date
            };
            _context.InspectionOrders.Add(inspectionorderdata);
            _context.SaveChanges();
            return Ok(inspectionorderdata);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string year, string name, string order, string createBy, string file)
        {
            var inspectionorder = _context.InspectionOrders.Find(id);
            inspectionorder.Name = name;
            inspectionorder.Year = year;
            inspectionorder.Order = order;
            inspectionorder.CreateBy = createBy;

            _context.Entry(inspectionorder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var inspectionorder = _context.InspectionOrders.Find(id);

            _context.InspectionOrders.Remove(inspectionorder);
            _context.SaveChanges();
        }
    }
}
