using Microsoft.EntityFrameworkCore;
using WebApp_Core.Models;

namespace WebApp_Core.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
         protected override void OnModelCreating(ModelBuilder builder)
         {
             
         }
        public DbSet<User> Users {get;set;}
        public DbSet<Part> Parts {get;set;}
        public DbSet<PartAndUser> PartsAndUsers{get;set;}
    }
}