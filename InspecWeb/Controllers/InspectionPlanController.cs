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
    public class InspectionPlanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InspectionPlanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<CentralPolicyEvent> Get()
        {
            var inspectionplandata = from P in _context.CentralPolicyEvents
                               select P;
            return inspectionplandata;

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
            var inspectionplandata = _context.InspectionPlanEvents
                .Include(m => m.CentralPolicyEvents)
                .ThenInclude(m => m.CentralPolicy)
                .Where(m => m.Id == id);
                //.Where(m => m.CentralPolicyEvents.Any(i => i.InspectionPlanEventId == id));

            return Ok(inspectionplandata);
        }

        // POST api/values
        [HttpPost]
        public District Post(string name)
        {
            //var date = DateTime.Now;

            var districtdata = new District
            {
                Name = name,
                //CreatedAt = date
            };

            _context.Districts.Add(districtdata);
            _context.SaveChanges();

            return districtdata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name)
        {
            var districts = _context.Districts.Find(id);
            districts.Name = name;
            _context.Entry(districts).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var districtdata = _context.Districts.Find(id);

            _context.Districts.Remove(districtdata);
            _context.SaveChanges();
        }
    }
}
