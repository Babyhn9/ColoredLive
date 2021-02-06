using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColoredLive.MainService.Controllers
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserBl _userBl;
        private readonly IRecommendationBl _recommendationBl;
        private readonly ITokenCreationBl _tokenBl;

        public AuthController(IUserBl userBl, IRecommendationBl recommendationBl, ITokenCreationBl tokenBl)
        {
           _userBl = userBl;
           _recommendationBl = recommendationBl;
           _tokenBl = tokenBl;
        }

        

        [HttpGet("authorize")]
        [AllowAnonymous]
        public ActionResult<string> GetUserInfo(string login, string password)
        {
            var findedUser = _userBl.Authorize(login,password);
            _recommendationBl.GetTopRecomendations();
            if (findedUser == null) return "";
            
            return _tokenBl.Generate(findedUser);
        }


        [HttpPost("reg")]
        [AllowAnonymous]
        public ActionResult<string> RegisterUser(UserEntity entity)
        {
            var findedUser = _userBl.Register(entity);

            if (findedUser == null) return "";

            return _tokenBl.Generate(findedUser);
        }

    }
}
