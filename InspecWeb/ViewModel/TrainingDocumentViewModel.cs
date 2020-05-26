using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace InspecWeb.ViewModel
{
    public class TrainingDocumentViewModel
    {
        public long TrainingId { get; set; }

        public string Detail { get; set; }

        public List<IFormFile> files { get; set; }


    }

}
