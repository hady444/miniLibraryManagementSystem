using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace miniLibraryManagementSystem.Web.ViewModels
{
    public class BorrowBookViewModel
    {
        [Required(ErrorMessage = "Please select a book.")]
        [Display(Name = "Book")]
        public int BookId { get; set; }

        public IEnumerable<SelectListItem>? Books { get; set; }
        
    }
}
