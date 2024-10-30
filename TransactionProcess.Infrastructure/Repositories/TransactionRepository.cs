using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransactionProcess.Core.Entities;
using TransactionProcess.Core.Interfaces;
using TransactionProcess.Infrastructure.Data;
namespace TransactionProcess.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TransactionRepository> _logger;

        public TransactionRepository(ApplicationDbContext context, ILogger<TransactionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<TransactionRecord>> GetAllAsync()
        {
            return await _context.TransactionRecords.ToListAsync();
        }

        public async Task<IEnumerable<TransactionRecord>> GetByCurrencyAsync(string currencyCode)
        {
            return await _context.TransactionRecords
                .Where(t => t.CurrencyCode == currencyCode)
                .ToListAsync();
        }

        public async Task<IEnumerable<TransactionRecord>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.TransactionRecords
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TransactionRecord>> GetByStatusAsync(string status)
        {
            return await _context.TransactionRecords
                .Where(t => t.Status == status)
                .ToListAsync();
        }

        public async Task AddAsync(TransactionRecord transaction)
        {
            await _context.TransactionRecords.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}