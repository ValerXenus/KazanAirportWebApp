using System;
using System.Collections.Generic;
using System.Linq;
using KazanAirportWebApi.DataAccess;
using KazanAirportWebApi.Logic;
using KazanAirportWebApi.Models;
using KazanAirportWebApi.Models.JoinModels;
using Microsoft.AspNetCore.Mvc;

namespace KazanAirportWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TicketsController : ControllerBase
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly KazanAirportDbContext _db;

        public TicketsController(KazanAirportDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Покупка билета
        /// </summary>
        /// <param name="ticketItem">Добавляемый пассажир</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("CreateTicket")]
        public string CreateTicket(TicketItem ticketItem)
        {
            try
            {
                var ticketNumber = InfoGenerators.GenerateTicketNumber();

                _db.Tickets.Add(new DbTicket
                {
                    FlightId = ticketItem.FlightId,
                    PassengerId = ticketItem.PassengerId,
                    Price = 3000,
                    TicketNumber = ticketNumber
                });
                _db.SaveChanges();

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Получение списка билетов по пассажиру
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetTicketsByPassengerId")]
        public List<PassengerTicket> GetTicketsByPassengerId(int passengerId)
        {
            try
            {
                var tickets = (from t in _db.Tickets
                    join f in _db.Flights on t.FlightId equals f.Id
                    join c in _db.Cities on f.CityId equals c.Id
                    join p in _db.Planes on f.PlaneId equals p.Id
                    join a in _db.Airlines on p.AirlineId equals a.Id
                    where t.PassengerId == passengerId 
                          && t.PassengerId != -1
                          && f.ScheduledDateTime >= DateTime.Now
                    select new PassengerTicket
                    {
                        PassengerId = passengerId,
                        TicketNumber = t.TicketNumber,
                        FlightNumber = f.FlightNumber,
                        DepartureScheduled = f.ScheduledDateTime,
                        AirlineName = a.Name,
                        CityName = c.Name,
                        SeatNumber = t.SeatNumber
                    }).ToList();

                return tickets;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Онлайн регистрация пассажира
        /// </summary>
        /// <param name="passportNumber">Номер паспорта пассажира</param>
        /// <param name="ticketNumber">Номер билета</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("OnlineRegisterPassenger")]
        public string OnlineRegisterPassenger(string passportNumber, string ticketNumber)
        {
            try
            {
                var currentTicket = (from t in _db.Tickets
                    join p in _db.Passengers on t.PassengerId equals p.Id
                    where t.TicketNumber == ticketNumber && p.PassportNumber == passportNumber
                    select t)
                    .FirstOrDefault();
                if (currentTicket == null)
                    return $"Билет с номером {ticketNumber} и указанным номером паспорта {passportNumber} не найден";

                var ticket = _db.Tickets.FirstOrDefault(x => x.Id == currentTicket.Id);
                if (ticket == null)
                    return $"Ticket with ID = {currentTicket.Id} wasn't found";

                if (!string.IsNullOrEmpty(ticket.SeatNumber))
                    return "Вы уже прошли онлайн-регистрацию. Повторно ее проходить не требуется.";

                ticket.SeatNumber = InfoGenerators.GenerateSeatNumber();
                _db.SaveChanges();

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}