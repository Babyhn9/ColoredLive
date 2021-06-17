using System;
using ColoredLive.Core.Entities;

namespace ColoredLive.MainService.ScenarioModels
{
    public class CreateEventScenarioRequest
    {
        public Guid UserId { get; set; }
        public EventEntity Event { get; set; }
    }
}