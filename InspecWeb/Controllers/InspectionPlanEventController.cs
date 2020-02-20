using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
//using InspecWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionPlanEventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InspectionPlanEventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<InspectionPlanEvent> Get()
        {
            var inspectionPlanEventdata = from P in _context.InspectionPlanEvents
                                          select P;
            return inspectionPlanEventdata;

            //return 
            //_context.Provinces
            //   .Include(p => p.Districts)
            //   .Where(p => p.Id == 1)
            //   .ToList();
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    //var centralpolicydata = _context.CentralPolicies.Include(x => x.Subjects).Where(m => m.Id == id).First();

        //    //return Ok(centralpolicydata);
        //}

        // POST api/values
        [HttpPost]
        public InspectionPlanEvent Post(string title, DateTime start_date, DateTime end_date)
        {
            var date = DateTime.Now;

            var inspectionPlanEventdata = new InspectionPlanEvent
            {
                Name = title,
                StartDate = start_date,
                EndDate = end_date,
                CreatedBy = "นาย ศรัณญ์ สาพรหม",
                CreatedAt = date,
                CentralPolicyId = 1,
                //Status = "ร่างกำหนดการ",
                //FiscalYearId = 1
            };

            _context.InspectionPlanEvents.Add(inspectionPlanEventdata);
            _context.SaveChanges();

            return inspectionPlanEventdata;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var inspectionPlanEventdata = _context.InspectionPlanEvents.Find(id);

            _context.InspectionPlanEvents.Remove(inspectionPlanEventdata);
            _context.SaveChanges();
        }
    }
}