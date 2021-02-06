using ColoredLive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.BL.Interfaces
{
    public interface IRecommendationEventBl
    {
        RecommendationEventEntity GetRandomRecomendation();
        RecommendationEventEntity GetRandomTopRecomendation();
        RecommendationEventEntity GetRandomRecomendationByTags(List<Guid> tags);
        List<RecommendationEventEntity> GetTopRecomendations();
        List<RecommendationTagEntity> GetTags(RecommendationEventEntity entity);
    }
}
