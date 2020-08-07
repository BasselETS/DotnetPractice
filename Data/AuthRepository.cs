using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp_Core.Models;

namespace WebApp_Core.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        public AuthRepository(DataContext context)
        {
            this.context = context;

        }
        public async Task<User> Login(string username, string password)
        {
            User user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if(user == null)
                return null;
                
            if(!VerifyPasswordHash(password,user))
                return null;

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordSalt, passwordHash;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await context.Users.AnyAsync(u => u.Username == username))
                return true;

            return false;
        }

        private bool VerifyPasswordHash(string password, User user)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt))
            {
                var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for( int i = 0; i < ComputeHash.Length; i++)
                {
                    if(ComputeHash[i] != user.PasswordHash[i])
                    return false;
                }
            }
            return true;
        }

        private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
           
        }

        public async Task<bool> CheckOTP(int id, string otp)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.ID == id);
            bool success = false;
            if(user.OtpToCheckWith == otp)
                success = true;
            user.OTPAuth = success;
            if(success)
                await context.SaveChangesAsync();

            return success;
        }

        public async Task<string> SavePhoto(int id, string url)
        {
            var User = await context.Users.FirstOrDefaultAsync(u => u.ID == id);
            User.PhotoUrl = url;
            await context.SaveChangesAsync();
            return url;
        }
    }
}