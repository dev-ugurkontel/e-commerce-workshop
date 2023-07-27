using Business.Abstract;
using Core.Utils;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Concrete
{
    public class ShoppingService : IShoppingService
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public ShoppingService(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public IDataResult<CartResponse> AddToCart(int userId, CartItemRequest cartItemRequest)
        {
            var product = _productService.Get(cartItemRequest.ProductId).Data;

            CartRequest cartRequest = new()
            {
                UserId = userId,
                TotalItemQuantity = cartItemRequest.ItemQuantity,
                TotalItemPrice = cartItemRequest.ItemQuantity * product.ProductPrice,
                CartItems = new List<CartItemRequest>() { cartItemRequest }
            };

            var cartResult = _cartService.Add(cartRequest);
            if (cartResult.Status == 0)
            {
                return new ErrorDataResult<CartResponse>(default, "Sepet oluşturulamadı.");
            }

            ProductRequest productRequest = new()
            {
                ProductStock = product.ProductStock -= cartItemRequest.ItemQuantity,
                ProductCategoryId = product.Category.CategoryId,
                ProductCampaignId = product.Campaign.CampaignId,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                ProductStatus = product.ProductStatus,
                ProductImagePath = product.ProductImagePath,
                ProductName = product.ProductName
            };

            _productService.Update(product.ProductId, productRequest);

            var cart = _cartService.Get(cartResult.Data.CartId).Data;
            var cartItems = _cartService.GetCartItemsByCartId(cartResult.Data.CartId).Data;

            CartResponse cartResponse = new()
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                TotalItemQuantity = cart.TotalItemQuantity,
                TotalItemPrice = cart.TotalItemPrice,
                CreateDate = cart.CreateDate,
                EditDate = cart.EditDate,
                CartItems = cartItems
            };

            return new SuccessDataResult<CartResponse>(cartResponse, "Ürün sepete eklendi.");
        }

        public IDataResult<CartResponse> UpdateCart(int userId, CartItemRequest cartItemRequest)
        {
            throw new NotImplementedException();
        }

        public IResult RemoveFromCart(int cartItemId)
        {
            _cartService.DeleteCartItem(cartItemId);
            return new SuccessResult("Ürün sepetten silindi.");
        }

        public IResult ClearCart(int cartId)
        {
            throw new NotImplementedException();
        }
    }
}
