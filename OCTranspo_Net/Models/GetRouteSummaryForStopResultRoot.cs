using Newtonsoft.Json;

namespace OCTranspo_Net.Models
{
    public class GetRouteSummaryForStopResultRoot
    {
        [JsonProperty("GetRouteSummaryForStopResult")]
        public GetRouteSummaryForStopResult GetRouteSummaryForStopResult { get; set; }
    }
}
