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
        UserSettingEntity GetUserSettings(Guid userId);
        UserSettingEntity GetUserSettings(UserEntity user);
        UserEntity GetUser(Guid userId);
    }
}
