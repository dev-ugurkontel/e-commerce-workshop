using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Surrogate.Response
{
    public class ProductResponse : ISurrogate
    {
        public int ProductId { get; set; }
        public string ProductSKU { get; set; }
        public string ProductUrl { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductStock { get; set; }
        public double ProductPrice { get; set; }
        public string ProductImagePath { get; set; }
        public CategoryResponse Category { get; set; }
        public int ProductCampaingId { get; set; }
        public int ProductStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
