using System;

namespace KazanAirportWebApp.Models.Join_Models
{
    public class PassengerTicket
    {
        public int passengerId { get; set; }
        public string flightNumber { get; set; }
        public string ticketNumber { get; set; }
        public string cityName { get; set; }
        public DateTime departureScheduled { get; set; }
        public DateTime arrivalScheduled { get; set; }
        public string airlineName { get; set; }
    }
}