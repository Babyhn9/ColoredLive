using System;
using System.Collections.Generic;
using System.Linq;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.RefEntities;
using ColoredLive.Core.Utils;
using ColoredLive.DAL;

namespace ColoredLive.BL.Realizations
{
    [Buisness]
    public class TagBl : ITagBl
    {
        private readonly IRepository<TagEntity> _tags;
        private readonly IRepository<EventTagRef> _taggedEvents;

        public TagBl(IRepository<TagEntity> tags, IRepository<EventTagRef> taggedEvents)
        {
            _tags = tags;
            _taggedEvents = taggedEvents;
        }
        public IEnumerable<TagEntity> GetTags(Guid eventId)
        {
            return 
                _taggedEvents.FindAll(el => el.EventId == eventId)
                    .Select(el => el.TagId)
                    .Select(el => _tags.Find(el))
                    .ToArray();

        }

        public bool AddTag(Guid eventId, Guid tagId)
        {
            var seatedTag = _taggedEvents.Find(el => el.EventId == eventId && el.TagId == tagId);
            
            if (!seatedTag.IsEmpty) return true;

            var newTaggedEvent = new EventTagRef
            {
                EventId = eventId,
                TagId = tagId
            };

            _taggedEvents.Add(newTaggedEvent);
            return true;

        }

        public bool RemoveTag(Guid eventId, Guid tagId)
        {
            var foundedEvent = _taggedEvents.Find(el => el.EventId == eventId && el.TagId == tagId);
            if (foundedEvent.IsEmpty) return false;
            
            _taggedEvents.Remove(foundedEvent.Id);
            return true;
        }
    }
}