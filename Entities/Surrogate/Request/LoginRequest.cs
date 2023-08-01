using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Surrogate.Request
{
    public class LoginRequest : ISurrogate
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
