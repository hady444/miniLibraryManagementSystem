using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Domain.Validation_Attributes
{
    public class AuthorFullNameAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Full name is required.");
            var str = value?.ToString()?.Trim();
            if (string.IsNullOrEmpty(str)) return new ValidationResult("Full name is required.");
            var parts = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 4) return new ValidationResult("Full name must have exactly 4 names.");
            if (parts.Any(p => p.Length < 2)) return new ValidationResult("Each name must be at least 2 characters.");
            return ValidationResult.Success;
        }
    }
}
