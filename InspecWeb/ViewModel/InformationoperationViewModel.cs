using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace InspecWeb.ViewModel
{
    public class InformationoperationViewModel
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Tel { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public List<IFormFile> files { get; set; }


    }
}
