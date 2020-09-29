using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace InspecWeb.ViewModel
{
    public class CentralPolicyProvinceViewModel
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

        [JsonProperty(PropertyName = "FiscalYearNewId")]
        public long FiscalYearNewId { get; set; }

        [JsonProperty(PropertyName = "TypeexaminationplanId")]
        public long TypeexaminationplanId { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "ProvinceId")]
        public long[] ProvinceId { get; set; }

        [JsonProperty(PropertyName = "Class")]
        public string Class { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "fileType")]
        public string fileType { get; set; }


        [JsonProperty(PropertyName = "InspectionPlanEventId")]
        public long InspectionPlanEventId { get; set; }

        //[JsonProperty(PropertyName = "CentralPolicyId")]
        //public long CentralPolicyId { get; set; }
        public DateTime[] StartDate2 { get; set; }
        public DateTime[] EndDate2 { get; set; }
        public List<IFormFile> files { get; set; }

        public long[] SubjectId { get; set; }

        [JsonProperty(PropertyName = "RemoveProvince")]
        public long[] RemoveProvince { get; set; }

        [JsonProperty(PropertyName = "AddProvince")]
        public long[] AddProvince { get; set; }

    }

    //public class inputdate
    //{
    //    public DateTime StartDate { get; set; }
    //    public DateTime EndDate { get; set; }
    //}
}
