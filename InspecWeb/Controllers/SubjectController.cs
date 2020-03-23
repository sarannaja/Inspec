using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
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
            var subjectdata = _context.Subjects.Where(m => m.CentralPolicyId == id);

            return Ok(subjectdata);
        }

        // POST api/values
        [HttpPost]
        public Subject Post(string name, long centralpolicyid, DateTime start_date, DateTime end_date)
        {
            //var date = DateTime.Now;

            var subjectdata = new Subject
            {
                Name = name,
                CentralPolicyId = centralpolicyid,
                StartDate = start_date,
                EndDate = end_date,
                Answer = "",
                //CreatedAt = date
            };

            _context.Subjects.Add(subjectdata);
            _context.SaveChanges();

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
    }
}
