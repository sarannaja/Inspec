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
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("api/[controller]/[action]/{id}")]
        public IActionResult getnotifications(string id)
        {
            var Notifications = _context.Notifications
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
        public Notification Post(long CentralPolicyId, long ProvinceId, string UserId, long Status, long xe)
        {
            System.Console.WriteLine("Status : " + Status);

            var date = DateTime.Now;
            var notificationdata = new Notification(); //save ลง base แจ้งเตือน
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

                    _context.Notifications.Add(new Notification
                    {
                        UserID = item.UserId,
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
            if (Status == 2 || Status == 6)
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
                            _context.Notifications.Add(new Notification
                            {
                                UserID = item.Id,
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

            // แจ้งเตือนผู้ตรวจเขต เมื่อผู้ว่าราชการจังหวัดหรือ หัวหน้าส่วนจังหวัด รับทราบรายการสมุดตรวจ
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
            //ส่งเชิญในกำหนดการ
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
            // รอแก้ถ้าเป็นสมุดตรวจสร้างเองให้เอา electronicBook.centralPolicy มาใช้แทน xe
            if (Status == 17)
            {
                System.Console.WriteLine("in 17" + ProvinceId);
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
                    System.Console.WriteLine("Success Noti 18");
                }
            }

            //มอบหมาย
            if (Status == 19)
            {
                var users = _context.Users
                     .Where(m => m.Id == UserId).First();


                System.Console.WriteLine("USERID : " + users.Id);
                _context.Notifications.Add(new Notification
                {
                    UserID = users.Id,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe,
                });

                _context.SaveChanges();
            }

            if (Status == 20)
            {
                _context.Notifications.Add(new Notification
                {
                    UserID = UserId,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe,
                });

                _context.SaveChanges();

            }

            if (Status == 9)
            {
                var userData = _context.ImportReports
                    .Where(x => x.Id == xe)
                    .FirstOrDefault();


                System.Console.WriteLine("USERID : " + userData.CreatedBy);
                _context.Notifications.Add(new Notification
                {
                    UserID = userData.CreatedBy,
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    status = Status,
                    noti = 1,
                    CreatedAt = date,
                    xe = xe,
                });

                _context.SaveChanges();
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
                            _context.Notifications.Add(new Notification
                            {
                                UserID = item.Id,
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
