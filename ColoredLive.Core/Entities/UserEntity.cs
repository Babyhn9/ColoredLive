using System;

namespace ColoredLive.Core.Entities
{
    public class UserEntity : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public  string Email { get; set; }
    }
}