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

        //public IEnumerable<object> Get(long id)
        //{
        //    var fiscalyeardata = _context.FiscalYearRelations
        //        .GroupBy(g => new { fiscalyear = g.FiscalYearId, region = g.RegionId })
        //        .Select(g => new
        //        {
        //            g.Key.fiscalyear ,
        //            g.Key.region
        //        })
        //        .ToList();

        //    var fiscalyear = new List<object>();

        //    foreach (var fiscalyeardatas in fiscalyeardata)
        //    {
        //        var fiscal = _context.FiscalYearRelations

        //            .Where(p => p.FiscalYearId == fiscalyeardatas.fiscalyear)
        //            .AsQueryable();

        //        var regions = _context.FiscalYearRelations
        //            .Where(g => g.RegionId == fiscalyeardatas.region)
        //            .Include(x => x.Region)
        //            .ToList();


        //        fiscalyear.Add(new
        //        {
        //            fiscalyear = fiscal ,
        //            regionsId = regions


        //        });
        //    }

        //    return fiscalyear;






        //    //var fiscalyearprovince = new List<Array>();

        //    //foreach(var fiscalyeardata in fiscalyeardatas)
        //    //{
        //    //    fiscalyearprovince.Add(new
        //    //    {
        //    //        Province = fiscalyeardata.Pro
        //    //    });
        //    //}

        //    return fiscalyeardata;
        ////}

        //POST api/values
        //[HttpPost]
        //public object Post(int year)
        //{


        //    var date = DateTime.Now;

        //    var fiscalyeardata = new FiscalYear
        //    {
        //        Year = year,
        //        CreatedAt = date
        //    };

        //    _context.FiscalYears.Add(fiscalyeardata);
        //    _context.SaveChanges();



        //    var regoinTest =
        //          @" 'data': [" +
        //         "{'name': 'เขตการปกครองพิเศษ', 'provinces': [ {'name':'กรุงเทพมหานคร' }] }," +
        //         //"{'name':'เขตตรวจราชการที่ 1', 'provinces': [{'name': 'นนทบุรี'}, {'name':'ปทุมธานี'},{'name': 'พระนครศรีอยุธยา'}, {'name':'สระบุรี'}] }" +
        //         "{'name':'เขตตรวจราชการที่ 1', 'provinces': [{'name': 'นนทบุรี'}, {'name':'ปทุมธานี'},{'name': 'พระนครศรีอยุธยา'}, {'name':'สระบุรี'}] }" +
        //         //"{'name':'เขตตรวจราชการที่2','provinces':[{'name':'ชัยนาท'},{'name':'ลพบุรี'},{'name':'สิงห์บุรี'},{'name':'อ่างทอง'}] }"+
        //         "]";

        //    long[] provinceId1 = new long[] { 1, 3, 5, 7, 9 };
        //    var des = (RegoinData)Newtonsoft.Json.JsonConvert.DeserializeObject(regoinTest, typeof(RegoinData));

        //    //dynamic provinceId3 = JsonSerializer.Deserialize(regoinTest);
        //    //JsonConvert.DeserializeObject<List<RegoinData>>(regoinTest);

        //    //var regoin = (new RegoinData { name = "เขตการปกครองพิเศษ", provinces = (new {name = "กรุงเทพมหานคร" } } )   );

        //    return des;
        //    //{ 'name' = 'เขตการปกครองพิเศษ'};
        //    long[] provinceId2 = new long[] { 1, 3, 5, 7, 9 };


        //    //new { RegionId = 1 , ProvinceId = provinceId1 };

        //    var fiscalYearRelation = new FiscalYearRelation
        //    {
        //        FiscalYearId = fiscalyeardata.Id,
        //        RegionId = 1,
        //        ProvinceId = 1
        //    };
        //    _context.FiscalYearRelations.Add(fiscalYearRelation);





        //    return fiscalyeardata;
        //}

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
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var fiscalyeardata = _context.FiscalYears.Find(id);

            _context.FiscalYears.Remove(fiscalyeardata);
            _context.SaveChanges();
        }
    }
}