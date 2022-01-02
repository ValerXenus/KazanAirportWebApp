using System;
using GetFlightsService.Common;

namespace GetFlightsService.Objects
{
    /// <summary>
    /// Информация о рейсе из онлайн-табло
    /// </summary>
    internal class DashboardFlight
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

        /// <summary>
        /// Проверка на заполненность всех данных
        /// </summary>
        /// <returns></returns>
        public bool CheckDataValid()
        {
            var condition = ScheduledDateTime != DateTime.MinValue
                            && ActualDateTime != DateTime.MinValue
                            && !string.IsNullOrEmpty(FlightNumber)
                            && !string.IsNullOrEmpty(Destination)
                            && !string.IsNullOrEmpty(Airline);
            return condition;
        }
    }
}
