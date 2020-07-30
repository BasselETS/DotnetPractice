using System;

namespace WebApp_Core.Dto
{
    public class UserForRegistrationDto
    {
        public int ID { get; set; }
        public string Username { get; set; }    
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}