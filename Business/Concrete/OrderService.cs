using Business.Abstract;
using Core.Utils;
using Core.Utils.Results;
using DataAccess.EF.Abstract;
using DataAccess.EF.Concrete;
using Entities.Entity;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;

namespace Business.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepositoryBase _orderRepository;
        private readonly OrderItemRepositoryBase _orderItemRepository;

        public OrderService(OrderRepositoryBase orderRepository, OrderItemRepositoryBase orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public IDataResult<OrderResponse> CompleteOrder(OrderRequest orderRequest)
        {
            Order newOrder = new()
            {
                CartId = orderRequest.CartId,
                OrderNumber = Guid.NewGuid().ToString().ToUpper(),
                OrderStatus = 1,
                OrderDate = DateTime.Now,
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now
            };

            var orderResult = _orderRepository.Add(newOrder);

            foreach (var item in orderRequest.OrderItems)
            {
                OrderItem newOrderItem = new()
                {
                    OrderId = newOrder.OrderId,
                    ProductId = item.ProductId,
                    ItemQuantity = item.ItemQuantity,
                    ItemPrice = item.ItemPrice,
                    DiscountPrice = item.DiscountPrice,
                    DiscountRate = item.DiscountRate,
                    ShipDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    EditDate = DateTime.Now
                };

                _orderItemRepository.Add(newOrderItem);
            }

            var orderItems = _orderItemRepository.GetAll(c => c.OrderId == orderResult.OrderId);

            OrderResponse orderResponse = new()
            {
                CartId = orderResult.CartId,
                OrderNumber = orderResult.OrderNumber,
                OrderStatus = orderResult.OrderStatus,
                OrderDate = orderResult.OrderDate,
                CreateDate = orderResult.CreateDate,
                EditDate = orderResult.EditDate,
                OrderItems = orderItems.Select(x => new OrderItemResponse()
                {
                    OrderItemId = x.OrderItemId,
                    OrderId = x.OrderId,
                    ItemQuantity = x.ItemQuantity,
                    ItemPrice = x.ItemPrice,
                    DiscountRate = x.DiscountRate,
                    DiscountPrice = x.DiscountPrice,
                    ShipDate = x.ShipDate,
                    CreateDate = x.CreateDate,
                    EditDate = x.EditDate
                }).ToList()
            };

            return new SuccessDataResult<OrderResponse>(orderResponse, "Sipariş başarıyla oluşturuldu.");
        }
    }
}
