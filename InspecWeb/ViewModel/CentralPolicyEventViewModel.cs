using System;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class CentralPolicyEventViewModel
    {
        [JsonProperty(PropertyName = "CentralPolicyId")]
        public long[] CentralPolicyId { get; set; }

        [JsonProperty(PropertyName = "InspectionPlanEventId")]
        public long InspectionPlanEventId { get; set; }

        public string CreatedBy { get; set; }

        public long ProvinceId { get; set; }

        public DateTime NotificationDate { get; set; }
        public DateTime DeadlineDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
