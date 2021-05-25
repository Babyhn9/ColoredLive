using ColoredLive.Core.Requests;
using ColoredLive.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ColoredLive.MainService.Controllers
{
    public class TicketsController : ProjectControllerBase
    {
        [HttpGet("buy")]
        public ActionResult BuyTicketOn(BuyTicketRequest request)
        {

            return Ok();
        }
        
    }
}