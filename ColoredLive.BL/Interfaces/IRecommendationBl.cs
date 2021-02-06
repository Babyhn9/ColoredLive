using ColoredLive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.BL.Interfaces
{
    public interface IRecommendationBl
    {
        RecommendationEntity GetRandomRecomendation();
        RecommendationEntity GetRandomTopRecomendation();
        RecommendationEntity GetRandomRecomendationByTags(List<Guid> tags);
        List<RecommendationEntity> GetTopRecomendations();
    }
}
