using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Subject> Get()
        {
            var subjectdata = from P in _context.Subjects
                              select P;
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
            var subjectdata = _context.Subjects
                .Include(m => m.SubjectDates)
                .ThenInclude(m => m.CentralPolicyDate)
                .Where(m => m.CentralPolicyId == id);

            return Ok(subjectdata);
        }

        [HttpGet("subjectdetail/{id}")]
        public IActionResult Get2(long id)
        {
            var subjectdata = _context.Subjects
                .Include(m => m.SubjectDates)
                .ThenInclude(m => m.CentralPolicyDate)
                .Include(m => m.Subquestions)
                .ThenInclude(m => m.SubquestionChoices)
                .Where(m => m.Id == id)
                .First();

            return Ok(subjectdata);
        }

        // POST api/values
        [HttpPost("addsubquestionopen")]
        public Subquestion Post2(long subjectId, string name)
        {
            System.Console.WriteLine("subjectId"+ subjectId);

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
        public SubquestionChoice Post4(long subquestionid, string name)
        {
            //System.Console.WriteLine("subjectId" + subjectId);

            var subquestionchoicedata = new SubquestionChoice
            {
              SubquestionId = subquestionid,
              Name = name

            };

            _context.SubquestionChoices.Add(subquestionchoicedata);
            _context.SaveChanges();

            return subquestionchoicedata;
        }

        // POST api/values
        [HttpPost]
        public Subject Post([FromBody] SubjectViewModel model)
        {
            var subjectdata = new Subject
            {
                Name = model.Name,
                CentralPolicyId = model.CentralPolicyId,
                Answer = model.Answer,

            };
            _context.Subjects.Add(subjectdata);
            _context.SaveChanges();

            foreach (var id in model.CentralPolicyDateId)
            {
                var subjectdatedata = new SubjectDate
                {
                    SubjectId = subjectdata.Id,
                    CentralPolicyDateId = id
                };
                _context.SubjectDates.Add(subjectdatedata);
            }
            _context.SaveChanges();

            foreach (var questionopen in model.inputquestionopen)
            {
                var Subquestionopendata = new Subquestion
                {
                    SubjectId = subjectdata.Id,
                    Name = questionopen.questionopen,
                    Type = "คำถามปลายเปิด"
                };
                _context.Subquestions.Add(Subquestionopendata);
            }
            _context.SaveChanges();

            foreach (var questionclose in model.inputquestionclose)
            {
                var Subquestionclosedata = new Subquestion
                {
                    SubjectId = subjectdata.Id,
                    Name = questionclose.questionclose,
                    Type = "คำถามปลายปิด"
                };
                _context.Subquestions.Add(Subquestionclosedata);
                _context.SaveChanges();

                foreach (var questionclosechoice in questionclose.inputanswerclose)
                {
                    var Subquestionchoiceclosedata = new SubquestionChoice
                    {
                        SubquestionId = Subquestionclosedata.Id,
                        Name = questionclosechoice.answerclose,
                    };
                    _context.SubquestionChoices.Add(Subquestionchoiceclosedata);
                    _context.SaveChanges();
                }
            }

            return subjectdata;

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
        public void Put2([FromForm] SubjectViewModel model,long id)
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
 
            var subquestionopendata = _context.Subquestions.Find(id);
            subquestionopendata.Name = name;
            _context.Entry(subquestionopendata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // PUT api/values/5
        [HttpPut("editchoices/{id}")]
        public void Put4(long id, string name)
        {

            var subquestionopendata = _context.SubquestionChoices.Find(id);
            subquestionopendata.Name = name;
            _context.Entry(subquestionopendata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var subjectdata = _context.Subjects.Find(id);

            _context.Subjects.Remove(subjectdata);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("deletesubquestionopen/{id}")]
        public void Delete2(long id)
        {
            var subquestionopendata = _context.Subquestions.Find(id);

            _context.Subquestions.Remove(subquestionopendata);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("deletechoices/{id}")]
        public void Delete3(long id)
        {
            var subquestionchoicesdata = _context.SubquestionChoices.Find(id);

            _context.SubquestionChoices.Remove(subquestionchoicesdata);
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
    }
}
