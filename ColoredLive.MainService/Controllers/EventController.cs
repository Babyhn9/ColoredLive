using System;
using System.Collections.Generic;
using System.Linq;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Requests;
using ColoredLive.Core.Responses;
using ColoredLive.DAL;
using ColoredLive.MainService.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColoredLive.MainService.Controllers
{
    public class EventController : ProjectControllerBase
    {
       private readonly IEventBl _eventBl;
       private readonly ITagBl _tagBl;
       private readonly IRepository<EventEntity> _events;

       public EventController( IEventBl eventBl, ITagBl tagBl, IRepository<EventEntity> events)
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
        public BaseResponse AddNewEvent(CreateEventRequest request)
        {
            if (_eventBl.CanCreateEvent(Identity.Id))
            {
                var newEvent = new EventEntity
                {
                    EndSellingDate =  request.EndSellingDate,
                    PlayTime = request.PlayTime,
                    StartSellingDate = request.StartSellingDate,
                    Name = request.Name,
                    Description = request.Description
                };
                var createdEvent =  _eventBl.CreateEvent(Identity.Id, newEvent);
                return createdEvent.Id != Guid.Empty ? BaseResponse.Ok() : BaseResponse.Error(StatusCodes.Status500InternalServerError, "При создании события произошла ошибка");
            }
            
            return BaseResponse.Error(StatusCodes.Status400BadRequest, "Не возможно создать новое событие");
            
        }

        public BaseResponse SetTags(SetTagsRequest request)
        {

            var tags = _tagBl.GetTags(request.EventId).Select(el => el.Id);
            var allowedTags = request.TagsIds.Where(el => !tags.Contains(el));
            
            foreach (var tagId in allowedTags)  
                _tagBl.AddTag(request.EventId, tagId);
            
            return BaseResponse.Ok();
        }
        
        /// <summary>
        /// Добавляет текущее событие пользователю в список избранного
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("sub")]
        public BaseResponse Subscribe(SubscribeRequest request) =>
            _eventBl.Subscribe(request.EventId, Identity.Id) ? BaseResponse.Ok() : BaseResponse.Error(StatusCodes.Status400BadRequest,"Вы уже подписаны на это событие");

        /// <summary>
        /// Возвращает список ивентов, используя пагинацию
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public BaseResponse GetEvents(GetEventsRequest request)
        {
            return BaseResponse.Ok(_eventBl.GetActualEvents(request.PageSize, request.CurrentSize));
        }

        /// <summary>
        /// Возвращает список избранных пользователем событий
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/my")]
        public BaseResponse GetFavoriteEvents()
        {
           return  BaseResponse.Ok(_eventBl.GetFavoriteUserEvents(Identity.Id));
        }

        [HttpGet("get/all")]
        public ActionResult<IEnumerable<EventEntity>> All()
        {
            return _events.FindAll(item => true);
        }
        
    }   
}