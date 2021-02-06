using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.BL.Realizations
{
    [Buisness]
    public class RecommendationBl : IRecommendationBl
    {
        public RecommendationEntity  GetRandomRecomendation()
        {
            throw new NotImplementedException();
        }

        public RecommendationEntity  GetRandomRecomendationByTags(List<Guid> tags)
        {
            throw new NotImplementedException();
        }

        public RecommendationEntity  GetRandomTopRecomendation()
        {
            throw new NotImplementedException();
        }

        public List<RecommendationEntity > GetTopRecomendations()
        {
            return null;
        }
    }
}
