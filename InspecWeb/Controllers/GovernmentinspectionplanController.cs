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
    public class GovernmentinspectionplanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GovernmentinspectionplanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Governmentinspectionplan> Get()
        {
            var governmentinspectionplanyeardata = from P in _context.Governmentinspectionplans
                                 select P;
            return governmentinspectionplanyeardata;

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
        public Governmentinspectionplan Post(string year, string title)
        {
            var date = DateTime.Now;

            var governmentinspectionplandata = new Governmentinspectionplan
            {
                Year = year,
                Title = title,
                CreatedAt = date,
                File = "",
            };

            _context.Governmentinspectionplans.Add(governmentinspectionplandata);
            _context.SaveChanges();

            return governmentinspectionplandata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string year, string title, string file)
        {
            var governmentinspectionplan = _context.Governmentinspectionplans.Find(id);
            governmentinspectionplan.Year = year;
            governmentinspectionplan.Title = title;
            governmentinspectionplan.File = "";
            _context.Entry(governmentinspectionplan).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var governmentinspectionplan = _context.Governmentinspectionplans.Find(id);

            _context.Governmentinspectionplans.Remove(governmentinspectionplan);
            _context.SaveChanges();
        }
    }
}