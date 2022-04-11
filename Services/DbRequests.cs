using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VittaTest.Context;
using VittaTest.Models;

namespace VittaTest.Services
{
    public class DbRequests : IDbRequests
    {
        private readonly AppDbContext _dbContext;

        public DbRequests(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MoneyInflow>> GetAllMoneyInflows()
        {
            return await _dbContext.MoneyInflows.Select(m => m).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _dbContext.Orders.Select(o => o).ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _dbContext.Payments.Select(p => p).ToListAsync();
        }
    }
}
