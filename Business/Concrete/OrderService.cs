using Business.Abstract;
using Core.Utils;
using DataAccess.EF.Abstract;
using DataAccess.EF.Concrete;
using Entities.Entity;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepositoryBase _orderRepository;

        public OrderService(OrderRepositoryBase orderRepositoryBase)
        {
            _orderRepository = orderRepositoryBase;
        }
        public IResult Add(OrderRequest data)
        {
            var entity = new Order()
            {
                OrderNumber = data.OrderNumber,
                OrderDate = DateTime.Now,
                OrderStatus = data.OrderStatus,
                CartID = data.CartID,
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now

            };
            _orderRepository.Add(entity);
            return new SuccessResult("Sipariş başarıyla kaydedildi.");
        }

        public IResult Delete(int id)
        {
            var order = _orderRepository.Get(c => c.OrderId == id);
            _orderRepository.Delete(order);
            return new SuccessResult("Sipariş başarıyla silindi.");
        }

        public IDataResult<OrderResponse> Get(int id)
        {
            try
            {
                var getOrder = _orderRepository.Get(c => c.OrderId == id);

                if (getOrder == null)
                {
                    return new ErrorDataResult<OrderResponse>(default, "Kayıt bulunamadı.");
                }
                var OrderResponse = new OrderResponse()
                {
                    OrderId = getOrder.OrderId,
                    OrderNumber = getOrder.OrderNumber,
                    OrderDate = getOrder.OrderDate,
                    OrderStatus = getOrder.OrderStatus,
                    CreateDate = getOrder.CreateDate,
                    EditDate = getOrder.EditDate,
                    CartID = getOrder.CartID
                };
                return new SuccessDataResult<OrderResponse>(OrderResponse);
            }
            catch (Exception ex)
            {
                return new ExceptionDataResult<OrderResponse>();
            }
        }

        public IDataResult<List<OrderResponse>> GetAll()
        {
            var orderList = _orderRepository.GetAll().Select(o => new OrderResponse()
            {
                OrderNumber = o.OrderNumber,
                OrderDate = o.OrderDate,
                OrderStatus = o.OrderStatus,
                CreateDate = o.CreateDate,
                EditDate = o.EditDate,
                CartID = o.CartID
            }).ToList();

            return new SuccessDataResult<List<OrderResponse>>(orderList, "Sipariş bilgileri getirildi.");
        }

        public IResult Update(int id, OrderRequest data)
        {
            var order = _orderRepository.Get(c => c.OrderId == id);
            order.OrderNumber = data.OrderNumber;
            order.OrderDate = data.OrderDate;
            order.OrderStatus = data.OrderStatus;
            order.EditDate = DateTime.Now;
            order.CartID = data.CartID;
            _orderRepository.Update(order);
            return new SuccessResult("Sipariş başarıyla güncellendi");
        }
    }
}
