using System;
using System.Collections.Generic;

#nullable disable

namespace Logistics.Models.Data
{
    public partial class Car
    {
        public Car()
        {
            Flights = new HashSet<Flight>();
        }

        public int CarId { get; set; }
        public string CarNumber { get; set; }
        public string Color { get; set; }
        public string LoadCapacity { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
