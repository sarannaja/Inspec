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
                            var subjectdata = new SubjectCentralPolicyProvince
                            {
                                Name = model.Name,
                                CentralPolicyProvinceId = centralpolicyprovinceData.Id,
                                Type = "Master",
                                Status = model.Status
                            };
                            _context.SubjectCentralPolicyProvinces.Add(subjectdata);
                            _context.SaveChanges();

                            subjectid = subjectdata.Id;
                            GetSubjectID = subjectid;
                            //file



                        }

                        //var SubjectCentralPolicyProvinceGroupdata = new SubjectCentralPolicyProvinceGroup
                        //{
                        //    ProvincialDepartmentId = departmentId.departmentId,
                        //    //SubjectCentralPolicyProvinceId = subjectid,
                        //};
                        //_context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata);
                        //_context.SaveChanges();

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
                        var test = departmentId.inputquestionopen;
                        foreach (var data in model.inputsubjectdepartment)
                        {
                            System.Console.WriteLine("TEST: " + data.inputquestionopen);
                        }


                        //long boxcheck = departmentId.box;

                        if (box != departmentId.box)
                        {
                            foreach (var questionopen in departmentId.inputquestionopen)
                            {
                                System.Console.WriteLine("TEST: " + questionopen.questionopen);
                                var Subquestionopendata = new SubquestionCentralPolicyProvince
                                {
                                    SubjectCentralPolicyProvinceId = subjectid,
                                    Name = questionopen.questionopen,
                                    Type = "คำถามปลายเปิด",
                                    Box = departmentId.box
                                };
                                _context.SubquestionCentralPolicyProvinces.Add(Subquestionopendata);
                                _context.SaveChanges();

                                foreach (var box2 in model.inputsubjectdepartment)
                                {
                                    if (box2.box == departmentId.box)
                                    {
                                        var SubjectCentralPolicyProvinceGroupdata = new SubjectCentralPolicyProvinceGroup
                                        {
                                            ProvincialDepartmentId = box2.departmentId,
                                            SubquestionCentralPolicyProvinceId = Subquestionopendata.Id,
                                        };
                                        _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata);
                                        _context.SaveChanges();
                                    }
                                }
                            }

                            foreach (var questionclose in departmentId.inputquestionclose)
                            {
                                var Subquestionclosedata = new SubquestionCentralPolicyProvince
                                {
                                    SubjectCentralPolicyProvinceId = subjectid,
                                    Name = questionclose.questionclose,
                                    Type = "คำถามปลายปิด",
                                    Box = departmentId.box
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
                        box = departmentId.box;
                        n++;
                        //}
                    }
                }
                // return subjectdata;
            }

            if (model.Status == "ใช้งานจริง")
            {
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

                            var subjectdata = new SubjectCentralPolicyProvince
                            {
                                Name = model.Name,
                                CentralPolicyProvinceId = centralpolicyprovinceData.Id,
                                Type = "NoMaster",
                                Status = model.Status,
                                //Step = "หมอบหมายให้เขต",
                                //link = "https://localhost:5001/answersubject/outsider/"
                            };
                            _context.SubjectCentralPolicyProvinces.Add(subjectdata);
                            _context.SaveChanges();


                            termsList.Add(subjectdata.Id);
                            //long test2 = subjectdata.Id;

                            //GetSubjectID = test2;

                            //var SubjectCentralPolicyProvinceGroupdata = new SubjectCentralPolicyProvinceGroup
                            //{
                            //    ProvincialDepartmentId = departmentId.departmentId,
                            //    //SubjectCentralPolicyProvinceId = subjectdata.Id,
                            //};
                            //_context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata);
                            //_context.SaveChanges();

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
                                    SubjectCentralPolicyProvinceId = subjectdata.Id,
                                    CentralPolicyDateProvinceId = CentralPolicyDateProvincedata.Id,
                                };
                                _context.SubjectDateCentralPolicyProvinces.Add(subjectdatedata);
                            }
                            _context.SaveChanges();

                            var test = departmentId.inputquestionopen;
                            foreach (var data in model.inputsubjectdepartment)
                            {
                                System.Console.WriteLine("TEST: " + data.inputquestionopen);
                            }

                            foreach (var questionopen in departmentId.inputquestionopen)
                            {
                                System.Console.WriteLine("TEST: " + questionopen.questionopen);
                                var Subquestionopendata = new SubquestionCentralPolicyProvince
                                {
                                    SubjectCentralPolicyProvinceId = subjectdata.Id,
                                    Name = questionopen.questionopen,
                                    Type = "คำถามปลายเปิด"
                                };
                                _context.SubquestionCentralPolicyProvinces.Add(Subquestionopendata);
                                _context.SaveChanges();

                                var SubjectCentralPolicyProvinceGroupdata = new SubjectCentralPolicyProvinceGroup
                                {
                                    ProvincialDepartmentId = departmentId.departmentId,
                                    SubquestionCentralPolicyProvinceId = Subquestionopendata.Id,
                                };
                                _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata);
                                _context.SaveChanges();
                            }

                            foreach (var questionclose in departmentId.inputquestionclose)
                            {
                                var Subquestionclosedata = new SubquestionCentralPolicyProvince
                                {
                                    SubjectCentralPolicyProvinceId = subjectdata.Id,
                                    Name = questionclose.questionclose,
                                    Type = "คำถามปลายปิด"
                                };
                                _context.SubquestionCentralPolicyProvinces.Add(Subquestionclosedata);
                                _context.SaveChanges();

                                var SubjectCentralPolicyProvinceGroupdata2 = new SubjectCentralPolicyProvinceGroup
                                {
                                    ProvincialDepartmentId = departmentId.departmentId,
                                    SubquestionCentralPolicyProvinceId = Subquestionclosedata.Id,
                                };
                                _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata2);
                                _context.SaveChanges();

                                foreach (var questionclosechoice in questionclose.inputanswerclose)
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
                    //return subjectdata;
                }

            }
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
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                    }
                    System.Console.WriteLine("Start Upload 4.1");
                    {
                        System.Console.WriteLine("Start Upload 4.1");
                        var SubjectFile = new SubjectCentralPolicyProvinceFile
                        {

                            SubjectCentralPolicyProvinceId = id,
                            Name = random + filename,
                        };

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.SubjectCentralPolicyProvinceFiles.Add(SubjectFile);
                        _context.SaveChanges();

                        System.Console.WriteLine("Start Upload 4.3");


                        System.Console.WriteLine("Start Upload 5");
                    }
                }


            }
            return Ok(new { status = true });
        }

        [HttpPost("adddepartmentquestion")]
        public IActionResult Post6([FromBody] SubjectViewModel model)
        {
            System.Console.WriteLine("Start");
            foreach (var departmentId in model.inputsubjectdepartment)
            {
                System.Console.WriteLine("in1");
                foreach (var questionopen in departmentId.inputquestionopen)
                {
                    System.Console.WriteLine("in2");
                    System.Console.WriteLine("TEST: " + questionopen.questionopen);
                    var Subquestionopendata = new SubquestionCentralPolicyProvince
                    {
                        SubjectCentralPolicyProvinceId = departmentId.subjectid,
                        Name = questionopen.questionopen,
                        Type = "คำถามปลายเปิด",
                        Box = departmentId.box
                    };
                    _context.SubquestionCentralPolicyProvinces.Add(Subquestionopendata);
                    _context.SaveChanges();

                    foreach (var box2 in model.inputsubjectdepartment)
                    {
                        if (box2.box == departmentId.box)
                        {
                            var SubjectCentralPolicyProvinceGroupdata = new SubjectCentralPolicyProvinceGroup
                            {
                                ProvincialDepartmentId = box2.departmentId,
                                SubquestionCentralPolicyProvinceId = Subquestionopendata.Id,
                            };
                            _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata);
                            _context.SaveChanges();
                        }
                    }
                }

                foreach (var questionclose in departmentId.inputquestionclose)
                {
                    var Subquestionclosedata = new SubquestionCentralPolicyProvince
                    {
                        SubjectCentralPolicyProvinceId = departmentId.subjectid,
                        Name = questionclose.questionclose,
                        Type = "คำถามปลายปิด",
                        Box = departmentId.box
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
        public IActionResult Post5([FromBody] SubjectViewModel model)
        {

            long GetSubjectID = 0;
            List<object> termsList = new List<object>();

            long subjectid = 0;
            var n = 0;
            long box = -1;

            foreach (var departmentId in model.inputsubjectdepartment)
            {
                var provincialdepartmentprovicedata = _context.ProvincialDepartmentProvince
                    .Where(m => m.ProvincialDepartmentID == departmentId.departmentId)
                    .Select(x => x.ProvinceId)
                    .ToList();

                foreach (var provinceId in provincialdepartmentprovicedata)
                {

                    System.Console.WriteLine("all" + provinceId);
                    var centralpolicyprovinceData = _context.CentralPolicyProvinces
                            //.Where(x => x.ProvinceId == provinceId && x.CentralPolicyId == model.CentralPolicyId)
                            .Where(x => x.Id == model.CentralPolicyId && x.ProvinceId == provinceId)
                            .FirstOrDefault();

                    if (centralpolicyprovinceData != null)
                    {
                        System.Console.WriteLine("have" + centralpolicyprovinceData.ProvinceId);

                        if (n == 0)
                        {
                            var subjectdata = new SubjectCentralPolicyProvince
                            {
                                Name = model.Name,
                                CentralPolicyProvinceId = centralpolicyprovinceData.Id,
                                Type = "NoMaster",
                                Status = model.Status
                            };
                            _context.SubjectCentralPolicyProvinces.Add(subjectdata);
                            _context.SaveChanges();

                            subjectid = subjectdata.Id;
                            GetSubjectID = subjectid;

                        }

                        //if (n == 0)
                        //{
                        //    foreach (var id in model.CentralPolicyDateId)
                        //    {
                        //        var CentralPolicyDatedata = _context.CentralPolicyDates
                        //            .Where(m => m.Id == id).FirstOrDefault();

                        //        var CentralPolicyDateProvincedata = new CentralPolicyDateProvince
                        //        {
                        //            StartDate = CentralPolicyDatedata.StartDate,
                        //            EndDate = CentralPolicyDatedata.EndDate
                        //        };
                        //        _context.CentralPolicyDateProvinces.Add(CentralPolicyDateProvincedata);
                        //        _context.SaveChanges();

                        //        var subjectdatedata = new SubjectDateCentralPolicyProvince
                        //        {
                        //            SubjectCentralPolicyProvinceId = subjectid,
                        //            CentralPolicyDateProvinceId = CentralPolicyDateProvincedata.Id,
                        //        };
                        //        _context.SubjectDateCentralPolicyProvinces.Add(subjectdatedata);
                        //    }
                        //    _context.SaveChanges();
                        //}

                        var test = departmentId.inputquestionopen;
                        foreach (var data in model.inputsubjectdepartment)
                        {
                            System.Console.WriteLine("TEST: " + data.inputquestionopen);
                        }

                        if (box != departmentId.box)
                        {
                            foreach (var questionopen in departmentId.inputquestionopen)
                            {
                                System.Console.WriteLine("TEST: " + questionopen.questionopen);
                                var Subquestionopendata = new SubquestionCentralPolicyProvince
                                {
                                    SubjectCentralPolicyProvinceId = subjectid,
                                    Name = questionopen.questionopen,
                                    Type = "คำถามปลายเปิด",
                                    Box = departmentId.box
                                };
                                _context.SubquestionCentralPolicyProvinces.Add(Subquestionopendata);
                                _context.SaveChanges();

                                foreach (var box2 in model.inputsubjectdepartment)
                                {
                                    if (box2.box == departmentId.box)
                                    {
                                        var SubjectCentralPolicyProvinceGroupdata = new SubjectCentralPolicyProvinceGroup
                                        {
                                            ProvincialDepartmentId = box2.departmentId,
                                            SubquestionCentralPolicyProvinceId = Subquestionopendata.Id,
                                        };
                                        _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata);
                                        _context.SaveChanges();
                                    }
                                }
                            }

                            foreach (var questionclose in departmentId.inputquestionclose)
                            {
                                var Subquestionclosedata = new SubquestionCentralPolicyProvince
                                {
                                    SubjectCentralPolicyProvinceId = subjectid,
                                    Name = questionclose.questionclose,
                                    Type = "คำถามปลายปิด",
                                    Box = departmentId.box
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
                        box = departmentId.box;
                        n++;
                    }
                }
            }
            return Ok(new { GetSubjectID, termsList });
        }
    }
}