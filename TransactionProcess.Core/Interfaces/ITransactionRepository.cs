using TransactionProcess.Core.Entities;

namespace TransactionProcess.Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionRecord>> GetAllAsync();
        Task<IEnumerable<TransactionRecord>> GetByCurrencyAsync(string currencyCode);
        Task<IEnumerable<TransactionRecord>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TransactionRecord>> GetByStatusAsync(string status);
        Task AddAsync(TransactionRecord transaction);
        Task AddRangeAsync(IEnumerable<TransactionRecord> transactions);
    }
}