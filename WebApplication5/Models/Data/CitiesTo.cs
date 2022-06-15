using System;
using System.Collections.Generic;

#nullable disable

namespace Logistics.Models.Data
{
    public partial class CitiesTo
    {
        public CitiesTo()
        {
            FlightCities = new HashSet<FlightCity>();
            Orders = new HashSet<Order>();
        }

        public int CitieToId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FlightCity> FlightCities { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
