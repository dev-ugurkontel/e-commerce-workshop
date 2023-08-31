using Core.Entity.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Surrogate.Request
{
    public class ProductFileRequest : ISurrogate
    {
        public int ProductId { get; set; }
        public IFormFileCollection File { get; set; }
    }
}
