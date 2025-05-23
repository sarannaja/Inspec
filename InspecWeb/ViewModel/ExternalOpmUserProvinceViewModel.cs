// Generated by https://quicktype.io

namespace InspecWeb.ViewModel
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class OpmUserProvince
    {
        [JsonProperty("sla_level")]
        public long SlaLevel { get; set; }

        [JsonProperty("operating_date")]
        public DateTime OperatingDate { get; set; }

        [JsonProperty("contact_count")]
        public long ContactCount { get; set; }

        [JsonProperty("main_status_text")]
        public string MainStatusText { get; set; }

        [JsonProperty("case_manager_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CaseManagerId { get; set; }

        [JsonProperty("case_manager_text")]
        public string CaseManagerText { get; set; }

        [JsonProperty("secret_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SecretId { get; set; }

        [JsonProperty("secret_text")]
        public string SecretText { get; set; }

        [JsonProperty("summary_story")]
        public string SummaryStory { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("customer_citizen_id")]
        public string CustomerCitizenId { get; set; }

        [JsonProperty("customer_type")]
        public string CustomerType { get; set; }

        [JsonProperty("case_id")]
        public string CaseId { get; set; }

        [JsonProperty("case_code")]
        public string CaseCode { get; set; }

        [JsonProperty("objective_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ObjectiveId { get; set; }

        [JsonProperty("objective_text")]
        public string ObjectiveText { get; set; }

        [JsonProperty("inform_to_id")]
        public object InformToId { get; set; }

        [JsonProperty("inform_to_text")]
        public object InformToText { get; set; }

        [JsonProperty("type_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TypeId { get; set; }

        [JsonProperty("type_text")]
        public string TypeText { get; set; }

        [JsonProperty("status_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long StatusId { get; set; }

        [JsonProperty("status_text")]
        public string StatusText { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("detail")]
        public object Detail { get; set; }

        [JsonProperty("defendant")]
        public string Defendant { get; set; }

        [JsonProperty("date_opened")]
        public DateTime DateOpened { get; set; }

        [JsonProperty("sla")]
        public long Sla { get; set; }

        [JsonProperty("ShowDateTime")]
        public string ShowDateTime { get; set; }

        [JsonProperty("timeline_type")]
        public object TimelineType { get; set; }

        [JsonProperty("summary_result")]
        public object SummaryResult { get; set; }

        [JsonProperty("created_by_text")]
        public string CreatedByText { get; set; }

        [JsonProperty("updated_by_text")]
        public string UpdatedByText { get; set; }

        [JsonProperty("role_option")]
        public object RoleOption { get; set; }

        [JsonProperty("count_operating")]
        public long CountOperating { get; set; }
    }

    public partial class OpmUserProvince
    {
        public static OpmUserProvince FromJson(string json) => JsonConvert.DeserializeObject<OpmUserProvince>(json, OpmUserProvinceConverter.Settings);
    }

    public static class OpmUserProvinceSerialize
    {
        public static string ToJson(this OpmUserProvince self) => JsonConvert.SerializeObject(self, OpmUserProvinceConverter.Settings);
    }

    internal static class OpmUserProvinceConverter
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

    internal class OpmUserProvinceParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
