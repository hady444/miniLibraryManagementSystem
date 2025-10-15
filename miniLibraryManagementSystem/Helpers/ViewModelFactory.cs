using miniLibraryManagementSystem.Data.LibraryContext;
using miniLibraryManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using miniLibraryManagementSystem.Services;
using miniLibraryManagementSystem.Web.ViewModels;

namespace miniLibraryManagementSystem.Helpers
{
    public class viewModelFactory
    {
        public static BorrowBookViewModel CreateBorrowBookViewModel(IEnumerable<Book> books)
        {
            return new BorrowBookViewModel
            {
                Books = books
                    .Where(b => !b.IsBorrowed)
                    .Select(b => new SelectListItem
                    {
                        Value = b.Id.ToString(),
                        Text = b.Title
                    })
                    .ToList()
            };
        }
    }
}
