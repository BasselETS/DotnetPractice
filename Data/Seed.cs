using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WebApp_Core.Models;

namespace WebApp_Core.Data
{
    public class Seed
    {
        public static void SeedParts(DataContext context)
        {/*
                var parts = new List<Part>();

                for(int i = 0; i< 100; i++)
                {
                    Part part = new Part();
                    part.Price = new Random().Next(100,500);
                    part.Name = "Part Number " + (i+1);
                    part.PhotoUrl = "";
                    part.Count = new Random().Next(10,45);
                    part.wishList = false;
                    part.DateAdded = DateTime.Now;
                    parts.Add(part);
                    
                }
                context.Parts.AddRange(parts);
                context.SaveChanges();*/
        }
    }
}