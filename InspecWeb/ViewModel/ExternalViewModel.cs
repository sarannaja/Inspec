using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{

    public class ExternalOtpsViewModel
    {
        public partial class RegionOtps
        {
            [JsonProperty("Id")]
            public long Id { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("FiscalYears")]
            public FiscalYear[] FiscalYears { get; set; }
        }

        public partial class FiscalYear
        {
            [JsonProperty("Year")]
            public long Year { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("Projects")]
            public Projects Projects { get; set; }

            [JsonProperty("Provinces")]
            public Province[] Provinces { get; set; }
        }

        public partial class Projects
        {
            [JsonProperty("Count")]
            public long Count { get; set; }

            [JsonProperty("Completed")]
            public long Completed { get; set; }

            [JsonProperty("TotalBudget")]
            public double TotalBudget { get; set; }

            [JsonProperty("TotalAmount")]
            public double TotalAmount { get; set; }

            [JsonProperty("TotalSpent")]
            public double TotalSpent { get; set; }

            [JsonProperty("TotalPercent")]
            public double TotalPercent { get; set; }
        }

        public partial class Province
        {
            [JsonProperty("Id")]
            public long Id { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }
        }
    }

}