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
            //var villagedata = from P in _context.Villages

            //                  select P;
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

        
    }
}
