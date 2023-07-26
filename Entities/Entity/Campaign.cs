using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Campaign : IEntity
    {
        public int CampaignId { get; set; }
        public string CampaignCode { get; set; }
        public string CampaignName { get; set; }
        public double CampaignDiscountRate { get; set; }
        public int CampaignStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
