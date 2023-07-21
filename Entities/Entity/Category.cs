using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Category : IEntity
    {
        public int CategoryId { get; set; }       
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int CategoryStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
