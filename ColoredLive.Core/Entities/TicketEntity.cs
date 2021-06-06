using System;

namespace ColoredLive.Core.Entities
{
    public class TicketEntity : Entity
    {
        public Guid Owner { get; set; }
        public Guid Event { get; set; }
        public bool IsEnter { get; set; }
    }
}