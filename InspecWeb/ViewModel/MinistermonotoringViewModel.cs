using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class MinistermonotoringViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Position")]
        public string Position { get; set; }

        [JsonProperty(PropertyName = "Image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "RegionId")]
        public long[] RegionId { get; set; }

    }
}
