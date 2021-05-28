using System;
using System.Collections.Generic;

namespace ColoredLive.Core.Models
{
    public static class Roles
    {
        public const string EventOwner = "owner";
        public const string EventChecker = "checker";

        public static List<string> All { get; } = new List<string>
        {
            EventOwner,
            EventChecker
        };
    }
}