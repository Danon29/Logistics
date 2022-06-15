using System;
using System.Collections.Generic;

#nullable disable

namespace Logistics.Models.Data
{
    public partial class Recipient
    {
        public Recipient()
        {
            Orders = new HashSet<Order>();
        }

        public int RecipientId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
