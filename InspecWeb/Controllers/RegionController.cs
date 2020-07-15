using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class RegionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Region> Get()
        {
            var regiondata = from P in _context.Regions
                               select P;
            return regiondata;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public Region Post(string name)
        {
            var date = DateTime.Now;

            var regiondata = new Region
            {
                Name = name,
                CreatedAt = date
            };

            _context.Regions.Add(regiondata);
            _context.SaveChanges();

            return regiondata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name)
        {
            var region = _context.Regions.Find(id);
            region.Name = name;
            _context.Entry(region).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var regiondata = _context.Regions.Find(id);

            _context.Regions.Remove(regiondata);
            _context.SaveChanges();
        }

        //สำหรับใช้ตรง user
        [HttpGet("regionforuser/{id}")]
        public IActionResult GetRegionsforuser(long id)
        {
            var importFiscalYearRelations = _context.FiscalYearRelations
              .Include(x => x.Region)
              .Include(x => x.Province)
              .Where(x => x.FiscalYearId == id)
              .ToList();

            return Ok(new { importFiscalYearRelations });

        }

    }
}
