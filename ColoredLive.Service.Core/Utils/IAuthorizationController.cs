using ColoredLive.Core.Entities;
using ColoredLive.Core.Models;

namespace ColoredLive.Service.Core.Utils
{
    public interface IAuthorizationController
    {
        Identity Identity { get; set; }
    }
}