using System.Threading.Tasks;
using WebApp_Core.Helpers;
using WebApp_Core.Models;

namespace WebApp_Core.Data
{
    public interface IPartsRepository
    {
         Task<PagedList<Part>> GetParts(PartsParams partsParams);
         Task<Part> GetPart(int id);
         Task<Part> UpdatePartCount(int id, int amount);
         Task<PagedList<PartAndUser>> GetWishList(int userId,PartsParams partsParams);
         Task<PagedList<PartAndUser>> GetKart(int userId,PartsParams partsParams);
         Task<PartAndUser> GetKartItem(int userId, int kartItemID);
         Task<PartAndUser> UpdateKartItemAmount(int userId, int kartItemID, int amount);
         Task<bool> DeletePart(bool wishlist, int userId, int partId);
         Task<string> AddToCart(int partId, int userId);
         Task<string> AddToWishList(int partId, int userId);
    }
}