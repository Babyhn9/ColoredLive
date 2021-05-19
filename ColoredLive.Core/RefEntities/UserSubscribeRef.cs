using System;

namespace ColoredLive.Core.RefEntities
{
    public class UserSubscribeRef : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}