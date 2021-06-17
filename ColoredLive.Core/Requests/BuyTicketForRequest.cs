using System;

namespace ColoredLive.Core.Requests
{
    public class BuyTicketForRequest
    {
        public Guid EventId { get; set; }
        public string FriendLogin { get; set; }
    }
}