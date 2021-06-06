using System;

namespace ColoredLive.Core.Utils
{
    public static class Extensions
    {
        public static bool Empty(this Guid id) => id.Equals(Guid.Empty);
        public static bool Empty(this Entity entity) => entity.Id.Empty();
    }
}