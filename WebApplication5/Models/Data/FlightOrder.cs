using System;
using System.Collections.Generic;

#nullable disable

namespace Logistics.Models.Data
{
    public partial class FlightOrder
    {
        public int FlightId { get; set; }
        public int ContractNumber { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FirstId { get; set; }

        public virtual Order ContractNumberNavigation { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
