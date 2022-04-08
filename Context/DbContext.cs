using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VittaTest.Models;

#nullable disable

namespace VittaTest.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MoneyFlow> MoneyFlows { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Vitta;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<MoneyFlow>(entity =>
            {
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

                entity.Property(e => e.MoneyflowsId).HasColumnName("moneyflows_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PaymentAmount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("payment_amount");

                entity.HasOne(d => d.Moneyflows)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.MoneyflowsId)
                    .HasConstraintName("FK__Payments__moneyf__29572725");

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
