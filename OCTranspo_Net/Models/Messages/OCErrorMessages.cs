using System;
using System.Collections.Generic;
using System.Text;

namespace OCTranspo_Net.Models.Messages
{
    public static class OCErrorMessages
    {
        public const string GetNextTripsForStop_Error_2 = "Unable to query data source";
        public const string GetRouteSummaryForStop_Error_1 = "Invalid API key";
        public const string GetRouteSummaryForStop_Error_2 = "Unable to query data source";
        public const string GetRouteSummaryForStop_Error_10 = "Invalid stop number";
        public const string GetRouteSummaryForStop_Error_11 = "Invalid route number";
        public const string GetRouteSummaryForStop_Error_12 = "Stop does not service route";
    }
}
