using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Surrogate.Request
{
    public class CategoryRequest : ISurrogate
    {
        
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int CategoryStatus { get; set; }
   
    }
}
