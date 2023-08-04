using Business.Concrete;
using Core.Utils;
using Entities.Surrogate.Request;

namespace Test
{
    public class ShoppingTest : BaseTest
    {
        private readonly ShoppingService _shoppingService;

        public ShoppingTest()
        {
            _shoppingService = new ShoppingService(_productService, _cartService, _orderService);
        }
        
        [Fact]
        public void AddToCart_Success()
        {
            CartItemRequest cartItemRequest = new()
            {
                ItemQuantity = 1,
                ProductId = 1,
               
            };

            var result = _shoppingService.AddToCart(1, cartItemRequest);

            Assert.NotNull(result);
            Assert.True(result.Status == ResultStatus.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void AddToCart_Fail()
        {
            CartItemRequest cartItemRequest = new();

            var result = _shoppingService.AddToCart(1, cartItemRequest);

            Assert.Null(result.Data);
            Assert.True(result.Status == ResultStatus.Exception);
        }

        [Fact]
        public void UpdateCart_Success()
        {
            CartItemRequest cartItemRequest = new()
            {
                ItemQuantity = 1,
                ProductId = 1,

            };

            var result = _shoppingService.UpdateCart(1, cartItemRequest);

            Assert.NotNull(result);
            Assert.True(result.Status == ResultStatus.Success);
            Assert.NotNull(result.Data);

            CartItemRequest updateCartItemRequest = new()
            {
                ItemQuantity = 0,
                ProductId = 0,

            };

            var resultUpdate = _shoppingService.UpdateCart(0, updateCartItemRequest);
            Assert.NotNull(resultUpdate);
            Assert.True(resultUpdate.Status == ResultStatus.Success);
        }

        [Fact]
        public void UpdateCart_Fail()
        {
            CartItemRequest cartItemRequest = new()
            {
                ItemQuantity = 1,
                ProductId = 1,

            };

            var result = _shoppingService.UpdateCart(1, cartItemRequest);

            Assert.NotNull(result);
            Assert.True(result.Status == ResultStatus.Success);
            Assert.NotNull(result.Data);

            CartItemRequest updateCartItemRequest = new();

            var resultUpdate = _shoppingService.UpdateCart(0, updateCartItemRequest);
            Assert.NotNull(resultUpdate);
            Assert.True(resultUpdate.Status == ResultStatus.Exception);
        }

        [Fact]
        public void CompleteOrder_Success()
        {
            CartItemRequest cartItemRequest = new()
            {
                ItemQuantity = 1,
                ProductId = 1,

            };

            var result = _shoppingService.AddToCart(1, cartItemRequest);

            Assert.NotNull(result);
            Assert.True(result.Status == ResultStatus.Success);
            Assert.NotNull(result.Data);

            var order = _shoppingService.CompleteOrder(1);
           
            Assert.NotNull(order);
            Assert.True(order.Status == ResultStatus.Success);
            Assert.NotNull(order.Data);
        }

        [Fact]
        public void CompleteOrder_Fail()
        {
            var order = _shoppingService.CompleteOrder(-1);

            Assert.NotNull(order);
            Assert.True(order.Status == ResultStatus.Exception);
        }

        [Fact]
        public void RemoveFromCart_Success()
        {
            CartItemRequest cartItemRequest = new()
            {
                ItemQuantity = 1,
                ProductId = 1,

            };

            var result = _shoppingService.AddToCart(1, cartItemRequest);

            Assert.NotNull(result);
            Assert.True(result.Status == ResultStatus.Success);
            Assert.NotNull(result.Data);

            var order = _shoppingService.RemoveFromCart(1);

            Assert.NotNull(order);
            Assert.True(order.Status == ResultStatus.Success);
        }

        [Fact]
        public void RemoveFromCart_Fail()
        {
            var order = _shoppingService.RemoveFromCart(-1);

            Assert.NotNull(order);
            Assert.True(order.Status == ResultStatus.Exception);
        }

        [Fact]
        public void ClearCart_Success()
        {
            CartItemRequest cartItemRequest = new()
            {
                ItemQuantity = 1,
                ProductId = 1,

            };

            var result = _shoppingService.AddToCart(1, cartItemRequest);

            Assert.NotNull(result);
            Assert.True(result.Status == ResultStatus.Success);
            Assert.NotNull(result.Data);

            var order = _shoppingService.ClearCart(1);

            Assert.NotNull(order);
            Assert.True(order.Status == ResultStatus.Success);
        }

        [Fact]
        public void ClearCart_Fail()
        {
            var order = _shoppingService.ClearCart(-1);

            Assert.NotNull(order);
            Assert.True(order.Status == ResultStatus.Exception);
        }
    }
}
