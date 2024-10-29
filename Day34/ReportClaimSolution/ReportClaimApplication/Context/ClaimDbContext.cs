using Microsoft.EntityFrameworkCore;
using ReportClaimApplication.Models;

namespace ReportClaimApplication.Context
{
    public class ClaimDbContext : DbContext

    {
        // Constructor to pass options to the DbContext
        public ClaimDbContext(DbContextOptions<ClaimDbContext> options)
     : base(options)
        { }

        // DbSet properties for each model (entity) in the application
        public DbSet<Claims> Claims { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Document> Documents { get; set; }

      
        // Method to configure the model's relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Define the one-to-many relationship between Claim and Document
            modelBuilder.Entity<Document>()
               .HasOne(d => d.claim)
               .WithMany(c => c.Documents) // A claim can have multiple documents
               .HasForeignKey(d => d.ClaimId) // Foreign key on Document side
               .HasConstraintName("FK_Documents_Claim")
               .OnDelete(DeleteBehavior.Cascade); // If a Claim is deleted, its documents are deleted

            // Define the one-to-one relationship between Claim and Policy
            modelBuilder.Entity<Claims>()
              .HasOne(c => c.Policy)
              .WithOne() // No navigation property in Policy, hence WithOne() is sufficient
              .HasForeignKey<Claims>(c => c.PolicyId) // Use PolicyId as foreign key
              .HasConstraintName("FK_claim_policy")
              .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete on Policy

        }
    }
}
