using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VittaTest.Context;
using VittaTest.Models;
using VittaTest.Services;

namespace VittaTest
{
    public class Locator
    {
        private static ServiceProvider _provider;

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddDbContextFactory<AppDbContext>();
            services.AddScoped<DbRequests>();

            services.AddScoped<MainWindowViewModel>();

            services.AddSingleton<MainWindow>();

            services.AddTransient<MoneyInflow>();
            services.AddTransient<Order>();
            services.AddTransient<Payment>();

            _provider = services.BuildServiceProvider();
        }

        public MainWindowViewModel MainViewModel => _provider.GetRequiredService<MainWindowViewModel>();
    }
}
