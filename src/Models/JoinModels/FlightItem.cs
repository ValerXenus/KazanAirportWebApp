using System;

namespace KazanAirportWebApp.Models.JoinModels
{
    /// <summary>
    /// Информация о рейсе
    /// </summary>
    public class FlightItem
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
        /// Время отправления по расписанию
        /// </summary>
        public DateTime DepartureScheduled { get; set; }
        
        /// <summary>
        /// Время прибытия по расписанию
        /// </summary>
        public DateTime ArrivalScheduled { get; set; }

        /// <summary>
        /// Время отправления фактическое
        /// </summary>
        public DateTime? DepartureActual { get; set; }

        /// <summary>
        /// Время прибытия фактическое
        /// </summary>
        public DateTime? ArrivalActual { get; set; }

        /// <summary>
        /// Время в пути (в минутах)
        /// </summary>
        public int TimeOnBoard { get; set; }

        /// <summary>
        /// Тип перелета (0 - Вылет, 1 - Прилет)
        /// </summary>
        public bool FlightType { get; set; }

        /// <summary>
        /// ID самолета
        /// </summary>
        public int PlaneId { get; set; }

        /// <summary>
        /// ID города
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Статус полета
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Наименование модели самолета
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Бортовой номер самолета
        /// </summary>
        public string BoardNumber { get; set; }

        /// <summary>
        /// ID авиакомпании
        /// </summary>
        public int AirlineId { get; set; }

        /// <summary>
        /// Наименование авиакомпании
        /// </summary>
        public string AirlineName { get; set; }

        /// <summary>
        /// Наименование города
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Наименование статус полета
        /// </summary>
        public string StatusName { get; set; }
    }
}