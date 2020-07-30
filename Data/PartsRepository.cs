using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp_Core.Helpers;
using WebApp_Core.Models;

namespace WebApp_Core.Data
{
    public class PartsRepository : IPartsRepository
    {
        private readonly DataContext context;
        public PartsRepository(DataContext context)
        {
            this.context = context;

        }
        public async Task<string> AddToCart(int partId, int userId)
        {
            var found = await context.PartsAndUsers.FirstOrDefaultAsync(p => p.PartId == partId && p.UserId == userId && p.wishList == false);
            var available = await context.Parts.FirstOrDefaultAsync(p => p.ID == partId);

            var cartPart = new PartAndUser();
            string toReturn = "";
            if(found == null && available != null)
            {

                cartPart.UserId = userId;
                cartPart.PartId = partId;
                cartPart.wishList = false;

            
                await context.AddAsync(cartPart);
                await context.SaveChangesAsync();
            } else if (found != null && available != null) {
                toReturn = "Item already exists in your wishlist";
            } else {
                toReturn = "Part you are trying to add could not be found";
            }

            return toReturn;
        }

        public async Task<string> AddToWishList(int partId, int userId)
        {
            var found = await context.PartsAndUsers.FirstOrDefaultAsync(p => p.PartId == partId && p.UserId == userId && p.wishList == true);
            var available = await context.Parts.FirstOrDefaultAsync(p => p.ID == partId);

            var cartPart = new PartAndUser();
            string toReturn = "";
            if(found == null && available != null)
            {

                cartPart.UserId = userId;
                cartPart.PartId = partId;
                cartPart.wishList = true;

            
                await context.AddAsync(cartPart);
                await context.SaveChangesAsync();
            } else if (found != null && available != null) {
                toReturn = "Item already exists in your wishlist";
            } else {
                toReturn = "Part you are trying to add could not be found";
            }

            return toReturn;
        }
        

        public async Task<PagedList<PartAndUser>> GetWishList(int userId, PartsParams partsParams)
        {
            var wishList = context.PartsAndUsers.Where(c => c.UserId == userId && c.wishList == true).AsQueryable();
            return await PagedList<PartAndUser>.CreateAsync(wishList, partsParams.pageNumber, partsParams.PageSize);
        }

        public async Task<PagedList<PartAndUser>> GetKart(int userId, PartsParams partsParams)
        {
            var cart = context.PartsAndUsers.Where(c => c.UserId == userId && c.wishList == false).AsQueryable();
            return await PagedList<PartAndUser>.CreateAsync(cart, partsParams.pageNumber, partsParams.PageSize);
        }

        public async Task<Part> GetPart(int id)
        {
            var part = await context.Parts.FirstOrDefaultAsync(p => p.ID == id);
            
            if(part == null)
                return null;

            return part;
        }

        public async Task<PagedList<Part>> GetParts(PartsParams partsParams)
        {
            var queryableList = context.Parts.AsQueryable();

            return await PagedList<Part>.CreateAsync(queryableList, partsParams.pageNumber, partsParams.PageSize);
        }
        public async Task<bool> DeletePart(bool wishlist, int userId, int partId)
        {
            var part = await context.PartsAndUsers.FirstOrDefaultAsync(p => p.PartId == partId && p.UserId == userId && p.wishList == wishlist);
            bool foundPart = part != null;
            if(foundPart)
            {
                context.Remove(part);
                await context.SaveChangesAsync();
            }
            return foundPart;
        }
    }
}