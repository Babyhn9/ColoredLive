using System;

namespace ColoredLive.Core.RefEntities
{
    public class UserRoleRef : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}