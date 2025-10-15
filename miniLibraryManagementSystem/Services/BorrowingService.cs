using miniLibraryManagementSystem.Data.LibraryContext;
using miniLibraryManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace miniLibraryManagementSystem.Services
{
    public class BorrowingService: IBorrowingService
    {
        private readonly LibraryContext _ctx;
        public BorrowingService(LibraryContext ctx) { _ctx = ctx; }

        public async Task<IEnumerable<BorrowTransaction>> GetTransactionsAsync()
        {
            return await _ctx.BorrowTransactions
                .Include(t => t.Book)
                .OrderByDescending(t => t.BorrowedDate)
                .ToListAsync();
        }

        public async Task<BorrowTransaction> BorrowAsync(int bookId)
        {
            var book = await _ctx.Books
                .Include(b => b.CurrentBorrow)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
                throw new InvalidOperationException("Book not found.");

            if (book.IsBorrowed)
                throw new InvalidOperationException("This book is already borrowed.");

            var transaction = new BorrowTransaction
            {
                BookId = bookId,
                BookName = book.Title,
                BorrowedDate = DateTime.UtcNow
            };

            book.IsBorrowed = true;
            book.CurrentBorrow = transaction;

            _ctx.BorrowTransactions.Add(transaction);
            await _ctx.SaveChangesAsync();

            return transaction;
        }

        public async Task<BorrowTransaction> ReturnAsync(int borrowTransactionId)
        {
            var transaction = await _ctx.BorrowTransactions
                .Include(t => t.Book)
                .FirstOrDefaultAsync(t => t.Id == borrowTransactionId);

            if (transaction == null)
                throw new InvalidOperationException("Borrow transaction not found.");

            if (transaction.ReturnedDate.HasValue)
                throw new InvalidOperationException("Book has already been returned.");

            transaction.ReturnedDate = DateTime.UtcNow;
            var book = await _ctx.Books.FirstOrDefaultAsync(b => b.Id==transaction.BookId);
            // Mark the book as available again

            book.IsBorrowed = false;
            book.CurrentBorrow = null;

            await _ctx.SaveChangesAsync();

            return transaction;
        }
    }
}
