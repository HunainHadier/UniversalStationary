﻿using System.ComponentModel.DataAnnotations;
using UniversalStationary.Vadlidations;
namespace UniversalStationary.Models
{
    public class UserViewModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [PasswordValidation]
        public string? Password { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? City { get; set; }

       
        public string? PostalCode { get; set; }

  
        public string? Role { get; set; }

        public IFormFile? ProfilePicture { get; set; }

        [Required]
        public string? Gender { get; set; }

        public DateTime? Created { get; set; } = DateTime.Now;

        public DateTime? Updated { get; set; } = DateTime.Now;

    }
}
