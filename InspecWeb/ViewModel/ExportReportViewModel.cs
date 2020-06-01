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
    }
}
