using ColoredLive.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColoredLive.MainService.Controllers
{
    [Route("[controller]")]
    public class ProjectControllerBase : ControllerBase
    {
        protected UserEntity Identity { get; set; }
        public ProjectControllerBase(IHttpContextAccessor acsessor)
        {
            Identity = (UserEntity) acsessor.HttpContext.Items["User"];
        }
    }
}
