using Newtonsoft.Json;
using OCTranspo_Net.Converters;
using System;

namespace OCTranspo_Net.Models.GTFS
{
    public class AgencyGtfs : GtfsBase
    {
        public const string TableName = "agency";

        [JsonProperty("agency_name")]
        public string agency_name { get; set; }

        [JsonProperty("agency_url")]
        public Uri agency_url { get; set; }

        [JsonProperty("agency_timezone")]
        public string agency_timezone { get; set; }

        [JsonProperty("agency_lang")]
        public string agency_lang { get; set; }
    }
}
