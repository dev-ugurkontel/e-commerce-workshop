using Core.Entity.Abstract;

namespace Entities.Surrogate.Request
{
    public class OrderItemRequest : ISurrogate
    {
        public int ItemQuantity { get; set; }
        public double ItemPrice { get; set; }
        public int ProductId { get; set; }
        public double DiscountRate { get; set; }
        public double DiscountPrice { get; set; }
    }
}
