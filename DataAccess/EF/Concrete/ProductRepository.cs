using DataAccess.EF.Abstract;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EF.Concrete
{
    public class ProductRepository : ProductRepositoryBase
    {
        public ProductRepository(DbContext db) : base(db)
        {

        }
    }
}
