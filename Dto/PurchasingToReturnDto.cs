using System.Collections.Generic;
using WebApp_Core.Models;

namespace DotnetPractice.Dto
{
    public class PurchasingToReturnDto
    {
        public List<Part> Purchased { get; set; }
        public float AmountRemaining {get; set;}
        public float AmountPaid {get; set;}
        public bool purchasedSuccessfully {get; set;}
        public string ResponseDataString {get; set;}
    }
}