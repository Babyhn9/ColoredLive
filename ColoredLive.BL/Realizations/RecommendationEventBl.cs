using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.BL.Realizations
{
    [Buisness]
    public class RecommendationEventBl : IRecommendationEventBl
    {
        private readonly IRepository<RecommendationEventEntity> _events;
        private readonly IRepository<RecommendationEventTagEntity> _tags;

        public RecommendationEventBl(IRepository<RecommendationEventEntity> events, IRepository<RecommendationEventTagEntity> tags)
        {
            _events = events;
            _tags = tags;
        }
        public RecommendationEventEntity GetRandomRecomendation()
        {
            var events = _events.FindAll(el => true);
            var nextId = new Random().Next(0,events.Count);
            return events[nextId];
        }

        public RecommendationEventEntity GetRandomRecomendationByTags(List<Guid> tags)
        {
            return null;
            //var tagsForReccomendation = 
            //var tags = _tags.FindAll
        }

        public RecommendationEventEntity GetRandomTopRecomendation()
        {
            return null;
        }

        public List<RecommendationTagEntity> GetTags(RecommendationEventEntity entity)
        {
            return null;
            
        }

        public List<RecommendationEventEntity> GetTopRecomendations()
        {
            return null;
        }

    }
}
