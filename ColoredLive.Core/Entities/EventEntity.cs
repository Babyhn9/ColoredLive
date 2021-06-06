using System;

namespace ColoredLive.Core.Entities
{
    public class EventEntity : Entity
    {
        public Guid OwnerUserId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        
        /// <summary>
        /// Дата начала продаж билета
        /// </summary>
        public DateTime StartSellingDate { get; set; }
        /// <summary>
        /// Дата прекращения продажи билета
        /// </summary>
        public DateTime EndSellingDate { get; set; }
        /// <summary>
        /// Дата Проведения события
        /// </summary>
        public DateTime PlayTime { get; set; }
        /// <summary>
        /// Дата создания события
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Цена билета
        /// </summary>
        public int Price { get; set; } 
    }
}