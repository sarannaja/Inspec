using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using InspecWeb.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{

    public class NotificationController : Controller
    {
        private ExternalOrganizationController _externalOrganizationController;
        private readonly ApplicationDbContext _context;

        public NotificationController(ApplicationDbContext context, ExternalOrganizationController externalOrganizationController)
        {
            _context = context;
            _externalOrganizationController = externalOrganizationController;
        }

        [HttpGet("api/[controller]/[action]/{id}")]
        public IActionResult getnotifications(string id)
        {
            var Notifications = _context.Notifications
               .Include(m => m.User)
               .Include(m => m.Notificationcreateby)
               .ThenInclude(m => m.UserCreate)
               .Include(m => m.CentralPolicy)
               .Where(m => m.UserID == id).OrderByDescending(m => m.Id);
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
        public Notification Post(long CentralPolicyId, long ProvinceId, string UserId, long Status, long xe, string title, string createby)
        {
            System.Console.WriteLine("Status : " + Status);

            var CentralPolicyData = _context.CentralPolicies.Where(m => m.Id == CentralPolicyId).FirstOrDefault();

            var date = DateTime.Now;
            var notificationdata = new Notification(); //save ลง base แจ้งเตือน

            if (Status == 1)
            {
                var data = new Notification

                {
                    UserID = UserId,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe
                };
                _context.Notifications.Add(data);
                _context.SaveChanges();

                var data2 = new Notificationcreateby
                {
                    NotificationId = data.Id,
                    CreateBy = createby
                };
                _context.Notificationcreateby.Add(data2);
                _context.SaveChanges();

                _externalOrganizationController.SendNotification(UserId, "มีคำเชิญ" + CentralPolicyData.Title);

            }

            if (Status == 5) // ||Status == 1
            {
                System.Console.WriteLine("XE : " + xe);
                var data = _context.SubjectGroupPeopleQuestions
                    .Where(m => m.SubjectGroupId == xe)
                    .FirstOrDefault();

                System.Console.WriteLine("CentralPolicyEventId : " + data.CentralPolicyEventId);

                var data2 = _context.CentralPolicyEvents
                    .Where(m => m.Id == data.CentralPolicyEventId)
                    .FirstOrDefault();

                var data3 = _context.CentralPolicyUsers
                    .Include(m => m.User)
                    .Where(m => m.CentralPolicyId == data2.CentralPolicyId && m.InspectionPlanEventId == data2.InspectionPlanEventId)
                    .Where(m => m.User.Role_id == 7)
                    //.Where(m => m.ProvinceId == data2.InspectionPlanEvent.ProvinceId)
                    //.Where(m => m.InspectionPlanEventId == data2.InspectionPlanEventId)
                    .ToList();

                foreach (var item in data3)
                {

                    System.Console.WriteLine("UserId : " + item.UserId);

                    var data4 = new Notification
                    {
                        UserID = item.UserId,
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe
                    };
                    _context.Notifications.Add(data4);
                    _context.SaveChanges();

                    var data5 = new Notificationcreateby
                    {
                        NotificationId = data4.Id,
                        CreateBy = createby
                    };
                    _context.Notificationcreateby.Add(data5);
                    _context.SaveChanges();
                    _externalOrganizationController.SendNotification(item.UserId, "มีประเด็น/คำถาม(ภาคประชาชน)" + CentralPolicyData.Title);
                }

            }

            if (Status == 2 || Status == 26 || Status == 27)
            {
                var inspectionplans = _context.InspectionPlanEvents
                .Include(m => m.CentralPolicyEvents)
                .ThenInclude(m => m.CentralPolicy)
                .Where(m => m.Id == xe)
                .FirstOrDefault();

                var data = new Notification
                {
                    UserID = inspectionplans.CreatedBy,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe
                };
                _context.Notifications.Add(data);
                _context.SaveChanges();

                var data2 = new Notificationcreateby
                {
                    NotificationId = data.Id,
                    CreateBy = createby
                };
                _context.Notificationcreateby.Add(data2);
                _context.SaveChanges();

                if (Status == 2)
                {
                    _externalOrganizationController.SendNotification(UserId, "ตอบรับคำเชิญ" + CentralPolicyData.Title);
                }
                else if (Status == 26)
                {
                    _externalOrganizationController.SendNotification(UserId, "ปฏิเสธคำเชิญ" + CentralPolicyData.Title);
                }
                else if (Status == 27)
                {
                    _externalOrganizationController.SendNotification(UserId, "มอบหมายให้ผู้อื่น" + CentralPolicyData.Title);
                }
            }

            if (Status == 6)
            {

                var inspectionplans = _context.InspectionPlanEvents
                    .Include(m => m.CentralPolicyEvents)
                    .ThenInclude(m => m.CentralPolicy)
                    .Where(m => m.ProvinceId == ProvinceId)
                    .Where(m => m.CentralPolicyEvents.Any(i => i.CentralPolicy.Id == CentralPolicyId))
                    .FirstOrDefault();

                var data = new Notification
                {
                    UserID = inspectionplans.CreatedBy,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe
                };
                _context.Notifications.Add(data);
                _context.SaveChanges();

                var data2 = new Notificationcreateby
                {
                    NotificationId = data.Id,
                    CreateBy = createby
                };
                _context.Notificationcreateby.Add(data2);
                _context.SaveChanges();

                _externalOrganizationController.SendNotification(UserId, "ตอบประเด็นคำถามเรียบร้อย(ผู้ตรวจราชการ)" + CentralPolicyData.Title);

            }

            //if (Status == 3 || Status == 4)
            //{
            //    var users = _context.UserProvinces
            //   .Include(m => m.User)
            //   .Where(m => m.ProvinceId == ProvinceId)
            //   .Where(m => m.User.Role_id == 9 || m.User.Role_id == 5)
            //   .ToList();

            //    foreach (var item in users)
            //    {
            //        _context.Notifications.Add(new Notification
            //        {
            //            UserID = item.UserID,
            //            CentralPolicyId = CentralPolicyId,
            //            ProvinceId = ProvinceId,
            //            status = Status,
            //            noti = 1,
            //            CreatedAt = date,
            //            xe = xe
            //        });
            //        _context.SaveChanges();
            //    }
            //}

            if (Status == 3)
            {
                var users = _context.UserProvinces
               .Include(m => m.User)
               .Where(m => m.ProvinceId == ProvinceId)
               .Where(m => m.User.Role_id == 9 || m.User.Role_id == 5)
               .ToList();

                foreach (var item in users)
                {
                    var data = new Notification
                    {
                        UserID = item.UserID,
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe
                    };
                    _context.Notifications.Add(data);
                    _context.SaveChanges();

                    var data2 = new Notificationcreateby
                    {
                        NotificationId = data.Id,
                        CreateBy = createby
                    };
                    _context.Notificationcreateby.Add(data2);
                    _context.SaveChanges();
                    _externalOrganizationController.SendNotification(item.UserID, "ผู้ตรวจกำลังจะตรวจ" + CentralPolicyData.Title);
                }

            }

            if (Status == 4)
            {
                var subjectgroup = _context.SubjectGroups
                    .Where(m => m.Id == xe).FirstOrDefault();

                var SubjectCentralPolicyProvincesdatas = _context.SubjectCentralPolicyProvinces
                    .Where(m => m.SubjectGroupId == subjectgroup.Id).ToList();

                foreach (var SubjectCentralPolicyProvincesdata in SubjectCentralPolicyProvincesdatas)
                {
                    var SubquestionCentralPolicyProvincedata = _context.SubquestionCentralPolicyProvinces
                        .Where(m => m.SubjectCentralPolicyProvinceId == SubjectCentralPolicyProvincesdata.Id).FirstOrDefault();

                    var SubjectCentralPolicyProvinceGroupdatas = _context.SubjectCentralPolicyProvinceGroups
                        .Where(m => m.SubquestionCentralPolicyProvinceId == SubquestionCentralPolicyProvincedata.Id).ToList();

                    foreach (var SubjectCentralPolicyProvinceGroupdata in SubjectCentralPolicyProvinceGroupdatas)
                    {
                        // var users = _context.UserProvinces
                        //.Include(m => m.User)
                        //.Where(m => m.ProvinceId == ProvinceId)
                        //.Where(m => m.User.Role_id == 9)
                        //.ToList();
                        var users = _context.Users
                       .Where(m => m.ProvincialDepartmentId == SubjectCentralPolicyProvinceGroupdata.ProvincialDepartmentId)
                       .Where(m => m.Role_id == 9)
                       .ToList();

                        foreach (var item in users)
                        {
                            var data = new Notification
                            {
                                UserID = item.Id,
                                CentralPolicyId = CentralPolicyId,
                                ProvinceId = ProvinceId,
                                status = Status,
                                noti = 1,
                                CreatedAt = date,
                                xe = xe
                            };
                            _context.Notifications.Add(data);
                            _context.SaveChanges();

                            var data2 = new Notificationcreateby
                            {
                                NotificationId = data.Id,
                                CreateBy = createby
                            };
                            _context.Notificationcreateby.Add(data2);
                            _context.SaveChanges();
                            _externalOrganizationController.SendNotification(item.Id, "มีประเด็น/คำถาม(หน่วยรับตรวจ)" + CentralPolicyData.Title);
                        }

                    }

                }

            }

            // แจ้งเตือนผู้ร่วมตรวจเมื่อได้รับสมุดตรวจ
            if (Status == 7)
            {
                System.Console.WriteLine("User NotiJa: " + UserId);
                System.Console.WriteLine("User NotiJa2: " + xe);
                var data = new Notification
                {
                    UserID = UserId,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe
                };
                _context.Notifications.Add(data);
                _context.SaveChanges();

                var data2 = new Notificationcreateby
                {
                    NotificationId = data.Id,
                    CreateBy = createby
                };
                _context.Notificationcreateby.Add(data2);
                _context.SaveChanges();

                _externalOrganizationController.SendNotification(UserId, "สมุดตรวจอีเล็กทรอนิกส์" + CentralPolicyData.Title);
            }

            // แจ้งเตือนผู้ตรวจเขต เมื่อผู้ว่าราชการจังหวัดหรือ หัวหน้าส่วนจังหวัด รับทราบรายการสมุดตรวจ
            if (Status == 8)
            {
                var electData = _context.ElectronicBooks
                    .Where(x => x.Id == xe)
                    .FirstOrDefault();

                var data = new Notification
                {
                    UserID = electData.CreatedBy,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe
                };
                _context.Notifications.Add(data);
                _context.SaveChanges();

                var data2 = new Notificationcreateby
                {
                    NotificationId = data.Id,
                    CreateBy = createby
                };
                _context.Notificationcreateby.Add(data2);
                _context.SaveChanges();
                _externalOrganizationController.SendNotification(UserId, "ผู้ว่าราชการจังหวัดลงลายมือชื่อเรียบร้อย" + CentralPolicyData.Title);
            }
            //<!-- แจ้งข้อสั่งการ -->
            if (Status == 10)
            {
                System.Console.WriteLine("st10 1: " + CentralPolicyId + " : " + ProvinceId + " : " + UserId + " : " + Status + " : " + xe + " : " + title);

                var ExecutiveOrderAnswersdata = _context.ExecutiveOrderAnswers
                  .Where(m => m.ExecutiveOrderId == xe)
                  .ToList();

                foreach (var item in ExecutiveOrderAnswersdata)
                {
                    System.Console.WriteLine("st10 2 : " + item.UserID);

                    // var userregions = _context.UserRegions
                    //.Include(m => m.User)
                    //.Where(m => m.UserID == item.UserID)
                    //.First();

                    // var userother = _context.Users
                    //   .Include(m => m.UserRegion)
                    //   .Where(m => m.Role_id == userregions.User.Role_id)
                    //   .First();

                    var data = new Notification
                    {
                        UserID = item.UserID,
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe,
                        title = title,
                    };
                    _context.Notifications.Add(data);
                    _context.SaveChanges();


                    var data2 = new Notificationcreateby
                    {
                        NotificationId = data.Id,
                        CreateBy = createby
                    };
                    _context.Notificationcreateby.Add(data2);
                    _context.SaveChanges();
                    System.Console.WriteLine("st10 3 : " + createby);
                    _externalOrganizationController.SendNotification(item.UserID, "ข้อสั่งการ" + title);
                }
            }

            //<!-- ตอบรับข้อสั่งการ -->
            if (Status == 11)
            {
                // System.Console.WriteLine("st10 : " + CentralPolicyId + " : " + ProvinceId + " : " + UserId + " : " + Status + " : " + xe);

                var ExecutiveOrderdata = _context.ExecutiveOrders
                  .Where(m => m.Id == xe)
                  .First();


                var data = new Notification
                {
                    UserID = ExecutiveOrderdata.UserID,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe,
                    title = title,
                };

                _context.Notifications.Add(data);
                _context.SaveChanges();

                var data2 = new Notificationcreateby
                {
                    NotificationId = data.Id,
                    CreateBy = createby
                };
                _context.Notificationcreateby.Add(data2);
                _context.SaveChanges();
                _externalOrganizationController.SendNotification(ExecutiveOrderdata.UserID, "รับทราบข้อสั่งการ" + title);

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

                    var data = new Notification
                    {
                        UserID = item.UserID,
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe,
                        title = title,
                    };

                    _context.Notifications.Add(data);
                    _context.SaveChanges();

                    var data2 = new Notificationcreateby
                    {
                        NotificationId = data.Id,
                        CreateBy = createby
                    };
                    _context.Notificationcreateby.Add(data2);
                    _context.SaveChanges();
                    _externalOrganizationController.SendNotification(item.UserID, "แจ้งคำร้อง" + title);
                }
            }

            //<!-- ตอบรับการแจ้งเรื่องมา --> 
            if (Status == 13)
            {
                var RequestOrderdata = _context.RequestOrders
                  .Where(m => m.Id == xe)
                  .First();


                var data = new Notification
                {
                    UserID = RequestOrderdata.UserID,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe,
                    title = title,
                };

                _context.Notifications.Add(data);
                _context.SaveChanges();

                var data2 = new Notificationcreateby
                {
                    NotificationId = data.Id,
                    CreateBy = createby
                };
                _context.Notificationcreateby.Add(data2);
                _context.SaveChanges();
                _externalOrganizationController.SendNotification(RequestOrderdata.UserID, "รับทราบแจ้งคำร้อง" + title);
            }

            if (Status == 14)
            {
                var users = _context.Users

               .Where(m => m.Role_id == 8).ToList();

                foreach (var item in users)
                {
                    System.Console.WriteLine("USERID : " + item.Id);
                    var data = new Notification
                    {
                        UserID = item.Id,
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe,
                    };

                    _context.Notifications.Add(data);
                    _context.SaveChanges();

                    var data2 = new Notificationcreateby
                    {
                        NotificationId = data.Id,
                        CreateBy = createby
                    };
                    _context.Notificationcreateby.Add(data2);
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
                    var data = new Notification
                    {
                        UserID = item.Id,
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe,
                    };

                    _context.Notifications.Add(data);
                    _context.SaveChanges();

                    var data2 = new Notificationcreateby
                    {
                        NotificationId = data.Id,
                        CreateBy = createby
                    };
                    _context.Notificationcreateby.Add(data2);
                    _context.SaveChanges();
                    //System.Console.WriteLine("success");

                }
            }
            //ส่งเชิญในกำหนดการ
            if (Status == 16)
            {
                var users = _context.CentralPolicyUsers
                    .Where(m => m.InspectionPlanEventId == xe).ToList();

                foreach (var item in users)
                {
                    System.Console.WriteLine("USERID : " + item.Id);
                    var data = new Notification
                    {
                        UserID = item.UserId,
                        CentralPolicyId = item.CentralPolicyId,
                        ProvinceId = item.ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe,
                    };

                    _context.Notifications.Add(data);
                    _context.SaveChanges();

                    var data2 = new Notificationcreateby
                    {
                        NotificationId = data.Id,
                        CreateBy = createby
                    };
                    _context.Notificationcreateby.Add(data2);
                    _context.SaveChanges();
                    _externalOrganizationController.SendNotification(item.UserId, "มีคำเชิญ" + CentralPolicyData.Title);
                }
            }

            // แจ้งเตือนผู้ว่าราชการจังหวัด หรือ หัวหน้าส่วนจังหวัด เมื่อได้รับสมุดตรวจ
            // รอแก้ถ้าเป็นสมุดตรวจสร้างเองให้เอา electronicBook.centralPolicy มาใช้แทน xe
            if (Status == 17)
            {
                System.Console.WriteLine("in 17" + ProvinceId);
                var userProvinceRole4 = _context.Users
                    .Where(x => x.Role_id == 4 && x.UserProvince.Any(x => x.ProvinceId == ProvinceId))
                    .FirstOrDefault();

                var data = new Notification
                {
                    UserID = userProvinceRole4.Id,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe
                };

                _context.Notifications.Add(data);
                _context.SaveChanges();

                var data2 = new Notificationcreateby
                {
                    NotificationId = data.Id,
                    CreateBy = createby
                };
                _context.Notificationcreateby.Add(data2);
                _context.SaveChanges();

                _externalOrganizationController.SendNotification(UserId, "สมุดตรวจอีเล็กทรอนิกส์" + CentralPolicyData.Title);

                var userProvinceRole5 = _context.Users
                   .Where(x => x.Role_id == 5 && x.UserProvince.Any(x => x.ProvinceId == ProvinceId))
                   .FirstOrDefault();

                var data3 = new Notification
                {
                    UserID = userProvinceRole5.Id,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe
                };

                _context.Notifications.Add(data3);
                _context.SaveChanges();

                var data4 = new Notificationcreateby
                {
                    NotificationId = data3.Id,
                    CreateBy = createby
                };
                _context.Notificationcreateby.Add(data4);
                _context.SaveChanges();

                _externalOrganizationController.SendNotification(UserId, "สมุดตรวจอีเล็กทรอนิกส์" + CentralPolicyData.Title);
                System.Console.WriteLine("Success Noti 17");
            }

            // แจ้งเตือนหน่วยรับตรวจ เมื่อได้รับสมุดตรวจ
            // รอแก้ถ้าเป็นสมุดตรวจสร้างเองให้เอา electronicBook.centralPolicy มาใช้แทน xe
            if (Status == 18)
            {
                System.Console.WriteLine("in 18");
                List<ApplicationUser> termsList = new List<ApplicationUser>();
                var electronicBookProvince = _context.ElectronicBookAccepts
                    .Where(x => x.ElectronicBookId == xe)
                    .Select(x => x.ProvinceId)
                    .ToList();
                System.Console.WriteLine("in 18.1");

                foreach (var electProvinceId in electronicBookProvince)
                {
                    System.Console.WriteLine("in 18.2");
                    var userProvincialDepartment = _context.Users
                   .Where(x => x.Role_id == 9 && x.ProvincialDepartmentId == ProvinceId && x.UserProvince.Any(x => x.ProvinceId == electProvinceId))
                   .ToList();

                    foreach (var user in userProvincialDepartment)
                    {
                        System.Console.WriteLine("in 18.3");
                        termsList.Add(user);
                    }
                }
                System.Console.WriteLine("in 18.4");

                foreach (var userData in termsList)
                {
                    System.Console.WriteLine("in 18.5");
                    var data = new Notification
                    {
                        UserID = userData.Id,
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        status = Status,
                        noti = 1,
                        CreatedAt = date,
                        xe = xe
                    };
                    _context.Notifications.Add(data);
                    _context.SaveChanges();

                    var data2 = new Notificationcreateby
                    {
                        NotificationId = data.Id,
                        CreateBy = createby
                    };
                    _context.Notificationcreateby.Add(data2);
                    _context.SaveChanges();
                    _externalOrganizationController.SendNotification(UserId, "สมุดตรวจอีเล็กทรอนิกส์" + CentralPolicyData.Title);
                    System.Console.WriteLine("Success Noti 18");
                }
            }

            //มอบหมาย
            if (Status == 19)
            {
                var users = _context.Users
                     .Where(m => m.Id == UserId).First();


                System.Console.WriteLine("USERID : " + users.Id);
                var data = new Notification
                {
                    UserID = users.Id,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe,
                };

                _context.Notifications.Add(data);
                _context.SaveChanges();

                var data2 = new Notificationcreateby
                {
                    NotificationId = data.Id,
                    CreateBy = createby
                };
                _context.Notificationcreateby.Add(data2);
                _context.SaveChanges();

                _externalOrganizationController.SendNotification(UserId, "ถูกมอบหมาย" + CentralPolicyData.Title);
            }

            if (Status == 20)
            {
                var data = new Notification
                {
                    UserID = UserId,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe,
                };

                _context.Notifications.Add(data);
                _context.SaveChanges();

                var data2 = new Notificationcreateby
                {
                    NotificationId = data.Id,
                    CreateBy = createby
                };
                _context.Notificationcreateby.Add(data2);
                _context.SaveChanges();
                _externalOrganizationController.SendNotification(UserId, "รายงานผลการตรวจราชการ" + CentralPolicyData.Title);
            }

            if (Status == 9)
            {
                var userData = _context.ImportReports
                    .Where(x => x.Id == xe)
                    .FirstOrDefault();


                System.Console.WriteLine("USERID : " + userData.CreatedBy);
                var data = new Notification
                {
                    UserID = userData.CreatedBy,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe,
                };

                _context.Notifications.Add(data);
                _context.SaveChanges();

                var data2 = new Notificationcreateby
                {
                    NotificationId = data.Id,
                    CreateBy = createby
                };
                _context.Notificationcreateby.Add(data2);
                _context.SaveChanges();

                _externalOrganizationController.SendNotification(UserId, "ข้อสั่งการในรายงานผลการตรวจราชการ" + CentralPolicyData.Title);
            }


            if (Status == 25)
            {
                var subjectgroup = _context.SubjectGroups
                    .Where(m => m.Id == xe).FirstOrDefault();

                var SubjectCentralPolicyProvincesdatas = _context.SubjectCentralPolicyProvinces
                    .Where(m => m.SubjectGroupId == subjectgroup.Id).ToList();

                foreach (var SubjectCentralPolicyProvincesdata in SubjectCentralPolicyProvincesdatas)
                {
                    var SubquestionCentralPolicyProvincedata = _context.SubquestionCentralPolicyProvinces
                        .Where(m => m.SubjectCentralPolicyProvinceId == SubjectCentralPolicyProvincesdata.Id).FirstOrDefault();

                    var SubjectCentralPolicyProvinceGroupdatas = _context.SubjectCentralPolicyProvinceGroups
                        .Where(m => m.SubquestionCentralPolicyProvinceId == SubquestionCentralPolicyProvincedata.Id).ToList();

                    foreach (var SubjectCentralPolicyProvinceGroupdata in SubjectCentralPolicyProvinceGroupdatas)
                    {
                        // var users = _context.UserProvinces
                        //.Include(m => m.User)
                        //.Where(m => m.ProvinceId == ProvinceId)
                        //.Where(m => m.User.Role_id == 9)
                        //.ToList();
                        var users = _context.Users
                       .Where(m => m.ProvincialDepartmentId == SubjectCentralPolicyProvinceGroupdata.ProvincialDepartmentId)
                       .Where(m => m.Role_id == 9)
                       .ToList();

                        foreach (var item in users)
                        {
                            var data = new Notification
                            {
                                UserID = item.Id,
                                CentralPolicyId = CentralPolicyId,
                                ProvinceId = ProvinceId,
                                status = Status,
                                noti = 1,
                                CreatedAt = date,
                                xe = xe
                            };
                            _context.Notifications.Add(data);
                            _context.SaveChanges();

                            var data2 = new Notificationcreateby
                            {
                                NotificationId = data.Id,
                                CreateBy = createby
                            };
                            _context.Notificationcreateby.Add(data2);
                            _context.SaveChanges();

                            _externalOrganizationController.SendNotification(item.Id, "ข้อเสนอแนะ" + CentralPolicyData.Title);
                        }
                    }

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


        [Route("api/[controller]/getinspactionsplaneven/{id}")]
        [HttpGet]
        public IActionResult getinspactionsplaneven(long id)
        {
            var data = _context.InspectionPlanEvents.Where(m => m.Id == id).FirstOrDefault();

            return Ok(data);
        }

        [Route("api/[controller]/getElectronicBookUserInvite/{electId}/{userId}")]
        [HttpGet]
        public IActionResult GetElectronicBookUserInvite(long electId, string userId)
        {
            var data = _context.ElectronicBookInvites
                .Where(x => x.ElectronicBookId == electId && x.UserId == userId)
                .Select(x => x.Id)
                .FirstOrDefault();

            return Ok(data);
        }

        [Route("api/[controller]/getElectronicBookProvincialDepartment/{electId}/{provincialId}")]
        [HttpGet]
        public IActionResult GetElectronicBookProvincialDepartment(long electId, long provincialId)
        {
            var data = _context.ElectronicBookProvincialDepartments
                .Where(x => x.ElectronicBookId == electId && x.ProvincialDepartmentId == provincialId)
                .Select(x => x.Id)
                .FirstOrDefault();

            return Ok(data);
        }

        [Route("api/[controller]/getCentralPolicyProvince/{cenId}/{provincialId}")]
        [HttpGet]
        public IActionResult GetCentralPolicyProvince(long cenId, long provincialId)
        {

            var data = _context.CentralPolicyProvinces
               .Where(m => m.CentralPolicyId == cenId && m.ProvinceId == provincialId)
               .Select(m => m.Id)
               .FirstOrDefault();

            return Ok(data);
        }
    }
}
