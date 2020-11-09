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
            var subdistrictdata = _context.Subdistricts
                .Include(m => m.District)
                .ThenInclude(m => m.Province)
                .Where(m=>m.DistrictId== id);

            return Ok(subdistrictdata);
        }

        [HttpPost]
        public Subdistrict Post([FromForm] SubdistrictRequest request)
        {
            var date = DateTime.Now;
            //Console.WriteLine("subdistrict 1 :" + request.Name + " : " + request.ProvincesId);
            var subdistrictdata = new Subdistrict
            {
                DistrictId = request.DistrictId,
                Name = request.Name,
            };
           // Console.WriteLine("subdistrict 2 :");
            _context.Subdistricts.Add(subdistrictdata);
            _context.SaveChanges();
           // Console.WriteLine("subdistrict 3 :");
            return subdistrictdata;
        }

        [HttpPut("{id}")]
        public Subdistrict Put([FromForm] SubdistrictRequest request, long id)
        {
            var subdistrictdata = _context.Subdistricts.Find(id);
            subdistrictdata.Name = request.Name;

            _context.Entry(subdistrictdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return subdistrictdata;
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

public class SubdistrictRequest
{
    public long Id { get; set; }
    public long DistrictId { get; set; }
    public string Name { get; set; }

}
