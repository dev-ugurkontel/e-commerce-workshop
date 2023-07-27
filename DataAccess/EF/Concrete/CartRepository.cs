using DataAccess.EF.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Concrete
{
    public class CartRepository : CartRepositoryBase
    {
        public CartRepository(DbContext db) : base(db)
        {
        }
    }
}
