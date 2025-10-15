using miniLibraryManagementSystem.Domain.Models;

namespace miniLibraryManagementSystem.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task<Author> CreateAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(int id);
        Task<bool> AnyAsync(string email, int authorId);
    }
}
