using Core.Entity.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Surrogate.Request
{
    public class ProductRequest : ISurrogate
    {
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int ProductStock { get; set; }
        public double ProductPrice { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductCampaignId { get; set; }
        public int ProductStatus { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
