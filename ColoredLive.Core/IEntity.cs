using System;
using System.Text.Json.Serialization;

namespace ColoredLive.Core
{
    public interface IEntity
    {
        Guid Id { get; }
        [JsonIgnore]
        bool IsEmpty { get;  }
        
    }
}