using Core.Entity.Abstract;

namespace Entities.Surrogate.Response
{
    public class CartResponse : ISurrogate
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int TotalItemQuantity { get; set; }
        public double TotalItemPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }

        public List<CartItemResponse> CartItems { get; set; }
    }
}
