using ApplyForClaimApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplyForClaimApplication.Context
{
    public class ApplyClaimDBContext : DbContext
    {
        public ApplyClaimDBContext(DbContextOptions<ApplyClaimDBContext> options) : base(options) { }

        // DbSets for each entity
        public DbSet<Policy> Policies { get; set; }
        public DbSet<ClaimData> Claims { get; set; }
        public DbSet<ClaimType> ClaimTypes { get; set; }
        public DbSet<DocumentData> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defining the primary keys (if not using [Key] in the model class)
            modelBuilder.Entity<Policy>().HasKey(p => p.PolicyNumber);
            modelBuilder.Entity<ClaimData>().HasKey(c => c.ClaimId);
            modelBuilder.Entity<ClaimType>().HasKey(ct => ct.Id);
            modelBuilder.Entity<DocumentData>().HasKey(d => d.DocumentId);

            // Relationships

            // One-to-Many between ClaimData and DocumentData (One claim can have many documents)
            modelBuilder.Entity<DocumentData>()
                .HasOne(d => d.claim)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.ClaimId);

            // One-to-Many between Policy and ClaimType (One claim type can be associated with many policies)
            modelBuilder.Entity<Policy>()
                .HasOne<ClaimType>()
                .WithMany()
                .HasForeignKey(p => p.PolicyType)
                .OnDelete(DeleteBehavior.Restrict); // Optional: define delete behavior

            // One-to-Many between Policy and ClaimType (One claim type can be associated with many policies)
            modelBuilder.Entity<ClaimData>()
                .HasOne<ClaimType>()
                .WithMany()
                .HasForeignKey(p => p.ClaimId);
            // Optional: define delete behavior

            // Defining relationship between Policy and ClaimData where InsuredName can be null in Policy but required in ClaimData
            modelBuilder.Entity<ClaimData>()
        .Property(c => c.ClaimantName)
        .IsRequired();

            modelBuilder.Entity<Policy>()
                .Property(p => p.InsuredName)
                .IsRequired(false); // InsuredName in Policy can be null

        }

    }
}
