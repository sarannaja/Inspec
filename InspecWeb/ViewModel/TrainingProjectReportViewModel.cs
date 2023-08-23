using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace InspecWeb.ViewModel
{
    public class TrainingProjectReportViewModel
    {
        [JsonProperty(PropertyName = "Year")]
        public string Year { get; set; }

        [JsonProperty(PropertyName = "CourseType")]
        public string CourseType { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string userId { get; set; }

        public List<IFormFile> reportFiles { get; set; }
        public List<IFormFile> modelDirectoryFiles { get; set; }
        public List<IFormFile> practiceGuideFiles { get; set; }
        public List<IFormFile> projectDocumentFiles { get; set; }
        public List<IFormFile> trainingDetailFiles { get; set; }
        
    }
}
