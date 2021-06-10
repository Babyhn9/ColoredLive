﻿using ColoredLive.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColoredLive.Core.Models;
using ColoredLive.DAL;
using ColoredLive.Service.Core.Attributes;
using ColoredLive.Service.Core.Utils;

namespace ColoredLive.MainService.Controllers
{
    [Route("[controller]")]
    [LazyUserIdentity]
    public class ProjectControllerBase : ControllerBase, IAuthorizationController
    {
        public Identity Identity { get; set; }
       
    }
}
