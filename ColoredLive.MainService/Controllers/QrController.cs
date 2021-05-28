using System.Drawing;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Requests;
using ColoredLive.Core.Responses;
using ColoredLive.MainService.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ColoredLive.MainService.Controllers
{
    public class QrController : ProjectControllerBase
    {
        private readonly IQrBl _qrBl;

        public QrController(IQrBl qrBl)
        {
            _qrBl = qrBl;
        }
        [HttpGet("get")]
        public ActionResult<byte[]> GetQrCode(QrRequest request)
        {
            return _qrBl.GenerateQr(request.TicketId);
        }
    }
}