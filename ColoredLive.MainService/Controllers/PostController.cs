using ColoredLive.BL.Interfaces;
using ColoredLive.BL.Realizations;
using ColoredLive.Core.Entities;
using ColoredLive.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColoredLive.MainService.Controllers
{
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private IRepository<UserEntity> _users;

        public PostController(IRepository<UserEntity> users)
        {
            _users = users;
        }


        [HttpGet]
        public List<UserEntity> OnGet()
        {
            _users.Add(new UserEntity { Password = "newew" });
            return _users.FindAll(el => true);

        }
    }
}
