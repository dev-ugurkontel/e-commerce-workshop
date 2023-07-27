using Core.Entity.Abstract;

namespace Entities.Surrogate.Response
{
    public class ShoppingResponse : ISurrogate
    {
        public CartResponse Cart { get; set; }
    }
}
