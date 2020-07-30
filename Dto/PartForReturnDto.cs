using System;

namespace WebApp_Core.Dto
{
    public class PartForReturnDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public DateTime DateAdded { get; set; }
        public string PhotoUrl { get; set; }

    }
}