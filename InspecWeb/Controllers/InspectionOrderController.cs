using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class InspectionOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InspectionOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<InspectionOrder> Get()
        {
            var inspectionorderdata = from P in _context.InspectionOrders
                                      select P;
            return inspectionorderdata;

            //return 
            //_context.inspectionorders
            //   .Include(p => p.Districts)
            //   .Where(p => p.Id == 1)
            //   .ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public InspectionOrder Post(string name)
        {
            var date = DateTime.Now;

            var inspectionorderdata = new InspectionOrder
            {
                Name = name,
                CreatedAt = date
            };

            _context.InspectionOrders.Add(inspectionorderdata);
            _context.SaveChanges();

            return inspectionorderdata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name)
        {
            var inspectionorder = _context.InspectionOrders.Find(id);
            inspectionorder.Name = name;   
            _context.Entry(inspectionorder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var inspectionorderdata = _context.InspectionOrders.Find(id);

            _context.InspectionOrders.Remove(inspectionorderdata);
            _context.SaveChanges();
        }
    }
}
