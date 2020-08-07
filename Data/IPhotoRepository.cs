using System.Threading.Tasks;

namespace WebApp_Core.Data
{
    public interface IPhotoRepository
    {
         Task<string> AddProfilePhoto(int id, string url);
         Task<string> GetProfilePhoto(int id, string url);
    }
}