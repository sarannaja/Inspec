using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace InspecWeb.ViewModel
{
    public class CalendarFileViewModel
    {
        public long ElectronicBookId { get; set; }
        public long CentralPolicyProvinceId { get; set; }
        public string Step { get; set; }
        public string Status { get; set; }
        public string QuestionPeople { get; set; }
        public List<IFormFile> files { get; set; }
        public List<IFormFile> signatureFiles { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Suggestion { get; set; }
        public DateTime NotificationSubjectDate { get; set; }
        public DateTime DeadlineSubjectDate { get; set; }
        public DateTime NotificationPeopleQuestiontDate { get; set; }
        public DateTime DeadlinePeopleQuestiontDate { get; set; }
    }
}
