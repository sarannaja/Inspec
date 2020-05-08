using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace InspecWeb.ViewModel
{
    public class AddExecutiveOrderViewModel
    {
        public string UserID { get; set; }

        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        public List<IFormFile> files { get; set; }

    }

    //public class inputdate
    //{
    //    public DateTime StartDate { get; set; }
    //    public DateTime EndDate { get; set; }
    //}
}
