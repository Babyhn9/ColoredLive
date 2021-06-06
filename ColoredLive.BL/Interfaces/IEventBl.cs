using System;
using System.Collections;
using System.Collections.Generic;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Models.Filter;

namespace ColoredLive.BL.Interfaces
{
    public interface IEventBl
    {
        ///возвращает список всех актуальных событий
        IEnumerable<EventEntity> GetActualEvents(int size, int current);
        ///возвращает список всех событий, на которые подписан пользователь
        IEnumerable<EventEntity> GetFavoriteUserEvents(Guid userId);
            /// <summary>
            /// Возвращает список созданных предметов
            /// </summary>
            /// <param name="userId"></param>
            /// <returns></returns>
        IEnumerable<EventEntity> GetCreatedEvents(Guid userId);
        ///подписывает пользователя на событие
        bool Subscribe(Guid user, Guid @event);
        ///подписывает пользователя на событие
        bool Subscribe(UserEntity user, EventEntity @event);

        EventEntity CreateEvent(Guid userId,EventEntity @event);
        bool CanCreateEvent(Guid userId);

        /// <summary>
        /// Возвращает список актуальных событий соответствующих тегу
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        IEnumerable<EventEntity> GetEventsByTag(Guid tagId);
    }
}