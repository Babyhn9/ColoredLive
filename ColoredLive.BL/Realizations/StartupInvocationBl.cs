using System.Linq;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Models;
using ColoredLive.DAL;

namespace ColoredLive.BL.Realizations
{
    [Buisness]
    public class StartupInvocationBl : IStartupInvocationBl
    {
        private readonly IRepository<RoleEntity> _roles;
        private readonly IRepository<UserEntity> _users;
        private readonly IRepository<EventEntity> _events;
        private readonly IEventBl _eventBl;
        private readonly IUserBl _userBl;

        public StartupInvocationBl(
            IRepository<RoleEntity> roles,
            IRepository<UserEntity> users,
            IRepository<EventEntity> events,
            IEventBl eventBl,
            IUserBl userBl
            
            )
        {
            _roles = roles;
            _users = users;
            _events = events;
            _eventBl = eventBl;
            _userBl = userBl;
        }
        public void Startup()
        {
            var rolesAtDb = _roles.FindAll(el => true).Select(el => el.Role);
           
            foreach (var role in Roles.All)
            {
                if (!rolesAtDb.Contains(role))
                    _roles.Add(new RoleEntity {Role = role});
            }
            
            if (_users.FindAll(el => true).Count == 0)
            {
                var defaultUser = new UserEntity
                {
                    Login = "test",
                    Password = "12345"
                };

                var userWhitOwns = new UserEntity
                {
                    Login = "test1",
                    Password = "12345"
                };

                defaultUser = _userBl.Register(defaultUser);
                userWhitOwns = _userBl.Register(userWhitOwns);


                var firstEvent = new EventEntity
                {
                    Name = "Выставка",
                    Description = "Выставка в москве"
                };
                    
                var secondEvent = new EventEntity
                {
                    Name = "картинная голерея",
                    Description = "Выставка картин в москве"
                };
                var thirdEvent = new EventEntity
                {
                    Name = "Мюзикл",
                    Description = "Мюзикл в старинном доме"
                };

                firstEvent = _eventBl.CreateEvent(userWhitOwns.Id, firstEvent);
                secondEvent = _eventBl.CreateEvent(userWhitOwns.Id, secondEvent);
                thirdEvent = _eventBl.CreateEvent(defaultUser.Id, thirdEvent);
                    
                    
                    
                _userBl.SetRole(userWhitOwns.Id, _roles.Find(el => el.Role == Roles.EventOwner).Id);
            }

        }
    }
}