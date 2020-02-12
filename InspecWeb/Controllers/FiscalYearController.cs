using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Mvc;


namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class FiscalYearController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FiscalYearController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<FiscalYear> Get()
        {
            var fiscalyeardata = from P in _context.FiscalYears
                               select P;
            return fiscalyeardata;

            //return 
            //_context.Provinces
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
        public FiscalYear Post(int year)
        {
            var date = DateTime.Now;

            var fiscalyeardata = new FiscalYear
            {
                Year = year,
                CreatedAt = date
            };

            _context.FiscalYears.Add(fiscalyeardata);
            _context.SaveChanges();

            return fiscalyeardata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, int year)
        {
            var fiscalyear = _context.FiscalYears.Find(id);
            fiscalyear.Year = year;
            _context.Entry(fiscalyear).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var fiscalyeardata = _context.FiscalYears.Find(id);

            _context.FiscalYears.Remove(fiscalyeardata);
            _context.SaveChanges();
        }
    }
}