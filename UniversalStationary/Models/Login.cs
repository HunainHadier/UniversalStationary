using System.ComponentModel.DataAnnotations;

namespace UniversalStationary.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage ="Password must be requierd")]
        public string Password { get; set; } = string.Empty;
    }
}
