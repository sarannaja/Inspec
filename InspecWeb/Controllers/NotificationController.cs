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
        public Notification Post(long CentralPolicyId, long ProvinceId, string UserId, long Status, long xe)
        {
            var date = DateTime.Now;
            var notificationdata = new Notification(); //save ลง base แจ้งเตือน
            if (Status == 1 || Status == 5)
            {
                _context.Notifications.Add(new Notification
                {
                    UserID = UserId,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe
                });
                _context.SaveChanges();

            }
            if (Status == 2 || Status == 6 || Status == 9)
            {
                var inspectionplans = _context.InspectionPlanEvents
                    .Include(m => m.CentralPolicyEvents)
                    .ThenInclude(m => m.CentralPolicy)
                    .Where(m => m.ProvinceId == ProvinceId)
                    .Where(m => m.CentralPolicyEvents.Any(i => i.CentralPolicy.Id == CentralPolicyId))
                    .FirstOrDefault();

                _context.Notifications.Add(new Notification
                {
                    UserID = inspectionplans.CreatedBy,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe
                });
                _context.SaveChanges();
            }
            if (Status == 3 || Status == 4)
            {
                var users = _context.UserProvinces
               .Include(m => m.User)
               .Where(m => m.ProvinceId == ProvinceId)
               .Where(m => m.User.Role_id == 9 || m.User.Role_id == 5 || m.User.Role_id == 7)
               .ToList();

                foreach (var item in users)
                {
                    _context.Notifications.Add(new Notification
                    {
                        UserID = item.UserID,
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe
                    });
                    _context.SaveChanges();
                }
            }

            // แจ้งเตือนผู้ร่วมตรวจเมื่อได้รับสมุดตรวจ
            if (Status == 7)
            {
                System.Console.WriteLine("User NotiJa: " + UserId);
                System.Console.WriteLine("User NotiJa2: " + xe);
                _context.Notifications.Add(new Notification
                {
                    UserID = UserId,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe
                });
                _context.SaveChanges();
            }
            if (Status == 8)
            {
                var electData = _context.ElectronicBooks
                    .Where(x => x.Id == xe)
                    .FirstOrDefault();

                _context.Notifications.Add(new Notification
                {
                    UserID = electData.CreatedBy,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe
                });
                _context.SaveChanges();
            }
            //<!-- แจ้งข้อสั่งการ -->
            if (Status == 10)
            {
                //System.Console.WriteLine("st10 : " + CentralPolicyId + " : " + ProvinceId + " : " + UserId + " : " + Status + " : " + xe);

                var ExecutiveOrderAnswersdata = _context.ExecutiveOrderAnswers
                  .Where(m => m.ExecutiveOrderId == xe)
                  .ToList();

                foreach (var item in ExecutiveOrderAnswersdata)
                {
                    System.Console.WriteLine("st10 USERID : " + item.UserID);

                    _context.Notifications.Add(new Notification
                    {
                        UserID = item.UserID,
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe,
                    });

                    _context.SaveChanges();

                }
            }

            //<!-- ตอบรับข้อสั่งการ -->
            if (Status == 11)
            {
                // System.Console.WriteLine("st10 : " + CentralPolicyId + " : " + ProvinceId + " : " + UserId + " : " + Status + " : " + xe);

                var ExecutiveOrderdata = _context.ExecutiveOrders
                  .Where(m => m.Id == xe)
                  .First();


                _context.Notifications.Add(new Notification
                {
                    UserID = ExecutiveOrderdata.UserID,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe,
                });

                _context.SaveChanges();


            }
            //<!-- แจ้งเรื่องถึงผู้ตรวจ -->
            if (Status == 12)
            {
                var RequestOrderAnswers = _context.RequestOrderAnswers
                 .Where(m => m.RequestOrderId == xe)
                 .ToList();

                foreach (var item in RequestOrderAnswers)
                {
                    // System.Console.WriteLine("st10 USERID : " + item.UserID);

                    _context.Notifications.Add(new Notification
                    {
                        UserID = item.UserID,
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe,
                    });

                    _context.SaveChanges();

                }
            }

            //<!-- ตอบรับการแจ้งเรื่องมา --> 
            if (Status == 13)
            {
                var RequestOrderdata = _context.RequestOrders
                  .Where(m => m.Id == xe)
                  .First();


                _context.Notifications.Add(new Notification
                {
                    UserID = RequestOrderdata.UserID,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe,
                });

                _context.SaveChanges();

            }

            if (Status == 14)
            {
                var users = _context.Users

               .Where(m => m.Role_id == 8).ToList();

                foreach (var item in users)
                {
                    System.Console.WriteLine("USERID : " + item.Id);
                    _context.Notifications.Add(new Notification
                    {
                        UserID = item.Id,
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe,
                    });

                    _context.SaveChanges();
                    //System.Console.WriteLine("success");

                }
            }

            if (Status == 15)
            {
                var users = _context.Users

               .Where(m => m.Id == UserId).ToList();

                foreach (var item in users)
                {
                    System.Console.WriteLine("USERID : " + item.Id);
                    _context.Notifications.Add(new Notification
                    {
                        UserID = item.Id,
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe,
                    });

                    _context.SaveChanges();
                    //System.Console.WriteLine("success");

                }
            }

            if (Status == 16)
            {
                var users = _context.CentralPolicyUsers
                    .Where(m => m.InspectionPlanEventId == xe).ToList();

                foreach (var item in users)
                {
                    System.Console.WriteLine("USERID : " + item.Id);
                    _context.Notifications.Add(new Notification
                    {
                        UserID = item.UserId,
                        CentralPolicyId = item.CentralPolicyId,
                        ProvinceId = item.ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe,
                    });

                    _context.SaveChanges();
                }
            }

            // แจ้งเตือนผู้ว่าราชการจังหวัด หรือ หัวหน้าส่วนจังหวัด เมื่อได้รับสมุดตรวจ
            if (Status == 17)
            {
                var userProvinceRole4 = _context.Users
                    .Where(x => x.Role_id == 4 && x.UserProvince.Any(x => x.ProvinceId == ProvinceId))
                    .FirstOrDefault();

                _context.Notifications.Add(new Notification
                {
                    UserID = userProvinceRole4.Id,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe
                });
                _context.SaveChanges();

                var userProvinceRole5 = _context.Users
                   .Where(x => x.Role_id == 5 && x.UserProvince.Any(x => x.ProvinceId == ProvinceId))
                   .FirstOrDefault();

                _context.Notifications.Add(new Notification
                {
                    UserID = userProvinceRole5.Id,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe
                });
                _context.SaveChanges();
            }

            if (Status == 18)
            {
                var userProvincialDepartment = _context.Users
                    .Where(x => x.Role_id == 9 && x.ProvincialDepartmentId == xe && x.UserProvince.Any(x => x.ProvinceId == ProvinceId))
                    .ToList();

                foreach (var userData in userProvincialDepartment) {

                    _context.Notifications.Add(new Notification
                    {
                        UserID = userData.Id,
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe
                    });
                    _context.SaveChanges();
                }
            }

            return notificationdata;
        }

        [Route("api/[controller]/{id}")]
        [HttpPut]
        public void Put(long id, string update)
        {
            var Notification = _context.Notifications.Find(id);
            Notification.noti = 0;
            _context.Entry(Notification).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }


    }
}
