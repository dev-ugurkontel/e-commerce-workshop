﻿using Business.Abstract;
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
        public IActionResult AddToCart(int userId, CartItemRequest cartItemRequest)
        {
            var result = _shoppingService.AddToCart(userId, cartItemRequest);
            return Ok(result);
        }
        [Route("UpdateToCart")]
        [HttpPut]
        public IActionResult UpdateToCart(int cartId, CartItemRequest cartItemRequest)
        {
            var result = _shoppingService.UpdateCart(cartId, cartItemRequest);  
          
            return Ok(result);
        }

        [Route("RemoveFromCart")]
        [HttpDelete]
        public IActionResult RemoveFromCart(int cartItemRequest)
        {
            var result = _shoppingService.RemoveFromCart(cartItemRequest);
            return Ok(result);
        }
        [Route("ClearCart")]
        [HttpDelete]
        public IActionResult ClearCart(int cartId)
        {
            var result =_shoppingService.ClearCart(cartId);
            return Ok(result);  
        }
    }
}
