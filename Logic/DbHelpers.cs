using System;
using System.Data.SqlClient;
using System.Linq;
using KazanAirportWebApp.Models.Data_Access;

namespace KazanAirportWebApp.Logic
{
    public class DbHelpers
    {
        public static string AddPassengerToUser(int passengerPassport, int userId)
        {
            var outcome = "";

            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    var passengers = db.Database
                        .SqlQuery<Passengers>("Select * From dbo.Passengers Where passportNumber = @passportNumber",
                            new SqlParameter("@passengerPassport", passengerPassport)).ToList();
                    if (passengers.Count == 0)
                        return "Пассажир с таким номером паспорта не найден";

                    // ToDo: 1. Проверить, что указанный индекс пассажира еще не прицеплен ни к одному пользователю
                    // ToDo: 2. Проверить, что на указанного пользователя еще не повешен id какого-либо пассажира

                }
            }
            catch (Exception exception)
            {
                return exception.Message;
            }

            return outcome;
        }
    }
}