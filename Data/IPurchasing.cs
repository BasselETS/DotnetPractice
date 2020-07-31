using System.Threading.Tasks;
using DotnetPractice.Dto;
using WebApp_Core.Models;

namespace DotnetPractice.Data
{
    public interface IPurchasing
    {
         Task<PurchasingToReturnDto> PurchaseParts(int id);
         Task<float> AddMoney(int amount, int id);
    }
}