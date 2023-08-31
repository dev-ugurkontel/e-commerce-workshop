using Core.DataAccess.Concrete.EF;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EF.Abstract
{
    public abstract class ProductFileRepositoryBase : EfEntityRepositoryBase<ProductFile>
    {
        public ProductFileRepositoryBase(DbContext db) : base(db)
        {
        }
    }
}
