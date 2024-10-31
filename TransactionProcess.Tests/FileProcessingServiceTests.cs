using System.Text;
using Microsoft.Extensions.Logging;
using Moq;
using TransactionProcess.Core.Interfaces;
using TransactionProcess.Infrastructure.Services;
using TransactionProcess.Core.Entities;

namespace TransactionProcess.Tests
{
    public class FileProcessingServiceTests
    {
        private readonly Mock<ITransactionRepository> _mockRepository;
        private readonly Mock<ILogger<FileProcessingService>> _mockLogger;
        private readonly FileProcessingService _service;

        public FileProcessingServiceTests()
        {
            _mockRepository = new Mock<ITransactionRepository>();
            _mockLogger = new Mock<ILogger<FileProcessingService>>();
            _service = new FileProcessingService(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task ProcessFileAsync_ValidCsvFile_ReturnsTrue()
        {
            // Arrange
            var csvContent = @"TransactionId,AccountNumber,Amount,CurrencyCode,TransactionDate,Status
                             Invoice0000001,1234,1000.00,USD,20/02/2012 12:33:16,Approved
                             Invoice0000002,5678,300.00,USD,21/02/2012 02:04:59,Failed";

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));

            // Act
            var result = await _service.ProcessFileAsync(stream, "test.csv", "testuser");

            // Assert
            Assert.True(result);
            _mockRepository.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<TransactionRecord>>()), Times.Once);
        }

        [Fact]
        public async Task ProcessFileAsync_ValidXmlFile_ReturnsTrue()
        {
            // Arrange
            var xmlContent = @"<Transactions>
                                  <Transaction id=""Invoice0000001"">
                                    <TransactionDate>2023-01-23T13:45:10</TransactionDate>
                                    <PaymentDetails>
                                      <AccountNo>1234</AccountNo>
                                      <Amount>2,000.00</Amount>
                                      <CurrencyCode>USD</CurrencyCode>
                                    </PaymentDetails>
                                    <Status>Done</Status>
                                  </Transaction>
                                </Transactions>";

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent));

            // Act
            var result = await _service.ProcessFileAsync(stream, "test.xml", "testuser");

            // Assert
            Assert.True(result);
            _mockRepository.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<TransactionRecord>>()), Times.Once);
        }

        [Fact]
        public async Task ProcessFileAsync_InvalidFileFormat_ReturnsFalse()
        {
            // Arrange
            var content = "Invalid file content";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            // Act
            var result = await _service.ProcessFileAsync(stream, "test.txt", "testuser");

            // Assert
            Assert.False(result);
            _mockRepository.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<TransactionRecord>>()), Times.Never);
        }
    }
}