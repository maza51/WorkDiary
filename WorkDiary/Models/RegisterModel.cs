using System;
using System.ComponentModel.DataAnnotations;

namespace WorkDiary.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Enter your name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter your confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}

