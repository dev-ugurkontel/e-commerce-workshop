using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Surrogate.Request
{
    public class CartRequest : ISurrogate
    {
        public int CartId { get; set; }
        public int TotalltemQuantity { get; set; }
        public double TotalltemPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
