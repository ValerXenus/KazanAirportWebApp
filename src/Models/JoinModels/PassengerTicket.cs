using System;

namespace KazanAirportWebApp.Models.JoinModels
{
    /// <summary>
    /// Билет пассажира
    /// </summary>
    public class PassengerTicket
    {
        /// <summary>
        /// ID пассажира
        /// </summary>
        public int PassengerId { get; set; }

        /// <summary>
        /// Номер рейса
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Номер билета
        /// </summary>
        public string TicketNumber { get; set; }

        /// <summary>
        /// Город назначения
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Дата и время вылета по расписанию
        /// </summary>
        public DateTime DepartureScheduled { get; set; }

        /// <summary>
        /// Дата и время прибытия по расписанию
        /// </summary>
        public DateTime ArrivalScheduled { get; set; }

        /// <summary>
        /// Наименование авиакомпании
        /// </summary>
        public string AirlineName { get; set; }
    }
}