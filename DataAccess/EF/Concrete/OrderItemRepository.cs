using DataAccess.EF.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Concrete
{
    public class OrderItemRepository : CartItemRepositoryBase
    {
        public OrderItemRepository(DbContext db) : base(db)
        {
        }
    }
}
