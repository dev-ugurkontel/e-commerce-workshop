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

        [Route("CompleteOrder")]
        [HttpPost]
        public IActionResult CompleteOrder(int cartId)
        {
            var result = _shoppingService.CompleteOrder(cartId);
            return Ok(result);
        }

        [Route("AddToCart")]
        [HttpPost]
        public IActionResult AddToCart(int userId, [FromBody] CartItemRequest cartItemRequest)
        {
            var result = _shoppingService.AddToCart(userId, cartItemRequest);
            return Ok(result);
        }

        [Route("GetCart")]
        [HttpGet]
        public IActionResult GetCart(int cartId)
        {
            var result = _shoppingService.GetCart(cartId);
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
