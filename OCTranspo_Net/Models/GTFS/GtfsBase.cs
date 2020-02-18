using Newtonsoft.Json;
using OCTranspo_Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCTranspo_Net.Models.GTFS
{
    public abstract class GtfsBase
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long id { get; set; }
    }
}
