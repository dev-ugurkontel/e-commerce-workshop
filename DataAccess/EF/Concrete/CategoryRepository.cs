using DataAccess.EF.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Concrete
{
    public class CategoryRepository : CategoryRepositoryBase
    {
        public CategoryRepository(DbContext db): base(db)
        {
                
        }
    }
}
