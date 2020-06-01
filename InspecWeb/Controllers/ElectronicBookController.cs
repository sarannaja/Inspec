using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Client;
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

            var ebook = _context.ElectronicBookGroups
                .Include(x => x.CentralPolicyProvince)
                .ThenInclude(x => x.Province)
                .Include(x => x.CentralPolicyProvince)
                .ThenInclude(x => x.CentralPolicy)
                .ThenInclude(x => x.CentralPolicyUser)
                .Include(x => x.ElectronicBook)
                .Where(x => x.ElectronicBook.CreatedBy == userId)
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

        [HttpGet("getElectronicBookById/{electID}")]
        public IActionResult GetById(long electID)
        {

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

            var electData = _context.ElectronicBooks
                .Include(x => x.ElectronicBookFiles)
                .Where(x => x.Id == electID)
                .FirstOrDefault();

            var report = _context.CentralPolicyUsers
                .Include(x => x.User)
                .Include(x => x.CentralPolicyGroup)
                .ThenInclude(x => x.CentralPolicyUserFiles)
                .Where(x => x.ElectronicBookId == electID)
                .ToList();

            return Ok(new { electData, report });
        }

        [HttpGet("getCalendarFile/{electID}")]
        public IActionResult GetCalendarFile(long electID)
        {
            System.Console.WriteLine("ELECT ID: " + electID);
            //var accept = _context.CentralPolicyUsers.Where(m => m.Id == centralPolicyUserId).FirstOrDefault();

            var carlendarFile = _context.ElectronicBookFiles
                .Where(x => x.ElectronicBookId == electID && x.Type == "Calendar File")
                .ToList();

            return Ok(carlendarFile);
        }

        [HttpGet("getElectronicbookFile/{electID}")]
        public IActionResult getElectronicbookFile(long electID)
        {
            System.Console.WriteLine("ELECT ID: " + electID);
            //var accept = _context.CentralPolicyUsers.Where(m => m.Id == centralPolicyUserId).FirstOrDefault();

            var electronicFile = _context.ElectronicBookFiles
                .Where(x => x.ElectronicBookId == electID && x.Type == "ElectronicBook File")
                .ToList();

            return Ok(electronicFile);
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
                .Select(x => x.CentralPolicyProvinceId)
                .First();

            System.Console.WriteLine("2: ");
            var centralPolicyId = _context.CentralPolicyProvinces
                .Where(x => x.Id == provinceId)
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
                            Type = "ElectronicBook File"
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
                    if (centralpolicyprovincedata.Id == electronicbookgroups[i].CentralPolicyProvinceId)
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
                CentralPolicyProvinceId = centralPolicyID
            };
            _context.ElectronicBookGroups.Add(ElectronicBookgroupdata);
            _context.SaveChanges();

            //System.Console.WriteLine("3.8");

            //foreach (var itemUserPeopleId in model.UserPeopleId)
            //{
            //    var CentralPolicyGroupdata = new CentralPolicyGroup
            //    {
            //    };
            //    _context.CentralPolicyGroups.Add(CentralPolicyGroupdata);
            //    _context.SaveChanges();

            //    System.Console.WriteLine("3.9");
            //    System.Console.WriteLine("USERPeople: " + itemUserPeopleId);

            //    var CentralPolicyUserdata = new CentralPolicyUser
            //        {
            //            CentralPolicyId = CentralPolicyId,
            //            ProvinceId = ProvinceId,
            //            ElectronicBookId = ElectronicBookdata.Id,
            //            CentralPolicyGroupId = CentralPolicyGroupdata.Id,
            //            UserId = itemUserPeopleId,
            //            Status = "รอการตอบรับ",
            //            DraftStatus = model.Status

            //    };
            //        _context.CentralPolicyUsers.Add(CentralPolicyUserdata);
            //        _context.SaveChanges();
            //}
            //System.Console.WriteLine("4");

            //foreach (var itemUserMinistryId in model.UserMinistryId)
            //{
            //    var CentralPolicyGroupdata2 = new CentralPolicyGroup
            //    {
            //    };
            //    _context.CentralPolicyGroups.Add(CentralPolicyGroupdata2);
            //    _context.SaveChanges();
            //    System.Console.WriteLine("5");
            //    var CentralPolicyUserdata2 = new CentralPolicyUser
            //    {
            //        CentralPolicyId = CentralPolicyId,
            //        ProvinceId = ProvinceId,
            //        ElectronicBookId = ElectronicBookdata.Id,
            //        CentralPolicyGroupId = CentralPolicyGroupdata2.Id,
            //        UserId = itemUserMinistryId,
            //        Status = "รอการตอบรับ",
            //        DraftStatus = model.Status
            //    };
            //    _context.CentralPolicyUsers.Add(CentralPolicyUserdata2);
            //    _context.SaveChanges();
            //    System.Console.WriteLine("6");
            //}

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

            //var CentralPolicyProvincedata = _context.CentralPolicyProvinces
            //    .Where(m => m.Id == model.CentralPolicyProvinceId).FirstOrDefault();

            var CentralPolicyProvincedata = _context.CentralPolicyProvinces.Find(model.CentralPolicyProvinceId);
            CentralPolicyProvincedata.Step = model.Step;
            CentralPolicyProvincedata.Status = model.Status;
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
                        ElectronicBookId = model.ElectronicBookId,
                        Name = random + filename,
                        Type = "Calendar File"
                    };

                    System.Console.WriteLine("Start Upload 4.2");
                    _context.ElectronicBookFiles.Add(ElectronicBookFile);
                    _context.SaveChanges();

                    System.Console.WriteLine("Start Upload 4.3");
                }

                System.Console.WriteLine("Start Upload 5");
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

            var ebook = _context.ElectronicBookGroups
                            .Include(x => x.CentralPolicyProvince)
                            .ThenInclude(x => x.Province)
                            .Include(x => x.CentralPolicyProvince)
                            .ThenInclude(x => x.CentralPolicy)
                            .ThenInclude(x => x.CentralPolicyUser)
                            .Include(x => x.ElectronicBook)
                            .Where(x => x.ElectronicBook.Status == "ใช้งานจริง")
                            .Where(x => x.CentralPolicyProvince.Province.Id == provinceuser.ProvinceId)
                            .ToList();

            return Ok(ebook);
        }

        [HttpPost("addSuggestion")]
        public void PostSuggestion([FromForm] ElectronicBookViewModel model)
        {
            var ElectSuggestionData = new ElectronicBookSuggestGroup
            {
                ElectronicBookId = model.ElectID,
                Detail = model.Detail,
                Problem = model.Problem,
                Suggestion = model.Suggestion,
                SubjectCentralPolicyProvinceId = model.SubjectCentralPolicyProvinceId
            };
            System.Console.WriteLine("1");

            _context.ElectronicBookSuggestGroups.Add(ElectSuggestionData);
            _context.SaveChanges();

            System.Console.WriteLine("Finish Add Suggestion");
        }

        [HttpPut("editSuggestion")]
        public void PutSuggestion([FromForm] ElectronicBookViewModel model)
        {
            //System.Console.WriteLine("Edit ja");
            //var ElectSuggestionData = _context.ElectronicBookSuggestGroups
            //    .Where(x => x.SubjectCentralPolicyProvinceId == model.SubjectCentralPolicyProvinceId
            //    && x.ElectronicBookId == model.ElectID)
            //    .FirstOrDefault();

            //{
            //    ElectSuggestionData.Detail = model.Detail;
            //    ElectSuggestionData.Problem = model.Problem;
            //    ElectSuggestionData.Suggestion = model.Suggestion;
            //}
            //_context.Entry(ElectSuggestionData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //_context.SaveChanges();

            System.Console.WriteLine("Detail: " + model.Detail);
            System.Console.WriteLine("Problem: " + model.Problem);
            System.Console.WriteLine("Suggestion: " + model.Suggestion);
            System.Console.WriteLine("SubjectCentralPolicyProvinceId: " + model.SubjectCentralPolicyProvinceId);
            System.Console.WriteLine("ElectID: " + model.ElectID);

            (from t in _context.ElectronicBookSuggestGroups
             where t.SubjectCentralPolicyProvinceId == model.SubjectCentralPolicyProvinceId
             && t.ElectronicBookId == model.ElectID
             select t).ToList().
             ForEach(x => x.Detail = model.Detail);

            (from t in _context.ElectronicBookSuggestGroups
             where t.SubjectCentralPolicyProvinceId == model.SubjectCentralPolicyProvinceId
             && t.ElectronicBookId == model.ElectID
             select t).ToList().
             ForEach(x => x.Problem = model.Problem);

            (from t in _context.ElectronicBookSuggestGroups
             where t.SubjectCentralPolicyProvinceId == model.SubjectCentralPolicyProvinceId
             && t.ElectronicBookId == model.ElectID
             select t).ToList().
             ForEach(x => x.Suggestion = model.Suggestion);

            _context.SaveChanges();

            System.Console.WriteLine("Finish Update Suggestion");
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
                .Where(x => x.SubjectCentralPolicyProvinceId == subjectCentralPolicyProvinceID && x.ElectronicBookId == electID)
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
                        Type = "SignatureProvince File"
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
                .Include(x => x.CentralPolicyProvince)
                .ThenInclude(x => x.Province)
                .Include(x => x.CentralPolicyProvince)
                .ThenInclude(x => x.CentralPolicy)
                .ThenInclude(x => x.CentralPolicyUser)
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
                .Include(x => x.ElectronicBookSuggestGroups)
                .Where(x => x.CentralPolicyProvinceId == centralPolicyProvinceData.Id)
                .ToList();

            foreach(var test in subjectCentralPolicyProvince)
            {
              foreach(var test2 in test.ElectronicBookSuggestGroups)
                {
                    var ExportBodyData = new ExportReportBody
                    {
                        ExportReportHeadId = ExportData.Id,
                        Subject = test.Name,
                        Problem = test2.Detail + " / " + test2.Problem,
                        Suggestion = test2.Suggestion,
                    };
                    _context.ExportReportBodies.Add(ExportBodyData);
                    _context.SaveChanges();

                    foreach (var test3 in test.SubquestionCentralPolicyProvinces)
                    {
                        foreach(var test4 in test3.SubjectCentralPolicyProvinceGroups)
                        {

                            //return Ok(test4);
                            System.Console.WriteLine(" test4.ProvincialDepartment.Name" + test4.ProvincialDepartment.Name);

                            var ExportBodyData2 = _context.ExportReportBodies.Find(ExportBodyData.Id);
                            {
                                if(ExportBodyData2.Department == null)
                                {
                                    System.Console.WriteLine(" test4 ja 1" + test4.ProvincialDepartment.Name);
                                    ExportBodyData2.Department = test4.ProvincialDepartment.Name;
                                } 
                                else 
                                {
                                    System.Console.WriteLine(" test4 ja 2" + test4.ProvincialDepartment.Name);
                                    ExportBodyData2.Department = ExportBodyData2.Department + ", " + test4.ProvincialDepartment.Name;
                                }
                            }
                            _context.Entry(ExportBodyData2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        _context.SaveChanges();
                    }
                }
            }


            return Ok("eiei");
            System.Console.WriteLine("Finish Add Suggestion");
        }

    }
}
