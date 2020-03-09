using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class FiscalYearController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FiscalYearController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<FiscalYear> Get()
        {
            var fiscalyeardata = from P in _context.FiscalYears
                                 select P;
            return fiscalyeardata;

            //return 
            //_context.Provinces
            //   .Include(p => p.Districts)
            //   .Where(p => p.Id == 1)
            //   .ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IEnumerable<object> Get(long id)
        {
            var fiscalyeardata = _context.FiscalYearRelations
                                        .Include(m => m.FiscalYear)
                                        .Include(m => m.Region)
                                        .Include(m => m.Province);


            return fiscalyeardata;


            //var fiscalyearprovince = new List<Array>();

            //foreach(var fiscalyeardata in fiscalyeardatas)
            //{ 
            //    fiscalyearprovince.Add(new
            //    {
            //        Province = fiscalyeardata.Pro
            //    });
            //}

            //return fiscalyeardata;
        }

        [HttpGet("getProvinceRecycled/{id}")]
        public IEnumerable<object> GetProvince(long id)
        {
            var fiscalyeardata = _context.FiscalYearRelations
                .Include(m => m.Province)
                .Where(m => m.FiscalYearId == id);

            //var province = _context.Provinces;
           
            //foreach(var eee in province)
            //{
            //    var pId = eee.Id;
            //    System.Console.WriteLine("PID: " + pId);
            //}
           
            //foreach (var ddd in fiscalyeardata)
            //{
            //    var fId = ddd.ProvinceId;
            //    System.Console.WriteLine("FID: " + fId);
            //}

            return fiscalyeardata;

        }

        //POST api/values
        [HttpPost("AddRelation")]
        public void Post([FromBody] FiscalYearRelationViewModel model)
        {

            foreach (var id in model.ProvinceId)
            {
                System.Console.WriteLine("LOOP: " + id);
                var fiscalyearrelationdata = new FiscalYearRelation
                {
                    FiscalYearId = model.FiscalYearId,
                    RegionId = model.RegionId,
                    ProvinceId = id
                };
                _context.FiscalYearRelations.Add(fiscalyearrelationdata);
            }


            _context.SaveChanges();


        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, int year)
        {
            var fiscalyear = _context.FiscalYears.Find(id);
            fiscalyear.Year = year;
            _context.Entry(fiscalyear).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("DeleteRelation/{id}/{fiscalyearid}")]
        public void Delete(long id, long fiscalyearid)

        {
            System.Console.WriteLine("LOOP: " + id + fiscalyearid);
            var fiscalyearrelationdata = _context.FiscalYearRelations
                .Where(x => x.RegionId == id && x.FiscalYearId == fiscalyearid);
            foreach (var test in fiscalyearrelationdata)
            {
                _context.FiscalYearRelations.Remove(test);
            }
            
            _context.SaveChanges();
        }
    }
}