using Business.Abstract;
using Core.Utils;
using DataAccess.EF.Abstract;
using DataAccess.EF.Concrete;
using Entities.Entity;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;

namespace Business.Concrete
{
    public class CartService : ICartService
    {
        private readonly CartRepositoryBase _cartRepository;
        private readonly CartItemRepositoryBase _cartItemRepository;

        public CartService(CartRepositoryBase cartRepository, CartItemRepositoryBase cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public IDataResult<CartResponse> Add(CartRequest data)
        {
            if (data is null)
            {
                return new ErrorDataResult<CartResponse>(default, "Sepet bilgileri boş olamaz.");
            }

            if (data.CartItems.Count == 0)
            {
                return new ErrorDataResult<CartResponse>(default, "Sepet detayları boş olamaz.");
            }

            var cart = _cartRepository.Get(c => c.UserId == data.UserId);
            if (cart is null)
            {
                Cart newCart = new()
                {
                    UserId = data.UserId,
                    TotalItemQuantity = data.TotalItemQuantity,
                    TotalItemPrice = data.TotalItemPrice,
                    CreateDate = DateTime.Now,
                    EditDate = DateTime.Now
                };

                var cartResult = _cartRepository.Add(newCart);
                if (cartResult.CartId == 0)
                {
                    return new ErrorDataResult<CartResponse>(default, "Sepet oluşturulamadı.");
                }

                cart = new();
                cart.CartId = cartResult.CartId;
                cart.UserId = cartResult.UserId;
            }
            else
            {
                cart.TotalItemQuantity = cart.TotalItemQuantity += data.TotalItemQuantity;
                cart.TotalItemPrice = cart.TotalItemPrice += data.TotalItemPrice;
                cart.EditDate = DateTime.Now;

                _cartRepository.Update(cart);
            }            

            List<CartItem> addCartItems = new();
            List<CartItem> updateCartItems = new();

            foreach (var item in data.CartItems)
            {
                var cartItem = _cartItemRepository.Get(c => c.ProductId == item.ProductId);
                if (cartItem is null)
                {
                    CartItem newCartItem = new()
                    {
                        CartId = cart.CartId,
                        ProductId = item.ProductId,
                        ItemQuantity = item.ItemQuantity,
                        CreateDate = DateTime.Now,
                        EditDate = DateTime.Now
                    };

                    addCartItems.Add(newCartItem);
                }
                else
                {
                    cartItem.ProductId = item.ProductId;
                    cartItem.ItemQuantity = cartItem.ItemQuantity += item.ItemQuantity;
                    cartItem.EditDate = DateTime.Now;

                    updateCartItems.Add(cartItem);
                }
            }

            if (addCartItems.Count > 0)
            {
                _cartItemRepository.AddRange(addCartItems);
            }

            if (updateCartItems.Count > 0)
            {
                _cartItemRepository.UpdateRange(updateCartItems);
            }

            var cartItems = _cartItemRepository.GetAll(c => c.CartId == cart.CartId);

            var cartResponse = new CartResponse()
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                TotalItemQuantity = cart.TotalItemQuantity,
                TotalItemPrice = cart.TotalItemPrice,
                CreateDate = cart.CreateDate,
                EditDate = cart.EditDate,
                CartItems = cartItems.Select(x => new CartItemResponse()
                {
                    CartItemId = x.CartItemId,
                    CartId = x.CartId,
                    ProductId = x.ProductId,
                    ItemQuantity = x.ItemQuantity,
                    CreateDate = x.CreateDate,
                    EditDate = x.EditDate
                }).ToList()
            };

            return new SuccessDataResult<CartResponse>(cartResponse, "Sepet başarıyla oluşturuldu.");
        }

        public IResult Delete(int id)
        {
            var cart = _cartRepository.Get(c=> c.CartId == id);
            if (cart is null)
            {
                return new ErrorDataResult<CartResponse>(default, "Sepet silinemedi.");
            }

            _cartRepository.Delete(cart);
      
            return new SuccessDataResult<CartResponse>(default, "Sepet silindi.");
        }

        public IDataResult<CartResponse> Get(int id)
        {
            var cart = _cartRepository.Get(c => c.CartId == id);
            if (cart is null)
            {
                return new ErrorDataResult<CartResponse>(default, "Descriptive error message here.");
            }

            var cartItems = _cartItemRepository.GetAll(c => c.CartId == cart.CartId);

            CartResponse cartResponse = new()
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                TotalItemQuantity = cart.TotalItemQuantity,
                TotalItemPrice = cart.TotalItemPrice,
                CreateDate = cart.CreateDate,
                EditDate = cart.EditDate,
                CartItems = cartItems?.Select(c => new CartItemResponse()
                {
                    CartItemId = c.CartItemId,
                    CartId = c.CartId,
                    ProductId = c.ProductId,
                    ItemQuantity = c.ItemQuantity,
                    CreateDate = c.CreateDate,
                    EditDate = c.EditDate
                }).ToList() ?? new()
            };

            return new SuccessDataResult<CartResponse>(cartResponse, "Sepet bilgisi getirildi.");
        }

        public IDataResult<List<CartResponse>> GetAll()
        {
            var carts = _cartRepository.GetAll().Select(x => new CartResponse()
            {
                CartId = x.CartId,
                UserId = x.UserId,
                TotalItemQuantity = x.TotalItemQuantity,
                TotalItemPrice = x.TotalItemPrice,
                CreateDate = x.CreateDate,
                EditDate = x.EditDate,
                CartItems = _cartItemRepository.GetAll(c => c.CartId == x.CartId).Select(c => new CartItemResponse()
                {
                    CartItemId = c.CartItemId,
                    CartId = c.CartId,
                    ProductId = c.ProductId,
                    ItemQuantity = c.ItemQuantity,
                    CreateDate = c.CreateDate,
                    EditDate = c.EditDate
                }).ToList()
            }).ToList();

            return new SuccessDataResult<List<CartResponse>>(carts, "Sepet bilgileri getirildi.");
        }

        public IResult Update(int id, CartRequest data)
        {
            throw new NotImplementedException();
        }

        public IResult DeleteCartItem(int cartItemId)
        {
            var cartItem = _cartItemRepository.Get(c => c.CartItemId == cartItemId);
            _cartItemRepository.Delete(cartItem);
            return new SuccessResult("Ürün sepetten başarıyla silindi.");
        }
    }
}
