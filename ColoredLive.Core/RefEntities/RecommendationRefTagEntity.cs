using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.Core.RefEntities
{
    public class RecommendationRefTagEntity : IEntity
    {
        public Guid Id { get; set; }
        public Guid RecommendationId { get; set; }
        public Guid RecommendationTagId { get; set; }
    }
}
