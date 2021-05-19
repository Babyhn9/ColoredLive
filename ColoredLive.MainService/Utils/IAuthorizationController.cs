using ColoredLive.Core.Entities;

namespace ColoredLive.MainService.Utils
{
    public interface IAuthorizationController
    {
        UserEntity Identity { get; set; }
    }
}