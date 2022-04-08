using System;
using System.Collections.Generic;

#nullable disable

namespace VittaTest.Models
{
    public partial class Order
    {
        public Order()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal MoneyAmount { get; set; }
        public decimal AmountPayable { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
