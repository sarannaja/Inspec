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
                .Include(m => m.Province)
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
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyProvinces)
                .Include(x => x.CentralPolicy)
                .ThenInclude(x => x.FiscalYear)
                .Where(m => m.InspectionPlanEvent.Id == id)
                .Where(m => m.InspectionPlanEvent.ProvinceId == provinceid)
                .Where(m => m.CentralPolicy.CentralPolicyProvinces.Any(m => m.ProvinceId == provinceid))
                .ToList();


            return Ok(new { test, inspectionplandata });
        }

        [HttpGet("getTimeline/{id}")]
        public IActionResult GetTimeline(long id)
        {
            var timelineData = _context.InspectionPlanEvents
                .Where(x => x.Id == id)
                .FirstOrDefault();
            return Ok(new { timelineData });
        }

        //[HttpGet("getScheduleData/{id}/{provinceId}")]
        //public IActionResult GetScheduleData(long id, long provinceId)
        //{
        //    var scheduleData = _context.InspectionPlanEvents
        //        .Include(x => x.Province)
        //        .Where(x => x.Id == id)
        //        .FirstOrDefault();

        //    var userData = _context.Users
        //        .Where(x => x.Id == scheduleData.CreatedBy)
        //        .FirstOrDefault();


        //    return Ok(new { scheduleData, userData });
        //}

        // GET api/values/5
        [HttpGet("getcentralpolicydata/{provinceid}")]
        public IEnumerable<CentralPolicy> Get2(long provinceid)
        {
            //return _context.CentralPolicies
            //           .Include(m => m.CentralPolicyProvinces)
            //           //.ThenInclude(m => m.SubjectCentralPolicyProvinces)
            //           .Where(m => m.CentralPolicyProvinces.Any(i => i.ProvinceId == provinceid)).ToList();

            var fiscalyearData = _context.FiscalYears
                                 .OrderByDescending(x => x.Year)
                                 .FirstOrDefault();

            return _context.CentralPolicies
                       .Include(m => m.CentralPolicyProvinces)
                       .ThenInclude(m => m.SubjectCentralPolicyProvinces)
                       //.Include(m => m.FiscalYear)
                       //.Where(m => m.CentralPolicyProvinces.Any(i => i.SubjectCentralPolicyProvinces.Any(m => m.Type == "NoMaster")))
                       //.Where(m => m.CentralPolicyProvinces.Any(i => i.SubjectCentralPolicyProvinces.Any(i => i.CentralPolicyProvince.ProvinceId == provinceid)))
                       .Where(m => m.FiscalYearId == fiscalyearData.Id)
                       .Where(m => m.CentralPolicyProvinces.Any(i => i.ProvinceId == provinceid))
                       .ToList();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] InspectionPlanViewModel model)
        {
            //var test = model.UserID;
            //System.Console.WriteLine(test);
            System.Console.WriteLine("1" + model.Title);
            var date = DateTime.Now;
            System.Console.WriteLine("2" + model.Type);
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
            System.Console.WriteLine("3");
            _context.CentralPolicies.Add(centralpolicydata);
            _context.SaveChanges();
            System.Console.WriteLine("4");
            //foreach (var id in model.ProvinceId)
            //{
            var centralpolicyprovincedata = new CentralPolicyProvince
            {
                ProvinceId = model.ProvinceId,
                CentralPolicyId = centralpolicydata.Id,
                Step = "มอบหมายหน่วยงาน",
                Status = "ร่างกำหนดการ"
            };
            _context.CentralPolicyProvinces.Add(centralpolicyprovincedata);
            _context.SaveChanges();

            var subjectdata = new SubjectCentralPolicyProvince
            {
                Name = model.Title,
                CentralPolicyProvinceId = centralpolicyprovincedata.Id,
                Type = "NoMaster",
                Status = "ใช้งานจริง"
            };
            _context.SubjectCentralPolicyProvinces.Add(subjectdata);
            _context.SaveChanges();

            //var inspectionplaneventdata = new InspectionPlanEvent
            //{
            //    StartDate = model.StartDate,
            //    EndDate = model.EndDate,
            //    ProvinceId = model.ProvinceId,
            //    CreatedAt = date,
            //    CreatedBy = model.UserID,
            //};
            //_context.InspectionPlanEvents.Add(inspectionplaneventdata);
            //_context.SaveChanges();
            var ElectronicBookdata = new ElectronicBook
            {
                CreatedBy = model.UserID,
                Status = "ร่างกำหนดการ",
            };
            _context.ElectronicBooks.Add(ElectronicBookdata);
            _context.SaveChanges();
            System.Console.WriteLine("3");

            var ElectronicBookGroupdata = new ElectronicBookGroup
            {
                // CentralPolicyProvinceId = centralpolicyprovincedata.Id,
                ElectronicBookId = ElectronicBookdata.Id,
            };
            _context.ElectronicBookGroups.Add(ElectronicBookGroupdata);
            _context.SaveChanges();

            var centralpolicyeventdata = new CentralPolicyEvent
            {
                CentralPolicyId = centralpolicydata.Id,
                InspectionPlanEventId = model.InspectionPlanEventId,
                HaveSubject = 0,
                //ElectronicBookId = ElectronicBookdata.Id,
            };
            _context.CentralPolicyEvents.Add(centralpolicyeventdata);
            _context.SaveChanges();
            //}
        }

        // POST api/values
        [HttpPost("AddCentralPolicyEvents")]
        public void Post([FromBody] CentralPolicyEventViewModel model)
        {

            System.Console.WriteLine("1");
            foreach (var id in model.CentralPolicyId)
            {
                var centralpolicyprovince = _context.CentralPolicyProvinces
                    .Where(m => m.CentralPolicyId == id && m.ProvinceId == model.ProvinceId).FirstOrDefault();

                System.Console.WriteLine("2");
                //var ElectronicBookdata = new ElectronicBook
                //{
                //    CreatedBy = model.CreatedBy,
                //    Status = "ร่างกำหนดการ",
                //};
                //_context.ElectronicBooks.Add(ElectronicBookdata);
                //_context.SaveChanges();
                //System.Console.WriteLine("3");
                //var ElectronicBookGroupdata = new ElectronicBookGroup
                //{
                //    CentralPolicyProvinceId = centralpolicyprovince.Id,
                //    ElectronicBookId = ElectronicBookdata.Id,
                //};
                //_context.ElectronicBookGroups.Add(ElectronicBookGroupdata);
                //_context.SaveChanges();
                System.Console.WriteLine("4");
                var centralpolicyeventdata = new CentralPolicyEvent
                {
                    CentralPolicyId = id,
                    InspectionPlanEventId = model.InspectionPlanEventId,
                    NotificationDate = model.NotificationDate,
                    DeadlineDate = model.DeadlineDate,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    //ElectronicBookId = ElectronicBookdata.Id,
                };
                _context.CentralPolicyEvents.Add(centralpolicyeventdata);
                _context.SaveChanges();

                //var CentralPolicyProvinceEventdata = new CentralPolicyProvinceEvent
                //{
                //    CentralPolicyProvinceId = centralpolicyprovince.Id,
                //    InspectionPlanEventId = model.InspectionPlanEventId,
                //};
                //_context.CentralPolicyProvinceEvents.Add(CentralPolicyProvinceEventdata);
                //_context.SaveChanges();

                System.Console.WriteLine("5");
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
                Status = "ร่างกำหนดการ"
            };

            _context.InspectionPlanEvents.Add(InspectionPlanEventdata);
            _context.SaveChanges();

            return InspectionPlanEventdata.Id;
        }

        // GET api/values/5
        [HttpGet("getcentralpolicyprovinceid/{centralpolicyid}/{provinceid}")]
        public IActionResult GetCentralpolicyprovinceid(long centralpolicyid, long provinceid)
        {
            System.Console.WriteLine("cenID: " + centralpolicyid);
            System.Console.WriteLine("proID: " + provinceid);
            var CentralPolicyProvincesid = _context.CentralPolicyProvinces
                .Where(m => m.CentralPolicyId == centralpolicyid && m.ProvinceId == provinceid)
                .Select(m => m.Id)
                .FirstOrDefault();
            //.Where(m => m.CentralPolicyEvents.Any(i => i.InspectionPlanEventId == id));

            System.Console.WriteLine("re ID: " + CentralPolicyProvincesid);
            return Ok(CentralPolicyProvincesid);
        }

        [HttpGet("getScheduleData/{id}/{provinceId}")]
        public IActionResult GetScheduleData(long id, long provinceId)
        {
            var scheduleData = _context.InspectionPlanEvents
                .Include(x => x.Province)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var userData = _context.Users
                .Where(x => x.Id == scheduleData.CreatedBy)
                .FirstOrDefault();


            return Ok(new { scheduleData, userData });
        }

        // POST api/values
        [HttpPost("changeplanstatus")]
        public void Changeplanstatus(long planid)
        {
            var InspectionPlanEventsdata = _context.InspectionPlanEvents
                .Find(planid);
            InspectionPlanEventsdata.Status = "ใช้งานจริง";

            _context.Entry(InspectionPlanEventsdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // POST api/values
        [HttpPost("editplandate")]
        public void Editplandate(long planid, DateTime startdate,DateTime enddate)
        {
            var InspectionPlanEventsdata = _context.InspectionPlanEvents
                .Find(planid);
            InspectionPlanEventsdata.StartDate = startdate;
            InspectionPlanEventsdata.EndDate = enddate;

            _context.Entry(InspectionPlanEventsdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("deleteplandate/{planid}")]
        public void Deleteplandate(long planid)
        {
            var InspectionPlanEventsdata = _context.InspectionPlanEvents.Find(planid);

            _context.InspectionPlanEvents.Remove(InspectionPlanEventsdata);
            _context.SaveChanges();
        }
    }
}
