using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
//using InspecWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutiveOrderController : Controller
    {
        public static IWebHostEnvironment _environment;


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private readonly ApplicationDbContext _context;

        public ExecutiveOrderController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        [HttpGet]
        public IEnumerable<ExecutiveOrder> Get()
        {

            var executivedata = _context.ExecutiveOrders.ToList();
            return executivedata;
        }

        [HttpGet("{id}")]
        public object Get(long id)
        {       
            var centralpolicydata = _context.CentralPolicies
                .Include(m => m.CentralPolicyProvinces)
                .ThenInclude(x => x.Province)
                .Include(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicyFiles)
                .Include(m => m.ExecutiveOrders)
                .Include(m => m.Subjects)
                .ThenInclude(m => m.Subquestions)
                .Where(m => m.Id == id).FirstOrDefault();

            return Ok(centralpolicydata);
            //return "value";
        }
        [HttpPost]
        public object Post([FromForm] string name,long centralpolicyid,long provinceid )
        {
            System.Console.WriteLine("centralpolicy: " + centralpolicyid);
            System.Console.WriteLine("provinceid: " + provinceid);
            var cabinedata = new ExecutiveOrder
            {
               
                DetailExecutiveOrder = name,
                CentralPolicyId = centralpolicyid,
                ProvinceId = provinceid

            };

           var data = _context.ExecutiveOrders.Add(cabinedata);
            _context.SaveChanges();

            return Ok(new { data });
        }

        [HttpGet("province/{id}")]
        public object Getprovince(long id)
        {
            var result = new List<object>();


            var centralpolicyprovincedata = _context.CentralPolicyProvinces
                .Include(m => m.Province)
                .Where(m => m.CentralPolicyId == id)
                .ToList();

            //foreach (var provinceid in centralpolicyprovincedata)
            //{
            //    var provincename = _context.Provinces
            //        .Where(x => x.Id == provinceid)
            //        .ToList();

            //    result.Add(

            //        provincename
            //    );
            //}

            return Ok(centralpolicyprovincedata);
            //return "value";
        }
        [HttpGet("detail/{id}")]//new///
        public IActionResult Getexecutive(long id)
        {
            var executiveOrderdata = _context.ExecutiveOrders
                /*.Include(m => m.DetailExecutiveOrder)
                .Include(m => m.Province)*/
                .Where(m => m.CentralPolicyId == id);

            return Ok(executiveOrderdata);
            //return "value";
        }
    }
    }

