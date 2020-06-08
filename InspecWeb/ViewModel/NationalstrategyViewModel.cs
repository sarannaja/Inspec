using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace InspecWeb.ViewModel
{
    public class NationalstrategyViewModel
    {

        public string Title { get; set; }
        public string Link { get; set; }
        public List<IFormFile> files { get; set; }


    }
}
