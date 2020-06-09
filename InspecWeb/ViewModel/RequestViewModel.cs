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
        public string Name { get; set; }
        public string AnswerDetail { get; set; }
        public string AnswerProblem { get; set; }
        public string AnswerCounsel { get; set; }
        public long CentralpolicyId { get; set; }
        public long ProvinceId { get; set; }
        public string UserId { get; set; }
        public string AnswerUserId { get; set; } 
        public List<IFormFile> files { get; set; }
    }
}
