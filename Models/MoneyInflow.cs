using System;
using System.Collections.Generic;

#nullable disable

namespace VittaTest.Models
{
    public partial class MoneyInflow
    {
        public MoneyInflow()
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
