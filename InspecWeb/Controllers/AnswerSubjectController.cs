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
    public class AnswerSubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnswerSubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        //[HttpGet]
        //public IEnumerable<Province> Get()
        //{
        //    var provincedata = from P in _context.Provinces
        //                       select P;
        //    return provincedata;

        //}

        // GET api/values/5
        [HttpGet("{userid}")]
        public IActionResult Get(string userid)
        {
            var loop = new List<object>();

            var userdata = _context.Users
                .Where(m => m.Id == userid).First();

            var provincialdepartment = _context.ProvincialDepartment
                .Where(m => m.DepartmentId == userdata.DepartmentId).First();

            //var subjectcentralpolicyprovincegroupsdata = _context.SubjectCentralPolicyProvinceGroups
            //    .Include(m => m.SubjectCentralPolicyProvince)
            //    .ThenInclude(m => m.CentralPolicyProvince)
            //    .ThenInclude(m => m.CentralPolicy)
            //    .Where(m => m.ProvincialDepartmentId == provincialdepartment.Id).ToList();

            var subjectcentralpolicyprovincegroupsdata = _context.CentralPolicies
                .Include(m => m.CentralPolicyProvinces)
                .ThenInclude(m => m.SubjectCentralPolicyProvinces)
                //.ThenInclude(m => m.SubjectCentralPolicyProvinceGroups)
                //.ThenInclude(m => m.ProvincialDepartment)
                .ToList();

            //var subjectcentralpolicyprovincegroupsdata = _context.SubjectCentralPolicyProvinceGroups
            //    .Where(x => x.ProvincialDepartmentId == provincialdepartment.Id)
            //    .GroupBy(g => new
            //    {
            //        SubjectCentralPolicyProvinces = g.SubjectCentralPolicyProvinceId,
            //        CentralPolicyProvinceID = g.SubjectCentralPolicyProvince.CentralPolicyProvinceId,
            //        CentralpolicyID = g.SubjectCentralPolicyProvince.CentralPolicyProvince.CentralPolicyId
            //    })
            //    .Select(g => new
            //    {
            //        g.Key.SubjectCentralPolicyProvinces,
            //        g.Key.CentralPolicyProvinceID,
            //        g.Key.CentralpolicyID
            //    })
            //    .ToList();

            List<object> termsList = new List<object>();
            List<long> termsList2 = new List<long>();
            List<long> termsList3 = new List<long>();

            var test = _context.SubjectCentralPolicyProvinceGroups
                .Where(x => x.ProvincialDepartmentId == provincialdepartment.Id)
                .GroupBy(g => new
                {
                    //subjectPolicyProvinceId = g.SubjectCentralPolicyProvinceId,
                    SubjectCentralPolicyProvinceGroupId = g.Id
                })
                .Select(g => new
                {
                    g.Key.SubjectCentralPolicyProvinceGroupId,
                    //g.Key.subjectPolicyProvinceId,
                   
                })
                .ToList();

            foreach(var data in test)
            {
                var test2 = _context.SubjectCentralPolicyProvinces
                    //.Where(x => x.Id == data.subjectPolicyProvinceId)
                    .GroupBy(g => new
                    {
                        CentralPolicyProvinceID = g.CentralPolicyProvinceId
                    })
                    .Select(g => new
                    {
                        g.Key.CentralPolicyProvinceID
                    })
                    .ToList();


                foreach(var gg in test2)
                {
                    long id = gg.CentralPolicyProvinceID;
                    termsList2.Add(id);
                }
            }

            foreach (long id in termsList2) {

                var test3 = _context.CentralPolicyProvinces
                    .Where(x => x.Id == id)
                    .GroupBy(g => new
                    {
                        centralPolicyID = g.CentralPolicyId
                    })
                    .Select(g => new
                    {
                        g.Key.centralPolicyID
                    })
                    .ToList();

                foreach (var gg in test3)
                {
                    long cid = gg.centralPolicyID;
                    termsList3.Add(cid);
                }
            }

            foreach( long idTest in termsList3)
            {
                var test4 = _context.CentralPolicies
                   .Where(x => x.Id == idTest)
                   .ToList();

                termsList.Add(new
                {
                    test4
                });
            }

            //foreach (var test in subjectcentralpolicyprovincegroupsdata)
            //{
            //    var data = _context.SubjectCentralPolicyProvinces
            //        .Where(x => x.Id == test.SubjectCentralPolicyProvinces)
            //        .Select(x => x.CentralPolicyProvinceId)
            //        .First();

            //    System.Console.WriteLine("PID: " + data);

            //    var provinceData = _context.CentralPolicyProvinces
            //       .Where(x => x.Id == data)
            //       .ToList();

            //    foreach(var testja in provinceData)
            //    {
            //        var centralPolicyData = _context.CentralPolicies
            //        .Where(x => x.Id == testja.CentralPolicyId)
            //        .ToList();

            //        loop.Add(new
            //        {
            //            centralPolicyData
            //        });
            //    }
            //}



            //var CentralPolicydata = _context.CentralPolicies
            //    .Include(m => m.CentralPolicyProvinces)
            //    .ThenInclude(m => m.SubjectCentralPolicyProvinces)
            //    .Where(x => x)

            //var subjectcentralpolicyprovincedata = _context.SubjectCentralPolicyProvinces
            //    .Where(m => m.Id == subjectcentralpolicyprovincegroupsdata.SubjectCentralPolicyProvinceId).First();

            //var CentralPolicyProvincedata = _context.CentralPolicyProvinces
            //    .Where(m => m.Id == subjectcentralpolicyprovincedata.CentralPolicyProvinceId).First();

            return Ok(termsList);

        }

        // POST api/values
        [HttpPost]
        public Province Post(string name, string link)
        {
            var date = DateTime.Now;

            var provincedata = new Province
            {
                Name = name,
                Link = link,
                CreatedAt = date
            };

            _context.Provinces.Add(provincedata);
            _context.SaveChanges();

            return provincedata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name, string link)
        {
            var province = _context.Provinces.Find(id);
            province.Name = name;
            province.Link = link;
            _context.Entry(province).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var provincedata = _context.Provinces.Find(id);

            _context.Provinces.Remove(provincedata);
            _context.SaveChanges();
        }
    }
}
