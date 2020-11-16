using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace InspecWeb.ViewModel
{
    public class ImportReportViewModel
    {
        [JsonProperty(PropertyName = "centralPolicyEventId")]
        public long[] centralPolicyEventId { get; set; }

        // [JsonProperty(PropertyName = "centralPolicyType")]
        // public string centralPolicyType { get; set; }

        [JsonProperty(PropertyName = "reportType")]
        public string reportType { get; set; }

        [JsonProperty(PropertyName = "centralPolicyTypeId")]
        public long centralPolicyTypeId { get; set; }

        [JsonProperty(PropertyName = "inspectionRound")]
        public string inspectionRound { get; set; }

        [JsonProperty(PropertyName = "fiscalYearId")]
        public long fiscalYearId { get; set; }

        [JsonProperty(PropertyName = "regionId")]
        public long regionId { get; set; }

        [JsonProperty(PropertyName = "provinceId")]
        public long provinceId { get; set; }

        [JsonProperty(PropertyName = "monitoringTopics")]
        public string monitoringTopics { get; set; }

        [JsonProperty(PropertyName = "detailReport")]
        public string detailReport { get; set; }

        [JsonProperty(PropertyName = "suggestion")]
        public string suggestion { get; set; }

        [JsonProperty(PropertyName = "command")]
        public string command { get; set; }

        [JsonProperty(PropertyName = "UserId")]
        public string UserId { get; set; }

        public long reportId { get; set; }

        public List<IFormFile> files { get; set; }

        public headData[] headData { get; set; }

        [JsonProperty(PropertyName = "Commander")]
        public string Commander { get; set; }

        [JsonProperty(PropertyName = "DepartmentId")]
        public long DepartmentId { get; set; }

        [JsonProperty(PropertyName = "ZoneId")]
        public long ZoneId { get; set; }

        [JsonProperty(PropertyName = "CommanderAr")]
        public string[] CommanderAr { get; set; }
    }

    public class headData
    {
        [JsonProperty(PropertyName = "centralPolicy")]
        public string centralPolicy { get; set; }

        // [JsonProperty(PropertyName = "centralPolicyType")]
        // public string centralPolicyType { get; set; }

        // [JsonProperty(PropertyName = "reportType")]
        // public string reportType { get; set; }

        // [JsonProperty(PropertyName = "inspectionRound")]
        // public string inspectionRound { get; set; }

        // [JsonProperty(PropertyName = "fiscalYear")]
        // public string fiscalYear { get; set; }

        // [JsonProperty(PropertyName = "region")]
        // public string region { get; set; }

        // [JsonProperty(PropertyName = "province")]
        // public string province { get; set; }

        // [JsonProperty(PropertyName = "monitoringTopics")]
        // public string monitoringTopics { get; set; }

        // [JsonProperty(PropertyName = "detailReport")]
        // public string detailReport { get; set; }

        // [JsonProperty(PropertyName = "suggestion")]
        // public string suggestion { get; set; }

        // [JsonProperty(PropertyName = "command")]
        // public string command { get; set; }

        // public bodyData[] bodyData { get; set; }
    }

    public class bodyData
    {
        [JsonProperty(PropertyName = "subject")]
        public string subject { get; set; }

        [JsonProperty(PropertyName = "detail")]
        public string detail { get; set; }

        [JsonProperty(PropertyName = "suggestion")]
        public string suggestion { get; set; }

        [JsonProperty(PropertyName = "problem")]
        public string problem { get; set; }

        [JsonProperty(PropertyName = "department")]
        public string department { get; set; }

        [JsonProperty(PropertyName = "opinionPeople")]
        public string opinionPeople { get; set; }
    }
}
