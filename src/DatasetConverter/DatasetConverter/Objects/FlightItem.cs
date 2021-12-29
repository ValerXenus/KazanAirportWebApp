using System;

namespace DatasetConverter.Objects
{
    /// <summary>
    /// Данные об авиарейсе
    /// </summary>
    internal class FlightItem
    {
        /// <summary>
        /// Наименование авиакомпании
        /// </summary>
        public string AirlineName { get; set; }

        /// <summary>
        /// Наименование города
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Дата и время по расписанию
        /// </summary>
        public DateTime ScheduledTime { get; set; }

        /// <summary>
        /// Дата и время реальное
        /// </summary>
        public DateTime ActualTime { get; set; }
    }
}
