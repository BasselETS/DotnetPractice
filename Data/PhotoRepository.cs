using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApp_Core.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext context;
        public PhotoRepository(DataContext context)
        {
            this.context = context;

        }
        public async Task<string> AddProfilePhoto(int id, string url)
        {
            var User = await context.Users.FirstOrDefaultAsync(u => u.ID == id);
            User.PhotoUrl = url;
            await context.SaveChangesAsync();
            return User.PhotoUrl;
        }
    
        public async Task<string> GetProfilePhoto(int id, string url)
        {
            var User = await context.Users.FirstOrDefaultAsync(u => u.ID == id); 
            return User.PhotoUrl;
        }
    }
}