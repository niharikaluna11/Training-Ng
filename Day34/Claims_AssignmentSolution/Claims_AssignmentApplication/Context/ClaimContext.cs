using Claims_AssignmentApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace Claims_AssignmentApplication.Context
{
    
        public class ClaimContext : DbContext

        {
            // Constructor to pass options to the DbContext
            public ClaimContext(DbContextOptions<ClaimContext> options)
                 : base(options)
            { }

            // DbSet properties for each model (entity) in the application
            public DbSet<Claims> Claims { get; set; }
            public DbSet<Document> Documents { get; set; }


            // Method to configure the model's relationships and constraints
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                    modelBuilder.Entity<Claims>()
                    .HasKey(c => c.ClaimId); // Set ClaimId as primary key

                    modelBuilder.Entity<Document>()
                   .HasKey(d => d.DocumentId); 


            // Define the one-to-many relationship between Claim and Document
            modelBuilder.Entity<Document>()
                   .HasOne(d => d.claim)
                   .WithMany(c => c.Documents) // A claim can have multiple documents
                   .HasForeignKey(d => d.ClaimId) // Foreign key on Document side
                   .HasConstraintName("FK_Documents_Claim")
                   .OnDelete(DeleteBehavior.Cascade); // If a Claim is deleted, its documents are deleted

              

            }
        }
    }

