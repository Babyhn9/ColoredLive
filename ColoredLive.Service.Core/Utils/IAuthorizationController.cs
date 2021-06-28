using ColoredLive.Core.Entities;
using ColoredLive.Core.Models;

namespace ColoredLive.Service.Core.Utils
{
    public interface IAuthorizationController<T>
    {
        T Identity { get; set; }
    }
}