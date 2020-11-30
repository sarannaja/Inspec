using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.Services;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

using System.Net.Http;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtinityCheckDateController : ControllerBase
    {
        private readonly IMailService mailService;
        private readonly IServiceScopeFactory scopeFactory;
        public UtinityCheckDateController(
            IMailService mailService,
            IServiceScopeFactory scopeFactory
        )
        {
            this.mailService = mailService;
            this.scopeFactory = scopeFactory;
        }

        // POST: ตัวอย่างการส่ง email 

        //cronjob เดี๋ยยวค่อยอธิบาย
        public void CheckPeopleQuestionNotificationDate() // คล้าย Status == 5

        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                Console.WriteLine(DateTime.Now.ToShortDateString());
                var date = DateTime.Now;
                Console.WriteLine("000");



                var subjectgroups = dbContext.SubjectGroups
                        // .Where(m => Convert.ToDateTime(m.PeopleQuestionNotificationDate).ToShortDateString() == DateTime.Now.ToShortDateString())
                        .ToList();



                Console.WriteLine("0.5");
                foreach (var subjectgroup in subjectgroups)
                {
                    var datenow = Convert.ToDateTime(subjectgroup.PeopleQuestionNotificationDate).ToShortDateString();
                    if (datenow == DateTime.Now.ToShortDateString())
                    {

                        var data = dbContext.SubjectGroupPeopleQuestions
                                  .Where(m => m.SubjectGroupId == subjectgroup.Id)
                                  .FirstOrDefault();


                        var data2 = dbContext.CentralPolicyEvents
                            .Where(m => m.Id == data.CentralPolicyEventId)
                            .FirstOrDefault();

                        var data3 = dbContext.CentralPolicyUsers
                            //.Include(m => m.User)
                            .Where(m => m.CentralPolicyId == data2.CentralPolicyId && m.InspectionPlanEventId == data2.InspectionPlanEventId)
                            .Where(m => m.User.Role_id == 7)
                            .ToList();


                        var CentralPolicyData = dbContext.CentralPolicies.Where(m => m.Id == data2.CentralPolicyId).FirstOrDefault();

                        foreach (var item in data3)
                        {

                            System.Console.WriteLine("UserId : " + item.UserId);

                            // var data3 = dbContext.Notifications.Add(new Notification
                            // {
                            //     UserID = item.UserId,
                            //     CentralPolicyId = subjectgroup.CentralPolicyId,
                            //     ProvinceId = subjectgroup.ProvinceId,
                            //     status = 21,
                            //     noti = 1,
                            //     CreatedAt = date,
                            //     xe = subjectgroup.Id,
                            // });
                            // dbContext.SaveChanges();

                            var data5 = new Notification
                            {
                                UserID = item.UserId,
                                CentralPolicyId = subjectgroup.CentralPolicyId,
                                ProvinceId = subjectgroup.ProvinceId,
                                status = 21,
                                noti = 1,
                                CreatedAt = date,
                                xe = subjectgroup.Id,
                            };

                            dbContext.Notifications.Add(data5);
                            dbContext.SaveChanges();


                            var data6 = new Notificationcreateby
                            {
                                NotificationId = data5.Id,
                                CreateBy = subjectgroup.CreatedBy
                            };
                            dbContext.Notificationcreateby.Add(data6);
                            dbContext.SaveChanges();

                            SendNotification(item.UserId, "มีคำเชิญ" + CentralPolicyData.Title);
                        }

                    }
                }

                Console.WriteLine("555");
            }
        }

        public void CheckPeopleQuestionDeadlineDate() // คล้าย Status == 5
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                Console.WriteLine("eiei " + DateTime.Now.ToShortDateString());
                var date = DateTime.Now;

                var subjectgroups = dbContext.SubjectGroups
                        // .Where(m => m.PeopleQuestionDeadlineDate.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
                        .ToList();

                foreach (var subjectgroup in subjectgroups)
                {
                    var datenow = Convert.ToDateTime(subjectgroup.PeopleQuestionDeadlineDate).ToShortDateString();
                    if (datenow == DateTime.Now.ToShortDateString())
                    {

                        var data = dbContext.SubjectGroupPeopleQuestions
                                  .Where(m => m.SubjectGroupId == subjectgroup.Id)
                                  .FirstOrDefault();


                        var data2 = dbContext.CentralPolicyEvents
                            .Where(m => m.Id == data.CentralPolicyEventId)
                            .FirstOrDefault();

                        var data3 = dbContext.CentralPolicyUsers
                            //.Include(m => m.User)
                            .Where(m => m.CentralPolicyId == data2.CentralPolicyId && m.InspectionPlanEventId == data2.InspectionPlanEventId)
                            .Where(m => m.User.Role_id == 7)
                            .ToList();

                        var CentralPolicyData = dbContext.CentralPolicies.Where(m => m.Id == data2.CentralPolicyId).FirstOrDefault();
                        foreach (var item in data3)
                        {

                            System.Console.WriteLine("UserId : " + item.UserId);

                            // dbContext.Notifications.Add(new Notification
                            // {
                            //     UserID = item.UserId,
                            //     CentralPolicyId = subjectgroup.CentralPolicyId,
                            //     ProvinceId = subjectgroup.ProvinceId,
                            //     status = 22,
                            //     noti = 1,
                            //     CreatedAt = date,
                            //     xe = subjectgroup.Id,
                            // });
                            // dbContext.SaveChanges();
                            var data5 = new Notification
                            {
                                UserID = item.UserId,
                                CentralPolicyId = subjectgroup.CentralPolicyId,
                                ProvinceId = subjectgroup.ProvinceId,
                                status = 22,
                                noti = 1,
                                CreatedAt = date,
                                xe = subjectgroup.Id,
                            };

                            dbContext.Notifications.Add(data5);
                            dbContext.SaveChanges();


                            var data6 = new Notificationcreateby
                            {
                                NotificationId = data5.Id,
                                CreateBy = subjectgroup.CreatedBy
                            };
                            dbContext.Notificationcreateby.Add(data6);
                            dbContext.SaveChanges();

                            SendNotification(item.UserId, "กำหนดส่งคำถามภาคประชาชน" + CentralPolicyData.Title);
                        }

                    }
                }
            }
        }

        public void CheckSubjectNotificationDate() // คล้าย Status == 4
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                Console.WriteLine("eiei " + DateTime.Now.ToShortDateString());
                var date = DateTime.Now;

                var subjectgroups = dbContext.SubjectGroups
                       //.Where(m => m.SubjectNotificationDate.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
                       .ToList();

                foreach (var subjectgroup in subjectgroups)
                {

                    var datenow = Convert.ToDateTime(subjectgroup.SubjectNotificationDate).ToShortDateString();
                    if (datenow == DateTime.Now.ToShortDateString())
                    {
                        var SubjectCentralPolicyProvincesdatas = dbContext.SubjectCentralPolicyProvinces
                        .Where(m => m.SubjectGroupId == subjectgroup.Id).ToList();

                        foreach (var SubjectCentralPolicyProvincesdata in SubjectCentralPolicyProvincesdatas)
                        {
                            var SubquestionCentralPolicyProvincedata = dbContext.SubquestionCentralPolicyProvinces
                                .Where(m => m.SubjectCentralPolicyProvinceId == SubjectCentralPolicyProvincesdata.Id).FirstOrDefault();

                            var SubjectCentralPolicyProvinceGroupdatas = dbContext.SubjectCentralPolicyProvinceGroups
                                .Where(m => m.SubquestionCentralPolicyProvinceId == SubquestionCentralPolicyProvincedata.Id).ToList();

                            foreach (var SubjectCentralPolicyProvinceGroupdata in SubjectCentralPolicyProvinceGroupdatas)
                            {
                                var users = dbContext.Users
                               .Where(m => m.ProvincialDepartmentId == SubjectCentralPolicyProvinceGroupdata.ProvincialDepartmentId)
                               .Where(m => m.Role_id == 9)
                               .ToList();

                                var CentralPolicyData = dbContext.CentralPolicies.Where(m => m.Id == subjectgroup.CentralPolicyId).FirstOrDefault();
                                foreach (var item in users)
                                {
                                    // dbContext.Notifications.Add(new Notification
                                    // {
                                    //     UserID = item.Id,
                                    //     CentralPolicyId = subjectgroup.CentralPolicyId,
                                    //     ProvinceId = subjectgroup.ProvinceId,
                                    //     status = 23,
                                    //     noti = 1,
                                    //     CreatedAt = date,
                                    //     xe = subjectgroup.Id
                                    // });
                                    // dbContext.SaveChanges();

                                    var data5 = new Notification
                                    {
                                        UserID = item.Id,
                                        CentralPolicyId = subjectgroup.CentralPolicyId,
                                        ProvinceId = subjectgroup.ProvinceId,
                                        status = 23,
                                        noti = 1,
                                        CreatedAt = date,
                                        xe = subjectgroup.Id,
                                    };

                                    dbContext.Notifications.Add(data5);
                                    dbContext.SaveChanges();


                                    var data6 = new Notificationcreateby
                                    {
                                        NotificationId = data5.Id,
                                        CreateBy = subjectgroup.CreatedBy
                                    };
                                    dbContext.Notificationcreateby.Add(data6);
                                    dbContext.SaveChanges();

                                    SendNotification(item.Id, "แจ้งเตือนประเด็นตรวจติดตาม" + CentralPolicyData.Title);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void CheckSubjectDeadlineDate() // คล้าย Status == 4
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                Console.WriteLine("eiei " + DateTime.Now.ToShortDateString());
                var date = DateTime.Now;

                var subjectgroups = dbContext.SubjectGroups
                       //.Where(m => m.SubjectDeadlineDate.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
                       .ToList();

                foreach (var subjectgroup in subjectgroups)
                {
                    var datenow = Convert.ToDateTime(subjectgroup.SubjectDeadlineDate).ToShortDateString();
                    if (datenow == DateTime.Now.ToShortDateString())
                    {
                        var SubjectCentralPolicyProvincesdatas = dbContext.SubjectCentralPolicyProvinces
                        .Where(m => m.SubjectGroupId == subjectgroup.Id).ToList();

                        foreach (var SubjectCentralPolicyProvincesdata in SubjectCentralPolicyProvincesdatas)
                        {
                            var SubquestionCentralPolicyProvincedata = dbContext.SubquestionCentralPolicyProvinces
                                .Where(m => m.SubjectCentralPolicyProvinceId == SubjectCentralPolicyProvincesdata.Id).FirstOrDefault();

                            var SubjectCentralPolicyProvinceGroupdatas = dbContext.SubjectCentralPolicyProvinceGroups
                                .Where(m => m.SubquestionCentralPolicyProvinceId == SubquestionCentralPolicyProvincedata.Id).ToList();

                            foreach (var SubjectCentralPolicyProvinceGroupdata in SubjectCentralPolicyProvinceGroupdatas)
                            {
                                var users = dbContext.Users
                               .Where(m => m.ProvincialDepartmentId == SubjectCentralPolicyProvinceGroupdata.ProvincialDepartmentId)
                               .Where(m => m.Role_id == 9)
                               .ToList();
                                var CentralPolicyData = dbContext.CentralPolicies.Where(m => m.Id == subjectgroup.CentralPolicyId).FirstOrDefault();
                                foreach (var item in users)
                                {
                                    // dbContext.Notifications.Add(new Notification
                                    // {
                                    //     UserID = item.Id,
                                    //     CentralPolicyId = subjectgroup.CentralPolicyId,
                                    //     ProvinceId = subjectgroup.ProvinceId,
                                    //     status = 24,
                                    //     noti = 1,
                                    //     CreatedAt = date,
                                    //     xe = subjectgroup.Id
                                    // });
                                    // dbContext.SaveChanges();

                                    var data5 = new Notification
                                    {
                                        UserID = item.Id,
                                        CentralPolicyId = subjectgroup.CentralPolicyId,
                                        ProvinceId = subjectgroup.ProvinceId,
                                        status = 24,
                                        noti = 1,
                                        CreatedAt = date,
                                        xe = subjectgroup.Id,
                                    };

                                    dbContext.Notifications.Add(data5);
                                    dbContext.SaveChanges();


                                    var data6 = new Notificationcreateby
                                    {
                                        NotificationId = data5.Id,
                                        CreateBy = subjectgroup.CreatedBy
                                    };
                                    dbContext.Notificationcreateby.Add(data6);
                                    dbContext.SaveChanges();

                                    SendNotification(item.Id, "กำหนดส่งประเด็นตรวจติดตาม" + CentralPolicyData.Title);
                                }
                            }
                        }
                    }
                }
            }
        }


        public IActionResult SendNotification(string userId, string message)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                // IActionResult OpmUserProvince = null;
                var mobileObj = dbContext.UserTokenMobiles
                    .Where(w => w.UserID == userId)
                    .ToArray();
                if (mobileObj.Length != 0)
                {
                    foreach (var item in mobileObj)
                    {
                        var client = new HttpClient();
                        var task = client.GetAsync("http://203.113.14.20:3000/inspecnotification/" + item.DeviceType + "/" + item.Token + "/" + message)
                            .ContinueWith((taskwithresponse) =>
                            {
                                var response = taskwithresponse.Result;

                                var jsonString = response.Content.ReadAsStringAsync();
                                jsonString.Wait();
                                // OpmUserProvince = JsonConvert.DeserializeObject<OpmCase>(jsonString.Result);
                            });
                        task.Wait();
                    }
                }
                return Ok(mobileObj);
            }
        }
    }

}