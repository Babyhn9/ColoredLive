using System;

namespace ColoredLive.Core.Requests
{
    public class CreateEventRequest
    {
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
        public DateTime StartTime { get; set; }
    }
}