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
        public IQueryable<UserProvince> getnotifications(string id)
        {
            //// var Notifications = _context.Notifications.Include(m => m.CentralPolicy).Where(m => m.UserID == id);
            //var centraPolicy = _context.CentralPolicies
            //       .Include(m => m.CentralPolicyUser)
            //       .Where(m => m.Id == 1);
            //// return Notifications;
            //return centraPolicy;
            //var inspectionplans = _context.InspectionPlanEvents

            //                   .Include(m => m.CentralPolicyEvents)
            //                   .ThenInclude(m => m.CentralPolicy)
            //                   .Where(m => m.ProvinceId == 1)
            //                   .Where(m => m.CentralPolicyEvents.Any(i => i.CentralPolicy.Id == 1)).FirstOrDefault();
            //return inspectionplans;

            //var centraPolicy = _context.CentralPolicyProvinces
            //       .Include(m => m.CentralPolicy)
            //       .Where(m => m.CentralPolicyId == 1)
            //       .Where(m => m.ProvinceId == 1)
            //       .FirstOrDefault();

            var users = _context.UserProvinces
                .Include(m => m.User)
                .Where(m => m.ProvinceId == 17 && m.User.Role_id == 5);

            return users;
        }

        // POST api/values
        [Route("api/[controller]")]
        [HttpPost]
        public Notification Post(long CentralPolicyId, long ProvinceId,string UserId, long Status)
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
                    _context.Notifications.Add(notificationdata);
                    _context.SaveChanges();
                }
            }
          
      
            if (Status == 10)
            {
                //2 
            }
            if (Status == 11)
            {

            }
            if (Status == 12)
            {
                //2 
            }
            if (Status == 13)
            {
               //3
            }

            return notificationdata;
        }

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
