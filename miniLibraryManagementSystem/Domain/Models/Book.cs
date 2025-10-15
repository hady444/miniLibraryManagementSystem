using miniLibraryManagementSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace miniLibraryManagementSystem.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }

        public Genre Genre { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;


        [MaxLength(300)]
        public string? Description { get; set; }

        // Author
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        // Tracking borrow status
        public bool IsBorrowed { get; set; } = false;

        public BorrowTransaction? CurrentBorrow { get; set; }

    }
}
