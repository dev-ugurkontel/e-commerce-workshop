using Business.Abstract;
using Core.Entity.Concrete;
using Core.Utils;
using Core.Utils.Enums;
using Core.Utils.Security.Helpers;
using Core.Utils.Security.JWT;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        private  IUserService _userService;
        private ITokenHelper _tokenHelper;
        public AuthService(IUserService userService,ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
      

        public IDataResult<UserResponse> Register(RegisterRequest data)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(data.UserPassword, out passwordHash, out passwordSalt);

            var userRequest = new UserRequest()
            {
                UserAddress = data.UserAddress,
                UserEmail = data.UserEmail,
                UserFirstName = data.UserFirstName,
                UserLastName = data.UserLastName,
                UserName = data.UserName,
                UserPasswordHash = passwordHash,
                UserPasswordSalt = passwordSalt,
                UserRole = (int)Roles.User
            };
            var userResponse = _userService.Add(userRequest);
            return new SuccessDataResult<UserResponse>(userResponse.Data);
        }

        public IDataResult<UserResponse> Login(LoginRequest data)
        {
            var userToCheck = _userService.GetByMail(data.UserEmail);
            if (userToCheck == null)
            {
                return new ErrorDataResult<UserResponse>(null, "Kullanıcı Bulunamadı");

            }
            if (!HashingHelper.VerifyPasswordHash(data.UserPassword, userToCheck.UserPasswordHash, userToCheck.UserPasswordSalt))
            {
                return new ErrorDataResult<UserResponse>(null, "Parola Hatası");
            }
            UserResponse user = new UserResponse()
            {
                UserAddress = userToCheck.UserAddress,
                UserEmail = userToCheck.UserEmail,
                UserFirstName = userToCheck.UserFirstName,
                UserId = userToCheck.UserId,
                UserLastName = userToCheck.UserLastName,
                UserName = userToCheck.UserName,
                UserRole = (int)Roles.User,
            };
            return new SuccessDataResult<UserResponse>(user, "Başarılı Giriş");
        }


        public IResult UserExists(string email)
        {
            if(_userService.GetByMail(email)!= null)
            {
                return new ErrorResult("Kullanıcı Mevcut");
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken>CreateAccessToken(UserTokenModel data)
        {
            var accessToken = _tokenHelper.CreateToken(data);
            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu");
        }
    }
}
