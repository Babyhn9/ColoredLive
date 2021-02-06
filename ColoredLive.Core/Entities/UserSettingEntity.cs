using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.Core.Entities
{
    public class UserSettingEntity : IEntity
    {
        public Guid Id { get; set; }
        public bool WouldAddInGroups { get; set; }
    }
}
