using System;

namespace WebApp_Core.Dto
{
    public class UserForReturnDto
    {
        
        public int ID { get; set; }
        public string Username { get; set; }   
        public float CashAmount { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}