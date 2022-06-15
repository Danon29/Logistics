using System;
using System.Collections.Generic;

#nullable disable

namespace Logistics.Models.Data
{
    public partial class Price
    {
        public Price()
        {
            Orders = new HashSet<Order>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public int CostPerKg { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
