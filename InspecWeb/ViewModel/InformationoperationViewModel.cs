using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace InspecWeb.ViewModel
{
    public class InformationoperationViewModel
    {

        public string Location { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Tel { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public List<IFormFile> files { get; set; }


    }
}
