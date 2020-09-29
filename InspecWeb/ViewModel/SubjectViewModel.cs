using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace InspecWeb.ViewModel
{
    public class SubjectViewModel
    {

        public long Id { get; set; }

        public long CentralPolicyId { get; set; }

        public string Name { get; set; }

        public string Answer { get; set; }

        public string Status { get; set; }

        public string Explanation { get; set; }

        public string UserID { get; set; }

        public List<IFormFile> files { get; set; }

        public long[] CentralPolicyDateId { get; set; }

        public inputsubjectdepartment[] inputsubjectdepartment { get; set; }

        public subjectevent[] subjectevent { get; set; }
    }

    public class inputsubjectdepartment
    {
        public long subjectid { get; set; }
        public long box { get; set; }
        public long departmentId { get; set; }
        public string explanation { get; set; }
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

    public class subjectevent
    {

        public long[] CentralpolicyId { get; set; }
        public CentralpolicyModel[] CentralpolicySelect { get; set; }
        //public CentralPolicyeventId[] CentralPolicyeventId { get; set; }
        public long ProvinceId { get; set; }

        public DateTime startdate { get; set; }

        public DateTime enddate { get; set; }

        public string CreatedBy { get; set; }

        public string Land { get; set; }

        public string Title { get; set; }
    }

    public class CentralpolicyModel
    {
        public long centralpolicyId { get; set; }
        public long centralPolicyeventId { get; set; }

    }
}
