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
    public class TypeexamibationplanController : Controller
    {
        private readonly ApplicationDbContext _context;
     

        public TypeexamibationplanController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Typeexaminationplan> Get()
        {
         
            var data = _context.Typeexaminationplans;
            return data;
        }

        // POST api/values
        [HttpPost]
        public Typeexaminationplan Post([FromForm] TypeexaminationplanRequest request)
        {
            var date = DateTime.Now;

            var data = new Typeexaminationplan
            {
                Name = request.Name,         
                CreatedAt = date
            };

            _context.Typeexaminationplans.Add(data);
            _context.SaveChanges();

            return data;
        }

       // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromForm] TypeexaminationplanRequest request, long id)
        {
            Console.WriteLine( " data :" + id);
            var data = _context.Typeexaminationplans.Find(id);
            data.Name = request.Name;

            _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var data = _context.Typeexaminationplans.Find(id);
            _context.Typeexaminationplans.Remove(data);
            _context.SaveChanges();
        }
    }
}

public class TypeexaminationplanRequest
{
    public long Id { get; set; }

    public string Name { get; set; }

}
