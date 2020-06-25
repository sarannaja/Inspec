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
    public class LogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public LogController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }


        [HttpGet]
        public IEnumerable<Log> Get()
        {
            System.Console.WriteLine("1");
            var logdata = _context.Logs.ToList();
            return logdata;
        }
    
        // POST api/values
       // [Route("api/[controller]")]
        [HttpPost]
        public Log Post(string UserId, string DatabaseName, string EventType, string ObjectType)
        {
           var date = DateTime.Now;
           var logdata = new Log(); //save ลง base
           
                _context.Logs.Add(new Log
                {
                    UserId = UserId, //คนที่ทำ
                    DatabaseName = DatabaseName, //table ที่ทำ
                    EventType = EventType, //create delete edit
                    EventDate = date,
                    ObjectType = ObjectType,
                  
                });
                _context.SaveChanges();
   
            
            return logdata;
        }

    }
}
