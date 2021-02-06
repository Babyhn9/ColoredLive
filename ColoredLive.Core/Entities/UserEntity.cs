using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.Core.Entities
{
    public class UserEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
