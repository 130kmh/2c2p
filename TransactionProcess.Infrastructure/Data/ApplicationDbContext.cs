using Microsoft.EntityFrameworkCore;
using TransactionProcess.Core.Entities;

namespace TransactionProcess.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        public DbSet<TransactionRecord> TransactionRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionRecord>()
                .Property(t => t.TransactionId)
                .HasMaxLength(50);

            modelBuilder.Entity<TransactionRecord>()
                .Property(t => t.AccountNumber)
                .HasMaxLength(30);

            modelBuilder.Entity<TransactionRecord>()
                .Property(t => t.CurrencyCode)
                .HasMaxLength(3);

            modelBuilder.Entity<TransactionRecord>()
                .Property(t => t.Status)
                .HasMaxLength(1);

            modelBuilder.Entity<TransactionRecord>().HasData(
                new TransactionRecord
                {
                    Id = 1,
                    TransactionId = "Invoice0000001",
                    AccountNumber = "1234",
                    Amount = 1000.00M,
                    CurrencyCode = "USD",
                    TransactionDate = DateTime.Parse("2024-02-20 12:33:16"),
                    Status = "A",
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "testuser"
                },
                new TransactionRecord
                {
                    Id = 2,
                    TransactionId = "Invoice0000002",
                    AccountNumber = "5678",
                    Amount = 300.00M,
                    CurrencyCode = "EUR",
                    TransactionDate = DateTime.Parse("2024-02-21 02:04:59"),
                    Status = "R",
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "testuser"
                }
            );
        }
    }
}