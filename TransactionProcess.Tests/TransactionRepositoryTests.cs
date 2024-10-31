using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TransactionProcess.Core.Entities;
using TransactionProcess.Infrastructure.Data;
using TransactionProcess.Infrastructure.Repositories;
using Xunit;

namespace TransactionProcess.Tests
{
    public class TransactionRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        private ApplicationDbContext CreateContext(DbContextOptions<ApplicationDbContext> options)
        {
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            // Seed the database
            context.TransactionRecords.AddRange(
                new TransactionRecord
                {
                    TransactionId = "Invoice0000001",
                    AccountNumber = "1234",
                    Amount = 100.00m,
                    CurrencyCode = "USD",
                    TransactionDate = DateTime.UtcNow.AddDays(-1),
                    Status = "A",
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "TestUser"
                },
                new TransactionRecord
                {
                    TransactionId = "Invoice0000002",
                    AccountNumber = "5678",
                    Amount = 200.00m,
                    CurrencyCode = "EUR",
                    TransactionDate = DateTime.UtcNow.AddDays(-2),
                    Status = "R",
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "TestUser"
                }
            );
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllTransactions()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = CreateContext(options);
            var mockLogger = new Mock<ILogger<TransactionRepository>>();
            var repository = new TransactionRepository(context, mockLogger.Object);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(12, result.Count());
        }

        [Fact]
        public async Task GetByCurrencyAsync_ReturnsMatchingTransactions()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = CreateContext(options);
            var mockLogger = new Mock<ILogger<TransactionRepository>>();
            var repository = new TransactionRepository(context, mockLogger.Object);

            // Act
            var result = await repository.GetByCurrencyAsync("USD");

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetByDateRangeAsync_ReturnsMatchingTransactions()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = CreateContext(options);
            var mockLogger = new Mock<ILogger<TransactionRepository>>();
            var repository = new TransactionRepository(context, mockLogger.Object);
            var startDate = DateTime.UtcNow.AddDays(-3);
            var endDate = DateTime.UtcNow;

            // Act
            var result = await repository.GetByDateRangeAsync(startDate, endDate);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByStatusAsync_ReturnsMatchingTransactions()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = CreateContext(options);
            var mockLogger = new Mock<ILogger<TransactionRepository>>();
            var repository = new TransactionRepository(context, mockLogger.Object);

            // Act
            var result = await repository.GetByStatusAsync("R");

            // Assert
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public async Task AddAsync_AddsNewTransaction()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = CreateContext(options);
            var mockLogger = new Mock<ILogger<TransactionRepository>>();
            var repository = new TransactionRepository(context, mockLogger.Object);
            var newTransaction = new TransactionRecord
            {
                TransactionId = "Invoice0000003",
                AccountNumber = "9876",
                Amount = 300.00m,
                CurrencyCode = "GBP",
                TransactionDate = DateTime.UtcNow,
                Status = "D",
                CreateDate = DateTime.UtcNow,
                CreateUser = "TestUser"
            };

            // Act
            await repository.AddAsync(newTransaction);

            // Assert
            var addedTransaction = await context.TransactionRecords.FindAsync(newTransaction.Id);
            Assert.NotNull(addedTransaction);
            Assert.Equal("Invoice0000003", addedTransaction.TransactionId);
        }
    }
}