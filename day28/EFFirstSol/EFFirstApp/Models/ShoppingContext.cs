using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EFFirstApp.Models
{
    public class ShoppingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=5CD413DKS0\\DEMOINSTANCE;Integrated Security=true;Initial Catalog=dbEFCode15Oct24;TrustServerCertificate=True");
        }
        public DbSet<Product> Products { get; set; }
    }

}
