using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
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
            //var centralpolicydata = from P in _context.CentralPolicies
            //                        select P;
            //return centralpolicydata;

            return _context.CentralPolicies
                   .Where(m => m.Class == "แผนการตรวจประจำปี")
                   .ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var centralpolicydata = _context.CentralPolicies
                .Include(m => m.Subjects)
                .ThenInclude(m => m.Subquestions)
                .Where(m => m.Id == id).First();

            return Ok(centralpolicydata);
            //return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] CentralPolicyProvinceViewModel model)
        {
            var date = DateTime.Now;
            var centralpolicydata = new CentralPolicy
            {
                Title = model.Title,
                Type = model.Type,
                FiscalYearId = model.FiscalYearId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Status = "ร่างกำหนดการ",
                CreatedAt = date,
                CreatedBy = "NIK",
                Class = "แผนการตรวจประจำปี",
            };

            _context.CentralPolicies.Add(centralpolicydata);
            _context.SaveChanges();

            foreach (var id in model.ProvinceId)
            {
                var centralpolicyprovincedata = new CentralPolicyProvince
                {
                    ProvinceId = id,
                    CentralPolicyId = centralpolicydata.Id,
                };
                _context.CentralPolicyProvinces.Add(centralpolicyprovincedata);
            }
            _context.SaveChanges();
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