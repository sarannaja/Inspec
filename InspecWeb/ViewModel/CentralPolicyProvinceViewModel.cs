using System;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class CentralPolicyProvinceViewModel
    {
        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "StartDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "EndDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "FiscalYearId")]
        public long FiscalYearId { get; set; }

        [JsonProperty(PropertyName = "ProvinceId")]
        public long[] ProvinceId { get; set; }

        [JsonProperty(PropertyName = "InspectionPlanEventId")]
        public long InspectionPlanEventId { get; set; }
        //[JsonProperty(PropertyName = "CentralPolicyId")]
        //public long CentralPolicyId { get; set; }
    }
}
