using miniLibraryManagementSystem.Domain.Models;

namespace miniLibraryManagementSystem.Services
{
    public interface IBorrowingService
    {
        Task<IEnumerable<BorrowTransaction>> GetTransactionsAsync();
        Task<BorrowTransaction> BorrowAsync(int bookId);
        Task<BorrowTransaction> ReturnAsync(int borrowTransactionId);
    }
}
