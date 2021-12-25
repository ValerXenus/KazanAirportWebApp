using System;

namespace GetFlightsService.Objects
{
    /// <summary>
    /// Объект с полной информацией об авиарейсе
    /// </summary>
    internal class FlightItem
    {
        /// <summary>
        /// Номер рейса
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Пункт назначения
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Наименование модели самолета
        /// </summary>
        public string PlaneName { get; set; }

        /// <summary>
        /// Авиакомпания
        /// </summary>
        public string Airline { get; set; }

        /// <summary>
        /// Дата и время (вылета/прибытия) по расписанию
        /// </summary>
        public DateTime ScheduledDateTime { get; set; }

        /// <summary>
        /// Дата и время (вылета/прибытия) реальное
        /// </summary>
        public DateTime ActualDateTime { get; set; }

        /// <summary>
        /// Статус рейса
        /// </summary>
        public string Status { get; set; }
    }
}
