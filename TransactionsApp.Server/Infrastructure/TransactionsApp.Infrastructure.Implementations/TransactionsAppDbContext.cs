using Microsoft.EntityFrameworkCore;
using TransactionsApp.Domain.Models.Entities;

namespace TransactionsApp.Infrastructure.Implementations
{
    /// <summary>
    /// Represents the database context for the application.
    /// </summary>
    public class TransactionsAppDbContext : DbContext
    {
        public TransactionsAppDbContext(DbContextOptions<TransactionsAppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Configures the schema needed for the database tables and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.UserIdentity)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(u => u.FullNameHebrew)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(u => u.FullNameEnglish)
                    .IsRequired()
                    .HasMaxLength(15);

            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Amount)
                      .IsRequired()
                      .HasPrecision(10, 0);

                entity.Property(t => t.AccountNumber)
                      .IsRequired()
                      .HasMaxLength(10)
                      .IsUnicode(false);
            });
        }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
