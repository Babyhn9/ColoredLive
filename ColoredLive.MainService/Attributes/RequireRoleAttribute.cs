using System;
using System.Collections.Generic;
using System.Linq;
using ColoredLive.MainService.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ColoredLive.MainService.Attributes
{
    public class RequireRoleAttribute : Attribute, IActionFilter, IOrderedFilter
    {

        public int Order { get; } = 2;
        

        private string _reqRole;
        public RequireRoleAttribute(string role)
        {
            _reqRole = role;
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is IAuthorizationController controller)
                if (controller.Identity.Roles.All(el => el.Role != _reqRole))
                    context.Result = new JsonResult(new {Message = "У вас нет прав"})
                    {
                        StatusCode = StatusCodes.Status405MethodNotAllowed
                    };
        }

    }
}