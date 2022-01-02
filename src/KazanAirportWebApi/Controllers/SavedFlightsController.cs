using System;
using System.Collections.Generic;
using System.Linq;
using KazanAirportWebApi.DataAccess;
using KazanAirportWebApi.Models.JoinModels;
using Microsoft.AspNetCore.Mvc;

namespace KazanAirportWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SavedFlightsController : ControllerBase
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly KazanAirportDbContext _db;

        public SavedFlightsController(KazanAirportDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Получение списка прилетающих рейсов
        /// </summary>
        /// <param name="todayOnly">Выбирать только сегодняшние</param>>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetDepartureFlightsList")]
        public List<FlightItem> GetDepartureFlightsList(bool todayOnly = false)
        {
            List<FlightItem> savedFlights;

            if (todayOnly)
            {
                savedFlights = (from f in _db.Flights
                    where f.ScheduledDateTime > DateTime.Now
                    join p in _db.Planes on f.PlaneId equals p.Id
                    join a in _db.Airlines on p.AirlineId equals a.Id
                    join c in _db.Cities on f.CityId equals c.Id
                    select new FlightItem
                    {
                        Id = f.Id,
                        FlightNumber = f.FlightNumber,
                        PlaneName = p.Name,
                        AirlineName = a.Name,
                        CityName = c.Name,
                        ScheduledDateTime = f.ScheduledDateTime,
                        ActualDateTime = f.ActualDateTime ?? f.ScheduledDateTime,
                        StatusName = f.StatusName
                    }).ToList();
            }
            else
            {
                savedFlights = (from f in _db.Flights
                    join p in _db.Planes on f.PlaneId equals p.Id
                    join a in _db.Airlines on p.AirlineId equals a.Id
                    join c in _db.Cities on f.CityId equals c.Id
                    select new FlightItem
                    {
                        Id = f.Id,
                        FlightNumber = f.FlightNumber,
                        PlaneName = p.Name,
                        AirlineName = a.Name,
                        CityName = c.Name,
                        ScheduledDateTime = f.ScheduledDateTime,
                        ActualDateTime = f.ActualDateTime ?? f.ScheduledDateTime,
                        StatusName = f.StatusName
                    }).ToList();
            }

            return savedFlights;
        }

        /// <summary>
        /// Удалить рейс
        /// </summary>
        /// <param name="flightId">ID рейса</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("RemoveFlight")]
        public string RemoveFlight(int flightId)
        {
            var flight = _db.Flights.FirstOrDefault(x => x.Id == flightId);
            if (flight == null)
                return $"Рейс с ID = {flightId} не был найден.";

            _db.Flights.Remove(flight);
            _db.SaveChanges();

            return "Success";
        }
    }
}