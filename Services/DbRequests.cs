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
        private IDbContextFactory<AppDbContext> _dbContextFactory;

        public DbRequests(IDbContextFactory<AppDbContext> dbContext)
        {
            _dbContextFactory = dbContext;
        }

        public async Task<int> AddDataRange<T>(List<T> data) where T : class
        {
            using(var _dbContext = _dbContextFactory.CreateDbContext())
            {
                DbSet<T> entity = _dbContext.Set<T>();
                await entity.AddRangeAsync(data);
                return await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MoneyInflow>> GetAllMoneyInflows()
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                return await _dbContext.MoneyInflows.ToListAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                return await _dbContext.Orders.ToListAsync();
            }
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                return await _dbContext.Payments.ToListAsync();
            }
        }

        public async Task<IEnumerable<MoneyInflow>> LoadAllMoneyInflows()
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                return await _dbContext.MoneyInflows.Select(m => m).ToListAsync();
            }
        }

        public async Task<IEnumerable<Order>> LoadAllOrders()
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                //await _dbContext.Orders.LoadAsync();
                return await _dbContext.Orders.Select(o => o).ToListAsync();
            }
        }

        public async Task<IEnumerable<Payment>> LoadAllPayments()
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                return await _dbContext.Payments.Select(p => p).ToListAsync();
            }
        }

        public async Task<int> AddPay(Order selectedOrder, MoneyInflow selectedMoneyInflow, decimal payAmount)
        {
            Payment pay = new Payment()
            {
                PaymentAmount = payAmount,
                MoneyInflowId = selectedMoneyInflow.Id,
                OrderId = selectedOrder.Id
            };
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                await _dbContext.Payments.AddAsync(pay);
                return await _dbContext.SaveChangesAsync(); ;
            }
        }
    }
}
