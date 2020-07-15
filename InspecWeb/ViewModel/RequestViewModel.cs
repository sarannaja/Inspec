using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace InspecWeb.ViewModel
{
    public class RequestViewModel
    {
        public long id { get; set; }
        public string Commanded_by { get; set; }
        public string Subject { get; set; }
        public string Subjectdetail { get; set; }
        public long Draft { get; set; }
        public string Status { get; set; }
        public List<string> Answer_by { get; set; }
        public string Answerdetail { get; set; }
        public string AnswerProblem { get; set; }
        public string AnswerCounsel { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? Commanded_date { get; set; }
        public DateTime? beaware_date { get; set; }
        public long Accept { get; set; }
        public long Cancel { get; set; }
        public string Canceldetail { get; set; }
        public long RequestOrderId { get; set; }
        public long RequestOrderAnswerId { get; set; }
        public DateTime? created_at { get; set; }
        public List<IFormFile> files { get; set; }
    }
}
