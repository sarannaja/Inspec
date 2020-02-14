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
    public class SubdistrictController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubdistrictController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Subdistrict> Get()
        {
            var subdistrictdata = from P in _context.Subdistricts
                               select P;
            return subdistrictdata;

            //return 
            //_context.Provinces
            //   .Include(p => p.Districts)
            //   .Where(p => p.Id == 1)
            //   .ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var subdistrictdata = _context.Subdistricts.Where(m=>m.DistrictId== id);

            return Ok(subdistrictdata);
        }

        // POST api/values
        [HttpPost]
        public Subdistrict Post(string name)
        {
            //var date = DateTime.Now;

            var subdistrictdata = new Subdistrict
            {
                Name = name,
                //CreatedAt = date
            };

            _context.Subdistricts.Add(subdistrictdata);
            _context.SaveChanges();

            return subdistrictdata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name)
        {
            var subdistricts = _context.Subdistricts.Find(id);
            subdistricts.Name = name;
            _context.Entry(subdistricts).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var subdistrictdata = _context.Subdistricts.Find(id);

            _context.Subdistricts.Remove(subdistrictdata);
            _context.SaveChanges();
        }
    }
}
