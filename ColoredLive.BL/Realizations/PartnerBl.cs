using System;
using System.Collections.Generic;
using System.Linq;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.RefEntities;
using ColoredLive.DAL;

namespace ColoredLive.BL.Realizations
{
    [Buisness]
    public class PartnerBl : IPartnerBl
    {
        private readonly IRepository<PartnerEntity> _partners;
        private readonly IRepository<UserRoleRef> _rolesRef;
        private readonly IRepository<RoleEntity> _roles;

        public PartnerBl(IRepository<PartnerEntity> partners, IRepository<UserRoleRef> rolesRef, IRepository<RoleEntity> roles)
        {
            _partners = partners;
            _rolesRef = rolesRef;
            _roles = roles;
        }
        
        public PartnerEntity Authorize(PartnerEntity partnerEntity)
        {
            var partner = _partners.Add(partnerEntity);
            return partner;
        }

        public PartnerEntity Register(PartnerEntity partnerEntity) => _partners.Add(partnerEntity);
        public PartnerEntity GetPartner(Guid id) => _partners.Find(id);

        public IEnumerable<RoleEntity> GetRoles(Guid partnerId)
        {
            return _rolesRef
                .FindAll(el => el.UserId == partnerId)
                .Select(el => _roles.Find(el.RoleId));
        }
    }
}