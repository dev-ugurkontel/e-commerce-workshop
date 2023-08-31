using Core.Entity.Abstract;

namespace Entities.Entity
{
    public class OrderItem : IEntity
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ItemQuantity { get; set; }
        public double ItemPrice { get; set; }
        public double DiscountRate { get; set; }
        public double DiscountPrice { get; set; }
        public DateTime ShipDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
