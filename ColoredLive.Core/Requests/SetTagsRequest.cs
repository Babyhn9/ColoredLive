using System;
using System.Collections.Generic;

namespace ColoredLive.Core.Requests
{
    public class SetTagsRequest
    {
        public Guid EventId { get; set; }
        public IEnumerable<Guid> TagsIds { get; set; }
    }
}