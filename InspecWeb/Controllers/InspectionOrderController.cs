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

        public InspectionOrderController (ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<InspectionOrder> Get()
        {

            var inspectionorderdata = _context.InspectionOrders            
                .OrderByDescending(m => m.Id);

            return inspectionorderdata;

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
            var inspectionorderdata = new InspectionOrder
            {
                Name = model.Name,
                Year = model.Year,
                Order = model.Order,
                File = filesname,
                CreateBy = model.CreateBy,
            };
            _context.InspectionOrders.Add(inspectionorderdata);
            _context.SaveChanges();
            return Ok(new { Id = inspectionorderdata.Id, title = model.Name });

        }

        // PUT api/values/5
        [HttpPut("{id}")]

        public async Task<IActionResult> Put([FromForm] InspectionorderRequest request, long id)
        {
        //    public void Put(long id, string year, string name, string order, string createBy, string file)
        //{
            var inspectionorder = _context.InspectionOrders.Find(id);
            inspectionorder.Name = request.Name;
            inspectionorder.Year = request.Year;
            inspectionorder.Order = request.Order;
            inspectionorder.CreateBy = request.CreateBy;


            var date = DateTime.Now;
            var filesname = "null";
            var random = RandomString(15);


            if (!Directory.Exists(_environment.WebRootPath + "/assets" + "//InspectionorderFile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "/assets" + "//InspectionorderFile//"); //สร้าง Folder Upload ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/assets" + "//InspectionorderFile//";

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
                        using (var stream = System.IO.File.Create(filePath + random + ext))
                        {
                            await formFile.Value.CopyToAsync(stream);

                            filesname = random + ext;
                        }
                    }
                }
            }


            inspectionorder.File = filesname;
            _context.Entry(inspectionorder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { Id = id, title = request.Name });
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

public class InspectionorderRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Year { get; set; }
    public string Order { get; set; }
    public string CreateBy { get; set; }
    public string ShortnameTH { get; set; }

    public List<IFormFile> files { get; set; }

}
