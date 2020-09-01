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
    public class ProvinceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public ProvinceController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {

            //var message = new Message(new string[] { "mrbuctico@gmail.com" }, "Test email", "This is the content from our email.");
            //_emailSender.SendEmail(message);

            var provincedata = _context.Provinces
                             .Include(p => p.Sectors)
                             .Include(p => p.ProvincesGroups)
                             .FirstOrDefault();
            return Ok(provincedata);


        }

        [HttpGet("centralpolicyprovinces")]
        public IActionResult Get2()
        {
            var provincedata = _context.Provinces;

            return Ok(provincedata);
        }
        // GET: api/values
        [HttpGet("getonlyprovince")]
        public IActionResult Getonlyprovince()
        {


            var provincedata = _context.Provinces
                .ToList();
            return Ok(provincedata);


        }

        //<!-- Get ภาค 20200720 -->
        [HttpGet("getsectordata")]
        public IActionResult getsectordata()
        {
            var sectordata = _context.Sectors;

            return Ok(sectordata);
        }
        //<!-- END Get ภาค 20200720 -->

        //<!-- Get กลุ่มจังหวัด 20200720 -->
        [HttpGet("getprovincegroupdata")]
        public IActionResult getprovincegroupdata()
        {
            var provincegroupdata = _context.ProvincesGroups;

            return Ok(provincegroupdata);
        }
        //<!-- END Get กลุ่มจังหวัด 20200720 -->

        // POST api/values
        [HttpPost]
        public Province Post([FromForm] ProviceRequest request)
        {
            var date = DateTime.Now;
            Console.WriteLine("province 1 :" + request.SectorId + " : " + request.Link + " : " + request.Name);
            var provincedata = new Province
            {
                Name = request.Name,
                NameEN = request.NameEN,
                Link = request.Link,
                SectorId = request.SectorId,
                ProvincesGroupId = request.ProvincesGroupId,
                ShortnameEN = request.ShortnameEN,
                ShortnameTH = request.ShortnameTH,
                CreatedAt = date
            };
            Console.WriteLine("province 2 :" + request.ProvincesGroupId);
            _context.Provinces.Add(provincedata);
            _context.SaveChanges();
            Console.WriteLine("province 3 :");
            return provincedata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromForm] ProviceRequest request, long id)
        {
            var province = _context.Provinces.Find(id);
            province.Name = request.Name;
            province.Link = request.Link;
            province.SectorId = request.SectorId;
            province.ProvincesGroupId = request.ProvincesGroupId;
            province.NameEN = request.NameEN;
            province.ShortnameEN = request.ShortnameEN;
            province.ShortnameTH = request.ShortnameTH;
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

public class ProviceRequest
{
    public long Id { get; set; }
    public long SectorId { get; set; }
    public long ProvincesGroupId { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public string NameEN { get; set; }

    public string ShortnameEN { get; set; }
    public string ShortnameTH { get; set; }
}
