using Core.Entity.Abstract;

namespace Entities.Surrogate.Request
{
    public class ShoppingRequest : ISurrogate
    {
        public int CartItemId { get; set; }
        public int ItemQuantity { get; set; }
    }
}
