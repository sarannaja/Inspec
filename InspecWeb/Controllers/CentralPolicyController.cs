using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentralPolicyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CentralPolicyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<CentralPolicy> Get()
        {
            var centralpolicydata = from P in _context.CentralPolicies
                                    select P;
            return centralpolicydata;

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
        public CentralPolicy Post(string title)
        {
            var date = DateTime.Now;

            var centralpolicydata = new CentralPolicy
            {
                Title = title,
                CreatedAt = date
            };

            _context.CentralPolicies.Add(centralpolicydata);
            _context.SaveChanges();

            return centralpolicydata;
        }

    }
}