using Core.Utils;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;

namespace Business.Abstract
{
    public interface IShoppingService
    {
        IDataResult<CartResponse> AddToCart(int userId, CartItemRequest cartItemRequest);
        IDataResult<CartResponse> UpdateCart(int userId, CartItemRequest cartItemRequest);
        IDataResult<OrderResponse> CompleteOrder(int cartId);
        IResult RemoveFromCart(int cartItemId);
        IResult ClearCart(int cartId);
    }
}
