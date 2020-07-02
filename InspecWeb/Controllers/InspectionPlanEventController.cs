using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
//using InspecWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
        [HttpGet("inspectionplan/{id}")]
        public IActionResult GetData(string id)
        {
            System.Console.WriteLine("DDDDD");
            System.Console.WriteLine("USERID : " + id);
            //var inspectionPlanEventdata = from P in _context.InspectionPlanEvents
            //                              select P;
            //return inspectionPlanEventdata;
            var userprovince = _context.UserProvinces
                               .Where(m => m.UserID == id)
                               .ToList();

            var user = _context.Users
                           .Where(m => m.Id == id)
                           .FirstOrDefault();

            if (user.Role_id == 3)
            {
                var inspectionplans = _context.InspectionPlanEvents
                                    .Include(m => m.Province)
                                    .Include(m => m.CentralPolicyEvents)
                                    .ThenInclude(m => m.CentralPolicy)
                                    .ThenInclude(m => m.CentralPolicyProvinces)
                                    .Where(m => m.RoleCreatedBy == "3")
                                    //.Where(m => m.Status == "ใช้งานจริง")
                                    //.Where(m => m.CentralPolicyEvents.Any(i => i.InspectionPlanEventId == id));
                                    .ToList();

                List<object> termsList = new List<object>();
                foreach (var inspectionplan in inspectionplans)
                {
                    for (int i = 0; i < userprovince.Count(); i++)
                    {
                        if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                            termsList.Add(inspectionplan);
                    }
                }
                return Ok(termsList);
            }
            else
            {
                var inspectionplans = _context.InspectionPlanEvents
                    .Include(m => m.Province)
                    .Include(m => m.CentralPolicyEvents)
                    .ThenInclude(m => m.CentralPolicy)
                    .ThenInclude(m => m.CentralPolicyProvinces)
                    //.Where(m => m.Status == "ใช้งานจริง")
                    //.Where(m => m.CentralPolicyEvents.Any(i => i.InspectionPlanEventId == id));
                    .ToList();

                List<object> termsList = new List<object>();
                foreach (var inspectionplan in inspectionplans)
                {
                    for (int i = 0; i < userprovince.Count(); i++)
                    {
                        if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                            termsList.Add(inspectionplan);
                    }
                }
                return Ok(termsList);
            }

            //List<object> termsList = new List<object>();
            //foreach (var inspectionplan in inspectionplans)
            //{
            //    for (int i = 0; i < userprovince.Count(); i++)
            //    {
            //        if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
            //            termsList.Add(inspectionplan);
            //    }
            //}

            //return Ok(termsList);

            //return 
            //_context.Provinces
            //   .Include(p => p.Districts)
            //   .Where(p => p.Id == 1)
            //   .ToList();
        }

        // GET: api/values
        [HttpGet("inspectionplanuser/{id}")]
        public IActionResult GetData2(string id)
        {
            //System.Console.WriteLine("DDDDD");
            //System.Console.WriteLine("USERID : " + id);

            //var userprovinces = _context.UserProvinces
            //                   .Where(m => m.UserID == id)
            //                   .ToList();

            //var inspectionplans = _context.InspectionPlanEvents
            //                    .Include(m => m.Province)
            //                    .Include(m => m.CentralPolicyEvents)
            //                    .ThenInclude(m => m.CentralPolicy.CentralPolicyProvinces)
            //                    .ThenInclude(m => m.CentralPolicy.CentralPolicyUser)
            //                    .Where(m => m.Status == "ใช้งานจริง")
            //                    .ToList();

            //List<object> termsList = new List<object>();
            //foreach (var inspectionplan in inspectionplans)
            //{
            //    System.Console.WriteLine("1");
            //    //for (int i = 0; i < userprovince.Count(); i++)
            //    foreach (var userprovince in userprovinces)
            //    {
            //        System.Console.WriteLine("2");
            //        if (inspectionplan.ProvinceId == userprovince.ProvinceId)
            //            foreach (var CentralPolicyEvent in inspectionplan.CentralPolicyEvents)
            //            {
            //                System.Console.WriteLine("3");
            //                foreach (var User in CentralPolicyEvent.CentralPolicy.CentralPolicyUser)
            //                {
            //                    System.Console.WriteLine("4");
            //                    System.Console.WriteLine("USER ID: " + User.UserId);
            //                    System.Console.WriteLine("CHECK USER: " + id);
            //                    if (User.UserId == id)
            //                    {
            //                        termsList.Add(User);
            //                        //break;
            //                    }

            //                }
            //            }
            //    }
            //}
            var termsList = _context.CentralPolicyUsers
                .Include(m => m.Province)
                .Include(m => m.InspectionPlanEvent)
                .Where(m => m.UserId == id)
                .Where(m => m.InspectionPlanEvent.Status == "ใช้งานจริง")
                .ToList();

            return Ok(termsList);
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
        public void Post([FromBody] InspectionPlanEventViewModel model)
        {
            //System.Console.WriteLine("LOOP: " + model.Name);
            //System.Console.WriteLine("Input: " + model.input);

            //return model.input;
            var date = DateTime.Now;

            //var inspectionplanevent = new InspectionPlanEvent
            //{
            //    CreatedAt = date,
            //    CreatedBy = "NIK"
            //};

            //_context.InspectionPlanEvents.Add(inspectionplanevent);
            //_context.SaveChanges();
            var roleid = _context.Users
                .Where(m => m.Id == model.CreatedBy)
                .Select(m => m.Role_id)
                .FirstOrDefault();

            foreach (var item2 in model.input)
            {
                //foreach (var item3 in item2.ProvinceId)
                //{
                var check = _context.InspectionPlanEvents
               .Where(x => x.CreatedBy == model.CreatedBy
               && x.StartDate == item2.StartPlanDate
               && x.EndDate == item2.EndPlanDate
               && x.ProvinceId == item2.ProvinceId)
               .Select(x => x.CreatedBy)
               .FirstOrDefault();

                System.Console.WriteLine("check: " + check);

                if (check == null)
                {
                    System.Console.WriteLine("no inspectionplanevent, create new");
                    var inspectionplanevent = new InspectionPlanEvent
                    {
                        StartDate = item2.StartPlanDate,
                        EndDate = item2.EndPlanDate,
                        ProvinceId = item2.ProvinceId,
                        CreatedAt = date,
                        CreatedBy = model.CreatedBy,
                        RoleCreatedBy = roleid.ToString(),
                        Status = "ร่างกำหนดการ"
                    };
                    _context.InspectionPlanEvents.Add(inspectionplanevent);
                    _context.SaveChanges();
                }
                else
                {
                    System.Console.WriteLine("already have inspectionplanevent, don't create");
                }

                //var centralpolicyeventdata = new CentralPolicyEvent
                //{
                //    CentralPolicyId = item2.CentralPolicyId,
                //    InspectionPlanEventId = inspectionplanevent.Id
                //};
                //   _context.CentralPolicyEvents.Add(centralpolicyeventdata);
                //   _context.SaveChanges();
                //}
            }

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var inspectionPlanEventdata = _context.InspectionPlanEvents.Find(id);

            _context.InspectionPlanEvents.Remove(inspectionPlanEventdata);
            _context.SaveChanges();
        }

        // GET: api/values
        [HttpGet("userregion/{userid}")] //province
        public IActionResult GetDataUserRegion(string userid)
        {
            var userdata = _context.UserProvinces
                .Include(m => m.Province)
                .Where(m => m.UserID == userid).ToList();

            return Ok(userdata);
        }

        // GET: api/values
        [HttpGet("userallregion/{userid}")]
        public IActionResult GetDataUserAllRegion(string userid)
        {
            var userdata = _context.UserRegions
                .Include(m => m.Region)
                .Where(m => m.UserID == userid).ToList();

            return Ok(userdata);
        }
        // GET: api/values
        [HttpGet("inspectionplanprovince/{id}/{proid}")]
        public IActionResult GetDataInspectionPlanProvince(string id, long proid)
        {
            var userprovince = _context.UserProvinces
                               .Where(m => m.UserID == id)
                               .ToList();
            var inspectionplans = _context.InspectionPlanEvents
                                .Include(m => m.Province)
                                .Include(m => m.CentralPolicyEvents)
                                .ThenInclude(m => m.CentralPolicy)
                                .ThenInclude(m => m.CentralPolicyProvinces)
                                .Where(m => m.ProvinceId == proid)
                                .ToList();
            List<object> termsList = new List<object>();
            foreach (var inspectionplan in inspectionplans)
            {
                for (int i = 0; i < userprovince.Count(); i++)
                {
                    if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                        termsList.Add(inspectionplan);
                }
            }
            return Ok(termsList);
        }

        // GET: api/values
        [HttpGet("inspectionplanexportindex/{id}")]
        public IActionResult GetCalendar(string id)
        {
            var userprovince = _context.UserProvinces
                               .Where(m => m.UserID == id)
                               .ToList();
            var inspectionplans = _context.CentralPolicyProvinces
                                .Include(m => m.Province)
                                .Include(m => m.CentralPolicy)
                                .Where(m => m.CentralPolicy.Class == "แผนการตรวจประจำปี" || m.CentralPolicy.Class == "แผนการตรวจ")
                                .ToList();

            List<object> termsList = new List<object>();
            foreach (var inspectionplan in inspectionplans)
            {
                for (int i = 0; i < userprovince.Count(); i++)
                {
                    if (inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                        termsList.Add(inspectionplan);
                }
            }
            return Ok(termsList);
        }

        // GET: api/ElectronicBook
        [HttpGet("exportexcelcalendarregion/{id}")]
        public IActionResult GetExcelCalendarRegion(long id)
        {

            List<object> data = new List<object>();
            var regions = _context.FiscalYearRelations
                .Where(m => m.RegionId == id).ToList();

            foreach (var region in regions)
            {
                var calendar = _context.CentralPolicyProvinces
                    .Include(x => x.CentralPolicy)
                    .Include(x => x.Province)
                    .Where(m => m.ProvinceId == region.ProvinceId)
                    .ToList();

                data.Add(calendar);
            }

            return Ok(new { data });
        }

        // GET: api/ElectronicBook
        [HttpGet("exportexcelcalendarprovince/{id}")]
        public IActionResult GetExcelCalendarProvince(long id)
        {
            //var calendar = _context.CentralPolicyProvinces
            //    .Include(x => x.CentralPolicy)
            //    .Include(x => x.Province)
            //    .Where(m => m.ProvinceId == id)
            //    .ToList();

            var calendar = _context.InspectionPlanEvents
                               .Include(m => m.Province)
                               .Include(m => m.CentralPolicyEvents)
                               .ThenInclude(m => m.CentralPolicy)
                               .ThenInclude(m => m.CentralPolicyProvinces)
                               .Include(m => m.CentralPolicyEvents)
                               .ThenInclude(m => m.CentralPolicy)
                               .ThenInclude(m => m.CentralPolicyUser)
                               .ThenInclude(m => m.User)
                               .Where(m => m.ProvinceId == id)

                               .ToList();

            //var calendar = _context.CentralPolicyEvents
            //    .Include(m => m.CentralPolicy)
            //    .ThenInclude(m => m.CentralPolicyUser)
            //    .Include(m => m.InspectionPlanEvent)
            //    .Where(m => m.InspectionPlanEvent.ProvinceId == id).ToList();

            return Ok(new { calendar });
        }
    }
}