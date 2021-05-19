using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.Core.Responses
{
    public class AuthResponse
    {
        public string Token { get; }
        public AuthResponse(string token)
        {
            Token = token;
        }
    }
}
