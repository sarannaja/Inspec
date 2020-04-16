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
    [ApiController]
    public class CentralPolicyController : Controller
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

        public CentralPolicyController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<CentralPolicy> Get()
        {
            //var centralpolicydata = from P in _context.CentralPolicies
            //                        select P;
            //return centralpolicydata;

            return _context.CentralPolicies
                   .Include(m => m.FiscalYear)
                   .Include(m => m.CentralPolicyProvinces)
                   .ThenInclude(x => x.Province)
                   .Include(m => m.CentralPolicyDates)
                   .Where(m => m.Class == "แผนการตรวจประจำปี")
                   .ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var centralpolicydata = _context.CentralPolicies
                .Include(m => m.CentralPolicyProvinces)
                .ThenInclude(x => x.Province)
                .Include(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicyFiles)
                .Include(m => m.Subjects)
                .ThenInclude(m => m.Subquestions)
                .Where(m => m.Id == id).First();

            return Ok(centralpolicydata);
            //return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CentralPolicyProvinceViewModel model)
        {
            //var eiei = model.StartDate;
            //System.Console.WriteLine("test: " + eiei);
            //return Ok(model);
            var date = DateTime.Now;
            var centralpolicydata = new CentralPolicy
            {
                Title = model.Title,
                Type = model.Type,
                FiscalYearId = model.FiscalYearId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Status = model.Status,
                CreatedAt = date,
                CreatedBy = "Super Admin",
                Class = "แผนการตรวจประจำปี",
            };

            _context.CentralPolicies.Add(centralpolicydata);
            _context.SaveChanges();

            foreach (var id in model.ProvinceId)
            {
                var centralpolicyprovincedata = new CentralPolicyProvince
                {
                    ProvinceId = id,
                    CentralPolicyId = centralpolicydata.Id,
                };
                _context.CentralPolicyProvinces.Add(centralpolicyprovincedata);
            }
            _context.SaveChanges();

            int index = 0;
            int indexend = 0;
            foreach (var item in model.StartDate2)
            {
                
                var CentralPolicyDate = new CentralPolicyDate
                {
                    CentralPolicyId = centralpolicydata.Id,
                    StartDate = item,
                    //EndDate = item.EndDate,
                };
                _context.CentralPolicyDates.Add(CentralPolicyDate);
                _context.SaveChanges();

                var id = _context.CentralPolicyDates.Find(CentralPolicyDate.Id);

                indexend = 0;

                foreach (var itemend in model.EndDate2)
                {
                    if (index == indexend) { 
                        id.EndDate = itemend;
                        
                        System.Console.WriteLine("END: " + indexend);
                    }
                    indexend++;
                }
               
                index++;

                System.Console.WriteLine("Start: " + index);
            }



            //int maxSize = Int32.Parse(ConfigurationManager.AppSettings["MaxFileSize"]);
            //var size = data.files.Sum(f => f.Length);
            
            //ตรวจสอบว่ามี Folder Upload ใน wwwroot มั้ย
            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";


            foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
            //foreach (var formFile in data.files)
            {
                var random = RandomString(10);
                string filePath2 = formFile.Value.FileName;
                string filename = Path.GetFileName(filePath2);
                string ext = Path.GetExtension(filename);

                if (formFile.Value.Length > 0)
                {
                    // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                    using (var stream = System.IO.File.Create(filePath + random + filename))
                    {
                        await formFile.Value.CopyToAsync(stream);
                    }

                    var CentralPolicyFile = new CentralPolicyFile
                    {
                        CentralPolicyId = centralpolicydata.Id,
                        Name =  random + filename,
                    };
                    _context.CentralPolicyFiles.Add(CentralPolicyFile);
                    _context.SaveChanges();
                }
            }
            return Ok(new { status = true });

        }

        [HttpPut("{editId}")]
        public async Task<IActionResult> Put([FromForm] CentralPolicyProvinceViewModel model, long editId)
        {
            //var eiei = model.StartDate;
            System.Console.WriteLine("test: " + editId);
            //return Ok(model);
            var date = DateTime.Now;
            var centralpolicydata = _context.CentralPolicies.Find(editId);
            {
                centralpolicydata.Title = model.Title;
                centralpolicydata.Type = model.Type;
                centralpolicydata.FiscalYearId = model.FiscalYearId;
                centralpolicydata.StartDate = model.StartDate;
                centralpolicydata.EndDate = model.EndDate;
                centralpolicydata.Status = model.Status;
                centralpolicydata.CreatedAt = date;
                centralpolicydata.CreatedBy = "Super Admin";
                centralpolicydata.Class = "แผนการตรวจประจำปี";
            };

            //_context.CentralPolicies.Add(centralpolicydata);
            _context.Entry(centralpolicydata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            var delData = _context.CentralPolicyProvinces
                   .Where(x => x.CentralPolicyId == editId)
                   .ToList();
            foreach(var del in delData)
            {
                _context.CentralPolicyProvinces.Remove(del);
            }
            _context.SaveChanges();

            foreach (var id in model.ProvinceId)
            {
                System.Console.WriteLine("in2");
                var centralpolicyprovincedata = new CentralPolicyProvince
                {
                    ProvinceId = id,
                    CentralPolicyId = editId,
                };
                _context.CentralPolicyProvinces.Add(centralpolicyprovincedata);
                //_context.Entry(centralpolicyprovincedata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            _context.SaveChanges();

            var deleteDate = _context.CentralPolicyDates
                    .Where(x => x.CentralPolicyId == editId)
                    .ToList();

            foreach (var delDate in deleteDate)
            {
                _context.CentralPolicyDates.Remove(delDate);
            }
            _context.SaveChanges();

            int index = 0;
            int indexend = 0;
            foreach (var item in model.StartDate2)
            {
                var CentralPolicyDate = new CentralPolicyDate
                {
                    CentralPolicyId = centralpolicydata.Id,
                    StartDate = item
                    //EndDate = item.EndDate,
                };
                _context.CentralPolicyDates.Add(CentralPolicyDate);
                //_context.Entry(CentralPolicyDate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

                var id = _context.CentralPolicyDates.Find(CentralPolicyDate.Id);

                indexend = 0;

                foreach (var itemend in model.EndDate2)
                {
                    if (index == indexend)
                    {
                        id.EndDate = itemend;

                        System.Console.WriteLine("END: " + indexend);
                    }
                    indexend++;
                }

                index++;
                _context.SaveChanges();
                System.Console.WriteLine("Start: " + index);
            }

            System.Console.WriteLine("FINISH");

            //if (model.files != null) {

            //    var delFile = _context.CentralPolicyFiles
            //               .Where(x => x.CentralPolicyId == editId)
            //               .ToList();

            //    foreach (var del in delFile)
            //    {
            //        _context.CentralPolicyFiles.Remove(del);
            //    }
            //    _context.SaveChanges();
            //    System.Console.WriteLine("in1");
            //}

            //int maxSize = Int32.Parse(ConfigurationManager.AppSettings["MaxFileSize"]);
            //var size = data.files.Sum(f => f.Length);
            
            //ตรวจสอบว่ามี Folder Upload ใน wwwroot มั้ย
            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                System.Console.WriteLine("in2");
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";


            System.Console.WriteLine("testJa: " + model.files);

            if (model.files != null) {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {
                    var random = RandomString(10);
                    System.Console.WriteLine("in3");
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("in4");
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        var CentralPolicyFiledata = new CentralPolicyFile
                        {
                            CentralPolicyId = centralpolicydata.Id,
                            Name = random + filename,
                        };
                        _context.CentralPolicyFiles.Add(CentralPolicyFiledata);
                        System.Console.WriteLine("in5");
                        _context.SaveChanges();
                        System.Console.WriteLine("in6");
                        //_context.Entry(CentralPolicyFile).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                } }
            return Ok(new { status = true });

        }

        [HttpDelete("deletefile/{id}")]
        public void DeleteFile(long id)
        {
            var centralpolicyfiledata = _context.CentralPolicyFiles.Find(id);

            _context.CentralPolicyFiles.Remove(centralpolicyfiledata);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var centralpolicydata = _context.CentralPolicies.Find(id);

            _context.CentralPolicies.Remove(centralpolicydata);
            _context.SaveChanges();
        }

        //POST api/values
        [HttpPost("users")]
        public void Post([FromBody] CentralPolicyUserModel model)
        {

            var CentralPolicyGroupdata = new CentralPolicyGroup
            {
            };
            _context.CentralPolicyGroups.Add(CentralPolicyGroupdata);
            _context.SaveChanges();

            var CentralPolicyId = _context.CentralPolicyProvinces
                .Where(x => x.Id == model.CentralPolicyId)
                .Select(x => x.CentralPolicyId)
                .First();

            var ProvinceId = _context.CentralPolicyProvinces
            .Where(x => x.Id == model.CentralPolicyId)
            .Select(x => x.ProvinceId)
            .First();
            System.Console.WriteLine("CID: " + CentralPolicyId);


            foreach (var id in model.UserId)
            {
                System.Console.WriteLine("CENTRALID: " + model.CentralPolicyId);
                System.Console.WriteLine("LOOP: " + id);
                var centralpolicyuserdata = new CentralPolicyUser
                {
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    CentralPolicyGroupId = CentralPolicyGroupdata.Id,
                    UserId = id,
                    Status = "รอการตอบรับ"
                };
                _context.CentralPolicyUsers.Add(centralpolicyuserdata);
            }
            _context.SaveChanges();
        }

        // GET api/values/5
        [HttpGet("users/{id}")]
        public IActionResult GetUsers(long id)
        {
            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.User)
                .Where(m => m.CentralPolicyId == id);

            return Ok(centralpolicyuserdata);
        }

        // GET api/values/5
        [HttpGet("usersprovince/{id}")]
        public IActionResult GetUserProvinces(long id)
        {
            var centralpolicyprovince = _context.CentralPolicyProvinces
            .Where(m => m.Id == id).FirstOrDefault();

            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.User)
                .Where(m => m.CentralPolicyId == centralpolicyprovince.CentralPolicyId);

            return Ok(centralpolicyuserdata);
        }

        // GET api/values/5
        [HttpGet("getcentralpolicyfromprovince/{id}")]
        public IActionResult getcentralpolicyfromprovince(long id)
        {
            var centralpolicyprovincedata = _context.CentralPolicyProvinces
                .Include(m => m.CentralPolicy)
                .Where(m => m.ProvinceId == id)
                .ToList();

            return Ok(centralpolicyprovincedata);
        }
        // PUT api/values/5
        [HttpPut("acceptcentralpolicy/{id}")]
        public void PutStatus(long id, string status)

        {
            System.Console.WriteLine("ID: " + id);
            System.Console.WriteLine("Status: " + status);


            //var acaccept = _context.CentralPolicyUsers.Where(x => x.Id == id)
            //               .First();
            //System.Console.WriteLine("acaccept: " + acaccept.Id);


            (from t in _context.CentralPolicyUsers where t.Id == id select t).ToList().
                ForEach(x => x.Status = status);

            //var accept = _context.CentralPolicyUsers.Find(id);
            //var accept = _context.CentralPolicyUsers.Where(m => m.Id == id).FirstOrDefault();
            //accept.Status = status;
            //System.Console.WriteLine("Status: " + accept);
            //_context.Entry(accept).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();


        }

        // GET api/values/5
        [HttpGet("usersinvited/{id}")]
        public IActionResult GetUsers2(string id)
        {
            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyDates)
                .Where(m => m.UserId == id);

            return Ok(centralpolicyuserdata);
        }

        // GET api/values/5
        [HttpGet("detailaccept/{id}")]
        public IActionResult GetDetailAccpet(long id)
        {
            var accept = _context.CentralPolicyUsers.Where(m => m.Id == id).FirstOrDefault();

            var centralpolicydata = _context.CentralPolicies
                .Include(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicyFiles)
                .Include(m => m.Subjects)
                .ThenInclude(m => m.Subquestions)
                .Where(m => m.Id == accept.CentralPolicyId).First();

            return Ok(centralpolicydata);
        }

        // GET api/values/5
        [HttpGet("detailaccept/users/{id}")]
        public IActionResult GetUsersAccept(long id)
        {
            var accept = _context.CentralPolicyUsers.Where(m => m.Id == id).FirstOrDefault();

            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.User)
                .Where(m => m.CentralPolicyId == accept.CentralPolicyId);

            return Ok(centralpolicyuserdata);
        }

        // GET api/values/5
        [HttpGet("centralpolicyprovince/{id}")]
        public IActionResult GetCentralPolicyProvince(long id)
        {
            var centralpolicyprovince = _context.CentralPolicyProvinces
            .Where(m => m.Id == id).FirstOrDefault();

            var centralpolicydata = _context.CentralPolicies
            .Include(m => m.CentralPolicyDates)
            .Include(m => m.CentralPolicyFiles)
            .Where(m => m.Id == centralpolicyprovince.CentralPolicyId).First();

            var subjectcentralpolicyprovincedata = _context.SubjectCentralPolicyProvinces
                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubquestionChoiceCentralPolicyProvinces)
                .Where(m => m.CentralPolicyProvinceId == id).ToList();

            return Ok( new { subjectcentralpolicyprovincedata , centralpolicydata });
            //return "value";
        }

    }
}