using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class ExportReportViewModel
    {
        [JsonProperty(PropertyName = "typeId")]
        public string typeId { get; set; }
        [JsonProperty(PropertyName = "centralPolicyId")]
        public string centralPolicyId { get; set; }
        [JsonProperty(PropertyName = "userId")]
        public string userId { get; set; }
        [JsonProperty(PropertyName = "centralPolicyProvinceId")]
        public long centralPolicyProvinceId { get; set; }
        [JsonProperty(PropertyName = "reportId")]
        public long reportId { get; set; }
        [JsonProperty(PropertyName = "electronicBookId")]
        public long electronicBookId { get; set; }

        [JsonProperty(PropertyName = "subjectData")]
        public string[] subjectData { get; set; }

        public reportData[] reportData { get; set; }

        public reportData2[] reportData2 { get; set; }

        public printReport[] printReport { get; set; }

        public printReport2[] printReport2 { get; set; }

    }

    public class reportData
    {
        //public string subject { get; set; }
        //public string department { get; set; }
        //public string detail { get; set; }
        //public string suggestion { get; set; }
        //public string problem { get; set; }
        //public string opinionPeople { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string subject { get; set; }

        [JsonProperty(PropertyName = "department")]
        public string department { get; set; }

        [JsonProperty(PropertyName = "detail")]
        public string detail { get; set; }

        [JsonProperty(PropertyName = "suggestion")]
        public string suggestion { get; set; }

        [JsonProperty(PropertyName = "problem")]
        public string problem { get; set; }

        [JsonProperty(PropertyName = "opinionPeople")]
        public string opinionPeople { get; set; }

        [JsonProperty(PropertyName = "reportId")]
        public long reportId { get; set; }
    }

    public class reportData2
    {
        [JsonProperty(PropertyName = "centralPolicy")]
        public string centralPolicy { get; set; }

        [JsonProperty(PropertyName = "department")]
        public string department { get; set; }

        [JsonProperty(PropertyName = "fiscalYear")]
        public string fiscalYear { get; set; }

        [JsonProperty(PropertyName = "region")]
        public string region { get; set; }

        [JsonProperty(PropertyName = "province")]
        public string province { get; set; }

        public tableData[] tableData { get; set; }
    }

    public class tableData
    {
        [JsonProperty(PropertyName = "subject")]
        public string subject { get; set; }
    }

    public class printReport
    {
        [JsonProperty(PropertyName = "inspectorName")]
        public string inspectorName { get; set; }
        [JsonProperty(PropertyName = "inspectorDescription")]
        public string inspectorDescription { get; set; }
        [JsonProperty(PropertyName = "inspectorSign")]
        public string inspectorSign { get; set; }
        [JsonProperty(PropertyName = "approve")]
        public string approve { get; set; }

    }

    public class printReport2
    {
        [JsonProperty(PropertyName = "departmentName")]
        public string departmentName { get; set; }
        [JsonProperty(PropertyName = "department")]
        public string department { get; set; }
        [JsonProperty(PropertyName = "departmentDescription")]
        public string departmentDescription { get; set; }
        [JsonProperty(PropertyName = "departmentSign")]
        public string departmentSign { get; set; }
        [JsonProperty(PropertyName = "departmentDate")]
        public DateTime departmentDate { get; set; }
    }
}
