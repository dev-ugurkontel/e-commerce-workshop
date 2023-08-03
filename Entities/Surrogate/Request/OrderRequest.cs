using Core.Entity.Abstract;

namespace Entities.Surrogate.Request
{
    public class OrderRequest : ISurrogate
    {
        public OrderRequest()
        {
            OrderItems = new();
        }

        public int CartId { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? OrderStatus { get; set; }

        public List<OrderItemRequest> OrderItems { get; set; }
    }
}
