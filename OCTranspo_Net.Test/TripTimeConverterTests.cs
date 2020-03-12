using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCTranspo_Net.Converters;
using OCTranspo_Net.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OCTranspo_Net.Test
{
    [TestClass]
    public class TripTimeConverterTests
    {
        [TestMethod]
        public void GetScheduledArrivalTime_Daytime_Test()
        {
            DateTime today = DateTime.Today;
            DateTime now = today.AddHours(23);
            Trip trip = new Trip
            {
                AdjustmentAge = -1,
                TripStartTime = "26:30",
                AdjustedScheduleTime = 10,
            };

            TripTimeConverter converter = new TripTimeConverter(trip, today, now);
            DateTime arrivalTime = converter.GetScheduledArrivalTime();

            DateTime expected = converter.Today.AddHours(26).AddMinutes(30).AddMinutes(10);
            Debug.WriteLine($"expected={expected} | arrivalTime={arrivalTime}");
            Assert.AreEqual(expected, arrivalTime);
        }

        [TestMethod]
        public void GetScheduledArrivalTime_Midday_Test()
        {
            DateTime today = DateTime.Today;
            DateTime now = today.AddHours(10);
            Trip trip = new Trip
            {
                AdjustmentAge = -1,
                TripStartTime = "10:30",
                AdjustedScheduleTime = 10,
            };

            TripTimeConverter converter = new TripTimeConverter(trip, today, now);
            DateTime arrivalTime = converter.GetScheduledArrivalTime();

            DateTime expected = converter.Today.AddHours(10).AddMinutes(30).AddMinutes(10);
            Debug.WriteLine($"expected={expected} | arrivalTime={arrivalTime}");
            Assert.AreEqual(expected, arrivalTime);
        }

        [TestMethod]
        public void GetScheduledArrivalTime_Midnight_Test()
        {
            DateTime today = DateTime.Today;
            DateTime now = today.AddHours(1);
            Trip trip = new Trip
            {
                AdjustmentAge = -1,
                TripStartTime = "26:30",
                AdjustedScheduleTime = 10,
            };

            TripTimeConverter converter = new TripTimeConverter(trip, today, now);
            DateTime arrivalTime = converter.GetScheduledArrivalTime();

            DateTime expected = converter.Today.AddHours(26).AddMinutes(30).AddMinutes(10);
            Debug.WriteLine($"expected={expected} | arrivalTime={arrivalTime}");
            Assert.AreEqual(expected, arrivalTime);
        }

        [TestMethod]
        public void GetScheduledArrivalTimeMinutes_Daytime_Test()
        {
            DateTime today = DateTime.Today;
            DateTime now = today.AddHours(23);
            Trip trip = new Trip
            {
                AdjustmentAge = -1,
                TripStartTime = "26:30",
                AdjustedScheduleTime = 10,
            };

            TripTimeConverter converter = new TripTimeConverter(trip, today, now);
            int arrivalTime = converter.GetScheduledArrivalTimeMinutes();

            int expected = (60 * 3) + 30 + 10;
            Debug.WriteLine($"expected={expected} | arrivalTime={arrivalTime}");
            Assert.AreEqual(expected, arrivalTime);
        }

        [TestMethod]
        public void GetScheduledArrivalTimeMinutes_Midday_Test()
        {
            DateTime today = DateTime.Today;
            DateTime now = today.AddHours(10);
            Trip trip = new Trip
            {
                AdjustmentAge = -1,
                TripStartTime = "10:30",
                AdjustedScheduleTime = 10,
            };

            TripTimeConverter converter = new TripTimeConverter(trip, today, now);
            int arrivalTime = converter.GetScheduledArrivalTimeMinutes();

            int expected = 30 + 10;
            Debug.WriteLine($"expected={expected} | arrivalTime={arrivalTime}");
            Assert.AreEqual(expected, arrivalTime);
        }


        [TestMethod]
        public void GetScheduledArrivalTimeMinutes_Midnight_Test()
        {
            DateTime today = DateTime.Today;
            DateTime now = today.AddHours(1);
            Trip trip = new Trip
            {
                AdjustmentAge = -1,
                TripStartTime = "26:30",
                AdjustedScheduleTime = 10,
            };

            TripTimeConverter converter = new TripTimeConverter(trip, today, now);
            int arrivalTime = converter.GetScheduledArrivalTimeMinutes();

            int expected = (60 * 1) + 30 + 10;
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

            TripTimeConverter converter = new TripTimeConverter(trip);
            DateTime arrivalTime = converter.GetAdjustedArrivalTime(timeOfRequest);

            DateTime expected = timeOfRequest.AddMinutes(trip.AdjustedScheduleTime);
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

            TripTimeConverter converter = new TripTimeConverter(trip);
            int arrivalTime = converter.GetAdjustedArrivalTimeMinutes(timeOfRequest);
            
            int expected = 10;
            Debug.WriteLine($"expected={expected} | arrivalTime={arrivalTime}");
            Assert.AreEqual(expected, arrivalTime);
        }
    }
}
