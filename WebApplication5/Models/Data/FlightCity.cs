using System;
using System.Collections.Generic;

#nullable disable

namespace Logistics.Models.Data
{
    public partial class FlightCity
    {
        public int SecondId { get; set; }
        public int FlightId { get; set; }
        public int CityId { get; set; }
        public DateTime ArrivalDate { get; set; }

        public virtual CitiesTo City { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
