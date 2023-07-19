using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace InspecWeb.ViewModel
{
    public class OldReportViewModel
    {
        [JsonProperty(PropertyName = "Year")]
        public string Year { get; set; }

        [JsonProperty(PropertyName = "CentralPolicyType")]
        public string CentralPolicyType { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "ReportType")]
        public string ReportType { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string userId { get; set; }

        public List<IFormFile> files { get; set; }
        
    }
}
