using Core.Business.Abstract;
using Core.Utils;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICampaignService : IService<CampaignRequest, CampaignResponse>
    {
    }
}
