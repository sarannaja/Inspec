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
    public class DistrictController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DistrictController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<District> Get()
        {
            var districtdata = from P in _context.Districts
                               select P;
            return districtdata;

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
            var districtdata = _context.Districts
                .Include(m => m.Province)
                .Where(m => m.ProvinceId == id);

            return Ok(districtdata);
        }

        [HttpPost]
        public District Post([FromForm] DistrictRequest request)
        {
            var date = DateTime.Now;
            Console.WriteLine("district 1 :" + request.Name + " : " + request.ProvincesId );
            var districtdata = new District
            {
                ProvinceId = request.ProvincesId,
                Name = request.Name,
            };
            Console.WriteLine("district 2 :");
            _context.Districts.Add(districtdata);
            _context.SaveChanges();
            Console.WriteLine("district 3 :");
            return districtdata;
        }

        [HttpPut("{id}")]
        public void Put([FromForm] DistrictRequest request, long id)
        {
            var districtdata = _context.Districts.Find(id);
            districtdata.Name = request.Name;
        
            _context.Entry(districtdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var districtdata = _context.Districts.Find(id);

            _context.Districts.Remove(districtdata);
            _context.SaveChanges();
        }
    }
}

public class DistrictRequest
{
    public long Id { get; set; }
    public long ProvincesId { get; set; }
    public string Name { get; set; }
  
}
