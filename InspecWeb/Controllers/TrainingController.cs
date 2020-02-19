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
    public class TrainingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Training> Get()
        {
            var trainingdata = from P in _context.Trainings
                               select P;
            return trainingdata;

            //return 
            //_context.Provinces
            //   .Include(p => p.Districts)
            //   .Where(p => p.Id == 1)
            //   .ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public Training Post(string name, string detail,DateTime start_date,DateTime end_date,string lecturer_name,DateTime regis_start_date, DateTime regis_end_date,string image)
        {
            var date = DateTime.Now;

            var trainingdata = new Training
            {
                Name = name,
                Detail = detail,
                StartDate = start_date,
                EndDate = end_date,
                LecturerName = lecturer_name,
                RegisStartDate = regis_start_date,
                RegisEndDate = regis_end_date,
                Image = image,
                CreatedAt = date
            };

            _context.Trainings.Add(trainingdata);
            _context.SaveChanges();

            return trainingdata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name, string detail, DateTime start_date, DateTime end_date, string lecturer_name, DateTime regis_start_date, DateTime regis_end_date, string image)
        {
            var training = _context.Trainings.Find(id);
            training.Name = name;
            training.Detail = detail;
            training.StartDate = start_date;
            training.EndDate = end_date;
            training.LecturerName = lecturer_name;
            training.RegisStartDate = regis_start_date;
            training.RegisEndDate = regis_end_date;
            training.Image = image;
            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var trainingdata = _context.Trainings.Find(id);

            _context.Trainings.Remove(trainingdata);
            _context.SaveChanges();
        }
    }
}
