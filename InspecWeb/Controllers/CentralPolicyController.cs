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
                   .Include(m => m.CentralPolicyDates)
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

            foreach (var item in model.inputdate)
            {
                var CentralPolicyDate = new CentralPolicyDate
                {
                    CentralPolicyId = centralpolicydata.Id,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                };
                _context.CentralPolicyDates.Add(CentralPolicyDate);
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

        //POST api/values
        [HttpPost("users")]
        public void Post([FromBody] CentralPolicyUserModel model)
        {

            foreach (var id in model.UserId)
            {
                System.Console.WriteLine("LOOP: " + id);
                var centralpolicyuserdata = new CentralPolicyUser
                {
                    CentralPolicyId = model.CentralPolicyId,
                    UserId = id,
                    Status = "รอการตอบรับ"
                };
                _context.CentralPolicyUsers.Add(centralpolicyuserdata);
            }
            _context.SaveChanges();
        }

        // GET api/values/5
        [HttpGet("users/{id}")]
        public IActionResult GetUsers(long id)
        {
            var centralpolicyuserdata = _context.CentralPolicyUsers
                .Include(m => m.User)
                .Where(m => m.CentralPolicyId == id);

            return Ok(centralpolicyuserdata);
        }


        // GET api/values/5
        [HttpGet("getcentralpolicyfromprovince/{id}")]
        public IActionResult getcentralpolicyfromprovince(long id)
        {
            var centralpolicyprovincedata = _context.CentralPolicyProvinces
                .Include(m => m.CentralPolicy)
                .Where(m => m.ProvinceId == id)
                .ToList();

            return Ok(centralpolicyprovincedata);
        }
        // PUT api/values/5
        [HttpPut("acceptcentralpolicy/{id}")]
        public void PutStatus(long id, string status)

        {
            System.Console.WriteLine("ID: " + id);
            System.Console.WriteLine("Status: " + status);


            //var acaccept = _context.CentralPolicyUsers.Where(x => x.Id == id)
            //               .First();
            //System.Console.WriteLine("acaccept: " + acaccept.Id);


            (from t in _context.CentralPolicyUsers where t.Id == id select t).ToList().
                ForEach(x => x.Status = status);

            //var accept = _context.CentralPolicyUsers.Find(1);
            //accept.Status = status;
            //System.Console.WriteLine("Status: " + accept);
            //_context.Entry(accept).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();


        }
    }
}