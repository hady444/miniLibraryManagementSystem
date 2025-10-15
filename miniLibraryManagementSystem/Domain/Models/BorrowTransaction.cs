using System.ComponentModel.DataAnnotations;

namespace miniLibraryManagementSystem.Domain.Models
{
    public class BorrowTransaction
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public string? BookName { get; set; }
        public Book? Book { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
