using Microsoft.EntityFrameworkCore;
using miniLibraryManagementSystem.Domain.Models;

namespace miniLibraryManagementSystem.Data.LibraryContext
{
    public class LibraryContext:DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> opts) : base(opts) { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowTransaction> BorrowTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
