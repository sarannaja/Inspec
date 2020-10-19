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

namespace InspecWeb.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class UtinityCheckDateController : ControllerBase {

        private readonly ApplicationDbContext _context;
        private readonly IMailService mailService;
        public UtinityCheckDateController(
            IMailService mailService,
            ApplicationDbContext context
        ) {
            this.mailService = mailService;
            this._context = context;
        }

        // POST: ตัวอย่างการส่ง email 
       
        //cronjob เดี๋ยยวค่อยอธิบาย
        public void CheckPeopleQuestionNotificationDate() // คล้าย Status == 5

        {
            Console.WriteLine ("eiei " + DateTime.Now.ToShortDateString());
            var date = DateTime.Now;
            Console.WriteLine("000");
            var subjectgroups = _context.SubjectGroups
                    .Where(m => m.PeopleQuestionNotificationDate.Value.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
            Console.WriteLine("0.5");
            foreach (var subjectgroup in subjectgroups)
            {
                Console.WriteLine("111");
                var SubjectCentralPolicyProvincesdatas = _context.SubjectCentralPolicyProvinces
                .Where(m => m.SubjectGroupId == subjectgroup.Id).ToList();


                foreach (var SubjectCentralPolicyProvincesdata in SubjectCentralPolicyProvincesdatas)
                {
                    Console.WriteLine("222");
                    var SubquestionCentralPolicyProvincedata = _context.SubquestionCentralPolicyProvinces
                        .Where(m => m.SubjectCentralPolicyProvinceId == SubjectCentralPolicyProvincesdata.Id).FirstOrDefault();

                    var SubjectCentralPolicyProvinceGroupdatas = _context.SubjectCentralPolicyProvinceGroups
                        .Where(m => m.SubquestionCentralPolicyProvinceId == SubquestionCentralPolicyProvincedata.Id).ToList();

                    foreach (var SubjectCentralPolicyProvinceGroupdata in SubjectCentralPolicyProvinceGroupdatas)
                    {
                        Console.WriteLine("333");
                        var users = _context.Users
                       .Where(m => m.ProvincialDepartmentId == SubjectCentralPolicyProvinceGroupdata.ProvincialDepartmentId)
                       .Where(m => m.Role_id == 9)
                       .ToList();

                        foreach (var item in users)
                        {
                            Console.WriteLine("444");
                            _context.Notifications.Add(new Notification
                            {
                                UserID = item.Id,
                                CentralPolicyId = subjectgroup.CentralPolicyId,
                                ProvinceId = subjectgroup.ProvinceId,
                                status = 21,
                                noti = 1,
                                CreatedAt = date,
                                xe = subjectgroup.Id
                            });
                            _context.SaveChanges();
                        }
                    }

                }
            }

            Console.WriteLine("555");
        }

        public void CheckPeopleQuestionDeadlineDate() // คล้าย Status == 5
        {
            Console.WriteLine("eiei " + DateTime.Now.ToShortDateString());
            var date = DateTime.Now;

            var subjectgroups = _context.SubjectGroups
                    .Where(m => m.PeopleQuestionDeadlineDate.Value.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();

            foreach (var subjectgroup in subjectgroups)
            {
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

                        var users = _context.Users
                       .Where(m => m.ProvincialDepartmentId == SubjectCentralPolicyProvinceGroupdata.ProvincialDepartmentId)
                       .Where(m => m.Role_id == 9)
                       .ToList();

                        foreach (var item in users)
                        {
                            _context.Notifications.Add(new Notification
                            {
                                UserID = item.Id,
                                CentralPolicyId = subjectgroup.CentralPolicyId,
                                ProvinceId = subjectgroup.ProvinceId,
                                status = 22,
                                noti = 1,
                                CreatedAt = date,
                                xe = subjectgroup.Id
                            });
                            _context.SaveChanges();
                        }
                    }

                }
            }
        }

        public void CheckSubjectNotificationDate() // คล้าย Status == 4
        {
            Console.WriteLine("eiei " + DateTime.Now.ToShortDateString());
            var date = DateTime.Now;

            var subjectgroups = _context.SubjectGroups
                   .Where(m => m.SubjectNotificationDate.Value.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();

            foreach (var subjectgroup in subjectgroups)
            {
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
                        var users = _context.Users
                       .Where(m => m.ProvincialDepartmentId == SubjectCentralPolicyProvinceGroupdata.ProvincialDepartmentId)
                       .Where(m => m.Role_id == 9)
                       .ToList();

                        foreach (var item in users)
                        {
                            _context.Notifications.Add(new Notification
                            {
                                UserID = item.Id,
                                CentralPolicyId = subjectgroup.CentralPolicyId,
                                ProvinceId = subjectgroup.ProvinceId,
                                status = 23,
                                noti = 1,
                                CreatedAt = date,
                                xe = subjectgroup.Id
                            });
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }

        public void CheckSubjectDeadlineDate() // คล้าย Status == 4
        {
            Console.WriteLine("eiei " + DateTime.Now.ToShortDateString());
            var date = DateTime.Now;

            var subjectgroups = _context.SubjectGroups
                   .Where(m => m.SubjectDeadlineDate.Value.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();

            foreach (var subjectgroup in subjectgroups)
            {
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
                        var users = _context.Users
                       .Where(m => m.ProvincialDepartmentId == SubjectCentralPolicyProvinceGroupdata.ProvincialDepartmentId)
                       .Where(m => m.Role_id == 9)
                       .ToList();

                        foreach (var item in users)
                        {
                            _context.Notifications.Add(new Notification
                            {
                                UserID = item.Id,
                                CentralPolicyId = subjectgroup.CentralPolicyId,
                                ProvinceId = subjectgroup.ProvinceId,
                                status = 24,
                                noti = 1,
                                CreatedAt = date,
                                xe = subjectgroup.Id
                            });
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
    }

}