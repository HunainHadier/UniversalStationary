namespace UniversalStationary.Models
{
    public class Order
    {
        public Guid Id { get; set; }           // Unique Order ID
        public Guid ProductId { get; set; }    // Product being ordered
        public string UserName { get; set; }   // User's Name
        public string Email { get; set; }      // User's Email
        public string Address { get; set; }    // User's Shipping Address
        public string City { get; set; }       // User's City
        public string PaymentMethod { get; set; } // Payment Method
        public DateTime OrderDate { get; set; }  = DateTime.Now;
    }
}
