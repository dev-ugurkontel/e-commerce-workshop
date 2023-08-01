using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class User : IEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public byte[] UserPasswordSalt{ get; set; }
        public byte[] UserPasswordHash{ get; set; }
        public string UserFirstName{ get; set; }
        public string UserLastName{ get; set; }
        public string UserAddress{ get; set; }
        public int UserRole{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime EditDate{ get; set; }
    }
}
