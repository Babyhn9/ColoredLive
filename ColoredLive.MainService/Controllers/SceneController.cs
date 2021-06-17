using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ColoredLive.BL.Interfaces;
using ColoredLive.BL.Realizations;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Models;
using ColoredLive.DAL;
using ColoredLive.MainService.ScenarioModels;
using ColoredLive.Service.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ColoredLive.MainService.Controllers
{
    public class SceneController : ProjectControllerBase
    {
        private readonly IRepository<UserEntity> _users;
        private readonly IRepository<EventEntity> _events;
        private readonly IRepository<TicketEntity> _tickets;
        private readonly ITokenCreationBl _tokenBl;
        private readonly IUserBl _userBl;
        private readonly IEventBl _eventBl;
        private readonly IQrBl _qrBl;
        private readonly ITicketBl _ticketBl;

        public SceneController(
            IRepository<UserEntity> users,
            IRepository<EventEntity> events,
            IRepository<TicketEntity> tickets,
            ITokenCreationBl tokenBl,
            IUserBl userBl,
            IEventBl eventBl,
            IQrBl qrBl,
            ITicketBl ticketBl
        )
        {
            _users = users;
            _events = events;
            _tickets = tickets;
            _tokenBl = tokenBl;
            _userBl = userBl;
            _eventBl = eventBl;
            _qrBl = qrBl;
            _ticketBl = ticketBl;
        }
        [HttpGet("showAllUsers")]
        public ActionResult<IEnumerable<UserEntity>> GetAllUsers() => _users.FindAll(el => true);
        
        [HttpGet("showAllEvents")]
        public ActionResult<IEnumerable<EventEntity>> ShowEvents() => _events.FindAll(el => true);
        
        [HttpGet("showTickets")]
        public ActionResult<IEnumerable<TicketEntity>> ShowTickets() => _tickets.FindAll(el => true);
        
        [HttpGet("showBoughtTickets/{userId}")]
        public ActionResult<IEnumerable<TicketEntity>> ShowBoughtTickets(Guid userId) => _ticketBl.GetUserTickets(userId).ToArray();
        
        [HttpGet("showLogin")]
        public ActionResult ShowLogin(EnterScenarioRequest request)
        {
            var user = _userBl.Authorize(request.Login, request.Password);
            if (user.IsEmpty)
                return new JsonResult(new {Token = ""});

            return new JsonResult(new {Token = _tokenBl.Generate(user)});

        }
        [HttpGet("showRegister")]
        public ActionResult ShowRegister(RegisterScenarioModel request)
        {
            var user = _users.Find(el => el.Login == request.Login);
            if (user.IsEmpty)
            {
                user = _userBl.Register(new UserEntity{Login =  request.Login, Password = request.Password});
                if (user.IsEmpty)
                    return WhitText("Ошибка регистрации");
                
                return WhitText(_tokenBl.Generate(user));
            }

            return WhitText("Ошибка регистрации");
        }

     [HttpPost("createEvent")]
        public ActionResult CreateEvent(CreateEventScenarioRequest request)
        {
            
            var @event = _eventBl.CreateEvent(request.UserId, request.Event);
            if (@event.IsEmpty)
                return WhitText("При создании события произошла ошибка");
                        
            return WhitText(@event.Id.ToString());
        }
        
        [HttpGet("addEventToFavorite/{userId}/{eventId}")]
        public ActionResult AddToFavorite(Guid userId, Guid eventId)
        {
            var result = _eventBl.Subscribe(userId,eventId);
            return WhitText(result.ToString());
        }
        
        [HttpGet("getFavoriteEvents/{userId}")]
        public ActionResult<IEnumerable<EventEntity>> GetFavorite(Guid userId)
        {
            return _eventBl.GetFavoriteUserEvents(userId).ToArray();
        }
        
        [HttpGet("getCreatedEvents/{userId}")]
        public ActionResult<IEnumerable<EventEntity>> GetCreated(Guid userId)
        {
            return _eventBl.GetCreatedEvents(userId).ToArray();
        }

        [HttpGet("showRoles/{userId}")]
        public ActionResult<IEnumerable<RoleEntity>> GetRoles(Guid userId)
        {
            return _userBl.GetUserRoles(userId).ToArray();
        }
        
        [HttpGet("luck")]
        public ActionResult<EventEntity> Luck() => _eventBl.GetLuckyEvent();
        
        [HttpGet("ticket/buy/{userId}/{eventId}")]
        public ActionResult<TicketEntity> BuyTicket(Guid userId,Guid eventId)
        {
            var ticket = _ticketBl.BuyTicket(userId, eventId);
            return ticket;
        }
        [HttpGet("ticket/enter/{ticketId}")]
        public ActionResult<string> BuyTicket(Guid ticketId)
        {
            return WhitText(_ticketBl.Enter(ticketId).ToString());
        }


        [HttpGet("qr/{ticketId}")]
        public ActionResult<byte[]> GetQr(Guid ticketId) => _qrBl.GenerateQr(ticketId);
        
        private JsonResult WhitText(string text) => new JsonResult(new {Message = text});
        
        
    }
}