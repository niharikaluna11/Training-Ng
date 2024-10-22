using EFFirstAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EFFirstAPI.Context
{
    public class ShoppingContext : DbContext
    {
        //injection done in program file
        public ShoppingContext(DbContextOptions contextOptions)  : base(contextOptions) 
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<User> Users { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().HasKey(ci => ci.SNo).HasName("PK_CartItem");

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .HasConstraintName("FK_Order_Customer");

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .HasConstraintName("FK_OrderDetail_Product");

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderNumber)
                .HasConstraintName("FK_OrderDetail_Order");

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithOne(c => c.Cart)
                .HasForeignKey<Cart>(c => c.CustomerId)
                .HasConstraintName("FK_Cart_Customer");

            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId)
                .HasConstraintName("FK_ProductImage_Product");


            modelBuilder.Entity<Customer>()
               .HasOne(c => c.User)
               .WithOne(u => u.Customer)
               .HasForeignKey<Customer>(c => c.Username)
               .HasConstraintName("FK_Customer_User");

        

        }


    }
}
