using System;
using System.Collections.Generic;

#nullable disable

namespace Logistics.Models.Data
{
    public partial class Order
    {
        public Order()
        {
            FlightOrders = new HashSet<FlightOrder>();
        }

        public int ContractNumber { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public int CityFromId { get; set; }
        public int CityToId { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int TotalCost { get; set; }

        public virtual CitiesFrom CityFrom { get; set; }
        public virtual CitiesTo CityTo { get; set; }
        public virtual Price Product { get; set; }
        public virtual Recipient Recipient { get; set; }
        public virtual Sender Sender { get; set; }
        public virtual ICollection<FlightOrder> FlightOrders { get; set; }
    }
}
