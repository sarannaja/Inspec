using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspecWeb.Models
{
  

    public class CentralPolicyViewModel
    {
        public class CentralPolicyClass
        {
            //public ReceiveTime[] MonJour { get; set; }
            public string title { get; set; }
            public DateTime start_date { get; set; }
            public DateTime end_date { get; set; }
            public Subject[] subjects { get; set; }
            public List<IFormFile> files { get; set; }
        }

        public class CentralPolicyClass2
        {
            //public ReceiveTime[] MonJour { get; set; }
            public string title { get; set; }
            //public DateTime start_date { get; set; }
            //public DateTime end_date { get; set; }
            //public Subject[] subjects { get; set; }
            //public List<IFormFile> files { get; set; }
        }
    }

    //public class SubjectModel
    //{
    //    public string answer { get; set; }
    //}
}
