using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ColoredLive.Core.Models;
using ColoredLive.Service.Core;
using ColoredLive.Service.Core.Attributes;
using ColoredLive.MainService.Requests;


namespace ColoredLive.MainService.Controllers
{
    
    public class AuthController : ProjectControllerBase
    {
        private readonly IUserBl _userBl;
        private readonly ITokenCreationBl _tokenBl;

        public AuthController(
            IUserBl userBl, 
            ITokenCreationBl tokenBl
            ) 
        {
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
        public ActionResult<UserIdentity> GetInfo() => Identity;
        
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
