using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Requests;
using ColoredLive.Core.Responses;
using ColoredLive.DAL;
using ColoredLive.MainService.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace ColoredLive.MainService.Controllers
{
    
    public class AuthController : ProjectControllerBase
    {
        private readonly IRepository<UserEntity> _users;
        private readonly IUserBl _userBl;
        private readonly ITokenCreationBl _tokenBl;

        public AuthController(
            IUserBl userBl, 
            ITokenCreationBl tokenBl, 
            IRepository<UserEntity> users) 
        {
            _users = users;
            _userBl = userBl;
            _tokenBl = tokenBl;
        } 

        /// <summary>
        /// возвращает токен пользователя по логину и паролю
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("me")]
        public BaseResponse GetUserInfo(AuthRequest request)
        {
            var findedUser = _userBl.Authorize(request.Login, request.Password);

            if (findedUser.Id == Guid.Empty)
                return BaseResponse.Error(StatusCodes.Status400BadRequest, "Пользователь с такими данными не обнаружен");


            return BaseResponse.Ok(new AuthResponse(_tokenBl.Generate(findedUser)));
        }

        /// <summary>
        /// Регестрирует пользователя в системе, возвращает токен пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("reg")]
        public ActionResult<string> RegisterUser(RegisterRequest request)
        {
            var newUser = _userBl.Register(new UserEntity { Email = request.Email, Login = request.Login, Password = request.Password });
            
            if (newUser.Id == Guid.Empty)
                return "";
            
            return _tokenBl.Generate(newUser);
        }
    }
}
