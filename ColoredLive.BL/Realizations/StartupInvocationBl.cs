using System.Linq;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Models;
using ColoredLive.DAL;

namespace ColoredLive.BL.Realizations
{
    [Buisness]
    public class StartupInvocationBl : IStartupInvocationBl
    {
        private readonly IRepository<RoleEntity> _roles;

        public StartupInvocationBl(IRepository<RoleEntity> roles)
        {
            _roles = roles;
        }
        public void Startup()
        {
            var rolesAtDb = _roles.FindAll(el => true).Select(el => el.Role);
           
            foreach (var role in Roles.All)
            {
                if (!rolesAtDb.Contains(role))
                    _roles.Add(new RoleEntity {Role = role});
            }
        }
    }
}