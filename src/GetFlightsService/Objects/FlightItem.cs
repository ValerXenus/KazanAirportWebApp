using System;

namespace GetFlightsService.Objects
{
    /// <summary>
    /// Объект с полной информацией об авиарейсе
    /// </summary>
    internal class FlightItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер рейса
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Наименование город
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Наименование модели самолета
        /// </summary>
        public string PlaneName { get; set; }

        /// <summary>
        /// Авиакомпания
        /// </summary>
        public string AirlineName { get; set; }

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
        public string StatusName { get; set; }
    }
}
