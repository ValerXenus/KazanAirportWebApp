using System;

namespace KazanAirportWebApp.Models.JoinModels
{
    public class FlightItem
    {
        public int id { get; set; }
        public string flightNumber { get; set; }
        public DateTime departureScheduled { get; set; }
        public DateTime arrivalScheduled { get; set; }
        public DateTime? departureActual { get; set; }
        public DateTime? arrivalActual { get; set; }
        public int timeOnBoard { get; set; }
        public int flightType { get; set; }
        public int planeId { get; set; }
        public int cityId { get; set; }
        public int statusId { get; set; }
        public string modelName { get; set; }
        public string boardNumber { get; set; }
        public int airlineId { get; set; }
        public string airlineName { get; set; }
        public string cityName { get; set; }
        public string statusName { get; set; }
    }
}