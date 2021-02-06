using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.Core.BaseEntities
{

    public enum RecomendationOwnerType
    {
        Person,
        Company
    }

    public abstract class Recommendation : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public RecomendationOwnerType OwnerType { get; set; }
        public string Description { get; set; }
        
    }
}
