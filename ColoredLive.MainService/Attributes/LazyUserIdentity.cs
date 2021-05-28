using System;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Models;
using ColoredLive.MainService.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

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

            if (context.HttpContext.Items.TryGetValue("User", out var user))
            {
                var convertedUser = (UserEntity) user;
                controller.Identity = new Identity
                {
                    User = (UserEntity) user,
                    Roles = context.HttpContext.RequestServices.GetService<IUserBl>().GetUserRoles(convertedUser.Id)
                };
            }
               
        }
    }
}