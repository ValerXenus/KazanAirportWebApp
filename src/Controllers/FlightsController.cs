using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KazanAirportWebApp.DataAccess;
using KazanAirportWebApp.Models;
using KazanAirportWebApp.Models.JoinModels;

namespace KazanAirportWebApp.Controllers
{
    public class FlightsController : ApiController
    {
        /// <summary>
        /// Получение списка рейсов
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetFlightsList")]
        public List<FlightItem> GetFlightsList()
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var flights = (from f in db.Flights
                    join p in db.Planes on f.PlaneId equals p.Id
                    join c in db.Cities on f.CityId equals c.Id
                    join a in db.Airlines on p.AirlineId equals a.Id
                    select new FlightItem
                    {
                        Id = f.Id,
                        FlightNumber = f.FlightNumber,
                        DepartureScheduled = f.DepartureScheduled,
                        ArrivalScheduled = f.ArrivalScheduled,
                        DepartureActual = f.DepartureActual,
                        ArrivalActual = f.ArrivalActual,
                        TimeOnBoard = f.TimeOnBoard,
                        FlightType = f.FlightType,
                        PlaneId = f.PlaneId,
                        CityId = f.CityId,
                        StatusId = f.StatusId,
                        ModelName = p.Name,
                        BoardNumber = p.Number,
                        CityName = c.Name,
                        AirlineId = p.AirlineId,
                        AirlineName = a.Name,
                        StatusName = "ToDo"
                    }).ToList();

                return flights;
            }
            catch(Exception exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Получение списка прилетающих рейсов
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetDepartureFlightsList")]
        public List<FlightItem> GetDepartureFlightsList()
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var flights = (from f in db.Flights
                    join p in db.Planes on f.PlaneId equals p.Id
                    join c in db.Cities on f.CityId equals c.Id
                    join a in db.Airlines on p.AirlineId equals a.Id
                    where f.FlightType == 0 
                          && f.DepartureScheduled > DateTime.Now.AddHours(-12) 
                          && f.DepartureScheduled < DateTime.Now.AddHours(12)
                               select new FlightItem
                    {
                        Id = f.Id,
                        FlightNumber = f.FlightNumber,
                        DepartureScheduled = f.DepartureScheduled,
                        ArrivalScheduled = f.ArrivalScheduled,
                        DepartureActual = f.DepartureActual,
                        ArrivalActual = f.ArrivalActual,
                        TimeOnBoard = f.TimeOnBoard,
                        FlightType = f.FlightType,
                        PlaneId = f.PlaneId,
                        CityId = f.CityId,
                        StatusId = f.StatusId,
                        ModelName = p.Name,
                        BoardNumber = p.Number,
                        CityName = c.Name,
                        AirlineId = p.AirlineId,
                        AirlineName = a.Name,
                        StatusName = "ToDo"
                    }).ToList();

                return flights;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Получение списка прилетающих рейсов
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetArrivalFlightsList")]
        public List<FlightItem> GetArrivalFlightsList()
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var flights = (from f in db.Flights
                    join p in db.Planes on f.PlaneId equals p.Id
                    join c in db.Cities on f.CityId equals c.Id
                    join a in db.Airlines on p.AirlineId equals a.Id
                    where f.FlightType == 1
                          && f.DepartureScheduled > DateTime.Now.AddHours(-12)
                          && f.DepartureScheduled < DateTime.Now.AddHours(12)
                    select new FlightItem
                    {
                        Id = f.Id,
                        FlightNumber = f.FlightNumber,
                        DepartureScheduled = f.DepartureScheduled,
                        ArrivalScheduled = f.ArrivalScheduled,
                        DepartureActual = f.DepartureActual,
                        ArrivalActual = f.ArrivalActual,
                        TimeOnBoard = f.TimeOnBoard,
                        FlightType = f.FlightType,
                        PlaneId = f.PlaneId,
                        CityId = f.CityId,
                        StatusId = f.StatusId,
                        ModelName = p.Name,
                        BoardNumber = p.Number,
                        CityName = c.Name,
                        AirlineId = p.AirlineId,
                        AirlineName = a.Name,
                        StatusName = "ToDo"
                    }).ToList();

                return flights;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Получение рейса по Id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetFlightById")]
        public FlightItem GetFlightById(int flightId)
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var flight = (from f in db.Flights
                    join p in db.Planes on f.PlaneId equals p.Id
                    join c in db.Cities on f.CityId equals c.Id
                    join a in db.Airlines on p.AirlineId equals a.Id
                    where f.Id == flightId
                    select new FlightItem
                    {
                        Id = f.Id,
                        FlightNumber = f.FlightNumber,
                        DepartureScheduled = f.DepartureScheduled,
                        ArrivalScheduled = f.ArrivalScheduled,
                        DepartureActual = f.DepartureActual,
                        ArrivalActual = f.ArrivalActual,
                        TimeOnBoard = f.TimeOnBoard,
                        FlightType = f.FlightType,
                        PlaneId = f.PlaneId,
                        CityId = f.CityId,
                        StatusId = f.StatusId,
                        ModelName = p.Name,
                        BoardNumber = p.Number,
                        CityName = c.Name,
                        AirlineId = p.AirlineId,
                        AirlineName = a.Name,
                        StatusName = "ToDo"
                    }).FirstOrDefault();

                return flight;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Добавить рейс
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddNewFlight")]
        public string AddNewFlight(DbFlight flight)
        {
            try
            {
                using var db = new KazanAirportDbContext();
                db.Flights.Add(flight);
                db.SaveChanges();

                return "New flight added successfully.";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Обновить данные рейса
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateFlight")]
        public string UpdateFlight(DbFlight flight)
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var currentFlight = db.Flights.FirstOrDefault(x => x.Id == flight.Id);
                if (currentFlight == null)
                    return $"Flight with ID = {flight.Id} wasn't found.";

                currentFlight.FlightNumber = flight.FlightNumber;
                currentFlight.DepartureScheduled = flight.DepartureScheduled;
                currentFlight.DepartureActual = flight.DepartureActual;
                currentFlight.ArrivalScheduled = flight.ArrivalScheduled;
                currentFlight.ArrivalActual = flight.ArrivalActual;
                currentFlight.TimeOnBoard = flight.TimeOnBoard;
                currentFlight.FlightType = flight.FlightType;
                currentFlight.PlaneId = flight.PlaneId;
                currentFlight.CityId = flight.CityId;
                currentFlight.StatusId = flight.StatusId;
                db.SaveChanges();

                return "Flight updated successfully.";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Удалить рейс
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("RemoveFlight")]
        public string RemoveFlight(int flightId)
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var flight = db.Flights.FirstOrDefault(x => x.Id == flightId);
                if (flight == null)
                    return $"Flight with ID = {flight.Id} wasn't found.";

                db.Flights.Remove(flight);
                db.SaveChanges();

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}