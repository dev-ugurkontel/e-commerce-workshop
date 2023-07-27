using DataAccess.EF.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Concrete
{
    public class CartItemRepository : CartItemRepositoryBase
    {
        public CartItemRepository(DbContext db) : base(db)
        {
        }
    }
}
