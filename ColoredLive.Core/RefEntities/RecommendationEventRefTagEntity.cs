using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.Core.RefEntities
{
    public class RecommendationEventRefTagEntity : IEntity
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid EventTagId { get; set; }
    }
}
