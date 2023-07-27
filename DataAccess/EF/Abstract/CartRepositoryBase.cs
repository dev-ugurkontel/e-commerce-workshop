using Core.DataAccess.Concrete.EF;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Abstract
{
    public class CartRepositoryBase : EfEntityRepositoryBase<Cart>
    {
        public CartRepositoryBase(DbContext db) : base(db)
        {
        }
    }
}
