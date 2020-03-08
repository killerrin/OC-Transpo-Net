using Newtonsoft.Json;
using System;

namespace OCTranspo_Net.Models
{
    public class GetRouteSummaryForStopResultRoot
    {
        [JsonProperty("GetRouteSummaryForStopResult")]
        public GetRouteSummaryForStopResult GetRouteSummaryForStopResult { get; set; }

        public DateTime TimeOfRequest { get; internal set; } = DateTime.Now;
        public DateTime TimeOfResponse { get; internal set; } = DateTime.Now;
    }
}
