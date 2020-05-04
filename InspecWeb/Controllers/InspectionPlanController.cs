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
        [HttpGet("{id}/{provinceid}")]
        public IActionResult Get(long id, long provinceid)
        {
            var inspectionplandata = _context.InspectionPlanEvents
                .Include(m => m.CentralPolicyEvents)
                .ThenInclude(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyDates)
                .Where(m => m.ProvinceId == provinceid)
                .Where(m => m.Id == id).ToList();
                //.Where(m => m.CentralPolicyEvents.Any(i => i.InspectionPlanEventId == id));

            var test = _context.CentralPolicyEvents
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyDates)
                .Include(m => m.InspectionPlanEvent)
                .Where(m => m.InspectionPlanEvent.ProvinceId == provinceid).ToList();


            return Ok( new { test , inspectionplandata });
        }

        // GET api/values/5
        [HttpGet("getcentralpolicydata/{provinceid}")]
        public IEnumerable<CentralPolicy> Get2(long provinceid)
        {
            return _context.CentralPolicies
                       .Include(m => m.CentralPolicyProvinces)
                       .Where(m => m.CentralPolicyProvinces.Any(i => i.ProvinceId == provinceid)).ToList();
                       //.ThenInclude(x => x.Province)
                       //.ToList();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] InspectionPlanViewModel model)
        {
            var test = model.UserID;
            System.Console.WriteLine(test);

            var date = DateTime.Now;
            var centralpolicydata = new CentralPolicy
            {
                Title = model.Title,
                Type = model.Type,
                FiscalYearId = model.FiscalYearId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Status = model.Status,
                CreatedAt = date,
                CreatedBy = model.UserID,
                Class = "แผนการตรวจ",
            };

            _context.CentralPolicies.Add(centralpolicydata);
            _context.SaveChanges();

            //foreach (var id in model.ProvinceId)
            //{
                var centralpolicyprovincedata = new CentralPolicyProvince
                {
                    ProvinceId = model.ProvinceId,
                    CentralPolicyId = centralpolicydata.Id,
                };
                _context.CentralPolicyProvinces.Add(centralpolicyprovincedata);
                _context.SaveChanges();

                var inspectionplaneventdata = new InspectionPlanEvent
                {
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    ProvinceId = model.ProvinceId,
                    CreatedAt = date,
                    CreatedBy = model.UserID,
                };
                _context.InspectionPlanEvents.Add(inspectionplaneventdata);
                _context.SaveChanges();

                var centralpolicyeventdata = new CentralPolicyEvent
                {
                    CentralPolicyId = centralpolicydata.Id,
                    InspectionPlanEventId = inspectionplaneventdata.Id,
                };
                _context.CentralPolicyEvents.Add(centralpolicyeventdata);
                _context.SaveChanges();
            //}
        }

        // POST api/values
        [HttpPost("AddCentralPolicyEvents")]
        public void Post([FromBody] CentralPolicyEventViewModel model)
        {
            foreach (var id in model.CentralPolicyId)
            {
                var ElectronicBookdata = new ElectronicBook
                {
                    CreatedBy = model.CreatedBy,
                    Status = "ร่างกำหนดการ",
                };
                _context.ElectronicBooks.Add(ElectronicBookdata);
                _context.SaveChanges();

                var centralpolicyeventdata = new CentralPolicyEvent
                {
                    CentralPolicyId = id,
                    InspectionPlanEventId = model.InspectionPlanEventId,
                    ElectronicBookId = ElectronicBookdata.Id,
                };
                _context.CentralPolicyEvents.Add(centralpolicyeventdata);
                _context.SaveChanges();
            }
        }

        // POST api/values
        [HttpPost("inspectionprovince")]
        public object Post(long provinceid, string userid, DateTime start_date_plan, DateTime end_date_plan)
        {
            var date = DateTime.Now;

            var InspectionPlanEventdata = new InspectionPlanEvent
            {
                ProvinceId = provinceid,
                CreatedAt = date,
                CreatedBy = userid,
                StartDate = start_date_plan,
                EndDate = end_date_plan,
            };

            _context.InspectionPlanEvents.Add(InspectionPlanEventdata);
            _context.SaveChanges();

            return InspectionPlanEventdata.Id;
        }

        // GET api/values/5
        [HttpGet("getcentralpolicyprovinceid/{centralpolicyid}/{provinceid}")]
        public IActionResult GetCentralpolicyprovinceid(long centralpolicyid,long provinceid)
        {
            var CentralPolicyProvincesid = _context.CentralPolicyProvinces
                .Where(m => m.CentralPolicyId == centralpolicyid)
                .Where(m => m.ProvinceId == provinceid)
                .Select(m => m.Id)
                .FirstOrDefault();
            //.Where(m => m.CentralPolicyEvents.Any(i => i.InspectionPlanEventId == id));

            return Ok(CentralPolicyProvincesid);
        }

    }
}
