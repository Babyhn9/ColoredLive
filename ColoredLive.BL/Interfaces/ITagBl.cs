using System;
using System.Collections.Generic;
using ColoredLive.Core;
using ColoredLive.Core.Entities;

namespace ColoredLive.BL.Interfaces
{
    public interface ITagBl
    {
        IEnumerable<TagEntity> GetTags(Guid eventId);
        bool AddTag(Guid eventId, Guid tagId);
        bool RemoveTag(Guid eventId, Guid tagId);
    }
}