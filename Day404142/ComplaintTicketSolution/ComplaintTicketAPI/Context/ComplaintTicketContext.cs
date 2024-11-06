using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Context
{
    public class ComplaintTicketContext : DbContext
    {

        public ComplaintTicketContext(DbContextOptions<ComplaintTicketContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User - Profile one-to-one relationship (Cascade delete)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne()
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure User - Organization one-to-one relationship (Cascade delete)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Organization)
                .WithOne()
                .HasForeignKey<Organization>(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure User - Complaint one-to-many relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Complaints)
                .WithOne(c => c.User) 
                // Reference to the navigation property in Complaint
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);


           

            // Configure Complaint - Organization one-to-one relationship
            modelBuilder.Entity<Complaint>()
                .HasOne<Organization>()
                .WithOne()
                .HasForeignKey<Complaint>(c => c.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Enum configurations
            modelBuilder.Entity<User>()
                .Property(u => u.Roles)
                .HasConversion<String>();

            modelBuilder.Entity<Complaint>()
                .Property(c => c.Status)
                .HasConversion<String>();

            modelBuilder.Entity<Complaint>()
                .Property(c => c.Priority)
                .HasConversion<String>();

            modelBuilder.Entity<Complaint>()
                .Property(c => c.Category)
                .HasConversion<String>();

            modelBuilder.Entity<Organization>()
                .Property(o => o.Types)
                .HasConversion<String>();
        }

    }
}
