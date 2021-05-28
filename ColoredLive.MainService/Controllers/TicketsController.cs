using System.Collections.Generic;
using System.Linq;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Requests;
using ColoredLive.Core.Responses;
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

        [HttpGet("get")]
        public ActionResult<IEnumerable<TicketEntity>> GetUserTickets()
        {
            var result =_ticketBl.GetUserTickets(Identity.User.Id);
            return result.ToArray();
        }
        
        [HttpGet("enter")]
        public ActionResult<bool> Enter(EnterRequest request)
        {
            return _ticketBl.Enter(request.TicketId, request.TicketId);
        }
        
    }
}