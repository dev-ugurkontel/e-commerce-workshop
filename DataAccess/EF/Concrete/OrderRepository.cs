using DataAccess.EF.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Concrete
{
    public class OrderRepository : CartRepositoryBase
    {
        public OrderRepository(DbContext db) : base(db)
        {
        }
    }
}
