using System.Collections.Generic;
using ColoredLive.Core.Entities;

namespace ColoredLive.Core.Models
{
    public class UserIdentity
    {
        public UserEntity User { get; set; }
        public IEnumerable<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}