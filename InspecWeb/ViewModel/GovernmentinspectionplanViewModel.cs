using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace InspecWeb.ViewModel
{
    public class GovernmentinspectionplanViewModel
    {

        public string Year { get; set; }
        public string Title { get; set; }
        public List<IFormFile> files { get; set; }


    }
}
