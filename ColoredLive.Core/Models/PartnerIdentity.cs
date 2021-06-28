using System.Collections;
using System.Collections.Generic;
using ColoredLive.Core.Entities;

namespace ColoredLive.Core.Models
{
    public class PartnerIdentity
    {
        public PartnerEntity User { get; set; }
        public IEnumerable<RoleEntity> Roles { get; set; }
    }
}