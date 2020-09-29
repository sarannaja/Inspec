using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

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


    }
}
