using System;
using System.Collections.Generic;

#nullable disable

namespace VittaTest.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? MoneyInflowId { get; set; }
        public decimal PaymentAmount { get; set; }

        public virtual MoneyInflow MoneyInflow { get; set; }
        public virtual Order Order { get; set; }
    }
}
