using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class ElectronicBookViewModel
    {
        [JsonProperty(PropertyName = "Detail")]
        public string Detail { get; set; }
        [JsonProperty(PropertyName = "Problem")]
        public string Problem { get; set; }
        [JsonProperty(PropertyName = "Suggestion")]
        public string Suggestion { get; set; }
        [JsonProperty(PropertyName = "PolicyIssue")]
        public long PolicyIssue { get; set; }
        [JsonProperty(PropertyName = "ElectID")]
        public long ElectID { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "SubjectCentralPolicyProvinceId")]
        public long SubjectCentralPolicyProvinceId { get; set; }

        //[JsonProperty(PropertyName = "CentralPolicyID")]
        //public string CentralPolicyID { get; set; }

        //public Inputelectronicbook[] Inputelectronicbook { get; set;}
        public long CentralPolicyId { get; set; }
        public long ProvinceId { get; set; }
        //public UserPeopleId[] UserPeopleId { get; set; }
        //public UserMinistryId[] UserMinistryId { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }
        public List<IFormFile> files { get; set; }

        public string[] UserPeopleId { get; set; }
        public string[] UserMinistryId { get; set; }
        public long[] ProvincialDepartmentId { get; set; }

        public long ReportProvinceId { get; set; }
        public string ReportTitle { get; set; }
        public string ReportYear { get; set; }
        public string ReportMinistry { get; set; }
        public string ReportStatus { get; set; }
        public string ReportUserId { get; set; }

        public string ReportBodySubject { get; set; }
        public string ReportBodyProblem { get; set; }
        public string ReportBodyDepartment { get; set; }
        public string ReportBodySuggestion { get; set; }
        public string ReportBodyReport { get; set; }
        public string ReportBodyFile { get; set; }
        public string ReportBodyComment { get; set; }
        public long ReportCentralPolicyId { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public long[] CentralPolicyEventId { get; set; }
        public long CentralEventId { get; set; }

        public long[] inspectionPlanEventId { get; set; }
        public string[] userId { get; set; }
        public long inviteId { get; set; }
        public string approve { get; set; }
        public string userInvite { get; set; }
    }

    //public class Inputelectronicbook
    //{
    //    public long ProvinceId { get; set; }
    //    public long CentralPolicyId { get; set; }
    //}

    //public class UserPeopleId
    //{
    //    public string Id { get; set; }
    //}
    //public class UserMinistryId
    //{
    //    public string Id { get; set; }
    //}
}
