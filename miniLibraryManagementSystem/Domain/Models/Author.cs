using Library_Management_System.Domain.Validation_Attributes;
using System.ComponentModel.DataAnnotations;

namespace miniLibraryManagementSystem.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }


        [Required]
        [AuthorFullName] // custom validation attribute 
        public string FullName { get; set; } = string.Empty;


        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        [Url]
        public string? Website { get; set; }


        [MaxLength(300)]
        public string? Bio { get; set; }


        // Read-only navigation
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
