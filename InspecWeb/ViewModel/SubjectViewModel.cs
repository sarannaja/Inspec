using System;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class SubjectViewModel
    {
       
        public long Id { get; set; }

       
        public long CentralPolicyId { get; set; }

        
        public string Name { get; set; }

        
        public string Answer { get; set; }

        
        public long[] CentralPolicyDateId { get; set; }
    }
}
