using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp_Core.Data;
using WebApp_Core.Dto;
using WebApp_Core.Models;
using WebApp_Core.Helpers;
using DotnetPractice.Dto;

namespace WebApp_Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartsController : ControllerBase
    {
        private readonly IPartsRepository repo;
        private readonly IMapper mapper;
        public PartsController(IPartsRepository repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> GetParts([FromQuery] PartsParams partsParams)
        {
            var parts = await repo.GetParts(partsParams);
            List<GeneralPartsToReturnDto> partsForReturn = new List<GeneralPartsToReturnDto>();
            foreach (var part in parts)
            {
                GeneralPartsToReturnDto partToReturn = mapper.Map<GeneralPartsToReturnDto>(part);
                partsForReturn.Add(partToReturn);
            }
            
            Response.AddPaginationsHeaders(parts.CurrentPage, parts.PageSize, parts.TotalCount, parts.TotalPages);
            return Ok(partsForReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPart(int id)
        {
            Part part = await repo.GetPart(id);
            if (part == null)
                return BadRequest("We couldnt find your request");

            PartForReturnDto partForReturn = mapper.Map<PartForReturnDto>(part);
            return Ok(partForReturn);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdatePart(UpdatePartCountDto updatePartDto)
        {
            Part part = await repo.UpdatePartCount(updatePartDto.ID, updatePartDto.Count);
            if (part == null)
                return BadRequest("We couldnt find your request");

            GeneralPartsToReturnDto partForReturn = mapper.Map<GeneralPartsToReturnDto>(part);
            return Ok(partForReturn);
        }

        [Authorize]
        [HttpGet("{id}/kart", Name = "GetKart")]
        public async Task<IActionResult> GetKart(int id, [FromQuery] PartsParams partsParams)
        {

            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var cart = await repo.GetKart(id, partsParams);
            if (cart == null)
                return BadRequest("Unable to find cart values");


            List<PartForReturnDto> cartList = new List<PartForReturnDto>();

            foreach (var part in cart)
            {
                var partForRet = mapper.Map<PartForReturnDto>(part.Part);
                partForRet.Amount = part.Amount;
                cartList.Add(partForRet);
            }
            Response.AddPaginationsHeaders(cart.CurrentPage, cart.PageSize, cart.TotalCount, cart.TotalPages);
            return Ok(cartList);
        }

        [Authorize]
        [HttpGet("{id}/kart/{partId}")]
        public async Task<IActionResult> GetKartItem(int id, int partId)
        {
            var item = await repo.GetKartItem(id, partId);
            if(item == null)
                return BadRequest("Item is not in your kart");
            var toReturn = mapper.Map<PartForReturnDto>(item.Part);
            toReturn.Amount = item.Amount;
            return Ok(toReturn);
        }
        
        [Authorize]
        [HttpPost("kart/update")]
        public async Task<IActionResult> UpdateKartItem([FromBody]KartUpdateAmountDto body)
        {
            var item = await repo.UpdateKartItemAmount(body.UserId, body.PartId,body.Amount);
            if(item == null)
                return BadRequest("Item is not in your kart");
            if(item.Amount < 0)
                return BadRequest("There is only " + item.Part.Count + " " + item.Part.Name + " Remaining and you are requesting " + body.Amount );

            var toReturn = mapper.Map<PartForReturnDto>(item.Part);
            toReturn.Amount = item.Amount;
            return Ok(toReturn);
        }

        [Authorize]
        [HttpPost("{id}/kart/{partId}")]
        public async Task<IActionResult> AddToCart(int id, int partId)
        {
            var cartToAdd = await repo.AddToCart(partId, id);

            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            if (cartToAdd != "")
                return BadRequest(cartToAdd);

            return CreatedAtRoute("GetKart",new {id = id, partParams = new PartsParams()}, cartToAdd);
        }

        [HttpDelete("{userId}/kart/{partId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int partId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            bool removed = await repo.DeletePart(false, userId, partId);

            if (!removed)
                return BadRequest("Failed to find the item in order to delete it");
                

            return CreatedAtRoute("GetKart", new { id = userId, partsParams = new PartsParams() }, "Deleted Successfully");
        }

        [Authorize]
        [HttpGet("{id}/wishlist", Name = "GetWishList")]
        public async Task<IActionResult> GetWishList(int id, [FromQuery]PartsParams partsParams)
        {

            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var wishlist = await repo.GetWishList(id, partsParams);

            if (wishlist == null)
                return BadRequest("Unable to find WishList values");

            List<PartForReturnDto> wishListParts = new List<PartForReturnDto>();

            foreach (var part in wishlist)
            {
                var partForRet = mapper.Map<PartForReturnDto>(part.Part);
                wishListParts.Add(partForRet);
            }
            Response.AddPaginationsHeaders(wishlist.CurrentPage, wishlist.PageSize, wishlist.TotalCount, wishlist.TotalPages);
            return Ok(wishListParts);
        }

        [Authorize]
        [HttpPost("{id}/wishlist/{partId}")]
        public async Task<IActionResult> AddToWishList(int id, int partId)
        {

            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var wishlistToAdd = await repo.AddToWishList(partId, id);

            if (wishlistToAdd != "")
                return BadRequest(wishlistToAdd);

            return CreatedAtRoute("GetWishList", new { id = id, partsParams = new PartsParams() }, wishlistToAdd);
        }

        [Authorize]
        [HttpDelete("{userId}/wishlist/{partId}")]
        public async Task<IActionResult> RemoveFromWishList(int userId, int partId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            bool removed = await repo.DeletePart(true, userId, partId);

            if (!removed)
                return BadRequest("Failed to find the item in order to delete it");


            return CreatedAtRoute("GetWishList", new { id = userId, partsParams = new PartsParams() }, "Deleted Successfully");
        }

    }
}