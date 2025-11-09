using AutoMapper;
using Ecom.API.Helpers;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class BasketController : BaseController
    {
        public BasketController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("{Id:alpha}")]
        public async Task<IActionResult> GetBasketByIdAsync(string Id)
        {
            var basket = await _unitOfWork.CustomerBaskets.GetBasketAsync(Id);
            if (basket == null) return NotFound();
            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasketAsync([FromBody] CustomerBasket basket)
        {
            var updatedBasket = await _unitOfWork.CustomerBaskets.UpdateBasketAsync(basket);
            if (updatedBasket == null) return BadRequest("Problem updating the basket");
            return Ok(updatedBasket);
        }

        [HttpDelete("{Id:alpha}")]
        public async Task<IActionResult> DeleteBasketAsync(string Id)
        {
            var result = await _unitOfWork.CustomerBaskets.DeleteBasketAsync(Id);
            if (!result) return BadRequest(new ResponseAPI(400, "Problem deleting the basket"));
            return Ok(new ResponseAPI(200, "Item deleted successfully"));
        }
    }
}
