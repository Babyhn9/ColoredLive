using System;

namespace ColoredLive.Core.Entities
{
    public class TicketEntity : IEntity
    {
        public Guid Id { get; set; }
        public Guid Owner { get; set; }
        public Guid Event { get; set; }
        public bool IsEnter { get; set; }
    }
}