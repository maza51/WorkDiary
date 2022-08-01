using System;
using System.ComponentModel.DataAnnotations;

namespace WorkDiary.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter your name")]
        [MaxLength(20, ErrorMessage = "Name must not exceed 15 characters")]
        [MinLength(5, ErrorMessage = "Name must be more than 5 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

