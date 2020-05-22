using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class SubjectFileViewModel
    {

        public long Id { get; set; }


        public long[] SubjectCentralPolicyProvinceId { get; set; }


        public List<IFormFile> files { get; set; }

    }
}
