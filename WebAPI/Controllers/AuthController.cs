using Business.Abstract;
using Core.Entity.Concrete;
using Core.Utils;
using Entities.Surrogate.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IUserService _userService;
        public AuthController(IAuthService authService,IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginRequest data)
        {
            var userToLogin = _authService.Login(data);
            if (userToLogin.Status == ResultStatus.Error)
            {
                return BadRequest(userToLogin.Message);
            }

            var userTokenModel = new UserTokenModel()
            {
                UserEmail = userToLogin.Data.UserEmail,
                UserId  = userToLogin.Data.UserId,
                UserFirstName = userToLogin.Data.UserFirstName,
                UserLastName = userToLogin.Data.UserLastName,
                UserRole = userToLogin.Data.UserRole
            };
            

            var result = _authService.CreateAccessToken(userTokenModel);
            if(result.Status == ResultStatus.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        public ActionResult Register(RegisterRequest data)
        {
            var userExists = _authService.UserExists(data.UserEmail);
            if(userExists.Status == ResultStatus.Error)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(data);

            var userTokenModel = new UserTokenModel()
            {
                UserEmail = registerResult.Data.UserEmail,
                UserId = registerResult.Data.UserId,
                UserFirstName = registerResult.Data.UserFirstName,
                UserLastName = registerResult.Data.UserLastName,
                UserRole = registerResult.Data.UserRole
            };
            var result = _authService.CreateAccessToken(userTokenModel);
            if(result.Status == ResultStatus.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


    }
}
