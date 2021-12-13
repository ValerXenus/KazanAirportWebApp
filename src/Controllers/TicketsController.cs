using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using KazanAirportWebApp.Logic;
using KazanAirportWebApp.Models.Data_Access;
using KazanAirportWebApp.Models.Join_Models;

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

                var connectionString = ConfigurationManager.ConnectionStrings["KazanAirportAdoConnection"].ConnectionString;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "Insert OpenQuery (AIRPORT_TICKETS, 'Select number, price, passengerId, flightId From tickets') " +
                                "Values (@ticketNumber, 3000, @passengerId, @flightId)";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ticketNumber", ticketNumber);
                        command.Parameters.Add("@passengerId", ticketItem.passengerId);
                        command.Parameters.Add("@flightId", ticketItem.flightId);
                        command.ExecuteNonQuery();
                    }
                }

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Получение пассажира по номеру паспорта
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetTicketsByPassengerId")]
        public List<PassengerTicket> GetTicketsByPassengerId(string passengerId)
        {
            try
            {
                List<PassengerTicket> passengersList;
                using (var db = new KazanAirportDbEntities())
                {
                    passengersList = db.Database.
                        SqlQuery<PassengerTicket>("Select * From PassengerTickets Where passengerId = @passengerId",
                            new SqlParameter("@passengerId", passengerId)).ToList();

                    if (passengersList.Count == 0)
                        return null;

                }

                return passengersList;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Покупка билета
        /// </summary>
        /// <param name="passportNumber">Номер паспорта пассажира</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("OnlineRegisterPassenger")]
        public string OnlineRegisterPassenger(string passportNumber)
        {
            try
            {
                var seatNumber = InfoGenerators.GenerateSeatNumber();

                var connectionString = ConfigurationManager.ConnectionStrings["KazanAirportAdoConnection"].ConnectionString;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "Exec RegisterPassenger @passportNumber = @passNum, @seatNumber = @seatNum";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@passNum", passportNumber);
                        command.Parameters.Add("@seatNum", seatNumber);
                        command.ExecuteNonQuery();
                    }
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