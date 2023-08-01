using Business.Abstract;
using Core.Utils;
using DataAccess.EF.Concrete;
using Entities.Entity;
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

            CartResponse cartResponse = new()
            {
                CartId = cartResult.Data.CartId,
                UserId = cartResult.Data.UserId,
                TotalItemQuantity = cartResult.Data.TotalItemQuantity,
                TotalItemPrice = cartResult.Data.TotalItemPrice,
                CreateDate = cartResult.Data.CreateDate,
                EditDate = cartResult.Data.EditDate,
                CartItems = cartResult.Data.CartItems
            };

            return new SuccessDataResult<CartResponse>(cartResponse, "Ürün sepete eklendi.");
        }

        public IDataResult<CartResponse> UpdateCart(int cartId, CartItemRequest cartItemRequest)
        {
            var product = _productService.Get(cartItemRequest.ProductId).Data;

            var cartResult = _cartService.Get(cartId);

            if (cartResult.Status == 0 || cartResult.Data == null || cartResult.Data.CartId != cartId)
            {
                return new ErrorDataResult<CartResponse>(default, "Sepet Bulunamadı.");
            }
            var cartUpdate = cartResult.Data;

            var itemUpdate = cartUpdate.CartItems.FirstOrDefault(item => item.ProductId == cartItemRequest.ProductId);



            if (itemUpdate == null)
            {
                return new ErrorDataResult<CartResponse>(default, "Ürün bulunamadı.");
            }
            itemUpdate.ItemQuantity = cartItemRequest.ItemQuantity;


            cartUpdate.TotalItemQuantity = cartUpdate.CartItems.Sum(item => item.ItemQuantity);

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

            CartResponse cartResponse = new()
            {
                CartId = cartUpdate.CartId,
                UserId = cartUpdate.UserId,
                TotalItemQuantity = cartUpdate.TotalItemQuantity,
                TotalItemPrice = cartUpdate.TotalItemPrice,
                CreateDate = cartUpdate.CreateDate,
                EditDate = cartUpdate.EditDate,
                CartItems = cartUpdate.CartItems
            };

            return new SuccessDataResult<CartResponse>(cartResponse, "Ürün sepetinizde güncellendi.");
        }


        public IResult RemoveFromCart(int cartItemId, CartItemRequest cartItemRequest)
        {
            var product = _productService.Get(cartItemRequest.ProductId).Data;
            _cartService.DeleteCartItem(cartItemId);

            ProductRequest productRequest = new()
            {
                ProductStock = product.ProductStock += cartItemRequest.ItemQuantity,
                ProductCategoryId = product.Category.CategoryId,
                ProductCampaignId = product.Campaign.CampaignId,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                ProductStatus = product.ProductStatus,
                ProductImagePath = product.ProductImagePath,
                ProductName = product.ProductName
            };
            _productService.Update(product.ProductId, productRequest);

            return new SuccessResult("Ürün sepetten silindi.");
        }

        public IResult ClearCart(int cartId)
        {
            _cartService.DeleteCartItem(cartId);
            return new SuccessResult("Cart silindi.");
        }


    }
}