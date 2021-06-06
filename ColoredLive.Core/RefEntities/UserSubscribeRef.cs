using System;

namespace ColoredLive.Core.RefEntities
{
    public class UserSubscribeRef : Entity
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}