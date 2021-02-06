using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.BL.Realizations
{
    [Buisness]
    public class TokenCreationBl : ITokenCreationBl
    {
        public string Generate(UserEntity user)
        {
            return $"{user.Id}:{user.Login}:{user.Password}:{user.Email}";
        }
    }
}
