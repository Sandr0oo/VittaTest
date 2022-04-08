using System;
using System.Collections.Generic;

#nullable disable

namespace VittaTest.Models
{
    public partial class MoneyFlow
    {
        public MoneyFlow()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal MoneyAmount { get; set; }
        public decimal RestMoney { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
