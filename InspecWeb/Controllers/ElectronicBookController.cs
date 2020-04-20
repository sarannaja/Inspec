using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectronicBookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        // GET: api/ElectronicBook
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ElectronicBook/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ElectronicBook
        [HttpPost]
        public void Post(string id, string detail)
        {
            var ElectronicBookdata = new ElectronicBook
            {
                UserId = id,
                Detail = detail,
            };

            _context.ElectronicBooks.Add(ElectronicBookdata);
            _context.SaveChanges();
        }

        // PUT: api/ElectronicBook/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
