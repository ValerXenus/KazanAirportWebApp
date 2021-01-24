using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using KazanAirportWebApp.Models.Data_Access;
using KazanAirportWebApp.Models.Join_Models;

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
                List<FlightItem> flights;
                using (var db = new KazanAirportDbEntities())
                {
                    flights = db.Database
                        .SqlQuery<FlightItem>("Select * From dbo.Flights as F " +
                                              "join dbo.Planes as P on F.planeId = P.id " +
                                              "join dbo.Cities as C on F.cityId = C.id " +
                                              "join dbo.FlightStatuses as FS on F.statusId = FS.id " +
                                              "join dbo.Airlines as A on P.airlineId = A.id").ToList();
                }

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
                List<FlightItem> flights;
                using (var db = new KazanAirportDbEntities())
                {
                    var notBefore = DateTime.Now.AddHours(-12);
                    var notAfter = DateTime.Now.AddHours(12);

                    flights = db.Database
                        .SqlQuery<FlightItem>("Select * From dbo.Flights as F " +
                                              "join dbo.Planes as P on F.planeId = P.id " +
                                              "join dbo.Cities as C on F.cityId = C.id " +
                                              "join dbo.FlightStatuses as FS on F.statusId = FS.id " +
                                              "join dbo.Airlines as A on P.airlineId = A.id " +
                                              "Where flightType = 0 and departureScheduled > @notBefore " +
                                              "and departureScheduled < @notAfter",
                                              new SqlParameter("@notBefore", DateTime.Now.AddHours(-12)),
                                              new SqlParameter("@notAfter", DateTime.Now.AddHours(12))).ToList();
                }

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
                List<FlightItem> flights;
                using (var db = new KazanAirportDbEntities())
                {
                    flights = db.Database
                        .SqlQuery<FlightItem>("Select * From dbo.Flights as F " +
                                              "join dbo.Planes as P on F.planeId = P.id " +
                                              "join dbo.Cities as C on F.cityId = C.id " +
                                              "join dbo.FlightStatuses as FS on F.statusId = FS.id " +
                                              "join dbo.Airlines as A on P.airlineId = A.id " +
                                              "Where flightType = 1 and departureScheduled > @notBefore " +
                                              "and departureScheduled < @notAfter",
                                              new SqlParameter("@notBefore", DateTime.Now.AddHours(-12)),
                                              new SqlParameter("@notAfter", DateTime.Now.AddHours(12))).ToList();
                }

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
                List<FlightItem> flights;
                using (var db = new KazanAirportDbEntities())
                {
                    flights = db.Database.SqlQuery<FlightItem>("Select * From dbo.Flights as F " +
                                                               "join dbo.Planes as P on F.planeId = P.id " +
                                                               "join dbo.Cities as C on F.cityId = C.id " +
                                                               "join dbo.FlightStatuses as FS on F.statusId = FS.id " +
                                                               "Where F.id = @id",
                        new SqlParameter("@id", flightId)).ToList();
                }

                return flights.Count == 0 ? null : flights.First();
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
        public string AddNewFlight(Flights flight)
        {
            try
            {
                if (flight.departureActual == null)
                    flight.departureActual = new DateTime(2000, 1, 1, 0, 0, 0);

                if (flight.arrivalActual == null)
                    flight.arrivalActual = new DateTime(2000, 1, 1, 0, 0, 0);

                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand(
                        "Insert Into dbo.Flights(flightNumber, departureScheduled, arrivalScheduled, " +
                        "departureActual, arrivalActual, timeOnBoard, flightType, planeId, cityId, statusId) " +
                        "Values (@flightNumber, @departureScheduled, @arrivalScheduled, @departureActual, " +
                        "@arrivalActual, @timeOnBoard, @flightType, @planeId, @cityId, @statusId)",
                        new SqlParameter("@flightNumber", flight.flightNumber),
                        new SqlParameter("@departureScheduled", flight.departureScheduled),
                        new SqlParameter("@arrivalScheduled", flight.arrivalScheduled),
                        new SqlParameter("@departureActual", flight.departureActual),
                        new SqlParameter("@arrivalActual", flight.arrivalActual),
                        new SqlParameter("@timeOnBoard", flight.timeOnBoard),
                        new SqlParameter("@flightType", flight.flightType),
                        new SqlParameter("@planeId", flight.planeId),
                        new SqlParameter("@cityId", flight.cityId),
                        new SqlParameter("@statusId", flight.statusId));
                }

                return "Success";
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
        public string UpdateFlight(Flights flight)
        {
            try
            {
                if (flight.departureActual == null)
                    flight.departureActual = new DateTime(2000, 1, 1, 0, 0, 0);

                if (flight.arrivalActual == null)
                    flight.departureActual = new DateTime(2000, 1, 1, 0, 0, 0);

                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand(
                        "Update dbo.Flights Set flightNumber = @flightNumber, departureScheduled = @departureScheduled, " +
                        "arrivalScheduled = @arrivalScheduled, departureActual = @departureActual, " +
                        "arrivalActual = @arrivalActual, timeOnBoard = @timeOnBoard, flightType = @flightType, " +
                        "planeId = @planeId, cityId = @cityId, statusId = @statusId Where id = @id",
                        new SqlParameter("@flightNumber", flight.flightNumber),
                        new SqlParameter("@departureScheduled", flight.departureScheduled),
                        new SqlParameter("@arrivalScheduled", flight.arrivalScheduled),
                        new SqlParameter("@departureActual", flight.departureActual),
                        new SqlParameter("@arrivalActual", flight.arrivalActual),
                        new SqlParameter("@timeOnBoard", flight.timeOnBoard),
                        new SqlParameter("@flightType", flight.flightType),
                        new SqlParameter("@planeId", flight.planeId),
                        new SqlParameter("@cityId", flight.cityId),
                        new SqlParameter("@statusId", flight.statusId),
                        new SqlParameter("@id", flight.id));
                }

                return "Success";
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
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand("Delete From dbo.Flights where id = @id",
                        new SqlParameter("@id", flightId));
                }

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}