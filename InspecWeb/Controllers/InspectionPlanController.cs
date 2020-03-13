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
    public class InspectionPlanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InspectionPlanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        //[HttpGet]
        //public IEnumerable<CentralPolicyEvent> Get()
        //{
        //    var inspectionplandata = from P in _context.CentralPolicyEvents
        //                             select P;
        //    return inspectionplandata;
        //}

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
        public void Post([FromBody] CentralPolicyProvinceViewModel model)
        {
            var date = DateTime.Now;
            var centralpolicydata = new CentralPolicy
            {
                Title = model.Title,
                Type = model.Type,
                FiscalYearId = model.FiscalYearId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Status = "ร่างกำหนดการ",
                CreatedAt = date,
                CreatedBy = "NIK",
                Class = "แผนการตรวจ",
            };

            _context.CentralPolicies.Add(centralpolicydata);
            _context.SaveChanges();

            foreach (var id in model.ProvinceId)
            {
                var centralpolicyprovincedata = new CentralPolicyProvince
                {
                    ProvinceId = id,
                    CentralPolicyId = centralpolicydata.Id,
                };
                _context.CentralPolicyProvinces.Add(centralpolicyprovincedata);
            }
            _context.SaveChanges();

            var centralpolicyeventdata = new CentralPolicyEvent
            {
                CentralPolicyId = centralpolicydata.Id,
                InspectionPlanEventId = model.InspectionPlanEventId
            };
            _context.CentralPolicyEvents.Add(centralpolicyeventdata);
            _context.SaveChanges();
        }

        // POST api/values
        [HttpPost("AddCentralPolicyEvents")]
        public void Post([FromBody] CentralPolicyEventViewModel model)
        {
            foreach (var id in model.CentralPolicyId)
            {
                var centralpolicyeventdata = new CentralPolicyEvent
                {
                    CentralPolicyId = id,
                    InspectionPlanEventId = model.InspectionPlanEventId
                };
                _context.CentralPolicyEvents.Add(centralpolicyeventdata);
            }
            _context.SaveChanges();

        }
      
    }
}
