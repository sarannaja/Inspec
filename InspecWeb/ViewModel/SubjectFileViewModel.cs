using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace InspecWeb.ViewModel
{
    public class SubjectFileViewModel
    {

        public long Id { get; set; }


        public long[] SubjectCentralPolicyProvinceId { get; set; }


        public List<IFormFile> files { get; set; }

    }
}
