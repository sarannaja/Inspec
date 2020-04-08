using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
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

        // GET: api/values
        [HttpGet]
        public IEnumerable<Subquestion> Get()
        {
            var subquestiondata = from P in _context.Subquestions
                              select P;
            return subquestiondata;

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
            var subquestiondata = _context.Subquestions.Where(m => m.SubjectId == id);

            return Ok(subquestiondata);
        }

        // POST api/values
        [HttpPost]
        public Subquestion Post(string name, long subjectid)
        {
            //var date = DateTime.Now;

            var subquestiondata = new Subquestion
            {
                Name = name,
                SubjectId = subjectid,
                //Answer = "",
                //CreatedAt = date
            };

            _context.Subquestions.Add(subquestiondata);
            _context.SaveChanges();

            return subquestiondata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name)
        {
            var subquestions = _context.Subquestions.Find(id);
            subquestions.Name = name;
            _context.Entry(subquestions).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var subquestions = _context.Subquestions.Find(id);

            _context.Subquestions.Remove(subquestions);
            _context.SaveChanges();
        }
    }
}