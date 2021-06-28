using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Models;
using ColoredLive.MainService.Requests;
using ColoredLive.Service.Core.Attributes;
using ColoredLive.Service.Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ColoredLive.MainService.Controllers
{
    [LazyUserIdentity]
    [Route("[controller]")]
    public class PartnerAuth : ControllerBase, IAuthorizationController<PartnerIdentity>
    {
        private readonly IPartnerBl _partnerBl;
        private readonly ITokenCreationBl _tokenCreationBl;
        public PartnerIdentity Identity { get; set; }

        public PartnerAuth(IPartnerBl partnerBl,ITokenCreationBl tokenCreationBl)
        {
            _partnerBl = partnerBl;
            _tokenCreationBl = tokenCreationBl;
        }

        [HttpGet("log")]
        public ActionResult<string> Authorize(AuthRequest request)
        {
           var partner = _partnerBl.Authorize(new PartnerEntity {Login = request.Login, Password = request.Password});
           return _tokenCreationBl.Generate(partner);
        }

        [HttpGet("reg")]
        public ActionResult<string> Register(CreatePartnerRequest request)
        {
            var partner = _partnerBl.Register(new PartnerEntity {Login = request.Login, Password = request.Password});
            return _tokenCreationBl.Generate(partner);
        }


        [HttpGet("info")]
        [JwtAuth]
        public ActionResult<PartnerIdentity> Info() => Identity;
    }
}