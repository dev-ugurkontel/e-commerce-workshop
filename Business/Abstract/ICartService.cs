using Core.Business.Abstract;
using Core.Utils.Results;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;

namespace Business.Abstract
{
    public interface ICartService : IService<CartRequest, CartResponse>
    {
        IDataResult<CartItemResponse> GetCartItemByCartItemId(int cartItemId);
        IResult DeleteCartItem(int cartItemId);
    }
}
