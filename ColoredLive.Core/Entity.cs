using System;
using System.Text.Json.Serialization;

namespace ColoredLive.Core
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public bool IsEmpty => Id == Guid.Empty;
    }
}
