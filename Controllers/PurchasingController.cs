using System.Security.Claims;
using System.Threading.Tasks;
using DotnetPractice.Data;
using DotnetPractice.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetPractice.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PurchasingController : ControllerBase
    {
        private readonly IPurchasing repo;
        public PurchasingController(IPurchasing repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> AddMoney([FromBody]AddMoneyDto moneyData)
        {
             if (moneyData.UserID != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
                
            var result = await repo.AddMoney(moneyData.CashAmount, moneyData.UserID);
            if(result == 0)
                return BadRequest("Could not add money to your wallet. Try again later");
            
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PurchaseCart(int id)
        {
            var result = await repo.PurchaseParts(id);
            if(!result.purchasedSuccessfully)
                return BadRequest(result.ResponseDataString);
                 return Ok(result);
        }
    }
}