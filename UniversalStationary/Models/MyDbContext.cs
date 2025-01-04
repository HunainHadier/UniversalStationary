using Microsoft.EntityFrameworkCore;

namespace UniversalStationary.Models
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Users> users { get; set; }

        public DbSet<AddProductModel> addproduct { get; set; }

        public DbSet<LogoChangeModel> logochanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var adminpassword = BCrypt.Net.BCrypt.HashPassword("SuperAmin!123");
            modelBuilder.Entity<Users>().HasData(new Users
            {
                Id = Guid.NewGuid(),
                UserName = "SuperAdmin",
                Email = "superadmin@gmail.com",
                Password = adminpassword,
                PhoneNumber = "1234567890",
                Address = "Malir Karachi",
                ProfilePicture = "AllImages/userimage.png",
                City = "Karachi",
                Gender = "Male",
                Role = "Admin",
                PostalCode = "12345",





            });


        }
    }
}
