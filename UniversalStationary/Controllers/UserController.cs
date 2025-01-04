
using UniversalStationary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;


namespace UniversalStationary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

       private readonly MyDbContext _dbContext;
         public UserController(MyDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserViewModel model)
        {
            if (!ModelState.IsValid) { 
            
                return BadRequest(new {error = new {massage = "Invalid data provided"}});
            
            }
            if (await EmailExist(model.Email)) {


                return BadRequest(new { error = new { massage = "Email is Already Exist" } });

            }
           
            // images upload
            string profilePicturePath = null;

            if (model.ProfilePicture != null)
            {
                // Define custom upload directory in project root
                string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "AllImages");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                // Generate a unique file name
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfilePicture.FileName);
                profilePicturePath = Path.Combine("AllImages/", fileName); // relative path
                string fullPath = Path.Combine(uploadDir, fileName); // full path

                // Save the file
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.ProfilePicture.CopyToAsync(fileStream);
                }
            }



            var users = new Users
            {
                Id = Guid.NewGuid(),
                UserName = model.UserName,
                Email = model.Email,
                Password = HashPassword(model.Password),
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                City = model.City,
                PostalCode = model.PostalCode,
                Gender = model.Gender,
                Role = model.Role,
                ProfilePicture = profilePicturePath,
                Created = DateTime.Now,
                Updated = DateTime.Now,

            };

            _dbContext.users.Add(users);
            await _dbContext.SaveChangesAsync();
            return Ok(new { message = "Registration Successful" });
        }

        private string HashPassword(string? password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

       

        private async Task<bool> EmailExist(string? email)
        {
            return await _dbContext.users.AnyAsync(users => users.Email == email);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] Login model)
        {

            if (!ModelState.IsValid) {

                BadRequest(new { error = new { massage = "Invalid data provided" } });

            }
            
            var user = await _dbContext.users.SingleOrDefaultAsync(userdata => userdata.Email == model.Email);
            if (user == null) {

                return BadRequest(new { error = new { massage="Invalid UserName" } });

            }
            if(!VarifyPassword(model.Password, user.Password))
            {

                return BadRequest(new { error = new { massage = "Invalid Password" } });

            }
            return Ok(new { massage = "Login Successfully", username = user.UserName, role = user.Role, email = user.Email, profilepic = user.ProfilePicture });

        }

        private bool VarifyPassword(string password1, string? password2)
        {
            return BCrypt.Net.BCrypt.Verify(password1, password2);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var allusers = await _dbContext.users.Where(all => all.Role == "Users").ToListAsync();
            if(GetAllUsers == null)
            {
                return NotFound(new { massage = "User Not Fount" });

            }
            var userwithprofile = allusers.Select(users => new
            {
                users.Id,
                users.UserName,
                users.Address,
                users.City,
                users.Email,
                users.Gender,
                users.PhoneNumber,
                users.ProfilePicture,
                users.Created,


            }).ToList();

            return Ok(userwithprofile);

        }



        // delete data 
        [HttpDelete("deleteproduvt/ {id}")]

        public async Task<IActionResult> deletedata(Guid id)
        {

            var users = await _dbContext.users.FindAsync(id);
            if (users == null)
            {

                return BadRequest(new { massage = "Invalid data parovided" });

            }

            _dbContext.users.Remove(users);
            await _dbContext.SaveChangesAsync();
            return Ok(new { massage = "User delete Successfully" });
        }




        [HttpPut("UpdateProfile/{id}")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromForm] UpadateUserModel model)
        {

            string ProductPicturePath = null;

            // Handle image upload if provided
            if (model.ProfilePicture != null)
            {
                try
                {
                    // Define the upload directory
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "AllImages");
                    Directory.CreateDirectory(uploadDir); // Ensure the directory exists

                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfilePicture.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ProfilePicture.CopyToAsync(stream);
                    }

                    // Save the relative path for storing in the database
                    ProductPicturePath = Path.Combine("AllImages/", fileName);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "File upload failed", error = ex.Message });
                }
            }




            var existingUser = await _dbContext.users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound(new { message = "Data not found" });
            }
            existingUser.UserName = model.UserName;
            existingUser.Address = model.Address;
            existingUser.Email = model.Email;
            existingUser.City = model.City;
            existingUser.PhoneNumber = model.PhoneNumber;
            existingUser.PostalCode = model.PostalCode;
            existingUser.ProfilePicture = ProductPicturePath;



            _dbContext.users.Update(existingUser);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Updated successfully", existingUser });
        }





        [HttpGet("GetUserById/{UserName}")]
        public IActionResult GetUserById(string UserName)
        {
            var user = _dbContext.users.FirstOrDefault(u => u.UserName == UserName);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.ProfilePicture
            });
        }


        [HttpGet("GetAllAdmin")]
        public async Task<IActionResult> GetAllAdmin()
        {
            var allAdmins = await _dbContext.users.Where(all => all.Role == "Admin").ToListAsync();
            if (GetAllAdmin == null)
            {
                return NotFound(new { massage = "User Not Fount" });

            }
            var userwithprofile = allAdmins.Select(users => new
            {
                users.Id,
                users.UserName,
                users.Address,
                users.City,
                users.Email,
                users.Gender,
                users.PhoneNumber,
                users.ProfilePicture,
                users.Created,


            }).ToList();

            return Ok(userwithprofile);

        }

    }
}
