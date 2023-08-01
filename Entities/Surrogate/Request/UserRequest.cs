using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Surrogate.Request
{
    public class UserRequest : ISurrogate
    {
       
        public string UserName { get; set; }
        public string UserEmail { get; set; }        
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserAddress { get; set; }
        public int UserRole { get; set; }
        public byte[] UserPasswordSalt { get; set; }
        public byte[] UserPasswordHash { get; set; }

    }
}
