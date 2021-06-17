using ColoredLive.Core.Models;
using ColoredLive.Service.Core.Attributes;
using ColoredLive.Service.Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ColoredLive.Service.Core
{
    [Route("[controller]")]
    [LazyUserIdentity]
    public class ProjectControllerBase : ControllerBase, IAuthorizationController
    {
        public Identity Identity { get; set; }
       
    }
}
