using System;
using ColoredLive.Core.Entities;
using ColoredLive.MainService.Utils;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ColoredLive.MainService.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class LazyUserIdentity : Attribute, IActionFilter 
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as IAuthorizationController;
            if(controller?.Identity != null) return;
            
            if(context.HttpContext.Items.TryGetValue("User", out var user))
                controller.Identity = (UserEntity) user;
        }
    }
}