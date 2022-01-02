using System;
using System.Collections.Generic;
using System.Linq;
using KazanAirportWebApi.DataAccess;
using KazanAirportWebApi.Logic;
using KazanAirportWebApi.Models;
using KazanAirportWebApi.Models.JoinModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace KazanAirportWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FlightsController : ControllerBase
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly KazanAirportDbContext _db;

        public FlightsController(IConfiguration config, KazanAirportDbContext db)
        {
            _config = config;
            _db = db;
        }

        /// <summary>
        /// Получение списка прилетающих рейсов
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetDepartureFlightsList")]
        public List<FlightItem> GetDepartureFlightsList()
        {
            return FlightsFileReader.Instance(_config).GetDepartureFlights();
        }

        /// <summary>
        /// Получение списка прилетающих рейсов
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetArrivalFlightsList")]
        public List<FlightItem> GetArrivalFlightsList()
        {
            return FlightsFileReader.Instance(_config).GetArrivalFlights();
        }

        /// <summary>
        /// Сохранить рейс в БД
        /// <param name="flightId">ID рейса в файле</param>
        /// <param name="directionType">Тип авиарейса, 1 - Вылет, 2 - Прилет</param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveFlight")]
        public string SaveFlight(int flightId, int directionType)
        {
            try
            {
                var flight = FlightsFileReader.Instance(_config).GetFlightById(flightId, directionType);
                if (!checkFlightIsNotInDatabase(flight.FlightNumber, flight.ScheduledDateTime))
                    return "Данный рейс уже был сохранен";

                var cityId = addOrGetCityId(flight.CityName);
                var airlineId = addOrGetAirlineId(flight.AirlineName);
                var planeId = addOrGetPlaneId(flight.PlaneName, airlineId);

                _db.Flights.Add(new DbFlight
                {
                    FlightNumber = flight.FlightNumber,
                    CityId = cityId,
                    PlaneId = planeId,
                    ScheduledDateTime = flight.ScheduledDateTime,
                    ActualDateTime = flight.ActualDateTime,
                    StatusName = flight.StatusName
                });
                _db.SaveChanges();
            }
            catch (Exception exception)
            {
                return exception.Message;
            }

            return "Success";
        }

        #region Private methods

        /// <summary>
        /// Получить или добавить и получить Id города
        /// </summary>
        /// <param name="cityName">Наименование города</param>
        /// <returns></returns>
        private int addOrGetCityId(string cityName)
        {
            var city = _db.Cities.FirstOrDefault(x => x.Name == cityName);
            if (city != null)
                return city.Id;

            city = new DbCity
            {
                Name = cityName
            };
            _db.Cities.Add(city);
            _db.SaveChanges();

            return city.Id;
        }

        /// <summary>
        /// Получить или добавить и получить Id авиакомпании
        /// </summary>
        /// <param name="airlineName">Наименование авиакомпании</param>
        /// <returns></returns>
        private int addOrGetAirlineId(string airlineName)
        {
            var airline = _db.Airlines.FirstOrDefault(x => x.Name == airlineName);
            if (airline != null)
                return airline.Id;

            airline = new DbAirline
            {
                Name = airlineName
            };
            _db.Airlines.Add(airline);
            _db.SaveChanges();

            return airline.Id;
        }

        /// <summary>
        /// Получить или добавить и получить Id самолета
        /// </summary>
        /// <param name="planeName">Наименование самолета</param>
        /// <param name="airlineId">ID авиакомпанииа</param>
        /// <returns></returns>
        private int addOrGetPlaneId(string planeName, int airlineId)
        {
            var plane = _db.Planes.FirstOrDefault(x => x.Name == planeName && x.AirlineId == airlineId);
            if (plane != null)
                return plane.Id;

            plane = new DbPlane
            {
                Name = planeName,
                AirlineId = airlineId
            };
            _db.Planes.Add(plane);
            _db.SaveChanges();

            return plane.Id;
        }

        /// <summary>
        /// Проверка на наличие рейса в БД
        /// </summary>
        /// <param name="flightNumber">Номер рейса</param>
        /// <param name="scheduleDateTime">Дата и время по расписанию</param>
        /// <returns></returns>
        private bool checkFlightIsNotInDatabase(string flightNumber, DateTime scheduleDateTime)
        {
            var flight = _db.Flights.FirstOrDefault(x =>
                x.FlightNumber == flightNumber && x.ScheduledDateTime == scheduleDateTime);
            return flight == null;
        }

        #endregion
    }
}