using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace InspecWeb.ViewModel
{
    public class PremierorderViewModel
    {

        public string Year { get; set; }
        public string Title { get; set; }
        public List<IFormFile> files { get; set; }


    }
}
