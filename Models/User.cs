using System;
using System.Collections.Generic;

namespace WebApp_Core.Models
{
    public class User
    {
        public int ID { get; set; }

        public float CashAmount { get; set; }
        public string Username { get; set; }    
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhotoUrl { get; set; }
        public virtual IEnumerable<PartAndUser> Parts { get; set; }

        public User()
        {
            this.DateCreated = DateTime.Now;
        }
        
    }
}