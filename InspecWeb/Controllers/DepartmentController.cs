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
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<ProvincialDepartmentProvince> Get()
        {
            var provincialdepartmentprovincedata = from P in _context.ProvincialDepartmentProvince
                               select P;
            return provincialdepartmentprovincedata;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var provincialdepartmentprovincedata = _context.ProvincialDepartmentProvince
                .Include(m => m.ProvincialDepartment)
                .Where(m => m.ProvinceId == id);

            return Ok(provincialdepartmentprovincedata);
        }

        // GET api/values/5
        [HttpGet("masteraof/{id}")]
        public IActionResult Get2(long id)
        {
            var centralpolicyprovinces = _context.CentralPolicyProvinces
                //.Where(m => m.ProvinceId == id)
                .Where(m => m.CentralPolicyId == id).ToList();

            List<object> termsList = new List<object>();
            List<object> termsList2 = new List<object>();
            foreach (var centralpolicyprovince in centralpolicyprovinces)
            {

                var provincialdepartmentprovincedata = _context.ProvincialDepartmentProvince
                 .Include(m => m.ProvincialDepartment)
                 .Where(m => m.ProvinceId == centralpolicyprovince.ProvinceId)
                 .ToList();

                foreach (var test in provincialdepartmentprovincedata) {
                        termsList.Add(new {
                            provinceId = test.ProvinceId,
                            provinceName = test.ProvincialDepartment.Name,
                            provinceDepartmentId = test.Id
                        });
                }
            }
            return Ok(termsList);
        }
    }
}
