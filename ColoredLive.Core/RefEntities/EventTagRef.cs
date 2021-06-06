using System;
using ColoredLive.Core.Utils;

namespace ColoredLive.Core.RefEntities
{
    public class EventTagRef : Entity
    {
        public Guid EventId { get; set; }
        public Guid TagId { get; set; }
    }
}