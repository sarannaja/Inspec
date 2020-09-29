using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace InspecWeb.ViewModel
{
    public class GovernmentinspectionplanViewModel
    {

        public string Year { get; set; }
        public string Title { get; set; }
        public string filesname { get; set; }
        public List<IFormFile> files { get; set; }


    }
}
