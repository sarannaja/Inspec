using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
                .Where(m => m.Id == id ).FirstOrDefault();

            return Ok(centralpolicydata);
            //return "value";
        }
        // GET api/values/5
        [HttpGet("fiscalfear/{id}")]
        public IActionResult Get2(long id)
        {
            var centralpolicydata = _context.CentralPolicies
                .Include(m => m.FiscalYear)
                .Include(m => m.CentralPolicyProvinces)
                .ThenInclude(x => x.Province)
                .Include(m => m.CentralPolicyDates)
                .Where(m => m.FiscalYearId == id && m.Class == "แผนการตรวจประจำปี").ToList();

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
                CreatedBy = model.UserID,
                Class = model.Class,
            };

            _context.CentralPolicies.Add(centralpolicydata);
            _context.SaveChanges();

            foreach (var id in model.ProvinceId)
            {
                var centralpolicyprovincedata = new CentralPolicyProvince
                {
                    ProvinceId = id,
                    CentralPolicyId = centralpolicydata.Id,
                    Step = "มอบหมายเขต",
                    Status = "ร่างกำหนดการ"
                };
                _context.CentralPolicyProvinces.Add(centralpolicyprovincedata);
                _context.SaveChanges();

                if (model.Class == "สมุดตรวจอิเล็กทรอนิกส์")
                {
                    foreach (var subjectid in model.SubjectId)
                    {
                        var subject = _context.SubjectCentralPolicyProvinces
                            .Where(m => m.Id == subjectid).First();

                        var subjectdata = new SubjectCentralPolicyProvince
                        {
                            CentralPolicyProvinceId = centralpolicyprovincedata.Id,
                            Name = subject.Name,
                            Type = "ElectronicBook",
                            Status = "ใช้งานจริง",
                        };
                        _context.SubjectCentralPolicyProvinces.Add(subjectdata);
                        _context.SaveChanges();
                    }
                }

            }

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
                    if (index == indexend)
                    {
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


            if (model.files != null)
            {
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
                            Name = random + filename,
                        };
                        _context.CentralPolicyFiles.Add(CentralPolicyFile);
                        _context.SaveChanges();
                    }
                }
            }
            return Ok(new { centralpolicydata.Id, model.ProvinceId });

        }

        [HttpPut("{editId}")]
        public async Task<IActionResult> Put([FromForm] CentralPolicyProvinceViewModel model, long editId)
        {
            //var eiei = model.StartDate;
            System.Console.WriteLine("test: " + editId);
            var user = model.UserID;
            System.Console.WriteLine("User: " + user);
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
                centralpolicydata.CreatedBy = model.UserID;
                centralpolicydata.Class = "แผนการตรวจประจำปี";
            };

            //_context.CentralPolicies.Add(centralpolicydata);
            _context.Entry(centralpolicydata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            var delData = _context.CentralPolicyProvinces
                   .Where(x => x.CentralPolicyId == editId)
                   .ToList();
            foreach (var del in delData)
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
                    Step = "มอบหมายเขต",
                    Status = "ร่างกำหนดการ"
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

            if (model.files != null)
            {
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
                }
            }
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
            var inviteby = _context.Users
                .Where(m => m.Id == model.InviteBy).First();

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
                System.Console.WriteLine("PLANID: " + model.planId);

                var centralpolicyuserdata = new CentralPolicyUser
                {
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    CentralPolicyGroupId = CentralPolicyGroupdata.Id,
                    UserId = id,
                    Status = "รอการตอบรับ",
                    DraftStatus = "ร่างกำหนดการ",
                    //ElectronicBookId = model.ElectronicBookId,
                    InspectionPlanEventId = model.planId,
                    InvitedBy = inviteby.Prefix + " " + inviteby.Name,
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
        [HttpGet("usersprovince/{id}/{planId}")]
        public IActionResult GetUserProvinces(long id, long planId)
        {
            var centralpolicyprovince = _context.CentralPolicyProvinces
            .Where(m => m.Id == id).FirstOrDefault();

            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.User)
                .Where(m => m.CentralPolicyId == centralpolicyprovince.CentralPolicyId && m.InspectionPlanEventId == planId);

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
        [HttpGet("usersinvited/{id}/{planid}")]
        public IActionResult GetUsers2(string id, long planid)
        {
            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyProvinces)
                  .Include(m => m.CentralPolicy)
                  .ThenInclude(m => m.FiscalYear)
                .Where(m => m.CentralPolicy.CentralPolicyEvents.Any(m => m.InspectionPlanEventId == planid))
                .Where(m => m.UserId == id);

            return Ok(centralpolicyuserdata);
        }

        // GET api/values/5
        [HttpGet("detailaccept/{id}")]
        public IActionResult GetDetailAccpet(long id)
        {
            var accept = _context.CentralPolicyUsers.Where(m => m.Id == id).FirstOrDefault();

            var centralpolicydata = _context.CentralPolicies
                .Include(m => m.CentralPolicyUser)
                //.ThenInclude(m => m.ElectronicBook)
                //.ThenInclude(x => x.ElectronicBookSuggestGroups)
                .Include(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicyFiles)
                //.Include(m => m.Subjects)
                //.ThenInclude(m => m.Subquestions)
                .Include(m => m.CentralPolicyProvinces)
                .ThenInclude(m => m.Province)
                .Where(m => m.Id == accept.CentralPolicyId).First();

            var userdata = _context.Users.Where(m => m.Id == centralpolicydata.CreatedBy).First();

            return Ok(new { centralpolicydata, userdata });
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

            var provincedata = _context.Provinces
            .Where(m => m.Id == centralpolicyprovince.ProvinceId).FirstOrDefault();

            var centralpolicydata = _context.CentralPolicies
            .Include(x => x.FiscalYear)
            .Include(m => m.CentralPolicyDates)
            .Include(m => m.CentralPolicyFiles)
            .Include(m => m.CentralPolicyProvinces)
            .ThenInclude(m => m.Province)
            .Where(m => m.Id == centralpolicyprovince.CentralPolicyId).First();

            //var userdata = _context.Users.Where(m => m.Id == centralpolicydata.CreatedBy).First();

            //var subjectcentralpolicyprovincedata = _context.SubjectCentralPolicyProvinces
            //    .Include(m => m.SubquestionCentralPolicyProvinces)
            //    .ThenInclude(m => m.SubquestionChoiceCentralPolicyProvinces)

            //    .Include(m => m.SubquestionCentralPolicyProvinces)
            //    .ThenInclude(m => m.SubjectCentralPolicyProvinceUserGroups)
            //    .ThenInclude(m => m.User)

            //    .Include(m => m.SubquestionCentralPolicyProvinces)
            //    .ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
            //    .ThenInclude(m => m.ProvincialDepartment)
            //    .Include(x => x.ElectronicBookSuggestGroups)

            //    .Include(m => m.SubquestionCentralPolicyProvinces)
            //    .ThenInclude(x => x.AnswerSubquestions)

            //    .Include(m => m.SubquestionCentralPolicyProvinces)
            //    .ThenInclude(x => x.AnswerSubquestionOutsiders)

            //    .Where(m => m.Type == "NoMaster")
            //    .Where(m => m.CentralPolicyProvinceId == id).ToList();

            System.Console.WriteLine("CentralPolicyId" + centralpolicyprovince.CentralPolicyId);
            System.Console.WriteLine("InspectionPlanEventId" + centralpolicyprovince.ProvinceId);

            var InspectionPlanEventdata = _context.InspectionPlanEvents
                .Include(m => m.CentralPolicyEvents)
                .Where(m => m.CentralPolicyEvents.Any(i => i.CentralPolicyId == centralpolicyprovince.CentralPolicyId) && m.ProvinceId == centralpolicyprovince.ProvinceId).FirstOrDefault();

            //.Where(m => m.ProvinceId == centralpolicyprovince.ProvinceId).FirstOrDefault();


            //System.Console.WriteLine("CentralPolicyId" + centralpolicyprovince.CentralPolicyId);
            //System.Console.WriteLine("InspectionPlanEventId" + InspectionPlanEventdata.Id);


            var answerPeople = _context.SubjectCentralPolicyProvinces
                .Include(x => x.SubquestionCentralPolicyProvinces)
                .ThenInclude(x => x.AnswerSubquestionOutsiders)
                .Where(x => x.Type == "NoMaster")
                .Where(x => x.CentralPolicyProvinceId == id)
                .ToList();

            if (InspectionPlanEventdata != null)
            {
                var CentralPolicyEventdata = _context.CentralPolicyEvents
                //.Include(m => m.ElectronicBook)
                .Where(m => m.CentralPolicyId == centralpolicyprovince.CentralPolicyId && m.InspectionPlanEventId == InspectionPlanEventdata.Id)
                //.Where(m => m.InspectionPlanEventId == InspectionPlanEventdata.Id)
                .FirstOrDefault();

                var userdata = _context.Users.Where(m => m.Id == CentralPolicyEventdata.InspectionPlanEvent.CreatedBy).First();
                return Ok(new {  centralpolicydata, userdata, CentralPolicyEventdata, provincedata, centralpolicyprovince });
            }
            else
            {
                var userdata = "";
                var CentralPolicyEventdata = "";
                return Ok(new {  centralpolicydata, userdata, CentralPolicyEventdata, provincedata, centralpolicyprovince,answerPeople });
            }


            //return "value";
        }


        [HttpGet("userfile/{userId}")]
        public IActionResult GetUserFile(long userId)
        {
            var report = _context.CentralPolicyUsers
               .Where(x => x.Id == userId)
               .Select(x => x.Report)
               .First();

            var status = _context.CentralPolicyUsers
               .Where(x => x.Id == userId)
               .Select(x => x.DraftStatus)
               .First();

            var centralGroupId = _context.CentralPolicyUsers
                .Where(x => x.Id == userId)
                .Select(x => x.CentralPolicyGroupId)
                .First();

            var userFile = _context.CentralPolicyUserFiles
                .Where(x => x.CentralPolicyGroupId == centralGroupId)
                .ToList();

            return Ok(new { report, status, userFile });
        }


        // POST api/values
        [HttpPut("reportcentralpolicy/{id}")]
        public async Task<IActionResult> PostReportCentralPolicy([FromForm]CentralPolicyProvinceViewModel model, CentralPolicyUserModel userModel, long id)
        {
            System.Console.WriteLine("UserID: " + id);
            System.Console.WriteLine("Report: " + userModel.Report);
            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Where(x => x.Id == id)
                .First();

            (from t in _context.CentralPolicyUsers where t.Id == id select t).ToList().
                ForEach(x => x.Report = userModel.Report);

            (from t in _context.CentralPolicyUsers where t.Id == id select t).ToList().
                ForEach(x => x.DraftStatus = userModel.DraftStatus);

            System.Console.WriteLine("IN2");
            _context.SaveChanges();

            System.Console.WriteLine("IN3");

            //ตรวจสอบว่ามี Folder Upload ใน wwwroot มั้ย
            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";

            System.Console.WriteLine("IN4");

            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {
                    System.Console.WriteLine("IN5");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("IN6");
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        var CentralPolicyUserFile = new CentralPolicyUserFile
                        {
                            CentralPolicyGroupId = centralpolicyuserdata.CentralPolicyGroupId,
                            Name = random + filename,
                            Description = model.Description,
                            Type = model.fileType
                        };
                        _context.CentralPolicyUserFiles.Add(CentralPolicyUserFile);
                        _context.SaveChanges();
                        System.Console.WriteLine("IN7");
                    }
                    System.Console.WriteLine("IN8");
                }
            }
            return Ok(new { status = true });

        }

        [HttpDelete("deleteuserfile/{id}")]
        public void DeleteUserFile(long id)
        {
            System.Console.WriteLine("ID: ", id);
            var centralpolicyuserfiledata = _context.CentralPolicyUserFiles.Find(id);

            _context.CentralPolicyUserFiles.Remove(centralpolicyuserfiledata);
            _context.SaveChanges();
        }

        // GET api/values/5
        [HttpGet("subjectcentralpolicyprovince/{id}")]
        public IActionResult GetSubjectCentralPolicyProvince(long id)
        {
            var centralpolicyuser = _context.CentralPolicyUsers
            .Where(m => m.Id == id).FirstOrDefault();

            System.Console.WriteLine("ID1: " + centralpolicyuser.CentralPolicyId);
            System.Console.WriteLine("ID2: " + centralpolicyuser.ProvinceId);

            var centralpolicyprovincedata = _context.CentralPolicyProvinces
            .Where(m => m.CentralPolicyId == centralpolicyuser.CentralPolicyId)
            .Where(m => m.ProvinceId == centralpolicyuser.ProvinceId)
            .FirstOrDefault();

            System.Console.WriteLine("ID3: " + centralpolicyprovincedata.Id);

            var subjectcentralpolicyprovincedata = _context.SubjectCentralPolicyProvinces
                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubquestionChoiceCentralPolicyProvinces)
                .Include(x => x.ElectronicBookSuggestGroups)
                .Where(m => m.CentralPolicyProvinceId == centralpolicyprovincedata.Id)
                .ToList();

            return Ok(subjectcentralpolicyprovincedata);
            //return "value";
        }

        [HttpPut("sendassign/{id}")]
        public void PutAssign(long id, string assign)
        {
            (from t in _context.CentralPolicyUsers where t.Id == id select t).ToList().
                ForEach(x => x.Forward = assign);
            (from t in _context.CentralPolicyUsers where t.Id == id select t).ToList().
               ForEach(x => x.Status = "มอบหมาย");
            _context.SaveChanges();
        }

        [HttpGet("getassign/{id}")]
        public IActionResult getassign(long id)
        {
            var centralPolicyUserData = _context.CentralPolicyUsers
                .Where(m => m.Id == id)
                .FirstOrDefault();

            return Ok(centralPolicyUserData);
        }

        //POST api/values
        [HttpPost("adddepartment")]
        public void Post2([FromBody] SubjectCentralPolicyProvinceGroupModel model)
        {
            var subjectdata = _context.SubjectCentralPolicyProvinces
                .Where(m => m.Id == model.SubjectCentralPolicyProvinceId).FirstOrDefault();

            var subquestdatas = _context.SubquestionCentralPolicyProvinces
                .Where(m => m.SubjectCentralPolicyProvinceId == subjectdata.Id).ToList();

            foreach (var subquestdata in subquestdatas)
            {
                foreach (var DepartmentIddata in model.DepartmentId)
                {
                    var SubjectCentralPolicyProvinceGroup = new SubjectCentralPolicyProvinceGroup
                    {
                        SubquestionCentralPolicyProvinceId = subquestdata.Id,
                        ProvincialDepartmentId = DepartmentIddata
                    };
                    _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroup);
                    _context.SaveChanges();
                }
            }
        }

        //POST api/values
        [HttpPost("addpeopleanswer")]
        public void Post3([FromBody] SubjectCentralPolicyProvinceUserGroupModel model)
        {
            var subjectdata = _context.SubjectCentralPolicyProvinces
                .Where(m => m.Id == model.SubjectCentralPolicyProvinceId).FirstOrDefault();

            var subquestdatas = _context.SubquestionCentralPolicyProvinces
                .Where(m => m.SubjectCentralPolicyProvinceId == subjectdata.Id).ToList();

            //foreach (var subquestdata in subquestdatas)
            //{
            //    foreach (var DepartmentIddata in model.UserId)
            //    {
            //        var SubjectCentralPolicyProvinceGroup = new SubjectCentralPolicyProvinceUserGroup
            //        {
            //            SubquestionCentralPolicyProvinceId = subquestdata.Id,
            //            UserId = DepartmentIddata
            //        };
            //        _context.SubjectCentralPolicyProvinceUserGroups.Add(SubjectCentralPolicyProvinceGroup);
            //        _context.SaveChanges();
            //    }
            //}
        }

        //POST api/values
        [HttpPost("addeditdepartment")]
        public void Post4([FromBody] SubjectCentralPolicyProvinceGroupModel model)
        {
            System.Console.WriteLine("Start");
            var subjectdata = _context.SubjectCentralPolicyProvinces
                .Where(m => m.Id == model.SubjectCentralPolicyProvinceId).FirstOrDefault();

            var subquestdatas = _context.SubquestionCentralPolicyProvinces
                .Where(m => m.SubjectCentralPolicyProvinceId == subjectdata.Id).ToList();

            foreach (var subquestdata in subquestdatas)
            {
                System.Console.WriteLine("in1");
                if (subquestdata.Box == model.Box)
                {
                    System.Console.WriteLine("in2");
                    foreach (var DepartmentIddata in model.DepartmentId)
                    {
                        System.Console.WriteLine("in3");
                        var SubjectCentralPolicyProvinceGroup = new SubjectCentralPolicyProvinceGroup
                        {
                            SubquestionCentralPolicyProvinceId = subquestdata.Id,
                            ProvincialDepartmentId = DepartmentIddata
                        };
                        _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroup);
                        _context.SaveChanges();
                    }
                }

            }
        }

        [HttpGet("comment/{id}")]
        public IActionResult GetComment(long id)
        {
            System.Console.WriteLine("CentralPolicyProvinceId: " + id);
            List<object> termsList = new List<object>();
            var subjectCentralPolicyData = _context.SubjectCentralPolicyProvinces
                .Where(x => x.CentralPolicyProvinceId == id && x.Type == "NoMaster")
                .ToList();

            foreach (var subjectCentralPolicyId in subjectCentralPolicyData)
            {
                var commentData = _context.SubjectCentralPolicyProvinces
                    .Include(x => x.SuggestionSubjects)
                    .ThenInclude(x => x.User)
                    .Where(x => x.Id == subjectCentralPolicyId.Id && x.Type == "NoMaster")
                    .FirstOrDefault();

                termsList.Add(commentData);
            }
            return Ok(termsList);
        }

        [HttpGet("getanswerpeople/{centralPolicyProvinceId}")]
        public IActionResult GetAnswerPeople(long centralPolicyProvinceId)
        {
            var answerData = _context.AnswerCentralPolicyProvinces
                .Include(x => x.User)
                .Where(x => x.CentralPolicyProvinceId == centralPolicyProvinceId)
                .ToList();

            return Ok(answerData);
        }

        // GET api/values/5
        [HttpGet("subjectevent/{id}/{subjectgroupid}")]
        public IActionResult GetSubjectEvent(long id , long subjectgroupid)
        {
            var subjectcentralpolicyprovincedata = _context.SubjectCentralPolicyProvinces
                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubquestionChoiceCentralPolicyProvinces)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubjectCentralPolicyProvinceUserGroups)
                .ThenInclude(m => m.User)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
                .ThenInclude(m => m.ProvincialDepartment)
                .Include(x => x.ElectronicBookSuggestGroups)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(x => x.AnswerSubquestions)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(x => x.AnswerSubquestionOutsiders)

                .Where(m => m.Type == "NoMaster")
                .Where(m => m.SubjectGroupId == subjectgroupid)
                .Where(m => m.CentralPolicyProvinceId == id).ToList();


            return Ok(subjectcentralpolicyprovincedata);
        }
    }
}