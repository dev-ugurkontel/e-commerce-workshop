using Core.Utils.Results;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;

namespace Business.Abstract
{
    public interface IShoppingService
    {
        IDataResult<CartResponse> AddToCart(int userId, CartItemRequest cartItemRequest);
        IDataResult<CartResponse> UpdateCart(int userId, CartItemRequest cartItemRequest);
        IDataResult<CartResponse> GetCart(int cartId);
        IDataResult<OrderResponse> CompleteOrder(int cartId);
        IResult RemoveFromCart(int cartItemId);
        IResult ClearCart(int cartId);
    }
}
