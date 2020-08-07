using System.Threading.Tasks;
using WebApp_Core.Models;

namespace WebApp_Core.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
         Task<bool> CheckOTP(int id, string otp);
         Task<string> SavePhoto(int id, string url);
    }
}