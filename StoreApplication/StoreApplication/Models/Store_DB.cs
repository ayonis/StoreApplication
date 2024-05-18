using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Configuration;
using Store.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreApplication.Configuration;
using StoreApplication.Models;

namespace Store
{
    public class Store_DB : DbContext
    {
        private readonly IConfiguration _configuration;

        public Store_DB(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CustomerEntityTypeConfiguration().Configure(modelBuilder.Entity<Customer>());

            new ItemEntityTypeConfiguration().Configure(modelBuilder.Entity<Item>());

            new EmployeeEntityTypeConfiguration().Configure(modelBuilder.Entity<Employee>());

            new OrderEntityTypeConfiguration().Configure(modelBuilder.Entity<Order>());

            new OrderDetailEntityTypeConfiguration().Configure(modelBuilder.Entity<OrderDetail>());

            new CategoryEntityTypeConfiguration().Configure(modelBuilder.Entity<Category>());
            new CartEntityTypeConfiguration().Configure(modelBuilder.Entity<Cart>());

            new CartItemEntityTypeConfiguration().Configure(modelBuilder.Entity<CartItem>());
            new TreasuryTransactionEntityTypeConfiguration().Configure(modelBuilder.Entity<TreasuryTransaction>());

        }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<TreasuryAccount> TreasuryAccounts { get; set; }
        public DbSet<TreasuryTransaction> TreasuryTransactions { get; set; }

    }
}
