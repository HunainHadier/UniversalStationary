namespace UniversalStationary.Models
{
    public class OrderRequest
    {
        public Guid ProductId { get; set; }  // Product ID
        public string UserName { get; set; } // User's Name
        public string Email { get; set; }    // User's Email
        public string Address { get; set; }  // Shipping Address
        public string City { get; set; }     // City
        public string PaymentMethod { get; set; } // Payment Method
    }
}
