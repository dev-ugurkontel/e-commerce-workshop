using Core.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(UserTokenModel user);
    }
}
