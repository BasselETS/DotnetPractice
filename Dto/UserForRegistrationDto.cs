using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp_Core.Dto
{
    public class UserForRegistrationDto
    {
        public int ID { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        public string Username { get; set; }    
        [Required]
        [StringLength(10)]
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}