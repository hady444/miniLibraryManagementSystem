using miniLibraryManagementSystem.Domain.Models;

namespace miniLibraryManagementSystem.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<PagedResult<Book>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<Book> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);

    }
}
