using System;

namespace KazanAirportWebApp.Models.JoinModels
{
    public class FlightItem
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureScheduled { get; set; }
        public DateTime ArrivalScheduled { get; set; }
        public DateTime? DepartureActual { get; set; }
        public DateTime? ArrivalActual { get; set; }
        public int TimeOnBoard { get; set; }
        public int FlightType { get; set; }
        public int PlaneId { get; set; }
        public int CityId { get; set; }
        public int StatusId { get; set; }
        public string ModelName { get; set; }
        public string BoardNumber { get; set; }
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public string CityName { get; set; }
        public string StatusName { get; set; }
    }
}