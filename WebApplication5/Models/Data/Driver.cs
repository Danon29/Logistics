using System;
using System.Collections.Generic;

#nullable disable

namespace Logistics.Models.Data
{
    public partial class Driver
    {
        public Driver()
        {
            Flights = new HashSet<Flight>();
        }

        public int DriverId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
