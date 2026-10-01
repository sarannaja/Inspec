using System;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class CentralPolicySummaryViewModel
    {

        public long userId { get; set; }


        public long CentralPolicyId { get; set; }


        public string Detail { get; set; }

         public string CreatedBy { get; set; }

    }
}
