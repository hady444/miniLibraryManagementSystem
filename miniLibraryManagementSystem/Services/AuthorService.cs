using miniLibraryManagementSystem.Data.LibraryContext;
using miniLibraryManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace miniLibraryManagementSystem.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryContext _ctx;
        public AuthorService(LibraryContext ctx) { _ctx = ctx; }


        public async Task<IEnumerable<Author>> GetAllAsync() => await _ctx.Authors.Include(a => a.Books).ToListAsync();


        public async Task<Author> GetByIdAsync(int id) => await _ctx.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);


        public async Task<Author> CreateAsync(Author author)
        {
            // validation
            if (await _ctx.Authors.AnyAsync(a => a.Email == author.Email))
                throw new InvalidOperationException("Author email must be unique.");

            _ctx.Authors.Add(author);
            await _ctx.SaveChangesAsync();
            return author;
        }


        public async Task UpdateAsync(Author author)
        {
            var exists = await _ctx.Authors.AnyAsync(a => a.Email == author.Email && a.Id != author.Id);
            if (exists) throw new InvalidOperationException("Email already in use by another author. email must be unique");


            _ctx.Authors.Update(author);
            await _ctx.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var author = await _ctx.Authors.FindAsync(id);
            if (author == null) return;
            _ctx.Authors.Remove(author);
            await _ctx.SaveChangesAsync();
        }
        public async Task<bool> AnyAsync(string email, int authorId)
        {
            return await _ctx.Authors.AnyAsync(a => a.Email == email && a.Id != authorId);
        }
        
    }
}
