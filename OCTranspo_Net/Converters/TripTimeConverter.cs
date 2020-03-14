using OCTranspo_Net.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCTranspo_Net.Converters
{
    public class TripTimeConverter
    {
        public Trip SourceTrip { get; set; }
        public DateTime Now { get; set; }
        public DateTime Today { get; set; }
        public DateTime Yesterday { get; set; }

        /// <summary>
        /// Used in GetTripStartTime as a threshold between when the Daytime and Midnight 
        /// </summary>
        public int MidnightHourThreshold { get; set; } = 4;
        public bool IsMidnightThresholdActive { get { return Now.Hour < MidnightHourThreshold; } }

        /// <summary>
        /// Used in GetScheduledArrivalTimeMinutes as a threshold of minutes that is checked against
        /// before the calculation is attempted again with the Midnight Flag flipped
        /// </summary>
        public int MidnightScheduledMinutesArrivalCorrectionThreshold { get; set; } = 600;

        public TripTimeConverter(Trip trip) : this(trip, DateTime.Today, DateTime.Now) { }
        public TripTimeConverter(Trip trip, DateTime? today, DateTime? now)
        {
            if (!today.HasValue) today = DateTime.Today;
            if (!now.HasValue) now = DateTime.Now;

            SourceTrip = trip;
            Now = now.Value;
            Today = today.Value;
            Yesterday = Today.AddDays(-1);
        }

        /// <summary>
        /// Gets the TripStartTime converted to a DateTime
        /// </summary>
        /// <param name="midnightRolloverMode">Whether the function should operate in Midnight Rollover Mode</param>
        /// <returns>The TripStartTime</returns>
        public virtual DateTime GetTripStartTime(bool midnightRolloverMode)
        {
            var startTimeSpan = SourceTrip.GetTripStartTimeTimespan();
            if (startTimeSpan.Days <= 0)
            {
                return Today.AddMinutes(startTimeSpan.TotalMinutes);
            }

            int year = Today.Year;
            int month = Today.Month;
            DateTime startTime = new DateTime(year, month, 1, startTimeSpan.Hours, startTimeSpan.Minutes, 0);

            // Because we start at the first day of the month, we have to subtract the first day
            int day = Today.Day - 1;

            // To handle the fact that Midnight times still operate on the 24 hour clock of the previous day (24:00->28:00)
            // Then we'll handle the times differently depending on what hour of the day it is
            if (midnightRolloverMode) { day += (startTimeSpan.Days - 1); }
            else { day += startTimeSpan.Days; }

            // Return the Start Time with the days added back in
            return startTime.AddDays(day);
        }

        /// <summary>
        /// Gets the TripStartTime converted to a DateTime. 
        /// Uses the MidnightHourThreshold to determine whether to operate in Midnight Rollover Mode or DayTime Mode
        /// </summary>
        /// <returns>The TripStartTime</returns>
        public virtual DateTime GetTripStartTime()
        {
            if (IsMidnightThresholdActive) { return GetTripStartTime(true); }
            return GetTripStartTime(false);
        }

        #region Scheduled Arrival Time
        /// <summary>
        /// Gets the Arrival Time as set on the Schedule
        /// Uses the MidnightHourThreshold to determine whether to operate in Midnight Rollover Mode or DayTime Mode
        /// </summary>
        /// <returns>The Scheduled Arrival Time</returns>
        public virtual DateTime GetScheduledArrivalTime()
        {
            DateTime startTime;
            if (IsMidnightThresholdActive) { startTime = GetTripStartTime(false); }
            else { startTime = GetTripStartTime(false); }

            return startTime.AddMinutes(SourceTrip.AdjustedScheduleTime);
        }

        /// <summary>
        /// Gets the Arrival Time as set on the Schedule
        /// </summary>
        /// <param name="midnightRolloverMode">Whether the function should operate in Midnight Rollover Mode</param>
        /// <returns>The Scheduled Arrival Time</returns>
        public virtual DateTime GetScheduledArrivalTime(bool midnightRolloverMode)
        {
            var startTime = GetTripStartTime(midnightRolloverMode);
            return startTime.AddMinutes(SourceTrip.AdjustedScheduleTime);
        }

        /// <summary>
        /// Gets the Scheduled Arrival Time in minutes
        /// Uses the MidnightHourThreshold to determine whether to operate in Midnight Rollover Mode or DayTime Mode
        /// </summary>
        /// <returns>The Scheduled Arrival Time in minutes</returns>
        public virtual int GetScheduledArrivalTimeMinutes()
        {
            DateTime arrivalTime = GetScheduledArrivalTime();
            int arrivalMinutes = Convert.ToInt32((arrivalTime - Now).TotalMinutes);

            // If the result is wildly inacurate, then try it again with the Midnight Mode Toggled On
            if (arrivalMinutes > MidnightScheduledMinutesArrivalCorrectionThreshold || arrivalMinutes < -MidnightScheduledMinutesArrivalCorrectionThreshold)
            {
                if (IsMidnightThresholdActive) { return GetScheduledArrivalTimeMinutes(true); }
            }

            return arrivalMinutes;
        }

        /// <summary>
        /// Gets the Scheduled Arrival Time in minutes
        /// </summary>
        /// <param name="midnightRolloverMode">Whether the function should operate in Midnight Rollover Mode</param>
        /// <returns>The Scheduled Arrival Time in minutes</returns>
        public virtual int GetScheduledArrivalTimeMinutes(bool midnightRolloverMode)
        {
            DateTime arrivalTime = GetScheduledArrivalTime(midnightRolloverMode);
            int arrivalMinutes = Convert.ToInt32((arrivalTime - Now).TotalMinutes);
            return arrivalMinutes;
        }
        #endregion

        /// <summary>
        /// Gets the Arrival Time as based upon the API Spec: 
        /// - The TripStartTime if AdjustmentAge is less than 0
        /// - The Adjusted Scheduled Time if AdjustmentAge is less than 0
        /// Uses the MidnightHourThreshold to determine whether to operate in Midnight Rollover Mode or DayTime Mode
        /// </summary>
        /// <param name="timeOfRequest">The Time that the API request was made</param>
        /// <returns>The Time that the trip should arrive</returns>
        public virtual DateTime GetArrivalTime(DateTime timeOfRequest)
        {
            if (SourceTrip.AdjustmentAge < 0) { return GetScheduledArrivalTime(); }
            return GetAdjustedArrivalTime(timeOfRequest);
        }

        /// <summary>
        /// Gets the Arrival Time as based upon the API Spec: 
        /// - The TripStartTime if AdjustmentAge is less than 0
        /// - The Adjusted Scheduled Time if AdjustmentAge is less than 0
        /// </summary>
        /// <param name="timeOfRequest">The Time that the API request was made</param>
        /// <param name="midnightRolloverMode">Whether the function should operate in Midnight Rollover Mode</param>
        /// <returns>The Time that the trip should arrive</returns>
        public virtual DateTime GetArrivalTime(DateTime timeOfRequest, bool midnightRolloverMode)
        {
            if (SourceTrip.AdjustmentAge < 0) { return GetScheduledArrivalTime(midnightRolloverMode); }
            return GetAdjustedArrivalTime(timeOfRequest);
        }

        /// <summary>
        /// Gets the Arrival Time in Minutes as based upon the API Spec: 
        /// - The TripStartTime if AdjustmentAge is less than 0
        /// - The Adjusted Scheduled Time if AdjustmentAge is less than 0
        /// Uses the MidnightHourThreshold to determine whether to operate in Midnight Rollover Mode or DayTime Mode
        /// </summary>
        /// <param name="timeOfRequest">The Time that the API request was made</param>
        /// <returns>The Time that the trip should arrive, in minutes</returns>
        public virtual int GetArrivalTimeMinutes(DateTime timeOfRequest)
        {
            if (SourceTrip.AdjustmentAge < 0) { return GetScheduledArrivalTimeMinutes(); }
            return GetAdjustedArrivalTimeMinutes(timeOfRequest);
        }

        /// <summary>
        /// Gets the Arrival Time in Minutes as based upon the API Spec: 
        /// - The TripStartTime if AdjustmentAge is less than 0
        /// - The Adjusted Scheduled Time if AdjustmentAge is less than 0
        /// </summary>
        /// <param name="timeOfRequest">The Time that the API request was made</param>
        /// <param name="midnightRolloverMode">Whether the function should operate in Midnight Rollover Mode</param>
        /// <returns>The Time that the trip should arrive, in minutes</returns>
        public virtual int GetArrivalTimeMinutes(DateTime timeOfRequest, bool midnightRolloverMode)
        {
            if (SourceTrip.AdjustmentAge < 0) { return GetScheduledArrivalTimeMinutes(midnightRolloverMode); }
            return GetAdjustedArrivalTimeMinutes(timeOfRequest);
        }

        #region Adjusted Arrival Time
        /// <summary>
        /// Gets the Adjusted Arrival Times
        /// </summary>
        /// <param name="timeOfRequest">The Time that the API request was made</param>
        /// <returns>The time that the trip should arrive</returns>
        public virtual DateTime GetAdjustedArrivalTime(DateTime timeOfRequest)
        {
            return timeOfRequest.AddMinutes(SourceTrip.AdjustedScheduleTime);
        }

        /// <summary>
        /// Gets the Adjusted Arrival Time in minutes
        /// </summary>
        /// <param name="timeOfRequest">The Time that the API request was made</param>
        /// <returns>When the trip should arrive, in minutes</returns>
        public virtual int GetAdjustedArrivalTimeMinutes(DateTime timeOfRequest)
        {
            DateTime arrivalTime = GetAdjustedArrivalTime(timeOfRequest);
            int arrivalMinutes = Convert.ToInt32((arrivalTime - timeOfRequest).TotalMinutes);
            return arrivalMinutes;
        }
        #endregion
    }
}
