using System;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Models;
using ColoredLive.Service.Core.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace ColoredLive.Service.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class LazyUserIdentity : Attribute, IActionFilter 
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userController = context.Controller as IAuthorizationController<UserIdentity>;
            if (userController?.Identity != null)
            {
                if (context.HttpContext.Items.TryGetValue("User", out var user))
                {
                    var convertedUser = (UserEntity) user;
                    userController.Identity = new UserIdentity
                    {
                        User = (UserEntity) user,
                        Roles = context.HttpContext.RequestServices.GetService<IUserBl>().GetRoles(convertedUser.Id)
                    };
                }
                return;
            }
            
            var partnerController = context.Controller as IAuthorizationController<PartnerIdentity>;
           
            if (partnerController?.Identity !=null)
            {
                if (context.HttpContext.Items.TryGetValue("Partner", out var partner))
                {
                    var convertedUser = (PartnerEntity) partner;
                    partnerController.Identity = new PartnerIdentity
                    {
                        User = (PartnerEntity) partner,
                        Roles = context.HttpContext.RequestServices.GetService<IPartnerBl>().GetRoles(convertedUser.Id)
                    };
                }
            }
            
          
            
            
               
        }
    }
}