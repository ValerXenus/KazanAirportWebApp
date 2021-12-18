﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KazanAirportWebApp.DataAccess;
using KazanAirportWebApp.Logic;
using KazanAirportWebApp.Models;
using KazanAirportWebApp.Models.JoinModels;

namespace KazanAirportWebApp.Controllers
{
    public class TicketsController : ApiController
    {
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

                using var db = new KazanAirportDbContext();
                db.Tickets.Add(new DbTicket
                {
                    FlightId = ticketItem.FlightId,
                    PassengerId = ticketItem.PassengerId,
                    Price = 3000,
                    TicketNumber = ticketNumber
                });
                db.SaveChanges();

                return
                    $"Passenger with ID = {ticketItem.PassengerId} successfully added to the flight with ID = {ticketItem.FlightId}";
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
                using var db = new KazanAirportDbContext();
                var tickets = (from t in db.Tickets
                    join f in db.Flights on t.FlightId equals f.Id
                    join c in db.Cities on f.CityId equals c.Id
                    join p in db.Planes on f.PlaneId equals p.Id
                    join a in db.Airlines on p.AirlineId equals a.Id
                    where t.PassengerId == passengerId
                    select new PassengerTicket
                    {
                        PassengerId = passengerId,
                        TicketNumber = t.TicketNumber,
                        FlightNumber = f.FlightNumber,
                        DepartureScheduled = f.DepartureScheduled,
                        ArrivalScheduled = f.ArrivalScheduled,
                        AirlineName = a.Name,
                        CityName = c.Name
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
        /// <param name="ticketId">Идентификатор билета</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("OnlineRegisterPassenger")]
        public string OnlineRegisterPassenger(string passportNumber, int ticketId)
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var currentTicket = (from t in db.Tickets
                    join p in db.Passengers on t.PassengerId equals p.Id
                    where t.Id == ticketId && p.PassportNumber == passportNumber
                    select t)
                    .FirstOrDefault();
                if (currentTicket == null)
                    return $"Ticket with ID = {ticketId} and passenger's passport number {passportNumber} wasn't found";

                var ticket = db.Tickets.FirstOrDefault(x => x.Id == ticketId);
                if (ticket == null)
                    return $"Ticket with ID = {ticketId} wasn't found";

                ticket.SeatNumber = InfoGenerators.GenerateSeatNumber();
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