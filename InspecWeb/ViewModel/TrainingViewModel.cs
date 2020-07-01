using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace InspecWeb.ViewModel
{
    public class TrainingViewModel
    {
       
        public string Name { get; set; }

        public string Detail { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime RegisStartDate { get; set; }

        public DateTime RegisEndDate { get; set; }

        public List<IFormFile> files { get; set; }

    }

}
