using ColoredLive.Core.Requests;
using ColoredLive.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ColoredLive.MainService.Controllers
{
    public class TicketsController : ProjectControllerBase
    {
        [HttpGet("buy")]
        public BaseResponse BuyTicketOn(BuyTicketRequest request)
        {
            
            return BaseResponse.Ok();
        }
        
    }
}