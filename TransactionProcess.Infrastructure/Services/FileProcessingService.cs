using CsvHelper;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Xml.Linq;
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
                else if (Path.GetExtension(fileName).ToLower() == ".xml")
                {
                    transactions = await ProcessXmlAsync(fileStream, createUser);
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

        private async Task<List<TransactionRecord>> ProcessXmlAsync(Stream fileStream, string createUser)
        {
            var records = new List<TransactionRecord>();

            var xdoc = await XDocument.LoadAsync(fileStream, LoadOptions.None, default);

            foreach (var transactionElement in xdoc.Descendants("Transaction"))
            {
                var transaction = new TransactionRecord
                {
                    TransactionId = transactionElement.Attribute("id").Value,
                    AccountNumber = transactionElement.Element("PaymentDetails").Element("AccountNo").Value,
                    Amount = decimal.Parse(transactionElement.Element("PaymentDetails").Element("Amount").Value),
                    CurrencyCode = transactionElement.Element("PaymentDetails").Element("CurrencyCode").Value,
                    TransactionDate = DateTime.ParseExact(transactionElement.Element("TransactionDate").Value, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture),
                    Status = MapStatus(transactionElement.Element("Status").Value, false),
                    CreateDate = DateTime.UtcNow,
                    CreateUser = createUser
                };

                records.Add(transaction);
            }

            return records;
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
                    Status = MapStatus(record.Status, true),
                    CreateDate = DateTime.UtcNow,
                    CreateUser = createUser
                };

                transactionRecords.Add(transaction);
            }

            return transactionRecords;
        }

        private string MapStatus(string status, bool isCsv)
        {
            if (isCsv)
            {
                return status switch
                {
                    "Approved" => "A",
                    "Failed" => "R",
                    "Finished" => "D",
                    _ => throw new ArgumentException($"Invalid CSV status: {status}")
                };
            }
            else
            {
                return status switch
                {
                    "Approved" => "A",
                    "Rejected" => "R",
                    "Done" => "D",
                    _ => throw new ArgumentException($"Invalid XML status: {status}")
                };
            }
        }
    }
}