using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class FiscalYearRelationViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "FiscalYearId")]
        public long FiscalYearId { get; set; }

        [JsonProperty(PropertyName = "RegionId")]
        public long RegionId { get; set; }

        [JsonProperty(PropertyName = "ProvinceId")]
        public long[] ProvinceId { get; set; }

    }
}
