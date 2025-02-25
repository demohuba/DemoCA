using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace JEPCO.Shared.Attributes;

public class PasswordAttribute : ValidationAttribute
{
    private static readonly Regex PasswordRegEx = new Regex(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&(),.?""\:\{\}\|<>])[A-Za-z\d!@#$%^&(),.?""\:\{\}\|<>]{6,}$",
        RegexOptions.Compiled
    );

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string password)
        {
            if (PasswordRegEx.IsMatch(password))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Password does not meet the complexity requirements.");
            }
        }

        return new ValidationResult("Password is required.");
    }
}
