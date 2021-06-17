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
        private readonly IRepository<UserEntity> _users;

        public TicketBl(IRepository<TicketEntity> tickets, IRepository<UserEntity> users)
        {
            _tickets = tickets;
            _users = users;
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

        public TicketEntity BuyTicket(string login, Guid eventId)
        {
            var user = _users.Find(el => el.Login == login);
            return user.IsEmpty ? new TicketEntity() : BuyTicket(user.Id, eventId);
        }

        public IEnumerable<TicketEntity> GetUserTickets(Guid userId)
        {
            return _tickets.FindAll(el => el.Owner == userId);
        }

        public bool Enter(Guid ticketId)
        {
            var ticket = _tickets.Find(ticketId);
            if (ticket.IsEnter || ticket.IsEmpty) return false;
           
            ticket.IsEnter = true;
            _tickets.Update(ticket);
            return true;
        }
    }
}