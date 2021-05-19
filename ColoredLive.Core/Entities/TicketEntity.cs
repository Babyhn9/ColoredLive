using System;

namespace ColoredLive.Core.Entities
{
    public class TicketEntity : IEntity
    {
        public Guid Id { get; set; }
        public UserEntity Owner { get; set; }
        public EventEntity Event { get; set; }
        public bool IsEnter { get; set; }
    }
}