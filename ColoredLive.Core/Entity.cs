using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ColoredLive.Core
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
        public bool IsEmpty => Id == Guid.Empty;
    }
}
