using System;
using System.Collections.Generic;

#nullable disable

namespace Logistics.Models.Data
{
    public partial class Flight
    {
        public Flight()
        {
            FlightCities = new HashSet<FlightCity>();
            FlightOrders = new HashSet<FlightOrder>();
        }

        public int FlightId { get; set; }
        public int DriverId { get; set; }
        public int CarId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Car Car { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual ICollection<FlightCity> FlightCities { get; set; }
        public virtual ICollection<FlightOrder> FlightOrders { get; set; }
    }
}
