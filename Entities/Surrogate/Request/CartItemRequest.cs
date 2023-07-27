using Core.Entity.Abstract;

namespace Entities.Surrogate.Request
{
    public class CartItemRequest : ISurrogate
    {
        public int ProductId { get; set; }
        public int ItemQuantity { get; set; }
    }
}
