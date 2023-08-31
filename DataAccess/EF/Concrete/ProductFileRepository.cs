using DataAccess.EF.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EF.Concrete
{
    public class ProductFileRepository : ProductFileRepositoryBase
    {
        public ProductFileRepository(DbContext db) : base(db)
        {

        }
    }
}
