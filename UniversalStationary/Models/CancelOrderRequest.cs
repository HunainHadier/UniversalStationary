using System.ComponentModel.DataAnnotations;

namespace UniversalStationary.Models
{
    public class CancelOrderRequest
    {
        public Guid ProductId { get; set; }
    

        [Required(ErrorMessage = "User email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string UserEmail { get; set; }
    }
}
