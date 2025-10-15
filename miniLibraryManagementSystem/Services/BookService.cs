using miniLibraryManagementSystem.Data.LibraryContext;
using miniLibraryManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace miniLibraryManagementSystem.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _ctx;

        public BookService(LibraryContext ctx) { _ctx = ctx; }

        public async Task<IEnumerable<Book>> GetAllAsync() => await _ctx.Books.Include(b => b.Author).ToListAsync();
        public async Task<Book?> GetByIdAsync(int id) => await _ctx.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id==id);


        public async Task<Book> CreateAsync(Book book)
        {
            Console.WriteLine(_ctx.BorrowTransactions.Count());
            // no duplicate title constraint required in spec but could be added
            _ctx.Books.Add(book);
            await _ctx.SaveChangesAsync();
            return book;
        }


        public async Task UpdateAsync(Book book)
        {
            var existingBook = await _ctx.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == book.Id);
            if (existingBook == null)
            {
                _ctx.Books.Update(book);
                await _ctx.SaveChangesAsync();
            }
            if (!existingBook.IsBorrowed && book.IsBorrowed)
            {
                var borrow = new BorrowTransaction
                {
                    BookId = book.Id,
                    BookName = book.Title,
                    BorrowedDate = DateTime.UtcNow
                };
                _ctx.BorrowTransactions.Add(borrow);
            }
            else if (existingBook.IsBorrowed && !book.IsBorrowed)
            {
                var borrow = await _ctx.BorrowTransactions.FirstOrDefaultAsync(tr=>tr.BookId == book.Id && tr.ReturnedDate == null);
                borrow.ReturnedDate = DateTime.UtcNow;
                _ctx.BorrowTransactions.Update(borrow);
            }
            _ctx.Books.Update(book);
            await _ctx.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var book = await _ctx.Books.FindAsync(id);
            if (book == null) return;
            _ctx.Books.Remove(book);
            await _ctx.SaveChangesAsync();
        }
    }
}
