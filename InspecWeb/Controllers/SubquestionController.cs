using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubquestionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubquestionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST api/values
        [HttpPost("addquestionopen")]
        public IActionResult Post(long SubjectCentralPolicyProvinceId, string Name, long Box, long[] departmentId)
        {
            System.Console.WriteLine("in");
            var Subquestionopendata = new SubquestionCentralPolicyProvince
            {
                SubjectCentralPolicyProvinceId = SubjectCentralPolicyProvinceId,
                Name = Name,
                Type = "คำถามปลายเปิด",
                Box = Box
            };
            _context.SubquestionCentralPolicyProvinces.Add(Subquestionopendata);
            _context.SaveChanges();

            foreach (var departmentIddata in departmentId)
            {
                System.Console.WriteLine("in2");
                var SubjectCentralPolicyProvinceGroupdata = new SubjectCentralPolicyProvinceGroup
                {
                    ProvincialDepartmentId = departmentIddata,
                    SubquestionCentralPolicyProvinceId = Subquestionopendata.Id,
                };
                _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata);
                _context.SaveChanges();

            }
            return Ok(new { status = true });
        }

        // POST api/values
        [HttpPost("addquestionclose")]
        public IActionResult Post2(long SubjectCentralPolicyProvinceId, string Name, long Box, long[] departmentId, string[] answerclose)
        {
            System.Console.WriteLine("Start" + answerclose);
            var Subquestionopendata = new SubquestionCentralPolicyProvince
            {
                SubjectCentralPolicyProvinceId = SubjectCentralPolicyProvinceId,
                Name = Name,
                Type = "คำถามปลายปิด",
                Box = Box
            };
            System.Console.WriteLine("in");
            _context.SubquestionCentralPolicyProvinces.Add(Subquestionopendata);
            _context.SaveChanges();

            foreach (var departmentIddata in departmentId)
            {
                System.Console.WriteLine("in2");
                var SubjectCentralPolicyProvinceGroupdata = new SubjectCentralPolicyProvinceGroup
                {
                    ProvincialDepartmentId = departmentIddata,
                    SubquestionCentralPolicyProvinceId = Subquestionopendata.Id,
                };
                _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata);
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
            return Ok(new { status = true });
        }

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<Subquestion> Get()
        //{
        //    var subquestiondata = from P in _context.Subquestions
        //                      select P;
        //    return subquestiondata;

        //    //return
        //    //_context.Provinces
        //    //   .Include(p => p.Districts)
        //    //   .Where(p => p.Id == 1)
        //    //   .ToList();
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public IActionResult Get(long id)
        //{
        //    var subquestiondata = _context.Subquestions.Where(m => m.SubjectId == id);

        //    return Ok(subquestiondata);
        //}

        //// POST api/values
        //[HttpPost]
        //public Subquestion Post(string name, long subjectid)
        //{
        //    //var date = DateTime.Now;

        //    var subquestiondata = new Subquestion
        //    {
        //        Name = name,
        //        SubjectId = subjectid,
        //        //Answer = "",
        //        //CreatedAt = date
        //    };

        //    _context.Subquestions.Add(subquestiondata);
        //    _context.SaveChanges();

        //    return subquestiondata;
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(long id, string name)
        //{
        //    var subquestions = _context.Subquestions.Find(id);
        //    subquestions.Name = name;
        //    _context.Entry(subquestions).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    _context.SaveChanges();

        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(long id)
        //{
        //    var subquestions = _context.Subquestions.Find(id);

        //    _context.Subquestions.Remove(subquestions);
        //    _context.SaveChanges();
        //}

        // POST api/values

        [HttpPost("addquestioncloseevent")]
        public IActionResult Post3(long SubjectCentralPolicyProvinceId, string Name, long Box, string[] answerclose)
        {
            System.Console.WriteLine("Start");
            var Subquestionopendata = new SubquestionCentralPolicyProvince
            {
                SubjectCentralPolicyProvinceId = SubjectCentralPolicyProvinceId,
                Name = Name,
                Type = "คำถามปลายปิด",
                Box = Box,
            };
            System.Console.WriteLine("in");
            _context.SubquestionCentralPolicyProvinces.Add(Subquestionopendata);
            _context.SaveChanges();

            //if (departmentId != null)
            //{
            //    long[] departmentId,
            //    foreach (var departmentIddata in departmentId)
            //    {
            //        System.Console.WriteLine("in2");
            //        var SubjectCentralPolicyProvinceGroupdata = new SubjectCentralPolicyProvinceGroup
            //        {
            //            ProvincialDepartmentId = departmentIddata,
            //            SubquestionCentralPolicyProvinceId = Subquestionopendata.Id,
            //        };
            //        _context.SubjectCentralPolicyProvinceGroups.Add(SubjectCentralPolicyProvinceGroupdata);
            //        _context.SaveChanges();

            //    }
            //}

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
            return Ok(new { status = true });
        }
    }
}