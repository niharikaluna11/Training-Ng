
using BankManagementApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BankManagementApplication.Context
{
    public class BankManagementContext : DbContext
    {

        public BankManagementContext(DbContextOptions<BankManagementContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Customer>()
                .Property(u=>u.AccountType)
                .HasConversion<string>();


            modelBuilder.Entity<Customer>()
             .Property(u => u.CStatus)
             .HasConversion<string>();
            


        }
    }
}
