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
                   .Include(m => m.FiscalYearNew)
                   .Include(m => m.Typeexaminationplan)
                   .Include(m => m.CentralPolicyProvinces)
                   .ThenInclude(x => x.Province)
                   .Include(m => m.CentralPolicyDates)
                   .Include(m => m.User)
                   .ThenInclude(m => m.Ministries)
                   .ThenInclude(m => m.Departments)
                   .OrderByDescending(m => m.Id)
                   .Where(m => m.Class == "แผนการตรวจประจำปี" && m.Id != 1)
                   .ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var centralpolicydata = _context.CentralPolicies
                .Include(m => m.FiscalYearNew)
                .Include(m => m.Typeexaminationplan)
                .Include(m => m.CentralPolicyProvinces)
                .ThenInclude(x => x.Province)
                .Include(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicyFiles)
                .Include(m => m.Subjects)
                .ThenInclude(m => m.Subquestions)
                .Include(m => m.User)
                .ThenInclude(m => m.Ministries)
                .ThenInclude(m => m.Departments)
                .Where(m => m.Id == id).FirstOrDefault();

            return Ok(centralpolicydata);
            //return "value";
        }
        // GET api/values/5
        [HttpGet("fiscalfear/{id}")]
        public IActionResult Get2(long id)
        {
            var centralpolicydata = _context.CentralPolicies
                .Include(m => m.FiscalYearNew)
                .Include(m => m.Typeexaminationplan)
                .Include(m => m.CentralPolicyProvinces)
                .ThenInclude(x => x.Province)
                .Include(m => m.CentralPolicyDates)
                .Include(m => m.User)
                .ThenInclude(m => m.Ministries)
                .ThenInclude(m => m.Departments)
                .OrderByDescending(m => m.Id)
                .Where(m => m.FiscalYearNewId == id && m.Class == "แผนการตรวจประจำปี" && m.Id != 1).ToList();


            return Ok(centralpolicydata);
            //return "value";
        }

        // GET api/values/5
        [HttpGet("subjectcount/{id}")]
        public IActionResult Get3(long id)
        {
            var subjectdata = _context.SubjectCentralPolicyProvinces
                  .Include(m => m.SubjectDateCentralPolicyProvinces)
                  .ThenInclude(m => m.CentralPolicyDateProvince)
                  .Include(m => m.CentralPolicyProvince)
                  //.Where(m => m.CentralPolicyId == id);
                  .Where(m => m.CentralPolicyProvince.CentralPolicyId == id && m.Type == "Master");

            return Ok(subjectdata.Count());
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
                TypeexaminationplanId = model.TypeexaminationplanId,
                FiscalYearNewId = model.FiscalYearNewId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Status = model.Status,
                CreatedAt = date,
                CreatedBy = model.UserID,
                Class = model.Class,
                UpdateAt = date
            };

            _context.CentralPolicies.Add(centralpolicydata);
            _context.SaveChanges();

            var logdata = new Log
            {
                UserId = model.UserID,
                DatabaseName = "CentralPolicy",
                EventType = "เพิ่ม",
                EventDate = date,
                Detail = "เพิ่มแผนการตรวจราชการ",
                Allid = centralpolicydata.Id,
            };

            _context.Logs.Add(logdata);
            _context.SaveChanges();

            foreach (var id in model.ProvinceId)
            {
                var centralpolicyprovincedata = new CentralPolicyProvince
                {
                    ProvinceId = id,
                    CentralPolicyId = centralpolicydata.Id,
                    Step = "มอบหมายเขต",
                    Status = "ร่างกำหนดการ",
                    Active = 1,
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
                    _context.SaveChanges();
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
                        using (var stream = System.IO.File.Create(filePath + random + ext))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        var CentralPolicyFile = new CentralPolicyFile
                        {
                            CentralPolicyId = centralpolicydata.Id,
                            Name = random + ext,
                            Description = Path.GetFileNameWithoutExtension(filePath2),
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
                centralpolicydata.TypeexaminationplanId = model.TypeexaminationplanId;
                centralpolicydata.FiscalYearNewId = model.FiscalYearNewId;
                centralpolicydata.StartDate = model.StartDate;
                centralpolicydata.EndDate = model.EndDate;
                centralpolicydata.Status = model.Status;
                centralpolicydata.UpdateAt = date;
                centralpolicydata.CreatedBy = model.UserID;
                centralpolicydata.Class = "แผนการตรวจประจำปี";

            };

            //_context.CentralPolicies.Add(centralpolicydata);
            _context.Entry(centralpolicydata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            var logdata = new Log
            {
                UserId = model.UserID,
                DatabaseName = "CentralPolicy",
                EventType = "แก้ไข",
                EventDate = date,
                Detail = "แก้ไขแผนการตรวจราชการ",
                Allid = centralpolicydata.Id,
            };

            _context.Logs.Add(logdata);
            _context.SaveChanges();

            // var delData = _context.CentralPolicyProvinces
            //        .Where(x => x.CentralPolicyId == editId)
            //        .ToList();
            // foreach (var del in delData)
            // {
            //     _context.CentralPolicyProvinces.Remove(del);
            // }
            // _context.SaveChanges();
            // System.Console.WriteLine("edit 1: " + model.AddProvince.Length);
            if (model.RemoveProvince != null)
            {
                System.Console.WriteLine("edit 1.1: " + model.RemoveProvince.Length);
                foreach (var provinceId in model.RemoveProvince)
                {
                    System.Console.WriteLine("removeID: " + provinceId);
                    var removeData = _context.CentralPolicyProvinces
                    .Where(x => x.CentralPolicyId == editId && x.ProvinceId == provinceId)
                    .FirstOrDefault();
                    {
                        removeData.Active = 0;
                    };
                    _context.Entry(removeData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                }
            }


            // System.Console.WriteLine("edit 2: " + model.AddProvince.Count());

            if (model.AddProvince != null)
            {
                System.Console.WriteLine("edit 2.1: " + model.AddProvince.Length);
                foreach (var provinceId in model.AddProvince)
                {
                    var addProvince = _context.CentralPolicyProvinces
                    .Where(x => x.ProvinceId == provinceId && x.CentralPolicyId == editId)
                    .ToList();

                    System.Console.WriteLine("addProvince" + addProvince);

                    if (addProvince.Count() == 0)
                    {
                        var centralPolicyData = new CentralPolicyProvince
                        {
                            ProvinceId = provinceId,
                            CentralPolicyId = editId,
                            Step = "มอบหมายเขต",
                            Status = "ร่างกำหนดการ",
                            Active = 1,
                        };
                        _context.CentralPolicyProvinces.Add(centralPolicyData);
                        _context.SaveChanges();
                    }
                    else
                    {
                        var sameData = _context.CentralPolicyProvinces
                        .Where(x => x.CentralPolicyId == editId && x.ProvinceId == provinceId)
                        .FirstOrDefault();
                        {
                            sameData.Active = 1;
                        };
                        _context.Entry(sameData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                    }
                    System.Console.WriteLine("edit 3");
                }
            }


            // foreach (var id in model.ProvinceId)
            // {
            //     System.Console.WriteLine("in2");
            //     var centralpolicyprovincedata = new CentralPolicyProvince
            //     {
            //         ProvinceId = id,
            //         CentralPolicyId = editId,
            //         Step = "มอบหมายเขต",
            //         Status = "ร่างกำหนดการ"
            //     };
            //     _context.CentralPolicyProvinces.Add(centralpolicyprovincedata);
            // }
            // _context.SaveChanges();

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
                    _context.SaveChanges();
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
                        using (var stream = System.IO.File.Create(filePath + random + ext))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        var CentralPolicyFiledata = new CentralPolicyFile
                        {
                            CentralPolicyId = centralpolicydata.Id,
                            Name = random + ext,
                            Description = Path.GetFileNameWithoutExtension(filePath2),
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
        [HttpDelete("{id}/{userid}")]
        public void Delete(long id, string userid)
        {
            var date = DateTime.Now;
            var centralpolicydata = _context.CentralPolicies.Find(id);

            _context.CentralPolicies.Remove(centralpolicydata);
            _context.SaveChanges();

            var logdata = new Log
            {
                UserId = userid,
                DatabaseName = "CentralPolicy",
                EventType = "ลบ",
                EventDate = date,
                Detail = "ลบแผนการตรวจราชการ"+centralpolicydata.Title,
                Allid = centralpolicydata.Id,
            };

            _context.Logs.Add(logdata);
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

            if (model.UserMinistryId != null)
            {
                foreach (var id in model.UserMinistryId)
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

            if (model.UserId != null)
            {

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

            if (model.UserDepartmentId != null)
            {
                foreach (var id in model.UserDepartmentId)
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

            if (model.UserProvincialDepartmentId != null)
            {
                foreach (var id in model.UserProvincialDepartmentId)
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
                .ThenInclude(s => s.Ministries)
                .Include(m => m.User)
                .ThenInclude(x => x.Departments)
                .Include(m => m.User)
                .ThenInclude(x => x.ProvincialDepartments)
                .Include(m => m.User)
                .ThenInclude(m => m.UserProvince)
                .Include(p => p.User)
                .ThenInclude(p => p.Sides)
                .OrderBy(m => m.User.Ministries.Name)
                .OrderBy(m => m.User.Departments.Name)
                .OrderBy(m => m.User.ProvincialDepartments.Name)
                .OrderBy(m => m.User.SideId)
                .Where(m => m.CentralPolicyId == centralpolicyprovince.CentralPolicyId && m.InspectionPlanEventId == planId);

            return Ok(centralpolicyuserdata);
        }


        // GET api/values/5
        [HttpGet("ministry/{id}")]
        public IActionResult Getministryuser(long id)
        {
            //var centralpolicyprovince = _context.CentralPolicyProvinces
            //.Where(m => m.Id == id).FirstOrDefault();

            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.User)
                //.ThenInclude(m => m.UserProvince)
                .ThenInclude(s => s.Ministries)
                .OrderBy(s => s.User.Ministries.Name)
                .Where(m => m.InspectionPlanEventId == id)
                .Where(m => m.User.Role_id == 6).ToList();

            return Ok(centralpolicyuserdata);
        }

        // GET api/values/5
        [HttpGet("department/{id}")]
        public IActionResult Getdepartmentuser(long id)
        {
            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.User)
                 //.ThenInclude(m => m.UserProvince)
                 .ThenInclude(s => s.Departments)
                .OrderBy(s => s.User.Departments.Name)
                .Where(m => m.InspectionPlanEventId == id)
                .Where(m => m.User.Role_id == 10).ToList();

            return Ok(centralpolicyuserdata);
        }

        // GET api/values/5
        [HttpGet("provincialdepartment/{id}")]
        public IActionResult Getprovincialdepartment(long id)
        {
            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.User)
                             //.ThenInclude(m => m.UserProvince)
                             .ThenInclude(s => s.ProvincialDepartments)
                .OrderBy(s => s.User.ProvincialDepartments.Name)
                .Where(m => m.InspectionPlanEventId == id)
                .Where(m => m.User.Role_id == 9).ToList();

            return Ok(centralpolicyuserdata);
        }

        // GET api/values/5
        [HttpGet("people/{id}")]
        public IActionResult Getpeopleuser(long id)
        {
            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.User)
                .ThenInclude(m => m.Sides)
                //.ThenInclude(m => m.UserProvince)
                .Where(m => m.InspectionPlanEventId == id)
                .Where(m => m.User.Role_id == 7).ToList();

            return Ok(centralpolicyuserdata);
        }

        // GET api/values/5
        [HttpGet("getcentralpolicyfromprovince/{id}")]
        public IActionResult getcentralpolicyfromprovince(long id)
        {
            var year = DateTime.Now;

            var fiscalyearData = _context.FiscalYearNew
                .Where(m => m.StartDate <= year && m.EndDate >= year).FirstOrDefault();

            //var fiscalyearData = _context.FiscalYears
            //                  .OrderByDescending(x => x.Year)
            //                  .FirstOrDefault();

            var centralpolicyprovincedata = _context.CentralPolicyProvinces
                .Include(m => m.CentralPolicy)
                .Where(m => m.CentralPolicy.FiscalYearNewId == fiscalyearData.Id)
                .Where(m => m.ProvinceId == id && m.Active == 1)
                .ToList();

            return Ok(centralpolicyprovincedata);


        }

        // PUT api/values/5
        [HttpPut("acceptcentralpolicy/{id}")]
        public void PutStatus(long id, string status, string userid)

        {
            System.Console.WriteLine("ID: " + id);
            System.Console.WriteLine("Status: " + status);


            //var acaccept = _context.CentralPolicyUsers.Where(x => x.Id == id)
            //               .First();
            //System.Console.WriteLine("acaccept: " + acaccept.Id);


            (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
              ForEach(x => x.Status = status);

            //(from t in _context.CentralPolicyUsers where t.Id == id select t).ToList().
            //    ForEach(x => x.Status = status);

            //var accept = _context.CentralPolicyUsers.Find(id);
            //var accept = _context.CentralPolicyUsers.Where(m => m.Id == id).FirstOrDefault();
            //accept.Status = status;
            //System.Console.WriteLine("Status: " + accept);
            //_context.Entry(accept).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("acceptcentralpolicy2/{id}")]
        public void PutStatus2(long id, string status, string userid, string report)
        {
            (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
              ForEach(x => x.Status = status);

            (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
              ForEach(x => x.Report = report);

            _context.SaveChanges();
        }
        // GET api/values/5
        [HttpGet("getcentralpolicyfromprovince/{id}/{planid}")]
        public IActionResult GetUsers2(string id, long planid)
        {
            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyProvinces)
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.FiscalYearNew)
                .Where(m => m.InspectionPlanEventId == planid)
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
            .Include(x => x.FiscalYearNew)
            .Include(m => m.CentralPolicyDates)
            .Include(m => m.CentralPolicyFiles)
            .Include(m => m.CentralPolicyProvinces)
            .ThenInclude(m => m.Province)
            .Include(m => m.Typeexaminationplan)
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
                return Ok(new { centralpolicydata, userdata, CentralPolicyEventdata, provincedata, centralpolicyprovince });
            }
            else
            {
                var userdata = "";
                var CentralPolicyEventdata = "";
                return Ok(new { centralpolicydata, userdata, CentralPolicyEventdata, provincedata, centralpolicyprovince, answerPeople });
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
        public async Task<IActionResult> PostReportCentralPolicy([FromForm] CentralPolicyProvinceViewModel model, CentralPolicyUserModel userModel, long id)
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
                // .Include(x => x.ElectronicBookSuggestGroups)
                .Where(m => m.CentralPolicyProvinceId == centralpolicyprovincedata.Id)
                .ToList();

            return Ok(subjectcentralpolicyprovincedata);
            //return "value";
        }

        [HttpPut("sendassign/{id}")]
        public void PutAssign(long id, string assign, string department, string position, string phone, string email, string userid)
        {

            //var CentralPolicyUsersdata = _context.CentralPolicyUsers.Find(id);
            //CentralPolicyUsersdata.ForwardName = assign;
            //CentralPolicyUsersdata.ForwardDepartment = department;
            //CentralPolicyUsersdata.ForwardPosition = position;
            //CentralPolicyUsersdata.ForwardPhone = phone;
            //CentralPolicyUsersdata.ForwardEmail = email;
            //_context.Entry(CentralPolicyUsersdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //_context.SaveChanges();

            (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
                ForEach(x => x.ForwardName = assign);

            (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
                ForEach(x => x.ForwardDepartment = department);

            (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
                ForEach(x => x.ForwardPosition = position);

            (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
                ForEach(x => x.ForwardPhone = phone);

            (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
                ForEach(x => x.ForwardEmail = email);

            (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
               ForEach(x => x.Status = "มอบหมาย");

            (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
                ForEach(x => x.ForwardCount = 1);

            (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
                ForEach(x => x.ForwardExternal = 1);


            _context.SaveChanges();
        }

        [HttpPut("sendassigninternal/{id}")]
        public void PutAssigninternal(long id, string userid, string assignuserid)
        {
            var peopleinvitedatas = _context.CentralPolicyUsers
                .Where(m => m.InspectionPlanEventId == id && m.UserId == userid).ToList();


            var userdata = _context.Users.Find(userid);

            foreach (var peopleinvitedata in peopleinvitedatas)
            {
                var CentralPolicyGroupdata = new CentralPolicyGroup
                {
                };
                _context.CentralPolicyGroups.Add(CentralPolicyGroupdata);
                _context.SaveChanges();

                var CentralPolicyUserdata = new CentralPolicyUser
                {
                    CentralPolicyId = peopleinvitedata.CentralPolicyId,
                    ProvinceId = peopleinvitedata.ProvinceId,
                    InspectionPlanEventId = peopleinvitedata.InspectionPlanEventId,
                    CentralPolicyGroupId = CentralPolicyGroupdata.Id,
                    UserId = assignuserid,
                    Status = "รอการตอบรับ",
                    ForwardName = "ถูกมอบหมาย",
                    InvitedBy = userdata.Prefix + " " + userdata.Name,
                    DraftStatus = "ร่างกำหนดการ",
                };
                _context.CentralPolicyUsers.Add(CentralPolicyUserdata);
                _context.SaveChanges();
            }
            //var CentralPolicyUsersdata = _context.CentralPolicyUsers.Find(id);
            //CentralPolicyUsersdata.ForwardName = assign;
            //CentralPolicyUsersdata.ForwardDepartment = department;
            //CentralPolicyUsersdata.ForwardPosition = position;
            //CentralPolicyUsersdata.ForwardPhone = phone;
            //CentralPolicyUsersdata.ForwardEmail = email;
            //_context.Entry(CentralPolicyUsersdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //_context.SaveChanges();



            (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
               ForEach(x => x.Status = "มอบหมาย");

            (from t in _context.CentralPolicyUsers where t.InspectionPlanEventId == id && t.UserId == userid select t).ToList().
               ForEach(x => x.ForwardCount = 1);

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
            System.Console.WriteLine("model.SubjectCentralPolicyProvinceId" + model.SubjectCentralPolicyProvinceId);

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
        public IActionResult GetSubjectEvent(long id, long subjectgroupid)
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
                // .Include(x => x.ElectronicBookSuggestGroups)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(x => x.AnswerSubquestions)
                .ThenInclude(x => x.AnswerSubquestionStatus)
                .ThenInclude(x => x.User)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(x => x.AnswerSubquestionOutsiders)

                .Include(m => m.AnswerSubquestionFiles)

                .Where(m => m.Type == "NoMaster")
                .Where(m => m.SubjectGroupId == subjectgroupid)
                .Where(m => m.CentralPolicyProvinceId == id)
                .OrderBy(p => p.Name)
                .ToList();

            var subjectgroup = _context.SubjectGroups
                .Include(m => m.SubjectGroupPeopleQuestions)
                .Include(m => m.AnswerRecommenDationInspectors)
                .ThenInclude(m => m.User)
                .ThenInclude(m => m.UserProvince)
                .ThenInclude(m => m.Province)
                .Where(m => m.Id == subjectgroupid).FirstOrDefault();

            var subjectgroupevent = _context.SubjectGroupPeopleQuestions
                .Where(p => p.SubjectGroupId == subjectgroupid).FirstOrDefault();

            if (subjectgroupevent != null)
            {
                var centralPolicyEventdata = _context.CentralPolicyEvents
                .Include(p => p.InspectionPlanEvent)
                .Where(p => p.Id == subjectgroupevent.CentralPolicyEventId).FirstOrDefault();

                return Ok(new { subjectgroup, subjectcentralpolicyprovincedata, centralPolicyEventdata });
            }
            else
            {
                var centralPolicyEventdata = "";
                return Ok(new { subjectgroup, subjectcentralpolicyprovincedata, centralPolicyEventdata });
            }
        }

        // GET api/values/5
        [HttpGet("getquestionpeople/{cenproid}/{planid}")]
        public IActionResult getquestionpeople(long cenproid, long planid)
        {
            //var cenid = _context.CentralPolicyProvinces
            //    .Where(m => m.Id == cenproid).FirstOrDefault();

            //var cenprolicyevent = _context.CentralPolicyEvents
            //    .Where(m => m.InspectionPlanEventId == planid)
            //    //.Where(m => m.CentralPolicyId == cenid.CentralPolicyId)
            //    .FirstOrDefault();

            var question = _context.CentralPolicyEventQuestions
                .Include(m => m.CentralPolicyEvent)
                .Include(m => m.AnswerCentralPolicyProvinces)
                .ThenInclude(m => m.User)
                .ThenInclude(m => m.UserProvince)
                .ThenInclude(m => m.Province)
                .Where(m => m.CentralPolicyEventId == planid)
                .ToList();

            return Ok(question);
        }

        //POST api/values
        [HttpPost("addPeoplequestion")]
        public IActionResult addPeoplequestion(CentralPolicyEventQuestionViewModel model)
        {
            //var cenid = _context.CentralPolicyProvinces
            //.Where(m => m.Id == model.cenproid).FirstOrDefault();

            //var cenprolicyevent = _context.CentralPolicyEvents
            //    .Where(m => m.InspectionPlanEventId == model.planid && m.CentralPolicyId == cenid.CentralPolicyId)
            //    //.Where(m => m.CentralPolicyId == cenid.Id)
            //    .FirstOrDefault();

            var cenprolicyevent = _context.CentralPolicyEvents
                .Where(m => m.Id == model.planid)
                //.Where(m => m.CentralPolicyId == cenid.Id)
                .FirstOrDefault();

            var CentralPolicyEventQuestiondata = new CentralPolicyEventQuestion
            {
                CentralPolicyEventId = cenprolicyevent.Id,
                QuestionPeople = model.question,
                //NotificationDate = model.notificationdate,
                //DeadlineDate = model.deadlinedate
            };

            _context.CentralPolicyEventQuestions.Add(CentralPolicyEventQuestiondata);
            _context.SaveChanges();

            return Ok(cenprolicyevent);
        }
        // DELETE api/values/5
        [HttpDelete("deletedepartment/{id}")]
        public void Delete2(long id)
        {
            var departmentdata = _context.SubjectCentralPolicyProvinceGroups.Find(id);

            _context.SubjectCentralPolicyProvinceGroups.Remove(departmentdata);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("deletecentralpolicyuser/{id}")]
        public void Deletecentralpolicyuser(long id)
        {
            var departmentdata = _context.CentralPolicyUsers.Find(id);

            _context.CentralPolicyUsers.Remove(departmentdata);
            _context.SaveChanges();
        }

        [HttpGet("usersinvited/{id}/{planid}")]
        public IActionResult GetUsers3(string id, long planid)
        {
            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyProvinces)
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.FiscalYearNew)
                .Where(m => m.InspectionPlanEventId == planid)
                .Where(m => m.UserId == id);

            return Ok(centralpolicyuserdata);
        }

        [HttpGet("getSubjectgroupidfromplanid/{cenproid}/{planid}")]
        public IActionResult GetSubjectgroupidfromplanid(long cenproid, long planid)
        {
            var cenprodata = _context.CentralPolicyProvinces
                .Where(p => p.Id == cenproid).FirstOrDefault();

            var centralpolicyevent = _context.CentralPolicyEvents
                .Where(p => p.CentralPolicyId == cenprodata.CentralPolicyId)
                .Where(p => p.InspectionPlanEvent.ProvinceId == cenprodata.ProvinceId)
                .Where(p => p.InspectionPlanEventId == planid).FirstOrDefault();

            var eventgroup = _context.SubjectGroupPeopleQuestions
                .Where(p => p.CentralPolicyEventId == centralpolicyevent.Id).FirstOrDefault();

            if (eventgroup != null)
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
         // .Include(x => x.ElectronicBookSuggestGroups)

         .Include(m => m.SubquestionCentralPolicyProvinces)
         .ThenInclude(x => x.AnswerSubquestions)
         .ThenInclude(x => x.AnswerSubquestionStatus)
         .ThenInclude(x => x.User)

         .Include(m => m.SubquestionCentralPolicyProvinces)
         .ThenInclude(x => x.AnswerSubquestionOutsiders)

         .Where(m => m.Type == "NoMaster")
         .Where(m => m.SubjectGroupId == eventgroup.SubjectGroupId)
         .Where(m => m.CentralPolicyProvinceId == cenproid).ToList();

                return Ok(new { subjectcentralpolicyprovincedata });
            }
            else
            {
                return Ok(false);
            }
            //var centralpolicyuserdata = _context.CentralPolicyUsers
            //    .Include(m => m.CentralPolicy)
            //    .ThenInclude(m => m.CentralPolicyDates)
            //    .Include(m => m.CentralPolicy)
            //    .ThenInclude(m => m.CentralPolicyProvinces)
            //    .Include(m => m.CentralPolicy)
            //    .ThenInclude(m => m.FiscalYearNew)
            //    .Where(m => m.InspectionPlanEventId == planid)
            //    .Where(m => m.UserId == id);


        }
    }
}