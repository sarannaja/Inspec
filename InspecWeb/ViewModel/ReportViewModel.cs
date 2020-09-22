using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspecWeb.ViewModel
{
    public class ReportViewModel
    {

    }

    public class reportsubject
    {
        public string type { get; set; }

        public string fiscalyear { get; set; }

        public string title { get; set; }

        public Subject[] Subject { get; set; }
    }

    public class Subject
    {
        public string name { get; set; }

        public string explanation { get; set; }

        public string[] provincialDepartment { get; set; }

        public string[] namesubquestion { get; set; }

    }

    //public class subquestion1
    //{
    //    public string namesubquestion { get; set; }

    //}

    public class reportperformance
    {
        public long provinceid { get; set; }

        public long centralpolicyid { get; set; }

        public long subjectid { get; set; }

        public string reporttype { get; set; }

        public long provincialDepartmentId { get; set; }

        public long CentralPolicyProvinceId { get; set; }

        public long SubjectGroupId { get; set; }
    }

    public class reportsuggestions
    {
        public long provinceid { get; set; }

        public long centralpolicyid { get; set; }

        public long subjectid { get; set; }

        public string reporttype { get; set; }

        public long subjectgroupsid { get; set; }
    }

    public class reportquestionnaire
    {
        public long planid { get; set; }

    }

    public class reportcomment
    {
        public string userid { get; set; }

        public string reporttype { get; set; }

        public long provinceid { get; set; }

        public long CentralPolicyProvinceId { get; set; }

    }

    public class reportsuggestionsresult
    {
        public long SubjectGroupId { get; set; }

        public string reporttype { get; set; }

        public long provincialDepartmentId { get; set; }

        public long provinceId { get; set; }
    }

    // public class reportquestionnaire
    // {
    //     public long planid { get; set; }

    // }

    // public class reportcomment
    // {
    //     public string userid { get; set; }

    //     public string reporttype { get; set; }

    //     public long provinceid { get; set; }

    // }
}
