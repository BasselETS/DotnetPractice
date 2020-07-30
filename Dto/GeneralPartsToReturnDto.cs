using System;

namespace WebApp_Core.Dto
{
    public class GeneralPartsToReturnDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int Count {get; set;}
        public float Price { get; set; }

        public DateTime DateAdded { get; set; }
        public string PhotoUrl { get; set; }
    }
}