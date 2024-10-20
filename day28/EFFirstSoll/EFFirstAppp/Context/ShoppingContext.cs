using EFFirstAppp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFFirstAppp.Context
{
    public class ShoppingContext : DbContext
    {
        //we don
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=5CD413DKS0\\DEMOINSTANCE;Integrated Security=true;Initial Catalog=NG15Oct;TrustServerCertificate=True");
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<SupplierProduct> SupplierProduct { get; set; }

    }
}
