using System.Collections.Generic;
using System.Linq;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.MainService.Requests;
using ColoredLive.Service.Core;
using Microsoft.AspNetCore.Mvc;

namespace ColoredLive.MainService.Controllers
{
    public class TicketsController : ProjectControllerBase
    {
        private readonly ITicketBl _ticketBl;

        public TicketsController(ITicketBl ticketBl)
        {
            _ticketBl = ticketBl;
        }
        
        [HttpGet("buy")]
        public ActionResult BuyTicketOn(BuyTicketRequest request)
        {
            _ticketBl.BuyTicket(Identity.User.Id, request.EventId);
            return Ok();
        }

        [HttpGet("buy/for")]
        public ActionResult BuyTicketFor(BuyTicketForRequest request)
        {
            var ticket = _ticketBl.BuyTicket(request.FriendLogin, request.EventId);
            if (ticket.IsEmpty)
                return new BadRequestResult();
            return Ok();
        }

        [HttpGet("get")]
        public ActionResult<IEnumerable<TicketEntity>> GetUserTickets()
        {
            var result =_ticketBl.GetUserTickets(Identity.User.Id);
            return result.ToArray();
        }
        
        [HttpGet("enter")]
        public ActionResult<bool> Enter(EnterRequest request)
        {
            return _ticketBl.Enter(request.TicketId);
        }
        
    }
}