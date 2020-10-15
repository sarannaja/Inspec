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
    public class SectorController : Controller
    {
        private readonly ApplicationDbContext _context;
     

        public SectorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Sector> Get()
        {
            //var data = from P in _context.Sectors
            //           select P;

            var data = _context.Sectors;
            return data;
        }

        // POST api/values
        [HttpPost]
        public Sector Post([FromForm] SectorRequest request)
        {
            var date = DateTime.Now;

            var data = new Sector
            {
                Name = request.Name,         
                CreatedAt = date
            };

            _context.Sectors.Add(data);
            _context.SaveChanges();

            return data;
        }

       // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromForm] SectorRequest request, long id)
        {
            Console.WriteLine( " data :" + id);
            var data = _context.Sectors.Find(id);
            data.Name = request.Name;

            _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var data = _context.Sectors.Find(id);
            _context.Sectors.Remove(data);
            _context.SaveChanges();
        }
    }
}

public class SectorRequest
{
    public long Id { get; set; }

    public string Name { get; set; }

}
