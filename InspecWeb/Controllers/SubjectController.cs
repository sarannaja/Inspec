using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class SubjectController : Controller
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

        public SubjectController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet("getsubjectfromprovince/{proid}")]
        public IEnumerable<SubjectCentralPolicyProvince> Get4(long proid)
        {
            //var subjectdata = from P in _context.Subjects
            //                  select P;
            //return subjectdata;
            var subjectdata = _context.SubjectCentralPolicyProvinces
                //.Where(m => m.CentralPolicyProvince.ProvinceId == proid)
                .Where(m => m.Status == "ใช้งานจริง")
                .Where(m => m.Type == "Master")
                .ToList();

            return subjectdata;
            //return 
            //_context.Provinces
            //   .Include(p => p.Districts)
            //   .Where(p => p.Id == 1)
            //   .ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var subjectdata = _context.SubjectCentralPolicyProvinces
                .Include(m => m.SubjectDateCentralPolicyProvinces)
                .ThenInclude(m => m.CentralPolicyDateProvince)
                .Include(m => m.CentralPolicyProvince)
                .Include(m => m.SubquestionCentralPolicyProvinces)
                //.Where(m => m.CentralPolicyId == id);
                .Where(m => m.CentralPolicyProvince.CentralPolicyId == id && m.Type == "Master");

            return Ok(subjectdata);
        }

        [HttpGet("subjectdetail/{id}")]
        public IActionResult Get2(long id)
        {
            var subjectdata = _context.SubjectCentralPolicyProvinces
                .Include(m => m.CentralPolicyProvince)
                .Include(m => m.SubjectDateCentralPolicyProvinces)
                .ThenInclude(m => m.CentralPolicyDateProvince)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubquestionChoiceCentralPolicyProvinces)

                .Include(m => m.SubquestionCentralPolicyProvinces)
                .ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
                .ThenInclude(m => m.ProvincialDepartment)
                .Include(m => m.SubjectCentralPolicyProvinceFiles)
                .Where(m => m.Id == id)
                .First();

            return Ok(subjectdata);
        }

        // POST api/values
        [HttpPost("addsubquestionopen")]
        public Subquestion Post2(long subjectId, string name)
        {
            System.Console.WriteLine("subjectId" + subjectId);

            var questionsopendata = new Subquestion
            {
                SubjectId = subjectId,
                Name = name,
                Type = "คำถามปลายเปิด"

            };

            _context.Subquestions.Add(questionsopendata);
            _context.SaveChanges();

            return questionsopendata;
        }

        // POST api/values
        [HttpPost("addsubquestionclose")]
        public Subquestion Post3([FromBody] QuestioncloseViewModel model)
        {
            //System.Console.WriteLine("subjectId" + subjectId);

            var questionsclosedata = new Subquestion
            {
                SubjectId = model.SubjectId,
                Name = model.Name,
                Type = "คำถามปลายปิด"

            };

            _context.Subquestions.Add(questionsclosedata);
            _context.SaveChanges();

            foreach (var questionclosechoice in model.inputanswerclose2)
            {
                var Subquestionchoiceclosedata = new SubquestionChoice
                {
                    SubquestionId = questionsclosedata.Id,
                    Name = questionclosechoice.answerclose

                };
                _context.SubquestionChoices.Add(Subquestionchoiceclosedata);
                _context.SaveChanges();
            }

            return questionsclosedata;
        }

        // POST api/values
        [HttpPost("addchoices")]
        public IActionResult Post4(long subquestionid, string name)
        {
            //System.Console.WriteLine("subjectId" + subjectId);

            var subquestionchoicedata = new SubquestionChoiceCentralPolicyProvince
            {
                SubquestionCentralPolicyProvinceId = subquestionid,
                Name = name

            };

            _context.SubquestionChoiceCentralPolicyProvinces.Add(subquestionchoicedata);
            _context.SaveChanges();

            return Ok(new { status = true });
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] SubjectViewModel model)
        {
            var userdata = _context.Users
                .Where(p => p.Id == model.UserID).FirstOrDefault();

            var date = DateTime.Now;
            long GetSubjectID = 0;
            List<object> termsList = new List<object>();
            //var subjectdata = new Subject
            //{
            //    Name = model.Name,
            //    CentralPolicyId = model.CentralPolicyId,
            //    Answer = model.Answer,

            //};
            //_context.Subjects.Add(subjectdata);
            //_context.SaveChanges();

            //foreach (var id in model.CentralPolicyDateId)
            //{
            //    var subjectdatedata = new SubjectDate
            //    {
            //        SubjectId = subjectdata.Id,
            //        CentralPolicyDateId = id
            //    };
            //    _context.SubjectDates.Add(subjectdatedata);
            //}
            //_context.SaveChanges();

            //foreach (var questionopen in model.inputquestionopen)
            //{
            //    var Subquestionopendata = new Subquestion
            //    {
            //        SubjectId = subjectdata.Id,
            //        Name = questionopen.questionopen,
            //        Type = "คำถามปลายเปิด"
            //    };
            //    _context.Subquestions.Add(Subquestionopendata);
            //}
            //_context.SaveChanges();

            //foreach (var questionclose in model.inputquestionclose)
            //{
            //    var Subquestionclosedata = new Subquestion
            //    {
            //        SubjectId = subjectdata.Id,
            //        Name = questionclose.questionclose,
            //        Type = "คำถามปลายปิด"
            //    };
            //    _context.Subquestions.Add(Subquestionclosedata);
            //    _context.SaveChanges();

            //    foreach (var questionclosechoice in questionclose.inputanswerclose)
            //    {
            //        var Subquestionchoiceclosedata = new SubquestionChoice
            //        {
            //            SubquestionId = Subquestionclosedata.Id,
            //            Name = questionclosechoice.answerclose,
            //        };
            //        _context.SubquestionChoices.Add(Subquestionchoiceclosedata);
            //        _context.SaveChanges();
            //    }
            //}
            long subjectid = 0;
            var n = 0;
            long box = -1;

            foreach (var departmentId in model.inputsubjectdepartment)
            {
                //System.Console.WriteLine("In1");
                var provincialdepartmentprovicedata = _context.ProvincialDepartmentProvince
                    .Where(m => m.ProvincialDepartmentID == departmentId.departmentId)
                    .Select(x => x.ProvinceId)
                    .ToList();

                foreach (var provinceId in provincialdepartmentprovicedata)
                {

                    System.Console.WriteLine("all" + provinceId);
                    var centralpolicyprovinceData = _context.CentralPolicyProvinces
                            .Where(x => x.ProvinceId == provinceId && x.CentralPolicyId == model.CentralPolicyId)
                            .FirstOrDefault();

                    //System.Console.WriteLine("have" + centralpolicyprovinceData.ProvinceId);

                    if (centralpolicyprovinceData != null)
                    {
                        System.Console.WriteLine("have" + centralpolicyprovinceData.ProvinceId);

                        if (n == 0)
                        {
                            var SubjectGroupdata = new SubjectGroup
                            {
                                CentralPolicyId = model.CentralPolicyId,
                                ProvinceId = provinceId,
                                Type = "Master",
                                Land = "Master",
                                Status = "Master",

                                ProvincialDepartmentIdCreatedBy = userdata.ProvincialDepartmentId,
                                CreatedBy = userdata.Id,
                                RoleCreatedBy = userdata.Role_id,
                            };
                            _context.SubjectGroups.Add(SubjectGroupdata);
                            _context.SaveChanges();

                            var subjectdata = new SubjectCentralPolicyProvince
                            {
                                Name = model.Name,
                                CentralPolicyProvinceId = centralpolicyprovinceData.Id,
                                Type = "Master",
                                Status = model.Status,
                                SubjectGroupId = SubjectGroupdata.Id,
                                Explanation = model.Explanation,
                                CreatedAt = date,
                                CreatedBy = model.UserID,
                                UpdateAt = date
                            };
                            _context.SubjectCentralPolicyProvinces.Add(subjectdata);
                            _context.SaveChanges();

                            subjectid = subjectdata.Id;
                            GetSubjectID = subjectid;
                            //file

                            //old if n == 0 dup
                            //foreach (var id in model.CentralPolicyDateId)
                            //{
                            //    //System.Console.WriteLine("In3");
                            //    var CentralPolicyDatedata = _context.CentralPolicyDates
                            //        .Where(m => m.Id == id).FirstOrDefault();

                            //    var CentralPolicyDateProvincedata = new CentralPolicyDateProvince
                            //    {
                            //        StartDate = CentralPolicyDatedata.StartDate,
                            //        EndDate = CentralPolicyDatedata.EndDate
                            //    };
                            //    _context.CentralPolicyDateProvinces.Add(CentralPolicyDateProvincedata);
                            //    _context.SaveChanges();

                            //    var subjectdatedata = new SubjectDateCentralPolicyProvince
                            //    {
                            //        SubjectCentralPolicyProvinceId = subjectid,
                            //        CentralPolicyDateProvinceId = CentralPolicyDateProvincedata.Id,
                            //    };
                            //    _context.SubjectDateCentralPolicyProvinces.Add(subjectdatedata);
                            //}
                            //_context.SaveChanges();
                            // end dup

                        }

                        if (n == 0)
                        {
                            foreach (var id in model.CentralPolicyDateId)
                            {
                                //System.Console.WriteLine("In3");
                                var CentralPolicyDatedata = _context.CentralPolicyDates
                                    .Where(m => m.Id == id).FirstOrDefault();

                                var CentralPolicyDateProvincedata = new CentralPolicyDateProvince
                                {
                                    StartDate = CentralPolicyDatedata.StartDate,
                                    EndDate = CentralPolicyDatedata.EndDate
                                };
                                _context.CentralPolicyDateProvinces.Add(CentralPolicyDateProvincedata);
                                _context.SaveChanges();

                                var subjectdatedata = new SubjectDateCentralPolicyProvince
                                {
                                    SubjectCentralPolicyProvinceId = subjectid,
                                    CentralPolicyDateProvinceId = CentralPolicyDateProvincedata.Id,
                                };
                                _context.SubjectDateCentralPolicyProvinces.Add(subjectdatedata);
                            }
                            _context.SaveChanges();
                        }
                        if (box != departmentId.box)
                        {

                            foreach (var questionclose in departmentId.inputquestionclose)
                            {
                                var Subquestionclosedata = new SubquestionCentralPolicyProvince
                                {
                                    SubjectCentralPolicyProvinceId = subjectid,
                                    Name = questionclose.questionclose,
                                    Type = "คำถามปลายปิด",
                                    Box = departmentId.box,
                                };
                                _context.SubquestionCentralPolicyProvinces.Add(Subquestionclosedata);
                                _context.SaveChanges();

                                foreach (var box2 in model.inputsubjectdepartment)
                                {
                                    if (box2.box == departmentId.box)
                                    {
                                        var SubjectCentralPolicyProvinceGroupdata2 = new SubjectCentralPolicyProvinceGroup
                                        {
                                            ProvincialDepartmentId = box2.departmentId,
                                            SubquestionCentralPolicyProvinceId = Subquestionclosedata.Id,
                                        };
                                        _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata2);
                                        _context.SaveChanges();
                                    }
                                }
                                foreach (var questionclosechoice in questionclose.inputanswerclose)
                                {
                                    //var Subquestionchoiceclosedata = new SubquestionChoiceCentralPolicyProvince
                                    //{
                                    //    SubquestionCentralPolicyProvinceId = Subquestionclosedata.Id,
                                    //    Name = questionclosechoice.answerclose,
                                    //};
                                    //_context.SubquestionChoiceCentralPolicyProvinces.Add(Subquestionchoiceclosedata);
                                    //_context.SaveChanges();

                                    if (questionclosechoice.answerclose == null || questionclosechoice.answerclose == "")
                                    {
                                        var Subquestionchoiceclosedata = new SubquestionChoiceCentralPolicyProvince
                                        {
                                            SubquestionCentralPolicyProvinceId = Subquestionclosedata.Id,
                                            Name = "โปรดระบุ",
                                        };
                                        _context.SubquestionChoiceCentralPolicyProvinces.Add(Subquestionchoiceclosedata);
                                        _context.SaveChanges();
                                    }
                                    else
                                    {
                                        var Subquestionchoiceclosedata = new SubquestionChoiceCentralPolicyProvince
                                        {
                                            SubquestionCentralPolicyProvinceId = Subquestionclosedata.Id,
                                            Name = questionclosechoice.answerclose,
                                        };
                                        _context.SubquestionChoiceCentralPolicyProvinces.Add(Subquestionchoiceclosedata);
                                        _context.SaveChanges();
                                    }
                                }
                            }
                        }
                        box = departmentId.box;
                        n++;
                        //}
                    }
                    //else
                    //{
                    //    return Ok(new { upload = false });
                    //}
                }
                // return subjectdata;
            }

            //if (model.Status == "ใช้งานจริง")
            //{
            //    foreach (var departmentId in model.inputsubjectdepartment)
            //    {
            //        //System.Console.WriteLine("In1");
            //        var provincialdepartmentprovicedata = _context.ProvincialDepartmentProvince
            //            .Where(m => m.ProvincialDepartmentID == departmentId.departmentId)
            //            .Select(x => x.ProvinceId)
            //            .ToList();

            //        foreach (var provinceId in provincialdepartmentprovicedata)
            //        {

            //            System.Console.WriteLine("all" + provinceId);
            //            var centralpolicyprovinceData = _context.CentralPolicyProvinces
            //                    .Where(x => x.ProvinceId == provinceId && x.CentralPolicyId == model.CentralPolicyId)
            //                    .FirstOrDefault();

            //            //System.Console.WriteLine("have" + centralpolicyprovinceData.ProvinceId);

            //            if (centralpolicyprovinceData != null)
            //            {

            //                var subjectdata = new SubjectCentralPolicyProvince
            //                {
            //                    Name = model.Name,
            //                    CentralPolicyProvinceId = centralpolicyprovinceData.Id,
            //                    Type = "NoMaster",
            //                    Status = model.Status,
            //                    //Step = "หมอบหมายให้เขต",
            //                    //link = "https://localhost:5001/answersubject/outsider/"
            //                };
            //                _context.SubjectCentralPolicyProvinces.Add(subjectdata);
            //                _context.SaveChanges();


            //                termsList.Add(subjectdata.Id);
            //                //long test2 = subjectdata.Id;

            //                //GetSubjectID = test2;

            //                //var SubjectCentralPolicyProvinceGroupdata = new SubjectCentralPolicyProvinceGroup
            //                //{
            //                //    ProvincialDepartmentId = departmentId.departmentId,
            //                //    //SubjectCentralPolicyProvinceId = subjectdata.Id,
            //                //};
            //                //_context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata);
            //                //_context.SaveChanges();

            //                foreach (var id in model.CentralPolicyDateId)
            //                {
            //                    //System.Console.WriteLine("In3");
            //                    var CentralPolicyDatedata = _context.CentralPolicyDates
            //                        .Where(m => m.Id == id).FirstOrDefault();

            //                    var CentralPolicyDateProvincedata = new CentralPolicyDateProvince
            //                    {
            //                        StartDate = CentralPolicyDatedata.StartDate,
            //                        EndDate = CentralPolicyDatedata.EndDate
            //                    };
            //                    _context.CentralPolicyDateProvinces.Add(CentralPolicyDateProvincedata);
            //                    _context.SaveChanges();

            //                    var subjectdatedata = new SubjectDateCentralPolicyProvince
            //                    {
            //                        SubjectCentralPolicyProvinceId = subjectdata.Id,
            //                        CentralPolicyDateProvinceId = CentralPolicyDateProvincedata.Id,
            //                    };
            //                    _context.SubjectDateCentralPolicyProvinces.Add(subjectdatedata);
            //                }
            //                _context.SaveChanges();

            //                //var test = departmentId.inputquestionopen;
            //                //foreach (var data in model.inputsubjectdepartment)
            //                //{
            //                //    System.Console.WriteLine("TEST: " + data.inputquestionopen);
            //                //}

            //                //foreach (var questionopen in departmentId.inputquestionopen)
            //                //{
            //                //    System.Console.WriteLine("TEST: " + questionopen.questionopen);
            //                //    var Subquestionopendata = new SubquestionCentralPolicyProvince
            //                //    {
            //                //        SubjectCentralPolicyProvinceId = subjectdata.Id,
            //                //        Name = questionopen.questionopen,
            //                //        Type = "คำถามปลายเปิด"
            //                //    };
            //                //    _context.SubquestionCentralPolicyProvinces.Add(Subquestionopendata);
            //                //    _context.SaveChanges();

            //                //    var SubjectCentralPolicyProvinceGroupdata = new SubjectCentralPolicyProvinceGroup
            //                //    {
            //                //        ProvincialDepartmentId = departmentId.departmentId,
            //                //        SubquestionCentralPolicyProvinceId = Subquestionopendata.Id,
            //                //    };
            //                //    _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata);
            //                //    _context.SaveChanges();
            //                //}

            //                foreach (var questionclose in departmentId.inputquestionclose)
            //                {
            //                    var Subquestionclosedata = new SubquestionCentralPolicyProvince
            //                    {
            //                        SubjectCentralPolicyProvinceId = subjectdata.Id,
            //                        Name = questionclose.questionclose,
            //                        Type = "คำถามปลายปิด"
            //                    };
            //                    _context.SubquestionCentralPolicyProvinces.Add(Subquestionclosedata);
            //                    _context.SaveChanges();

            //                    var SubjectCentralPolicyProvinceGroupdata2 = new SubjectCentralPolicyProvinceGroup
            //                    {
            //                        ProvincialDepartmentId = departmentId.departmentId,
            //                        SubquestionCentralPolicyProvinceId = Subquestionclosedata.Id,
            //                    };
            //                    _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata2);
            //                    _context.SaveChanges();

            //                    foreach (var questionclosechoice in questionclose.inputanswerclose)
            //                    {
            //                        var Subquestionchoiceclosedata = new SubquestionChoiceCentralPolicyProvince
            //                        {
            //                            SubquestionCentralPolicyProvinceId = Subquestionclosedata.Id,
            //                            Name = questionclosechoice.answerclose,
            //                        };
            //                        _context.SubquestionChoiceCentralPolicyProvinces.Add(Subquestionchoiceclosedata);
            //                        _context.SaveChanges();
            //                    }
            //                }
            //            }
            //        }
            //        //return subjectdata;
            //    }

            //}
            return Ok(new { GetSubjectID, termsList });
        }
        // POST: api/
        [HttpPost("addfiles")]
        public async Task<IActionResult> Post5([FromForm] SubjectFileViewModel model)
        {


            System.Console.WriteLine("Start Upload");
            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                System.Console.WriteLine("in2");
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "//Uploads//";
            System.Console.WriteLine("Start Upload 2");

            foreach (var id in model.SubjectCentralPolicyProvinceId)
            {
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
                        System.Console.WriteLine("id" + id);
                        {
                            System.Console.WriteLine("Start Upload 4.1");
                            var SubjectFile = new SubjectCentralPolicyProvinceFile
                            {

                                SubjectCentralPolicyProvinceId = id,
                                Name = random + ext,
                                Description = Path.GetFileNameWithoutExtension(filePath2),
                            };

                            System.Console.WriteLine("Start Upload 4.2");
                            _context.SubjectCentralPolicyProvinceFiles.Add(SubjectFile);
                            _context.SaveChanges();

                            System.Console.WriteLine("Start Upload 4.3");


                            System.Console.WriteLine("Start Upload 5");
                        }
                    }
                }
            }
            return Ok(new { status = true });
        }

        [HttpPost("adddepartmentquestion")]
        public IActionResult Post6([FromBody] SubjectViewModel model)
        {
            long box = -1;
            System.Console.WriteLine("Start");
            foreach (var departmentId in model.inputsubjectdepartment)
            {
                if (box != departmentId.box)
                {
                    foreach (var questionclose in departmentId.inputquestionclose)
                    {
                        var Subquestionclosedata = new SubquestionCentralPolicyProvince
                        {
                            SubjectCentralPolicyProvinceId = departmentId.subjectid,
                            Name = questionclose.questionclose,
                            Type = "คำถามปลายปิด",
                            Box = departmentId.box,
                        };
                        _context.SubquestionCentralPolicyProvinces.Add(Subquestionclosedata);
                        _context.SaveChanges();

                        foreach (var box2 in model.inputsubjectdepartment)
                        {
                            if (box2.box == departmentId.box)
                            {
                                var SubjectCentralPolicyProvinceGroupdata2 = new SubjectCentralPolicyProvinceGroup
                                {
                                    ProvincialDepartmentId = box2.departmentId,
                                    SubquestionCentralPolicyProvinceId = Subquestionclosedata.Id,
                                };
                                _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata2);
                                _context.SaveChanges();
                            }
                        }
                        foreach (var questionclosechoice in questionclose.inputanswerclose)
                        {
                            if (questionclosechoice.answerclose == null || questionclosechoice.answerclose == "")
                            {
                                var Subquestionchoiceclosedata = new SubquestionChoiceCentralPolicyProvince
                                {
                                    SubquestionCentralPolicyProvinceId = Subquestionclosedata.Id,
                                    Name = "โปรดระบุ",
                                };
                                _context.SubquestionChoiceCentralPolicyProvinces.Add(Subquestionchoiceclosedata);
                                _context.SaveChanges();
                            }
                            else
                            {
                                var Subquestionchoiceclosedata = new SubquestionChoiceCentralPolicyProvince
                                {
                                    SubquestionCentralPolicyProvinceId = Subquestionclosedata.Id,
                                    Name = questionclosechoice.answerclose,
                                };
                                _context.SubquestionChoiceCentralPolicyProvinces.Add(Subquestionchoiceclosedata);
                                _context.SaveChanges();
                            }

                        }
                    }
                }
                box = departmentId.box;
            }
            return Ok(new { status = true });
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name)
        {
            var subjects = _context.Subjects.Find(id);
            subjects.Name = name;
            _context.Entry(subjects).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // PUT api/values/5
        [HttpPut("editsubject/{id}")]
        public void Put2([FromForm] SubjectViewModel model, long id)
        {
            var subjectDateId = _context.SubjectDates
                .Where(x => x.SubjectId == id)
                .ToList();




            var subjects = _context.Subjects.Find(id);
            subjects.Name = model.Name;
            _context.Entry(subjects).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            System.Console.WriteLine("in2");

            foreach (var sjId in subjectDateId)
            {
                var delId = _context.SubjectDates
                  .Where(x => x.Id == sjId.Id)
                  .ToList();

                foreach (var del in delId)
                {
                    _context.SubjectDates.Remove(del);
                }
                _context.SaveChanges();
            }


            System.Console.WriteLine("in3");


            foreach (var add in model.CentralPolicyDateId)
            {
                System.Console.WriteLine("idddd: " + add);
                System.Console.WriteLine("in4");
                var subjectdatedata = new SubjectDate
                {
                    SubjectId = id,
                    CentralPolicyDateId = add
                };
                _context.SubjectDates.Add(subjectdatedata);
            }
            _context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("editsubquestionopen/{id}")]
        public void Put3(long id, string name)
        {

            var subquestionopendata = _context.SubquestionCentralPolicyProvinces.Find(id);
            subquestionopendata.Name = name;
            _context.Entry(subquestionopendata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // PUT api/values/5
        [HttpPut("editchoices/{id}")]
        public void Put4(long id, string name)
        {

            var subquestionopendata = _context.SubquestionChoiceCentralPolicyProvinces.Find(id);
            subquestionopendata.Name = name;
            _context.Entry(subquestionopendata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var subjectdata = _context.SubjectCentralPolicyProvinces.Find(id);

            _context.SubjectCentralPolicyProvinces.Remove(subjectdata);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("deletesubquestionopen/{id}")]
        public void Delete2(long id)
        {
            var subquestionopendata = _context.SubquestionCentralPolicyProvinces.Find(id);

            _context.SubquestionCentralPolicyProvinces.Remove(subquestionopendata);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("deletechoices/{id}")]
        public void Delete3(long id)
        {
            var subquestionchoicesdata = _context.SubquestionChoiceCentralPolicyProvinces.Find(id);

            _context.SubquestionChoiceCentralPolicyProvinces.Remove(subquestionchoicesdata);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("deletefile/{id}")]
        public void Delete4(long id)
        {
            var filedata = _context.SubjectCentralPolicyProvinceFiles.Find(id);

            _context.SubjectCentralPolicyProvinceFiles.Remove(filedata);
            _context.SaveChanges();
        }

        [HttpPost("subjectprovince")]
        public object Post(long centralpolicyid, long provincevalue)
        {

            var centralpolicyprovince = _context.CentralPolicyProvinces
                .Where(m => m.ProvinceId == provincevalue)
                .Where(m => m.CentralPolicyId == centralpolicyid).FirstOrDefault();

            var Subjectdatas = _context.Subjects
            .Where(m => m.CentralPolicyId == centralpolicyid).ToList();

            var SubjectCentralPolicyProvincescheck = _context.SubjectCentralPolicyProvinces
            .Where(m => m.CentralPolicyProvinceId == centralpolicyprovince.Id).Count();

            if (SubjectCentralPolicyProvincescheck > 0)
            {
                return centralpolicyprovince.Id;
            }
            else
            {
                foreach (var Subjectdata in Subjectdatas)
                {
                    Console.WriteLine("TEST: " + Subjectdata.Name);
                    Console.WriteLine("TEST2: " + centralpolicyprovince.Id);
                    var SubjectCentralPolicyProvinceData = new SubjectCentralPolicyProvince
                    {
                        CentralPolicyProvinceId = centralpolicyprovince.Id,
                        Name = Subjectdata.Name
                    };
                    _context.SubjectCentralPolicyProvinces.Add(SubjectCentralPolicyProvinceData);
                    _context.SaveChanges();

                    Console.WriteLine("TEST3: " + Subjectdata.Id);

                    var subjectdatedatas = _context.SubjectDates
                        .Where(m => m.SubjectId == Subjectdata.Id).FirstOrDefault();

                    Console.WriteLine("TEST4: " + subjectdatedatas.CentralPolicyDateId);

                    var centralpolicydatedatas = _context.CentralPolicyDates
                      .Where(m => m.Id == subjectdatedatas.CentralPolicyDateId).ToList();

                    //Console.WriteLine("TEST5: " + centralpolicydatedatas.Id);
                    Console.WriteLine("TEST6: " + SubjectCentralPolicyProvinceData.Id);

                    foreach (var centralpolicydatedata in centralpolicydatedatas)
                    {
                        var CentralPolicyDateProvincedatas = new CentralPolicyDateProvince
                        {
                            StartDate = centralpolicydatedata.StartDate,
                            EndDate = centralpolicydatedata.EndDate
                        };
                        _context.CentralPolicyDateProvinces.Add(CentralPolicyDateProvincedatas);
                        _context.SaveChanges();

                        var subjectdatecentralpolicyprovincedata = new SubjectDateCentralPolicyProvince
                        {
                            SubjectCentralPolicyProvinceId = SubjectCentralPolicyProvinceData.Id,
                            CentralPolicyDateProvinceId = CentralPolicyDateProvincedatas.Id
                        };
                        _context.SubjectDateCentralPolicyProvinces.Add(subjectdatecentralpolicyprovincedata);
                        _context.SaveChanges();
                    }

                    var Subquestiondatas = _context.Subquestions
                    .Where(m => m.SubjectId == Subjectdata.Id).ToList();

                    foreach (var Subquestiondata in Subquestiondatas)
                    {
                        var SubquestionCentralPolicyProvincedata = new SubquestionCentralPolicyProvince
                        {
                            SubjectCentralPolicyProvinceId = SubjectCentralPolicyProvinceData.Id,
                            Name = Subquestiondata.Name,
                            Type = Subquestiondata.Type
                        };
                        _context.SubquestionCentralPolicyProvinces.Add(SubquestionCentralPolicyProvincedata);
                        _context.SaveChanges();

                        var Subquestiondatachoices = _context.SubquestionChoices
                        .Where(m => m.SubquestionId == Subquestiondata.Id).ToList();

                        foreach (var Subquestiondatachoice in Subquestiondatachoices)
                        {
                            var SubquestionChoiceCentralPolicyProvincedata = new SubquestionChoiceCentralPolicyProvince
                            {
                                SubquestionCentralPolicyProvinceId = SubquestionCentralPolicyProvincedata.Id,
                                Name = Subquestiondatachoice.Name,
                            };
                            _context.SubquestionChoiceCentralPolicyProvinces.Add(SubquestionChoiceCentralPolicyProvincedata);
                            _context.SaveChanges();
                        }
                    }
                }
                _context.SaveChanges();
                return centralpolicyprovince.Id;
            }
        }

        // PUT api/values/5
        [HttpPut("editsunquestionprovince/{id}")]
        public void PutSubquestionProvince(long id, string name)
        {
            var Subquestiondata = _context.SubquestionCentralPolicyProvinces.Find(id);
            Subquestiondata.Name = name;

            _context.Entry(Subquestiondata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // PUT api/values/5
        [HttpPut("editsunquestionchoiceprovince/{id}")]
        public void PutSubquestionchoiceProvince(long id, string name)
        {
            var Subquestionchoicedata = _context.SubquestionChoiceCentralPolicyProvinces.Find(id);
            Subquestionchoicedata.Name = name;

            _context.Entry(Subquestionchoicedata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        [HttpPut("editsubjectchoiceprovince/{id}")]
        public void PutSubjectchoiceProvince(long id, string name)
        {
            System.Console.WriteLine("NAME: " + name);
            var Subjectchoicedata = _context.SubjectCentralPolicyProvinces.Find(id);
            Subjectchoicedata.Name = name;

            _context.Entry(Subjectchoicedata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        [HttpPut("editsubjectquestionopenchoiceprovince/{id}")]
        public void PutSubjectquestionopenchoiceProvince(long id, string name)
        {
            var SubjectQuestionOpenchoicedata = _context.SubquestionCentralPolicyProvinces.Find(id);
            SubjectQuestionOpenchoicedata.Name = name;

            _context.Entry(SubjectQuestionOpenchoicedata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        [HttpPut("editAnswer/{id}")]
        public void PutAnswer(long id, string answer)
        {
            var answerData = _context.AnswerSubquestions.Find(id);
            answerData.Answer = answer;

            _context.Entry(answerData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("deleteprovincial/{id}")]
        public void DeleteProvincial(long id)
        {
            var subjectcentralpolicyprovincegroup = _context.SubjectCentralPolicyProvinceGroups.Find(id);

            _context.SubjectCentralPolicyProvinceGroups.Remove(subjectcentralpolicyprovincegroup);
            _context.SaveChanges();
        }
        // DELETE api/values/5
        [HttpDelete("deletepeopleanswer/{id}")]
        public void deletepeopleanswer(long id)
        {
            var subjectcentralpolicyprovincegroup = _context.SubjectCentralPolicyProvinceUserGroups.Find(id);

            _context.SubjectCentralPolicyProvinceUserGroups.Remove(subjectcentralpolicyprovincegroup);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("deletesubjectrole3/{id}")]
        public void Deletesubjectrole3(long id)
        {
            var subjectcentralpolicyprovincegroup = _context.SubjectCentralPolicyProvinces.Find(id);


            _context.SubjectCentralPolicyProvinces.Remove(subjectcentralpolicyprovincegroup);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("deletequestionrole3/{id}")]
        public void Deletequestrole3(long id)
        {
            //var subs = _context.SubquestionCentralPolicyProvinces.Where(x => x.Id == id).First();

            //var SubjectCentralPolicyProvinceGroupdata = _context.SubjectCentralPolicyProvinceGroups
            //    .Where(x => x.SubquestionCentralPolicyProvinceId == subs.Id)
            //    .ToList();

            //foreach(var SubjectCentralPolicyProvinceGroupdataloop in SubjectCentralPolicyProvinceGroupdata)
            //{
            //    var subjectcentralpolicyprovincegroup2 = _context.SubjectCentralPolicyProvinceGroups
            //        .Find(SubjectCentralPolicyProvinceGroupdataloop.Id);

            //    _context.SubjectCentralPolicyProvinceGroups.Remove(subjectcentralpolicyprovincegroup2);
            //    _context.SaveChanges();
            //}

            var subjectcentralpolicyprovincegroup = _context.SubquestionCentralPolicyProvinces.Find(id);

            _context.SubquestionCentralPolicyProvinces.Remove(subjectcentralpolicyprovincegroup);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("deleteoptionrole3/{id}")]
        public void Deleteoptionrole3(long id)
        {
            var subjectcentralpolicyprovincegroup = _context.SubquestionChoiceCentralPolicyProvinces.Find(id);

            _context.SubquestionChoiceCentralPolicyProvinces.Remove(subjectcentralpolicyprovincegroup);
            _context.SaveChanges();
        }
        // DELETE api/values/5
        [HttpDelete("delet/{id}")]
        public void DeleteSubjectDate(long[] id)
        {
            foreach (var iddata in id)
            {
                var subjectdatedata = _context.SubjectDateCentralPolicyProvinces.Find(iddata);

                _context.SubjectDateCentralPolicyProvinces.Remove(subjectdatedata);
                _context.SaveChanges();
            }
        }
        // POST api/values
        [HttpPost("deletedate")]
        public void Delete(long[] id, long[] CentralPolicyDateId, long subjectid)
        {

            System.Console.WriteLine("CentralPolicyDateId" + CentralPolicyDateId);
            System.Console.WriteLine("login1");
            if (id != null)
            {
                foreach (var iddata in id)
                {
                    System.Console.WriteLine("login2");
                    System.Console.WriteLine("id" + iddata);
                    var subjectdatedata = _context.SubjectDateCentralPolicyProvinces.Find(iddata);

                    _context.SubjectDateCentralPolicyProvinces.Remove(subjectdatedata);
                    _context.SaveChanges();
                }
            }

            System.Console.WriteLine("login2.2");

            foreach (var CentralPolicyDateIdata in CentralPolicyDateId)
            {
                System.Console.WriteLine("login3");
                System.Console.WriteLine("CentralPolicyDateId" + CentralPolicyDateId);
                var CentralPolicyDatedata = _context.CentralPolicyDates
                    .Where(m => m.Id == CentralPolicyDateIdata).FirstOrDefault();
                System.Console.WriteLine("login4");
                var CentralPolicyDateProvincedata = new CentralPolicyDateProvince
                {
                    StartDate = CentralPolicyDatedata.StartDate,
                    EndDate = CentralPolicyDatedata.EndDate
                };
                _context.CentralPolicyDateProvinces.Add(CentralPolicyDateProvincedata);
                _context.SaveChanges();
                System.Console.WriteLine("login5");
                var subjectdatedata = new SubjectDateCentralPolicyProvince
                {
                    SubjectCentralPolicyProvinceId = subjectid,
                    CentralPolicyDateProvinceId = CentralPolicyDateProvincedata.Id,
                };
                System.Console.WriteLine("login6");
                _context.SubjectDateCentralPolicyProvinces.Add(subjectdatedata);
            }
            _context.SaveChanges();
        }
        [HttpPost("addsubjectrole3")]
        public IActionResult Post5(long SubjectCentralPolicyProvinceId, string Name, long Box, string[] answerclose, long[] DepartmentId)
        {
            var subjectdata = _context.SubjectCentralPolicyProvinces
                .Where(m => m.Id == SubjectCentralPolicyProvinceId).FirstOrDefault();

            var subquestiondata = _context.SubquestionCentralPolicyProvinces
                .Where(m => m.SubjectCentralPolicyProvinceId == SubjectCentralPolicyProvinceId).FirstOrDefault();

            var SubjectCentralPolicyProvince = new SubjectCentralPolicyProvince
            {
                CentralPolicyProvinceId = subjectdata.CentralPolicyProvinceId,
                SubjectGroupId = subjectdata.SubjectGroupId,
                Name = subjectdata.Name,
                Type = subjectdata.Type,
                Status = subjectdata.Status,
                Explanation = subjectdata.Explanation,
                CreatedBy = subjectdata.CreatedBy,
            };
            _context.SubjectCentralPolicyProvinces.Add(SubjectCentralPolicyProvince);
            _context.SaveChanges();

            var Subquestionopendata = new SubquestionCentralPolicyProvince
            {
                SubjectCentralPolicyProvinceId = SubjectCentralPolicyProvince.Id,
                Name = Name,
                Type = "คำถามปลายปิด",
                Box = Box,
            };
            System.Console.WriteLine("in");
            _context.SubquestionCentralPolicyProvinces.Add(Subquestionopendata);
            _context.SaveChanges();

            foreach (var DepartmentIddata in DepartmentId)
            {
                var SubjectCentralPolicyProvinceGroup = new SubjectCentralPolicyProvinceGroup
                {
                    SubquestionCentralPolicyProvinceId = Subquestionopendata.Id,
                    ProvincialDepartmentId = DepartmentIddata
                };
                _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroup);
                _context.SaveChanges();
            }

            foreach (var answerclosedata in answerclose)
            {
                System.Console.WriteLine("in3");
                var Subquestionchoiceclosedata = new SubquestionChoiceCentralPolicyProvince
                {
                    SubquestionCentralPolicyProvinceId = Subquestionopendata.Id,
                    Name = answerclosedata

                };
                _context.SubquestionChoiceCentralPolicyProvinces.Add(Subquestionchoiceclosedata);
                _context.SaveChanges();
            }

            return Ok();
        }

        // POST api/values
        [HttpPost("subjectevent")]
        public IActionResult PostSubjectEvent([FromBody] subjectevent model)
        {
            var userdata = _context.Users
             .Where(m => m.Id == model.CreatedBy)
             //.Select(m => m.Role_id)
             .FirstOrDefault();

            System.Console.WriteLine("in");
            //System.Console.WriteLine("StartProvinceId: " + ProvinceId);
            var date = DateTime.Now;
            var inspectionplanevent = new InspectionPlanEvent
            {
                StartDate = model.startdate,
                EndDate = model.enddate,
                ProvinceId = model.ProvinceId,
                CreatedAt = date,
                CreatedBy = model.CreatedBy,
                Status = "ร่างกำหนดการ",
                RoleCreatedBy = userdata.Role_id.ToString(),
                ProvincialDepartmentIdCreatedBy = userdata.ProvincialDepartmentId,
            };
            _context.InspectionPlanEvents.Add(inspectionplanevent);
            _context.SaveChanges();
            System.Console.WriteLine("in2");
            foreach (var cenid in model.CentralpolicyId)
            {
                var SubjectGroupdata = new SubjectGroup
                {
                    CentralPolicyId = cenid,
                    ProvinceId = model.ProvinceId,
                    Type = "NoMaster",
                    Status = "ร่างกำหนดการ",
                    Land = model.Land,
                    StartDate = model.startdate,
                    EndDate = model.enddate,

                    ProvincialDepartmentIdCreatedBy = userdata.ProvincialDepartmentId,
                    CreatedBy = userdata.Id,
                    RoleCreatedBy = userdata.Role_id,
                };
                _context.SubjectGroups.Add(SubjectGroupdata);
                _context.SaveChanges();

                var CentralPolicyEventsdata = new CentralPolicyEvent
                {
                    CentralPolicyId = cenid,
                    InspectionPlanEventId = inspectionplanevent.Id,
                    StartDate = model.startdate,
                    EndDate = model.enddate,
                    HaveSubject = 1,
                };
                _context.CentralPolicyEvents.Add(CentralPolicyEventsdata);
                _context.SaveChanges();

                var SubjectGroupPeopleQuestiondata = new SubjectGroupPeopleQuestion
                {
                    SubjectGroupId = SubjectGroupdata.Id,
                    CentralPolicyEventId = CentralPolicyEventsdata.Id,
                };
                _context.SubjectGroupPeopleQuestions.Add(SubjectGroupPeopleQuestiondata);
                _context.SaveChanges();

                var subjectcen = _context.SubjectCentralPolicyProvinces
                    .Where(m => m.CentralPolicyProvince.CentralPolicyId == cenid)
                    .Where(m => m.Type == "Master")
                    .Where(m => m.Status == "ใช้งานจริง")
                    .ToList();

                foreach (var subcen in subjectcen)
                {
                    var cenpro = _context.CentralPolicyProvinces
                        .Where(m => m.CentralPolicyId == cenid && m.ProvinceId == model.ProvinceId).FirstOrDefault();

                    var subques = _context.SubquestionCentralPolicyProvinces
                 .Where(m => m.SubjectCentralPolicyProvinceId == subcen.Id)
                 .Include(w => w.SubjectCentralPolicyProvinceGroups)
                 .ThenInclude(m => m.ProvincialDepartment)
                 .ToList();

                    List<ViewModel.SubjectProvinceCl> subjectQuestList = new List<ViewModel.SubjectProvinceCl>();
                    for (int i = 0; i < subques.Count(); i++)
                    {
                        foreach (var iteminw in subques[i].SubjectCentralPolicyProvinceGroups)
                        {
                            subjectQuestList.Add(new SubjectProvinceCl
                            {
                                Id = subques[i].Id,
                                SubquestionCentralPolicyProvinceId = iteminw.SubquestionCentralPolicyProvinceId,
                                ProvincialDepartmentId = iteminw.ProvincialDepartmentId,
                                Name = subques[i].Name,
                                Box = subques[i].Box,
                                Type = subques[i].Type

                            });
                        }
                    }

                    // You can convert it back to an array if you would like to
                    IOrderedEnumerable<SubjectProvinceCl> orderedEnumerables = subjectQuestList.ToList().OrderBy(d => d.ProvincialDepartmentId);
                    ViewModel.SubjectProvinceCl[] terms = orderedEnumerables.ToArray();
                    //SubjectCentralPolicyProvince SubjectCentralPolicyProvincedata ;


                    //SubjectCentralPolicyProvince SubjectCentralPolicyProvincedata ;
                    long departId = 0;
                    long subjectId = 0;
                    foreach (var subque in terms)
                    {
                        // var subqueObj = _context.
                        // var checkdepartsdata = _context.SubjectCentralPolicyProvinceGroups
                        //     .Where(m => m.SubquestionCentralPolicyProvinceId == subque.Id)
                        //     //.OrderBy(m => m.ProvincialDepartmentId)
                        //     .Select(x => x.ProvincialDepartmentId)
                        //     .ToList();

                        long checkdeparts = subque.ProvincialDepartmentId;
                        //departId = checkdeparts;
                        //foreach (var checkdepart in checkdeparts)
                        // foreach (var checkdeparts in checkdepartsdata)
                        // {

                        SubjectCentralPolicyProvince test;
                        //foreach (var checkdepart in checkdeparts)
                        //{
                        var SubjectCentralPolicyProvincedata = new SubjectCentralPolicyProvince
                        {
                            Name = subcen.Name,
                            CentralPolicyProvinceId = cenpro.Id,
                            Type = "NoMaster",
                            Status = "ใช้งานจริง",
                            SubjectGroupId = SubjectGroupdata.Id,
                            CreatedBy = subcen.CreatedBy,
                            Explanation = subcen.Explanation
                        };
                        //subjectId = SubjectCentralPolicyProvincedata.Id;
                        //departId = checkdepart;
                        Console.WriteLine("subque1" + subque.Id);
                        Console.WriteLine("subque1" + subjectId);

                        if (departId != checkdeparts)
                        {
                            Console.WriteLine("subque1" + departId + ":" + checkdeparts);
                            departId = checkdeparts;
                            var checkprovince = _context.ProvincialDepartmentProvince
                           .Where(m => m.ProvincialDepartmentID == departId && m.ProvinceId == model.ProvinceId).FirstOrDefault();


                            if (checkprovince != null)
                            {
                                _context.SubjectCentralPolicyProvinces.Add(SubjectCentralPolicyProvincedata);
                                _context.SaveChanges();
                                test = SubjectCentralPolicyProvincedata;
                                subjectId = SubjectCentralPolicyProvincedata.Id;
                                //SubjectCentralPolicyProvincedata = SubjectCentralPolicyProvincedata;

                                NewTestFunction(subque, departId, subjectId);
                            }
                        }
                        else
                        {
                            departId = checkdeparts;
                            var checkprovince = _context.ProvincialDepartmentProvince
                           .Where(m => m.ProvincialDepartmentID == checkdeparts && m.ProvinceId == model.ProvinceId).FirstOrDefault();

                            if (checkprovince != null)
                            {

                                //subjectId = SubjectCentralPolicyProvincedata.Id;
                                Console.WriteLine("subque2", subque);
                                NewTestFunction(subque, checkdeparts, subjectId);
                            }
                            //TestFunction(SubjectCentralPolicyProvincedata, subque, departId, subjectId);
                        }

                        //}
                        // }
                    }
                }
            }
            return Ok();
        }
        public void TestFunction(SubquestionCentralPolicyProvince subque, long checkdepart, long subjectId)


        {
            var SubquestionCentralPolicyProvincedata = new SubquestionCentralPolicyProvince
            {
                SubjectCentralPolicyProvinceId = subjectId,
                Name = subque.Name,
                Type = subque.Type,
                Box = subque.Box,
            };
            _context.SubquestionCentralPolicyProvinces.Add(SubquestionCentralPolicyProvincedata);
            _context.SaveChanges();

            var SubjectCentralPolicyProvinceGroupdata2 = new SubjectCentralPolicyProvinceGroup
            {
                ProvincialDepartmentId = checkdepart,
                SubquestionCentralPolicyProvinceId = SubquestionCentralPolicyProvincedata.Id,
            };
            _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata2);
            _context.SaveChanges();

            var subqueschoices = _context.SubquestionChoiceCentralPolicyProvinces
                .Where(x => x.SubquestionCentralPolicyProvinceId == subque.Id).ToList();

            foreach (var subqueschoice in subqueschoices)
            {
                var SubquestionChoiceCentralPolicyProvincedata = new SubquestionChoiceCentralPolicyProvince
                {
                    SubquestionCentralPolicyProvinceId = SubquestionCentralPolicyProvincedata.Id,
                    Name = subqueschoice.Name,
                };
                _context.SubquestionChoiceCentralPolicyProvinces.Add(SubquestionChoiceCentralPolicyProvincedata);
                _context.SaveChanges();
            }
        }

        public void NewTestFunction(ViewModel.SubjectProvinceCl subque, long checkdepart, long subjectId)


        {
            var SubquestionCentralPolicyProvincedata = new SubquestionCentralPolicyProvince
            {
                SubjectCentralPolicyProvinceId = subjectId,
                Name = subque.Name,
                Type = subque.Type,
                Box = subque.Box,
            };
            _context.SubquestionCentralPolicyProvinces.Add(SubquestionCentralPolicyProvincedata);
            _context.SaveChanges();

            var SubjectCentralPolicyProvinceGroupdata2 = new SubjectCentralPolicyProvinceGroup
            {
                ProvincialDepartmentId = checkdepart,
                SubquestionCentralPolicyProvinceId = SubquestionCentralPolicyProvincedata.Id,
            };
            _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata2);
            _context.SaveChanges();

            var subqueschoices = _context.SubquestionChoiceCentralPolicyProvinces
                .Where(x => x.SubquestionCentralPolicyProvinceId == subque.Id).ToList();

            foreach (var subqueschoice in subqueschoices)
            {
                var SubquestionChoiceCentralPolicyProvincedata = new SubquestionChoiceCentralPolicyProvince
                {
                    SubquestionCentralPolicyProvinceId = SubquestionCentralPolicyProvincedata.Id,
                    Name = subqueschoice.Name,
                };
                _context.SubquestionChoiceCentralPolicyProvinces.Add(SubquestionChoiceCentralPolicyProvincedata);
                _context.SaveChanges();
            }
        }

        // GET api/values/5
        [HttpGet("getevent/{id}")]
        public IActionResult Get3(string id)
        {
            var userprovince = _context.UserProvinces
                       .Where(m => m.UserID == id)
                       .ToList();

            var user = _context.Users
                           .Where(m => m.Id == id)
                           .FirstOrDefault();

            if (user.Role_id == 3)
            {
                //var inspectionplans = _context.InspectionPlanEvents
                //                    .Include(m => m.Province)
                //                    .Include(m => m.CentralPolicyEvents)
                //                    .ThenInclude(m => m.CentralPolicy)
                //                    .ThenInclude(m => m.CentralPolicyProvinces)
                //                    .Where(m => m.RoleCreatedBy == "3")
                //                    .ToList();

                var subjectgroupsdatas = _context.SubjectGroups
                        .Include(m => m.Province)
                        .Include(m => m.CentralPolicy)
                        .ThenInclude(m => m.FiscalYearNew)
                        .OrderByDescending(m => m.Id)
                        //.Include(m => m.SubjectCentralPolicyProvinces)
                        //.Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.Type != "Master"))
                        .Where(p => p.RoleCreatedBy == 3)
                        .Where(m => m.Type == "NoMaster").ToList();

                List<object> termsList = new List<object>();
                foreach (var inspectionplan in subjectgroupsdatas)
                {
                    for (int i = 0; i < userprovince.Count(); i++)
                    {
                        if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                            termsList.Add(inspectionplan);
                    }
                }
                return Ok(termsList);
            }
            else if (user.Role_id == 6)
            {
                //var inspectionplans = _context.InspectionPlanEvents
                //                    .Include(m => m.Province)
                //                    .Include(m => m.CentralPolicyEvents)
                //                    .ThenInclude(m => m.CentralPolicy)
                //                    .ThenInclude(m => m.CentralPolicyProvinces)
                //                    .Where(m => m.RoleCreatedBy == "3")
                //                    .ToList();

                var subjectgroupsdatas = _context.SubjectGroups
                        .Include(m => m.Province)
                        .Include(m => m.CentralPolicy)
                        .ThenInclude(m => m.FiscalYearNew)
                        .OrderByDescending(m => m.Id)
                        //.Include(m => m.SubjectCentralPolicyProvinces)
                        //.Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.Type != "Master"))
                        .Where(p => p.RoleCreatedBy == 6)
                        .Where(m => m.Type == "NoMaster").ToList();

                List<object> termsList = new List<object>();
                foreach (var inspectionplan in subjectgroupsdatas)
                {
                    for (int i = 0; i < userprovince.Count(); i++)
                    {
                        if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                            termsList.Add(inspectionplan);
                    }
                }
                return Ok(termsList);
            }
            else if (user.Role_id == 10)
            {
                //var inspectionplans = _context.InspectionPlanEvents
                //                    .Include(m => m.Province)
                //                    .Include(m => m.CentralPolicyEvents)
                //                    .ThenInclude(m => m.CentralPolicy)
                //                    .ThenInclude(m => m.CentralPolicyProvinces)
                //                    .Where(m => m.RoleCreatedBy == "3")
                //                    .ToList();

                var subjectgroupsdatas = _context.SubjectGroups
                        .Include(m => m.Province)
                        .Include(m => m.CentralPolicy)
                        .ThenInclude(m => m.FiscalYearNew)
                        .OrderByDescending(m => m.Id)
                        //.Include(m => m.SubjectCentralPolicyProvinces)
                        //.Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.Type != "Master"))
                        .Where(p => p.RoleCreatedBy == 10)
                        .Where(m => m.Type == "NoMaster").ToList();

                List<object> termsList = new List<object>();
                foreach (var inspectionplan in subjectgroupsdatas)
                {
                    for (int i = 0; i < userprovince.Count(); i++)
                    {
                        if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                            termsList.Add(inspectionplan);
                    }
                }
                return Ok(termsList);
            }
            return Ok("nothing");
            //var subjectgroupsdata = _context.SubjectGroups
            //    .Include(m => m.Province)
            //    .Include(m => m.CentralPolicy)
            //    .ThenInclude(m => m.FiscalYear)
            //    .Include(m => m.SubjectCentralPolicyProvinces)
            //    .Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.Type != "Master")).ToList();

            //var subjectgroupsdata = _context.SubjectGroups
            //        .Include(m => m.Province)
            //        .Include(m => m.CentralPolicy)
            //        .ThenInclude(m => m.FiscalYear)
            //        .Include(m => m.SubjectCentralPolicyProvinces)
            //        //.Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.Type != "Master"))
            //        .Where(m => m.Type == "NoMaster").ToList();

            //return Ok(subjectgroupsdata);
        }

        // GET api/values/5
        [HttpGet("geteventfromcalendar/{id}")]
        public IActionResult Geteventfromcalendar(long id)
        {
            System.Console.WriteLine("DDDDD");
            var CentralPolicyEvents = _context.CentralPolicyEvents
                .Include(m => m.InspectionPlanEvent)
                .ThenInclude(p => p.Province)
                .Include(m => m.CentralPolicy)
                .Where(m => m.InspectionPlanEvent.ProvinceId == id)
                .ToList();

            var subjectgroupsdatas = _context.SubjectGroups
                .Where(m => m.ProvinceId == id)
                .Where(m => m.Type == "NoMaster").ToList();

            return Ok(new { CentralPolicyEvents, subjectgroupsdatas });

        }

        // POST api/values
        [HttpPost("subjecteventnoland")]
        public IActionResult PostSubjectEventNoLand([FromBody] subjectevent model)
        {
            var userdata = _context.Users
               .Where(m => m.Id == model.CreatedBy)
               //.Select(m => m.Role_id)
               .FirstOrDefault();

            var date = DateTime.Now;

            System.Console.WriteLine("in2");
            foreach (var cenid in model.CentralpolicyId)
            {
                var SubjectGroupdata = new SubjectGroup
                {
                    CentralPolicyId = cenid,
                    ProvinceId = model.ProvinceId,
                    Type = "NoMaster",
                    Status = "ร่างกำหนดการ",
                    Land = model.Land,

                    ProvincialDepartmentIdCreatedBy = userdata.ProvincialDepartmentId,
                    CreatedBy = userdata.Id,
                    RoleCreatedBy = userdata.Role_id,
                };
                _context.SubjectGroups.Add(SubjectGroupdata);
                _context.SaveChanges();

                var subjectcen = _context.SubjectCentralPolicyProvinces
                    .Where(m => m.CentralPolicyProvince.CentralPolicyId == cenid)
                    .Where(m => m.Type == "Master")
                    .Where(m => m.Status == "ใช้งานจริง")
                    .ToList();

                foreach (var subcen in subjectcen)
                {
                    var cenpro = _context.CentralPolicyProvinces
                        .Where(m => m.CentralPolicyId == cenid && m.ProvinceId == model.ProvinceId).FirstOrDefault();

                    var subques = _context.SubquestionCentralPolicyProvinces
                        .Where(m => m.SubjectCentralPolicyProvinceId == subcen.Id)
                        .Include(w => w.SubjectCentralPolicyProvinceGroups)
                        .ThenInclude(m => m.ProvincialDepartment)
                        .ToList();
                    // .Where(m => m.SubjectCentralPolicyProvinceId == subcen.Id).ToList();



                    List<ViewModel.SubjectProvinceCl> subjectQuestList = new List<ViewModel.SubjectProvinceCl>();
                    for (int i = 0; i < subques.Count(); i++)
                    {
                        foreach (var iteminw in subques[i].SubjectCentralPolicyProvinceGroups)
                        {
                            subjectQuestList.Add(new SubjectProvinceCl
                            {
                                Id = subques[i].Id,
                                SubquestionCentralPolicyProvinceId = iteminw.SubquestionCentralPolicyProvinceId,
                                ProvincialDepartmentId = iteminw.ProvincialDepartmentId,
                                Name = subques[i].Name,
                                Box = subques[i].Box,
                                Type = subques[i].Type

                            });
                        }
                    }

                    // You can convert it back to an array if you would like to
                    IOrderedEnumerable<SubjectProvinceCl> orderedEnumerables = subjectQuestList.ToList().OrderBy(d => d.ProvincialDepartmentId);
                    ViewModel.SubjectProvinceCl[] terms = orderedEnumerables.ToArray();
                    //SubjectCentralPolicyProvince SubjectCentralPolicyProvincedata ;
                    long departId = 0;
                    long subjectId = 0;
                    foreach (var subque in terms)
                    {
                        // var subqueObj = _context.
                        // var checkdepartsdata = _context.SubjectCentralPolicyProvinceGroups
                        //     .Where(m => m.SubquestionCentralPolicyProvinceId == subque.Id)
                        //     //.OrderBy(m => m.ProvincialDepartmentId)
                        //     .Select(x => x.ProvincialDepartmentId)
                        //     .ToList();

                        long checkdeparts = subque.ProvincialDepartmentId;

                        Console.WriteLine("subque1" + departId);
                        //departId = checkdeparts;
                        //foreach (var checkdepart in checkdeparts)
                        // foreach (var checkdeparts in checkdepartsdata)
                        // {

                        SubjectCentralPolicyProvince test;
                        //foreach (var checkdepart in checkdeparts)
                        //{
                        var SubjectCentralPolicyProvincedata = new SubjectCentralPolicyProvince
                        {
                            Name = subcen.Name,
                            CentralPolicyProvinceId = cenpro.Id,
                            Type = "NoMaster",
                            Status = "ใช้งานจริง",
                            SubjectGroupId = SubjectGroupdata.Id,
                            CreatedBy = subcen.CreatedBy,
                            Explanation = subcen.Explanation
                        };
                        //subjectId = SubjectCentralPolicyProvincedata.Id;
                        //departId = checkdepart;
                        Console.WriteLine("subque1" + subque.Id);
                        Console.WriteLine("subque1" + subjectId);

                        if (departId != checkdeparts)
                        {
                            Console.WriteLine("subque1" + departId + ":" + checkdeparts);
                            departId = checkdeparts;
                            var checkprovince = _context.ProvincialDepartmentProvince
                           .Where(m => m.ProvincialDepartmentID == departId && m.ProvinceId == model.ProvinceId).ToList();


                            if (checkprovince.Count() != 0)
                            {
                                _context.SubjectCentralPolicyProvinces.Add(SubjectCentralPolicyProvincedata);
                                _context.SaveChanges();
                                test = SubjectCentralPolicyProvincedata;
                                subjectId = SubjectCentralPolicyProvincedata.Id;
                                //SubjectCentralPolicyProvincedata = SubjectCentralPolicyProvincedata;

                                NewTestFunction(subque, departId, subjectId);
                            }
                        }
                        else
                        {

                            departId = checkdeparts;
                            var checkprovince = _context.ProvincialDepartmentProvince
                           .Where(m => m.ProvincialDepartmentID == checkdeparts && m.ProvinceId == model.ProvinceId).ToList();

                            if (checkprovince.Count() != 0)
                            {
                                var SubjectCentralPolicyProvincedata_else = new SubjectCentralPolicyProvince
                                {
                                    Name = subcen.Name,
                                    CentralPolicyProvinceId = cenpro.Id,
                                    Type = "NoMaster",
                                    Status = "ใช้งานจริง",
                                    SubjectGroupId = SubjectGroupdata.Id,
                                    CreatedBy = subcen.CreatedBy,
                                    Explanation = subcen.Explanation
                                };
                                //subjectId = SubjectCentralPolicyProvincedata.Id;
                                Console.WriteLine("subque2", subque);
                                NewTestFunction(subque, checkdeparts, subjectId);
                            }
                            //TestFunction(SubjectCentralPolicyProvincedata, subque, departId, subjectId);
                        }

                        //}
                        // }
                    }
                }
            }
            return Ok();
        }

        // POST api/values
        [HttpPost("postsubjecteventfromcalendar")]
        public IActionResult Postsubjecteventfromcalendar([FromBody] subjectevent model)
        {
            var userdata = _context.Users
               .Where(m => m.Id == model.CreatedBy)
               //.Select(m => m.Role_id)
               .FirstOrDefault();
            //foreach (var id in model.CentralpolicySelect.centralPolicyeventId) {
            //    //var province = _context.CentralPolicyEvents.Find(model.CentralpolicyId2);
            //    //province.HaveSubject = 1;
            //    //_context.Entry(province).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //    //_context.SaveChanges();
            //}

            foreach (var cenid in model.CentralpolicySelect)
            {
                var centralpolicyeventdate = _context.CentralPolicyEvents
                    //.Where(m => m.CentralPolicyId == cenid.centralpolicyId)
                    //.Where(m => m.InspectionPlanEvent.ProvinceId == model.ProvinceId)
                    .Where(p => p.Id == cenid.centralPolicyeventId)
                    .FirstOrDefault();

                var SubjectGroupdata = new SubjectGroup
                {
                    CentralPolicyId = cenid.centralpolicyId,
                    ProvinceId = model.ProvinceId,
                    Type = "NoMaster",
                    Status = "ร่างกำหนดการ",
                    Land = model.Land,
                    StartDate = centralpolicyeventdate.StartDate,
                    EndDate = centralpolicyeventdate.EndDate,

                    ProvincialDepartmentIdCreatedBy = userdata.ProvincialDepartmentId,
                    CreatedBy = userdata.Id,
                    RoleCreatedBy = userdata.Role_id,
                };
                _context.SubjectGroups.Add(SubjectGroupdata);
                _context.SaveChanges();

                var SubjectGroupPeopleQuestiondata = new SubjectGroupPeopleQuestion
                {
                    SubjectGroupId = SubjectGroupdata.Id,
                    CentralPolicyEventId = centralpolicyeventdate.Id,
                };
                _context.SubjectGroupPeopleQuestions.Add(SubjectGroupPeopleQuestiondata);
                _context.SaveChanges();

                var subjectcen = _context.SubjectCentralPolicyProvinces
                    .Where(m => m.CentralPolicyProvince.CentralPolicyId == cenid.centralpolicyId)
                    .Where(m => m.Type == "Master")
                    .Where(m => m.Status == "ใช้งานจริง")
                    .ToList();

                foreach (var subcen in subjectcen)
                {
                    var cenpro = _context.CentralPolicyProvinces
                        .Where(m => m.CentralPolicyId == cenid.centralpolicyId && m.ProvinceId == model.ProvinceId).FirstOrDefault();

                    var subques = _context.SubquestionCentralPolicyProvinces
                      .Where(m => m.SubjectCentralPolicyProvinceId == subcen.Id)
                      .Include(w => w.SubjectCentralPolicyProvinceGroups)
                      .ThenInclude(m => m.ProvincialDepartment)
                      .ToList();

                    List<ViewModel.SubjectProvinceCl> subjectQuestList = new List<ViewModel.SubjectProvinceCl>();
                    for (int i = 0; i < subques.Count(); i++)
                    {
                        foreach (var iteminw in subques[i].SubjectCentralPolicyProvinceGroups)
                        {
                            subjectQuestList.Add(new SubjectProvinceCl
                            {
                                Id = subques[i].Id,
                                SubquestionCentralPolicyProvinceId = iteminw.SubquestionCentralPolicyProvinceId,
                                ProvincialDepartmentId = iteminw.ProvincialDepartmentId,
                                Name = subques[i].Name,
                                Box = subques[i].Box,
                                Type = subques[i].Type

                            });
                        }
                    }

                    // You can convert it back to an array if you would like to
                    IOrderedEnumerable<SubjectProvinceCl> orderedEnumerables = subjectQuestList.ToList().OrderBy(d => d.ProvincialDepartmentId);
                    ViewModel.SubjectProvinceCl[] terms = orderedEnumerables.ToArray();

                    var centralpolicydata = _context.CentralPolicies
                        .Where(m => m.Id == cenid.centralpolicyId).FirstOrDefault();

                    System.Console.WriteLine("before if");
                    if (centralpolicydata.Class == "แผนการตรวจ")
                    {
                        System.Console.WriteLine("in if");
                        var SubjectCentralPolicyProvincedata2 = new SubjectCentralPolicyProvince
                        {
                            Name = subcen.Name,
                            CentralPolicyProvinceId = cenpro.Id,
                            Type = "NoMaster",
                            Status = "ใช้งานจริง",
                            SubjectGroupId = SubjectGroupdata.Id,
                            CreatedBy = subcen.CreatedBy,
                            Explanation = subcen.Explanation
                        };
                        _context.SubjectCentralPolicyProvinces.Add(SubjectCentralPolicyProvincedata2);
                        _context.SaveChanges();
                    }

                    //SubjectCentralPolicyProvince SubjectCentralPolicyProvincedata;
                    long departId = 0;
                    long subjectId = 0;
                    foreach (var subque in terms)
                    {
                        // var subqueObj = _context.
                        // var checkdepartsdata = _context.SubjectCentralPolicyProvinceGroups
                        //     .Where(m => m.SubquestionCentralPolicyProvinceId == subque.Id)
                        //     //.OrderBy(m => m.ProvincialDepartmentId)
                        //     .Select(x => x.ProvincialDepartmentId)
                        //     .ToList();

                        long checkdeparts = subque.ProvincialDepartmentId;
                        Console.WriteLine("subque1" + departId);
                        //departId = checkdeparts;
                        //foreach (var checkdepart in checkdeparts)
                        // foreach (var checkdeparts in checkdepartsdata)
                        // {

                        SubjectCentralPolicyProvince test;
                        //foreach (var checkdepart in checkdeparts)
                        //{
                        var SubjectCentralPolicyProvincedata = new SubjectCentralPolicyProvince
                        {
                            Name = subcen.Name,
                            CentralPolicyProvinceId = cenpro.Id,
                            Type = "NoMaster",
                            Status = "ใช้งานจริง",
                            SubjectGroupId = SubjectGroupdata.Id,
                            CreatedBy = subcen.CreatedBy,
                            Explanation = subcen.Explanation
                        };
                        //subjectId = SubjectCentralPolicyProvincedata.Id;
                        //departId = checkdepart;
                        Console.WriteLine("subque1" + subque.Id);
                        Console.WriteLine("subque1" + subjectId);

                        if (departId != checkdeparts)
                        {
                            Console.WriteLine("subque1" + departId + ":" + checkdeparts);
                            departId = checkdeparts;
                            var checkprovince = _context.ProvincialDepartmentProvince
                           .Where(m => m.ProvincialDepartmentID == departId && m.ProvinceId == model.ProvinceId).FirstOrDefault();


                            if (checkprovince != null)
                            {
                                _context.SubjectCentralPolicyProvinces.Add(SubjectCentralPolicyProvincedata);
                                _context.SaveChanges();
                                test = SubjectCentralPolicyProvincedata;
                                subjectId = SubjectCentralPolicyProvincedata.Id;
                                //SubjectCentralPolicyProvincedata = SubjectCentralPolicyProvincedata;

                                NewTestFunction(subque, departId, subjectId);
                            }
                        }
                        else
                        {
                            departId = checkdeparts;
                            var checkprovince = _context.ProvincialDepartmentProvince
                           .Where(m => m.ProvincialDepartmentID == checkdeparts && m.ProvinceId == model.ProvinceId).FirstOrDefault();

                            if (checkprovince != null)
                            {

                                //subjectId = SubjectCentralPolicyProvincedata.Id;
                                Console.WriteLine("subque2", subque);
                                NewTestFunction(subque, checkdeparts, subjectId);
                            }
                            //TestFunction(SubjectCentralPolicyProvincedata, subque, departId, subjectId);
                        }

                        //}
                        // }
                    }
                }

                var province = _context.CentralPolicyEvents.Find(cenid.centralPolicyeventId);
                province.HaveSubject = 1;
                _context.Entry(province).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            return Ok();
        }

        // POST api/values
        [HttpPost("postsubjecteventOther")]
        public IActionResult PostsubjecteventOther([FromBody] subjectevent model)
        {
            //foreach (var id in model.CentralpolicySelect.centralPolicyeventId) {
            //    //var province = _context.CentralPolicyEvents.Find(model.CentralpolicyId2);
            //    //province.HaveSubject = 1;
            //    //_context.Entry(province).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //    //_context.SaveChanges();
            //}
            var userdata = _context.Users
         .Where(m => m.Id == model.CreatedBy)
         //.Select(m => m.Role_id)
         .FirstOrDefault();

            var year = _context.FiscalYearNew
                .Where(m => m.Year == DateTime.Now.Year + 543).FirstOrDefault();
            System.Console.WriteLine("year" + year.Id);
            //if(year == null)
            //{

            //}
            //var test = model.UserID;
            //System.Console.WriteLine(test);
            System.Console.WriteLine("111");
            System.Console.WriteLine("TITLE: " + model.Title);
            var date = DateTime.Now;
            var centralpolicydata1 = new CentralPolicy
            {
                Title = model.Title,
                TypeexaminationplanId = 3,
                FiscalYearNewId = year.Id,
                Status = "ใช้งานจริง",
                CreatedAt = date,
                CreatedBy = model.CreatedBy,
                Class = "ประเด็นการตรวจติดตาม",
            };
            System.Console.WriteLine("3");
            _context.CentralPolicies.Add(centralpolicydata1);
            _context.SaveChanges();
            System.Console.WriteLine("4");
            //foreach (var id in model.ProvinceId)
            //{
            var centralpolicyprovincedata = new CentralPolicyProvince
            {
                ProvinceId = model.ProvinceId,
                CentralPolicyId = centralpolicydata1.Id,
                Step = "มอบหมายหน่วยงาน",
                Status = "ร่างกำหนดการ"
            };
            _context.CentralPolicyProvinces.Add(centralpolicyprovincedata);
            _context.SaveChanges();

            var centralpolicyeventdate = _context.CentralPolicyEvents
                .Where(m => m.CentralPolicyId == centralpolicydata1.Id)
                .Where(m => m.InspectionPlanEvent.ProvinceId == model.ProvinceId)
                .FirstOrDefault();

            var SubjectGroupdata = new SubjectGroup
            {
                CentralPolicyId = centralpolicydata1.Id,
                ProvinceId = model.ProvinceId,
                Type = "NoMaster",
                Status = "ร่างกำหนดการ",
                Land = "ไม่ลงพื้นที่",

                ProvincialDepartmentIdCreatedBy = userdata.ProvincialDepartmentId,
                CreatedBy = userdata.Id,
                RoleCreatedBy = userdata.Role_id,
            };
            _context.SubjectGroups.Add(SubjectGroupdata);
            _context.SaveChanges();

            var subjectdata = new SubjectCentralPolicyProvince
            {
                Name = model.Title,
                CentralPolicyProvinceId = centralpolicyprovincedata.Id,
                Type = "NoMaster",
                Status = "ใช้งานจริง",
                SubjectGroupId = SubjectGroupdata.Id,
                CreatedBy = model.CreatedBy,
            };
            _context.SubjectCentralPolicyProvinces.Add(subjectdata);
            _context.SaveChanges();

            var subjectcen = _context.SubjectCentralPolicyProvinces
                .Where(m => m.CentralPolicyProvince.CentralPolicyId == centralpolicydata1.Id)
                .Where(m => m.Type == "Master")
                .Where(m => m.Status == "ใช้งานจริง")
                .ToList();

            foreach (var subcen in subjectcen)
            {
                var cenpro = _context.CentralPolicyProvinces
                    .Where(m => m.CentralPolicyId == centralpolicydata1.Id && m.ProvinceId == model.ProvinceId).FirstOrDefault();

                var subques = _context.SubquestionCentralPolicyProvinces
                    .Where(m => m.SubjectCentralPolicyProvinceId == subcen.Id).ToList();


                var centralpolicydata = _context.CentralPolicies
                    .Where(m => m.Id == centralpolicydata1.Id).FirstOrDefault();

                System.Console.WriteLine("before if");
                if (centralpolicydata.Class == "แผนการตรวจ")
                {
                    System.Console.WriteLine("in if");
                    var SubjectCentralPolicyProvincedata2 = new SubjectCentralPolicyProvince
                    {
                        Name = subcen.Name,
                        CentralPolicyProvinceId = cenpro.Id,
                        Type = "NoMaster",
                        Status = "ใช้งานจริง",
                        SubjectGroupId = SubjectGroupdata.Id,
                    };
                    _context.SubjectCentralPolicyProvinces.Add(SubjectCentralPolicyProvincedata2);
                    _context.SaveChanges();
                }

                //SubjectCentralPolicyProvince SubjectCentralPolicyProvincedata;


                //var province = _context.CentralPolicyEvents.Find(centralpolicydata1.Id);
                //province.HaveSubject = 1;
                //_context.Entry(province).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //_context.SaveChanges();
            }
            return Ok(new { Status = true });
        }

        // POST api/values
        [HttpPost("postsubjecteventOtherland")]
        public IActionResult PostsubjecteventOtherland([FromBody] subjectevent model)
        {
            var userdata = _context.Users
         .Where(m => m.Id == model.CreatedBy)
         .FirstOrDefault();


            var year = _context.FiscalYearNew
                .Where(m => m.Year == DateTime.Now.Year + 543).FirstOrDefault();
            System.Console.WriteLine("year" + year.Id);
            System.Console.WriteLine("111");
            System.Console.WriteLine("TITLE: " + model.Title);
            var date = DateTime.Now;

            var inspectionplanevent = new InspectionPlanEvent
            {
                StartDate = model.startdate,
                EndDate = model.enddate,
                ProvinceId = model.ProvinceId,
                CreatedAt = date,
                CreatedBy = model.CreatedBy,
                Status = "ร่างกำหนดการ",
                RoleCreatedBy = userdata.Role_id.ToString(),
                ProvincialDepartmentIdCreatedBy = userdata.ProvincialDepartmentId,
            };
            _context.InspectionPlanEvents.Add(inspectionplanevent);
            _context.SaveChanges();

            var centralpolicydata1 = new CentralPolicy
            {
                Title = model.Title,
                // Type = "อื่นๆ", comment ไว้ก่อนมันพัง
                TypeexaminationplanId = 3,
                FiscalYearNewId = year.Id,
                Status = "ใช้งานจริง",
                CreatedAt = date,
                CreatedBy = model.CreatedBy,
                Class = "ประเด็นการตรวจติดตาม",
            };
            System.Console.WriteLine("3");
            _context.CentralPolicies.Add(centralpolicydata1);
            _context.SaveChanges();
            System.Console.WriteLine("4");

            var CentralPolicyEventsdata = new CentralPolicyEvent
            {
                CentralPolicyId = centralpolicydata1.Id,
                InspectionPlanEventId = inspectionplanevent.Id,
                StartDate = model.startdate,
                EndDate = model.enddate,
                HaveSubject = 1,
            };
            _context.CentralPolicyEvents.Add(CentralPolicyEventsdata);
            _context.SaveChanges();

            var centralpolicyprovincedata = new CentralPolicyProvince
            {
                ProvinceId = model.ProvinceId,
                CentralPolicyId = centralpolicydata1.Id,
                Step = "มอบหมายหน่วยงาน",
                Status = "ร่างกำหนดการ"
            };
            _context.CentralPolicyProvinces.Add(centralpolicyprovincedata);
            _context.SaveChanges();

            //var centralpolicyeventdate = _context.CentralPolicyEvents
            //    .Where(m => m.CentralPolicyId == centralpolicydata1.Id)
            //    .Where(m => m.InspectionPlanEvent.ProvinceId == model.ProvinceId)
            //    .FirstOrDefault();

            var SubjectGroupdata = new SubjectGroup
            {
                CentralPolicyId = centralpolicydata1.Id,
                ProvinceId = model.ProvinceId,
                Type = "NoMaster",
                Status = "ร่างกำหนดการ",

                Land = model.Land,
                StartDate = model.startdate,
                EndDate = model.enddate,

                ProvincialDepartmentIdCreatedBy = userdata.ProvincialDepartmentId,
                CreatedBy = userdata.Id,
                RoleCreatedBy = userdata.Role_id,
            };
            _context.SubjectGroups.Add(SubjectGroupdata);
            _context.SaveChanges();

            var SubjectGroupPeopleQuestiondata = new SubjectGroupPeopleQuestion
            {
                SubjectGroupId = SubjectGroupdata.Id,
                CentralPolicyEventId = CentralPolicyEventsdata.Id,
            };
            _context.SubjectGroupPeopleQuestions.Add(SubjectGroupPeopleQuestiondata);
            _context.SaveChanges();

            var subjectdata = new SubjectCentralPolicyProvince
            {
                Name = model.Title,
                CentralPolicyProvinceId = centralpolicyprovincedata.Id,
                Type = "NoMaster",
                Status = "ใช้งานจริง",
                SubjectGroupId = SubjectGroupdata.Id,
                CreatedBy = model.CreatedBy,
            };
            _context.SubjectCentralPolicyProvinces.Add(subjectdata);
            _context.SaveChanges();

            var subjectcen = _context.SubjectCentralPolicyProvinces
                .Where(m => m.CentralPolicyProvince.CentralPolicyId == centralpolicydata1.Id)
                .Where(m => m.Type == "Master")
                .Where(m => m.Status == "ใช้งานจริง")
                .ToList();

            foreach (var subcen in subjectcen)
            {
                var cenpro = _context.CentralPolicyProvinces
                    .Where(m => m.CentralPolicyId == centralpolicydata1.Id && m.ProvinceId == model.ProvinceId).FirstOrDefault();

                var subques = _context.SubquestionCentralPolicyProvinces
                    .Where(m => m.SubjectCentralPolicyProvinceId == subcen.Id).ToList();


                var centralpolicydata = _context.CentralPolicies
                    .Where(m => m.Id == centralpolicydata1.Id).FirstOrDefault();

                System.Console.WriteLine("before if");
                if (centralpolicydata.Class == "แผนการตรวจ")
                {
                    System.Console.WriteLine("in if");
                    var SubjectCentralPolicyProvincedata2 = new SubjectCentralPolicyProvince
                    {
                        Name = subcen.Name,
                        CentralPolicyProvinceId = cenpro.Id,
                        Type = "NoMaster",
                        Status = "ใช้งานจริง",
                        SubjectGroupId = SubjectGroupdata.Id,
                    };
                    _context.SubjectCentralPolicyProvinces.Add(SubjectCentralPolicyProvincedata2);
                    _context.SaveChanges();
                }
            }
            return Ok(new { Status = true });
        }

        // PUT api/values/5
        [HttpPut("editsubject2/{id}")]
        public void Put3([FromForm] SubjectViewModel model, long id)
        {
            var date = DateTime.Now;
            var subjects = _context.SubjectCentralPolicyProvinces.Find(id);
            subjects.Name = model.Name;
            subjects.Status = model.Status;
            subjects.Explanation = model.Explanation;
            subjects.UpdateAt = date;
            _context.Entry(subjects).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }
        // DELETE api/values/5
        [HttpDelete("delsubjecteventnoland/{id}")]
        public void Delsubjecteventnoland(long id)
        {
            var subjectgroupdata = _context.SubjectGroups.Where(p => p.Id == id).FirstOrDefault();


            var SubjectCentralPolicyProvincesdatas = _context.SubjectCentralPolicyProvinces
                .Where(p => p.SubjectGroupId == id).ToList();

            foreach (var SubjectCentralPolicyProvincesdata in SubjectCentralPolicyProvincesdatas)
            {
                var delsubjectCentralPolicyProvincesdata = _context.SubjectCentralPolicyProvinces.Find(SubjectCentralPolicyProvincesdata.Id);
                _context.SubjectCentralPolicyProvinces.Remove(delsubjectCentralPolicyProvincesdata);
                _context.SaveChanges();
            }

            var subjectdata = _context.SubjectGroups.Find(id);
            _context.SubjectGroups.Remove(subjectdata);
            _context.SaveChanges();
        }
        // GET api/values/5
        [HttpGet("geteventprovice/{id}/{provinceid}")]
        public IActionResult Geteventprovice(string id, long provinceid)
        {
            var userprovince = _context.UserProvinces
                       .Where(m => m.UserID == id)
                       .ToList();

            var user = _context.Users
                           .Where(m => m.Id == id)
                           .FirstOrDefault();

            if (user.Role_id == 3)
            {

                var subjectgroupsdatas = _context.SubjectGroups
                        .Include(m => m.Province)
                        .Include(m => m.CentralPolicy)
                        .ThenInclude(m => m.FiscalYearNew)
                        .OrderByDescending(m => m.Id)
                        //.Include(m => m.SubjectCentralPolicyProvinces)
                        //.Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.Type != "Master"))
                        .Where(p => p.RoleCreatedBy == 3)
                        .Where(m => m.ProvinceId == provinceid)
                        .Where(m => m.Type == "NoMaster").ToList();

                List<object> termsList = new List<object>();
                foreach (var inspectionplan in subjectgroupsdatas)
                {
                    for (int i = 0; i < userprovince.Count(); i++)
                    {
                        if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                            termsList.Add(inspectionplan);
                    }
                }
                return Ok(termsList);
            }
            else if (user.Role_id == 6)
            {

                var subjectgroupsdatas = _context.SubjectGroups
                        .Include(m => m.Province)
                        .Include(m => m.CentralPolicy)
                        .ThenInclude(m => m.FiscalYearNew)
                        .OrderByDescending(m => m.Id)
                        //.Include(m => m.SubjectCentralPolicyProvinces)
                        //.Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.Type != "Master"))
                        .Where(p => p.RoleCreatedBy == 6)
                        .Where(m => m.ProvinceId == provinceid)
                        .Where(m => m.Type == "NoMaster").ToList();

                List<object> termsList = new List<object>();
                foreach (var inspectionplan in subjectgroupsdatas)
                {
                    for (int i = 0; i < userprovince.Count(); i++)
                    {
                        if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                            termsList.Add(inspectionplan);
                    }
                }
                return Ok(termsList);
            }
            else if (user.Role_id == 10)
            {


                var subjectgroupsdatas = _context.SubjectGroups
                        .Include(m => m.Province)
                        .Include(m => m.CentralPolicy)
                        .ThenInclude(m => m.FiscalYearNew)
                        .OrderByDescending(m => m.Id)
                        //.Include(m => m.SubjectCentralPolicyProvinces)
                        //.Where(m => m.SubjectCentralPolicyProvinces.Any(m => m.Type != "Master"))
                        .Where(p => p.RoleCreatedBy == 10)
                        .Where(m => m.ProvinceId == provinceid)
                        .Where(m => m.Type == "NoMaster").ToList();

                List<object> termsList = new List<object>();
                foreach (var inspectionplan in subjectgroupsdatas)
                {
                    for (int i = 0; i < userprovince.Count(); i++)
                    {
                        if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                            termsList.Add(inspectionplan);
                    }
                }
                return Ok(termsList);
            }
            return Ok("nothing");

        }
    }
}