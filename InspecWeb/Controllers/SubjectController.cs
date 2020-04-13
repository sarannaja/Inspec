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
        [HttpPost("addsubquestion")]
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var subjectdata = _context.Subjects.Find(id);

            _context.Subjects.Remove(subjectdata);
            _context.SaveChanges();
        }

        [HttpPost("subjectprovince")]
        public object Post(long centralpolicyid, int provincevalue)
        {
            return provincevalue;
        }
    }
}
