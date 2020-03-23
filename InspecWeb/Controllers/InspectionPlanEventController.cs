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
        public void Post([FromBody] InspectionPlanEventViewModel model)
        {
            //System.Console.WriteLine("LOOP: " + model.Name);
            //System.Console.WriteLine("Input: " + model.input);

            //return model.input;
            var date = DateTime.Now;

            var mindate = DateTime.Now;
            var maxdate = DateTime.Now;
            int index = 0;
            foreach (var item1 in model.input)
            {
                if (index == 0)
                {
                    mindate = item1.PlanDate;
                    maxdate = item1.PlanDate;
                }
                if (item1.PlanDate > maxdate)
                {
                    maxdate = item1.PlanDate;
                }
                else if(item1.PlanDate < mindate)
                {
                    mindate = item1.PlanDate;
                }
                index++;
            }
 
            var inspectionplanevent = new InspectionPlanEvent
            {
                Name = model.Name,
                StartDate = mindate,
                EndDate = maxdate,
                CreatedAt = date,
                CreatedBy = "NIK"
            };

            _context.InspectionPlanEvents.Add(inspectionplanevent);
            _context.SaveChanges();

            foreach (var item2 in model.input)
            {
                foreach (var item3 in item2.ProvinceId)
                {
                    var inspectionplaneventprovince = new InspectionPlanEventProvince
                    {
                        InspectionPlanEventId = inspectionplanevent.Id,
                        PlanDate = item2.PlanDate,
                        ProvinceId = item3,
                    };

                    _context.InspectionPlanEventProvinces.Add(inspectionplaneventprovince);
                    _context.SaveChanges();
                }
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