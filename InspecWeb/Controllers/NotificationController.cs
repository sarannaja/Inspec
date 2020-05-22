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
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public NotificationController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        [HttpGet("api/[controller]/[action]/{id}")]
        public IEnumerable<Notification> getnotifications(string id)
        {
            var Notifications = _context.Notifications.Include(m => m.CentralPolicy).Where(m => m.UserID == id);
            //var centraPolicy = _context.CentralPolicies
            //       .Include(m => m.CentralPolicyUser)
            //       .Where(m => m.Id == 1);
             return Notifications;
            //return centraPolicy;
        }

        //// POST api/values
        //[Route("api/[controller]")]
        //[HttpPost]
        //public Notification Post(long CentralPolicyId, long ProvinceId, long status)
        //{
        //    var date = DateTime.Now;
        //    var notificationdata = new Notification(); //save ลง base แจ้งเตือน
        //    if (status == 1)
        //    {
        //        var centraPolicy = _context.CentralPolicies
        //            .Include(m => m.CentralPolicyUser)
        //            .Where(m => m.Id == CentralPolicyId);
            
        //        foreach (var item in centraPolicy)
        //        {
        //            notificationdata.UserID = item.CentralPolicyUser,
        //            notificationdata.CentralPolicyId = CentralPolicyId;
        //            notificationdata.ProvinceId = ProvinceId;
        //            notificationdata.status = status;
        //            notificationdata.noti = 1;
        //            notificationdata.CreatedAt = date;

        //            _context.Notifications.Add(notificationdata);
        //            _context.SaveChanges();
        //        }
        //    }
        //    return notificationdata;
        //}

        // 
        [Route("api/[controller]/{id}")]
        [HttpPut]
        public void Put(long id) 
        {
            var Notification = _context.Notifications.Find(id);
                Notification.noti = 1;
                _context.Entry(Notification).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

        }


    }
}
