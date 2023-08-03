using Core.DataAccess.Concrete.EF;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Abstract
{
    public class OrderItemRepositoryBase : EfEntityRepositoryBase<OrderItem>
    {
        public OrderItemRepositoryBase(DbContext db) : base(db)
        {
        }
    }
}
