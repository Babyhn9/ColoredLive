using System;
using System.Linq;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Requests;
using ColoredLive.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ColoredLive.Core.Models;
using ColoredLive.Core.Utils;
using ColoredLive.Service.Core.Attributes;
using Microsoft.EntityFrameworkCore;


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

        [HttpGet("log")]
        public ActionResult<string> Login(AuthRequest request)
        {
            var findedUser = _userBl.Authorize(request.Login, request.Password);

            if (findedUser.IsEmpty)
                return StatusCode(StatusCodes.Status400BadRequest);

            return new JsonResult(new { Token = _tokenBl.Generate(findedUser)});
        }
        
        [HttpGet("info")]
        [JwtAuth]
        public ActionResult<Identity> GetInfo() => Identity;
        
        /// <summary>
        /// Регестрирует пользователя в системе, возвращает токен пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("reg")]
        public ActionResult<string> RegisterUser(RegisterRequest request)
        {
            var newUser = _userBl.Register(new UserEntity { Email = request.Email, Login = request.Login, Password = request.Password });
            
            if (newUser.IsEmpty)
                return "";
            
            return new JsonResult( new { Token = _tokenBl.Generate(newUser)});
        }
    }
}
