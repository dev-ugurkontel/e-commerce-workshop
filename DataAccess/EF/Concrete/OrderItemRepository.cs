using DataAccess.EF.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Concrete
{
    public class OrderItemRepository : OrderItemRepositoryBase
    {
        public OrderItemRepository(DbContext db) : base(db)
        {
        }
    }
}
