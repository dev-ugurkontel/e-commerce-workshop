using Core.Utils.Results;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<OrderResponse> CompleteOrder(OrderRequest orderRequest);
    }
}
