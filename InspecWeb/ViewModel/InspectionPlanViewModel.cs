using Newtonsoft.Json;
using System;

namespace InspecWeb.ViewModel
{
    public class InspectionPlanViewModel
    {
        public string UserID { get; set; }

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

        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "ProvinceId")]
        public long ProvinceId { get; set; }
        //public List<IFormFile> files { get; set; }
        public long InspectionPlanEventId { get; set; }

    }

}
