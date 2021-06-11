using System;
using System.Linq;
using ColoredLive.Service.Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ColoredLive.Service.Core.Attributes
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