using ColoredLive.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.Core.Entities
{
    public class RecommendationEntity : Recommendation
    {
        public byte [] Image { get; set; }
    }
}
