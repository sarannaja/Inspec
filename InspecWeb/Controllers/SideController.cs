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
    public class SideController : Controller
    {
        private readonly ApplicationDbContext _context;
     

        public SideController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Side> Get()
        {
            var data = from P in _context.Sides
                       select P;
            return data;
        }

        // POST api/values
        [HttpPost]
        public Side Post([FromForm] SideRequest request)
        {
            var date = DateTime.Now;
         
            var data = new Side
            {
                Name = request.Name,
                NameEN = request.NameEN,
                ShortnameEN = request.ShortnameEN,
                ShortnameTH = request.ShortnameTH,
                CreatedAt = date
            };
           
            _context.Sides.Add(data);
            _context.SaveChanges();
          
            return data;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromForm] SideRequest request,long id)
        {
            var data = _context.Sides.Find(id);
            data.Name = request.Name;
            data.NameEN = request.NameEN;
            data.ShortnameEN = request.ShortnameEN;
            data.ShortnameTH = request.ShortnameTH;

            _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var data = _context.Sides.Find(id);

            _context.Sides.Remove(data);
            _context.SaveChanges();
        }
    }
}

public class SideRequest
{
    public long Id { get; set; }
   
    public string Name { get; set; }
    public string NameEN { get; set; }
    public string ShortnameEN { get; set; }
    public string ShortnameTH { get; set; }

}
