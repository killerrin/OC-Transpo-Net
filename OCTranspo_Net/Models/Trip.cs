﻿using Newtonsoft.Json;
using OCTranspo_Net.Converters;
using OCTranspo_Net.Models.States;
using System;
using System.Globalization;
using System.Text;

namespace OCTranspo_Net.Models
{
    public class Trip
    {
        /// <summary>
        /// Final stop on the trip
        /// </summary>
        [JsonProperty("TripDestination")]
        public string TripDestination { get; set; }

        /// <summary>
        /// start time for the trip. Format HH:MI, where HH = 24 hour format
        /// </summary>
        /// <remarks>
        /// TripStartTime is the scheduled time the run was supposed to leave the first stop.
        /// It's only really useful for determining which bus corresponds to which run in a full timetable.
        /// @https://www.reddit.com/r/ottawa/comments/epms7d/question_for_programmers_using_the_octranspo_api/femw9sr/
        /// </remarks>
        [JsonProperty("TripStartTime")]
        public string TripStartTime { get; set; }

        /// <summary>
        /// adjusted scheduled time in minutes
        /// </summary>
        /// <remarks>
        /// AdjustedScheduleTime is the number of minutes after the time of the request (which is not included in the response)
        /// that the bus is presumed to arrive, assuming it does not gain or lose time from where it is until it arrives at the stop.
        /// For instance, if the scheduled time from Stop A to Stop B is exactly 15 minutes, then any bus located at
        /// Stop A (no matter how early or late he was in arriving at Stop A, or how slow or fast the drive to Stop B will be in reality)
        /// will be AdjustedScheduleTime "15" in a data request for Stop B.
        /// @https://www.reddit.com/r/ottawa/comments/epms7d/question_for_programmers_using_the_octranspo_api/femw9sr/
        /// </remarks>
        [JsonProperty("AdjustedScheduleTime")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AdjustedScheduleTime { get; set; }

        /// <summary>
        /// The time since the scheduled was adjusted in whole and fractional minutes.
        /// </summary>
        /// <remarks>
        /// If the Adjustment Age is a Negative Value, then it is using the Scheduled Data
        /// Otherwise if the result is a Positive Value, then it is based off of GPS
        /// </remarks>
        [JsonProperty("AdjustmentAge")]
        public double AdjustmentAge { get; set; } = double.MinValue;

        /// <summary>
        /// Last trip to pass the stop for the route and direction
        /// </summary>
        [JsonProperty("LastTripOfSchedule")]
        public bool LastTripOfSchedule { get; set; }

        /// <summary>
        /// type of bus : low floor, bike rack etc.
        /// </summary>
        [JsonProperty("BusType")]
        public string BusType { get; set; }

        /// <summary>
        /// Latitude of the last gps reading for the bus
        /// </summary>
        [JsonProperty("Latitude")]
        public string Latitude { get; set; }

        /// <summary>
        /// Longitude of the last gps reading for the bus
        /// </summary>
        [JsonProperty("Longitude")]
        public string Longitude { get; set; }

        /// <summary>
        /// speed of the bus in km/hr
        /// </summary>
        [JsonProperty("GPSSpeed")]
        public string GpsSpeed { get; set; }

        /// <summary>
        /// Checks if this Trip is using the GPS or Schedule
        /// </summary>
        [JsonIgnore]
        public bool IsGpsData { get { return AdjustmentAge >= 0; } }

        /// <summary>
        /// Where this Trip was Sourced From
        /// </summary>
        [JsonIgnore]
        public TripDataSource TripSource
        {
            get
            {
                if (AdjustmentAge == double.MinValue) return TripDataSource.None;
                return IsGpsData ? TripDataSource.GPS : TripDataSource.Schedule;
            }
        }

        /// <summary>
        /// Gets the TripStartTime converted to a TimeSpan
        /// </summary>
        /// <returns>The TripStartTime</returns>
        public TimeSpan GetTripStartTimeTimespan()
        {
            var parts = TripStartTime.Split(':');
            var hours = int.Parse(parts[0]);
            var minutes = int.Parse(parts[1]);
            var startTime = new TimeSpan(hours, minutes, 0);
            return startTime;
        }

        /// <summary>
        /// Gets when the GPS was last updated (AdjustmentAge), represented as a DateTime from when the Request was made
        /// </summary>
        /// <param name="timeOfRequest">When the Request was made</param>
        /// <returns>DateTime represening when the GPS was last Updated; or Null if not a GPS time</returns>
        public DateTime? GetGPSLastUpdatedTime(DateTime timeOfRequest)
        {
            if (TripSource == TripDataSource.GPS) { return timeOfRequest.AddMinutes(AdjustmentAge); }
            return null;
        }
    }
}
