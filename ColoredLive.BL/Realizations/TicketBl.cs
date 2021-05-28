using System;
using System.Collections.Generic;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Utils;
using ColoredLive.DAL;

namespace ColoredLive.BL.Realizations
{
    [Buisness]
    public class TicketBl : ITicketBl 
    {
        private readonly IRepository<TicketEntity> _tickets;

        public TicketBl(IRepository<TicketEntity> tickets)
        {
            _tickets = tickets;
        }
        
        public TicketEntity BuyTicket(Guid userId, Guid eventId)
        {
            
            var newTicket = new TicketEntity
            {
                Owner = userId,
                Event = eventId,
                IsEnter = false
            };

            return _tickets.Add(newTicket);

        }

        public IEnumerable<TicketEntity> GetUserTickets(Guid userId)
        {
            return _tickets.FindAll(el => el.Owner == userId);
        }

        public bool Enter(Guid ticketId, Guid userid)
        {
            var ticket = _tickets.Find(el => el.Owner == userid);
            if (ticket.IsEnter || ticket.Id.Empty()) return false;
           
            ticket.IsEnter = true;
            _tickets.Update(ticket);
            return true;
        }
    }
}