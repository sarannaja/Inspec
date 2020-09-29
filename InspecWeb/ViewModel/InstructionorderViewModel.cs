using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace InspecWeb.ViewModel
{
    public class InstructionorderViewModel
    {

        public string Year { get; set; }
        public string Order { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string CreateBy { get; set; }
        public List<IFormFile> files { get; set; }

    }
}
