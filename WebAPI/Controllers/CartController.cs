using Business.Abstract;
using Core.Utils;
using Entities.Surrogate.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v1/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Route("List_Cart")]
        public IActionResult List_Cart()
        {
            var result = _cartService.GetAll();
            if (result.Status == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Route("Save_Cart")]
        [HttpPost]
        public IActionResult Save_Cart(CartRequest cart)
        {
            _cartService.Add(cart);
            return Ok(cart);
        }

        [Route("Find_Cart/{id}")]
        [HttpGet]
        public IActionResult Find_Cart(int id)
        {
            var cart = _cartService.Get(id);
            if (cart.Status == ResultStatus.Error)
            {
                return NotFound();
            }
            else if (cart.Status == ResultStatus.Success)
            {
                return Ok(cart);
            }
            else
            {
                return BadRequest();
            }

        }

        [Route("Update_Cart/{id}")]
        [HttpPut]
        public IActionResult Update_Cart(int id, CartRequest cart)
        {
            _cartService.Update(id, cart);
            return Ok(cart);
        }

        [Route("Delete_Cart/{id}")]
        [HttpDelete]
        public IActionResult Delete_Cart(int id)
        {
            _cartService.Delete(id);
            return NoContent();
        }
    }
}
