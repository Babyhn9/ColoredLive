using System;
using System.Drawing;
using System.IO;
using System.Text;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Utils;
using ColoredLive.DAL;

using QRCoder;

namespace ColoredLive.BL.Realizations
{
    [Buisness]
    public class QrBl : IQrBl
    {
        private readonly IRepository<TicketEntity> _tickets;

        public QrBl(IRepository<TicketEntity> tickets)
        {
            _tickets = tickets;
        }
        
        public byte[] GenerateQr(Guid ticketId)
        {
            var ticket = _tickets.Find(ticketId);

            //if (!ticket.Id.Empty()) return new byte[0];
           
            var generator = new QRCodeGenerator();
            var codeData = generator.CreateQrCode(Guid.NewGuid().ToString(), QRCodeGenerator.ECCLevel.Q);
            var coderResult = new QRCode(codeData);
            var result = coderResult.GetGraphic(5);
            return ImageToByteArray(result);

        }

        private byte[] ImageToByteArray(Bitmap img)
        {
            using var stream = new MemoryStream();
            img.Save(stream,System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}