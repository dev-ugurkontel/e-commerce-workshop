using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Utils;
using Core.Validation;
using Core.Utils.Results;
using Entities.Entity;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;

namespace Business.Concrete
{
    public class ShoppingService : IShoppingService
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public ShoppingService(IProductService productService, ICartService cartService, IOrderService orderService)
        {
            _productService = productService;
            _cartService = cartService;
            _orderService = orderService;
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
                //ProductImagePaths = product.ProductImagePaths,
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

        public IDataResult<CartResponse> UpdateCart(int userId, CartItemRequest cartItemRequest)
        {
            //TODO: Adet güncelleme işlemleri yapılmalıdır.
            return new SuccessDataResult<CartResponse>(default, "Sepet güncellendi (henüz tamamlanmadı).");
        }

        public IDataResult<OrderResponse> CompleteOrder(int cartId)
        {
            var cart = _cartService.Get(cartId);
            if (cart is null)
            {
                return new ErrorDataResult<OrderResponse>(default, "Sepet bulunamadı.");
            }

            if (cart.Data.CartItems.Count == 0)
            {
                return new ErrorDataResult<OrderResponse>(default, "Sepetinizde ürün bulunmamaktadır.");
            }

            OrderRequest orderRequest = new()
            {
                CartId = cartId,
                OrderItems = new()
            };

            foreach (var cartItem in cart.Data.CartItems)
            {
                var product = _productService.Get(cartItem.ProductId).Data;
                if (product is null)
                {
                    return new ErrorDataResult<OrderResponse>(default, "Ürün bulunamadı.");
                }

                double totalPrice = product.ProductPrice * cartItem.ItemQuantity;
                double discountPrice = totalPrice - totalPrice * product.Campaign.CampaignDiscountRate / 100;

                var orderItemRequest = new OrderItemRequest()
                {
                    ItemQuantity = cartItem.ItemQuantity,
                    ItemPrice = product.ProductPrice * cartItem.ItemQuantity,
                    DiscountRate = product.Campaign.CampaignDiscountRate,
                    DiscountPrice = discountPrice
                };

                orderRequest.OrderItems.Add(orderItemRequest);
            }

            var orderResult = _orderService.CompleteOrder(orderRequest);
            if (orderResult.Status == 0)
            {
                return new ErrorDataResult<OrderResponse>(default, "Sipariş oluşturulurken hata oluştu.");
            }

            ClearCart(cartId);

            return new SuccessDataResult<OrderResponse>(default, "Sipariş tamamlandı.");
        }     


        public IDataResult<CartResponse> GetCart(int cartId)
        {
            var cart = _cartService.Get(cartId).Data;
            if (cart is null)
            {
                return new ErrorDataResult<CartResponse>(default, "Sepet bulunamadı.");
            }

            if (cart.CartItems.Count == 0)
            {
                return new ErrorDataResult<CartResponse>(default, "Sepetinizde ürün bulunmamaktadır.");
            }
            return new SuccessDataResult<CartResponse>(cart, "Sepet bilgisi getirildi.");
        }

        public IResult RemoveFromCart(int cartItemId)
        {
            var cartItem = _cartService.GetCartItemByCartItemId(cartItemId);
            if (cartItem is null)
            {
                return new ErrorDataResult<CartResponse>(default, "Sepet detayı bulunamadı.");
            }

            var cart = _cartService.Get(cartItem.Data.CartId).Data;
            if (cart is null)
            {
                return new ErrorDataResult<CartResponse>(default, "Sepet bulunamadı.");
            }

            var product = _productService.Get(cartItem.Data.ProductId).Data;
            if (product is null)
            {
                return new ErrorDataResult<CartResponse>(default, "Ürün bulunamadı.");
            }

            CartRequest cartRequest = new()
            {
                UserId = cart.UserId,
                TotalItemQuantity = cart.TotalItemQuantity -= cartItem.Data.ItemQuantity,
                TotalItemPrice = cart.TotalItemPrice -= (product.ProductPrice * cartItem.Data.ItemQuantity)
            };

            var cartResult = _cartService.Update(cart.CartId, cartRequest);
            if (cartResult.Status == 0)
            {
                return new ErrorDataResult<CartResponse>(default, "Sepet güncellenemedi.");
            }

            ProductRequest productRequest = new()
            {
                ProductStock = product.ProductStock += cartItem.Data.ItemQuantity,
                ProductCategoryId = product.Category.CategoryId,
                ProductCampaignId = product.Campaign.CampaignId,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                ProductStatus = product.ProductStatus,
                //ProductImagePaths = product.ProductImagePaths,
                ProductName = product.ProductName
            };

            _productService.Update(product.ProductId, productRequest);

            _cartService.DeleteCartItem(cartItemId);

            if (cart.CartItems.Count == 1)
            {
                _cartService.Delete(cart.CartId);
            }

            return new SuccessResult("Ürün sepetten silindi.");
        }

        public IResult ClearCart(int cartId)
        {
            var cart = _cartService.Get(cartId);
            if (cart is null)
            {
                return new ErrorDataResult<CartResponse>(default, "Sepet bulunamadı.");
            }

            if (cart.Data.CartItems.Count > 0)
            {
                foreach (var cartItem in cart.Data.CartItems)
                {
                    var product = _productService.Get(cartItem.ProductId).Data;
                    if (product is null)
                    {
                        return new ErrorDataResult<CartResponse>(default, "Ürün bulunamadı.");
                    }

                    ProductRequest productRequest = new()
                    {
                        ProductStock = product.ProductStock += cartItem.ItemQuantity,
                        ProductCategoryId = product.Category.CategoryId,
                        ProductCampaignId = product.Campaign.CampaignId,
                        ProductDescription = product.ProductDescription,
                        ProductPrice = product.ProductPrice,
                        ProductStatus = product.ProductStatus,
                        //ProductImagePaths = product.ProductImagePaths,
                        ProductName = product.ProductName
                    };

                    _productService.Update(product.ProductId, productRequest);
                    _cartService.DeleteCartItem(cartItem.CartItemId);
                }
            }

            _cartService.Delete(cartId);

            return new SuccessResult("Sepet temizlendi.");
        }
    }
}
