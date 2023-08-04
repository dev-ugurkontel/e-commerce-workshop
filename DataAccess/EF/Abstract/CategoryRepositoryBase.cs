using Core.DataAccess.Concrete.EF;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Abstract
{
    public abstract class CategoryRepositoryBase : EfEntityRepositoryBase<Category>
    {
        public CategoryRepositoryBase(DbContext db) : base(db)
        {
                
        }
    }
}
