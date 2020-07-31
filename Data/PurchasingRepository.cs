using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetPractice.Dto;
using Microsoft.EntityFrameworkCore;
using WebApp_Core.Data;
using WebApp_Core.Models;

namespace DotnetPractice.Data
{
    public class PurchasingRepository : IPurchasing
    {
        private readonly DataContext context;
        public PurchasingRepository(DataContext context)
        {
            this.context = context;

        }
        public async Task<float> AddMoney(int amount, int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.ID == id);
            if(user == null)
                return 0;

            user.CashAmount += amount;
            await context.SaveChangesAsync();
            return user.CashAmount;
        }

        public async Task<PurchasingToReturnDto> PurchaseParts(int id)
        {
            var cart = await context.PartsAndUsers.Where(p => p.UserId == id && p.wishList == false && p.purchased == false).ToListAsync();
            var user = await context.Users.FirstOrDefaultAsync(u => u.ID == id);
            PurchasingToReturnDto response = new PurchasingToReturnDto();
            if(cart.Count <= 0)
            {
                response.purchasedSuccessfully = false;
                response.ResponseDataString = "No items to be purchased";
                return response;
            }

            float amountToBePaid = 0;
            foreach (var item in cart)
            {
                amountToBePaid += (item.Amount * item.Part.Price);
            }

            if(amountToBePaid <= user.CashAmount)
            {
                user.CashAmount -= amountToBePaid;
                response.Purchased = new List<Part>();
                foreach (var item in cart)
                {
                    item.Part.Amount = item.Amount;
                    response.Purchased.Add(item.Part);
                    item.purchased = true;
                }
            response.AmountRemaining = user.CashAmount;
            response.AmountPaid = amountToBePaid;
            response.purchasedSuccessfully = true;
            await context.SaveChangesAsync();
            return response;
            } else {
                response.purchasedSuccessfully = false;
                response.ResponseDataString = "You do not have enough Credits: Amount to be paid: " + amountToBePaid + " and You have " + user.CashAmount;
                return response;
            }
        }
    }
}