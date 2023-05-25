using ManageStocks.ApplicationCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStocks.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the one-to-one relationship
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Stock)
            //    .WithOne(s => s.Order)
            //    .HasForeignKey<Stock>(s => s.OrderId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
