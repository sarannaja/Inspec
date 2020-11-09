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
    public class ProvincialDepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProvincialDepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var provincialdepartmentprovincedata = _context.ProvincialDepartment        
                .Where(x => x.DepartmentId == id);

            return Ok(provincialdepartmentprovincedata);
        }

        [HttpGet("Getdetail/{id}")]
        public IActionResult Getdetail(long id)
        {
            var provincialdepartmentprovincedata = _context.ProvincialDepartmentProvince
                .Include(m => m.Province)
                .Include(m => m.ProvincialDepartment)
                .Where(m => m.ProvincialDepartment.Id == id).ToList();

            List<NewProvince> termsList = new List<NewProvince>();
            for (int i = 0;i< provincialdepartmentprovincedata.Count; i++)
            {
                string provinceName = _context.Provinces.Find(provincialdepartmentprovincedata[i].ProvinceId).Name;
                long provinceId = provincialdepartmentprovincedata[i].ProvinceId;
                termsList.Add(new NewProvince { Name = provinceName, ProvinceId= provinceId });
            }

            // You can convert it back to an array if you would like to
            NewProvince[] terms = termsList.ToArray();

            return Ok(terms);
        }



        [HttpPost]
        public ProvincialDepartment Post([FromForm] ProvincialDepartmentRequest request)
        {
            Console.WriteLine ("test 1 :" + request.DepartmentId);
            var date = DateTime.Now;

            var provincialdepartmentdata = new ProvincialDepartment
            {
                DepartmentId = request.DepartmentId,
                Name = request.Name,         
                CreatedAt = date
            };

            _context.ProvincialDepartment.Add(provincialdepartmentdata);
            _context.SaveChanges();

            
            foreach (var item in request.Province)
            {
                Console.WriteLine("test 2 :");
                var provincedata = new ProvincialDepartmentProvince
                {
                    ProvincialDepartmentID = provincialdepartmentdata.Id,
                    ProvinceId = item
                };
               
                _context.ProvincialDepartmentProvince.Add(provincedata);
                _context.SaveChanges();
            }



            Console.WriteLine("test 3 :");
            return provincialdepartmentdata;
        }

    
        [HttpPut("{id}")]
        public ProvincialDepartment Put([FromForm] ProvincialDepartmentRequest request,long id)
        {
            Console.WriteLine("department 1 :" + id +"///"+ request.Name);

            var provincialdepartmentdata = _context.ProvincialDepartment.Find(id);
            provincialdepartmentdata.Name = request.Name;
      
            _context.Entry(provincialdepartmentdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            // <! -- ลบข้อมูล -->
            var ProvincialDepartmentProvince = _context.ProvincialDepartmentProvince
                .Where(m => m.ProvincialDepartmentID == id);
            _context.ProvincialDepartmentProvince.RemoveRange(ProvincialDepartmentProvince);
            _context.SaveChanges();
            // <! -- END ลบข้อมูล -->

            foreach (var item in request.Province)
            {
                Console.WriteLine("test 2 :");
                var provincedata = new ProvincialDepartmentProvince
                {
                    ProvincialDepartmentID = id,
                    ProvinceId = item
                };

                _context.ProvincialDepartmentProvince.Add(provincedata);
                _context.SaveChanges();
            }
            return provincialdepartmentdata;

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var ProvincialDepartmentProvince = _context.ProvincialDepartmentProvince
             .Where(m => m.ProvincialDepartmentID == id);
            _context.ProvincialDepartmentProvince.RemoveRange(ProvincialDepartmentProvince);
            _context.SaveChanges();

            var departmentdata = _context.ProvincialDepartment.Find(id);

            _context.ProvincialDepartment.Remove(departmentdata);
            _context.SaveChanges();
        }

    }
}

public class NewProvince
{
    public long ProvinceId { get; set; }
    public string Name { get; set; }

}

public class ProvincialDepartmentRequest
{
    public long Id { get; set; }
    public long DepartmentId { get; set; }

    public string Name { get; set; }
    public List<long> Province { get; set; } 

}
