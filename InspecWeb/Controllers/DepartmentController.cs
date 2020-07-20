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
        public IEnumerable<ProvincialDepartment> Get()
        {
            var provincialdepartmentdata = from P in _context.ProvincialDepartment
                               select P;
            return provincialdepartmentdata;
        }
        //สำหรับใช้ตรงข้อมูลสนับสนุน
        [HttpGet("departmentsforsupport/{id}")]
        public IActionResult GetDepartments(long id)
        {
            var departments = _context.Ministries
                .Include(m => m.Departments)
                .Where(m => m.Id == id);

            return Ok(departments);
        }

        //<!-- Get กรมตาม ID กระทรวง 20200720 -->
        [HttpGet("departmentsdata/{id}")]
        public IActionResult Getdepartmentsdata(long id)
        {
            var departmentdata = _context.Departments
               .Where(x => x.MinistryId == id);

            return Ok(departmentdata);
        }
        //<!-- END Get กรมตาม ID กระทรวง 20200720 -->

        //<!-- Get กรมตาม ID กระทรวง 20200720 -->
        [HttpGet("departmentsfirst/{id}")]
        public IActionResult departmentsfirst(long id)
        {
            var departmentdata = _context.Departments
               .Where(x => x.Id == id).FirstOrDefault(); 

            return Ok(departmentdata);
        }
        //<!-- END Get กรมตาม ID กระทรวง 20200720 -->


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

        //สำหรับใช้ตรง user
        [HttpGet("departmentsforuser/{id}")]
        public IActionResult GetDepartmentsforuser(long id)
        {
            var departments = _context.Departments
                .Where(m => m.MinistryId == id);

            return Ok(departments);
        }

        [HttpPost]
        public Department Post([FromForm] DepartmentRequest request)
        {
            Console.WriteLine ("department 1 :" + request.MinistryId);
            var date = DateTime.Now;

            var departmentdata = new Department
            {
                MinistryId = request.MinistryId,
                Name = request.Name,         
                CreatedAt = date
            };

            _context.Departments.Add(departmentdata);
            _context.SaveChanges();
            Console.WriteLine("department 2 :" + request.Name);
            return departmentdata;
        }

    
        [HttpPut("{id}")]
        public void Put([FromForm] DepartmentRequest request,long id)
        {
            Console.WriteLine("department 1 :" + id +"///"+ request.Name);
            var department = _context.Departments.Find(id);
                department.Name = request.Name;
      
            _context.Entry(department).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var departmentdata = _context.Departments.Find(id);

            _context.Departments.Remove(departmentdata);
            _context.SaveChanges();
        }

    }
}


public class DepartmentRequest
{
    public long Id { get; set; }
    public long MinistryId { get; set; }

    public string Name { get; set; }
}
