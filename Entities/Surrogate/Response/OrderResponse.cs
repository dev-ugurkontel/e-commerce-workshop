using Core.Entity.Abstract;

namespace Entities.Surrogate.Response
{
    public class OrderResponse : ISurrogate
    {
        public OrderResponse()
        {
            OrderItems = new();
        }

        public int OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
        public int CartId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }

        public List<OrderItemResponse> OrderItems { get; set; }
    }
}
