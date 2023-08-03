using Core.Entity.Concrete;
using Core.Utils;
using Core.Utils.Security.JWT;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<UserResponse> Register(RegisterRequest data);
        IDataResult<UserResponse> Login(LoginRequest data);
        IDataResult<AccessToken> CreateAccessToken(UserTokenModel data);
        IResult UserExists(string email);
    }
}
