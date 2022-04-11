using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VittaTest.Models;

namespace VittaTest.Services
{
    public interface IDbRequests
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IEnumerable<MoneyInflow>> GetAllMoneyInflows();
        Task<IEnumerable<Payment>> GetAllPayments();
    }
}
