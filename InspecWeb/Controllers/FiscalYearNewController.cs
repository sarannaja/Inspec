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
    public class FiscalYearNewController : Controller
    {
        private readonly ApplicationDbContext _context;
     

        public FiscalYearNewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<FiscalYearNew> Get()
        {
            var data = from P in _context.FiscalYearNew
                       select P;
            return data;
        }

        // POST api/values
        [HttpPost]
        public FiscalYearNew Post([FromForm] FiscalYearNewRequest request)
        {
            var date = DateTime.Now;
            Console.WriteLine("data 1 :" + request.Year + "///" + request.StartDate + "/////" + request.EndDate);
            var data = new FiscalYearNew
            {
                Year = request.Year,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedAt = date
            };
           
            _context.FiscalYearNew.Add(data);
            _context.SaveChanges();
          
            return data;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromForm] FiscalYearNewRequest request,long id)
        {
            var data = _context.FiscalYearNew.Find(id);
            data.Year = request.Year;
            data.StartDate = request.StartDate;
            data.EndDate = request.EndDate;
          
            _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var data = _context.FiscalYearNew.Find(id);

            _context.FiscalYearNew.Remove(data);
            _context.SaveChanges();
        }
    }
}

public class FiscalYearNewRequest
{
    public long Id { get; set; }
   
    public int Year { get; set; }
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

}
