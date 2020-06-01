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
        public IActionResult getnotifications(string id)
        {
             var Notifications = _context.Notifications
                .Include(m => m.CentralPolicy)
                .Where(m => m.UserID == id);
             return Ok(Notifications); 
        }

        [HttpGet("api/[controller]/[action]/{id}")]
        public IActionResult getnotificationscount(string id)
        {
            var Notifications = _context.Notifications
               .Include(m => m.CentralPolicy)
               .Where(m => m.UserID == id)
               .Where(m => m.noti == 1);
            return Ok(Notifications);
        }

        [HttpGet("api/[controller]/[action]/{id}")]
        public IActionResult getnotificationsforexecutiveorder(long id)
        {
            var executiveorder = _context.ExecutiveOrders       
               .Where(m => m.Id == id);
            return Ok(executiveorder);

        }

        // POST api/values
        [Route("api/[controller]")]
        [HttpPost]
        public Notification Post(long CentralPolicyId, long ProvinceId,string UserId, long Status,long xe)
        {
            var date = DateTime.Now;
            var notificationdata = new Notification(); //save ลง base แจ้งเตือน
            if (Status == 1 ||Status == 5)
            {
                notificationdata.UserID = UserId;
                notificationdata.CentralPolicyId = CentralPolicyId;
                notificationdata.ProvinceId = ProvinceId;
                notificationdata.status = Status;
                notificationdata.noti = 1;
                notificationdata.CreatedAt = date;
                notificationdata.xe = xe;
                _context.Notifications.Add(notificationdata);
                _context.SaveChanges();
   
            }
            if (Status == 2 || Status == 6 || Status == 8 || Status == 9)
            {
                var inspectionplans = _context.InspectionPlanEvents                   
                    .Include(m => m.CentralPolicyEvents)
                    .ThenInclude(m => m.CentralPolicy)
                    .Where(m => m.ProvinceId == ProvinceId)
                    .Where(m => m.CentralPolicyEvents.Any(i => i.CentralPolicy.Id == CentralPolicyId))
                    .FirstOrDefault();
                                      
                notificationdata.UserID = inspectionplans.CreatedBy;
                notificationdata.CentralPolicyId = CentralPolicyId;
                notificationdata.ProvinceId = ProvinceId;
                notificationdata.status = Status;
                notificationdata.noti = 1;
                notificationdata.CreatedAt = date;
                notificationdata.xe = xe;
                _context.Notifications.Add(notificationdata);
                _context.SaveChanges();
            }
            if (Status == 3 || Status == 4)
            {
                var users = _context.UserProvinces
               .Include(m => m.User)
               .Where(m => m.ProvinceId == ProvinceId && m.User.Role_id == 5);
               
                foreach (var item in users)
                {
                    notificationdata.UserID = item.UserID;
                    notificationdata.CentralPolicyId = CentralPolicyId;
                    notificationdata.ProvinceId = ProvinceId;
                    notificationdata.status = Status;
                    notificationdata.noti = 1;
                    notificationdata.CreatedAt = date;
                    notificationdata.xe = xe;
                    _context.Notifications.Add(notificationdata);
                    _context.SaveChanges();
                }
            }

            if (Status == 7)
            {

            }

            if (Status == 10)
            {
                System.Console.WriteLine("st10 : " + CentralPolicyId + " : "+ ProvinceId + " : " + UserId + " : " + Status + " : " + xe);
                var users = _context.UserProvinces
                .Include(m => m.User)
               .Where(m => m.ProvinceId == ProvinceId && m.User.Role_id == 3);

                foreach (var item in users)
                {
                    System.Console.WriteLine("USERID : " + item.UserID);
                    notificationdata.UserID = item.UserID;
                    notificationdata.CentralPolicyId = CentralPolicyId;
                    notificationdata.ProvinceId = ProvinceId;
                    notificationdata.status = Status;
                    notificationdata.noti = 1;
                    notificationdata.CreatedAt = date;
                    notificationdata.xe = xe;
                    //System.Console.WriteLine("1");
                    _context.Notifications.Add(notificationdata);
                    //System.Console.WriteLine("2");

                    _context.SaveChanges();
                    //System.Console.WriteLine("3");
                }
               
               
            }
            if (Status == 11)
            {
                var ExecutiveOrders = _context.ExecutiveOrders 
                .Where(m => m.Id == xe)
                .FirstOrDefault();

                notificationdata.UserID = ExecutiveOrders.UserId;
                notificationdata.CentralPolicyId = CentralPolicyId;
                notificationdata.ProvinceId = ProvinceId;
                notificationdata.status = Status;
                notificationdata.noti = 1;
                notificationdata.CreatedAt = date;
                notificationdata.xe = xe;
                _context.Notifications.Add(notificationdata);
                _context.SaveChanges();

            }
            if (Status == 12)
            {
                System.Console.WriteLine("st12 : " + CentralPolicyId + " : " + ProvinceId + " : " + UserId + " : " + Status + " : " + xe);
                var users = _context.UserProvinces
                .Include(m => m.User)
               .Where(m => m.ProvinceId == ProvinceId && m.User.Role_id == 3);

                foreach (var item in users)
                {
                    System.Console.WriteLine("USERID : " + item.UserID);
                    notificationdata.UserID = item.UserID;
                    notificationdata.CentralPolicyId = CentralPolicyId;
                    notificationdata.ProvinceId = ProvinceId;
                    notificationdata.status = Status;
                    notificationdata.noti = 1;
                    notificationdata.CreatedAt = date;
                    notificationdata.xe = xe;
                    //System.Console.WriteLine("1");
                    _context.Notifications.Add(notificationdata);
                    //System.Console.WriteLine("2");

                    _context.SaveChanges();
                    //System.Console.WriteLine("3");
                }


            }
            if (Status == 13)
            {
                var RequestOrders = _context.RequestOrders
               .Where(m => m.Id == xe)
               .FirstOrDefault();

                notificationdata.UserID = RequestOrders.UserId;
                notificationdata.CentralPolicyId = CentralPolicyId;
                notificationdata.ProvinceId = ProvinceId;
                notificationdata.status = Status;
                notificationdata.noti = 1;
                notificationdata.CreatedAt = date;
                notificationdata.xe = xe;
                _context.Notifications.Add(notificationdata);
                _context.SaveChanges();
                //3
            }

            return notificationdata;
        }

        // 
        [Route("api/[controller]/{id}")]
        [HttpPut]
        public void Put(long id,string update) 
        {
            var Notification = _context.Notifications.Find(id);
                Notification.noti = 0;
                _context.Entry(Notification).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

        }


    }
}
