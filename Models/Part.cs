using System;
using System.Collections.Generic;

namespace WebApp_Core.Models
{
    public class Part
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int Count {get; set;}
        public int Amount {get; set;} = 1;
        public float Price { get; set; }

        public DateTime DateAdded { get; set; }
        public string PhotoUrl { get; set; }

        public bool wishList {get; set;}

    }
}