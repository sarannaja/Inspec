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

        public reportData[] reportData { get; set; }
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
    }
}
