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
                // .Include(m => m.Province)
                .Include(m => m.CentralPolicyEvents)
                // .ThenInclude(m => m.CentralPolicy)
                // .ThenInclude(m => m.Typeexaminationplan)
                // .ThenInclude(m => m.CentralPolicyDates)
                .OrderByDescending(m => m.Id)
                .Where(m => m.ProvinceId == provinceid)
                .Where(m => m.Id == id).ToList()
                .Select(m => new{
                    startDate = m.StartDate,
                    endDate = m.EndDate,
                    provinceid = m.ProvinceId,
                    centralPolicyId = m.CentralPolicyEvents.Select(m => m.CentralPolicyId),
                    //centralPolicyClass = m.CentralPolicyEvents.Select(m => m.CentralPolicy.Class)
                    // typeexaminationplan = m.CentralPolicyEvents.Select(m => m.CentralPolicy.Typeexaminationplan.Name),
                    // centralPolicyname = m.CentralPolicyEvents.Select(m => m.CentralPolicy.Title),
                });
            //.Where(m => m.CentralPolicyEvents.Any(i => i.InspectionPlanEventId == id));

            var test = _context.CentralPolicyEvents
                // .Include(m => m.CentralPolicy)
                // .ThenInclude(m => m.CentralPolicyDates)
                .Include(m => m.InspectionPlanEvent)
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.CentralPolicyProvinces)
                // .Include(x => x.CentralPolicy)
                // .ThenInclude(x => x.FiscalYearNew)
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.Typeexaminationplan)
                .OrderByDescending(m => m.Id)
                .Where(m => m.InspectionPlanEvent.Id == id)
                .Where(m => m.InspectionPlanEvent.ProvinceId == provinceid)
                .Where(m => m.CentralPolicy.CentralPolicyProvinces.Any(m => m.ProvinceId == provinceid)).ToList()
                .Select(m => new { 
                    centralPolicyId = m.CentralPolicyId,
                    typeexaminationplan = m.CentralPolicy.Typeexaminationplan.Name,
                    centralPolicyname = m.CentralPolicy.Title,
                    startDate = m.StartDate,
                    endDate = m.EndDate,
                    centralPolicyClass = m.CentralPolicy.Class,
                    id = m.Id,
                    inspectionPlanEvent = new
                    {
                        provinceId = m.InspectionPlanEvent.ProvinceId
                    }
                });

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
        [HttpGet("getcentralpolicydata/{provinceid}/{year}")]
        public IActionResult Get2(long provinceid, long year) //IEnumerable<CentralPolicy>
        {
            //return _context.CentralPolicies
            //           .Include(m => m.CentralPolicyProvinces)
            //           //.ThenInclude(m => m.SubjectCentralPolicyProvinces)
            //           .Where(m => m.CentralPolicyProvinces.Any(i => i.ProvinceId == provinceid)).ToList();

            var year2 = _context.FiscalYearNew
                .Where(m => m.Year == year).FirstOrDefault();

            //var fiscalyearData = _context.FiscalYears
            //    .Where(m => m.Id == year2.Id).FirstOrDefault();
            //.OrderByDescending(x => x.Year)
            //.FirstOrDefault();

            var data = _context.CentralPolicies
                .Include(m => m.CentralPolicyProvinces)
                .ThenInclude(m => m.SubjectCentralPolicyProvinces)
                //.Include(m => m.FiscalYear)
                //.Where(m => m.CentralPolicyProvinces.Any(i => i.SubjectCentralPolicyProvinces.Any(m => m.Type == "NoMaster")))
                //.Where(m => m.CentralPolicyProvinces.Any(i => i.SubjectCentralPolicyProvinces.Any(i => i.CentralPolicyProvince.ProvinceId == provinceid)))
                .Where(m => m.FiscalYearNewId == year2.Id)
                .Where(m => m.CentralPolicyProvinces.Any(i => i.ProvinceId == provinceid && i.Active == 1))
                .Select(m => new {
                    id = m.Id,
                    title = m.Title,
                    status = m.Status,
                })
                .ToList();

                return Ok(new{data});
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] InspectionPlanViewModel model)
        {

            var userdata = _context.Users
              .Where(m => m.Id == model.UserID)
              //.Select(m => m.Role_id)
              .FirstOrDefault();
            System.Console.WriteLine("FiscalYearId" + model.FiscalYearId);
            var year = _context.FiscalYearNew
                .Where(m => m.Year == model.FiscalYearId).FirstOrDefault();
            System.Console.WriteLine("year" + year.Id);
            //if(year == null)
            //{

            //}
            //var test = model.UserID;
            //System.Console.WriteLine(test);
            System.Console.WriteLine("111");
            var date = DateTime.Now;
            System.Console.WriteLine("222" + model.Type);
            var centralpolicydata = new CentralPolicy
            {
                Title = model.Title,
                TypeexaminationplanId = 3,
                FiscalYearNewId = year.Id,
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

            ///////////////////////////////
            var SubjectGroupdata = new SubjectGroup
            {
                CentralPolicyId = centralpolicydata.Id,
                ProvinceId = model.ProvinceId,
                Type = "Master",
                Land = "Master",
                Status = "Master",
                StatusSuggestion = "ร่างกำหนดการ",

                ProvincialDepartmentIdCreatedBy = userdata.ProvincialDepartmentId,
                CreatedBy = userdata.Id,
                RoleCreatedBy = userdata.Role_id,
            };
            _context.SubjectGroups.Add(SubjectGroupdata);
            _context.SaveChanges();
            ///////////////////////////////
            ///
            var subjectdata = new SubjectCentralPolicyProvince
            {
                Name = model.Title,
                CentralPolicyProvinceId = centralpolicyprovincedata.Id,
                Type = "Master",
                Status = "ใช้งานจริง",
                SubjectGroupId = SubjectGroupdata.Id,
                CreatedBy = model.UserID,
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

            //var ElectronicBookdata = new ElectronicBook
            //{
            //    CreatedBy = model.UserID,
            //    Status = "ร่างกำหนดการ",
            //};
            //_context.ElectronicBooks.Add(ElectronicBookdata);
            //_context.SaveChanges();
            //System.Console.WriteLine("3");

            //var ElectronicBookGroupdata = new ElectronicBookGroup
            //{
            //    // CentralPolicyProvinceId = centralpolicyprovincedata.Id,
            //    ElectronicBookId = ElectronicBookdata.Id,
            //};
            //_context.ElectronicBookGroups.Add(ElectronicBookGroupdata);
            //_context.SaveChanges();

            var centralpolicyeventdata = new CentralPolicyEvent
            {
                CentralPolicyId = centralpolicydata.Id,
                InspectionPlanEventId = model.InspectionPlanEventId,
                HaveSubject = 0,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                //ElectronicBookId = ElectronicBookdata.Id,
            };
            _context.CentralPolicyEvents.Add(centralpolicyeventdata);
            _context.SaveChanges();

            var logdata = new Log
            {
                UserId = model.UserID,
                DatabaseName = "CentralPolicyEvent",
                EventType = "เพิ่ม",
                EventDate = date,
                Detail = "เพิ่มแผนตรวจราชการในกำหนดการตรวจราชการ",
                Allid = centralpolicyeventdata.Id,
            };

            _context.Logs.Add(logdata);
            _context.SaveChanges();
            //}
            return Ok(new { status = true });
        }

        // POST api/values
        [HttpPost("AddCentralPolicyEvents")]
        public void Post([FromBody] CentralPolicyEventViewModel model)
        {
            var date = DateTime.Now;
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
                    // NotificationDate = model.NotificationDate,
                    // DeadlineDate = model.DeadlineDate,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    HaveSubject = 0,
                    //ElectronicBookId = ElectronicBookdata.Id,
                };
                _context.CentralPolicyEvents.Add(centralpolicyeventdata);
                _context.SaveChanges();


                var logdata = new Log
                {
                    UserId = model.CreatedBy,
                    DatabaseName = "CentralPolicyEvent",
                    EventType = "เพิ่ม",
                    EventDate = date,
                    Detail = "เพิ่มแผนการตรวจราชการประจำปีในกำหนดการตรวจราชการ",
                    Allid = centralpolicyeventdata.Id,
                };

                _context.Logs.Add(logdata);
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
            var userdata = _context.Users

                .Where(m => m.Id == userid)
                //.Select(m => m.Role_id)
                .FirstOrDefault();

            var date = DateTime.Now;

            var InspectionPlanEventdata = new InspectionPlanEvent
            {
                ProvinceId = provinceid,
                CreatedAt = date,
                CreatedBy = userid,
                StartDate = start_date_plan,
                EndDate = end_date_plan,
                Status = "ร่างกำหนดการ",
                RoleCreatedBy = userdata.Role_id.ToString(),
                ProvincialDepartmentIdCreatedBy = userdata.ProvincialDepartmentId,
            };

            _context.InspectionPlanEvents.Add(InspectionPlanEventdata);
            _context.SaveChanges();

            var logdata = new Log
            {
                UserId = userid,
                DatabaseName = "InspectionPlanEvent",
                EventType = "เพิ่ม",
                EventDate = date,
                Detail = "เพิ่มกำหนดการตรวจราชการ",
                Allid = InspectionPlanEventdata.Id,
            };

            _context.Logs.Add(logdata);
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
        public void Editplandate(long planid, DateTime startdate, DateTime enddate, string userid)
        {

            var date = DateTime.Now;
            var logdata = new Log
            {
                UserId = userid,
                DatabaseName = "InspectionPlanEvent",
                EventType = "แก้ไข",
                EventDate = date,
                Detail = "แก้ไขกำหนดการตรวจราชการ",
                Allid = planid,
            };

            _context.Logs.Add(logdata);
            _context.SaveChanges();

            var InspectionPlanEventsdata = _context.InspectionPlanEvents
                .Find(planid);
            InspectionPlanEventsdata.StartDate = startdate;
            InspectionPlanEventsdata.EndDate = enddate;

            _context.Entry(InspectionPlanEventsdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("deleteplandate/{planid}/{userid}")]
        public void Deleteplandate(long planid, string userid)
        {
            var date = DateTime.Now;
            var centralpolicyeventdatas = _context.CentralPolicyEvents
            .Where(m => m.InspectionPlanEventId == planid).ToList();

            foreach (var centralpolicyeventdata in centralpolicyeventdatas)
            {

                var group = _context.SubjectGroupPeopleQuestions.Where(p => p.CentralPolicyEventId == centralpolicyeventdata.Id).FirstOrDefault();
                if (group != null)
                {

                    var subjectgroupdata = _context.SubjectGroups
                    .Include(p => p.CentralPolicy)
                    .Where(p => p.Id == group.SubjectGroupId).FirstOrDefault();

                    var SubjectCentralPolicyProvincesdatas = _context.SubjectCentralPolicyProvinces
                        .Where(p => p.SubjectGroupId == group.SubjectGroupId).ToList();

                    foreach (var SubjectCentralPolicyProvincesdata in SubjectCentralPolicyProvincesdatas)
                    {
                        var delsubjectCentralPolicyProvincesdata = _context.SubjectCentralPolicyProvinces.Find(SubjectCentralPolicyProvincesdata.Id);
                        _context.SubjectCentralPolicyProvinces.Remove(delsubjectCentralPolicyProvincesdata);
                        _context.SaveChanges();
                    }

                    var SubjectGroupPeopleQuestionsdata = _context.SubjectGroupPeopleQuestions.Find(group.Id);
                    _context.SubjectGroupPeopleQuestions.Remove(SubjectGroupPeopleQuestionsdata);
                    _context.SaveChanges();

                    var logdata2 = new Log
                    {
                        UserId = userid,
                        DatabaseName = "SubjectGroups",
                        EventType = "ลบ",
                        EventDate = date,
                        Detail = "ลบประเด็นตรวจติดตามของแผน" + subjectgroupdata.CentralPolicy.Title,
                        Allid = group.SubjectGroupId,
                    };

                    _context.Logs.Add(logdata2);
                    _context.SaveChanges();

                    // var logdata = new Log
                    // {
                    //     UserId = userid,
                    //     DatabaseName = "CentralPolicyEvent",
                    //     EventType = "ลบ",
                    //     EventDate = date,
                    //     Detail = "ลบแผนตรวจราชการ" + subjectgroupdata.CentralPolicy.Title + "ในกำหนดการตรวจราชการ",
                    //     Allid = id,
                    // };

                    // _context.Logs.Add(logdata);
                    // _context.SaveChanges();

                    var subject = _context.SubjectGroups.Find(group.SubjectGroupId);
                    _context.SubjectGroups.Remove(subject);
                    _context.SaveChanges();
                }

                //     var InspectionPlanEventsdata = _context.CentralPolicyEvents.Find(id);
                //     _context.CentralPolicyEvents.Remove(InspectionPlanEventsdata);
                //     _context.SaveChanges();
                // }
            }

            var CalendarFiledata = _context.CalendarFiles
            .Where(m => m.InspectionPlanEventId == planid).ToList();
            _context.CalendarFiles.RemoveRange(CalendarFiledata);
            _context.SaveChanges();

            var InspectionPlanEventsdatalog = _context.InspectionPlanEvents
            .Include(m => m.Province)
            .Where(m => m.Id == planid).FirstOrDefault();

            var logdata = new Log
            {
                UserId = userid,
                DatabaseName = "InspectionPlanEvent",
                EventType = "ลบ",
                EventDate = date,
                Detail = "ลบกำหนดการตรวจราชการจังหวัด" + InspectionPlanEventsdatalog.Province.Name + "วันที่ " + InspectionPlanEventsdatalog.StartDate.ToShortDateString() + "ถึง " + InspectionPlanEventsdatalog.EndDate.ToShortDateString(),
                Allid = planid,
            };

            _context.Logs.Add(logdata);
            _context.SaveChanges();

            var InspectionPlanEventsdata = _context.InspectionPlanEvents.Find(planid);


            _context.InspectionPlanEvents.Remove(InspectionPlanEventsdata);
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("deletecentralpolicyevent/{id}/{userid}/{planid}")]
        public void Deletecentralpolicyevent(long id, string userid, long planid)
        {

            var date = DateTime.Now;

            var cenid = _context.CentralPolicyEvents
            .Where(p => p.Id == id).FirstOrDefault();

            var cenuser = _context.CentralPolicyUsers
            .Where(p => p.CentralPolicyId == cenid.CentralPolicyId && p.InspectionPlanEventId == planid);
            _context.CentralPolicyUsers.RemoveRange(cenuser);
            _context.SaveChanges();

            var group = _context.SubjectGroupPeopleQuestions.Where(p => p.CentralPolicyEventId == id).FirstOrDefault();
            if (group != null)
            {

                var subjectgroupdata = _context.SubjectGroups
                .Include(p => p.CentralPolicy)
                .Where(p => p.Id == group.SubjectGroupId).FirstOrDefault();

                var SubjectCentralPolicyProvincesdatas = _context.SubjectCentralPolicyProvinces
                    .Where(p => p.SubjectGroupId == group.SubjectGroupId).ToList();

                foreach (var SubjectCentralPolicyProvincesdata in SubjectCentralPolicyProvincesdatas)
                {
                    var delsubjectCentralPolicyProvincesdata = _context.SubjectCentralPolicyProvinces.Find(SubjectCentralPolicyProvincesdata.Id);
                    _context.SubjectCentralPolicyProvinces.Remove(delsubjectCentralPolicyProvincesdata);
                    _context.SaveChanges();
                }

                var SubjectGroupPeopleQuestionsdata = _context.SubjectGroupPeopleQuestions.Find(group.Id);
                _context.SubjectGroupPeopleQuestions.Remove(SubjectGroupPeopleQuestionsdata);
                _context.SaveChanges();

                var logdata2 = new Log
                {
                    UserId = userid,
                    DatabaseName = "SubjectGroups",
                    EventType = "ลบ",
                    EventDate = date,
                    Detail = "ลบประเด็นตรวจติดตามของแผน" + subjectgroupdata.CentralPolicy.Title,
                    Allid = group.SubjectGroupId,
                };

                _context.Logs.Add(logdata2);
                _context.SaveChanges();


                var logdata = new Log
                {
                    UserId = userid,
                    DatabaseName = "CentralPolicyEvent",
                    EventType = "ลบ",
                    EventDate = date,
                    Detail = "ลบแผนตรวจราชการ" + subjectgroupdata.CentralPolicy.Title + "ในกำหนดการตรวจราชการ",
                    Allid = id,
                };

                _context.Logs.Add(logdata);
                _context.SaveChanges();

                var subject = _context.SubjectGroups.Find(group.SubjectGroupId);
                _context.SubjectGroups.Remove(subject);
                _context.SaveChanges();


                var InspectionPlanEventsdata = _context.CentralPolicyEvents.Find(id);
                _context.CentralPolicyEvents.Remove(InspectionPlanEventsdata);
                _context.SaveChanges();



            }
            else
            {
                var InspectionPlanEventsdata2 = _context.CentralPolicyEvents
                .Include(p => p.CentralPolicy)
                .Where(p => p.Id == id).FirstOrDefault();

                var logdata = new Log
                {
                    UserId = userid,
                    DatabaseName = "CentralPolicyEvent",
                    EventType = "ลบ",
                    EventDate = date,
                    Detail = "ลบแผนตรวจราชการ" + InspectionPlanEventsdata2.CentralPolicy.Title + "ในกำหนดการตรวจราชการ",
                    Allid = id,
                };

                _context.Logs.Add(logdata);
                _context.SaveChanges();

                var InspectionPlanEventsdata = _context.CentralPolicyEvents.Find(id);
                _context.CentralPolicyEvents.Remove(InspectionPlanEventsdata);
                _context.SaveChanges();
            }
        }

        // DELETE api/values/5
        [HttpDelete("deletecentralpolicy/{id}/{userid}")]
        public void Deletecentralpolicy(long id, string userid)
        {

            var CalendarFiledata = _context.CalendarFiles
              .Where(m => m.CentralPolicyId == id).ToList();
            _context.CalendarFiles.RemoveRange(CalendarFiledata);
            _context.SaveChanges();

            var InspectionPlanEventsdata = _context.CentralPolicies.Find(id);

            var date = DateTime.Now;
            var logdata = new Log
            {
                UserId = userid,
                DatabaseName = "CentralPolicyEvent",
                EventType = "ลบ",
                EventDate = date,
                Detail = "ลบแผนตรวจราชการ" + InspectionPlanEventsdata.Title + "ในกำหนดการตรวจราชการ",
                Allid = id,
            };

            _context.Logs.Add(logdata);
            _context.SaveChanges();

            _context.CentralPolicies.Remove(InspectionPlanEventsdata);
            _context.SaveChanges();
        }

        // GET api/values/5
        [HttpGet("getcentralpolicyeventdata/{id}")]
        public IActionResult Getcentralpolicyeventdata(long id)
        {
            var centralpolicydata = _context.CentralPolicyEvents
                .Include(m => m.CentralPolicy)
                .ThenInclude(m => m.FiscalYearNew)
                .Where(m => m.Id == id)
                .FirstOrDefault();

            return Ok(centralpolicydata);
        }

        // GET api/values/5
        [HttpGet("getcentralpolicyeventdataid/{id}")]
        public IActionResult Getcentralpolicyeventdataid(long id)
        {
            var centralpolicydata = _context.SubjectGroupPeopleQuestions
                                  .Include(m => m.CentralPolicyEvent)
                .Where(p => p.SubjectGroupId == id).FirstOrDefault();

            var idid = centralpolicydata.CentralPolicyEventId;
            return Ok(centralpolicydata);
        }

        // POST api/values
        [HttpPost("editcentralpolicy")]
        public void Editcentralpolicy(long ceneventid, DateTime startdate, DateTime enddate, string title, string userid)
        {
            // var InspectionPlanEventsdata = _context.InspectionPlanEvents
            //     .Find(cenid);
            // InspectionPlanEventsdata.StartDate = startdate;
            // InspectionPlanEventsdata.EndDate = enddate;

            var CentralPolicyEventdata = _context.CentralPolicyEvents
                .Find(ceneventid);

            CentralPolicyEventdata.StartDate = startdate;
            CentralPolicyEventdata.EndDate = enddate;
            _context.Entry(CentralPolicyEventdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            var CentralPolicyId = _context.CentralPolicyEvents
                .Where(m => m.Id == ceneventid).Select(m => m.CentralPolicyId).FirstOrDefault();

            var CentralPolicydata = _context.CentralPolicies
                .Find(CentralPolicyId);
            CentralPolicydata.Title = title;
            CentralPolicydata.StartDate = startdate;
            CentralPolicydata.EndDate = enddate;
            //CentralPolicydata.FiscalYearNewId = year;

            _context.Entry(CentralPolicydata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            var date = DateTime.Now;
            var logdata = new Log
            {
                UserId = userid,
                DatabaseName = "CentralPolicyEvent",
                EventType = "แก้ไข",
                EventDate = date,
                Detail = "แก้ไขแผนการตรวจราชการในกำหนดตรวจราชการ",
                Allid = ceneventid,
            };

            _context.Logs.Add(logdata);
            _context.SaveChanges();

        }
    }
}