using System;
using System.Data.SqlClient;
using System.Web.Http;
using KazanAirportWebApp.Logic;
using KazanAirportWebApp.Models.Data_Access;
using KazanAirportWebApp.Models.Join_Models;

namespace KazanAirportWebApp.Controllers
{
    public class TicketController : ApiController
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
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database
                        .ExecuteSqlCommand("Insert OpenQuery(KAZAN_AIRPORT_TICKETS, 'Select number, price, passengerId, flightId From tickets') " +
                                           "Values (@ticketNumber, 3000, @passengerId, @flightId)",
                        new SqlParameter("@ticketNumber", ticketNumber),
                        new SqlParameter("@passengerId", ticketItem.passengerId),
                        new SqlParameter("@flightId", ticketItem.flightId));
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