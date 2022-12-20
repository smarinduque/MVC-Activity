using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCProject.Context
{
    public class CustomerContext : DbContext
    {
        public CustomerContext() : base("CustomerContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Order_items> OrdersItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Customer>().Property(c => c.ContactName).HasMaxLength(250);
            modelBuilder.Entity<Customer>().Property(c => c.FirstName).HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.LastName).HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.MiddleName).HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.Gender).HasMaxLength(10);
            modelBuilder.Entity<Customer>().Property(c => c.LocalAddress).HasMaxLength(256);
            modelBuilder.Entity<Customer>().Property(c => c.TelNo).HasMaxLength(150);
            modelBuilder.Entity<Customer>().Property(c => c.MobileNO).HasMaxLength(150);

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Product>().Property(p => p.Code).HasMaxLength(10);
            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(50);

            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Product>().HasKey(o => o.Id);
            modelBuilder.Entity<Product>().Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Order>().Property(o => o.OrderNo).HasMaxLength(10);
            modelBuilder.Entity<Order>().HasRequired(o => o.customer).WithMany().HasForeignKey(d => d.CustomerId);

            modelBuilder.Entity<Order_items>()
             .ToTable("Orders_Items");
            modelBuilder.Entity<Order_items>().HasKey(i => i.Id);
            modelBuilder.Entity<Order_items>().Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Order_items>().Property(i => i.ProductCode).HasMaxLength(10);
            modelBuilder.Entity<Order_items>().Property(i => i.ProductName).HasMaxLength(100);
            modelBuilder.Entity<Order_items>().Property(i => i.Quantity).HasPrecision(18, 2);
            modelBuilder.Entity<Order_items>().Property(i => i.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Order_items>().Property(i => i.Amount).HasPrecision(18, 2);
            modelBuilder.Entity<Order_items>().HasRequired(i => i.Order).WithMany(x => x.Items).HasForeignKey(d => d.OrderId);
            modelBuilder.Entity<Order_items>().HasRequired(i => i.Product).WithMany().HasForeignKey(d => d.ProductId);
        }
    }
}