using ColoredLive.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.Core.Entities
{
    public class RecommendationEventEntity : Recommendation
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate{ get; set; }
    }
}
