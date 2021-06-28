using System;
using System.Collections;
using System.Collections.Generic;
using ColoredLive.Core.Entities;

namespace ColoredLive.BL.Interfaces
{
    public interface IPartnerBl
    {
        PartnerEntity Authorize(PartnerEntity partnerEntity);
        PartnerEntity Register(PartnerEntity partnerEntity);
        PartnerEntity GetPartner(Guid id);
        IEnumerable<RoleEntity> GetRoles(Guid id);
    }
}