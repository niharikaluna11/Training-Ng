using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Context
{
    public class ComplaintTicketContext : DbContext
    {
        public ComplaintTicketContext(DbContextOptions<ComplaintTicketContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserOtp> UserOtp { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ComplaintFile> ComplaintFiles { get; set; }
        public DbSet<ComplaintCategory> ComplaintCategories { get; set; }
        public DbSet<ComplaintStatus> ComplaintStatuses { get; set; }
        public DbSet<ComplaintStatusDate> ComplaintStatusDates { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User - Profile one-to-one relationship (Cascade delete)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne()
                .HasForeignKey<UserProfile>(p => p.UserId)
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
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Complaint - ComplaintFile one-to-many relationship
            modelBuilder.Entity<Complaint>()
                .HasMany(c => c.ComplaintFiles)
                .WithOne(cf => cf.Complaint)
                .HasForeignKey(cf => cf.ComplaintId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Complaint - ComplaintStatus many-to-many relationship using ComplaintStatusDate
            modelBuilder.Entity<ComplaintStatusDate>()
                .HasKey(cs => new { cs.ComplaintId, cs.ComplaintStatusId }); // Composite key

            modelBuilder.Entity<ComplaintStatusDate>()
                .HasOne(cs => cs.Complaint)
                .WithMany(c => c.ComplaintStatusDates)
                .HasForeignKey(cs => cs.ComplaintId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ComplaintStatusDate>()
                .HasOne(cs => cs.ComplaintStatus)
                .WithMany(s => s.ComplaintStatusDates)
                .HasForeignKey(cs => cs.ComplaintStatusId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Complaint - ComplaintCategory one-to-many relationship
            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.Category)
                .WithMany(cc => cc.Complaints)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Complaint - Organization one-to-many relationship
            modelBuilder.Entity<Complaint>()
                .HasOne<Organization>()
                .WithMany()
                .HasForeignKey(c => c.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            
            // Enum configurations
            modelBuilder.Entity<User>()
                .Property(u => u.Roles)
                .HasConversion<string>();

            modelBuilder.Entity<ComplaintCategory>()
                .Property(cc => cc.Name)
                .HasConversion<string>();

            modelBuilder.Entity<ComplaintStatus>()
                .Property(cs => cs.Status)
                .HasConversion<string>();

            modelBuilder.Entity<ComplaintStatus>()
                .Property(cs => cs.Priority)
                .HasConversion<string>();

            modelBuilder.Entity<Organization>()
                .Property(o => o.Types)
                .HasConversion<string>();


            modelBuilder.Entity<User>()
              .HasIndex(u => u.Username)
              .IsUnique();

            modelBuilder.Entity<User>()
              .HasIndex(u => u.Email)
              .IsUnique();
        }
    }
}
