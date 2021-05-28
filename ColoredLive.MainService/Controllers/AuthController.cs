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
using ColoredLive.Core.Models;
using ColoredLive.Core.Utils;


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
        [HttpGet("log")]
        public ActionResult<string> Login(AuthRequest request)
        {
            var findedUser = _userBl.Authorize(request.Login, request.Password);

            if (findedUser.Id.Empty())
                return StatusCode(StatusCodes.Status400BadRequest);

            return _tokenBl.Generate(findedUser);
        }
        [HttpGet("info")]
        [JwtAuth]
        [RequireRole(Roles.EventChecker)]
        public ActionResult<UserEntity> GetInfo() => Identity.User;

        /// <summary>
        /// Регестрирует пользователя в системе, возвращает токен пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("reg")]
        public ActionResult<string> RegisterUser(RegisterRequest request)
        {
            var newUser = _userBl.Register(new UserEntity { Email = request.Email, Login = request.Login, Password = request.Password });
            
            if (newUser.Id.Empty())
                return "";
            
            return _tokenBl.Generate(newUser);
        }
    }
}
