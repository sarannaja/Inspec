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
    public class ExecutiveOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExecutiveOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ExecutiveOrder> Get()
        {
            var executiveorderdata = from P in _context.ExecutiveOrders
                                     .Include(m => m.CentralPolicy)
                                     select P;
            return executiveorderdata;

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
        public Province Post(string name, string link)
        {
            var date = DateTime.Now;

            var provincedata = new Province
            {
                Name = name,
                Link = link,
                CreatedAt = date
            };

            _context.Provinces.Add(provincedata);
            _context.SaveChanges();

            return provincedata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name, string link)
        {
            var province = _context.Provinces.Find(id);
            province.Name = name;
            province.Link = link;
            _context.Entry(province).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var provincedata = _context.Provinces.Find(id);

            _context.Provinces.Remove(provincedata);
            _context.SaveChanges();
        }
    }
}
