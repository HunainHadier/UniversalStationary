using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UniversalStationary.Vadlidations
{
    public class PasswordValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("Password is requierd");

            }

            var password = value as string;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperCase = new Regex(@"[A-Z]+");
            var hasLowerCase = new Regex(@"[a-z]+");
            var hasMinChar = new Regex(@".{8,}");
            var hasSpecialChar = new Regex(@"[\W]+");


            if (!hasNumber.IsMatch(password))
            {
                return new ValidationResult("Password Must has Number");
            }
            else if (!hasUpperCase.IsMatch(password))
            {
                return new ValidationResult("Password Must has 1 UpperCase");
            }
            else if (!hasLowerCase.IsMatch(password))
            {
                return new ValidationResult("Password Must has LowerCase");
            }
            else if (!hasMinChar.IsMatch(password))
            {
                return new ValidationResult("Password Must has 8 Characeter long");
            }
            else if (!hasSpecialChar.IsMatch(password))
            {
                return new ValidationResult("Password Must has Special Characeter");
            }

            return ValidationResult.Success;
        }
    }
}
