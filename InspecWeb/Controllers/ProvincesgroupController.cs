using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class ProvincesgroupController : Controller
    {
        private readonly ApplicationDbContext _context;
     

        public ProvincesgroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ProvincesGroup> Get()
        {
         
            var data = _context.ProvincesGroups;
            return data;
        }

        // POST api/values
        [HttpPost]
        public ProvincesGroup Post([FromForm] ProvincesgroupRequest request)
        {
            var date = DateTime.Now;

            var data = new ProvincesGroup
            {
                Name = request.Name,         
                CreatedAt = date
            };

            _context.ProvincesGroups.Add(data);
            _context.SaveChanges();

            return data;
        }

       // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromForm] ProvincesgroupRequest request, long id)
        {
            Console.WriteLine( " data :" + id);
            var data = _context.ProvincesGroups.Find(id);
            data.Name = request.Name;

            _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var data = _context.ProvincesGroups.Find(id);
            _context.ProvincesGroups.Remove(data);
            _context.SaveChanges();
        }
    }
}

public class ProvincesgroupRequest
{
    public long Id { get; set; }

    public string Name { get; set; }

}
