using System.Collections.Generic;

namespace KazanAirportWebApi.Models.JoinModels
{
    /// <summary>
    /// Полный список авиарейсов
    /// </summary>
    internal class FullFlights
    {
        public List<FlightItem> DepartureFlights { get; set; }

        public List<FlightItem> ArrivalFlights { get; set; }

        public string Metar { get; set; }
    }
}