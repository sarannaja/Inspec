using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class AnswerSubquestionOutsiderViewModel
    {
        public inputansweroutsider[] inputansweroutsider { get; set; }

        public inputanswer[] inputanswer { get; set; }

        //public inputanswerfile[] inputanswerfile { get; set; }

        public long SubjectCentralPolicyProvinceId { get; set; }
        public string Type { get; set; }
        public List<IFormFile> files { get; set; }


    }
    public class inputansweroutsider
    {
        public long Id { get; set; }

        public long SubquestionCentralPolicyProvinceId { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Phonenumber { get; set; }

        public string Answer { get; set; }
    }
    public class inputanswer
    {
        public long Id { get; set; }

        public long SubquestionCentralPolicyProvinceId { get; set; }

        public string UserId { get; set; }

        public string Answer { get; set; }
    }
    //public class inputanswerfile
    //{
    //    public long SubjectCentralPolicyProvinceId { get; set; }
    //    public List<IFormFile> files { get; set; }
    //}
}
