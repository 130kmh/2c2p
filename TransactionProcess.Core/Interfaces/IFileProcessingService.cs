using System.IO;
using System.Threading.Tasks;

namespace TransactionProcess.Core.Interfaces
{
    public interface IFileProcessingService
    {
        Task<bool> ProcessFileAsync(Stream fileStream, string fileName, string createUser);
    }
}