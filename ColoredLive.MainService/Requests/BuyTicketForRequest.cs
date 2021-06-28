using System;

namespace ColoredLive.MainService.Requests
{
    public class BuyTicketForRequest
    {
        public Guid EventId { get; set; }
        public string FriendLogin { get; set; }
    }
}