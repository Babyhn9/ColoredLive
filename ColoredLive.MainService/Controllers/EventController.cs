using System;
using System.Collections.Generic;
using System.Linq;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Models;
using ColoredLive.Core.Requests;
using ColoredLive.DAL;
using ColoredLive.Service.Core;
using ColoredLive.Service.Core.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColoredLive.MainService.Controllers
{
    [JwtAuth]
    public class EventController : ProjectControllerBase
    {
       private readonly IEventBl _eventBl;
       private readonly ITagBl _tagBl;
       private readonly IRepository<EventEntity> _events;

       public EventController(IEventBl eventBl, ITagBl tagBl, IRepository<EventEntity> events)
       {
           _eventBl = eventBl;
           _tagBl = tagBl;
           _events = events;
       }

        /// <summary>
        /// Создаёт новое событие
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public ActionResult AddNewEvent(CreateEventRequest request)
        {
            if (_eventBl.CanCreateEvent(Identity.User.Id))
            {
                var newEvent = new EventEntity
                {
                    EndSellingDate =  request.EndSellingDate,
                    PlayTime = request.StartTime,
                    StartSellingDate = request.StartSellingDate,
                    Name = request.Name,
                    Description = request.Description
                };
                var createdEvent =  _eventBl.CreateEvent(Identity.User.Id, newEvent);
                return createdEvent.Id != Guid.Empty ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("tags/set")]
        public ActionResult SetTags(SetTagsRequest request)
        {

            var tags = _tagBl.GetTags(request.EventId).Select(el => el.Id);
            var allowedTags = request.TagsIds.Where(el => !tags.Contains(el));
            
            foreach (var tagId in allowedTags)  
                _tagBl.AddTag(request.EventId, tagId);

            return Ok();
        }
        
        /// <summary>
        /// Добавляет текущее событие пользователю в список избранного
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("sub")]
        public ActionResult Subscribe(SubscribeRequest request) =>
            _eventBl.Subscribe(request.EventId, Identity.User.Id) ? (ActionResult) Ok(): BadRequest();

        /// <summary>
        /// Возвращает список ивентов, используя пагинацию
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public ActionResult<IEnumerable<EventEntity>> GetEvents(GetEventsRequest request)
        {
            return Ok(_eventBl.GetActualEvents(request.PageSize, request.CurrentPage));
        }

        /// <summary>
        /// Возвращает список избранных пользователем событий
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/my")]
        public ActionResult<IEnumerable<EventEntity>> GetFavoriteEvents()
        {
           return  Ok(_eventBl.GetFavoriteUserEvents(Identity.User.Id));
        }
        [HttpGet("get/own")]
        [RequireRole(Roles.EventOwner)]
        public ActionResult<IEnumerable<EventEntity>> GetCreatedEvents()
        {
            return  Ok(_eventBl.GetCreatedEvents(Identity.User.Id));
        }

        [HttpGet("luck")]
        public ActionResult<EventEntity> GetRandomEvent() => _eventBl.GetLuckyEvent();
    }   
}