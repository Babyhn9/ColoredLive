using ColoredLive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.BL.Interfaces
{
    public interface ITokenCreationBl
    {
        string Generate(UserEntity user);
    }
}
