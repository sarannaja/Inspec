using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class MinistermonitoringController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MinistermonitoringController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Ministermonitoring> Get()
        {
            var ministermonitoringdata = from P in _context.Ministermonitorings
                                     .Include(m => m.MinistermonitoringRegions)
                                     .ThenInclude(m => m.Region)
                                         select P;
            return ministermonitoringdata;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public Ministermonitoring Post([FromBody] MinistermonotoringViewModel model)
        {
            var date = DateTime.Now;

            var ministermonitoringdata = new Ministermonitoring
            {
                Name = model.Name,
                Position = model.Position,
                Image = model.Image,
                CreatedAt = date
            };

            _context.Ministermonitorings.Add(ministermonitoringdata);
            _context.SaveChanges();

            foreach (var id in model.RegionId)
            {
                var ministermonitoringregiondata = new MinistermonitoringRegion
                {
                    MinistermonitoringId = ministermonitoringdata.Id,
                    RegionId = id
                };
                _context.MinistermonitoringRegions.Add(ministermonitoringregiondata);
            }


            _context.SaveChanges();

            return ministermonitoringdata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name, string position, string image)
        {
            var cabine = _context.Cabines.Find(id);
            cabine.Name = name;
            cabine.Position = position;
            cabine.Image = image;
            _context.Entry(cabine).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var ministermonitoringdata = _context.Ministermonitorings.Find(id);

            _context.Ministermonitorings.Remove(ministermonitoringdata);
            _context.SaveChanges();
        }
    }
}
