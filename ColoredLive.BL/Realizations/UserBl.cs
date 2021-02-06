using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.BL.Realizations
{
    [Buisness]
    public class UserBl : IUserBl
    {
        private IRepository<UserEntity> _users;
        private readonly IRepository<UserSettingEntity> _userSettings;

        public UserBl(IRepository<UserEntity> users, IRepository<UserSettingEntity> userSettings)
        {
            _users = users;
            _userSettings = userSettings;
        }
        public UserEntity Authorize(string login, string password) => _users.Find(el => el.Login == login && el.Password == password);

        public UserSettingEntity GetUserSettings(Guid userId) => _userSettings.Find(userId);

        public UserSettingEntity GetUserSettings(UserEntity user) => _userSettings.Find(user.Id);

        public UserEntity Register(UserEntity newUser) =>
            _users.Find(el => el.Login == newUser.Login || el.Email == newUser.Email).Id != Guid.Empty ? new UserEntity() : _users.Add(newUser);
    }
}
