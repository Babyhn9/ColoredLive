using System;

namespace ColoredLive.Core.RefEntities
{
    public class UserRoleRef : Entity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}