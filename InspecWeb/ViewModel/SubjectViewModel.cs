using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class SubjectViewModel
    {
       
        public long Id { get; set; }

       
        public long CentralPolicyId { get; set; }

        
        public string Name { get; set; }

        
        public string Answer { get; set; }

        public string Status { get; set; }

        public List<IFormFile> files { get; set; }

        public long[] CentralPolicyDateId { get; set; }

        public inputsubjectdepartment[] inputsubjectdepartment { get; set; }

       
    }

    public class inputsubjectdepartment
    {
        public long subjectid { get; set; }
        public long box { get; set; }
        public long departmentId { get; set; }
        public inputquestionopen[] inputquestionopen { get; set; }
        public inputquestionclose[] inputquestionclose { get; set; }
    }

    public class inputquestionopen
    {
        public string questionopen { get; set; }
    }

    public class inputquestionclose
    {
        public string questionclose { get; set; }
        public inputanswerclose[] inputanswerclose { get; set; }
    }

    public class inputanswerclose
    {
        public string answerclose { get; set; }
    }
}
