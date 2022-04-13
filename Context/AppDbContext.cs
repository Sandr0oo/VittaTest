using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using VittaTest.Models;

#nullable disable

namespace VittaTest.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        [ActivatorUtilitiesConstructor]
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MoneyInflow> MoneyInflows { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Vitta;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<MoneyInflow>(entity =>
            {
                entity.ToTable("MoneyInflow");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.MoneyAmount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("money_amount");

                entity.Property(e => e.RestMoney)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("rest_money");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AmountPayable)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("amount_payable");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.MoneyAmount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("money_amount");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MoneyInflowId).HasColumnName("money_inflow_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PaymentAmount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("payment_amount");

                entity.HasOne(d => d.MoneyInflow)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.MoneyInflowId)
                    .HasConstraintName("FK__Payments__money___29572725");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Payments__order___286302EC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
