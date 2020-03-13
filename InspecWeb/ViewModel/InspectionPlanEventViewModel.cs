using System;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class InspectionPlanEventViewModel
    {
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        public Input[] input { get; set;}
    }

    public class Input
    {
        public long[] ProvinceId { get; set; }

        public DateTime PlanDate { get; set; }
    }
}
