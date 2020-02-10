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
    public class MinistryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MinistryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Ministry> Get()
        {
            var Ministrydata = from P in _context.Ministries
                               select P;
            return Ministrydata;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public Ministry Post(string name)
        {
            var date = DateTime.Now;

            var Ministrydata = new Ministry
            {
                Name = name,
                CreatedAt = date
            };

            _context.Ministries.Add(Ministrydata);
            _context.SaveChanges();

            return Ministrydata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name)
        {
            var Ministry = _context.Ministries.Find(id);
            Ministry.Name = name;
            _context.Entry(Ministry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var Ministrydata = _context.Ministries.Find(id);

            _context.Ministries.Remove(Ministrydata);
            _context.SaveChanges();
        }
    }
}