using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Order : IEntity
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
        public int CartID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
