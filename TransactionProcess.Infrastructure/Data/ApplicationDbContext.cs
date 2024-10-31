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
                },
                new TransactionRecord
                {
                    Id = 3,
                    TransactionId = "Invoice0000003",
                    AccountNumber = "2345",
                    Amount = 500.00M,
                    CurrencyCode = "GBP",
                    TransactionDate = DateTime.Parse("2024-03-01 14:23:10"),
                    Status = "D",
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "testuser"
                },
                new TransactionRecord
                {
                    Id = 4,
                    TransactionId = "Invoice0000004",
                    AccountNumber = "6789",
                    Amount = 1200.00M,
                    CurrencyCode = "USD",
                    TransactionDate = DateTime.Parse("2024-03-10 10:30:05"),
                    Status = "A",
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "testuser"
                },
                new TransactionRecord
                {
                    Id = 5,
                    TransactionId = "Invoice0000005",
                    AccountNumber = "3456",
                    Amount = 750.00M,
                    CurrencyCode = "CAD",
                    TransactionDate = DateTime.Parse("2024-04-15 18:45:25"),
                    Status = "R",
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "testuser"
                },
                new TransactionRecord
                {
                    Id = 6,
                    TransactionId = "Invoice0000006",
                    AccountNumber = "7890",
                    Amount = 200.00M,
                    CurrencyCode = "AUD",
                    TransactionDate = DateTime.Parse("2024-05-20 08:15:30"),
                    Status = "D",
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "testuser"
                },
                new TransactionRecord
                {
                    Id = 7,
                    TransactionId = "Invoice0000007",
                    AccountNumber = "4567",
                    Amount = 950.00M,
                    CurrencyCode = "EUR",
                    TransactionDate = DateTime.Parse("2024-06-12 11:25:45"),
                    Status = "A",
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "testuser"
                },
                new TransactionRecord
                {
                    Id = 8,
                    TransactionId = "Invoice0000008",
                    AccountNumber = "8901",
                    Amount = 1300.00M,
                    CurrencyCode = "JPY",
                    TransactionDate = DateTime.Parse("2020-07-05 09:50:10"),
                    Status = "R",
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "testuser"
                },
                new TransactionRecord
                {
                    Id = 9,
                    TransactionId = "Invoice0000009",
                    AccountNumber = "5670",
                    Amount = 450.00M,
                    CurrencyCode = "INR",
                    TransactionDate = DateTime.Parse("2005-08-14 16:40:00"),
                    Status = "D",
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "testuser"
                },
                new TransactionRecord
                {
                    Id = 10,
                    TransactionId = "Invoice0000010",
                    AccountNumber = "2341",
                    Amount = 1600.00M,
                    CurrencyCode = "CHF",
                    TransactionDate = DateTime.Parse("1999-09-22 06:10:50"),
                    Status = "A",
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "testuser"
                }
            );
        }
    }
}