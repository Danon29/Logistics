using System;
using System.Collections.Generic;

#nullable disable

namespace Logistics.Models.Data
{
    public partial class Sender
    {
        public Sender()
        {
            Orders = new HashSet<Order>();
        }

        public int SenderId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
