using System;

namespace ColoredLive.Core.Entities
{
    public class RoleEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
    }
}