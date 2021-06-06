using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ColoredLive.Core.RefEntities;
using ColoredLive.Core.Utils;

namespace ColoredLive.BL.Realizations
{
    [Buisness]
    public class UserBl : IUserBl
    {
        private IRepository<UserEntity> _users;
        private readonly IRepository<UserRoleRef> _userRoles;
        private readonly IRepository<RoleEntity> _roles;

        public UserBl(IRepository<UserEntity> users, IRepository<UserRoleRef> userRoles, IRepository<RoleEntity> roles)
        {
            _users = users;
            _userRoles = userRoles;
            _roles = roles;
        }
        public UserEntity Authorize(string login, string password)
        {
            try
            {
                var founded = _users.Find(el => el.Login == login);
                if (founded.Id == Guid.Empty)
                    return founded;

                if (founded.Password == password)
                    return founded;
                
                return new UserEntity();
            }
            catch (Exception ex)
            {
                return new UserEntity();
            }
        }
     
        public UserEntity Register(UserEntity newUser)
        {
            var founded = _users.Find(el => el.Login == newUser.Login);
            
            if (!founded.Id.Empty())
                return founded;
            
            return _users.Add(newUser);

        }
        public UserEntity GetUser(Guid userId) => _users.Find(userId);
        public IEnumerable<RoleEntity> GetUserRoles(Guid userId)
        {
            return _userRoles
                .FindAll(el => el.UserId == userId)
                .Select(el => _roles.Find(el.RoleId));
        }

        public bool SetRole(Guid userId, Guid roleId)
        {
            var role = _roles.Find(roleId);
            if (role.Id.Empty()) return false; // если роли с таким id нет
            
            var attachedRole = _userRoles.Find(el => el.RoleId == roleId && el.UserId == userId);
            if (!attachedRole.Id.Empty()) return false; // если роль уже назначенна

            _userRoles.Add(new UserRoleRef {UserId = userId, RoleId = roleId});
            
            return true;
        }

        public bool SetRole(Guid userId, string role)
        {
            var foundedRole = _roles.Find(el => el.Role == role);
            if (foundedRole.Id.Empty()) return false; // если роли с таким id нет
            
            var attachedRole = _userRoles.Find(el => el.RoleId == foundedRole.Id && el.UserId == userId);
            if (!attachedRole.Id.Empty()) return false; // если роль уже назначенна

            _userRoles.Add(new UserRoleRef {UserId = userId, RoleId = foundedRole.Id});
            return true;
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        private bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        private bool ByteArraysEqual(byte[] buffer3, byte[] buffer4)
        {
            if (buffer3.Length != buffer4.Length)
                return false;

            for (var i = 0; i < buffer3.Length; i++)
                if (buffer3[i] != buffer4[i])
                    return false;

            return true;
        }
    }
}
