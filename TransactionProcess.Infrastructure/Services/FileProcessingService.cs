using CsvHelper;
using Microsoft.Extensions.Logging;
using System.Globalization;
using TransactionProcess.Core.Entities;
using TransactionProcess.Core.Interfaces;

namespace TransactionProcess.Infrastructure.Services
{
    public class FileProcessingService : IFileProcessingService
    {
        private readonly ITransactionRepository _repository;
        private readonly ILogger<FileProcessingService> _logger;

        public FileProcessingService(ITransactionRepository repository, ILogger<FileProcessingService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> ProcessFileAsync(Stream fileStream, string fileName, string createUser)
        {
            try
            {
                List<TransactionRecord> transactions;

                if (Path.GetExtension(fileName).ToLower() == ".csv")
                {
                    transactions = await ProcessCsvAsync(fileStream, createUser);
                }
                else
                {
                    _logger.LogError($"Unsupported file format: {fileName}");
                    return false;
                }

                await _repository.AddRangeAsync(transactions);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing file: {fileName}");
                return false;
            }
        }

        private async Task<List<TransactionRecord>> ProcessCsvAsync(Stream fileStream, string createUser)
        {
            using var reader = new StreamReader(fileStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            
            var transactionRecords = new List<TransactionRecord>();

            await foreach (var record in csv.GetRecordsAsync<dynamic>())
            {
                var transaction = new TransactionRecord
                {
                    TransactionId = record.TransactionId,
                    AccountNumber = record.AccountNumber,
                    Amount = decimal.Parse(record.Amount),
                    CurrencyCode = record.CurrencyCode,
                    TransactionDate = DateTime.ParseExact(record.TransactionDate, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture),
                    Status = MapStatus(record.Status),
                    CreateDate = DateTime.UtcNow,
                    CreateUser = createUser
                };

                transactionRecords.Add(transaction);
            }

            return transactionRecords;
        }

        private string MapStatus(string status)
        {
            return status switch
            {
                "Approved" => "A",
                "Failed" => "R",
                "Finished" => "D",
                _ => throw new ArgumentException($"Invalid CSV status: {status}")
            };
        }
    }
}