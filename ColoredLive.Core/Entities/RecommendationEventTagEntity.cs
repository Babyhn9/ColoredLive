using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.Core.Entities
{
    public class RecommendationEventTagEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
