using DataAccess.EF.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EF.Concrete
{
    public class CampaignRepository : CampaignRepositoryBase
    {
        public CampaignRepository(DbContext db) : base(db)
        {
        }
    }
}
