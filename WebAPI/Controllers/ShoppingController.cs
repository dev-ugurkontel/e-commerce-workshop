using Business.Abstract;
using Entities.Surrogate.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v1/shopping")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly IShoppingService _shoppingService;

        public ShoppingController(IShoppingService shoppingService)
        {
            _shoppingService = shoppingService;
        }

        [Route("AddToCart")]
        [HttpPost]
        public IActionResult AddToCart(int userId, CartItemRequest cartItemId)
        {
            var result = _shoppingService.AddToCart(userId, cartItemId);
            return Ok(result);
        }

        [Route("RemoveFromCart")]
        [HttpDelete]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            var result = _shoppingService.RemoveFromCart(cartItemId);
            return Ok(result);
        }

        [Route("ClearCart")]
        [HttpDelete]
        public IActionResult ClearCart(int cartId)
        {
            var result = _shoppingService.ClearCart(cartId);
            return Ok(result);
        }
    }
}
