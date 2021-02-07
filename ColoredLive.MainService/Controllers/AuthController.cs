using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.DAL;
using ColoredLive.MainService.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColoredLive.MainService.Controllers
{
    public class AuthController : ProjectControllerBase
    {
        private readonly IRepository<UserEntity> _users;
        private readonly IUserBl _userBl;
        private readonly IRecommendationBl _recommendationBl;
        private readonly ITokenCreationBl _tokenBl;

        public AuthController(
            IHttpContextAccessor acsessor,
            IUserBl userBl, IRecommendationBl recommendationBl, 
            ITokenCreationBl tokenBl, 
            IRepository<UserEntity> users) : base(acsessor)
        {
            _users = users;
            _userBl = userBl;
            _recommendationBl = recommendationBl;
            _tokenBl = tokenBl;
        }

        [HttpGet("authorize")]
        public ActionResult<string> GetUserInfo(string login, string password)
        {
            var findedUser = _userBl.Authorize(login, password);

            if (findedUser.Id == Guid.Empty)
                return "";


            return _tokenBl.Generate(findedUser);
        }


        [HttpPost("reg")]
        public ActionResult<string> RegisterUser(UserEntity entity)
        {
            var newUser = _userBl.Register(entity);
            
            if (newUser.Id == Guid.Empty)
                return "";
            
            return _tokenBl.Generate(newUser);
        }

        [JwtAuth]
        [HttpGet]
        public ActionResult<IEnumerable<UserEntity>> GetAllUsers()
        {
            return _users.FindAll(el => true);
        }

    }
}
