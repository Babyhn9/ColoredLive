using System;

namespace ColoredLive.Core.Entities
{
    public class TagEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}