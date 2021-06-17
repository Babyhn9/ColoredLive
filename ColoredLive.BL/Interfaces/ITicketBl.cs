using System;
using System.Collections.Generic;
using ColoredLive.Core.Entities;

namespace ColoredLive.BL.Interfaces
{
    public interface ITicketBl
    {
        TicketEntity BuyTicket(Guid userId, Guid eventId);
        TicketEntity BuyTicket(string login, Guid eventId);
        IEnumerable<TicketEntity> GetUserTickets(Guid userId);
        bool Enter(Guid ticketId);
       
    }
}