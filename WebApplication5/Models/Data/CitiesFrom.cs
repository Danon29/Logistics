using System;
using System.Collections.Generic;

#nullable disable
namespace Logistics.Models.Data
{
    public partial class CitiesFrom
    {
        public CitiesFrom()
        {
            Orders = new HashSet<Order>();
        }

        public int CityFromId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
