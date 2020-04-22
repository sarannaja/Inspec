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
            var inspectionplans = _context.InspectionPlanEvents
                                .Include(m => m.Province)
                                .Include(m => m.CentralPolicyEvents)
                                .ThenInclude(m => m.CentralPolicy)
                                .ThenInclude(m => m.CentralPolicyProvinces)
                                //.Where(m => m.CentralPolicyEvents.Any(i => i.InspectionPlanEventId == id));
                                .ToList();
            List<object> termsList = new List<object>();
            foreach (var inspectionplan in inspectionplans)
            {
                for (int i = 0; i < userprovince.Count(); i++)
                {
                    if(inspectionplan.ProvinceId == userprovince[i].ProvinceId)
                    termsList.Add(inspectionplan);
                }
            }

            return Ok(termsList);

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

            foreach (var item2 in model.input)
            {
                //foreach (var item3 in item2.ProvinceId)
                //{
                    var inspectionplanevent = new InspectionPlanEvent
                    {
                        StartDate = item2.StartPlanDate,
                        EndDate = item2.EndPlanDate,
                        ProvinceId = item2.ProvinceId,
                        CreatedAt = date,
                        CreatedBy = "NIK"
                    };
                       _context.InspectionPlanEvents.Add(inspectionplanevent);
                       _context.SaveChanges();

                    var centralpolicyeventdata = new CentralPolicyEvent
                    {
                        CentralPolicyId = item2.CentralPolicyId,
                        InspectionPlanEventId = inspectionplanevent.Id
                    };
                       _context.CentralPolicyEvents.Add(centralpolicyeventdata);
                       _context.SaveChanges();
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
    }
}