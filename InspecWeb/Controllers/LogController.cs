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
    public class LogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LogController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IEnumerable<Log> Get()
        {
            var logdata = _context.Logs
                .Include(m => m.User)
                .OrderByDescending(m => m.Id)
                .ToList();
            return logdata;
        }
    
        // POST api/values
       // [Route("api/[controller]")]
        [HttpPost]
        public Log Post(string UserId, string DatabaseName, string EventType, string ObjectType,long Allid)
        {
           var date = DateTime.Now;
           var logdata = new Log(); //save ลง base
           
                _context.Logs.Add(new Log
                {
                    UserId = UserId, //คนที่ทำ
                    DatabaseName = DatabaseName, //table ที่ทำ
                    EventType = EventType, //create delete edit
                    EventDate = date,
                    Detail = ObjectType,
                    Allid = Allid,

                });
                _context.SaveChanges();
            return logdata;
        }

    }
}
