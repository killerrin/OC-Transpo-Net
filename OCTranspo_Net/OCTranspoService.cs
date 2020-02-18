using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OCTranspo_Net.Models;
using OCTranspo_Net.Models.GTFS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OCTranspo_Net
{
    public class OCTranspoService
    {
        public string AppID { get; protected set; }
        public string APIKey { get; protected set; }

        private readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        protected HttpClient Client { get; private set; } = new HttpClient();

        public OCTranspoService(string appID, string aPIKey)
        {
            AppID = appID;
            APIKey = aPIKey;
        }

        /// <summary>
        /// Retrieves the routes for a given stop number
        /// </summary>
        /// <param name="stopNo">4-digit stop number found on bus stops. A full list of stops can be downloaded here: http://data.ottawa.ca/dataset/oc-transpo-schedules</param>
        /// <param name="routeNo">Bus route number.</param>
        /// <returns></returns>
        public async Task<GetRouteSummaryForStopResultRoot> GetRouteSummaryForStop(string stopNo, string routeNo)
        {
            Uri url = new Uri("https://api.octranspo1.com/v1.3/GetRouteSummaryForStop", UriKind.Absolute);

            var formContent = new List<KeyValuePair<string, string>>();
            formContent.Add(new KeyValuePair<string, string>("appID", AppID));
            formContent.Add(new KeyValuePair<string, string>("apiKey", APIKey));
            formContent.Add(new KeyValuePair<string, string>("stopNo", stopNo));
            formContent.Add(new KeyValuePair<string, string>("routeNo", routeNo));
            formContent.Add(new KeyValuePair<string, string>("format", OCTranspoDataFormat.JSON.ToString()));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(formContent)
            };

            var response = await Client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetRouteSummaryForStopResultRoot>(responseString, SerializerSettings);
                return result;
            }

            return null;
        }

        /// <summary>
        /// Retrieves next three trips on the route for a given stop number
        /// </summary>
        /// <param name="stopNo">4-digit stop number found on bus stops. A full list of stops can be downloaded here: http://data.ottawa.ca/dataset/oc-transpo-schedules</param>
        /// <param name="routeNo">Bus route number.</param>
        /// <returns></returns>
        public async Task<GetNextTripsForStopResultRoot> GetNextTripsForStop(string stopNo, string routeNo)
        {
            Uri url = new Uri("https://api.octranspo1.com/v1.3/GetNextTripsForStop", UriKind.Absolute);

            var formContent = new List<KeyValuePair<string, string>>();
            formContent.Add(new KeyValuePair<string, string>("appID", AppID));
            formContent.Add(new KeyValuePair<string, string>("apiKey", APIKey));
            formContent.Add(new KeyValuePair<string, string>("stopNo", stopNo));
            formContent.Add(new KeyValuePair<string, string>("routeNo", routeNo));
            formContent.Add(new KeyValuePair<string, string>("format", OCTranspoDataFormat.JSON.ToString()));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(formContent)
            };

            var response = await Client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetNextTripsForStopResultRoot>(responseString, SerializerSettings);
                return result;
            }
            return null;
        }

        /// <summary>
        /// Retrieves next three trips for all routes for a given stop number.
        /// </summary>
        /// <param name="stopNo">4-digit stop number found on bus stops. A full list of stops can be downloaded here: http://data.ottawa.ca/dataset/oc-transpo-schedules</param>
        /// <returns></returns>
        public async Task<GetRouteSummaryForStopResultRoot> GetNextTripsForStopAllRoutes(string stopNo)
        {
            Uri url = new Uri("https://api.octranspo1.com/v1.3/GetNextTripsForStopAllRoutes", UriKind.Absolute);

            var formContent = new List<KeyValuePair<string, string>>();
            formContent.Add(new KeyValuePair<string, string>("appID", AppID));
            formContent.Add(new KeyValuePair<string, string>("apiKey", APIKey));
            formContent.Add(new KeyValuePair<string, string>("stopNo", stopNo));
            formContent.Add(new KeyValuePair<string, string>("format", OCTranspoDataFormat.JSON.ToString()));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(formContent)
            };

            var response = await Client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetRouteSummaryForStopResultRoot>(responseString, SerializerSettings);
                return result;
            }
            return null;
        }

        /// <summary>
        /// Retrieves specific records from all sections of the GTFS file.
        /// </summary>
        /// <param name="table">The table to query.</param>
        /// <param name="id">Optional. A specific row in a table by the id value.</param>
        /// <param name="column">Optional. A specific column in a table. The use of column requires the use of the value parameter.</param>
        /// <param name="value">Optional*. A specific value in a column. *Required if column is specified.</param>
        /// <param name="orderBy">Specify a column to sort by.</param>
        /// <param name="direction">Specify the direction of sorted records. asc or desc. Default asc.</param>
        /// <param name="limit">Specify a maximum limit of returned records.</param>
        /// <returns></returns>
        public async Task<GTFSQueryRoot<T>> GTFS<T>(string table, string id = null, string column = null, string value = null, string orderBy = null, OCTranspoDataSortOrder direction = OCTranspoDataSortOrder.asc, int? limit = null)
            where T: GtfsBase
        {
            Uri url = new Uri("https://api.octranspo1.com/v1.3/GetNextTripsForStopAllRoutes", UriKind.Absolute);

            var formContent = new List<KeyValuePair<string, string>>();
            formContent.Add(new KeyValuePair<string, string>("appID", AppID));
            formContent.Add(new KeyValuePair<string, string>("apiKey", APIKey));
            formContent.Add(new KeyValuePair<string, string>("table", APIKey));
            formContent.Add(new KeyValuePair<string, string>("direction", APIKey));

            if (!string.IsNullOrWhiteSpace(id)) { formContent.Add(new KeyValuePair<string, string>("id", id)); }
            if (!string.IsNullOrWhiteSpace(column)) { formContent.Add(new KeyValuePair<string, string>("column", column)); }
            if (!string.IsNullOrWhiteSpace(value)) { formContent.Add(new KeyValuePair<string, string>("value", value)); }
            if (!string.IsNullOrWhiteSpace(orderBy)) { formContent.Add(new KeyValuePair<string, string>("orderBy", orderBy)); }
            if (limit != null) { formContent.Add(new KeyValuePair<string, string>("limit", limit.ToString())); }

            formContent.Add(new KeyValuePair<string, string>("format", OCTranspoDataFormat.JSON.ToString()));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(formContent)
            };

            var response = await Client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GTFSQueryRoot<T>>(responseString, SerializerSettings);
                return result;
            }
            return null;
        }

    }
}
