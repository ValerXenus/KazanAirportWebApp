using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KazanAirportWebApp.DataAccess;
using KazanAirportWebApp.Models.JoinModels;

namespace KazanAirportWebApp.Controllers
{
    public class SavedFlightsController : ApiController
    {
        /// <summary>
        /// Получение списка прилетающих рейсов
        /// </summary>
        /// <param name="todayOnly">Выбирать только сегодняшние</param>>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetDepartureFlightsList")]
        public List<FlightItem> GetDepartureFlightsList(bool todayOnly = false)
        {
            using var db = new KazanAirportDbContext();
            List<FlightItem> savedFlights;

            if (todayOnly)
            {
                savedFlights = (from f in db.Flights
                    where f.ScheduledDateTime > DateTime.Now
                    join p in db.Planes on f.PlaneId equals p.Id
                    join a in db.Airlines on p.AirlineId equals a.Id
                    join c in db.Cities on f.CityId equals c.Id
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
                savedFlights = (from f in db.Flights
                    join p in db.Planes on f.PlaneId equals p.Id
                    join a in db.Airlines on p.AirlineId equals a.Id
                    join c in db.Cities on f.CityId equals c.Id
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
            using var db = new KazanAirportDbContext();
            var flight = db.Flights.FirstOrDefault(x => x.Id == flightId);
            if (flight == null)
                return $"Рейс с ID = {flightId} не был найден.";

            db.Flights.Remove(flight);
            db.SaveChanges();

            return "Success";
        }
    }
}