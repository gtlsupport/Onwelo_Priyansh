using Microsoft.EntityFrameworkCore;
using VotingApp.DE.DataModels;

namespace VotingApp.DE
{
    /// <summary>
    /// DbContext class representing the database context for the VotingApp application.
    /// This class defines the database tables and their relationships.
    /// </summary>
    public class VotingAppDbContext : DbContext
    {
        public VotingAppDbContext(DbContextOptions<VotingAppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet for Candidate data.
        /// Represents the Candidates table in the database.
        /// </summary>
        public DbSet<Candidate> Candidates { get; set; }

        /// <summary>
        /// DbSet for Voter data.
        /// Represents the Voters table in the database.
        /// </summary>
        public DbSet<Voter> Voters { get; set; }

        /// <summary>
        /// DbSet for Vote data.
        /// Represents the Votes table in the database.
        /// </summary>
        public DbSet<Vote> Votes { get; set; }

        /// <summary>
        /// Configures additional model creation rules.
        /// </summary>
        /// <param name="modelBuilder">The model builder instance for configuring the database model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vote>()
                // Configure a unique index on the VoterId property in the Votes table
                .HasIndex(v => v.VoterId)
                .IsUnique();// Enforces uniqueness for VoterId
        }
    }
}