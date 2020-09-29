using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace InspecWeb.ViewModel
{
    public class NationalstrategyViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }

        public string namefile { get; set; }
        public List<IFormFile> files { get; set; }


    }
}
