using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using KazanAirportWebApp.Logic;
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
    }
}