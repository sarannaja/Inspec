﻿using System;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class CentralPolicyEventViewModel
    {
        [JsonProperty(PropertyName = "CentralPolicyId")]
        public long[] CentralPolicyId { get; set; }

        [JsonProperty(PropertyName = "InspectionPlanEventId")]
        public long InspectionPlanEventId { get; set; }
    }
}