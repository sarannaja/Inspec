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

        [JsonProperty(PropertyName = "Report")]
        public string Report { get; set; }

        [JsonProperty(PropertyName = "DraftStatus")]
        public string DraftStatus { get; set; }
    }
}
