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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class AnswerSubjectController : Controller
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

        public AnswerSubjectController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        //[HttpGet]
        //public IEnumerable<Province> Get()
        //{
        //    var provincedata = from P in _context.Provinces
        //                       select P;
        //    return provincedata;

        //}

        // GET api/values/5
        [HttpGet("{userid}")]
        public IActionResult Get(string userid)
        {
            var loop = new List<object>();

            var userdata = _context.Users
                .Where(m => m.Id == userid).First();

            var provincialdepartment = _context.ProvincialDepartment
                .Where(m => m.DepartmentId == userdata.DepartmentId).First();

            //var subjectcentralpolicyprovincegroupsdata = _context.SubjectCentralPolicyProvinceGroups
            //    .Include(m => m.SubjectCentralPolicyProvince)
            //    .ThenInclude(m => m.CentralPolicyProvince)
            //    .ThenInclude(m => m.CentralPolicy)
            //    .Where(m => m.ProvincialDepartmentId == provincialdepartment.Id).ToList();

            var subjectcentralpolicyprovincegroupsdata = _context.CentralPolicies
                .Include(m => m.CentralPolicyProvinces)
                .ThenInclude(m => m.SubjectCentralPolicyProvinces)
                //.ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
                //.ThenInclude(m => m.ProvincialDepartment)
                .ToList();

            //var subjectcentralpolicyprovincegroupsdata = _context.SubjectCentralPolicyProvinceGroups
            //    .Where(x => x.ProvincialDepartmentId == provincialdepartment.Id)
            //    .GroupBy(g => new
            //    {
            //        SubjectCentralPolicyProvinces = g.SubjectCentralPolicyProvinceId,
            //        CentralPolicyProvinceID = g.SubjectCentralPolicyProvince.CentralPolicyProvinceId,
            //        CentralpolicyID = g.SubjectCentralPolicyProvince.CentralPolicyProvince.CentralPolicyId
            //    })
            //    .Select(g => new
            //    {
            //        g.Key.SubjectCentralPolicyProvinces,
            //        g.Key.CentralPolicyProvinceID,
            //        g.Key.CentralpolicyID
            //    })
            //    .ToList();

            List<object> termsList = new List<object>();
            List<long> termsList2 = new List<long>();
            List<long> termsList3 = new List<long>();

            var test = _context.SubjectCentralPolicyProvinceGroups
                .Where(x => x.ProvincialDepartmentId == userdata.ProvincialDepartmentId)
                .GroupBy(g => new
                {
                    //subjectPolicyProvinceId = g.SubjectCentralPolicyProvinceId,
                    SubjectCentralPolicyProvinceGroupId = g.Id
                })
                .Select(g => new
                {
                    g.Key.SubjectCentralPolicyProvinceGroupId,
                    //g.Key.subjectPolicyProvinceId,

                })
                .ToList();

            foreach (var data in test)
            {
                var test2 = _context.SubjectCentralPolicyProvinces
                    //.Where(x => x.Id == data.subjectPolicyProvinceId)
                    .GroupBy(g => new
                    {
                        CentralPolicyProvinceID = g.CentralPolicyProvinceId
                    })
                    .Select(g => new
                    {
                        g.Key.CentralPolicyProvinceID
                    })
                    .ToList();


                foreach (var gg in test2)
                {
                    long id = gg.CentralPolicyProvinceID;
                    termsList2.Add(id);
                }
            }

            foreach (long id in termsList2)
            {

                var test3 = _context.CentralPolicyProvinces
                    .Where(x => x.Id == id)
                    .GroupBy(g => new
                    {
                        centralPolicyID = g.CentralPolicyId
                    })
                    .Select(g => new
                    {
                        g.Key.centralPolicyID
                    })
                    .ToList();

                foreach (var gg in test3)
                {
                    long cid = gg.centralPolicyID;
                    termsList3.Add(cid);
                }
            }

            foreach (long idTest in termsList3)
            {
                var test4 = _context.CentralPolicies
                   .Where(x => x.Id == idTest)
                   .ToList();

                termsList.Add(new
                {
                    test4
                });
            }

            //foreach (var test in subjectcentralpolicyprovincegroupsdata)
            //{
            //    var data = _context.SubjectCentralPolicyProvinces
            //        .Where(x => x.Id == test.SubjectCentralPolicyProvinces)
            //        .Select(x => x.CentralPolicyProvinceId)
            //        .First();

            //    System.Console.WriteLine("PID: " + data);

            //    var provinceData = _context.CentralPolicyProvinces
            //       .Where(x => x.Id == data)
            //       .ToList();

            //    foreach(var testja in provinceData)
            //    {
            //        var centralPolicyData = _context.CentralPolicies
            //        .Where(x => x.Id == testja.CentralPolicyId)
            //        .ToList();

            //        loop.Add(new
            //        {
            //            centralPolicyData
            //        });
            //    }
            //}



            //var CentralPolicydata = _context.CentralPolicies
            //    .Include(m => m.CentralPolicyProvinces)
            //    .ThenInclude(m => m.SubjectCentralPolicyProvinces)
            //    .Where(x => x)

            //var subjectcentralpolicyprovincedata = _context.SubjectCentralPolicyProvinces
            //    .Where(m => m.Id == subjectcentralpolicyprovincegroupsdata.SubjectCentralPolicyProvinceId).First();

            //var CentralPolicyProvincedata = _context.CentralPolicyProvinces
            //    .Where(m => m.Id == subjectcentralpolicyprovincedata.CentralPolicyProvinceId).First();

            return Ok(termsList);

        }

        // GET api/values/5
        [HttpGet("user/{userid}")]
        public IActionResult Get2(string userid)
        {
            var userdata = _context.Users
                .Where(m => m.Id == userid).First();

            var province = _context.UserProvinces
                .Where(m => m.UserID == userid).First();

            var provincialdepartment = _context.ProvincialDepartment
               .Where(m => m.DepartmentId == userdata.DepartmentId).First();



            //var centralpolicyprovincedata = _context.CentralPolicyProvinces
            //    .Include(m => m.CentralPolicy)
            //    .Include(m => m.SubjectCentralPolicyProvinces)
            //    .ThenInclude(m => m.SubquestionCentralPolicyProvinces)
            //    .ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
            //    .ThenInclude(m => m.ProvincialDepartment)

            //    //.Where(m => m.ProvinceId == province.ProvinceId)

            //    .Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.Type == "NoMaster"))
            //    .Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.SubquestionCentralPolicyProvinces.Any(m => m.SubjectCentralPolicyProvinceGroups.Any(m => m.ProvincialDepartmentId == userdata.ProvincialDepartmentId))))
            //    .ToList();

            var centralpolicyprovincedata = _context.SubjectGroups
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.Typeexaminationplan)
                .Include(m => m.User)
                .Where(m => m.Status == "ใช้งานจริง" || m.Status == "รายงานแล้ว")
                .Where(m => m.Type == "NoMaster")
                .Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.SubquestionCentralPolicyProvinces.Any(m => m.SubjectCentralPolicyProvinceGroups.Any(m => m.ProvincialDepartmentId == userdata.ProvincialDepartmentId))))
                .Where(m => m.ProvinceId == province.ProvinceId)
                .OrderByDescending(m => m.Id)
                .ToList();

            return Ok(centralpolicyprovincedata);
        }

        // GET api/values/5
        [HttpGet("userpeople/{userid}")]
        public IActionResult Get5(string userid)
        {
            //var subjectcentralpolicyprovinceusergroupsdata = _context.SubjectCentralPolicyProvinceUserGroups
            //    .Where(m => m.UserId == userid);


            var centralpolicydata = _context.CentralPolicyUsers
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.Typeexaminationplan)
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.FiscalYearNew)
                .Include(m => m.InspectionPlanEvent)
                .ThenInclude(m => m.User)
                .OrderByDescending(m => m.Id)
                .Where(m => m.InspectionPlanEvent.Status == "ใช้งานจริง")
                .Where(m => m.Status == "ตอบรับ")
                .Where(m => m.UserId == userid).ToList();

            //var centralpolicyprovincedata = _context.CentralPolicyProvinces
            //    .Include(m => m.CentralPolicy)
            //    .Include(m => m.SubjectCentralPolicyProvinces)
            //    .ThenInclude(m => m.SubquestionCentralPolicyProvinces)
            //    .ThenInclude(m => m.SubjectCentralPolicyProvinceUserGroups)
            //    //.ThenInclude(m => m.ProvincialDepartment)
            //    .Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.Type == "NoMaster"))
            //    .Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.SubquestionCentralPolicyProvinces.Any(m => m.SubjectCentralPolicyProvinceUserGroups.Any(m => m.UserId == userid))))
            //    .ToList();


            return Ok(centralpolicydata);
        }

        // GET api/values/5
        [HttpGet("subjectlist/{id}/{userid}")]
        public IActionResult Get3(long id, string userid)
        {
            var userdata = _context.Users
               .Where(m => m.Id == userid).First();

            var provincialdepartment = _context.ProvincialDepartment
           .Where(m => m.DepartmentId == userdata.DepartmentId).First();


            if (userdata.Role_id == 7)
            {
                var subjectdata = _context.SubjectCentralPolicyProvinces
                    .Include(m => m.CentralPolicyProvince)
                    .ThenInclude(m => m.CentralPolicy)
                    .Where(m => m.CentralPolicyProvinceId == id && m.Type == "NoMaster");
                return Ok(subjectdata);
            }
            else
            {
                System.Console.WriteLine("provincialdepartment.Id" + id);

                var subjectdata = _context.SubjectCentralPolicyProvinces
                        .Include(m => m.SubjectDateCentralPolicyProvinces)
                        .Include(m => m.SubquestionCentralPolicyProvinces)
                        .ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
                        .ThenInclude(m => m.ProvincialDepartment)
                        .Where(m => m.SubjectGroupId == id && m.Type == "NoMaster" && m.Status == "ใช้งานจริง")
                        //.Where(m => m.CentralPolicyProvinceId == id && m.Type == "NoMaster")
                        .Where(m => m.SubquestionCentralPolicyProvinces.Any(m => m.SubjectCentralPolicyProvinceGroups.Any(m => m.ProvincialDepartmentId == userdata.ProvincialDepartmentId)))
                        .ToList();

                //var subjectdata = _context.SubjectCentralPolicyProvinces
                //.Include(m => m.CentralPolicyProvince)
                //.ThenInclude(m => m.CentralPolicy)
                //.Where(m => m.CentralPolicyProvinceId == id && m.Type == "NoMaster")
                //.Where(m => m.SubquestionCentralPolicyProvinces.Any(m => m.SubjectCentralPolicyProvinceGroups.Any(m => m.ProvincialDepartmentId == userdata.ProvincialDepartmentId)))
                //.ToList();

                return Ok(subjectdata);
            }
            //var userdata = _context.Users
            //    .Where(m => m.Id == userid).First();

            //var provincialdepartment = _context.ProvincialDepartment
            //   .Where(m => m.DepartmentId == userdata.DepartmentId).First();

            //var centralpolicyprovincedata = _context.CentralPolicyProvinces
            //    .Include(m => m.CentralPolicy)
            //    .Include(m => m.SubjectCentralPolicyProvinces)
            //    .ThenInclude(m => m.SubquestionCentralPolicyProvinces)
            //    .ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
            //    .ThenInclude(m => m.ProvincialDepartment)
            //    .Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.Type == "NoMaster"))
            //    .Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.SubquestionCentralPolicyProvinces.Any(m => m.SubjectCentralPolicyProvinceGroups.Any(m => m.ProvincialDepartmentId == provincialdepartment.Id))))
            //    .ToList();

        }

        // GET api/values/5
        [HttpGet("subjectdetail/{id}")]
        public IActionResult Get4(long id)
        {
            var subjectdata = _context.SubjectCentralPolicyProvinces
                .Include(m => m.CentralPolicyProvince)
                .ThenInclude(m => m.Province)
                .Include(m => m.SubjectGroup)
                .ThenInclude(m => m.SubjectEventFiles)
                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubquestionChoiceCentralPolicyProvinces)
                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
                .Include(m => m.SubjectDateCentralPolicyProvinces)
                .ThenInclude(m => m.CentralPolicyDateProvince)
                .Where(m => m.Id == id && m.Type == "NoMaster")
                .First();
            return Ok(subjectdata);
        }
        // GET api/values/5
        [HttpGet("centralpolicyprovince/{id}/{inspectionPlanEventId}")]
        public IActionResult Get6(long id, long inspectionPlanEventId)
        {
            var centralpolicyprovincedata = _context.CentralPolicyProvinces
                .Where(m => m.Id == id)
                .First();

            var CentralPolicyEventdata = _context.CentralPolicyEvents
                .Where(m => m.InspectionPlanEventId == inspectionPlanEventId)
                .Where(m => m.CentralPolicyId == centralpolicyprovincedata.CentralPolicyId && m.InspectionPlanEvent.ProvinceId == centralpolicyprovincedata.ProvinceId).First();

            System.Console.WriteLine("centralpolicyprovincedata.CentralPolicyId" + centralpolicyprovincedata.CentralPolicyId);

            System.Console.WriteLine("centralpolicyprovincedata.ProvinceId" + centralpolicyprovincedata.ProvinceId);
            System.Console.WriteLine("CentralPolicyEventdata.Id" + CentralPolicyEventdata.Id);

            var question = _context.CentralPolicyEventQuestions
                .Include(m => m.CentralPolicyEvent)
                .ThenInclude(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicyEvent)
                .ThenInclude(m => m.InspectionPlanEvent)
                .ThenInclude(m => m.Province)
                .Include(m => m.CentralPolicyEvent)
                .ThenInclude(m => m.InspectionPlanEvent)
                .ThenInclude(m => m.User)
                .Include(m => m.CentralPolicyEvent)
                .ThenInclude(m => m.SubjectGroupPeopleQuestions)
                .Where(m => m.CentralPolicyEventId == CentralPolicyEventdata.Id)
                .ToList();

            return Ok(question);
        }

        // GET api/values/5
        [HttpGet("answeruser/{userid}")]
        public IActionResult Get7(long id, string userid)
        {
            var answeruserdata = _context.AnswerSubquestions
                .Include(m => m.SubquestionCentralPolicyProvince)
                .ThenInclude(m => m.SubquestionChoiceCentralPolicyProvinces)
                .Where(m => m.UserId == userid)
                .ToList();

            return Ok(answeruserdata);
        }

        // GET api/values/5
        [HttpGet("answeruserdetail/{id}/{userid}")]
        public IActionResult Get8(long id, string userid)
        {
            var answeruserdata = _context.AnswerSubquestions
                .Include(m => m.SubquestionCentralPolicyProvince)
                .ThenInclude(m => m.SubquestionChoiceCentralPolicyProvinces)
                .Where(m => m.UserId == userid && m.Id == id)
                .First();

            return Ok(answeruserdata);
        }

        // GET api/values/5
        [HttpGet("answeruserlist/{id}/{userid}")]
        public IActionResult Get9(long id, string userid)
        {
            var answeruserdata = _context.AnswerSubquestions
                .Include(m => m.SubquestionCentralPolicyProvince)
                .ThenInclude(m => m.SubquestionChoiceCentralPolicyProvinces)
                .Where(m => m.UserId == userid)
                .Where(m => m.SubquestionCentralPolicyProvince.SubjectCentralPolicyProvinceId == id)
                .ToList();

            return Ok(answeruserdata);
        }
        // GET api/values/5
        [HttpGet("answeruserrole7/{userid}")]
        public IActionResult Get10(string userid)
        {
            var answeruserdata = _context.AnswerCentralPolicyProvinces
                .Include(m => m.AnswerCentralPolicyProvinceStatus)
                .ThenInclude(m => m.CentralPolicyEvent)
                .Where(m => m.UserId == userid)
                .ToList();

            return Ok(answeruserdata);
        }
        // GET api/values/5
        [HttpGet("answeruserlistrold7/{id}/{InspectionPlanEventId}/{userid}")]
        public IActionResult Get11(long id, long InspectionPlanEventId, string userid)
        {
            var answeruserdata = _context.AnswerCentralPolicyProvinces
                .Include(m => m.CentralPolicyProvince)
                .Include(m => m.CentralPolicyEventQuestion)
                .Include(m => m.AnswerCentralPolicyProvinceStatus)
                .ThenInclude(m => m.CentralPolicyEvent)
                .Where(m => m.UserId == userid)
                .Where(m => m.CentralPolicyProvinceId == id)
                .Where(m => m.AnswerCentralPolicyProvinceStatus.CentralPolicyEvent.InspectionPlanEventId == InspectionPlanEventId)
                .ToList();

            return Ok(answeruserdata);
        }
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] AnswerSubquestionOutsiderViewModel model)
        {
            var date = DateTime.Now;

            foreach (var answer in model.inputanswer)
            {
                var Answerdata = new AnswerSubquestion
                {
                    AnswerSubquestionStatusId = answer.AnswerSubquestionStatusId,
                    SubquestionCentralPolicyProvinceId = answer.SubquestionCentralPolicyProvinceId,
                    UserId = answer.UserId,
                    Answer = answer.Answer,
                    CreatedAt = date,
                    Description = answer.Description

                };
                _context.AnswerSubquestions.Add(Answerdata);
                _context.SaveChanges();
            }

            return Ok(new { status = true });
        }
        // POST api/values
        [HttpPost("outsider")]
        public IActionResult Post2([FromBody] AnswerSubquestionOutsiderViewModel model)
        {
            var date = DateTime.Now;

            foreach (var answeroutsider in model.inputansweroutsider)
            {
                var Answeroutsiderdata = new AnswerSubquestionOutsider
                {
                    SubquestionCentralPolicyProvinceId = answeroutsider.SubquestionCentralPolicyProvinceId,
                    Name = answeroutsider.Name,
                    Position = answeroutsider.Position,
                    Phonenumber = answeroutsider.Phonenumber,
                    Answer = answeroutsider.Answer,
                    Description = answeroutsider.Description,
                    CreatedAt = date,
                    SenderUserId = answeroutsider.SenderUserId

                };
                _context.AnswerSubquestionOutsiders.Add(Answeroutsiderdata);
                _context.SaveChanges();
            }

            return Ok(new { status = true });
        }
        // POST: api/
        [HttpPost("addfiles")]
        public async Task<IActionResult> Post3([FromForm] AnswerSubquestionOutsiderViewModel model)
        {

            System.Console.WriteLine("Start Upload");
            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                System.Console.WriteLine("in2");
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "//Uploads//";
            System.Console.WriteLine("Start Upload 2");

            //var answerfile = model.inputanswerfile[0];
            //foreach (var answerfile in model.inputanswerfile)
            //{
            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                {
                    //foreach (var formFile in data.files)
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



                    }
                    System.Console.WriteLine("Start Upload 4.1");
                    {
                        System.Console.WriteLine("Start Upload 4.1");
                        var AnswerFile = new AnswerSubquestionFile
                        {

                            SubjectCentralPolicyProvinceId = model.SubjectCentralPolicyProvinceId,
                            UserId = model.UserId,
                            Name = random + ext,
                            Type = ext,
                            Description = Path.GetFileNameWithoutExtension(filePath2),
                        };

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.AnswerSubquestionFiles.Add(AnswerFile);
                        _context.SaveChanges();

                        System.Console.WriteLine("Start Upload 4.3");


                        System.Console.WriteLine("Start Upload 5");
                    }

                    //}

                }
            }
            return Ok(new { status = true });
        }
        // POST api/values
        [HttpPost("answercentralpolicyprovince")]
        public IActionResult Post4([FromBody] AnswerSubquestionOutsiderViewModel model)
        {
            var date = DateTime.Now;
            System.Console.WriteLine("in1");
            foreach (var answer in model.inputanswercentralpolicyprovince)
            {
                System.Console.WriteLine("in2");
                var Answercentralpolicyprovincedata = new AnswerCentralPolicyProvince
                {
                    CentralPolicyProvinceId = answer.CentralPolicyProvinceId,
                    CentralPolicyEventQuestionId = answer.CentralPolicyEventQuestionId,
                    AnswerCentralPolicyProvinceStatusId = answer.AnswerCentralPolicyProvinceStatusId,
                    UserId = answer.UserId,
                    Answer = answer.Answer,
                    CreatedAt = date
                };
                System.Console.WriteLine("in3");
                _context.AnswerCentralPolicyProvinces.Add(Answercentralpolicyprovincedata);
                _context.SaveChanges();
            }

            return Ok(new { status = true });
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string answer, string description)
        {
            var answerdata = _context.AnswerSubquestions.Find(id);
            answerdata.Answer = answer;
            answerdata.Description = description;
            _context.Entry(answerdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }
        // PUT api/values/5
        [HttpPut("editstatus/{id}")]
        public void Put2(long id, string status, long subjectGroupId)
        {
            if (status == "ใช้งานจริง")
            {
                var answer = _context.AnswerSubquestionStatuses
                .Where(m => m.Id == id)
                .FirstOrDefault();

                var AnswerSubquestionStatuses = _context.AnswerSubquestionStatuses
                    .Where(m => m.SubjectCentralPolicyProvinceId == answer.SubjectCentralPolicyProvinceId)
                    .Include(m => m.User)
                    .OrderBy(m => m.User.ProvincialDepartmentId)
                    .Select(m => m.User.ProvincialDepartmentId)
                    .ToList(); //department answer

                long n = 0;
                long checkn = 0;
                var count = 0;
                foreach (var AnswerSubquestionStatus in AnswerSubquestionStatuses)
                {
                    checkn = AnswerSubquestionStatus;
                    if (n != checkn)
                    {
                        n = checkn;
                        count++;
                    }
                    else
                    {
                        n = checkn;
                    }
                }

                var subque = _context.SubquestionCentralPolicyProvinces
                  .Where(m => m.SubjectCentralPolicyProvinceId == answer.SubjectCentralPolicyProvinceId)
                  .FirstOrDefault();

                var invited_depart = _context.SubjectCentralPolicyProvinceGroups
                           .Where(m => m.SubquestionCentralPolicyProvinceId == subque.Id).Count(); //department invited

                if (count == invited_depart)
                {
                    var subjectdata = _context.SubjectCentralPolicyProvinces.Find(answer.SubjectCentralPolicyProvinceId);
                    subjectdata.CheckAnswer = 1;
                    _context.Entry(subjectdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                }

                var subjectall = _context.SubjectCentralPolicyProvinces
                .Where(m => m.SubjectGroupId == subjectGroupId && m.Type == "NoMaster").Count();

                var subjectcheckanswer = _context.SubjectCentralPolicyProvinces
               .Where(m => m.SubjectGroupId == subjectGroupId && m.Type == "NoMaster" && m.CheckAnswer == 1).Count();

                if (subjectall == subjectcheckanswer)
                {
                    var subjectgroupdata = _context.SubjectGroups.Find(subjectGroupId);
                    subjectgroupdata.Status = "รายงานแล้ว";
                    _context.Entry(subjectgroupdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                }

                System.Console.WriteLine("answer.SubjectCentralPolicyProvinceId" + answer.SubjectCentralPolicyProvinceId);
                System.Console.WriteLine("subque.Id" + subque.Id);
                System.Console.WriteLine("Count" + count);
                System.Console.WriteLine("invited_depart" + invited_depart);
            }

            var statusdata = _context.AnswerSubquestionStatuses.Find(id);
            statusdata.Status = status;
            _context.Entry(statusdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }
        // PUT api/values/5
        [HttpPut("editstatusrole7/{id}")]
        public void Put3(long id, string status)
        {
            System.Console.WriteLine("start" + status);
            var statusdata = _context.AnswerCentralPolicyProvinceStatuses
                .Where(m => m.CentralPolicyEventId == id)
                .FirstOrDefault();
            System.Console.WriteLine("tatusdata.Status" + statusdata.ToString());
            statusdata.Status = status;
            _context.Entry(statusdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }
        // PUT api/values/5
        [HttpPut("editanswerrole7")]
        public IActionResult Put4([FromBody] AnswerSubquestionOutsiderViewModel model)
        {
            System.Console.WriteLine("in1");
            foreach (var answer in model.editanswerrole7)
            {
                var Answercentralpolicyprovincedata = _context.AnswerCentralPolicyProvinces.Find(answer.Id);
                Answercentralpolicyprovincedata.Answer = answer.Answer;
                _context.Entry(Answercentralpolicyprovincedata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }

            return Ok(new { status = true });

        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var provincedata = _context.Provinces.Find(id);

            _context.Provinces.Remove(provincedata);
            _context.SaveChanges();
        }
        // POST api/values
        [HttpPost("addstatus")]
        public IActionResult Post5(long SubjectCentralPolicyProvinceId, string UserId, string Status, long subjectGroupId)
        {
            System.Console.WriteLine("in", UserId);
            var date = DateTime.Now;
            var Statusdata = new AnswerSubquestionStatus
            {
                SubjectCentralPolicyProvinceId = SubjectCentralPolicyProvinceId,
                UserId = UserId,
                Status = Status,
                CreatedAt = date
            };
            System.Console.WriteLine("in2");
            _context.AnswerSubquestionStatuses.Add(Statusdata);
            _context.SaveChanges();

            if (Status == "ใช้งานจริง")
            {
                var answer = _context.AnswerSubquestionStatuses
               .Where(m => m.Id == Statusdata.Id)
               .FirstOrDefault();

                var AnswerSubquestionStatuses = _context.AnswerSubquestionStatuses
                    .Where(m => m.SubjectCentralPolicyProvinceId == answer.SubjectCentralPolicyProvinceId)
                    .Include(m => m.User)
                    .OrderBy(m => m.User.ProvincialDepartmentId)
                    .Select(m => m.User.ProvincialDepartmentId)
                    .ToList(); //department answer

                long n = 0;
                long checkn = 0;
                var count = 0;
                foreach (var AnswerSubquestionStatuse in AnswerSubquestionStatuses)
                {
                    checkn = AnswerSubquestionStatuse;
                    if (n != checkn)
                    {
                        n = checkn;
                        count++;
                    }
                    else
                    {
                        n = checkn;
                    }

                }
                var subque = _context.SubquestionCentralPolicyProvinces
                    .Where(m => m.SubjectCentralPolicyProvinceId == answer.SubjectCentralPolicyProvinceId)
                    .FirstOrDefault();

                var invited_depart = _context.SubjectCentralPolicyProvinceGroups
                           .Where(m => m.SubquestionCentralPolicyProvinceId == subque.Id).Count(); //department invited

                if (count == invited_depart)
                {
                    var subjectdata = _context.SubjectCentralPolicyProvinces.Find(answer.SubjectCentralPolicyProvinceId);
                    subjectdata.CheckAnswer = 1;
                    _context.Entry(subjectdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                }

                var subjectall = _context.SubjectCentralPolicyProvinces
                .Where(m => m.SubjectGroupId == subjectGroupId && m.Type == "NoMaster").Count();

                var subjectcheckanswer = _context.SubjectCentralPolicyProvinces
               .Where(m => m.SubjectGroupId == subjectGroupId && m.Type == "NoMaster" && m.CheckAnswer == 1).Count();

                if (subjectall == subjectcheckanswer)
                {
                    var subjectgroupdata = _context.SubjectGroups.Find(subjectGroupId);
                    subjectgroupdata.Status = "รายงานแล้ว";
                    _context.Entry(subjectgroupdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                }

                System.Console.WriteLine("answer.SubjectCentralPolicyProvinceId" + answer.SubjectCentralPolicyProvinceId);
                System.Console.WriteLine("subque.Id" + subque.Id);
                System.Console.WriteLine("Count" + count);
                System.Console.WriteLine("invited_depart" + invited_depart);
            }



            return Ok(Statusdata);
        }
        // POST api/values
        [HttpPost("addstatusrole7")]
        public IActionResult Post6(long CentralPolicyEventId, string UserId, string Status)
        {
            System.Console.WriteLine("in", UserId);
            var date = DateTime.Now;
            var Statusdata = new AnswerCentralPolicyProvinceStatus
            {
                CentralPolicyEventId = CentralPolicyEventId,
                UserId = UserId,
                Status = Status,
                CreatedAt = date
            };
            System.Console.WriteLine("in2");
            _context.AnswerCentralPolicyProvinceStatuses.Add(Statusdata);
            _context.SaveChanges();

            //return Ok(new { status = true });
            return Ok(Statusdata);
        }
        // GET api/values/5
        [HttpGet("answerstatus/{id}/{userid}")]
        public IActionResult Get2(long id, string userid)
        {
            var answerstatusdata = _context.AnswerSubquestionStatuses
                .Where(m => m.SubjectCentralPolicyProvinceId == id && m.UserId == userid)
                .First();
            return Ok(answerstatusdata);
        }
        // GET api/values/5
        [HttpGet("answerfile/{id}/{userid}")]
        public IActionResult Get4(long id, string userid)
        {
            var answerfiledata = _context.AnswerSubquestionFiles
                .Where(m => m.SubjectCentralPolicyProvinceId == id && m.UserId == userid)
                .ToList();
            return Ok(answerfiledata);
        }
        // DELETE api/values/5
        [HttpDelete("deleteanswerfile/{id}")]
        public void Delete2(long id)
        {
            var filedata = _context.AnswerSubquestionFiles.Find(id);

            _context.AnswerSubquestionFiles.Remove(filedata);
            _context.SaveChanges();
        }
        // GET api/values/5
        [HttpGet("answerstatusrole7/{id}/{userid}")]
        public IActionResult Get5(long id, string userid)
        {
            var answerstatusdata = _context.AnswerCentralPolicyProvinceStatuses
                .Where(m => m.CentralPolicyEventId == id && m.UserId == userid)
                .First();
            return Ok(answerstatusdata);
        }
        // GET api/values/5
        [HttpGet("answersoutsider")]
        public IActionResult Get6(long id, string userid)
        {
            var answerstatusdata = _context.AnswerSubquestionOutsiders
                .Include(m => m.SubquestionCentralPolicyProvince)
                .ToList();
            return Ok(answerstatusdata);
        }
        // GET api/values/5
        [HttpGet("answersoutsider/{id}/{userid}")]
        public IActionResult Get12(long id, string userid)
        {
            var answerdata = _context.AnswerSubquestionOutsiders
                .Include(m => m.SubquestionCentralPolicyProvince)
                .Where(m => m.SubquestionCentralPolicyProvince.SubjectCentralPolicyProvinceId == id && m.SenderUserId == userid)
                .ToList();
            return Ok(answerdata);
        }

        // GET api/values/5
        [HttpGet("recommendationinspector/{userid}")]
        public IActionResult GetRecommendationinspector(string userid)
        {
            var userdata = _context.Users
                .Where(m => m.Id == userid).First();

            var province = _context.UserProvinces
                .Where(m => m.UserID == userid).First();

            var provincialdepartment = _context.ProvincialDepartment
               .Where(m => m.DepartmentId == userdata.DepartmentId).First();

            var centralpolicyprovincedata = _context.SubjectGroups
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.Typeexaminationplan)
                .OrderByDescending(m => m.Id)
                .Where(m => m.Status == "ใช้งานจริง" || m.Status == "รายงานแล้ว")
                .Where(m => m.Type == "NoMaster")
                .Where(m => m.Suggestion != "null")
                .Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.SubquestionCentralPolicyProvinces.Any(m => m.SubjectCentralPolicyProvinceGroups.Any(m => m.ProvincialDepartmentId == userdata.ProvincialDepartmentId))))
                .Where(m => m.ProvinceId == province.ProvinceId)
                .ToList();

            return Ok(centralpolicyprovincedata);
        }

        // GET api/values/5
        [HttpGet("recommendationinspectordetail/{id}")]
        public IActionResult GetRecommendationinspector2(long id)
        {
            var centralpolicyprovincedata = _context.SubjectGroups
                .Include(m => m.CentralPolicy)
                .Include(m => m.Province)
                .Include(m => m.AnswerRecommenDationInspectors)
                .Where(m => m.Id == id)
                .FirstOrDefault();
            return Ok(centralpolicyprovincedata);
        }
        // POST api/values
        [HttpPost("addrecommendationinspector")]
        public IActionResult Post7(long SubjectGroupId, string UserId, string Answer, string Status)
        {
            System.Console.WriteLine("in", UserId);
            var date = DateTime.Now;
            var AnswerRecommenDationInspectorData = new AnswerRecommenDationInspector
            {
                SubjectGroupId = SubjectGroupId,
                UserId = UserId,
                Answersuggestion = Answer,
                Status = Status,
                CreatedAt = date
            };
            System.Console.WriteLine("in2");
            _context.AnswerRecommenDationInspectors.Add(AnswerRecommenDationInspectorData);
            _context.SaveChanges();

            return Ok(new { status = true });
            //return Ok(Statusdata);
        }
        // GET api/values/5
        [HttpGet("recommendationinspectoruser/{userid}")]
        public IActionResult Get11(string userid)
        {
            var answeruserdata = _context.AnswerRecommenDationInspectors
                .Where(m => m.UserId == userid)
                .ToList();

            return Ok(answeruserdata);
        }
        // GET api/values/5
        [HttpGet("answerrecommendationinspectoruser/{id}/{userid}")]
        public IActionResult Get12(string userid, long id)
        {
            var answeruserdata = _context.AnswerRecommenDationInspectors
                .Where(m => m.SubjectGroupId == id)
                .Where(m => m.UserId == userid)
                .FirstOrDefault();

            return Ok(answeruserdata);
        }
        // PUT api/values/5
        [HttpPut("editanswerrecommendationinspector/{id}")]
        public IActionResult Put5(long id, string Answer, string Status)
        {

            var AnswerRecommenDationInspectordata = _context.AnswerRecommenDationInspectors.Find(id);
            AnswerRecommenDationInspectordata.Answersuggestion = Answer;
            AnswerRecommenDationInspectordata.Status = Status;
            _context.Entry(AnswerRecommenDationInspectordata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { status = true });

        }
        // GET api/values/5
        [HttpGet("subjecteventfiles/{subjectgroupid}")]
        public IActionResult Get13( long subjectgroupid)
        {
            var answeruserdata = _context.SubjectEventFiles
                .Where(m => m.SubjectGroupId == subjectgroupid)
                .ToList();
               
            return Ok(answeruserdata);
        }
    }
}
