using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace InspecWeb.ViewModel
{
    public class ImportReportViewModel
    {
        [JsonProperty(PropertyName = "SubjectId")]
        public long SubjectId { get; set; }

        [JsonProperty(PropertyName = "TypeReport")]
        public string TypeReport { get; set; }

        [JsonProperty(PropertyName = "TypeExport")]
        public string TypeExport { get; set; }

        [JsonProperty(PropertyName = "CreateBy")]
        public string CreateBy { get; set; }

        [JsonProperty(PropertyName = "Command")]
        public string Command { get; set; }

        [JsonProperty(PropertyName = "Commander")]
        public string Commander { get; set; }

        [JsonProperty(PropertyName = "ReportId")]
        public long ReportId { get; set; }

        public List<IFormFile> fileWord { get; set; }
        public List<IFormFile> fileExcel { get; set; }
    }
}
