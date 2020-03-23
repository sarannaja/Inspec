using System;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class CentralPolicyUserModel
    {
        [JsonProperty(PropertyName = "CentralPolicyId")]
        public long CentralPolicyId { get; set; }

        [JsonProperty(PropertyName = "UserId")]
        public string[] UserId { get; set; }
    }
}
