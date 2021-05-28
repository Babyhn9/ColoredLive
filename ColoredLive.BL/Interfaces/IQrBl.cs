using System;
using System.Drawing;
using ColoredLive.Core.Entities;

namespace ColoredLive.BL.Interfaces
{
    public interface IQrBl
    {
        byte[] GenerateQr(Guid ticketId);
    }
}