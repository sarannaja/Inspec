
using System.Collections.Generic;
using System.Globalization;
using System;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

    namespace InspecWeb.ViewModel
    {
      
        public partial class NewRegion
        {
            [JsonProperty("Id")]
            public long Id { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("FiscalYears")]
            public NewRegionFiscalYear[] FiscalYears { get; set; }
        }

        public partial class NewRegionFiscalYear
    {
            [JsonProperty("Year")]
            public long Year { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("Projects")]
            public NewRegionProjects Projects { get; set; }

            [JsonProperty("Provinces")]
            public Province[] Provinces { get; set; }
        }

        public partial class NewRegionProjects
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

        public partial class Region
        {
            public static Region FromJson(string json) => JsonConvert.DeserializeObject<Region>(json, Converter.Settings);
        }

        public static class Serialize
        {
            public static string ToJson(this Region self) => JsonConvert.SerializeObject(self,Converter.Settings);
        }

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters = {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
           };
        }
        
    }

