using Core.Entity.Abstract;

namespace Entities.Entity
{
    public class Order : IEntity
    {
        public int OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
        public int CartId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
