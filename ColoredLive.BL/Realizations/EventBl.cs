using System;
using System.Collections.Generic;
using System.Linq;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Models;
using ColoredLive.Core.RefEntities;
using ColoredLive.DAL;

namespace ColoredLive.BL.Realizations
{
    [Buisness]
    public class EventBl : IEventBl
    {
        private readonly IRepository<EventEntity> _events;
        private readonly IRepository<UserSubscribeRef> _subscribers;
        private readonly IRepository<EventTagRef> _taggedEvents;
        private readonly IUserBl _userBl;

        public EventBl(
            IRepository<EventEntity> events,
            IRepository<UserSubscribeRef> subscribers,
            IRepository<EventTagRef> taggedEvents,
            IUserBl userBl
            )
        {
            _events = events;
            _subscribers = subscribers;
            _taggedEvents = taggedEvents;
            _userBl = userBl;
        }

        public IEnumerable<EventEntity> GetActualEvents(int size, int current)
        {
            var actualDateTime = DateTime.Now.AddMinutes(30).Ticks;
            return _events
                .FindAll(el => el.StartSellingDate.Ticks > actualDateTime)
                .Skip(size * (current-1))
                .Take(size);
        }

        public IEnumerable<EventEntity> GetFavoriteUserEvents(Guid userId)
        {
            var subscribedEvents = _subscribers.FindAll(el => el.UserId == userId)
                .Select(el => _events.Find(el.EventId));
            return subscribedEvents;
        }

        public IEnumerable<EventEntity> GetCreatedEvents(Guid userId)
        {
            return _events.FindAll(el => el.OwnerUserId == userId);
        }

        public bool Subscribe(Guid user, Guid @event)
        {
            var isSubscribed = _subscribers
                .Find(el => el.EventId == @event && el.UserId == user).Id == Guid.Empty;


            if (isSubscribed)
                return false;
            
            _subscribers.Add(new UserSubscribeRef {UserId = user, EventId = @event});
            return true;
        }

        public bool Subscribe( UserEntity user,EventEntity @event)
        {
            return Subscribe(@event.Id, user.Id);
        }

        public EventEntity CreateEvent(Guid userId, EventEntity @event)
        {
            @event.OwnerUserId = userId;
            
            _userBl.SetRole(userId, Roles.EventOwner);
            
            return _events.Add(@event);
        }

        public bool CanCreateEvent(Guid userId)
        {
            return true;
        }

     
        public IEnumerable<EventEntity> GetEventsByTag(Guid tagId)
        {
            var actualDateTime = DateTime.Now.AddMinutes(30);
            return _taggedEvents
                .FindAll(el => el.TagId == tagId) // находим id-шки всех событий с таким тегом 
                .Select(el => _events.Find(el.EventId)) // конвертим id-Шки в полноценные модели
                .Where(el => el.StartSellingDate.Ticks > actualDateTime.Ticks);
        }

        public EventEntity GetLuckyEvent()
        {
            var totalCount = _events.Count();
            var random = new Random().Next(0, totalCount);
            return _events.FindAll(el => true)[random];
        }
    }
}