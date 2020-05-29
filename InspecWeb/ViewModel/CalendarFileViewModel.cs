using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class CalendarFileViewModel
    {
        public long ElectronicBookId { get; set; }
        public long CentralPolicyProvinceId { get; set; }
        public string Step { get; set; }
        public string Status { get; set; }
        public List<IFormFile> files { get; set; }
    }
}
