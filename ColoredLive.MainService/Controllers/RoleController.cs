using System;
using System.Collections.Generic;
using System.Linq;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Requests;
using ColoredLive.Service.Core.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColoredLive.MainService.Controllers
{
    [JwtAuth]
    public class RoleController : ProjectControllerBase
    {
        private readonly IUserBl _userBl;

        public RoleController(IUserBl userBl)
        {
            _userBl = userBl;
        }

        [HttpPost("set")]
        public ActionResult SetRole(SetRoleRequest request)
        {
            var isAdded = _userBl.SetRole(Identity.User.Id, request.RoleId);

            return new JsonResult(new { Message = isAdded ? "Роль назначенна" : "При назначении роли произошла ошибка"  }) { StatusCode = StatusCodes.Status200OK};
        }
        
        
        
        [HttpGet("get")]
        public ActionResult<IEnumerable<RoleEntity>> GetRoles() => Identity.Roles.ToArray();
        
    }
}