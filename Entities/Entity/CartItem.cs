using Core.Entity.Abstract;

namespace Entities.Entity
{
    public class CartItem : IEntity
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int ItemQuantity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
