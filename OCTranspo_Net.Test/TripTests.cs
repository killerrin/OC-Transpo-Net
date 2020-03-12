using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCTranspo_Net.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OCTranspo_Net.Test
{
    [TestClass]
    public class TripTests
    {
        [TestMethod]
        public void GetScheduledArrivalTime_Test()
        {
            DateTime today = DateTime.Today;
            Trip trip = new Trip
            {
                AdjustmentAge = -1,
                TripStartTime = "26:30",
                AdjustedScheduleTime = 10,
            };

            DateTime expected = today.AddHours(26).AddMinutes(30).AddMinutes(10);
            DateTime arrivalTime = trip.GetScheduledArrivalTime(today);
            Debug.WriteLine($"expected={expected} | arrivalTime={arrivalTime}");

            Assert.AreEqual(expected, arrivalTime);
        }

        [TestMethod]
        public void GetScheduledArrivalTimeMinutes_Test()
        {
            DateTime today = DateTime.Today;
            DateTime now = today.AddHours(23);
            Trip trip = new Trip
            {
                AdjustmentAge = -1,
                TripStartTime = "26:30",
                AdjustedScheduleTime = 10,
            };

            int expected = (60 * 3) + 30 + 10;
            int arrivalTime = trip.GetScheduledArrivalTimeMinutes(today, now);
            Debug.WriteLine($"expected={expected} | arrivalTime={arrivalTime}");

            Assert.AreEqual(expected, arrivalTime);
        }

        [TestMethod]
        public void GetAdjustedArrivalTime_Test()
        {
            DateTime timeOfRequest = new DateTime(2020, 1, 1, 0, 0, 0);
            Trip trip = new Trip
            {
                AdjustmentAge = 1,
                AdjustedScheduleTime = 10,
            };

            DateTime expected = timeOfRequest.AddMinutes(trip.AdjustedScheduleTime);
            DateTime arrivalTime = trip.GetAdjustedArrivalTime(timeOfRequest);
            Debug.WriteLine($"expected={expected} | arrivalTime ={arrivalTime}");

            Assert.AreEqual(expected, arrivalTime);
        }

        [TestMethod]
        public void GetAdjustedArrivalTimeMinutes_Test()
        {
            DateTime timeOfRequest = new DateTime(2020, 1, 1, 0, 0, 0);
            Trip trip = new Trip
            {
                AdjustmentAge = 1,
                AdjustedScheduleTime = 10,
            };

            int expected = 10;
            int arrivalTime = trip.GetAdjustedArrivalTimeMinutes(timeOfRequest);
            Debug.WriteLine($"expected={expected} | arrivalTime={arrivalTime}");

            Assert.AreEqual(expected, arrivalTime);
        }
    }
}
