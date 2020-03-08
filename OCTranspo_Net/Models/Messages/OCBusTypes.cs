using System;
using System.Collections.Generic;
using System.Text;

namespace OCTranspo_Net.Models.Messages
{
    public static class OCBusTypes
    {
        public static Dictionary<string, string> BusTypes = new Dictionary<string, string>()
        {
            { "4", "40-Foot Busses" },
            { "40", "40-Foot Busses" },
            
            { "6", "60-Foot Busses" },
            { "60", "60-Foot Busses" },
            
            { "DD", "Double Decker Busses" },
            
            { "E", "Low FLoor Easy Access" },
            { "L", "Low FLoor Easy Access" },
            { "A", "Low FLoor Easy Access" },
            { "EA", "Low FLoor Easy Access" },

            { "B", "Bike Rack" },
            { "DEH", "Deisel Electric Hybrid" },
            { "IN", "INVIRO (Bus Type)" },
            { "ON", "ORION (Bus Type)" }
        };
    }
}
