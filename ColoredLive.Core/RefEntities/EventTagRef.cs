using System;

namespace ColoredLive.Core.RefEntities
{
    public class EventTagRef : IEntity
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid TagId { get; set; }
    }
}