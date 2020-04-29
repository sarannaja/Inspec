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
    public class AnswerSubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnswerSubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        //[HttpGet]
        //public IEnumerable<Province> Get()
        //{
        //    var provincedata = from P in _context.Provinces
        //                       select P;
        //    return provincedata;

        //}

        // GET api/values/5
        [HttpGet("{userid}")]
        public IActionResult Get(string userid)
        {
            var userdata = _context.Users
                .Where(m => m.Id == userid).First();

            var provincialdepartment = _context.ProvincialDepartment
                .Where(m => m.DepartmentId == userdata.DepartmentId).First();

            return Ok(provincialdepartment);

        }

        // POST api/values
        [HttpPost]
        public Province Post(string name, string link)
        {
            var date = DateTime.Now;

            var provincedata = new Province
            {
                Name = name,
                Link = link,
                CreatedAt = date
            };

            _context.Provinces.Add(provincedata);
            _context.SaveChanges();

            return provincedata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name, string link)
        {
            var province = _context.Provinces.Find(id);
            province.Name = name;
            province.Link = link;
            _context.Entry(province).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var provincedata = _context.Provinces.Find(id);

            _context.Provinces.Remove(provincedata);
            _context.SaveChanges();
        }
    }
}
