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
    public class VillageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VillageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var villagedata = _context.Villages.ToList();
            return Ok(villagedata);

        }

         // GET api/values/5
          [HttpGet("{id}")]
          public IActionResult Get(long id)
          {
              var villagedata = _context.Villages
                  .Include(m => m.Subdistrict)
                  .ThenInclude(m => m.District)
                  .ThenInclude(m => m.Province)
                  .Where(m=>m.SubdistrictId== id);

              return Ok(villagedata);
          }
        [HttpPost]
        public Village Post([FromForm] VillageRequest request)
        {
            var date = DateTime.Now;
            var data = new Village
            {
                SubdistrictId = request.SubdistrictId,
                Name = request.Name,
            };
            _context.Villages.Add(data);
            _context.SaveChanges();
            return data;
        }

        [HttpPut("{id}")]
        public Village Put([FromForm] VillageRequest request, long id)
        {
            var data = _context.Villages.Find(id);
            data.Name = request.Name;

            _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return data;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var data = _context.Villages.Find(id);

            _context.Villages.Remove(data);
            _context.SaveChanges();
        }

    }
}

public class VillageRequest
{
    public long Id { get; set; }
    public long SubdistrictId { get; set; }
    public string Name { get; set; }

}

