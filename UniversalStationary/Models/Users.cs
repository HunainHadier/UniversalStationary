namespace UniversalStationary.Models
{
    public class Users
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address {get; set;}

        public string? City { get; set;}

        public string? PostalCode { get; set;}

        public string? Role { get; set;} = "User";

        public string? ProfilePicture { get; set;}

        public string? Gender { get; set;}

        public DateTime? Created { get; set;} = DateTime.Now;

        public DateTime? Updated { get; set; } = DateTime.Now;
    }
}
