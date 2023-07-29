using Core.DataAccess.Concrete.EF;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Abstract
{
    public class CartItemRepositoryBase : EfEntityRepositoryBase<CartItem>
    {
        public CartItemRepositoryBase(DbContext db) : base(db)
        {
        }
    }
}
