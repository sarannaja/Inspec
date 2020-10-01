using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectronicBookController : Controller
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

        public ElectronicBookController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/ElectronicBook
        [HttpGet("{userId}")]
        public IActionResult Get(string userId)
        {

            System.Console.WriteLine("UserIdNaja: " + userId);

            //var user = _context.Users
            //    .Where(x => x.Id == userId)
            //    .Select(x => x.Name)
            //    .First();
            //System.Console.WriteLine("Name: " + user);

            var ebook = _context.ElectronicBooks
                .Include(x => x.ElectronicBookGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.CentralPolicy)

                .Include(x => x.ElectronicBookGroups)
                .ThenInclude(x => x.CentralPolicyEvent)
                .ThenInclude(x => x.InspectionPlanEvent)
                .Where(x => x.CreatedBy == userId)
                .OrderByDescending(x => x.Id)
                .ToList();

            //var ebook = _context.CentralPolicyEvents
            //    .Include(m => m.InspectionPlanEvent.Province)
            //    //.ThenInclude(m => m.Province)
            //    .Include(m => m.CentralPolicy)
            //    //.ThenInclude(m => m.CentralPolicyProvinces)
            //    .Include(m => m.ElectronicBook)
            //    .Where(m => m.ElectronicBook.CreatedBy == userId);

            return Ok(ebook);
        }
        // GET: api/ElectronicBook
        [HttpGet("invited/{userId}")]
        public IActionResult Get4(string userId)
        {
            var electronicBookInvite = _context.ElectronicBookInvites
            .Include(x => x.ElectronicBook)
            .ThenInclude(x => x.ElectronicBookGroups)
            .ThenInclude(x => x.CentralPolicyEvent)
            .ThenInclude(x => x.CentralPolicy)
            .Include(x => x.User)
            .Include(x => x.ElectronicBook)
            .ThenInclude(x => x.User)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Id)
            .ToList();

            return Ok(electronicBookInvite);
        }

        [HttpGet("getElectronicBookById/{electID}")]
        public IActionResult GetById(long electID)
        {
            // var user = _context.Users.FirstOrDefault();
            //var accept = _context.CentralPolicyUsers.Where(m => m.Id == centralPolicyUserId).FirstOrDefault();

            //var centralpolicydata = _context.CentralPolicies
            //    .Include(m => m.CentralPolicyUser)
            //    .ThenInclude(m => m.ElectronicBook)
            //    .ThenInclude(x => x.ElectronicBookFiles)
            //    .Include(m => m.CentralPolicyUser)
            //    .ThenInclude(m => m.User)
            //    .Include(m => m.CentralPolicyUser)
            //    .ThenInclude(m => m.CentralPolicyGroup)
            //    .ThenInclude(m => m.CentralPolicyUserFiles)
            //    .Include(m => m.CentralPolicyDates)
            //    .Include(m => m.CentralPolicyFiles)
            //    //.Include(m => m.Subjects)
            //    //.ThenInclude(m => m.Subquestions)
            //    .Include(m => m.CentralPolicyProvinces)
            //    .ThenInclude(m => m.Province)
            //    .Where(m => m.Id == centralPolicyUserId).First();

            // var electData = _context.ElectronicBooks
            //     .Include(x => x.ElectronicBookFiles)
            //     .Where(x => x.Id == electID)
            //     .FirstOrDefault();

            var electronicBook = _context.ElectronicBooks
            .Include(x => x.User)
            .Include(x => x.ElectronicBookFiles)
            .Where(x => x.Id == electID)
            .FirstOrDefault();

            var electronicBookGroup = _context.ElectronicBookGroups
            .Include(x => x.ElectronicBook)
            .ThenInclude(x => x.ElectronicBookSuggestGroups)

            .Include(x => x.CentralPolicyEvent)
            .ThenInclude(x => x.CentralPolicy)
            .ThenInclude(x => x.CentralPolicyUser)
            .ThenInclude(x => x.User)
            .ThenInclude(x => x.Departments)

            .Include(x => x.CentralPolicyEvent)
            .ThenInclude(x => x.CentralPolicy)
            .ThenInclude(x => x.CentralPolicyProvinces)
            .ThenInclude(x => x.SubjectCentralPolicyProvinces)
            .ThenInclude(x => x.SubquestionCentralPolicyProvinces)
            .ThenInclude(x => x.SubjectCentralPolicyProvinceGroups)
            .ThenInclude(x => x.ProvincialDepartment)

            .Include(x => x.CentralPolicyEvent)
            .ThenInclude(x => x.InspectionPlanEvent)
            .ThenInclude(x => x.Province)

            .Where(x => x.ElectronicBookId == electID && x.CentralPolicyEvent.InspectionPlanEvent.Status == "ใช้งานจริง")

            // .Where(m => m.CentralPolicyEvent.CentralPolicy
            // .CentralPolicyProvinces.Any(m => m.SubjectCentralPolicyProvinces
            // .Any(m => m.SubquestionCentralPolicyProvinces.Any(m => m.SubjectCentralPolicyProvinceGroups
            // .Any(m => m.ProvincialDepartmentId == user.ProvincialDepartmentId)))))

             .Select(x => new
             {
                 user = x.CentralPolicyEvent.CentralPolicy.CentralPolicyUser,

                 //provincialDepartmentId = x.CentralPolicyEvent.CentralPolicy.CentralPolicyProvinces
                 //    .Select(m => m.SubjectCentralPolicyProvinces
                 //    .Select(n => n.SubquestionCentralPolicyProvinces
                 //    .Select(b => b.SubjectCentralPolicyProvinceGroups
                 //    .Select(v => v.ProvincialDepartmentId)))),


                 provincialDepartmentId = x.CentralPolicyEvent.CentralPolicy.SubjectGroups
                 .Where(x => x.Status == "ใช้งานจริง")
                 .Select(x => x.SubjectCentralPolicyProvinces
                 .Select(x => x.SubquestionCentralPolicyProvinces
                 .Select(x => x.SubjectCentralPolicyProvinceGroups
                 .Select(x => x.ProvincialDepartmentId)))),

                 inspectionPlanProvinceName = x.CentralPolicyEvent.InspectionPlanEvent.Province.Name,
                 centralPolicyTitle = x.CentralPolicyEvent.CentralPolicy.Title,
                 inspectionPlanEventDate = new
                 {
                     startDate = x.CentralPolicyEvent.InspectionPlanEvent.StartDate,
                     endDate = x.CentralPolicyEvent.InspectionPlanEvent.EndDate
                 },
                 inspectionPlanEventProvince = x.CentralPolicyEvent.InspectionPlanEvent.Province,
                 inspectionPlanEventId = x.CentralPolicyEvent.InspectionPlanEventId,
                 provinceId = x.CentralPolicyEvent.InspectionPlanEvent.ProvinceId,
                 centralPolicyId = x.CentralPolicyEvent.CentralPolicyId
             })

            .ToList();

            var electronicBookSuggestion = _context.ElectronicBookSuggestGroups
            .Include(x => x.CentralPolicyEvent)
            .ThenInclude(x => x.CentralPolicy)
            .Where(x => x.ElectronicBookId == electID)
            .ToList();

            var ebookInvite = _context.ElectronicBookInvites
            .Include(x => x.User)
            .ThenInclude(x => x.Ministries)

            .Include(x => x.User)
            .ThenInclude(x => x.Departments)

            .Include(x => x.User)
            .ThenInclude(x => x.ProvincialDepartments)

            .Where(x => x.ElectronicBookId == electID)
            .ToList();

            var electronicBookAccept = _context.ElectronicBookAccepts
                .Include(x => x.User)
                .Include(x => x.ElectronicBookProvinceApproveFiles)

                //.Include(x => x.ElectronicBookOtherAccepts)
                //.ThenInclude(x => x.ProvincialDepartment)

                //.Include(x => x.ElectronicBookOtherAccepts)
                //.ThenInclude(x => x.User)

                .Include(x => x.Province)

                .Where(x => x.ElectronicBookId == electID)
                .ToList();

            var electronicBookDepartment = _context.ElectronicBookProvincialDepartments
            .Include(x => x.UserCreate)
            .Include(x => x.UserProvincialDepartment)
            .Include(x => x.ProvincialDepartments)

            .Include(x => x.ElectronicBookOtherAccepts)
            .ThenInclude(x => x.ProvincialDepartment)
            .Include(x => x.ElectronicBookOtherAccepts)
            .ThenInclude(x => x.User)

            .Where(x => x.ElectronicBookId == electID)
            .ToList();

            // var report = _context.CentralPolicyUsers
            //     .Include(x => x.User)
            //     .Include(x => x.CentralPolicyGroup)
            //     .ThenInclude(x => x.CentralPolicyUserFiles)
            //     //.Where(x => x.ElectronicBookId == electID)
            //     .ToList();

            return Ok(new { electronicBook, electronicBookGroup, electronicBookSuggestion, ebookInvite, electronicBookAccept, electronicBookDepartment });
        }

        [HttpGet("getCalendarFile/{planId}/{cenproid}")]
        public IActionResult GetCalendarFile(long planId, long cenproid)
        {
            //System.Console.WriteLine("ELECT ID: " + electID);
            //var accept = _context.CentralPolicyUsers.Where(m => m.Id == centralPolicyUserId).FirstOrDefault();
            var cenpro = _context.CentralPolicyProvinces
                .Where(m => m.Id == cenproid).FirstOrDefault();

            var carlendarFile = _context.CalendarFiles
                .Where(x => x.InspectionPlanEventId == planId && x.CentralPolicyId == cenpro.CentralPolicyId && x.Type == "Calendar File" || x.Type == "Calendar Image File")
                .ToList();

            var signatureFile = _context.CalendarFiles
               .Where(x => x.InspectionPlanEventId == planId && x.CentralPolicyId == cenpro.CentralPolicyId && x.Type == "Calendar Signature File")
               .ToList();

            return Ok(new { carlendarFile, signatureFile });
        }

        [HttpGet("getElectronicbookFile/{electID}")]
        public IActionResult getElectronicbookFile(long electID)
        {
            System.Console.WriteLine("ELECT ID: " + electID);
            //var accept = _context.CentralPolicyUsers.Where(m => m.Id == centralPolicyUserId).FirstOrDefault();

            //var electronicFile = _context.ElectronicBookFiles
            //    .Where(x => x.ElectronicBookId == electID && x.Type == "ElectronicBook File")
            //    .ToList();

            var electronicFile = _context.ElectronicBookFiles
               .Where(x => x.ElectronicBookId == electID && x.Type == "ElectronicBook File" || x.Type == "ElectronicBook Image File")
               .ToList();

            var signatureFile = _context.ElectronicBookFiles
               .Where(x => x.ElectronicBookId == electID && x.Type == "ElectronicBook Signature File")
               .ToList();

            return Ok(new { electronicFile, signatureFile });
        }

        [HttpPut("editElectronicBookDetail/{id}")]
        public async Task<IActionResult> Put([FromForm] ElectronicBookViewModel model, long id)
        {
            var test = model.Detail;
            System.Console.WriteLine("detail ja: " + test);
            var electronicBookDetail = _context.ElectronicBooks.Find(id);
            {
                //electronicBookDetail.Detail = model.Detail;
                electronicBookDetail.Status = model.Status;
            }
            _context.Entry(electronicBookDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            System.Console.WriteLine("1: ");

            var provinceId = _context.ElectronicBookGroups
                .Where(x => x.ElectronicBookId == id)
                // .Select(x => x.CentralPolicyProvinceId)
                .First();

            System.Console.WriteLine("2: ");
            var centralPolicyId = _context.CentralPolicyProvinces
                // .Where(x => x.Id == provinceId)
                .Select(x => x.CentralPolicyId)
                .First();



            System.Console.WriteLine("3: ");

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

                        var ElectronicBookFileData = new ElectronicBookFile
                        {
                            ElectronicBookId = id,
                            Name = random + filename,
                            Description = model.Description,
                            Type = model.Type
                        };
                        _context.ElectronicBookFiles.Add(ElectronicBookFileData);
                        System.Console.WriteLine("in5");
                        _context.SaveChanges();
                        System.Console.WriteLine("in6");
                        //_context.Entry(CentralPolicyFile).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }
            }

            return Ok(new { status = true });
        }

        // GET api/values/5
        [HttpGet("getcentralpolicyfromprovince/{id}")]
        public IActionResult getcentralpolicyfromprovince(long id)
        {
            var centralpolicyprovincedatas = _context.CentralPolicyProvinces
                .Include(m => m.CentralPolicy)
                .Where(m => m.ProvinceId == id)
                .ToList(); //All

            var electronicbookgroups = _context.ElectronicBookGroups
              .ToList(); //Chose

            List<object> termsList = new List<object>();

            foreach (var centralpolicyprovincedata in centralpolicyprovincedatas)
            {
                for (int i = 0; i < electronicbookgroups.Count(); i++)
                {
                    // if (centralpolicyprovincedata.Id == electronicbookgroups[i].CentralPolicyProvinceId)
                    termsList.Add(centralpolicyprovincedata);
                }
            }

            return Ok(termsList);
        }

        // POST: api/ElectronicBook
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ElectronicBookViewModel model)
        {
            var test1 = model.Detail;
            //var test2 = model.UserId;

            System.Console.WriteLine("Detail: " + test1);
            //System.Console.WriteLine("UserId: " + test2);



            var ElectronicBookdata = new ElectronicBook
            {
                Detail = model.Detail,
                Problem = model.Problem,
                Suggestion = model.Suggestion,
                CreatedBy = model.id,
                Status = model.Status
            };
            System.Console.WriteLine("1");

            _context.ElectronicBooks.Add(ElectronicBookdata);
            _context.SaveChanges();

            System.Console.WriteLine("2");

            //var CentralPolicyId = model.CentralPolicyId;
            //var ProvinceId = model.ProvinceId;

            //System.Console.WriteLine("CentralPolicyId" + CentralPolicyId);
            //System.Console.WriteLine("ProvinceId" + ProvinceId);

            //System.Console.WriteLine("3");

            //var centralpolicyprovinceid = _context.CentralPolicyProvinces
            //    .Where(m => m.CentralPolicyId == CentralPolicyId)
            //    .Where(m => m.ProvinceId == ProvinceId)
            //    .Select(m => m.Id).First();

            //var SubjectCentralPolicyProvincesdatas = _context.SubjectCentralPolicyProvinces
            //    .Where(m => m.CentralPolicyProvinceId == centralpolicyprovinceid).ToList();

            //foreach (var itemProvincialDepartmentId in model.ProvincialDepartmentId)
            //{

            //    foreach (var SubjectCentralPolicyProvincesdata in SubjectCentralPolicyProvincesdatas)
            //    {
            //        var SubjectCentralPolicyProvinceGroupdata = new SubjectCentralPolicyProvinceGroup
            //        {
            //            ProvincialDepartmentId = itemProvincialDepartmentId,
            //            SubjectCentralPolicyProvinceId = SubjectCentralPolicyProvincesdata.Id
            //        };
            //        _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata);
            //        _context.SaveChanges();
            //    }
            //}

            //System.Console.WriteLine("3.5" + centralpolicyprovinceid);



            var centralPolicyID = _context.CentralPolicyProvinces
                .Where(x => x.CentralPolicyId == model.CentralPolicyId)
                .Select(x => x.Id)
                .FirstOrDefault();

            System.Console.WriteLine("CentralPolicyProvince: " + centralPolicyID);

            var ElectronicBookgroupdata = new ElectronicBookGroup
            {
                ElectronicBookId = ElectronicBookdata.Id,
                // CentralPolicyProvinceId = centralPolicyID
            };
            _context.ElectronicBookGroups.Add(ElectronicBookgroupdata);
            _context.SaveChanges();

            //System.Console.WriteLine("3.8");

            foreach (var itemUserPeopleId in model.UserPeopleId)
            {
                var CentralPolicyGroupdata = new CentralPolicyGroup
                {
                };
                _context.CentralPolicyGroups.Add(CentralPolicyGroupdata);
                _context.SaveChanges();

                System.Console.WriteLine("3.9");
                System.Console.WriteLine("USERPeople: " + itemUserPeopleId);

                var CentralPolicyUserdata = new CentralPolicyUser
                {
                    CentralPolicyId = model.CentralPolicyId,
                    ProvinceId = model.ProvinceId,
                    //ElectronicBookId = ElectronicBookdata.Id,
                    CentralPolicyGroupId = CentralPolicyGroupdata.Id,
                    UserId = itemUserPeopleId,
                    Status = "รอการตอบรับ",
                    DraftStatus = "ร่างกำหนดการ",

                };
                _context.CentralPolicyUsers.Add(CentralPolicyUserdata);
                _context.SaveChanges();
            }
            System.Console.WriteLine("4");

            foreach (var itemUserMinistryId in model.UserMinistryId)
            {
                var CentralPolicyGroupdata2 = new CentralPolicyGroup
                {
                };
                _context.CentralPolicyGroups.Add(CentralPolicyGroupdata2);
                _context.SaveChanges();
                System.Console.WriteLine("5");
                var CentralPolicyUserdata2 = new CentralPolicyUser
                {
                    CentralPolicyId = model.CentralPolicyId,
                    ProvinceId = model.ProvinceId,
                    //ElectronicBookId = ElectronicBookdata.Id,
                    CentralPolicyGroupId = CentralPolicyGroupdata2.Id,
                    UserId = itemUserMinistryId,
                    Status = "รอการตอบรับ",
                    DraftStatus = "ร่างกำหนดการ",
                };
                _context.CentralPolicyUsers.Add(CentralPolicyUserdata2);
                _context.SaveChanges();
                System.Console.WriteLine("6");
            }

            System.Console.WriteLine("Start Upload");

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";

            if (model.files != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {

                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {

                        System.Console.WriteLine("Start Upload 4");
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        System.Console.WriteLine("Start Upload 4.1");
                        var ElectronicBookFile = new ElectronicBookFile
                        {
                            ElectronicBookId = ElectronicBookdata.Id,
                            Name = random + filename,
                            Description = model.Description,
                            Type = model.Type
                        };

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.ElectronicBookFiles.Add(ElectronicBookFile);
                        _context.SaveChanges();

                        System.Console.WriteLine("Start Upload 4.3");
                    }

                    System.Console.WriteLine("Start Upload 5");
                }
            }
            return Ok(new { status = true });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            System.Console.WriteLine("ID: " + id);
            var electronicBookData = _context.ElectronicBooks.Find(id);

            _context.ElectronicBooks.Remove(electronicBookData);
            _context.SaveChanges();

            var data = electronicBookData.ElectronicBookGroups.ToArray();

            var logdata = new Log
            {
                UserId = electronicBookData.CreatedBy,
                DatabaseName = "ElectronicBook",
                EventType = "ลบ",
                EventDate = DateTime.Now,
                Detail = "ลบสมุดตรวจอิเล็กทรอนิกส์" + data[0].CentralPolicyEvent.CentralPolicy.Title.ToString(),
                Allid = electronicBookData.Id,
            };
            _context.Logs.Add(logdata);
            _context.SaveChanges();
        }

        [HttpGet("electronicbookfile/{electronicBookId}")]
        public IActionResult GetUserFile(long userId)
        {
            var report = _context.CentralPolicyUsers
               .Where(x => x.Id == userId)
               .Select(x => x.Report)
               .First();

            var centralGroupId = _context.CentralPolicyUsers
                .Where(x => x.Id == userId)
                .Select(x => x.CentralPolicyGroupId)
                .First();

            var userFile = _context.CentralPolicyUserFiles
                .Where(x => x.CentralPolicyGroupId == centralGroupId)
                .ToList();



            return Ok(new { report, userFile });
        }

        [HttpDelete("deletefile/{id}")]
        public void DeleteFile(long id)
        {
            var electronicBookFileData = _context.ElectronicBookFiles.Find(id);

            _context.ElectronicBookFiles.Remove(electronicBookFileData);
            _context.SaveChanges();
        }
        // POST: api/ElectronicBook
        [HttpPost("calendarfile")]
        public async Task<IActionResult> Post2([FromForm] CalendarFileViewModel model)
        {

            var CentralPolicyProvincedata = _context.CentralPolicyProvinces
                .Where(m => m.Id == model.CentralPolicyProvinceId).FirstOrDefault();

            //if(model.Status != null)
            //{

            //}
            //var CentralPolicyProvincedata = _context.CentralPolicyProvinces.Find(model.CentralPolicyProvinceId);
            ////CentralPolicyProvincedata.Step = model.Step;
            //CentralPolicyProvincedata.Status = model.Status;
            //CentralPolicyProvincedata.QuestionPeople = model.QuestionPeople;
            //_context.Entry(CentralPolicyProvincedata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //_context.SaveChanges();

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";

            System.Console.WriteLine("Start Upload 2");

            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {

                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {

                        System.Console.WriteLine("Start Upload 4");
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + ext))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        System.Console.WriteLine("Start Upload 4.1");
                        var ElectronicBookFile = new CalendarFile
                        {
                            CentralPolicyId = CentralPolicyProvincedata.CentralPolicyId,
                            InspectionPlanEventId = model.ElectronicBookId,
                            Name = random + ext,
                            Type = "Calendar File",
                            Description = Path.GetFileNameWithoutExtension(filePath2),
                        };

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.CalendarFiles.Add(ElectronicBookFile);
                        _context.SaveChanges();

                        System.Console.WriteLine("Start Upload 4.3");
                    }

                    System.Console.WriteLine("Start Upload 5");
                }
            }

            if (model.signatureFiles != null)
            {
                foreach (var formFile in model.signatureFiles.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {

                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {

                        System.Console.WriteLine("Start Upload 4");
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        System.Console.WriteLine("Start Upload 4.1");
                        var ElectronicBookFile = new ElectronicBookFile
                        {
                            ElectronicBookId = model.ElectronicBookId,
                            Name = random + filename,
                            Type = "Calendar Signature File"
                        };

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.ElectronicBookFiles.Add(ElectronicBookFile);
                        _context.SaveChanges();

                        System.Console.WriteLine("Start Upload 4.3");
                    }

                    System.Console.WriteLine("Start Upload 5");
                }
            }



            return Ok(new { status = true });
        }

        // GET: api/ElectronicBook
        [HttpGet("province/{userId}")]
        public IActionResult Get2(string userId)
        {
            var user = _context.Users
                .Where(m => m.Id == userId).FirstOrDefault();

            var provinceuser = _context.UserProvinces
                .Where(m => m.UserID == user.Id).FirstOrDefault();

            if (user.Role_id == 5)
            {
                var ebook = _context.ElectronicBookGroups
                                // .Include(x => x.CentralPolicyProvince)
                                // .ThenInclude(x => x.Province)
                                // .Include(x => x.CentralPolicyProvince)
                                // .ThenInclude(x => x.CentralPolicy)
                                // .ThenInclude(x => x.CentralPolicyUser)
                                .Include(x => x.ElectronicBook)
                                .Where(x => x.ElectronicBook.Status == "ใช้งานจริง")
                                // .Where(x => x.CentralPolicyProvince.Province.Id == provinceuser.ProvinceId)
                                .ToList();
                return Ok(ebook);
            }
            else
            {
                var ebook = _context.ElectronicBookGroups
                // .Include(x => x.ElectronicBookAccepts)
                // .Include(x => x.CentralPolicyProvince)
                // .ThenInclude(x => x.Province)
                // .Include(x => x.CentralPolicyProvince)
                // .ThenInclude(x => x.CentralPolicy)
                // .ThenInclude(x => x.CentralPolicyUser)
                .Include(x => x.ElectronicBook)
                .ThenInclude(x => x.ElectronicBookFiles)
                //.Where(x => x.ElectronicBook.ElectronicBookFiles.Any(x => x.Type == "SignatureProvince File"))
                .Where(x => x.ElectronicBook.Status == "ใช้งานจริง")
                // .Where(x => x.CentralPolicyProvince.SubjectCentralPolicyProvinces.Any(x => x.SubquestionCentralPolicyProvinces.Any(x => x.SubjectCentralPolicyProvinceGroups.Any(x => x.ProvincialDepartment.DepartmentId == user.DepartmentId))))
                .ToList();

                return Ok(ebook);
            }


        }

        [HttpPost("addSuggestion")]
        public void PostSuggestion([FromForm] ElectronicBookViewModel model)
        {
            var ElectSuggestionData = new ElectronicBookSuggestGroup
            {
                ElectronicBookId = model.ElectID,
                Suggestion = model.Suggestion,
                CentralPolicyEventId = model.CentralEventId
            };
            System.Console.WriteLine("1");

            _context.ElectronicBookSuggestGroups.Add(ElectSuggestionData);
            _context.SaveChanges();

            System.Console.WriteLine("Finish Add Suggestion");
        }

        [HttpPut("editSuggestion")]
        public void PutSuggestion([FromForm] ElectronicBookViewModel model)
        {
            var ElectSuggestionData = _context.ElectronicBooks
                .Where(x => x.Id == model.ElectID)
                .FirstOrDefault();
            {
                ElectSuggestionData.Detail = model.Detail;
                ElectSuggestionData.Problem = model.Problem;
                ElectSuggestionData.Suggestion = model.Suggestion;
                ElectSuggestionData.Status = model.Status;
            }
            _context.Entry(ElectSuggestionData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            System.Console.WriteLine("Finish Edit Suggestion");

            if (model.Status == "ใช้งานจริง")
            {
                if (ElectSuggestionData.CentralPolicy == null)
                {
                    foreach (var item in model.userId)
                    {
                        var ElectronicBookInviteData = new ElectronicBookInvite
                        {
                            ElectronicBookId = model.ElectID,
                            UserId = item,
                            Status = "รอลงความเห็น"
                        };
                        _context.ElectronicBookInvites.Add(ElectronicBookInviteData);
                        _context.SaveChanges();
                    }
                    System.Console.WriteLine("Finish Invite People");
                }
                else
                {
                    var ElectronicBookStatus = _context.ElectronicBooks
                    .Where(x => x.Id == model.ElectID)
                    .FirstOrDefault();
                    {
                        //ElectronicBookStatus.ProvinceStatus = "ส่งสมุดตรวจแล้ว";
                        //ElectronicBookStatus.Status = "ส่งสมุดตรวจแล้ว";
                        ElectronicBookStatus.Status = model.Status;
                    }
                    _context.Entry(ElectronicBookStatus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            System.Console.WriteLine("Finish Update Suggestion");
            _context.SaveChanges();

            var logdata = new Log
            {
                UserId = ElectSuggestionData.CreatedBy,
                DatabaseName = "ElectronicBook",
                EventType = "แก้ไข",
                EventDate = DateTime.Now,
                Detail = "แก้ไขสมุดตรวจอิเล็กทรอนิกส์",
                Allid = model.ElectID,
            };
            _context.Logs.Add(logdata);
            _context.SaveChanges();
        }

        [HttpPut("editSuggestionown")]
        public void PutSuggestionOwn([FromForm] ElectronicBookViewModel model)
        {
            System.Console.WriteLine("Edit ja");
            System.Console.WriteLine("Detail: " + model.Detail);
            System.Console.WriteLine("Problem: " + model.Problem);
            System.Console.WriteLine("Suggestion: " + model.Suggestion);
            System.Console.WriteLine("SubjectCentralPolicyProvinceId: " + model.SubjectCentralPolicyProvinceId);
            System.Console.WriteLine("ElectID: " + model.ElectID);

            var ElectSuggestionData = _context.ElectronicBooks
                .Where(x => x.Id == model.ElectID)
                .FirstOrDefault();

            {
                ElectSuggestionData.Detail = model.Detail;
                ElectSuggestionData.Problem = model.Problem;
                ElectSuggestionData.Suggestion = model.Suggestion;
                ElectSuggestionData.Status = model.Status;
            }
            _context.Entry(ElectSuggestionData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            System.Console.WriteLine("Finish Update Own Suggestion");
        }


        [HttpGet("suggestiondetail/{subjectCentralPolicyProvinceID}/{electID}")]
        public IActionResult GetSuggestion(long subjectCentralPolicyProvinceID, long electID)
        {
            System.Console.WriteLine("subjectCentralPolicyProvinceID: " + subjectCentralPolicyProvinceID);

            var ebook = _context.ElectronicBookSuggestGroups
                .Where(x => x.CentralPolicyEventId == subjectCentralPolicyProvinceID && x.ElectronicBookId == electID)
                .FirstOrDefault();
            return Ok(ebook);
        }

        [HttpGet("getElectronicBookOwn/{electID}")]
        public IActionResult getElectronicBookOwn(long electID)
        {
            System.Console.WriteLine("EID: " + electID);
            var electData = _context.ElectronicBooks
                .Where(m => m.Id == electID)
                .FirstOrDefault();

            return Ok(electData);
        }

        [HttpPost("addSignature")]
        public async Task<IActionResult> PostSignature([FromForm] ElectronicBookViewModel model)
        {
            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";

            System.Console.WriteLine("Start Upload 2");
            foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
            //foreach (var formFile in data.files)
            {

                System.Console.WriteLine("Start Upload 3");
                var random = RandomString(10);
                string filePath2 = formFile.Value.FileName;
                string filename = Path.GetFileName(filePath2);
                string ext = Path.GetExtension(filename);

                if (formFile.Value.Length > 0)
                {

                    System.Console.WriteLine("Start Upload 4");
                    // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                    using (var stream = System.IO.File.Create(filePath + random + filename))
                    {
                        await formFile.Value.CopyToAsync(stream);
                    }

                    System.Console.WriteLine("Start Upload 4.1");
                    var ElectronicBookFile = new ElectronicBookFile
                    {
                        ElectronicBookId = model.ElectID,
                        Name = random + filename,
                        Type = "SignatureProvince File",
                        Description = model.Description // Admin province suggestion.
                    };

                    System.Console.WriteLine("Start Upload 4.2");
                    _context.ElectronicBookFiles.Add(ElectronicBookFile);
                    _context.SaveChanges();

                    System.Console.WriteLine("Start Upload 4.3");
                }

                System.Console.WriteLine("Start Upload 5");
            }

            //var date = DateTime.Now;
            //var provincedata = new ExportRegistration
            //{
            //    Date = date,
            //    ProvinceId = 1,
            //    Subject
            //};

            //_context.ExportRegistrations.Add(provincedata);
            //_context.SaveChanges();

            return Ok(new { status = true });
        }

        [HttpGet("getSignatureFile/{electID}")]
        public IActionResult GetSignatureFile(long electID)
        {
            System.Console.WriteLine("ELECT ID Sign: " + electID);
            //var accept = _context.CentralPolicyUsers.Where(m => m.Id == centralPolicyUserId).FirstOrDefault();

            var carlendarFile = _context.ElectronicBookFiles
                .Where(x => x.ElectronicBookId == electID && x.Type == "SignatureProvince File")
                .ToList();

            return Ok(carlendarFile);
        }

        // GET: api/ElectronicBook
        [HttpGet("export/{userId}")]
        public IActionResult Get3(string userId)
        {
            //var user = _context.Users
            //    .Where(m => m.Id == userId).FirstOrDefault();

            //var provinceuser = _context.UserProvinces
            //    .Where(m => m.UserID == user.Id).FirstOrDefault();


            var ebook = _context.ElectronicBookGroups
                // .Include(x => x.CentralPolicyProvince)
                // .ThenInclude(x => x.Province)
                // .Include(x => x.CentralPolicyProvince)
                // .ThenInclude(x => x.CentralPolicy)
                // .ThenInclude(x => x.CentralPolicyUser)
                .Include(x => x.ElectronicBook)
                .Where(x => x.ElectronicBook.CreatedBy == userId && x.ElectronicBook.ElectronicBookFiles.Any(x => x.Type == "SignatureProvince File"))
                .ToList();

            //var ebook = _context.ElectronicBookGroups
            //                .Include(x => x.CentralPolicyProvince)
            //                .ThenInclude(x => x.Province)
            //                .Include(x => x.CentralPolicyProvince)
            //                .ThenInclude(x => x.CentralPolicy)
            //                .ThenInclude(x => x.CentralPolicyUser)
            //                .Include(x => x.ElectronicBook)
            //                .Where(x => x.ElectronicBook.Status == "ใช้งานจริง")
            //                .Where(x => x.CentralPolicyProvince.Province.Id == provinceuser.ProvinceId)
            //                .ToList();

            return Ok(ebook);
        }

        [HttpPost("addReport")]
        public IActionResult PostReport([FromBody] ElectronicBookViewModel model)
        {
            System.Console.WriteLine("ProviceId: " + model.ReportProvinceId);
            System.Console.WriteLine("ReportTitle: " + model.ReportTitle);
            System.Console.WriteLine("ReportYear: " + model.ReportYear);
            System.Console.WriteLine("ReportUserId: " + model.ReportUserId);
            System.Console.WriteLine("ReportStatus: " + model.ReportStatus);

            var misnistryData = _context.Users
                .Where(x => x.Id == model.ReportUserId)
                .Select(x => x.MinistryId)
                .FirstOrDefault();

            var ministryName = _context.Ministries
                .Where(x => x.Id == misnistryData)
                .Select(x => x.Name)
                .FirstOrDefault();
            System.Console.WriteLine("department: " + ministryName);
            var ExportData = new ExportReportHead
            {
                ProvinceId = model.ReportProvinceId,
                Title = model.ReportTitle,
                Year = model.ReportYear,
                Ministry = ministryName,
                Status = model.ReportStatus
            };
            System.Console.WriteLine("1");
            _context.ExportReportHeads.Add(ExportData);
            _context.SaveChanges();

            var centralPolicyProvinceData = _context.CentralPolicyProvinces
                .Where(x => x.CentralPolicyId == model.ReportCentralPolicyId && x.ProvinceId == model.ReportProvinceId)
                .FirstOrDefault();

            System.Console.WriteLine("centralPolicyProvinceData" + centralPolicyProvinceData.Id);

            var subjectCentralPolicyProvince = _context.SubjectCentralPolicyProvinces
                .Include(x => x.SubquestionCentralPolicyProvinces)
                .ThenInclude(x => x.SubjectCentralPolicyProvinceGroups)
                .ThenInclude(x => x.ProvincialDepartment)
                // .Include(x => x.ElectronicBookSuggestGroups)
                .Where(x => x.CentralPolicyProvinceId == centralPolicyProvinceData.Id)
                .ToList();

            foreach (var test in subjectCentralPolicyProvince)
            {
                // foreach (var test2 in test.ElectronicBookSuggestGroups)
                // {
                //     var ExportBodyData = new ExportReportBody
                //     {
                //         ExportReportHeadId = ExportData.Id,
                //         Subject = test.Name,
                //         // Problem = test2.Detail + " / " + test2.Problem,
                //         Suggestion = test2.Suggestion,
                //     };
                //     _context.ExportReportBodies.Add(ExportBodyData);
                //     _context.SaveChanges();

                //     foreach (var test3 in test.SubquestionCentralPolicyProvinces)
                //     {
                //         foreach (var test4 in test3.SubjectCentralPolicyProvinceGroups)
                //         {

                //             //return Ok(test4);
                //             System.Console.WriteLine(" test4.ProvincialDepartment.Name" + test4.ProvincialDepartment.Name);

                //             var ExportBodyData2 = _context.ExportReportBodies.Find(ExportBodyData.Id);
                //             {
                //                 if (ExportBodyData2.Department == null)
                //                 {
                //                     System.Console.WriteLine(" test4 ja 1" + test4.ProvincialDepartment.Name);
                //                     ExportBodyData2.Department = test4.ProvincialDepartment.Name;
                //                 }
                //                 else
                //                 {
                //                     System.Console.WriteLine(" test4 ja 2" + test4.ProvincialDepartment.Name);
                //                     ExportBodyData2.Department = ExportBodyData2.Department + ", " + test4.ProvincialDepartment.Name;
                //                 }
                //             }
                //             _context.Entry(ExportBodyData2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //         }
                //         _context.SaveChanges();
                //     }
                // }
            }
            return Ok("eiei");
        }

        [HttpGet("getelectronicbookdetailown/{centralPolicyProvinceId}")]
        public IActionResult GetElectronicBookDetailOwn(long centralPolicyProvinceId)
        {
            var centralpolicyprovince = _context.CentralPolicyProvinces
             .Where(m => m.Id == centralPolicyProvinceId).FirstOrDefault();

            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.User)
                .Where(m => m.CentralPolicyId == centralpolicyprovince.CentralPolicyId);

            var subjectData = _context.SubjectCentralPolicyProvinces
                .Where(x => x.CentralPolicyProvinceId == centralPolicyProvinceId && x.Type == "NoMaster")
                .ToList();

            return Ok(new { centralpolicyuserdata, subjectData });
        }

        // GET: api/ElectronicBook
        [HttpGet("exportexcel/{id}")]
        public IActionResult Get4(long id)
        {
            var ebook = _context.ElectronicBookGroups
                // .Include(x => x.CentralPolicyProvince)
                // .ThenInclude(x => x.Province)
                // .Include(x => x.CentralPolicyProvince)
                // .ThenInclude(x => x.CentralPolicy)
                // .ThenInclude(x => x.CentralPolicyUser)
                .Include(x => x.ElectronicBook)
                // .Include(x => x.CentralPolicyProvince)
                // .ThenInclude(x => x.SubjectCentralPolicyProvinces)
                // .Where(x => x.CentralPolicyProvince.SubjectCentralPolicyProvinces.Any(x => x.Type == "NoMaster" && x.Status == "ใช้งานจริง"))
                .Where(x => x.Id == id).First();

            var centralprovinceid = _context.CentralPolicyProvinces
                // .Where(x => x.Id == ebook.CentralPolicyProvinceId)
                .First();

            var exe = "";
            //var exe = _context.ExecutiveOrders
            //    .Where(x => x.CentralPolicyId == centralprovinceid.CentralPolicyId && x.ProvinceId == centralprovinceid.ProvinceId).ToList();

            var user = _context.CentralPolicyUsers
                .Include(x => x.User)
                .Where(x => x.CentralPolicyId == centralprovinceid.CentralPolicyId && x.ProvinceId == centralprovinceid.ProvinceId).ToList();

            return Ok(new { ebook, exe, user });
        }

        // POST api/values
        [HttpPost("accept")]
        public ElectronicBookAccept Post(long bookgroupid, string userid)
        {
            var ElectronicBookAcceptdata = new ElectronicBookAccept
            {
                UserId = userid,
                // ElectronicBookGroupId = bookgroupid
            };

            _context.ElectronicBookAccepts.Add(ElectronicBookAcceptdata);
            _context.SaveChanges();

            return ElectronicBookAcceptdata;
        }

        [HttpPost("addProceed")]
        public void PostProceed(string userid, string proceed, long ElectID)
        {
            var ElectronicBookProceeddata = new ElectronicBookProceed
            {
                ElectronicBookId = ElectID,
                Proceed = proceed,
                UserId = userid
            };

            _context.ElectronicBookProceeds.Add(ElectronicBookProceeddata);
            _context.SaveChanges();
        }

        [HttpGet("proceed/{id}")]
        public IActionResult GetProceed(long id)
        {
            var data = _context.ElectronicBookProceeds
                .Include(m => m.User)
                .Where(m => m.ElectronicBookId == id).ToList();

            return Ok(data);
        }

        [HttpGet("centralPolicyEbook/{userId}")]
        public IActionResult GetCentralPolicyEbook(ElectronicBookViewModel model, string userId)
        {
            System.Console.WriteLine("UserID: " + userId);
            var centralPolicyEbookData = _context.CentralPolicyEvents
            .Include(x => x.CentralPolicy)
            .ThenInclude(x => x.CentralPolicyProvinces)
            .ThenInclude(x => x.Province)
            .Include(x => x.CentralPolicy)
            .ThenInclude(x => x.CentralPolicyProvinces)
            .ThenInclude(x => x.SubjectCentralPolicyProvinces)
            .Include(x => x.InspectionPlanEvent)
            .ThenInclude(x => x.Province)
            .Where(x => x.InspectionPlanEvent.Status == "ใช้งานจริง" && x.InspectionPlanEvent.CreatedBy == userId)
            .ToList();

            // var centralPolicyEbookData = _context.CentralPolicies
            // .ToList();

            return Ok(centralPolicyEbookData);
        }

        [HttpPost("centralPolicyEbook2")]
        public IActionResult GetCentralPolicyEbook2(ElectronicBookViewModel model)
        {
            System.Console.WriteLine("StartDate: " + model.startDate);

            var centralPolicyEbookData = _context.CentralPolicyEvents
            .Include(x => x.CentralPolicy)
            .ThenInclude(x => x.CentralPolicyProvinces)
            .ThenInclude(x => x.Province)
            .Include(x => x.CentralPolicy)
            .ThenInclude(x => x.CentralPolicyProvinces)
            .ThenInclude(x => x.SubjectCentralPolicyProvinces)
            .Include(x => x.InspectionPlanEvent)
            .ThenInclude(x => x.Province)
            .Where(x => x.InspectionPlanEvent.CreatedBy == model.User && x.InspectionPlanEvent.Status == "ใช้งานจริง" && (x.StartDate <= model.startDate && x.EndDate >= model.startDate))
            .ToList();

            return Ok(centralPolicyEbookData);
        }

        [HttpPost("CreateElectronicBook")]
        public IActionResult CreateElectronicBook([FromForm] ElectronicBookViewModel model)
        {
            var test1 = model.Detail;
            //var test2 = model.UserId;
            long ebookId;
            System.Console.WriteLine("Detail: " + test1);
            //System.Console.WriteLine("UserId: " + test2);
            var ElectronicBookdata = new ElectronicBook
            {
                Detail = model.Detail,
                Problem = model.Problem,
                Suggestion = model.Suggestion,
                CreatedBy = model.id,
                Status = model.Status,
                StartDate = model.startDate,
                EndDate = model.endDate,
            };
            System.Console.WriteLine("1");

            _context.ElectronicBooks.Add(ElectronicBookdata);
            _context.SaveChanges();

            ebookId = ElectronicBookdata.Id;

            foreach (var item in model.CentralPolicyEventId)
            {
                var ElectronicBookgroupdata = new ElectronicBookGroup
                {
                    ElectronicBookId = ElectronicBookdata.Id,
                    CentralPolicyEventId = item
                };
                _context.ElectronicBookGroups.Add(ElectronicBookgroupdata);
                _context.SaveChanges();
            }

            // System.Console.WriteLine("Start Upload");

            // if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            // {
            //     Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            // }

            // //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // // path ที่เก็บไฟล์
            // var filePath = _environment.WebRootPath + "//Uploads//";
            // long ebookFileId = 0;

            // if (model.files != null)
            // {
            //     System.Console.WriteLine("Start Upload 2");
            //     foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
            //     //foreach (var formFile in data.files)
            //     {
            //         System.Console.WriteLine("Start Upload 3");
            //         var random = RandomString(10);
            //         string filePath2 = formFile.Value.FileName;
            //         string filename = Path.GetFileName(filePath2);
            //         string ext = Path.GetExtension(filename);

            //         if (formFile.Value.Length > 0)
            //         {
            //             System.Console.WriteLine("Start Upload 4");
            //             // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
            //             using (var stream = System.IO.File.Create(filePath + random + filename))
            //             {
            //                 await formFile.Value.CopyToAsync(stream);
            //             }
            //             System.Console.WriteLine("Start Upload 4.1");
            //             var ElectronicBookFile = new ElectronicBookFile
            //             {
            //                 ElectronicBookId = ElectronicBookdata.Id,
            //                 Name = random + filename,
            //                 // Description = model.fileDescription,
            //                 // Type = model.Type
            //             };
            //             System.Console.WriteLine("Start Upload 4.2");
            //             _context.ElectronicBookFiles.Add(ElectronicBookFile);
            //             _context.SaveChanges();
            //             ebookFileId = ElectronicBookFile.Id;
            //             System.Console.WriteLine("Start Upload 4.3");
            //         }
            //         System.Console.WriteLine("Start Upload 5");
            //     }
            //     _context.SaveChanges();
            // }
            // _context.SaveChanges();
            return Ok(new { eBookID = ebookId });
        }

        [HttpGet("getDetailEbook/{eBookId}")]
        public IActionResult getDetailEbook(long eBookId)
        {
            var centralPolicyEbookData = _context.ElectronicBookGroups
            .Where(x => x.ElectronicBookId == eBookId)
            .Include(x => x.CentralPolicyEvent)
            .ThenInclude(x => x.InspectionPlanEvent)
            .ThenInclude(x => x.Province)
            .ToList();

            // var centralPolicyEbookData = _context.CentralPolicies
            // .ToList();
            return Ok(centralPolicyEbookData);
        }

        [HttpPost("getInvitedPeople")]
        public IActionResult GetInvitedPeople(ElectronicBookViewModel model)
        {
            List<object> termsList = new List<object>();
            foreach (var id in model.inspectionPlanEventId)
            {
                var invitedPeople = _context.CentralPolicyUsers
                .Include(x => x.User)
                .ThenInclude(x => x.Departments)
                .Include(x => x.User)
                .ThenInclude(x => x.ProvincialDepartments)
                .Include(x => x.User)
                .ThenInclude(x => x.Ministries)
                .Include(x => x.CentralPolicy)
                .Include(x => x.InspectionPlanEvent)
                .Where(x => x.InspectionPlanEventId == id)
                .ToList();

                foreach (var item in invitedPeople)
                {
                    termsList.Add(item);
                }
            }
            return Ok(termsList);
        }

        [HttpPut("addEbookInvite")]
        public IActionResult PutEbookInvite(ElectronicBookViewModel model)
        {
            var ebookInvite = _context.ElectronicBookInvites
            .Where(x => x.Id == model.inviteId)
            .FirstOrDefault();

            {
                ebookInvite.Description = model.Description;
                ebookInvite.Status = "ลงความเห็นแล้ว";
                ebookInvite.Approve = model.approve;
            }
            _context.Entry(ebookInvite).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { Status = true });
        }

        [HttpPost("getElectronicBookInviteOpinion")]
        public IActionResult GetElectronicBookInviteOpinion(ElectronicBookViewModel model)
        {
            var ebookInvite = _context.ElectronicBookInvites
            .Where(x => x.ElectronicBookId == model.ElectID && x.UserId == model.userInvite)
            .FirstOrDefault();

            return Ok(ebookInvite);
        }

        [HttpPost("sendElectronicBookToProvince")]
        public IActionResult PostElectronicBookToProvince(ElectronicBookViewModel model)
        {
            foreach (var provinceId in model.electProvinceId)
            {
                System.Console.WriteLine("ProvinceId: " + provinceId);
                var ElectronicBook = new ElectronicBookAccept
                {
                    ElectronicBookId = model.ElectID,
                    ProvinceId = provinceId,
                    Status = "รอลงนามเอกสาร",
                    CreateBy = model.userCreate,
                    CreatedAt = DateTime.Now
                };

                _context.ElectronicBookAccepts.Add(ElectronicBook);
                _context.SaveChanges();
            }

            foreach (var provincialId in model.provincialDepartmentIdAr)
            {
                System.Console.WriteLine("ProvinceId: " + provincialId);
                var ElectronicBookDepartment = new ElectronicBookProvincialDepartment
                {
                    ElectronicBookId = model.ElectID,
                    ProvincialDepartmentId = provincialId,
                    Status = "รอดำเนินการ",
                    CreateDate = DateTime.Now,
                    CreateBy = model.userCreate,
                };

                _context.ElectronicBookProvincialDepartments.Add(ElectronicBookDepartment);
                _context.SaveChanges();
            }

            //var ElectronicBookStatus = _context.ElectronicBooks
            //    .Where(x => x.Id == model.ElectID)
            //    .FirstOrDefault();
            //{
            //    //ElectronicBookStatus.ProvinceStatus = "ส่งสมุดตรวจแล้ว";
            //    ElectronicBookStatus.Status = "ส่งสมุดตรวจแล้ว";
            //}
            //_context.Entry(ElectronicBookStatus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //_context.SaveChanges();

            return Ok(new { status = true });
        }

        [HttpPost("sendElectronicBookToOtherProvince")]
        public IActionResult PostElectronicBookToOtherProvince(ElectronicBookViewModel model)
        {
            var ElectronicBook = new ElectronicBookOtherAccept
            {
                ElectronicBookProvincialDepartmentId = model.electAcceptId,
                ProvincialDepartmentId = model.provincialDepartmentId,
                CreateBy = model.userCreate,
                CreatedAt = DateTime.Now,
                Description = model.Description,
                Status = "ยังไม่ดำเนินการ"
            };

            _context.ElectronicBookOtherAccepts.Add(ElectronicBook);
            _context.SaveChanges();
            return Ok(new { status = true });
        }

        [HttpGet("electronicbookprovince/{provinceId}/{provincialDepartmentId}")]
        public IActionResult GetElectronicbookProvince(long provinceId, long provincialDepartmentId)
        {
            System.Console.WriteLine("PDID: " + provincialDepartmentId);
            var ebookProvince = _context.ElectronicBookAccepts
            .Include(x => x.ElectronicBook)
            .ThenInclude(x => x.ElectronicBookGroups)
            .ThenInclude(x => x.CentralPolicyEvent)
            .ThenInclude(x => x.CentralPolicy)
            .ThenInclude(x => x.CentralPolicyProvinces)
            .ThenInclude(x => x.SubjectCentralPolicyProvinces)

            .Include(x => x.User)
            .Include(x => x.UserCreate)
            .Where(x => x.ProvinceId == provinceId)
            .Where(x => x.ElectronicBook.Status == "ใช้งานจริง" || x.ElectronicBook.Status == "ส่งสมุดตรวจแล้ว")
            .OrderByDescending(x => x.Id)
            // .Where(m => m.ElectronicBook.ElectronicBookGroups
            // .Any(x => x.CentralPolicyEvent.CentralPolicy.CentralPolicyProvinces
            // .Any(m => m.SubjectCentralPolicyProvinces
            // .Any(m => m.SubquestionCentralPolicyProvinces
            // .Any(m => m.SubjectCentralPolicyProvinceGroups
            // .Any(m => m.ProvincialDepartmentId == provincialDepartmentId))))))
            .ToList();


            return Ok(ebookProvince);
        }

        [HttpGet("electronicbookotherprovince/{provincialDepartmentId}")]
        public IActionResult GetElectronicbookOtherProvince(long provincialDepartmentId)
        {
            var ebookOtherProvince = _context.ElectronicBookOtherAccepts
            .Include(x => x.ElectronicBookProvincialDepartment)
            .ThenInclude(x => x.ElectronicBook)
            .Include(x => x.User)
            .Include(x => x.UserCreate)
            .ThenInclude(x => x.Departments)
            .Include(x => x.UserCreate)
            .ThenInclude(x => x.ProvincialDepartments)
            .Where(x => x.ProvincialDepartmentId == provincialDepartmentId)
            .ToList();
            return Ok(ebookOtherProvince);
        }

        // POST: api/ElectronicBook
        [HttpPost("subjecteventfile")]
        public async Task<IActionResult> Post3([FromForm] CalendarFileViewModel model)
        {
            var CentralPolicyProvincedata = _context.CentralPolicyProvinces
                .Where(m => m.Id == model.CentralPolicyProvinceId).FirstOrDefault();

            var SubjectGroupsdata = _context.SubjectGroups.Find(model.ElectronicBookId);
            ////CentralPolicyProvincedata.Step = model.Step;
            SubjectGroupsdata.Status = model.Status;
            SubjectGroupsdata.Suggestion = model.Suggestion;

            SubjectGroupsdata.SubjectNotificationDate = model.NotificationSubjectDate;
            SubjectGroupsdata.SubjectDeadlineDate = model.DeadlineSubjectDate;
            SubjectGroupsdata.PeopleQuestionNotificationDate = model.NotificationPeopleQuestiontDate;
            SubjectGroupsdata.PeopleQuestionDeadlineDate = model.DeadlinePeopleQuestiontDate;

            //CentralPolicyProvincedata.QuestionPeople = model.QuestionPeople;
            _context.Entry(CentralPolicyProvincedata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";

            System.Console.WriteLine("Start Upload 2");

            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {

                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("Start Upload 4");
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + ext))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        System.Console.WriteLine("Start Upload 4.1");
                        var ElectronicBookFile = new SubjectEventFile
                        {
                            //CentralPolicyId = CentralPolicyProvincedata.CentralPolicyId,
                            SubjectGroupId = model.ElectronicBookId,
                            Name = random + ext,
                            Type = "Subject Event File",
                            Description = Path.GetFileNameWithoutExtension(filePath2),
                        };

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.SubjectEventFiles.Add(ElectronicBookFile);
                        _context.SaveChanges();

                        System.Console.WriteLine("Start Upload 4.3");
                    }

                    System.Console.WriteLine("Start Upload 5");
                }
            }

            return Ok(new { status = true });
        }

        [HttpGet("getSubjectEventFile/{planId}/{cenproid}")]
        public IActionResult getSubjectEventFile(long planId, long cenproid)
        {
            //System.Console.WriteLine("ELECT ID: " + electID);
            //var accept = _context.CentralPolicyUsers.Where(m => m.Id == centralPolicyUserId).FirstOrDefault();
            var cenpro = _context.CentralPolicyProvinces
                .Where(m => m.Id == cenproid).FirstOrDefault();

            var carlendarFile = _context.SubjectEventFiles
                .Where(x => x.SubjectGroupId == planId && x.Type == "Subject Event File" || x.Type == "Subject Event Image File")
                .ToList();

            var signatureFile = _context.SubjectEventFiles
               .Where(x => x.SubjectGroupId == planId && x.Type == "Subject Event Signature File")
               .ToList();

            return Ok(new { carlendarFile, signatureFile });
        }

        [HttpPost("addProvinceSignature")]
        public async Task<IActionResult> AddProvinceSignature([FromForm] ElectronicBookViewModel model)
        {
            var ElectronicBookAccept = _context.ElectronicBookAccepts
               .Where(x => x.ElectronicBookId == model.ElectID && x.ProvinceId == model.ProvinceId)
               .FirstOrDefault();
            {
                ElectronicBookAccept.UserId = model.userCreate;
                ElectronicBookAccept.Description = model.Description;
                ElectronicBookAccept.Status = "ลงนามเอกสารแล้ว";
            }
            _context.Entry(ElectronicBookAccept).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            var Ebook = _context.ElectronicBooks
              .Where(x => x.Id == model.ElectID)
              .FirstOrDefault();
            {
                Ebook.ProvinceStatus = "หัวหน้าส่วนจังหวัดแนบลายมือชื่อแล้ว";
            }
            _context.Entry(Ebook).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";

            System.Console.WriteLine("Start Upload 2");

            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {

                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("Start Upload 4");
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        System.Console.WriteLine("Start Upload 4.1");
                        var ElectronicBookProvinceSignature = new ElectronicBookProvinceApproveFile
                        {
                            //CentralPolicyId = CentralPolicyProvincedata.CentralPolicyId,
                            ElectronicBookAcceptId = ElectronicBookAccept.Id,
                            Name = random + filename,
                        };

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.ElectronicBookProvinceApproveFiles.Add(ElectronicBookProvinceSignature);
                        _context.SaveChanges();

                        System.Console.WriteLine("Start Upload 4.3");
                    }

                    System.Console.WriteLine("Start Upload 5");
                }
            }

            return Ok(new { status = true });
        }

        [HttpGet("getProvincialDepartment")]
        public IActionResult GetProvincialDepartment()
        {
            var provincialDepartmentData = _context.ProvincialDepartment
            .ToList();
            var provinceData = _context.Provinces
            .ToList();
            return Ok(new { provincialDepartmentData, provinceData });
        }

        [HttpPut("agreeOtherDepartment")]
        public void PutAgreeOtherDepartment([FromForm] ElectronicBookViewModel model)
        {
            var electronicBookAcceptData = _context.ElectronicBookOtherAccepts
                .Where(x => x.Id == model.electAcceptId)
                .FirstOrDefault();

            {
                electronicBookAcceptData.AcceptDate = DateTime.Now;
                electronicBookAcceptData.OtherDescription = model.Description;
                electronicBookAcceptData.UserId = model.userCreate;
                electronicBookAcceptData.Status = "ดำเนินการแล้ว";
            }
            _context.Entry(electronicBookAcceptData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            System.Console.WriteLine("Finish Update Other Department");
        }

        [HttpPost("imageDescription/{electID}")]
        public async Task<IActionResult> imageDescription(inputfile model, long electID)
        {
            System.Console.WriteLine("ID: " + model.fileDescription2);
            var ebookData = _context.ElectronicBookFiles
            .Where(x => x.ElectronicBookId == electID)
            .FirstOrDefault();

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";

            foreach (var formFile in model.files2.Select((value, index) => new { Value = value, Index = index }))
            //foreach (var formFile in data.files)
            {

                System.Console.WriteLine("Start Upload 3");
                var random = RandomString(10);
                string filePath2 = formFile.Value.FileName;
                string filename = Path.GetFileName(filePath2);
                string ext = Path.GetExtension(filename);

                if (formFile.Value.Length > 0)
                {
                    System.Console.WriteLine("Start Upload 4");
                    // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                    using (var stream = System.IO.File.Create(filePath + random + filename))
                    {
                        await formFile.Value.CopyToAsync(stream);
                    }

                    System.Console.WriteLine("Start Upload 4.1");
                    var ElectronicBookProvinceSignature = new ElectronicBookFile
                    {
                        //CentralPolicyId = CentralPolicyProvincedata.CentralPolicyId,
                        ElectronicBookId = electID,
                        Name = random + filename,
                        Description = model.fileDescription2
                    };

                    System.Console.WriteLine("Start Upload 4.2");
                    _context.ElectronicBookFiles.Add(ElectronicBookProvinceSignature);
                    _context.SaveChanges();

                    System.Console.WriteLine("Start Upload 4.3");
                }

                System.Console.WriteLine("Start Upload 5");
            }
            return Ok(new { Status = true });
        }

        [HttpPost("CreateElectronicBook2")]
        public async Task<IActionResult> CreateElectronicBook2([FromForm] ElectronicBookViewModel model)
        {
            var test1 = model.Detail;
            //var test2 = model.UserId;
            long ebookId;
            System.Console.WriteLine("Detail: " + test1);
            //System.Console.WriteLine("UserId: " + test2);
            var ElectronicBookdata = new ElectronicBook
            {
                Detail = model.Detail,
                Problem = model.Problem,
                Suggestion = model.Suggestion,
                CreatedBy = model.id,
                Status = model.Status,
                StartDate = model.startDate,
                EndDate = model.endDate,
                ProvinceType = model.sendToProvince,
            };
            System.Console.WriteLine("1");

            _context.ElectronicBooks.Add(ElectronicBookdata);
            _context.SaveChanges();

            ebookId = ElectronicBookdata.Id;

            foreach (var item in model.CentralPolicyEventId)
            {
                var ElectronicBookgroupdata = new ElectronicBookGroup
                {
                    ElectronicBookId = ElectronicBookdata.Id,
                    CentralPolicyEventId = item
                };
                _context.ElectronicBookGroups.Add(ElectronicBookgroupdata);
                _context.SaveChanges();
            }

            System.Console.WriteLine("Start Upload");

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";
            long ebookFileId = 0;

            if (model.files != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {
                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);
                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("Start Upload 4");
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + ext))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }
                        System.Console.WriteLine("Start Upload 4.1");
                        System.Console.WriteLine("ContentType : " + formFile.Value.ContentType.ToString());
                        var ElectronicBookFile = new ElectronicBookFile
                        {
                            ElectronicBookId = ElectronicBookdata.Id,
                            Name = random + ext,
                            Description = Path.GetFileNameWithoutExtension(filePath2),
                            Type = formFile.Value.ContentType.ToString(),
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.ElectronicBookFiles.Add(ElectronicBookFile);
                        _context.SaveChanges();
                        ebookFileId = ElectronicBookFile.Id;
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
                _context.SaveChanges();
            }
            _context.SaveChanges();

            var logdata = new Log
            {
                UserId = model.id,
                DatabaseName = "ElectronicBook",
                EventType = "เพิ่ม",
                EventDate = DateTime.Now,
                Detail = "เพิ่มสมุดตรวจอิเล็กทรอนิกส์",
                Allid = ElectronicBookdata.Id,
            };

            _context.Logs.Add(logdata);
            _context.SaveChanges();
            return Ok(new { eBookID = ebookId });
        }

        [HttpPost("CreateElectronicBookOwn")]
        public async Task<IActionResult> CreateElectronicBookOwn([FromForm] ElectronicBookViewModel model)
        {
            System.Console.WriteLine("IN 0");
            long ebookId;
            var ElectronicBookdata = new ElectronicBook
            {
                Detail = model.Detail,
                Problem = model.Problem,
                Suggestion = model.Suggestion,
                CreatedBy = model.id,
                Status = model.Status,
                StartDate = model.startDate,
                EndDate = model.endDate,
                ProvinceType = model.sendToProvince,
                CentralPolicy = model.centralPolicyEventTitle,
                // ProvincialDepartmentId = model.provincialDepartmentId,
            };

            _context.ElectronicBooks.Add(ElectronicBookdata);
            _context.SaveChanges();

            ebookId = ElectronicBookdata.Id;
            System.Console.WriteLine("1");

            foreach (var provinceId in model.electProvinceId)
            {
                System.Console.WriteLine("ProvinceId: " + provinceId);
                var ElectronicBook = new ElectronicBookAccept
                {
                    ElectronicBookId = ebookId,
                    ProvinceId = provinceId,
                    Status = "รอลงนามเอกสาร",
                    CreateBy = model.userCreate,
                    CreatedAt = DateTime.Now
                };

                _context.ElectronicBookAccepts.Add(ElectronicBook);
                _context.SaveChanges();
                System.Console.WriteLine("2");

            }

            foreach (var provincialId in model.provincialDepartmentIdAr)
            {
                System.Console.WriteLine("ProvinceId: " + provincialId);
                var ElectronicBookDepartment = new ElectronicBookProvincialDepartment
                {
                    ElectronicBookId = ebookId,
                    ProvincialDepartmentId = provincialId,
                    Status = "รอดำเนินการ",
                    CreateDate = DateTime.Now,
                    CreateBy = model.userCreate,
                };

                _context.ElectronicBookProvincialDepartments.Add(ElectronicBookDepartment);
                _context.SaveChanges();
                System.Console.WriteLine("3");

            }

            System.Console.WriteLine("Start Upload");

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";
            long ebookFileId = 0;

            if (model.files != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {
                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("Start Upload 4");
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + ext))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }
                        System.Console.WriteLine("Start Upload 4.1");
                        var ElectronicBookFile = new ElectronicBookFile
                        {
                            ElectronicBookId = ElectronicBookdata.Id,
                            Name = random + ext,
                            Description = Path.GetFileNameWithoutExtension(filePath2),
                            Type = formFile.Value.ContentType.ToString(),
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.ElectronicBookFiles.Add(ElectronicBookFile);
                        _context.SaveChanges();
                        ebookFileId = ElectronicBookFile.Id;
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
                _context.SaveChanges();
            }
            _context.SaveChanges();
            return Ok(new { eBookID = ebookId });
        }

        [HttpGet("electronicbookdepartment/{provincialDepartmentId}")]
        public IActionResult GetElectronicbookDepartment(long provincialDepartmentId)
        {
            System.Console.WriteLine("PDID: " + provincialDepartmentId);
            var ebookProvince = _context.ElectronicBookProvincialDepartments
            .Include(x => x.ElectronicBook)
            .ThenInclude(x => x.ElectronicBookGroups)
            .ThenInclude(x => x.CentralPolicyEvent)
            .ThenInclude(x => x.CentralPolicy)
            .ThenInclude(x => x.CentralPolicyProvinces)
            .ThenInclude(x => x.SubjectCentralPolicyProvinces)

            .Include(x => x.UserCreate)
            .Include(x => x.UserProvincialDepartment)
            .Where(x => x.ProvincialDepartmentId == provincialDepartmentId)
            .Where(x => x.ElectronicBook.Status == "ใช้งานจริง" || x.ElectronicBook.Status == "ส่งสมุดตรวจแล้ว")
            .OrderByDescending(x => x.Id)
            .ToList();

            var provinceData = _context.ElectronicBookProvincialDepartments
            .Include(x => x.ElectronicBook)
            .ThenInclude(x => x.ElectronicBookGroups)
            .ThenInclude(x => x.CentralPolicyEvent)
            .ThenInclude(x => x.CentralPolicy)
            .ThenInclude(x => x.CentralPolicyProvinces)
            .ThenInclude(x => x.SubjectCentralPolicyProvinces)
            .Where(x => x.ProvincialDepartmentId == provincialDepartmentId)
            .Where(x => x.ElectronicBook.Status == "ใช้งานจริง" || x.ElectronicBook.Status == "ส่งสมุดตรวจแล้ว")
            .OrderByDescending(x => x.Id)
            .Select(x => new
            {
                provinceId = x.ElectronicBook.ElectronicBookGroups.Select(m => m.CentralPolicyEvent.InspectionPlanEvent.ProvinceId)
            })
             .ToList();
            return Ok(new { ebookProvince, provinceData });
        }

        [HttpPost("addDepartmentSignature")]
        public async Task<IActionResult> AddDepartmentSignature([FromForm] ElectronicBookViewModel model)
        {
            var ElectronicBookProvincialDepartment = _context.ElectronicBookProvincialDepartments
               .Where(x => x.ElectronicBookId == model.ElectID && x.Id == model.electProvincialId)
               .FirstOrDefault();
            {
                ElectronicBookProvincialDepartment.UserId = model.userCreate;
                ElectronicBookProvincialDepartment.Description = model.Description;
                ElectronicBookProvincialDepartment.Status = "ดำเนินการแล้ว";
            }
            _context.Entry(ElectronicBookProvincialDepartment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            // var Ebook = _context.ElectronicBooks
            //   .Where(x => x.Id == model.ElectID)
            //   .FirstOrDefault();
            // {
            //     Ebook.ProvinceStatus = "หน่วยรับตรวจดำเนินการแล้ว";
            // }
            // _context.Entry(Ebook).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            // _context.SaveChanges();

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";

            System.Console.WriteLine("Start Upload 2");

            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {

                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("Start Upload 4");
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        System.Console.WriteLine("Start Upload 4.1");
                        var ElectronicBookProvinceSignature = new ElectronicBookProvinceApproveFile
                        {
                            //CentralPolicyId = CentralPolicyProvincedata.CentralPolicyId,
                            ElectronicBookAcceptId = ElectronicBookProvincialDepartment.Id,
                            Name = random + filename,
                        };

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.ElectronicBookProvinceApproveFiles.Add(ElectronicBookProvinceSignature);
                        _context.SaveChanges();

                        System.Console.WriteLine("Start Upload 4.3");
                    }

                    System.Console.WriteLine("Start Upload 5");
                }
            }

            return Ok(new { status = true });
        }

        [HttpPost("GetElectronicBookDepartmentById")]
        public IActionResult GetElectronicBookDepartmentById(ElectronicBookViewModel model)
        {
            var ebookInvite = _context.ElectronicBookProvincialDepartments
                .Include(x => x.ElectronicBookOtherAccepts)
                .ThenInclude(x => x.ProvincialDepartment)

                .Include(x => x.ElectronicBookOtherAccepts)
                .ThenInclude(x => x.User)
            .Where(x => x.ElectronicBookId == model.ElectID && x.ProvincialDepartmentId == model.provincialDepartmentId)
            .FirstOrDefault();

            return Ok(ebookInvite);
        }

        [HttpPost("inviteMorePeople")]
        public void InviteMorePeople([FromForm] ElectronicBookViewModel model)
        {
            foreach (var item in model.userId)
            {
                var ElectronicBookInviteData = new ElectronicBookInvite
                {
                    ElectronicBookId = model.ElectID,
                    UserId = item,
                    Status = "รอลงความเห็น"
                };
                _context.ElectronicBookInvites.Add(ElectronicBookInviteData);
                _context.SaveChanges();
            }
            System.Console.WriteLine("Finish Invite More People");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("deleteMoreInvited/{id}")]
        public void DeleteMoreInvited(long id)
        {
            System.Console.WriteLine("Invited ID: " + id);
            var invitedData = _context.ElectronicBookInvites.Find(id);

            _context.ElectronicBookInvites.Remove(invitedData);
            _context.SaveChanges();
        }

        [HttpGet("getElectronicBookDepartmentById/{electID}")]
        public IActionResult GetEBookDepartmentById(long electID)
        {
            var electronicBookProvincialDepartmentData = _context.ElectronicBookProvincialDepartments
                .Include(x => x.ElectronicBookOtherAccepts)
                .ThenInclude(x => x.ProvincialDepartment)

                .Include(x => x.ElectronicBookOtherAccepts)
                .ThenInclude(x => x.User)
                .Where(x => x.Id == electID)
                .FirstOrDefault();
            return Ok(electronicBookProvincialDepartmentData);
        }

        [HttpGet("getElectronicBookOtherById/{electID}")]
        public IActionResult GetEBookOtherById(long electID)
        {
            var electronicBookOtherData = _context.ElectronicBookOtherAccepts
                .Include(x => x.ElectronicBookProvincialDepartment)
                .Include(x => x.UserCreate)
                .Include(x => x.ProvincialDepartment)
                .Include(x => x.User)
                .Where(x => x.Id == electID)
                .FirstOrDefault();
            return Ok(electronicBookOtherData);
        }
    }
}
