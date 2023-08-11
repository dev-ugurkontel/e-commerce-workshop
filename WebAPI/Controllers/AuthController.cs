﻿using Business.Abstract;
using Entities.Surrogate.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Utils;
using Core.Entity.Model;

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

        public ActionResult Login(LoginRequest data)
        {
            var userToLogin = _authService.Login(data);
            if(userToLogin.Status == ResultStatus.Error)
            {
                return BadRequest(userToLogin.Message);
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
                UserFirstName = registerResult.Data.UserFirstName,
                UserLastName = registerResult.Data.UserLastName,
                UserId = registerResult.Data.UserId,
                UserRole = registerResult.Data.UserRole

            };

            var result = _authService.CreateAccessToken(userTokenModel);

            if (result.Status == ResultStatus.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);


        }
    }
}