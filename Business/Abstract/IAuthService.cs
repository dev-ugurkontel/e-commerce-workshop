using Core.Entity.Model;
using Core.Utils.Results;
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
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(UserTokenModel data);
    }
}
