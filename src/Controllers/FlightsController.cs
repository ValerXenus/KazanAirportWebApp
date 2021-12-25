using System;
using System.Collections.Generic;
using System.Web.Http;
using KazanAirportWebApp.Logic;
using KazanAirportWebApp.Models.JoinModels;

namespace KazanAirportWebApp.Controllers
{
    public class FlightsController : ApiController
    {
        /// <summary>
        /// Получение списка прилетающих рейсов
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetDepartureFlightsList")]
        public List<FlightItem> GetDepartureFlightsList()
        {
            var flightsReader = new FlightsFileReader();
            return flightsReader.GetDepartureFlights();
        }

        /// <summary>
        /// Получение списка прилетающих рейсов
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetArrivalFlightsList")]
        public List<FlightItem> GetArrivalFlightsList()
        {
            var flightsReader = new FlightsFileReader();
            return flightsReader.GetArrivalFlights();
        }

        /// <summary>
        /// Получение рейса по Id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetFlightById")]
        public FlightItem GetFlightById(int flightId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Сохранить рейс в БД
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveFlight")]
        public string SaveFlight(int flightId)
        {
            throw new NotImplementedException();
        }
    }
}