using System;
using System.Collections.Generic;

namespace WebApp_Core.Models
{
    public class User
    {
        public int ID { get; set; }

        public float CashAmount { get; set; }
        public string Email {get; set;}
        public string Username { get; set; }    
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhotoUrl { get; set; }

        public bool OTPAuth { get; set; }
        public string OtpToCheckWith { get; set; }
        public virtual IEnumerable<PartAndUser> Parts { get; set; }

        public User()
        {
            this.DateCreated = DateTime.Now;    
            Random random = new Random();
            int otpNumber = random.Next(0,999999);
            this.OtpToCheckWith = otpNumber.ToString().PadLeft(6);
        }
        
    }
}