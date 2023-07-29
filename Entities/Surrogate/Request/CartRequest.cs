using Core.Entity.Abstract;

namespace Entities.Surrogate.Request
{
    public class CartRequest : ISurrogate
    {
        public CartRequest()
        {
            CartItems = new();
        }

        public int CartId { get; set; }
        public int UserId { get; set; }
        public int TotalItemQuantity { get; set; }
        public double TotalItemPrice { get; set; }

        public List<CartItemRequest> CartItems { get; set; }
    }
}
