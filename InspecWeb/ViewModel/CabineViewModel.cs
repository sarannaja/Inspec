using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace InspecWeb.ViewModel
{
    public class CabineViewModel
    {

        public string Name { get; set; }
        public string Position { get; set; }
        public string Prefix { get; set; }
        public string Type { get; set; }
        public string Detail { get; set; }
        public List<IFormFile> files { get; set; }
        public string tel { get; set; }    
        public string Commandnumber { get; set; }
        public string cabinet { get; set; }
        public long MinistryId { get; set; }
        public string Filename { get; set; }

    }
}
