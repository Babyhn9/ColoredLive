using ColoredLive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.BL.Interfaces
{
    public interface IUserBl
    {
        UserEntity Register(UserEntity newUser);
        UserEntity Authorize(string login, string password);
        UserEntity GetUser(Guid userId);

        
        IEnumerable<RoleEntity> GetRoles(Guid userId);
        bool SetRole(Guid userId, Guid roleId);
        bool SetRole(Guid userId, string role);

        
    }
}
