using ColoredLive.Core.Entities;
using ColoredLive.Core.Models;

namespace ColoredLive.MainService.Utils
{
    public interface IAuthorizationController
    {
        Identity Identity { get; set; }
    }
}