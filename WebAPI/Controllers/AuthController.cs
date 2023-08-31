using Business.Abstract;
using Core.Entity.Model;
using Core.Utils.Results;
using Entities.Surrogate.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult Login(LoginRequest data)
        {
            var userToLogin = _authService.Login(data);
            if(userToLogin.Status == ResultStatus.Error)
            {
                return BadRequest(userToLogin);
            }
            var userTokenModel = new UserTokenModel()
            {

                UserEmail = userToLogin.Data.UserEmail,
                UserFirstName = userToLogin.Data.UserFirstName,
                UserLastName = userToLogin.Data.UserLastName,
                UserId = userToLogin.Data.UserId,
                UserRole = userToLogin.Data.UserRole

            };


            var result = _authService.CreateAccessToken(userTokenModel);
            if (result.Status == ResultStatus.Success)
            {
                return Ok(result);
            } 

            return BadRequest(result);
        }

        [Route("Register")]
        [HttpPost]
      
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
                UserFirstName = registerResult.Data.UserFirstName,
                UserLastName = registerResult.Data.UserLastName,
                UserId = registerResult.Data.UserId,
                UserRole = registerResult.Data.UserRole

            };

            var result = _authService.CreateAccessToken(userTokenModel);

            if (result.Status == ResultStatus.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);


        }
    }
}
