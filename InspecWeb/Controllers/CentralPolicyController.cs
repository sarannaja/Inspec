using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
//using InspecWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Get(int id)
        {
            var centralpolicydata = _context.CentralPolicies.Include(m => m.Subjects).Where(m => m.Id == id).First();

            return Ok(centralpolicydata);
            //return "value";
        }

        // POST api/values
        [HttpPost]
        public CentralPolicy Post(string title, DateTime start_date, DateTime end_date)
        {
            var date = DateTime.Now;

            var centralpolicydata = new CentralPolicy
            {
                Title = title,
                StartDate = start_date,
                EndDate = end_date,
                CreatedBy = "นาย ศรัณญ์ สาพรหม",
                CreatedAt = date,
                Status = "ร่างกำหนดการ",
                FiscalYearId = 1,
                Type = "แผนตรวจราชการประจำปี",
            };

            _context.CentralPolicies.Add(centralpolicydata);
            _context.SaveChanges();

            return centralpolicydata;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var centralpolicydata = _context.CentralPolicies.Find(id);

            _context.CentralPolicies.Remove(centralpolicydata);
            _context.SaveChanges();
        }

    }
}